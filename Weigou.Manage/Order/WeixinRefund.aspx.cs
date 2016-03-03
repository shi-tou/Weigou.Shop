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
using Weigou.Model.Enum;

/***
       * 申请退款完整业务流程逻辑
       * @param transaction_id 微信订单号（优先使用）
       * @param out_trade_no 商户订单号
       * @param total_fee 订单总金额
       * @param refund_fee 退款金额
       * @return 退款结果（xml格式）
       */
namespace Weigou.Manage.Order
{
    public partial class WeixinRefund : ManagePage
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

                Log.Info("Refund", "Refund is processing...");

                WxPayData data = new WxPayData();
                if (!string.IsNullOrEmpty(orderDr["NotifyTradeNo"].ToString()))//微信订单号存在的条件下，则已微信订单号为准
                {
                    data.SetValue("transaction_id", orderDr["NotifyTradeNo"].ToString());
                }
                else//微信订单号不存在，才根据商户订单号去退款
                {
                    data.SetValue("out_trade_no", orderDr["TradeNo"].ToString());
                }

                DataTable notifyDt = orderService.GetDataByKey("T_Payment", "NotifyTradeNo", orderDr["NotifyTradeNo"].ToString());
                if (notifyDt.Rows.Count > 0)
                {
                    DataRow notifyDr = notifyDt.Rows[0];

                    //data.SetValue("total_fee", Convert.ToDecimal(notifyDr["Amount"]) * 100);//订单总金额 真实环境请替换
                    data.SetValue("total_fee", 1);//订单总金额 测试环境 默认为1分

                }

                //data.SetValue("refund_fee", Convert.ToDecimal(orderDr["TotalMoney"]) * 100);//退款金额 真实环境请替换
                data.SetValue("refund_fee", 1);//退款金额 测试环境 默认为1分

                data.SetValue("out_refund_no", WxPayApi.GenerateOutTradeNo());//随机生成商户退款单号
                data.SetValue("op_user_id", WxPayConfig.MchID);//操作员，默认为商户号

                WxPayData result = WxPayApi.Refund(data);//提交退款申请给API，接收返回数据
                Log.Info("Refund", "Refund process complete, result : " + result.ToXml());

                string return_code = result.GetValue("return_code").ToString();//获得统一下单接口返回的结果
                if (return_code == "SUCCESS")
                {
                    dr["RefundStatus"] = 1;
                    orderService.UpdateDataTable(dt);
                    successtotal++;
                    orderService.RefundNotify(batch_no);

                }
                else
                {
                    dr["RefundStatus"] = 0;
                    orderService.UpdateDataTable(dt);
                }
                total++;
                //插入数据至日志表
                orderService.SaveSysLog(str.ToString(), EnumModule.OrderManage, EnumOperation.RefundMoney, UserInfo.ID, "退款");
            }
            Response.Write("申请退款总笔数:" + total + "笔,成功笔数:" + successtotal + "笔。退款处理完成，请关闭本页面！");

        }
    }
}
