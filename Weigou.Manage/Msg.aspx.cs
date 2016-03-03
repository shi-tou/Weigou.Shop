using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Manage
{
    public partial class Msg : System.Web.UI.Page
    {
        //跳转URL
        public string strUrl
        {
            get { return Request.QueryString["Url"].ToString(); }
        }
        //提示消息
        public string strMsg
        {
            get { return Request.QueryString["Msg"].ToString(); }
        }
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.labMsg.Text = strMsg;
                this.hfUrl.Value = strUrl;
            }
        }
    }
}