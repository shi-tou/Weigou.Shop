using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Weigou.Pay.Alipay;
using Weigou.Config;
using Weigou.Pay.Alipay.Refund;
using Weigou.Pay.Weixin.Pay;
using Weigou.Pay.Unionpay.Pay;

namespace Weigou.Manage.Order
{
    public partial class UnionpayRefund : ManagePage
    {
        /// <summary>
        /// ID
        /// </summary>
        public string _OrderNo
        {
            get
            {
                return GetRequest("OrderNo", "");
            }
        }

        public int total = 0;
        public int successtotal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            foreach (string str in _OrderNo.Split(','))
            {
                //退款批次号
                Random rnd = new Random();
                string batch_no = DateTime.Now.ToString("yyyyMMddHHmmss") + (rnd.Next(900) + 100).ToString().Trim();
                //必填，格式：当天日期[8位]+序列号[3至24位]，如：201008010000001
                DataTable orderDt = orderService.GetMallOrderReturnTradeNo(str).Tables[0];
                DataRow orderDr = orderDt.Rows[0];
                //插入数据至退款流水表
                DataTable dt = orderService.GetDataByKey("T_Refund", "OrderNo", str);
                DataRow dr;
                if (dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];
                }
                else
                {
                    dr = dt.NewRow();
                    dt.Rows.Add(dr);
                }

                dr["OrderNo"] = str;
                dr["BatchNo"] = batch_no;
                dr["TotalMoney"] = orderDr["TotalMoney"].ToString();
                dr["RefundSource"] = 2;

                dr["CreateTime"] = DateTime.Now;


                ////////////////////////////////////////////请求参数////////////////////////////////////////////

                // 使用Dictionary保存参数
                Dictionary<string, string> param = new Dictionary<string, string>();

                param["version"] = "5.0.0";//版本号
                param["encoding"] = "UTF-8";//编码方式
                param["certId"] = CertUtil.GetSignCertId();//证书ID
                param["signMethod"] = "01";//签名方法
                param["txnType"] = "04";//交易类型
                param["txnSubType"] = "00";//交易子类
                param["bizType"] = "000201";//业务类型
                param["accessType"] = "0";//接入类型
                param["channelType"] = "07";//渠道类型
                param["orderId"] = batch_no;//退款批次号
                param["merId"] = SDKConfig.MerId;//商户代码，请改成自己的测试商户号
                param["origQryId"] = orderDr["NotifyTradeNo"].ToString();//原消费的queryId，可以从查询接口或者通知接口中获取
                param["txnTime"] = DateTime.Now.ToString("yyyyMMddHHmmss");//订单发送时间，重新产生，不同于原消费

                //param["txnAmt"] = (Convert.ToDecimal(orderDr["TotalMoney"]) * 100).ToString();//订单总金额 真实环境请替换
                param["txnAmt"] = "1";//交易金额，消费撤销时需和原消费一致

                param["backUrl"] = SDKConfig.BackRefundTransUrl;  //退款异步通知地址


                string result = "";//返回结果
                SDKUtil.Sign(param, Encoding.UTF8);

                HttpClient hc = new HttpClient(SDKConfig.BackTransUrl);

                int status = hc.Send(param, Encoding.UTF8);
                if (status == 200)
                {
                    result = hc.Result;
                    if (result != null && string.IsNullOrEmpty(result))
                    {
                        Dictionary<string, string> resData = SDKUtil.CoverstringToDictionary(result);
                        string respcode = resData["respCode"];

                        if (SDKUtil.Validate(resData, Encoding.UTF8))
                        {
                            dr["RefundStatus"] = 1;
                            orderService.UpdateDataTable(dt);
                            successtotal++;
                        }
                        else
                        {
                            dr["RefundStatus"] = 0;
                            orderService.UpdateDataTable(dt);
                        }
                    }
                    else
                    {
                        result = hc.Result;
                        dr["RefundStatus"] = 0;
                        orderService.UpdateDataTable(dt);
                    }
                }
                else
                {
                    dr["RefundStatus"] = 0;
                    orderService.UpdateDataTable(dt);
                }
                total++;
            }
            Response.Write("申请退款总笔数:" + total + "笔,成功笔数:" + successtotal + "笔。退款处理完成，请关闭本页面！");

        }
    }
}
