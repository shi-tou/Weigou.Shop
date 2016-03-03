using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeiJin.Weixin
{
    /// <summary>
    /// 自定义微信处理通道(<add verb="*" path="*.ashx" type="MeiJin.Weixin.WxHttpHandler,MeiJin.Weixin" validate="true"/>)
    /// </summary>
    public class WxHttpHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }
        /// <summary>
        /// 处理请求
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            //由微信服务接收请求，具体处理请求
            WxService wxService = new WxService(context.Request);
            string responseMsg = wxService.Response();
            context.Response.Clear();
            context.Response.Charset = "UTF-8";
            context.Response.Write(responseMsg);
            context.Response.End();
        }
    }
}