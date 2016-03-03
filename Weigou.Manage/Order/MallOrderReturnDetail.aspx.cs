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

namespace Weigou.Manage.Order
{
    public partial class MallOrderReturnDetail : ManagePage
    {
        /// <summary>
        /// ID
        /// </summary>
        public string _ID
        {
            get
            {
                return GetRequest("ID", "");
            }
        }
        /// <summary>
        /// 是否编辑
        /// </summary>
        private int _IsEdit
        {
            get { return GetRequest("IsEdit", -1); }
        }
        /// <summary>
        /// OrderNo
        /// </summary>
        public string _OrderNo
        {
            get
            {
                return GetRequest("OrderNo", "");
            }
        }
        public string strOrderDetal = "";


        protected string Tab = "";

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (_IsEdit == 0)
            {
                this.BtnAgree.Enabled = false;
                this.BtnAgree.Visible = false;
                this.BtnShip.Enabled = false;
                this.BtnShip.Visible = false;
            }
            if (!IsPostBack)
            {
                BindOrderReturnInfo();

                if (Request["tab"] != null)
                {
                    Tab = Request["tab"];
                }
            }
        }
        /// <summary>
        /// 绑定对应信息
        /// </summary>
        public void BindOrderReturnInfo()
        {
            if (_ID == "")
                return;
            DataSet ds = orderService.GetMallOrderReturnDetailsInfo(_ID, _OrderNo);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                //绑定基本信息
                DataRow dr = dt.Rows[0];
                this.labApplyTime.Text = dr["ApplyTime"].ToString();
                this.labDealTime.Text = dr["DealTime"].ToString();
                if (dr["PayType"].ToString() == "1")
                {
                    this.labPayMentType.Text = "微信";
                }
                else if (dr["PayType"].ToString() == "2")
                {
                    this.labPayMentType.Text = "支付宝";
                }
                else if (dr["PayType"].ToString() == "3")
                {
                    this.labPayMentType.Text = "银联";
                }
                this.labMemberName.Text = dr["MemberName"].ToString();
                this.labMemberMobile.Text = dr["MemberMobileNo"].ToString();
                this.labConsigneeName.Text = dr["ConsigneeName"].ToString();
                this.labConsigneeMobileNo.Text = dr["ConsigneeMobileNo"].ToString();
                this.labDetailsAddress.Text = dr["DeliverAddress"].ToString();
                this.lblZipCode.Text = dr["ZipCode"].ToString();
                this.labOrderTotalMoney.Text = dr["TotalMoney"].ToString();
                this.labReason.Text = dr["Reason"].ToString();
                if (dr["OrderStatus"].ToString() == "50" && dr["LogisticsStatus"].ToString() == "1")
                {
                    this.labOrderStatus.Text = EnumHelper.GetMallOrderStatuss(dr["OrderStatus"]) + "<span style='color:red;'>(待发货)</span>";
                }
                else if (dr["OrderStatus"].ToString() == "50" && dr["LogisticsStatus"].ToString() == "2")
                {
                    this.labOrderStatus.Text = EnumHelper.GetMallOrderStatuss(dr["OrderStatus"]) + "<span style='color:red;'>(待收货)</span>";
                }
                else
                {
                    this.labOrderStatus.Text = EnumHelper.GetMallOrderStatuss(dr["OrderStatus"]);
                }
                this.txtRemark.Text = dr["Remark"].ToString();
                if (dr["Status"].ToString() != "0")
                {
                    this.BtnAgree.Visible = false;
                    this.BtnShip.Visible = false;
                }
                else
                {
                    if (dr["LogisticsStatus"].ToString() == "2")
                    {
                        this.BtnShip.Visible = false;
                    }
                }
                DataTable dtOrderDetail = ds.Tables[1];
                foreach (DataRow drOrderDetails in dtOrderDetail.Rows)
                {
                    drOrderDetails["SaleProp"] = goodsService.GetGoodsAttributeValueInfo(drOrderDetails["SaleProp"].ToString());
                }
                strOrderDetal = JsonConvert.SerializeObject(dtOrderDetail);
            }

        }

        protected void BtnAgree_Click(object sender, EventArgs e)
        {
            int res = orderService.DealOrderReturn(_ID, this.txtRemark.Text, UserInfo.ID.ToString());
            if (res > 0)
            {
                ShowMsg("/Order/OrderReturnList.aspx?tab="+Tab, "操作成功！");
            }
            else
               ShowMsg("/Order/OrderReturnList.aspx?tab=" + Tab, "操作失败！");
        }

        protected void BtnShip_Click(object sender, EventArgs e)
        {
            Response.Redirect("MallOrderDetail.aspx?OrderNo=" + _OrderNo + "&IsReturn=1");
        }
    }
}
