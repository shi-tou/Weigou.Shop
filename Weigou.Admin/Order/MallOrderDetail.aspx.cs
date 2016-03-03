using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Service;
using System.Data;
using Weigou.Common;
using Newtonsoft.Json;
using Weigou.Model.Enum;
using System.Drawing;
using Weigou.Model;

namespace Weigou.Admin.Order
{
    public partial class MallOrderDetail : AdminPage
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
                BindLogisticsInfo();
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
            DataRow drOrder = dtOrder.Rows[0];
            this.labOrderNo.Text = Convert.ToString(drOrder["OrderNo"]);
            this.labOrderTime.Text = Convert.ToString(drOrder["OrderTime"]);
            EnumMallOrderStatus status = (EnumMallOrderStatus)Convert.ToInt16(drOrder["OrderStatus"]);
            this.labStatus.Text = EnumHelper.GetMallOrderStatus((int)status);
            switch (status)
            {
                case EnumMallOrderStatus.Canceled:
                    this.labStatus.ForeColor = Color.Gray;
                    break;
                case EnumMallOrderStatus.Finished:
                    this.labStatus.ForeColor = Color.Green;
                    break;
                case EnumMallOrderStatus.Paid:
                    this.labStatus.ForeColor = Color.Black;
                    this.panelShip.Visible = true;
                    break;
                default:
                    this.labStatus.ForeColor = Color.Blue;
                    break;
            }
            this.labMemberName.Text = Convert.ToString(drOrder["MemberName"]);
            this.labMobileNo.Text = Convert.ToString(drOrder["MobileNo"]);
            this.labTotalCount.Text = Convert.ToString(drOrder["TotalCount"]);
            this.labTotalMoney.Text = Convert.ToString(drOrder["TotalMoney"]);
            this.labConsigneeName.Text = Convert.ToString(drOrder["ConsigneeName"]);
            this.labConsigneeMobileNo.Text = Convert.ToString(drOrder["ConsigneeMobileNo"]);
            this.labDeliverAddress.Text = Convert.ToString(drOrder["DeliverAddress"]);
            this.labLogisticsNo.Text = Convert.ToString(drOrder["LogisticsNo"]);
            this.hfLogisticsCode.Value = Convert.ToString(drOrder["LogisticsCode"]);
            this.labLogisticsName.Text = Convert.ToString(drOrder["LogisticsName"]);

            //快递公司/单号
            string LogisticsCode = Convert.ToString(drOrder["LogisticsCode"]);
            if (!string.IsNullOrEmpty(LogisticsCode))
            {
                this.ddlLogistics.SelectedValue = LogisticsCode;
                this.txtLogisticsNo.Text = Convert.ToString(drOrder["LogisticsNo"]);
            }

            if (Convert.ToInt32(drOrder["OrderStatus"]) == Convert.ToInt32(EnumMallOrderStatus.Paid) || Convert.ToInt32(drOrder["OrderStatus"]) == Convert.ToInt32(EnumMallOrderStatus.ApplyRefund))
            {
                if (drOrder["LogisticsStatus"].ToString() == "1")
                {
                    DataSet checkds = orderService.CheckSellerShip(OrderNo);
                    DataTable Dt_1 = checkds.Tables[0];
                    DataTable Dt_2 = checkds.Tables[1];
                    if (Dt_1.Rows.Count == 0 || Dt_2.Rows.Count == 0)
                    {
                        this.txtLogisticsNo.Visible = true;
                        this.labLogisticsNo.Visible = false;

                        //绑定物流公司
                        DataTable LogisticsDt = merchantService.GetData("T_Logistics");
                        if (LogisticsDt.Rows.Count > 0)
                        {
                            this.ddlLogistics.DataTextField = "Name";
                            this.ddlLogistics.DataValueField = "Code";
                            this.ddlLogistics.DataSource = LogisticsDt;
                            this.ddlLogistics.DataBind();
                            this.ddlLogistics.Visible = true;
                        }
                        this.btnShipped.Visible = true;
                    }
                }

            }
            //if (Convert.ToInt32(drOrder["OrderStatus"]) == Convert.ToInt32(EnumMallOrderStatus.ToPay))
            //{
            //    this.txtLogisticsPrice.Visible = true;
            //    this.labLogisticsPrice.Visible = false;
            //    this.btnSave.Visible = true;
            //}
            if (Convert.ToInt32(drOrder["OrderStatus"]) == Convert.ToInt32(EnumMallOrderStatus.Shipped) || Convert.ToInt32(drOrder["OrderStatus"]) == Convert.ToInt32(EnumMallOrderStatus.Received) || Convert.ToInt32(drOrder["OrderStatus"]) == Convert.ToInt32(EnumMallOrderStatus.HasComment))
            {
                this.BtnViewLogistics.Visible = true;
            }


            DataTable dtOrderDetail = ds.Tables[1];
            foreach (DataRow drOrderDetails in dtOrderDetail.Rows)
            {
                drOrderDetails["SaleProp"] = goodsService.GetGoodsAttributeValueInfo(drOrderDetails["SaleProp"].ToString());
            }
            strOrderDetal = JsonConvert.SerializeObject(dtOrderDetail);
        }
        /// <summary>
        /// 绑定快递信息
        /// </summary>
        public void BindLogisticsInfo()
        {
            DataTable dt = orderService.GetDataByKey("T_Logistics", "Status", 1);
            this.ddlLogistics.DataTextField = "Name";
            this.ddlLogistics.DataValueField = "ID";
            this.ddlLogistics.DataSource = dt;
            this.ddlLogistics.DataBind();
            this.ddlLogistics.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        /// <summary>
        /// 确认发货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShipped_Click(object sender, EventArgs e)
        {
            MallOrderShipInfo info = new MallOrderShipInfo();
            info.OrderNo = OrderNo;
            info.LogisticsCode = this.ddlLogistics.SelectedValue;
            info.LogisticsNo = this.txtLogisticsNo.Text.Trim();
            info.CreateBy = UserInfo.ID;

            int result= orderService.ConfirmMallOrderShipped(info);
            if (result == RT.SUCCESS)
            {
                orderService.SaveSysLog(OrderNo.ToString(), EnumModule.OrderManage, EnumOperation.Ship, UserInfo.ID, "订单:" + OrderNo + "确认发货");
                RegistScript("CloseWin('操作成功！',parent.GetList);");
            }
            else
            {
                RegistScript("ShowMsg('操作失败');");
            }
        }
    }
}
