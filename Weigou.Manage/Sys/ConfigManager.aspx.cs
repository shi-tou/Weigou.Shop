using Weigou.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Manage.Sys
{
    public partial class ConfigManager : ManagePage
    {
        private BaseConfigInfo BaseConfigInfo = BaseConfigs.GetConfig();
        private EmailConfigInfo EmailConfigInfo = EmailConfigs.GetConfig();
        /// <summary>
        /// tab选项卡Id
        /// </summary>
        public int TabIndex
        {
            get
            {
                return GetRequest("TabID", 1);
            }
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
                BindBaseInfo();
                BindEmailInfo();
            }            
        }

        #region 绑定相关
        /// <summary>
        /// 基本参数设置
        /// </summary>
        public void BindBaseInfo()
        {
            this.txtWebName.Text = BaseConfigInfo.Webname;
            this.txtTitle.Text = BaseConfigInfo.Title;
            this.txtKeywords.Text = BaseConfigInfo.Keywords;
            this.txtDescription.Text = BaseConfigInfo.Description;
            this.hfAvatar.Value = BaseConfigInfo.Logo;
            this.AvatarUrl.ImageUrl = BaseConfigInfo.Logo;
            this.txtFooter.Text = BaseConfigInfo.Footer;
        }
        /// <summary>
        /// 邮件设置
        /// </summary>
        public void BindEmailInfo()
        {
            this.txtSmtp.Text = EmailConfigInfo.Smtp;
            this.txtPort.Text = EmailConfigInfo.Port;
            this.txtUsername.Text = EmailConfigInfo.Username;
            this.txtSysemail.Text = EmailConfigInfo.Sysemail;
            this.txtPassword.Text = EmailConfigInfo.Password;
        }
       
        #endregion

        #region 更改配置事件
        /// <summary>
        /// 保存基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmitBase_Click(object sender, EventArgs e)
        {
            BaseConfigInfo.Webname = this.txtWebName.Text;
            BaseConfigInfo.Title = this.txtTitle.Text;
            BaseConfigInfo.Keywords = this.txtKeywords.Text;
            BaseConfigInfo.Description = this.txtDescription.Text;
            BaseConfigInfo.Logo = Upload(this.txtLogo, this.hfAvatar.Value, "Logo");
            BaseConfigInfo.Footer = this.txtFooter.Text;
            BaseConfigs.SaveConfig(BaseConfigInfo);
            Response.Redirect(Request.FilePath + "?TabID=1");
        }
        /// <summary>
        /// 保存邮件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmitEmail_Click(object sender, EventArgs e)
        {
            EmailConfigInfo.Smtp = this.txtSmtp.Text;
            EmailConfigInfo.Port = this.txtPort.Text;
            EmailConfigInfo.Username = this.txtUsername.Text;
            EmailConfigInfo.Sysemail = this.txtSysemail.Text;
            EmailConfigInfo.Password = this.txtPassword.Text;
            EmailConfigs.SaveConfig(EmailConfigInfo);
            Response.Redirect(Request.FilePath + "?TabID=2");
        }
        
        #endregion
    }
}