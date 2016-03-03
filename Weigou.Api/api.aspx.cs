using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Weigou.Api.Base;
using Weigou.Common;
using Weigou.Service;
using System.Collections;
using Newtonsoft.Json.Converters;

namespace Weigou.Api
{
    public partial class api : System.Web.UI.Page
    {
        #region 注入
        public static ISysService sysService;
        public ISysService SysService
        {
            set { sysService = value; }
            get { return sysService; }
        }
        #endregion

        #region 属性
        /// <summary>
        /// Api命名空间
        /// </summary>
        public static string ApiAssemblyName = "Weigou.Api.SDK";
        /// <summary>
        /// 需要会员登录App后方可调用的方法集
        /// </summary>
        public static List<string> op = new List<string> 
        { 
            //会员模块
            "MemberApi.GetMemberInfo", "MemberApi.UpdateMemberInfo", "MemberApi.UpdatePwdByOldPwd", "MemberApi.UpdatePwdByMobile" ,
            "MemberApi.UpdateReserveMobileNo","MemberApi.SaveMemberInfoForAuth","MemberApi.GetAccountBanlance","MemberApi.GetAccountList",
            "MemberApi.AddFavorite","MemberApi.GetFavoriteList","MemberApi.DeleteFavorite","MemberApi.AddGoodsComment",
            "MemberApi.SaveDeliverAddress","MemberApi.SetDefaultAddress","MemberApi.GetDeliverAddress","MemberApi.GetDeliverAddressInfo",
            "MemberApi.GetDefaultAddress","MemberApi.DeleteDeliverAddress",
            //租车订单模块
            "OrderApi.SubmitCarOrder","OrderApi.GetCarOrderList","OrderApi.GetCarOrderDetail","OrderApi.CancelCarOrder","OrderApi.DeleteCarOrder",
            "OrderApi.IllegalCarOrder","OrderApi.TravelStartCarOrder","OrderApi.FinishCarOrder","OrderApi.SettlementCarOrder",
            //商城订单模块
            "OrderApi.GetPrepayOrderInfo","OrderApi.SubmitMallOrder","OrderApi.GetMallOrderList",
            //商城购物车模块
            "ShoppingCartsApi.GetShoppingCartList","ShoppingCartsApi.AddShoppingCart","ShoppingCartsApi.UpdateShoppingCart",
            "ShoppingCartsApi.DeleteShoppingCart","ShoppingCartsApi.DeleteShoppingCart",
            //支付模块
            "PaymentApi.GetAlipayInfo","PaymentApi.GetWeixinInfo","PaymentApi.GetUnionpayInfo","PaymentApi.GetUnionpayPrePayInfo"
        };
        #endregion

        /// <summary>
        /// Api入口 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //_token:令牌 _method:方法名 _params:参数串
            string _token = RequestHelper.GetRequest("token", "");
            string _method = RequestHelper.GetRequest("method", "");
            string _params = RequestHelper.GetRequest("params", "");
            Result result = new Result();
            result.data = "{}";
            result.msg = "";
            Utils.SaveLog("请求参数：", _token + "-" + _method + "-" + _params);
            if (string.IsNullOrEmpty(_method))
            {
                result.status = RT.RESULT_ERROR_METHOD;
                result.msg = "方法名为空！";
                result.data = "[]";
                ResponseWrite(result);
                return;
            }

            //验证需要会员登录方可调用的方法
            if (op.Contains(_method))
            {
                #region =======验证Token=========
                DataTable dtToken = sysService.GetDataByKey("T_AppToken", "Token", _token);
                if (dtToken.Rows.Count != 1)
                {
                    result.msg = "非法访问！";
                    result.data = "[]";
                    result.status = RT.RESULT_NOT_AUTHORIZED;//不存在
                    ResponseWrite(result);
                    return;
                }
                DateTime refreshTime = Convert.ToDateTime(dtToken.Rows[0]["RefreshTime"]);
                TimeSpan ts = DateTime.Now - refreshTime;
                if (ts.TotalHours > 12)
                {
                    result.msg = "登陆失效！";
                    result.data = "[]";
                    result.status = RT.RESULT_NOT_AUTHORIZED;//过期
                    ResponseWrite(result);
                    return;
                }
                else
                {
                    //刷新时间
                    dtToken.Rows[0]["RefreshTime"] = DateTime.Now;
                    sysService.UpdateDataTable(dtToken);
                }
                #endregion
            }
            //调用接口
            try
            {
                int index = _method.IndexOf(".");
                string class_name = _method.Substring(0, index);
                string class_method = _method.Substring(index + 1);

                string asscemblyPath = ApiAssemblyName + "." + class_name;
                object obj = CreateApiInstance(asscemblyPath);
                MethodInfo mi = Type.GetType(asscemblyPath).GetMethod(class_method);
                if (mi == null)
                {
                    result.status = RT.RESULT_ERROR_METHOD;
                    result.msg = "找不到指定方法名！";
                    result.data = "[]";
                    ResponseWrite(result);
                }
                else
                {
                    object objResult = Type.GetType(asscemblyPath).GetMethod(class_method).Invoke(obj, new MyHashtable[] { GetMyHashtable(_params) });
                    //处理序列化时间带"T"的情况
                    IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                    timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                    Response.Write(JsonConvert.SerializeObject(objResult, timeFormat));
                }
            }
            catch (Exception ex)
            {
                Utils.SaveLog("调用CreateApiInstance方法反射获取实例时异常", ex.Message);
                result.status = RT.RESULT_API_ERROR;
                result.msg = "服务异常！";
                result.data = "[]";
                Response.Write(JsonConvert.SerializeObject(result));
            }
            finally
            {
                Response.End();
            }
        }

        #region 公用方法
        /// <summary>
        /// 反射获取实例
        /// </summary>
        /// <param name="className">类名</param>
        /// <returns></returns>
        public static object CreateApiInstance(string asscemblyPath)
        {
            object obj = null;
            try
            {
                obj = Assembly.GetExecutingAssembly().CreateInstance(asscemblyPath, true);
            }
            catch (Exception ex)
            {
                Utils.SaveLog("调用CreateApiInstance方法反射获取实例时异常", ex.Message);
            }
            return obj;
        }
        /// <summary>
        /// 构造参数（base64解码）
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static MyHashtable GetMyHashtable(string param)
        {
            MyHashtable hs = new MyHashtable();
            try
            {
                if (string.IsNullOrEmpty(param))
                    return hs;
                param = param.Replace(" ", "+");
                string strParam = Base64Helper.DecodeBase64("utf-8", param);
                Hashtable hsTemp = JsonConvert.DeserializeObject<Hashtable>(strParam);
                foreach (string key in hsTemp.Keys)
                {
                    hs.Add(key, hsTemp[key]);
                }
            }
            catch (Exception ex)
            {

                Utils.SaveLog("GetMyHashtable", ex.Message);
            }
            return hs;
        }
        /// <summary>
        /// 输出josn结果
        /// </summary>
        /// <param name="context"></param>
        /// <param name="result"></param>
        public void ResponseWrite(object result)
        {
            Response.Write(JsonConvert.SerializeObject(result));
            Response.End();
        }
        #endregion
    }
}