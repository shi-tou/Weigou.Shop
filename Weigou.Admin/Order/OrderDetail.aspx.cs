using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weiche.Service;
using System.Data;
using Weiche.Common;
using Newtonsoft.Json;
using Weiche.Model.Enum;
using System.Drawing;

namespace Weiche.Admin.Order
{
    public partial class OrderDetail : AdminPage
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string OrderNo
        {
            get
            {
                return GetRequest("OrderNo", "");
            }
        }
        public string strOrderDetal = "";

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            DataSet ds = orderService.GetCarOrderDetail(OrderNo);
            DataTable dtOrder = ds.Tables[0];
            DataRow drOrder = dtOrder.Rows[0];
            this.labOrderNo.Text = Convert.ToString(drOrder["OrderNo"]);
            this.labOrderTime.Text = Convert.ToString(drOrder["OrderTime"]);
            int status=Convert.ToInt16(drOrder["Status"]);
            this.labStatus.Text = EnumHelper.GetOrderStatus(status);
            switch ((EnumOrderStatus)status)
            {
                case EnumOrderStatus.Submit:
                    break;
                case EnumOrderStatus.Paid:
                    this.labStatus.ForeColor = Color.Blue;
                    break;
                case EnumOrderStatus.ApplyRefund:
                    this.labStatus.ForeColor = Color.Red;
                    break;
                case EnumOrderStatus.Cancel:
                    this.labStatus.ForeColor = Color.Gray;
                    break;
                case EnumOrderStatus.Finish:
                    this.labStatus.ForeColor = Color.Green;
                    break;
            }
            this.labMemberName.Text = Convert.ToString(drOrder["MemberName"]);
            this.labMobileNo.Text = Convert.ToString(drOrder["MobileNo"]);
            this.labTotalCount.Text = Convert.ToString(drOrder["TotalCount"]);
            this.labTotalMoney.Text = Convert.ToString(drOrder["TotalMoney"]);
            this.labConsigneeName.Text = Convert.ToString(drOrder["ConsigneeName"]);
            this.labConsigneeMobileNo.Text = Convert.ToString(drOrder["ConsigneeMobileNo"]);
            this.labDeliverAddress.Text = Convert.ToString(drOrder["DeliverAddress"]);

            DataTable dtOrderDetail = ds.Tables[1];
            strOrderDetal = JsonConvert.SerializeObject(dtOrderDetail);
        }
    }
}
