using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Weixin
{
    public partial class WxHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //由微信服务接收请求，具体处理请求
                WxService wxService = new WxService(Request);
                string responseMsg = wxService.Response();
                Response.Clear();
                Response.Charset = "UTF-8";
                Response.Write(responseMsg);
                Response.End();
            }
        }
    }
}