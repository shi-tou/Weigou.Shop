<%@ WebHandler Language="C#" Class="ajax" %>

using System;
using System.Web;

public class ajax : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        System.Threading.Thread.Sleep(1000);
        context.Response.ContentType = "text/plain";
        context.Response.Write(context.Request.Form[0] == "12345" ? "1" : string.Empty);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}