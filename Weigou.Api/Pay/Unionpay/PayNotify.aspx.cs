using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;
using Weigou.Pay.Alipay;
using Weigou.Service;
using System.Data;
using Weigou.Model.Enum;
using Weigou.Pay.Alipay.Pay;
using Weigou.Pay.Unionpay.Pay;
using System.Text;

namespace Weigou.Api.Pay.Unionpay
{
    public partial class PayNotify : System.Web.UI.Page
    {
        #region 注入
        public static IOrderService orderService;
        public IOrderService OrderService
        {
            set { orderService = value; }
            get { return orderService; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                // 使用Dictionary保存参数
                Dictionary<string, string> resData = new Dictionary<string, string>();

                NameValueCollection coll = Request.Form;

                string[] requestItem = coll.AllKeys;

                for (int i = 0; i < requestItem.Length; i++)
                {
                    resData.Add(requestItem[i], Request.Form[requestItem[i]]);
                }

                string respcode = resData["respCode"];

                // 返回报文中不包含UPOG,表示Server端正确接收交易请求,则需要验证Server端返回报文的签名
                if (SDKUtil.Validate(resData, Encoding.UTF8))
                {
                    //Response.Write("商户端验证返回报文签名成功\n");

                    string out_trade_no = resData["orderId"];
                    string trade_no = resData["queryId"];
                    //商户端根据返回报文内容处理自己的业务逻辑 ,DEMO此处只输出报文结果
                    try
                    {
                        orderService.PayNotify(out_trade_no, trade_no);
                        Utils.SaveLog("PayNotify->银联支付成功", "TRADE_SUCCESS");
                    }
                    catch (Exception ex)
                    {
                        Utils.SaveLog("PayNotify->银联支付失败", "Exception:" + ex.Message);
                    }           
                }
                else
                {
                    Utils.SaveLog("PayNotify->银联支付通知失败", "验证失败");
                    Response.Write("fail");
                }
            }
            else
            {
                Utils.SaveLog("PayNotify->银联支付通知失败", "请求方式错误");
                Response.Write("请求方式错误");
            }
        }

    }
}