using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Weigou.Common;

namespace Weigou.Admin
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();

            string url = Request.Url.ToString();

            if (ex.GetType().Name.Equals("HttpException"))
            {
                HttpException httpEx = (HttpException)ex;
                int httpCode = httpEx.GetHttpCode();

                if (httpCode == 404)
                {
                    return;
                }
            }

            string errorDesc = "错误消息：" + ex.Message +
                              "\r\n发生时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") +
                              "\r\n错误源： " + ex.Source +
                              "\r\n站点地址：" + url +
                              "\r\n引发异常的方法： " + ex.TargetSite +
                              "\r\n堆栈信息： " + ex.StackTrace;
            this.Context.Server.ClearError();

            Utils.SaveLog("系统出现异常" + url, errorDesc);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}