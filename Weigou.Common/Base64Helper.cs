using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Common
{
   public class Base64Helper
    {
        #region ========编码========
        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="code_type">编码类型</param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string EncodeBase64(string codeType, string code)
        {
            return EncodeBase64(Encoding.GetEncoding(codeType), code);
        }
        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="code_type">编码类型</param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string EncodeBase64(Encoding encoding, string code)
        {
            string encode = "";
            byte[] bytes = encoding.GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }
        #endregion 

        #region ========解码========
        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="code_type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string DecodeBase64(string codeType, string code)
        {
            return DecodeBase64(Encoding.GetEncoding(codeType), code);
        }
        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="code_type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string DecodeBase64(Encoding encoding, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = encoding.GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }
        #endregion
    }
}
