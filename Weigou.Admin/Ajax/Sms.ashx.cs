using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Weigou.Service;
using Weigou.Common;
using System.Text;
using System.Web.SessionState;
using Weigou.Model;
using Weigou.Model.Enum;

namespace Weigou.Admin.Ajax
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Sms : BaseAjax, IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 模板列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetSmsTemplateList(HttpContext hc)
        {
            string code = GetRequest("Code","");
            string content = GetRequest("Content", "");
            Hashtable hs = new Hashtable();
            if (code!="")
            {
                hs.Add("Code", code);
            }
            if (content != "")
            {
                hs.Add("Content", content);
            }
            Pager p = new Pager(PageSize, PageIndex, "CreateTime desc");
            smsService.GetSmsTemplateList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 删除模板
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteSmsTemplate(HttpContext hc)
        {
            int id = GetRequest("ID", 0);
            int res = RT.FAILED;
            DataTable dt = smsService.GetDataByKey("T_SmsTemplate", "ID", id);
            if (dt.Rows.Count > 0)
            {
                if (smsService.Delete("T_SmsTemplate", "ID", id) > 0)
                {
                    res = RT.SUCCESS;
                    smsService.SaveSysLog(id.ToString(), EnumModule.SmsManage, EnumOperation.Delete, UserInfo.ID, "删除编码为[" + dt.Rows[0]["Code"] + "]的短信模板");
                }
            }
            ResponseWrite(hc, res.ToString());
        }
        /// <summary>
        /// 短信日志
        /// </summary>
        /// <param name="hc"></param>
        public void GetSmsLogList(HttpContext hc)
        {
            string mobileNo = GetRequest("MobileNo", "");
            string content = GetRequest("Content", "");
            string status = GetRequest("Status", "");
            string sendType = GetRequest("SendType", "");
            string source = GetRequest("Source", "");
            string minTime = GetRequest("MinTime", "");
            string maxTime = GetRequest("MaxTime", "");
            Hashtable hs = new Hashtable();
            if (mobileNo != "")
            {
                hs.Add("MobileNo", mobileNo);
            }
            if (content != "")
            {
                hs.Add("Content", content);
            }
            if (status != "")
            {
                hs.Add("Status", status);
            }
            if (sendType != "")
            {
                hs.Add("SendType", sendType);
            }
            if (source != "")
            {
                hs.Add("Source", source);
            }
            if (minTime != "")
            {
                hs.Add("MinTime", minTime);
            }
            if (maxTime != "")
            {
                hs.Add("MaxTime", maxTime);
            }
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            smsService.GetSmsLogList(p, hs);
            ResponseWrite(hc, p);
        }
    }
}
