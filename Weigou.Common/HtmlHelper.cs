using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace SigeShop.Common
{
    public class HtmlHelper
    {
        public static int AspxToHtml(string url, string fileName)
        {
            try
            {
                string html = "";
                WebRequest request;
                request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Encoding enc = Encoding.GetEncoding("utf-8");
                StreamReader sr = new StreamReader(response.GetResponseStream(), enc);
                html = sr.ReadToEnd();
                response.Close();
                sr.Close();
                //写文件操作
                StreamWriter sw = new StreamWriter(fileName, false, System.Text.Encoding.UTF8);
                sw.WriteLine(html);
                sw.Close();
            }
            catch (Exception ex)
            {
                string errorDesc = "错误消息：" + ex.Message +
                                  "\r\n发生时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") +
                                  "\r\n错误源： " + ex.Source +
                                  "\r\n站点地址：" + url +
                                  "\r\n引发异常的方法： " + ex.TargetSite +
                                  "\r\n堆栈信息： " + ex.StackTrace;
                Utils.SaveLog("系统出现异常" + url, errorDesc);
            }
            return 0;
        }
    }
}
