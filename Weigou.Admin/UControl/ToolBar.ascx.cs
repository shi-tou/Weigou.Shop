using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Spring.Context;
using Weigou.Service;
using Weigou.Model;
using Weigou.Common;
using Spring.Context.Support;
using System.Text;
using Weigou.Model.Enum;

namespace Weigou.Admin.UControl
{
    public partial class ToolBar : System.Web.UI.UserControl
    {
        public string UserCookieName = "SysUserInfo";
        IApplicationContext ctx;
        private ISysService sysService;
        /// <summary>
        /// 菜单编码
        /// </summary>
        public string PrivilegeCode
        {
            get;
            set;
        }
            /// <summary>
        /// 当前登录管理员
        /// </summary>
        public UserInfo UserInfo
        {
            get
            {
                if (Request.Cookies[UserCookieName] == null)
                {
                    return null;
                }
                string str = DESEncrypt.Decrypt(Request.Cookies[UserCookieName].Value);
                return (UserInfo)Newtonsoft.Json.JsonConvert.DeserializeObject(str, typeof(UserInfo));
            }
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserInfo != null)
            {
                ctx = ContextRegistry.GetContext();
                sysService = (ISysService)ctx["sysService"];
                StringBuilder str = new StringBuilder();
                Hashtable hs = new Hashtable();
                hs.Add("ParentCode", PrivilegeCode);
                hs.Add("Status", (int)EnumStatus.Normal);
                DataTable dt = sysService.GetPrivilegeList(hs);
                //超级管理员拥有所有权限
                if (UserInfo.UserName.ToLower() == "admin")
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        str.Append("<a href=\"javascript:void(0)\" class=\"easyui-linkbutton\" iconcls=\"" + Convert.ToString(dr["Icon"]) + "\" plain=\"true\" onclick=\"" + Convert.ToString(dr["Func"]) + "()\"> " + dr["Name"].ToString() + "</a> ");
                    }
                }
                else
                {
                    //判断用户权限
                    DataTable dtPrivilege = sysService.GetUserPrivilege(UserInfo.ID);
                    if (!CheckAuth(dtPrivilege, PrivilegeCode))
                    {
                        Response.Write("<div style=\"margin:10px; padding:5px; border:dashed 1px #f6981e; background:#ffc;font-size:12px;\">非法访问！</div>");
                        Response.End();
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (CheckAuth(dtPrivilege, dr["Code"].ToString()) )
                            str.Append("<a href=\"javascript:void(0)\" class=\"easyui-linkbutton\" iconcls=\"" + Convert.ToString(dr["Icon"]) + "\" plain=\"true\" onclick=\"" + Convert.ToString(dr["Func"]) + "()\"> " + dr["Name"].ToString() + "</a> ");
                    }
                }
               
                this.Literal_Tb.Text = str.ToString();
            }
        }
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CheckAuth(DataTable dt, string value)
        {
            if (dt == null || dt.Rows.Count == 0)
                return false;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Code"].ToString() == value)
                    return true;
            }
            return false;
        }
    }
}