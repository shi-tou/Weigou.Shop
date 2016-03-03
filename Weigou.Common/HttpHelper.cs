using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Weigou.Common
{
    /// <summary>   
    ///  Http操作类   
    /// </summary>   
    public static class HttpHelper
    {
        private static Encoding DEFAULT_ENCODING = Encoding.GetEncoding("GB2312");
        private static string ACCEPT = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
        private static string CONTENT_TYPE = "text/xml";
        private static string USERAGENT = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; InfoPath.2; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; msn OptimizedIE8;ZHCN)";

        /// <summary>   
        ///  获取网址HTML   
        /// </summary>   
        /// <param name="url">网址 </param>   
        /// <returns> </returns>   
        public static string GetHtmlContent(string url)
        {
            return GetHtmlContent(url, DEFAULT_ENCODING);
        }

        /// <summary>   
        ///  获取网址HTML   
        /// </summary>   
        /// <param name="url">网址 </param>   
        /// <returns> </returns>   
        public static string GetHtmlContent(string url, Encoding encoding)
        {
            string html = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = USERAGENT;
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = request.GetResponse();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    html = reader.ReadToEnd();
                    reader.Close();
                }
            }
            return html;
        }

        /// <summary>   
        /// 获取网站cookie   
        /// </summary>   
        /// <param name="url">网址 </param>   
        /// <returns> </returns>   
        public static string GetCookie(string url)
        {
            string cookie = string.Empty;
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            using (WebResponse response = request.GetResponse())
            {
                cookie = response.Headers.Get("Set-Cookie");
            }
            return cookie;
        }

        /// <summary>   
        /// Post模式浏览   
        /// </summary>   
        /// <param name="url">网址</param>   
        /// <param name="data">流</param>   
        /// <returns> </returns>   
        public static byte[] Post(string url, byte[] data)
        {
            return Post(null, url, data, null);
        }

        /// <summary>   
        /// Post模式浏览   
        /// </summary>   
        /// <param name="server">服务器地址 </param>   
        /// <param name="url">网址</param>   
        /// <param name="data">流</param>   
        /// <param name="cookieHeader">cookieHeader</param>   
        /// <returns> </returns>   
        public static byte[] Post(string server, string url, byte[] data, string cookieHeader)
        {
            if (data == null || data.Length == 0)
            {
                throw new ArgumentNullException("data");
            }
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            if (!string.IsNullOrEmpty(cookieHeader))
            {
                if (string.IsNullOrEmpty(server))
                {
                    throw new ArgumentNullException("server");
                }
                CookieContainer co = new CookieContainer();
                co.SetCookies(new Uri(server), cookieHeader);
                httpWebRequest.CookieContainer = co;
            }
            httpWebRequest.ContentType = CONTENT_TYPE;
            httpWebRequest.Accept = ACCEPT;
            httpWebRequest.Referer = server;
            httpWebRequest.UserAgent = USERAGENT;
            httpWebRequest.Method = "Post";
            httpWebRequest.ContentLength = data.Length;
            using (Stream stream = httpWebRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
            using (HttpWebResponse webResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (Stream stream = webResponse.GetResponseStream())
                {
                    long contentLength = webResponse.ContentLength;
                    byte[] bytes = new byte[contentLength];
                    bytes = ReadFully(stream);
                    stream.Close();
                    return bytes;
                }
            }
        }

        private static byte[] ReadFully(Stream stream)
        {
            byte[] buffer = new byte[128];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }

        /// <summary>   
        /// Get模式浏览   
        /// </summary>   
        /// <param name="url">Get网址</param>   
        /// <returns> </returns>   
        public static byte[] Get(string url)
        {
            return Get(null, url, null);
        }

        /// <summary>   
        /// Get模式浏览   
        /// </summary>   
        /// <param name="url">Get网址</param>   
        /// <param name="cookieHeader">cookieHeader</param>   
        /// <param name="server">服务器地址 </param>   
        /// <returns> </returns>   
        public static byte[] Get(string server, string url, string cookieHeader)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            if (!string.IsNullOrEmpty(cookieHeader))
            {
                if (string.IsNullOrEmpty(server))
                {
                    throw new ArgumentNullException("server");
                }
                CookieContainer co = new CookieContainer();
                co.SetCookies(new Uri(server), cookieHeader);
                httpWebRequest.CookieContainer = co;
            }
            httpWebRequest.Accept = "*/*";
            httpWebRequest.Referer = server;
            httpWebRequest.UserAgent = USERAGENT;
            httpWebRequest.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (Stream stream = webResponse.GetResponseStream())
            {
                long contentLength = webResponse.ContentLength;
                byte[] bytes = new byte[contentLength];
                bytes = ReadFully(stream);
                stream.Close();
                return bytes;
            }
        }
    }  
}
