﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;
using Weigou.Model.Enum;
using System.Data;
using System.Collections;

namespace Weigou.Manage.Sys
{
    public partial class SmsLogList : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckAuth("SmsLogList"))
                    return;
                BindList();
            }
        }
        /// <summary>
        /// 绑定用户数据
        /// </summary>
        public void BindList()
        {
            Hashtable hs = new Hashtable();
            if (this.txtMobileNo.Text.Trim() != "")
            {
                hs.Add("MobileNo", this.txtMobileNo.Text.Trim());
            }
            if (this.txtContent.Text.Trim() != "")
            {
                hs.Add("Content", this.txtContent.Text.Trim());
            }
            if (this.txtMinTime.Text.Trim() != "")
            {
                hs.Add("MaxTime", this.txtMaxTime.Text.Trim());
            }
            if (this.txtMaxTime.Text.Trim() != "")
            {
                hs.Add("MaxTime", this.txtMaxTime.Text.Trim());
            }
            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "a.CreateTime desc");
            smsService.GetSmsLogList(p, hs);
            this.AspNetPager.RecordCount = p.ItemCount;//记录总数
            this.repData.DataSource = p.DataSource;
            this.repData.DataBind();
        }
        /// <summary>
        /// 列表行操作事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repData_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            if (e.CommandName == "Delete")
            {
                int res = 0;
                DataTable dt = sysService.GetDataByKey("T_SmsSend", "ID", id);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                    res = goodsService.UpdateDataTable(dt);
                }
                if (res > 0)
                    ShowMsg(this.Request.Url.ToString(), "删除成功");
                else
                    ShowMsg(this.Request.Url.ToString(), "删除失败");
            }

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
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindList();
        }
    }
}