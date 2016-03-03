using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Config;

namespace Weigou.Manage.Weixin
{
    public partial class WeixinReply : ManagePage
    {
        private WeixinReplyConfigInfo WeixinReplyConfigInfo = WeixinReplyConfigs.GetConfig();
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtDefaultReply.Text = WeixinReplyConfigInfo.DefaultReply;
                this.txtSubscribeReply.Text = WeixinReplyConfigInfo.SubscribeReply;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            WeixinReplyConfigInfo.DefaultReply = this.txtDefaultReply.Text;
            WeixinReplyConfigInfo.SubscribeReply = this.txtSubscribeReply.Text;
            WeixinReplyConfigs.SaveConfig(WeixinReplyConfigInfo);
            ShowMsg(this.Request.Url.ToString(), "设置成功！");
        }
    }
}