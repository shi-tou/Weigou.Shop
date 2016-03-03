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
    public class AlipayConfigInfo : IConfigInfo
    {
        #region 私有字段

        private string partner;
        /// <summary>
        /// 合作商户ID。用签约支付宝账号登录ms.alipay.com后，在账户信息页面获取。  
        /// </summary>
        public string Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        private string seller;
        /// <summary>
        /// 账户ID。用签约支付宝账号登录ms.alipay.com后，在账户信息页面获取。  
        /// </summary>
        public string Seller 
        {
            get { return seller; }
            set { seller = value; }
        }

        private string key;
        /// <summary>
        /// 32位安全交易码
        /// </summary>
        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        private string privateKey;
       /// <summary>
        /// 商户（RSA）私钥 ,即rsa_private_key.pem中去掉首行，最后一行，空格和换行最后拼成一行的字符串  
        /// </summary>
        public string PrivateKey 
        {
            get { return privateKey; }
            set { privateKey = value; }
        }

        private string publicKey;
        /// <summary>
        /// 支付宝（RSA）公钥  用签约支付宝账号登录ms.alipay.com后，在密钥管理页面获取。  
        /// </summary>
        public string PublicKey
        {
            get { return publicKey; }
            set { publicKey = value; }
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
        public AlipayConfigInfo()
        {
        }

    }
}
