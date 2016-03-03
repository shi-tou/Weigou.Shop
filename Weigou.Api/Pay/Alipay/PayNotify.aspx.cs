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

namespace Weigou.Api.Pay.Alipay
{
    /// <summary>
    /// 功能：服务器异步通知页面
    /// 版本：3.3
    /// 日期：2012-07-10
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// ///////////////////页面功能说明///////////////////
    /// 创建该页面文件时，请留心该页面文件中无任何HTML代码及空格。
    /// 该页面不能在本机电脑测试，请到服务器上做测试。请确保外部可以访问该页面。
    /// 该页面调试工具请使用写文本函数logResult。
    /// 如果没有收到该页面返回的 success 信息，支付宝会在24小时内按一定的时间策略重发通知
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
            //orderService.PayNotify("20151117113834", "2015111721001004830012411434");

            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {

                AlipayNotify aliNotify = new AlipayNotify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);
                //bool verifyResult = aliNotify.Verify(sPara, "3919d44848a655641e7b8c9b9bd75d7mek", "Rqv74a7G+k4fOavL/EEHFo3M7/KC/+BN36UgyphjQp1y2KY7LVVQBq7cfmpVRPIYYQXctatgJ5LC8DPx4jFpPwwudJkanOd6xZL8MZGPPmZgZNzpAAfvMch6QfWMBqU6HKy48JqqMDLQj7VP3FRfVN1azeJGxrHJMGcLAHbtcR8=");
                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //商户订单号
                    string out_trade_no = Request.Form["out_trade_no"];
                    //支付宝交易号
                    string trade_no = Request.Form["trade_no"];

                    //交易状态
                    string trade_status = Request.Form["trade_status"];

                    if (trade_status == "TRADE_FINISHED")
                    {


                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //该种交易状态只在两种情况下出现
                        //1、开通了普通即时到账，买家付款成功后。
                        //2、开通了高级即时到账，从该笔交易成功时间算起，过了签约时的可退款时限（如：三个月以内可退款、一年以内可退款等）后。
                    }
                    else if (trade_status == "TRADE_SUCCESS")
                    {
                        try
                        {
                            orderService.PayNotify(out_trade_no, trade_no);
                            Utils.SaveLog("PayNotify->支付宝支付成功", "TRADE_SUCCESS");
                        }
                        catch (Exception ex)
                        {
                            Utils.SaveLog("PayNotify->支付宝支付失败", "Exception:" + ex.Message);
                        }
                        
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //该种交易状态只在一种情况下出现——开通了高级即时到账，买家付款成功后。
                    }
                    else
                    {
                        Utils.SaveLog("PayNotify->支付宝支付失败", "trade_status:" + trade_status);
                        Response.Write("fail");
                    }

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——
                    Response.Write("success");  //请不要修改或删除
                }
                else//验证失败
                {
                    Utils.SaveLog("PayNotify->支付宝支付通知失败", "验证失败");
                    Response.Write("fail");
                }
            }
            else
            {
                Utils.SaveLog("PayNotify->支付宝支付通知失败", "无通知参数");
                Response.Write("无通知参数");
            }
        }
        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();

            //测试数据
            //string t = "body=为订单20151117113834进行付款&buyer_email=gx_ylb@126.com&buyer_id=2088602318017831&discount=0.00&gmt_create=2015-11-17 11:38:46&gmt_payment=2015-11-17 11:38:47&is_total_fee_adjust=N&notify_id=3919d44848a655641e7b8c9b9bd75d7mek&notify_time=2015-11-17 11:38:47&notify_type=trade_status_sync&out_trade_no=20151117113834&payment_type=1&price=0.01&quantity=1&seller_email=siigee@163.com&seller_id=2088021438063964&subject=为订单20151117113834进行付款&total_fee=0.01&trade_no=2015111721001004830012411434&trade_status=TRADE_SUCCESS&use_coupon=N&sign=Rqv74a7G+k4fOavL/EEHFo3M7/KC/+BN36UgyphjQp1y2KY7LVVQBq7cfmpVRPIYYQXctatgJ5LC8DPx4jFpPwwudJkanOd6xZL8MZGPPmZgZNzpAAfvMch6QfWMBqU6HKy48JqqMDLQj7VP3FRfVN1azeJGxrHJMGcLAHbtcR8=";
            //string[] arr = t.Split("&".ToArray());
            //foreach (string s in arr)
            //{
            //    sArray.Add(s.Split("=".ToArray())[0], s.Split("=".ToArray())[1]);
            //}

            int i = 0;
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;
            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }
            return sArray;
        }
    }
}