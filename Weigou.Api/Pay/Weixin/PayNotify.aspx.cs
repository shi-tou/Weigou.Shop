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
using Weigou.Pay.Weixin.Pay;

namespace Weigou.Api.Pay.Weixin
{
    /// <summary>
    /// 支付结果通知回调处理类
    /// 负责接收微信支付后台发送的支付结果并对订单有效性进行验证，将验证结果反馈给微信支付后台
    /// </summary>
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
            Notify notify = new Notify(this.Page);
            WxPayData notifyData = notify.GetNotifyData();

            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "支付结果中微信订单号不存在");
                Log.Error(this.GetType().ToString(), "The Pay result is error : " + res.ToXml());
                Response.Write(res.ToXml());
                Response.End();
            }
            //微信支付交易号
            string transaction_id = notifyData.GetValue("transaction_id").ToString();
            //商户订单号
            string out_trade_no = notifyData.GetValue("out_trade_no").ToString();

            //查询订单，判断订单真实性
            if (!QueryOrder(transaction_id))
            {
                //若订单查询失败，则立即返回结果给微信支付后台
                WxPayData res = new WxPayData();
                res.SetValue("return_code", "FAIL");
                res.SetValue("return_msg", "订单查询失败");
                Log.Error(this.GetType().ToString(), "Order query failure : " + res.ToXml());
                Response.Write(res.ToXml());
                Response.End();
            }
            //查询订单成功
            else
            {
                try
                {
                    orderService.PayNotify(out_trade_no, transaction_id);
                    Utils.SaveLog("PayNotify->微信支付成功", "TRADE_SUCCESS");
                }
                catch (Exception ex)
                {
                    Utils.SaveLog("PayNotify->微信支付失败", "Exception:" + ex.Message);
                }                          

                WxPayData res = new WxPayData();
                res.SetValue("return_code", "SUCCESS");
                res.SetValue("return_msg", "OK");
                Log.Info(this.GetType().ToString(), "order query success : " + res.ToXml());
                Response.Write(res.ToXml());
                Response.End();
            }
        }

        //查询订单
        private bool QueryOrder(string transaction_id)
        {
            WxPayData req = new WxPayData();
            req.SetValue("transaction_id", transaction_id);
            WxPayData res = WxPayApi.OrderQuery(req);
            if (res.GetValue("return_code").ToString() == "SUCCESS" &&
                res.GetValue("result_code").ToString() == "SUCCESS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}