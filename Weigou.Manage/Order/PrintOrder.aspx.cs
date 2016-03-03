using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Manage.Order
{
    public partial class PrintOrder : ManagePage
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo
        {
            get
            {
                return GetRequest("OrderNo", "");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrderInfo();
            }
        }

        /// <summary>
        /// 绑定对应信息
        /// </summary>
        public void BindOrderInfo()
        {
            if (OrderNo == "")
                return;
            DataSet ds = orderService.GetMallOrderDetail(OrderNo);
            DataTable dtOrder = ds.Tables[0];
            this.repOrder.DataSource = dtOrder;
            this.repOrder.DataBind();

        }

        protected void repOrder_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater repDetail = (Repeater)e.Item.FindControl("repOrderDetail");
                //找到分类Repeater关联的数据项
                DataRowView rowv = (DataRowView)e.Item.DataItem;

                string orderNo = Convert.ToString(rowv["OrderNo"]);
                DataSet ds = orderService.GetMallOrderDetail(orderNo);

                DataTable dtOrderDetail = ds.Tables[1];
                foreach (DataRow drOrderDetails in dtOrderDetail.Rows)
                {
                    drOrderDetails["SaleProp"] = goodsService.GetGoodsAttributeValueInfo(drOrderDetails["SaleProp"].ToString());
                }
                repDetail.DataSource = dtOrderDetail;
                repDetail.DataBind();

            }
        }
    }
}