using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Weigou.Common
{
    public class RegexHelper
    {

        #region 替换字符串
        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>替换后字符串</returns>
        public static string ReplaceInput(string input, string regex)
        {
            return Regex.Replace(input, regex, string.Empty);
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="replace">替换字符串</param>
        /// <returns>替换后字符串</returns>
        public static string ReplaceInput(string input, string regex, string replace)
        {
            return Regex.Replace(input, regex, replace);
        }

        #endregion

        #region 验证字符串

        /// <summary>
        /// 验证字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>是否验证通过</returns>
        public static bool CheckInput(string input, string regex)
        {
            return Regex.IsMatch(input, regex);
        }

        #endregion

        #region 常用方法
        /// <summary>
        /// 验证小数
        /// </summary>
        public static bool ValidDecimal(string v)
        {
            return CheckInput(v, @"^([-+]?[1-9]\d*\.\d+|-?0\.\d*[1-9]\d*)$");
        }
        /// <summary>
        /// 验证邮箱
        /// </summary>
        public static bool ValidEmail(string v)
        {
            return CheckInput(v, @"^([\w-\.]+)@([\w-\.]+)(\.[a-zA-Z0-9]+)$");
        }
        /// <summary>
        /// 验证IP
        /// </summary>
        public static bool ValidIp(string v)
        {
            return CheckInput(v, @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
        }
        /// <summary>
        /// 验证是否为后缀名
        /// </summary>
        public static bool ValidFix(string v)
        {
            return CheckInput(v, @"\.(?i:{0})$");
        }
        /// <summary>
        /// 验证电话号码
        /// </summary>
        public static bool ValidTel(string v)
        {
            return CheckInput(v, @"^(((\(0\d{2}\)|0\d{2})[- ]?)?\d{8}|((\(0\d{3}\)|0\d{3})[- ]?)?\d{7})(-\d{3})?$");
        }
        /// <summary>
        /// 验证手机号
        /// </summary>
        public static bool ValidMobileNo(string v)
        {
            return CheckInput(v, @"^[1]\d{10}$");
        }
        /// <summary>
        /// 验证URL
        /// </summary>
        public static bool ValidUrl(string v)
        {
            return CheckInput(v, @"^[a-zA-z]+://(\\w+(-\\w+)*)(\\.(\\w+(-\\w+)*))*(\\?\\S*)?$");
        }
        /// <summary>
        /// 验证身份证
        /// </summary>
        public static bool ValidIDCard(string v)
        {
            return CheckInput(v,  @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$");
        }
        /// <summary>  
        /// 验证汉字  
        /// </summary>  
        /// <param name="input">待验证的字符串</param>  
        /// <returns>是否匹配</returns>  
        public static bool ValidChnCharacter(string v)
        {
            return CheckInput(v, @"^[\u4e00-\u9fa5]+$");
        }
        /// <summary>  
        /// 验证只包含英文字母  
        /// </summary>  
        /// <param name="input">待验证的字符串</param>  
        /// <returns>是否匹配</returns>  
        public static bool ValidEnCharacter(string v)
        {
            return CheckInput(v, @"^[A-Za-z]+$");
        }
        /// <summary>  
        /// 验证只包含数字和英文字母  
        /// </summary> 
        public static bool ValidMunberAndCharacter(string v)
        {
            return CheckInput(v, @"^[0-9A-Za-z]+$");
        }
        /// <summary>  
        /// 验证数字[可以包含负号和小数点]  
        /// </summary>  
        public static bool ValidNumber(string v)
        {
            return CheckInput(v, @"^-?\d+$|^(-?\d+)(\.\d+)?$");
        }
        /// <summary>
        /// 验证整数(含负号)
        /// </summary>
        public static bool ValidInteger(string v)
        {
            return CheckInput(v, @"^-?\d+$");
        }
        /// <summary>
        /// 验证非负整数(含零)
        /// </summary>
        public static bool ValidIntegerNotNagtive(string v)
        {
            return CheckInput(v, @"^\d+$");
        }
        /// <summary>
        /// 验证正整数(不含零)
        /// </summary>
        public static bool ValidIntegerPositive(string v)
        {
            return CheckInput(v, @"^[0-9]*[1-9][0-9]*$");
        }
        /// <summary>  
        /// 验证日期格式 
        /// </summary>  
        public static bool ValidDateTime(string v)
        {
            DateTime dt;
            if (DateTime.TryParse(v, out dt))
                return true;
            else
                return false;
        }
        #endregion
    }
}
