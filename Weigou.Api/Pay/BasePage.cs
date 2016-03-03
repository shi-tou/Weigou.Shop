using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;
using System.Globalization;
using System.Net;
using System.IO;
using Weigou.Common;
using Weigou.Service;
using Weigou.Model;
using System.Text;

namespace Weigou.Api
{
    public class BasePage : System.Web.UI.Page
    {
        #region IOC注入

        public static ISysService sysService;
        public ISysService SysService
        {
            set { sysService = value; }
            get { return sysService; }
        }

        //注入
        public static IMerchantService merchantService;
        public IMerchantService MerchantService
        {
            set { merchantService = value; }
        }

        //注入
        public static IGoodsService goodsService;
        public IGoodsService GoodsService
        {
            set { goodsService = value; }
        }

        //注入
        public static IMemberService memberService;
        public IMemberService MemberService
        {
            set { memberService = value; }
        }

        //注入
        public static IReportService reportService;
        public IReportService ReportService
        {
            set { reportService = value; }
        }

        //注入
        public static ISmsService smsService;
        public ISmsService SmsService
        {
            set { smsService = value; }
        }

        //注入
        public static IContentService contentService;
        public IContentService ContentService
        {
            set { contentService = value; }
        }

        //注入
        public static IOrderService orderService;
        public IOrderService OrderService
        {
            set { orderService = value; }
        }
        #endregion

        #region UI相关
        /// <summary>
        /// 构建JS引用
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public HtmlGenericControl RegistJavaScript(string src)
        {
            HtmlGenericControl generic = new HtmlGenericControl("script");
            generic.Attributes["type"] = "text/javascript";
            generic.Attributes["src"] = src;
            return generic;
        }

        /// <summary>
        /// 构建CSS样式引用
        /// </summary>
        /// <param name="href">链接地址</param>
        /// <returns></returns>
        public static HtmlLink RegistCSS(string href)
        {
            HtmlLink generic = new HtmlLink();
            generic.Href = href;
            generic.Attributes.Add("rel", "stylesheet");
            generic.Attributes.Add("type", "text/css");
            generic.Attributes["href"] = href;
            return generic;
        }

        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="message"></param>
        public void Alert(string message)
        {
            //System.Web.UI.ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), " ", "alert('" + message + "');", true);
            this.ClientScript.RegisterStartupScript(Page.GetType(), null, "alert('" + message + "');", true);
        }
        /// <summary>
        /// 提示消息并跳转
        /// </summary>
        /// <param name="strScript"></param>
        public void AlertAndJump(string message, string url)
        {
            this.ClientScript.RegisterStartupScript(Page.GetType(), null, "alert('" + message + "');window.location.href='"+ url +"';", true);
        }
        /// <summary>
        /// 输出JavaScript
        /// </summary>
        /// <param name="strScript"></param>
        public void InvokeScript(string strScript)
        {
            this.ClientScript.RegisterStartupScript(Page.GetType(), null, strScript, true);
        }
        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="message"></param>
        public void AlertInfoAndJump(string message,string url)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), " ", "alert('" + message + "');window.location.href='" + url + "'", true);
        }
        #endregion

        #region Request参数
        /// <summary>
        /// 获得request参数的string类型值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>参数的string类型值</returns>
        public string GetRequest(string strName, string defaultValue)
        {
            string vaule = Convert.ToString(HttpContext.Current.Request[strName]);
            if (vaule != null && vaule != "")
            {
                return vaule;
            }
            else
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 获得request参数的int类型值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>参数的int类型值</returns>
        public int GetRequest(string strName, int defaultValue)
        {
            string vaule = HttpContext.Current.Request[strName];
            if (vaule != null && vaule != "")
                return Convert.ToInt16(HttpContext.Current.Request[strName]);
            else
                return defaultValue;
        }
        /// <summary>
        /// 获得request参数的bool类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public bool GetRequest(string strName, bool defaultValue)
        {
            string vaule = HttpContext.Current.Request[strName];
            if (vaule != null && vaule != "")
                return Convert.ToBoolean(HttpContext.Current.Request[strName]);
            else
                return defaultValue;

        }
        /// <summary>
        /// 获得request参数的decimal值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的decimal类型值</returns>
        public decimal GetRequest(string strName,decimal defaultValue)
        {
            string vaule = HttpContext.Current.Request[strName];
            if (vaule != null && vaule != "")
                return Convert.ToDecimal(HttpContext.Current.Request[strName]);
            else
                return defaultValue;
        }
        #endregion
    }

}
