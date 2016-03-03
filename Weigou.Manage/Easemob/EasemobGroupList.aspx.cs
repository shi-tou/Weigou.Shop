using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;
using Weigou.Model.Enum;
using System.Data;
using System.Collections;

namespace Weigou.Manage.Easemob
{
    public partial class EasemobGroupList : ManagePage
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
            if (this.txtGroupName.Text.Trim() != "")
            {
                hs.Add("Name", this.txtGroupName.Text.Trim());
            }
            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "a.CreateTime desc");
            memberService.GetEasemobGroupList(p, hs);
            this.AspNetPager.RecordCount = p.ItemCount;//记录总数
            this.repData.DataSource = p.DataSource;
            this.repData.DataBind();
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