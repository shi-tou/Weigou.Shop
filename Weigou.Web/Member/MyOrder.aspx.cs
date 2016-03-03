using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;
using Weigou.Model;
using Weigou.Model.Enum;
using Weigou.Service;
using Spring.Context;
using Spring.Context.Support;

namespace Weigou.Web.Member
{
    public partial class MyOrder : WebPage
    {
        public string TotalPrice = "";
        public int Count = 0;
        public string OrderStatus = "";

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
            }
        }
        /// <summary>
        /// 绑定列表
        /// </summary>
        public void BindList()
        {
            Hashtable hs = new Hashtable();
            hs.Add("MemberID", LoginMemberInfo.ID);
            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, " OrderTime desc");
            //orderService.GetMemOrderList(p, hs);
            //this.AspNetPager.RecordCount = p.ItemCount;//记录总数  
            //this.repOrder.DataSource = p.DataSource;
            //this.repOrder.DataBind();
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

        protected void repOrder_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater repDetail = (Repeater)e.Item.FindControl("repOrderDetail");
                //找到分类Repeater关联的数据项
                DataRowView rowv = (DataRowView)e.Item.DataItem;

                string orderNo = Convert.ToString(rowv["OrderNo"]);
                int merchantID = Convert.ToInt32(rowv["MerchantID"]);
                // 查询子数据
                Hashtable hs = new Hashtable();
                hs.Add("OrderNo", orderNo);
                hs.Add("MerchantID", merchantID);
                //DataSet ds = orderService.GetMemOrderDetail(hs);
                //if (ds.Tables.Count != 0)
                //{
                //    Count = ds.Tables[0].Rows.Count;
                //    //OrderStatus = EnumHelper.GetOrderStatus(ds.Tables[0].Rows[0]["Status"]);
                //    TotalPrice = Convert.ToString(ds.Tables[1].Rows[0][0]);
                //    repDetail.DataSource = ds.Tables[0];
                //    repDetail.DataBind();
                //}
            }
        }
    }
}