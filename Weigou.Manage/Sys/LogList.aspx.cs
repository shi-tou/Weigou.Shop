using Weigou.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Manage.Sys
{
    public partial class LogList : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Func.BindModule(this.ddlModule, "-全部-");
                Func.BindOperation(this.ddlOperation, "-全部-");
                BindList();
            }
        }
        /// <summary>
        /// 绑定用户数据
        /// </summary>
        public void BindList()
        {
            Hashtable hs = new Hashtable();
            if (this.ddlModule.SelectedValue != "")
            {
                hs.Add("Module",this.ddlModule.SelectedValue);
            }
            if (this.ddlOperation.SelectedValue != "")
            {
                hs.Add("Operation", this.ddlOperation.SelectedValue);
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
            sysService.GetLogList(p, hs);
            this.AspNetPager.RecordCount = p.ItemCount;//记录总数
            this.repLog.DataSource = p.DataSource;
            this.repLog.DataBind();
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