﻿using Weigou.Common;
using Weigou.Model.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Manage.Sys
{
    public partial class UserList : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
            }
        }
        /// <summary>
        /// 绑定用户数据
        /// </summary>
        public void BindList()
        {
            Hashtable hs = new Hashtable();
            if (this.txtName.Text.Trim() != "")
            {
                hs.Add("Name",this.txtName.Text.Trim());
            }
            if (this.txtUserName.Text.Trim() != "")
            {
                hs.Add("UserName", this.txtUserName.Text.Trim());
            }
            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "a.CreateTime desc");
            sysService.GetUserList(p, hs);
            this.AspNetPager.RecordCount = p.ItemCount;//记录总数
            this.repUser.DataSource = p.DataSource;
            this.repUser.DataBind();
        }
        /// <summary>
        /// 分页控件事件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager.CurrentPageIndex = e.NewPageIndex;
            BindList();
        }
        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindList();
        }
        /// <summary>
        /// 列表行操作事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repUser_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            if (e.CommandName == "Delete")
            {
                int res = 0;
                DataTable dt = sysService.GetDataByKey("T_User", "ID", id);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                    res=sysService.UpdateDataTable(dt);
                }
                if (res > 0)
                    ShowMsg(this.Request.Url.ToString(), "删除用户成功");
                else
                    ShowMsg("/Sys/UserList.aspx", "删除用户失败");
            }
            
        }
    }
}