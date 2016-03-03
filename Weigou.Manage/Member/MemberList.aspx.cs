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

namespace Weigou.Manage.Member
{
    public partial class MemberList : ManagePage
    { 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckAuth("MemberList"))
                    return;
                BindList();
                BindLevel();
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
            if (this.txtMobileNo.Text.Trim() != "")
            {
                hs.Add("MobileNo", this.txtMobileNo.Text.Trim());
            }
            if (this.ddlLevel.SelectedValue.Trim() != "")
            {
                hs.Add("LevelID", this.ddlLevel.SelectedValue.Trim());
            }
            if (this.ddlStatus.SelectedValue.Trim() != "")
            {
                hs.Add("Status", this.ddlStatus.SelectedValue.Trim());
            }
            if (this.ddlSex.SelectedValue.Trim() != "")
            {
                hs.Add("Sex", this.ddlSex.SelectedValue.Trim());
            }
            if (this.txtMinTime.Text.Trim()!="")
            { 
                hs.Add("MinTime", this.txtMinTime.Text.Trim());
            }
            if (this.txtMaxTime.Text.Trim() != "")
            {
                hs.Add("MaxTime", this.txtMaxTime.Text.Trim());
            }

            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "a.CreateTime desc");
            memberService.GetMemberList(p, hs);
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
                DataTable dt = goodsService.GetDataByKey("T_Member", "ID", id);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                    res = goodsService.UpdateDataTable(dt);
                }
                if (res > 0)
                    ShowMsg(this.Request.Url.ToString(), "删除成功");
                else
                    ShowMsg("/Member/MemberList.aspx", "删除失败");
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
         

        public void BindLevel()
        {
            DataTable dt = memberService.GetData("T_MemberLevel");
            this.ddlLevel.DataSource = dt;
            this.ddlLevel.DataTextField = "Name";
            this.ddlLevel.DataValueField = "ID";
            this.ddlLevel.DataBind();
            this.ddlLevel.Items.Insert(0, new ListItem("-全部级别-", ""));
        }
    }
}
