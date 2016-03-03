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
    public class WeixinConfigInfo : IConfigInfo
    {
        #region 私有字段

        private string appid;
        /// <summary>
        /// 公众账号ID 
        /// </summary>
        public string AppID
        {
            get { return appid; }
            set { appid = value; }
        }

        private string appsecret;
        /// <summary>
        /// 公众号密钥
        /// </summary>
        public string AppSecret
        {
            get { return appsecret; }
            set { appsecret = value; }
        }

        private string mch_id;
        /// <summary>
        /// 商户号
        /// </summary>
        public string Mch_ID
        {
            get { return mch_id; }
            set { mch_id = value; }
        }

        private string sslcert_path;
        /// <summary>
        /// 证书路径
        /// </summary>
        public string Sslcert_Path
        {
            get { return sslcert_path; }
            set { sslcert_path = value; }
        }

        private string sslcert_password;
        /// <summary>
        /// 证书路径
        /// </summary>
        public string Sslcert_Password
        {
            get { return sslcert_password; }
            set { sslcert_password = value; }
        }

        private string key;
        /// <summary>
        /// 商户支付密钥
        /// </summary>
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        private string paynotifyUrl;
        /// <summary>
        /// 支付通知地址
        /// </summary>
        public string PayNotifyUrl
        {
            get { return paynotifyUrl; }
            set { paynotifyUrl = value; }
        }

        private string refundnotifyUrl;
        /// <summary>
        /// 退款通知地址
        /// </summary>
        public string RefundNotifyUrl
        {
            get { return refundnotifyUrl; }
            set { refundnotifyUrl = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailConfigInfo"/> class.
        /// </summary>
        public WeixinConfigInfo()
        {
        }

    }
}
