using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Weixin
{
    public partial class AppDownload : System.Web.UI.Page
    {
        #region 属性
        /// <summary>
        /// 1-客户端 2-管家端
        /// </summary>
        public int Type
        {
            get
            {
                return GetRequest("t", 1);
            }
        }
        Hashtable hs = new Hashtable();
        #endregion

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        #region
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
        #endregion
    }
}