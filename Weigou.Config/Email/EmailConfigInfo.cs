using System;

namespace Weigou.Config
{
	/// <summary>
	/// Email配置信息类
	/// </summary>
	[Serializable]
    public class EmailConfigInfo : IConfigInfo
    {
        #region 私有字段

        private string smtp; //smtp 地址

		private string port ; //端口号

		private string sysemail;  //系统邮件地址

		private string username;  //邮件帐号

		private string password;  //邮件密码

        private int emailnotiftime = 0;//发送团购邮件通知时间

        private int mailon = 1;

        private string replyaddress;

        private int smtpauth = 0;

        private int isssl=0;

        private int sendpaidmail = 0;

        private int senddeliverymail = 0;

        private int sendgroupmail = 0;

        private int sendnodemail = 0;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailConfigInfo"/> class.
        /// </summary>
        public EmailConfigInfo()
		{
        }

        #region 属性

        /// <summary>
		/// smtp服务器
		/// </summary>
		public string Smtp
		{
			get { return smtp;}
			set { smtp = value;}
		}

		/// <summary>
		/// 端口号
		/// </summary>
		public string Port
		{
			get { return port;}
			set { port = value;}
		}
		

		/// <summary>
		/// 系统Email地址
		/// </summary>
		public string Sysemail
		{
			get { return sysemail;}
			set { sysemail = value;}
		}


		/// <summary>
		/// 用户名
		/// </summary>
		public string Username
		{
			get { return username;}
			set { username = value;}
		}

		/// <summary>
		/// 密码
		/// </summary>
		public string Password
		{
			get { return password;}
			set { password = value;}
		}

        /// <summary>
        /// 邮件服务
        /// </summary>
        public int MailOn
        {
            get { return mailon; }
            set { mailon = value; }
        }

        /// <summary>
        /// 回复地址
        /// </summary>
        public string ReplyAddress
        {
            get { return replyaddress; }
            set { replyaddress = value; }
        }

        /// <summary>
        /// SMTP验证
        /// </summary>
        public int SmtpAuth
        {
            get { return smtpauth; }
            set { smtpauth = value; }
        }

        /// <summary>
        /// SSL连接加密
        /// </summary>
        public int IsSsl
        {
            get { return isssl; }
            set { isssl = value; }
        }

        /// <summary>
        /// 收款邮件通知
        /// </summary>
        public int SendPaidMail
        {
            get { return sendpaidmail; }
            set { sendpaidmail = value; }
        }

        /// <summary>
        /// 发货邮件通知
        /// </summary>
        public int SendDeliveryMail
        {
            get { return senddeliverymail; }
            set { senddeliverymail = value; }
        }
        /// <summary>
        /// 服务节点通知
        /// </summary>
        public int SendNodeMail
        {
            get { return sendnodemail; }
            set { sendnodemail = value; }
        }

        /// <summary>
        /// 团购券邮件通知
        /// </summary>
        public int SendGroupMail
        {
            get { return sendgroupmail; }
            set { sendgroupmail = value; }
        }

        /// <summary>
        /// 发送团购邮件通知时间
        /// </summary>
        public int EmailNotifTime
        {
            get { return emailnotiftime; }
            set { emailnotiftime = value; }
        }
        #endregion

    }
}
