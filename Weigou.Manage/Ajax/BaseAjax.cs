using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Weigou.Common;
using System.Reflection;
using System.IO;
using System.Text;

namespace Weigou.Manage
{
    /// <summary>
    /// Ajax基类
    /// </summary>
    public class BaseAjax : ManagePage
    {
        #region 方法
        /// <summary>
        /// 将返回结果格式化成Json字符串
        /// </summary>
        public string ToJson(Pager p)
        {
            string strJson = "";
            if (p.ItemCount > 0)
                strJson = "{\"rows\":" + ToJson(p.DataSource) + ",\"total\":" + p.ItemCount + "}";
            else
                strJson = "{\"rows\":{},\"total\":0}";
            return strJson;
        }
        /// <summary>
        /// 将datatable数据转换成JSON数据
        /// </summary>
        public string ToJson(DataTable dt)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(dt);
        }
        public void ResponseWrite(HttpContext hc)
        {
            hc.Response.ContentType = "text/plain";
            hc.Response.Write(strJson);
        }
        public void ResponseWrite(HttpContext hc, string str)
        {
            hc.Response.ContentType = "text/plain";
            hc.Response.Write("{\"res\":\"" + str + "\"}");
        }
        public void ResponseWrite(HttpContext hc, int str)
        {
            hc.Response.ContentType = "text/plain";
            hc.Response.Write("{\"res\":\"" + str + "\"}");
        }
        public void ResponseWrite(HttpContext hc, Pager p)
        {
            hc.Response.ContentType = "text/plain";
            hc.Response.Write(ToJson(p));
        }
        public void ResponseWrite(HttpContext hc, DataTable dt)
        {
            hc.Response.ContentType = "text/plain";
            if (dt == null || dt.Rows.Count == 0)
                hc.Response.Write("{\"rows\":{},\"total\":0}");
            else
                hc.Response.Write(ToJson(dt));
        }
        public void ResponseWriteDT(HttpContext hc, DataTable dt)
        {
            hc.Response.ContentType = "text/plain";
            if (dt == null)
                hc.Response.Write("{}");
            else
                hc.Response.Write(ToJson(dt));
        }
        #endregion

        #region 输出Excel文件
        /// <summary>
        /// 输出Excel文件
        /// </summary>
        /// <param name="hc"></param>
        /// <param name="dt"></param>
        /// <param name="fileName"></param>
        /// <param name="title"></param>
        public void ExportExcel(HttpContext hc, DataTable dt, string fileName, string title)
        {
            ExportExcel(hc, NpoiHelper.Export(dt, "", title), fileName);
        }
        public void ExportExcel(HttpContext hc, MemoryStream ms,string fileName)
        {
            hc.Response.ContentType = "application/x-excel";
            hc.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName,Encoding.Default));
            hc.Response.BinaryWrite(ms.GetBuffer());
            hc.Response.End();
        }
        #endregion

        /// <summary>
        /// 利用反射调用方法
        /// </summary>
        public new void ProcessRequest(HttpContext context)
        {
            string method = context.Request["action"].ToString();
            object obj = Assembly.GetExecutingAssembly().CreateInstance(context.Handler.ToString(), false);
            object obj2 = Type.GetType(context.Handler.ToString()).GetMethod(method).Invoke(obj, new HttpContext[] { context });
        }
    }
}
