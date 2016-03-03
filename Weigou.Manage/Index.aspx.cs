using Weigou.Common;
using Weigou.Model;
using Weigou.Model.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Manage
{
    public partial class Index : ManagePage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (UserInfo == null)
                    return;
                LoadMenu();
            }
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        public void LoadMenu()
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
                sb.Append(string.Format("<li><a><i class=\"fa fa-{0}\"></i><span class=\"nav-label\">{1}</span><span class=\"fa arrow\"></span></a>", drParent["Icon"].ToString(), drParent["Name"].ToString()));
                sb.Append("<ul class=\"nav nav-second-level\">");
                DataTable dtSub = Utils.SelectDataTable(dt, "ParentCode='" + drParent["Code"].ToString() + "'");
                foreach (DataRow drSub in dtSub.Rows)
                {
                    sb.Append(string.Format("<li><a class=\"J_menuItem\" href=\"{0}\" data-index=\"0\">{1}</a></li>",drSub["Url"].ToString(), drSub["Name"].ToString()));
                }
                sb.Append("</ul>");
                sb.Append("</li>");
            }
            litMenu.Text = sb.ToString();
        }
    }
}