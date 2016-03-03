using Weigou.Common;
using Weigou.Model;
using Weigou.Model.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Manage.Content
{
    public partial class BannerList : ManagePage
    {
        /// <summary>
        /// Banner图ID
        /// </summary>
        public int _ID
        {
            get
            {
                return GetRequest("ID", 0);
            }
        }
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();      
                DataTable dt = goodsService.GetDataByKey("T_BaseData", "ParentID", BaseDataConst.BannerType);
                BindBasedataToList(this.ddlType, BaseDataConst.BannerType, "=全部=");
            }
        }
 
        /// <summary>
        /// 绑定用户数据
        /// </summary>
        public void BindList()
        {
            Hashtable hs = new Hashtable();
            if (this.ddlType.SelectedValue.Trim() != "")
            {
                hs.Add("Type", this.ddlType.SelectedValue.Trim());
            }
           
            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "a.CreateTime desc");
            contentService.GetBannerList(p, hs);
            this.AspNetPager.RecordCount = p.ItemCount;//记录总数
            this.repBanner.DataSource = p.DataSource;
            this.repBanner.DataBind();
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
        protected void repBanner_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            if (e.CommandName == "Delete")
            {
                int res = 0;
                DataTable dt = sysService.GetDataByKey("T_Banner", "ID", id);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                    res = sysService.UpdateDataTable(dt);
                }
                if (res > 0)
                    ShowMsg(this.Request.Url.ToString(), "删除用户成功");
                else
                    ShowMsg("/Sys/UserList.aspx", "删除用户失败");
            }

        }
    }
}