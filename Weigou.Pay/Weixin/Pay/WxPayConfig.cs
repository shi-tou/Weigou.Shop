using Weigou.Config;
using System;
using System.Collections.Generic;
using System.Web;

namespace Weigou.Pay.Weixin.Pay
{
    /**
    * 	配置账号信息
    */
    public class WxPayConfig
    {
        #region 字段
        private static string appid = "";
        private static string appsecret = "";
        private static string mchid = "";
        private static string key = "";
        private static string apikey ="";
        private static string sslcert_path = "";
        private static string sslcert_password = "";
        private static string paynotify_url = "";
        private static string ip = "";
        private static string proxy_url = "";
        private static int report_levenl = 1;
        private static int log_levenl = 0;
        
        #endregion

        static WxPayConfig()
        {
            //=======【基本信息设置】=====================================
            /* 微信公众号信息配置
            * APPID：绑定支付的APPID（必须配置）
            * MCHID：商户号（必须配置）
            * KEY：商户支付密钥，参考开户邮件设置（必须配置）
            * APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置）
            * IP：商户系统后台机器IP（此参数可手动配置也可在程序中自动获取）
            * PROXY_URL：代理服务器IP（默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置））
            * REPORT_LEVENL：测速上报（0.关闭上报; 1.仅错误时上报; 2.全量上报）
            * LOG_LEVENL：日志级别（0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息）
            * SSLCERT_PATH：证书路径（注意应该填写绝对路径，仅退款、撤销订单时需要）
            * SSLCERT_PASSWORD：证书密码（商户号）
            */

            WeixinConfigInfo weixinConfigInfo = WeixinConfigs.GetConfig();
            appid = weixinConfigInfo.AppID;
            appsecret = weixinConfigInfo.AppSecret;
            mchid = weixinConfigInfo.Mch_ID;
            key = weixinConfigInfo.Key;
            paynotify_url = weixinConfigInfo.PayNotifyUrl;
            sslcert_path = weixinConfigInfo.Sslcert_Path;
            sslcert_password = weixinConfigInfo.Sslcert_Password;
            ip = "8.8.8.8";
            proxy_url = "0.0.0.0:0";
            report_levenl = 1;
            log_levenl = 1;
        }

        #region 属性
        /// <summary>
        /// 公众号ID
        /// </summary>
        public static string AppID
        {
            get { return appid; }
            set { appid = value; }
        }
        /// <summary>
        /// 公众号密钥
        /// </summary>
        public static string AppSecret
        {
            get { return appsecret; }
            set { appsecret = value; }
        }

        /// <summary>
        /// 商户号
        /// </summary>
        public static string MchID
        {
            get { return mchid; }
            set { mchid = value; }
        }

        /// <summary>
        /// 商户支付密钥
        /// </summary>
        public static string Key
        {
            get { return key; }
            set { key = value; }
        }
        /// <summary>
        /// ApiKey
        /// </summary>
        public static string ApiKey
        {
            get { return apikey; }
            set { apikey = value; }
        }

        /// <summary>
        /// 证书路径
        /// </summary>
        public static string Sslcert_Path
        {
            get { return sslcert_path; }
            set { sslcert_path = value; }
        }

        /// <summary>
        /// 证书密码
        /// </summary>
        public static string Sslcert_Password
        {
            get { return sslcert_password; }
            set { sslcert_password = value; }
        }

        /// <summary>
        /// 支付异步通知地址
        /// </summary>
        public static string PayNotify_Url
        {
            get { return paynotify_url; }
            set { paynotify_url = value; }
        }

        /// <summary>
        /// 终端设备IP
        /// </summary>
        public static string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        /// <summary>
        /// 代理服务器IP
        /// </summary>
        public static string Proxy_Url
        {
            get { return proxy_url; }
            set { proxy_url = value; }
        }

        /// <summary>
        /// 日志级别
        /// </summary>
        public static int Log_Levenl
        {
            get { return log_levenl; }
            set { log_levenl = value; }
        }

        /// <summary>
        /// 测速等级
        /// </summary>
        public static int Report_Levenl
        {
            get { return report_levenl; }
            set { report_levenl = value; }
        }

        #endregion
    }
}