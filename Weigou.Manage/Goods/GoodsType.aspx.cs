using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;
using System.Collections;
using System.Data;
using Weigou.Model.Enum;
using System.Text;

namespace Weigou.Manage.Goods
{
    public partial class GoodsType : ManagePage
    {
        /// <summary>
        /// 
        /// </summary>
        public int ParentID
        {
            get { return GetRequest("ParentID", 0); }
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
                if (!CheckAuth("GoodsType"))
                    return;
                BindList();
                BindNav();
            }
        }
        /// <summary>
        /// 绑定用户数据
        /// </summary>
        public void BindList()
        {
            Hashtable hs = new Hashtable();

            hs.Add("ParentID", ParentID);
            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "a.CreateTime desc");
            goodsService.GetGoodsType(p, hs);
            this.AspNetPager.RecordCount = p.ItemCount;//记录总数
            this.repGoodsType.DataSource = p.DataSource;
            this.repGoodsType.DataBind();
            //添加按键、返回上一级
            DataTable dt = sysService.GetDataByKey("T_GoodsType", "ID", ParentID);
            if (dt.Rows.Count > 0)
            {
                this.backUrl.HRef = "GoodsType.aspx?ParentID=" + dt.Rows[0]["ParentID"];
            }
            else
            {
                this.backUrl.Visible = false;
            }
            this.addUrl.HRef = "GoodsTypeAdd.aspx?ParentID=" + ParentID;

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
        protected void repGoodsType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            if (e.CommandName == "Delete")
            {
                int res = 0;
                DataTable dt = sysService.GetDataByKey("T_GoodsType", "ID", id);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                    res = sysService.UpdateDataTable(dt);
                }
                if (res > 0)
                    ShowMsg(this.Request.Url.ToString(), "删除类别成功");
                else
                    ShowMsg("/Goods/GoodsTypeList.aspx", "删除类别失败");
            }
        }
        /// <summary>
        /// 绑定类别导航
        /// </summary>
        public void BindNav()
        {
            DataTable dt = goodsService.GetAllParentGoodsType(ParentID, true);
            StringBuilder sb = new StringBuilder("根节点");
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append(" -> ");
                sb.Append(dr["Name"]);
            }
            this.labNav.Text = sb.ToString();
        }
    }
}
