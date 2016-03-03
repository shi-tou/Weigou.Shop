using System;
using System.Collections;
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
using Newtonsoft.Json.Converters;

namespace Weigou.Api
{
    public partial class test : System.Web.UI.Page
    {
        #region 注入
        public static ISysService sysService;
        public ISysService SysService
        {
            set { sysService = value; }
            get { return sysService; }
        }
        #endregion

        public string act
        {
            get { return RequestHelper.GetRequest("act", ""); }
        }

        #region 属性
        /// <summary>
        /// Api命名空间
        /// </summary>
        public static string ApiAssemblyName = "Weigou.Api.SDK";
        /// <summary>
        /// 需要会员登录App后方可调用的方法集
        /// </summary>
        public static List<string> op = new List<string>{ "MemberApi.GetMemberInfo", "MemberApi.UpdateMemberInfo", "MemberApi.GetDeliverAddressList", "MemberApi.GetDeliverAddresInfo"
                                       ,"MemberApi.SaveDeliverAddresInfo","MemberApi.SetDefaultDeliverAddres","MemberApi.GetFavoriteList"
                                       ,"MemberApi.DeleteFavorite","MemberApi.GetGoodsCommentsList","MemberApi.GetScoreList"
                                       ,"MemberApi.DonationScore","MemberApi.GetVerifyCodeForUpPwd","MemberApi.UpdateMemberPwd"
                                       ,"MemberApi.GetVerifyCodeForUpCardNo","MemberApi.UpdateMemberCardNo","MemberApi.GetInsideLetterList"
                                       ,"GoodsApi.AddFavorite","GoodsApi.AddGoodsComment","GoodsApi.AppendGoodsComment","ShoppingCartsApi.GetShoppingCartList"
                                       ,"ShoppingCartsApi.AddShoppingCart","ShoppingCartsApi.UpdateShoppingCart","ShoppingCartsApi.DeleteShoppingCart"
                                    };
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (act == "test")
                {
                    string _token = RequestHelper.GetRequest("token", "");
                    string _method = RequestHelper.GetRequest("method", "");
                    string param = HttpUtility.UrlDecode(RequestHelper.GetRequest("params", ""));
                    string _params = param == "" ? "" : Base64Helper.EncodeBase64("utf-8", param);
                    TestApi(_token, _method, _params);
                }
                else if (act == "GetVerifyCode")
                {
                    string mobileNo = RequestHelper.GetRequest("MobileNo", "");
                    DataTable dt = sysService.ExecteSqlGetDataTable(CommandType.Text, "select *from T_VerifyCode where MobileNo='" + mobileNo + "' and IsUsed=0 order by CreateTime desc");

                    if (dt.Rows.Count > 0)
                    {
                        string code = dt.Rows[0]["VerifyCode"].ToString();
                        Response.Write("{\"code\":\"" + code + "\"}");
                    }
                    else
                    {
                        Response.Write("{\"code\":\"没有验证码，请重新获取！\"}");
                    }
                    Response.End();
                }
                else
                {
                    BindClass();
                }
            }
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            //_token:令牌 _method:方法名 _params:参数串
            string _token = "";
            string _method = this.ddlMethod.SelectedItem.Text;
            string _params = this.txtParams.Text == "" ? "" : Base64Helper.EncodeBase64("utf-8", this.txtParams.Text); ;
            TestApi(_token, _method, _params);
        }

        /// <summary>
        /// 测试Api
        /// </summary>
        /// <param name="_token"></param>
        /// <param name="_method"></param>
        /// <param name="_params"></param>
        public void TestApi(string _token, string _method, string _params)
        {
            TestResult res = new TestResult();
            string url = Request.Url.ToString();
            res.url = url.Substring(0, url.LastIndexOf('?')) + "?method=" + _method + "&" + (_token == "" ? "" : ("token=" + _token)) + "params=" + _params;
            //验证需要会员登录方可调用的方法
            //if (Array.IndexOf<string>(op, _method) > 0)
            //{
            //#region =======验证Token=========
            //if (op.Contains(_method))
            //{
            //    #region =======验证Token=========
            //    DataTable dtToken = sysService.GetDataByKey("T_AppToken", "Token", _token);
            //    if (dtToken.Rows.Count != 1)
            //    {
            //        res.result.msg = "非法访问！";
            //        res.result.status = RT.RESULT_NOT_AUTHORIZED;//不存在
            //        Response.Write(JsonConvert.SerializeObject(res));
            //        return;
            //    }
            //    DateTime refreshTime = Convert.ToDateTime(dtToken.Rows[0]["RefreshTime"]);
            //    TimeSpan ts = DateTime.Now - refreshTime;
            //    if (ts.TotalHours > 12)
            //    {
            //        res.result.msg = "登陆失效！";
            //        res.result.status = RT.RESULT_NOT_AUTHORIZED;//过期
            //        Response.Write(JsonConvert.SerializeObject(res));
            //        return;
            //    }
            //    else
            //    {
            //        //刷新时间
            //        dtToken.Rows[0]["RefreshTime"] = DateTime.Now;
            //        sysService.UpdateDataTable(dtToken);
            //    }
            //    if (res.result.status != 0)
            //    {
            //        res.result.msg = "非法访问！";
            //        Response.Write(JsonConvert.SerializeObject(res));
            //    }
            //    #endregion
            //}
            //#endregion
            //}
            //调用接口
            try
            {
                int index = _method.IndexOf(".");
                string class_name = _method.Substring(0, index);
                string class_method = _method.Substring(index + 1);

                string asscemblyPath = ApiAssemblyName + "." + class_name;
                object obj = CreateApiInstance(asscemblyPath);
                //Utils.SaveLog("class_name", class_name);
                //Utils.SaveLog("class_method", class_method);
                //Utils.SaveLog("_params", _params);
                object obj2 = Type.GetType(asscemblyPath).GetMethod(class_method).Invoke(obj, new MyHashtable[] { GetMyHashtable(_params) });
                res.result = (Result)obj2;
                //处理序列化时间带"T"的情况
                IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
                timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
                Response.Write(JsonConvert.SerializeObject(res, timeFormat));
            }
            catch (Exception ex)
            {
                Utils.SaveLog("反射获取实例时异常", ex.Message + "\n" + ex.InnerException.Message);
                res.result.status = RT.RESULT_API_ERROR;
                res.result.msg = "服务异常！";
                Response.Write(JsonConvert.SerializeObject(res));
            }
            finally
            {
                Response.End();
            }
        }
        #region 公用方法
        /// <summary>
        /// 接口模块
        /// </summary>
        public void BindClass()
        {
            AssemblyResult result = AssemblyHandler.GetClass();
            foreach (string s in result.ClassName)
            {
                if (s.IndexOf("SDK") > -1)
                {
                    this.ddlClass.Items.Add(new ListItem(s.Replace(ApiAssemblyName+".", ""), s));
                }
            }
            BindMethod(this.ddlClass.SelectedValue);
        }
        /// <summary>
        /// 接口模块方法
        /// </summary>
        /// <param name="className"></param>
        public void BindMethod(string className)
        {
            AssemblyResult result = AssemblyHandler.GetClassInfo(className);
            for (int i = 0; i < result.Methods.Count; i++)
            {
                this.ddlMethod.Items.Add(new ListItem(className.Replace(ApiAssemblyName + ".", "") + "." + result.Methods[i], result.Methods[i] + "@" + result.Desc[i]));
                if (i == 0)
                {
                    this.labMethod.Text = className.Replace("Weigou.Api.SDK.", "") + "." + result.Methods[i];
                    this.txtParams.Text = result.Desc[i];
                }
            }
        }
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
                //Utils.SaveLog("asscemblyPath", asscemblyPath);
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
            //string[] arr = strParam.Split('&');
            //foreach (string s in arr)
            //{
            //    if (!string.IsNullOrEmpty(s))
            //    {
            //        string[] kv = s.Split('=');
            //        hs.Add(kv[0], kv[1]);
            //    }
            //}
            return hs;
        }
        #endregion

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlMethod.Items.Clear();
            BindMethod(this.ddlClass.SelectedValue);
        }
    }
    public class TestResult
    {
        public Result result = new Result();
        public string url;
    }
}