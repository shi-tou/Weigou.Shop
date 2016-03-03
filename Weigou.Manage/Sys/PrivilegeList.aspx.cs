using Weigou.Common;
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
    public partial class PrivilegeList : ManagePage
    {
        public string ParentCode
        {
            get { return GetRequest("ParentCode", ""); }
        }
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
                hs.Add("Name", this.txtName.Text.Trim());
            }
            if (this.txtCode.Text.Trim() != "")
            {
                hs.Add("Code", this.txtCode.Text.Trim());
            }
            hs.Add("ParentCode", ParentCode);
            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "Sort asc");
            sysService.GetPrivilegeList(p, hs);
            this.AspNetPager.RecordCount = p.ItemCount;//记录总数
            this.repPrivilege.DataSource = p.DataSource;
            this.repPrivilege.DataBind();
            //添加按键、返回上一级
            DataTable dt = sysService.GetDataByKey("T_Privilege", "Code", ParentCode);
            if (dt.Rows.Count > 0)
            {
                this.backUrl.HRef = "PrivilegeList.aspx?ParentCode=" + dt.Rows[0]["ParentCode"];
                this.addUrl.HRef = "PrivilegeAdd.aspx?ParentCode=" + ParentCode;
            }
            else
            {
                this.backUrl.Visible = false;
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
                DataTable dt = sysService.GetDataByKey("T_Privilege", "ID", id);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                    res=sysService.UpdateDataTable(dt);
                }
                if (res > 0)
                    ShowMsg(this.Request.Url.ToString(), "删除权限成功");
                else
                    ShowMsg("/Sys/PrivilegeList.aspx", "删除权限失败");
            }
        }
        /// <summary>
        /// 权限类别
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetType(string type)
        {
            if (type == "1")
                return "模块";
            else if (type == "2")
                return "主窗体";
            else if (type == "3")
                return "工具栏";
            return "";
        }
    }
}