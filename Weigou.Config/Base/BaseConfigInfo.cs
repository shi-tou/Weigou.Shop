using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Config
{
   /// <summary>
	/// Email配置信息类
	/// </summary>
    [Serializable]
    public class BaseConfigInfo : IConfigInfo
    {
        #region 私有字段

        private string webname; //网站名称

        public string Webname
        {
            get { return webname; }
            set { webname = value; }
        }

        private string title;//标题

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string keywords;  //SEO关键词

        public string Keywords
        {
            get { return keywords; }
            set { keywords = value; }
        }

        private string description;  //SEO描述

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string logo;  //Logo

        public string Logo
        {
            get { return logo; }
            set { logo = value; }
        }

        private string footer;//底部内容

        public string Footer
        {
            get { return footer; }
            set { footer = value; }
        }
        private string userid;//短信企业ID

        public string UserID
        {
            get { return userid; }
            set { userid = value; }
        }

        private string password;//短信密码

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string account;//短信用户名

        public string Account
        {
            get { return account; }
            set { account = value; }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailConfigInfo"/> class.
        /// </summary>
        public BaseConfigInfo()
        {
        }

    }
}
