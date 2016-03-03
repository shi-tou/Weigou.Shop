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
using Weigou.Model.Enum;

/// <summary>
/// 功能：即时到账批量退款有密接口接入页
/// 版本：3.3
/// 日期：2012-07-05
/// 说明：
/// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
/// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
/// 
/// /////////////////注意///////////////////////////////////////////////////////////////
/// 如果您在接口集成过程中遇到问题，可以按照下面的途径来解决
/// 1、商户服务中心（https://b.alipay.com/support/helperApply.htm?action=consultationApply），提交申请集成协助，我们会有专业的技术工程师主动联系您协助解决
/// 2、商户帮助中心（http://help.alipay.com/support/232511-16307/0-16307.htm?sh=Y&info_type=9）
/// 3、支付宝论坛（http://club.alipay.com/read-htm-tid-8681712.html）
/// 
/// 如果不想使用扩展功能请把扩展功能参数赋空值。
/// </summary>
namespace Weigou.Manage.Order
{
    public partial class AliPayRefund : ManagePage
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

        protected void Page_Load(object sender, EventArgs e)
        {
            //退款详细数据
            string detail_data = "";
            //退款订单总笔数
            int refoundCount = 0;
            //退款批次号
            Random rnd = new Random();
            string batch_no = DateTime.Now.ToString("yyyyMMddHHmmss") + (rnd.Next(900) + 100).ToString().Trim();
            //必填，格式：当天日期[8位]+序列号[3至24位]，如：201008010000001
            foreach (string str in _OrderNo.Split(','))
            {
                DataTable orderDt = orderService.GetMallOrderReturnTradeNo(str).Tables[0];
                DataRow orderDr = orderDt.Rows[0];
                refoundCount++;
                if (detail_data != "")
                {
                    detail_data += "#";
                }
                //detail_data += orderDr["NotifyTradeNo"].ToString() + "^"+orderDr["TotalMoney"].ToString()+ "^买错东西了"; 真实环境请替换
                detail_data += orderDr["NotifyTradeNo"].ToString() + "^0.01" + "^买错东西了";

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
                dr["RefundStatus"] = 0;
                dr["CreateTime"] = DateTime.Now;
                orderService.UpdateDataTable(dt);

                //插入数据至日志表
                orderService.SaveSysLog(str.ToString(), EnumModule.OrderManage, EnumOperation.RefundMoney, UserInfo.ID, "退款");
            }

            ////////////////////////////////////////////请求参数////////////////////////////////////////////
            AlipayConfigInfo alipayConfigInfo = AlipayConfigs.GetConfig();

            //服务器异步通知页面路径
            string notify_url = alipayConfigInfo.RefundNotifyUrl;
            //需http://格式的完整路径，不允许加?id=123这类自定义参数

            //卖家支付宝帐户
            string seller_email = alipayConfigInfo.Seller;
            //必填

            //退款当天日期;
            string refund_date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //必填，格式：年[4位]-月[2位]-日[2位] 小时[2位 24小时制]:分[2位]:秒[2位]，如：2007-10-01 13:13:13

            //退款笔数
            string batch_num = refoundCount.ToString();
            //必填，参数detail_data的值中，“#”字符出现的数量加1，最大支持1000笔（即“#”字符出现的数量999个）


            //必填，具体格式请参见接口技术文档


            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", alipayConfigInfo.Partner);
            sParaTemp.Add("_input_charset", "utf-8");
            sParaTemp.Add("service", "refund_fastpay_by_platform_pwd");
            sParaTemp.Add("notify_url", notify_url);
            sParaTemp.Add("seller_email", seller_email);
            sParaTemp.Add("refund_date", refund_date);
            sParaTemp.Add("batch_no", batch_no);
            sParaTemp.Add("batch_num", batch_num);
            sParaTemp.Add("detail_data", detail_data);


            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "get", "确认");
            Response.Write(sHtmlText);

        }
    }
}
