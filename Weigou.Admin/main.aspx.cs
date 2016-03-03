using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Weigou.Service;
using System.Data;
using System.Collections;
using Weigou.Common;
using Weigou.Model.Enum;
using Weigou.Config;

namespace Weigou.Admin
{
    public partial class main : AdminPage
    {
        /// <summary>
        /// 菜单
        /// </summary>
        public string StrMenu = "";
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (UserInfo != null)
                {
                    GetMenu();
                }
            }
        }
            
        /// <summary>
        /// 获取菜单
        /// </summary>
        public void GetMenu()
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            //超级管理员拥有所有权限
            if (UserInfo.UserName.ToLower() == "admin")
            {
                Hashtable hs = new Hashtable();
                hs.Add("Status", EnumStatus.Normal);
                dt = sysService.GetPrivilegeList(hs);
            }
            else
            {
                //取分配给用户的权限
                dt = sysService.GetUserPrivilege(UserInfo.ID);
            }
            DataTable dtParent = Utils.SelectDataTable(dt, "ParentCode is null or ParentCode=''");
            foreach (DataRow drParent in dtParent.Rows)
            {
                sb.Append("<div title=\""+ drParent["Name"].ToString() +"\" style=\"padding:10px 10px 10px 20px;\">");
                sb.Append("    <ul class=\"menuUL\">");
                DataTable dtSub = Utils.SelectDataTable(dt, "ParentCode='"+ drParent["Code"].ToString() +"'");
                foreach (DataRow drSub in dtSub.Rows)
                {
                    sb.Append("        <li><a href=\"javascript:void(0)\" onclick=\"add('"+ drSub["Url"].ToString() +"',this)\">"+ drSub["Name"].ToString() +"</a></li>");
                }
                sb.Append("    </ul>");
                sb.Append("</div>");
            }
            StrMenu = sb.ToString();
        }
    }
}