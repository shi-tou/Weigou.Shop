using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Model
{
    /// <summary>
    /// 短信模板编码
    /// </summary>
    public static class SmsTemplateConst
    {
        /// <summary>
        /// 注册验证码
        /// </summary>
        public static string Sms_VerifyCodeForReg = "VerifyCodeForReg";

        /// <summary>
        /// 修改密码验证码
        /// </summary>
        public static string Sms_VerifyCodeForUpPwd = "VerifyCodeForUpPwd";

        /// <summary>
        /// 修改身份证号验证码
        /// </summary>
        public static string Sms_VerifyCodeForUpCardNo = "VerifyCodeForUpCardNo";

        /// <summary>
        /// 找回密码验证码
        /// </summary>
        public static string Sms_VerifyCodeForFindPwd = "VerifyCodeForFindPwd";
    }
}
