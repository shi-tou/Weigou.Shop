using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;
using Weigou.Model.Enum;
using System.Data;
using System.Collections;

namespace Weigou.Manage.Order
{
    public partial class MallOrderList : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckAuth("MallOrderList"))
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
            if (this.txtOrderNo.Text.Trim() != "")
            {
                hs.Add("OrderNo", this.txtOrderNo.Text.Trim());
            }
            //if (this.txtMemberName.Text.Trim() != "")
            //{
            //    hs.Add("MemberName", this.txtMemberName.Text.Trim());
            //}
            if (this.txtMobileNo.Text.Trim() != "")
            {
                hs.Add("MobileNo", this.txtMobileNo.Text.Trim());
            }
            if (this.ddlOrderStatus.SelectedValue.Trim() != "")
            {
                hs.Add("OrderStatus", this.ddlOrderStatus.SelectedValue.Trim());
            }
            if (this.txtMinTime.Text.Trim()!="")
            { 
                hs.Add("MinTime", this.txtMinTime.Text.Trim());
            }
            if (this.txtMaxTime.Text.Trim() != "")
            {
                hs.Add("MaxTime", this.txtMaxTime.Text.Trim());
            }

            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "a.OrderTime desc");
            orderService.GetMallOrderList(p, hs);
            this.AspNetPager.RecordCount = p.ItemCount;//记录总数
            this.repOrder.DataSource = p.DataSource;
            this.repOrder.DataBind();
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
        protected void repOrder_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            if (e.CommandName == "Delete")
            {
                int res = 0;
                DataTable dt = goodsService.GetDataByKey("T_MallOrder", "ID", id);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                    res = goodsService.UpdateDataTable(dt);
                }
                if (res > 0)
                    ShowMsg(this.Request.Url.ToString(), "删除商品成功");
                else
                    ShowMsg("/Goods/GoodsList.aspx", "删除商品失败");
            }

        }
        
    }
}