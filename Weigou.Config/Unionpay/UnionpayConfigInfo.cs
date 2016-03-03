using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Config
{
    /// <summary>
    /// 银联支付信息类
    /// </summary>
    [Serializable]
    public class UnionpayConfigInfo : IConfigInfo
    {
        #region 私有字段

        private string signcertpath;
        /// <summary>
        /// 签名证书路径 
        /// </summary>
        public string SignCertPath
        {
            get { return signcertpath; }
            set { signcertpath = value; }
        }
        private string signcertpwd;
        /// <summary>
        /// 签名证书密码
        /// </summary>
        public string SignCertPwd
        {
            get { return signcertpwd; }
            set { signcertpwd = value; }
        }
        private string validatecertdir;
        /// <summary>
        /// 验签目录
        /// </summary>
        public string ValidateCertDir
        {
            get { return validatecertdir; }
            set { validatecertdir = value; }
        }
        private string encryptCert;
        /// <summary>
        /// 加密公钥证书路径
        /// </summary>
        public string EncryptCert
        {
            get { return encryptCert; }
            set { encryptCert = value; }
        }
        private string cardrequesturl;
        /// <summary>
        /// 有卡交易路径
        /// </summary>
        public string CardRequestUrl
        {
            get { return cardrequesturl; }
            set { cardrequesturl = value; }
        }
        private string apprequesturl;
        /// <summary>
        /// app交易路径
        /// </summary>
        public string AppRequestUrl
        {
            get { return apprequesturl; }
            set { apprequesturl = value; }
        }
        private string fronttransurl;
        /// <summary>
        /// 前台同步通知地址
        /// </summary>
        public string FrontTransUrl
        {
            get { return fronttransurl; }
            set { fronttransurl = value; }
        }
        private string backtransurl;
        /// <summary>
        /// 后台异步通知地址
        /// </summary>
        public string BackTransUrl
        {
            get { return backtransurl; }
            set { backtransurl = value; }
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
        private string merid;
        /// <summary>
        /// 商户代码
        /// </summary>
        public string MerId
        {
            get { return merid; }
            set { merid = value; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailConfigInfo"/> class.
        /// </summary>
        public UnionpayConfigInfo()
        {
        }

    }
}
