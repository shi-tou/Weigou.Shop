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
    public partial class MallOrderSaleDetail : ManagePage
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
        public string strOrderDetal = "";

        protected DataTable OrderDetail;
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
                this.BtnRefuse.Enabled = false;
                this.BtnRefuse.Visible = false; 
            }
            if (!IsPostBack)
            {
                BindOrderSaleInfo();
            }
        }
        /// <summary>
        /// 绑定对应信息
        /// </summary>
        public void BindOrderSaleInfo()
        {
            if (_ID == "")
                return;
            DataSet ds = orderService.GetOrderSaleDetailsInfo(_ID);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.labApplyTime.Text = dr["ApplyTime"].ToString();
                this.labMemberName.Text = dr["MemberName"].ToString();
                this.labMemberMobile.Text = dr["MemberMobileNo"].ToString();
                this.labConsigneeName.Text = dr["ConsigneeName"].ToString();
                this.labConsigneeMobileNo.Text = dr["ConsigneeMobileNo"].ToString();
                this.labDetailsAddress.Text = dr["DeliverAddress"].ToString();
                this.lblZipCode.Text = dr["ZipCode"].ToString();
                this.labDescription.Text = dr["Description"].ToString();
                this.labApplyNumber.Text = dr["ApplyNumber"].ToString();
                this.txtRemark.Text = dr["Remark"].ToString();
                this.labOrderNo.Text = dr["OrderNo"].ToString();
                DataTable dtOrderDetail = ds.Tables[1];
                foreach (DataRow drOrderDetails in dtOrderDetail.Rows)
                {
                    drOrderDetails["SaleProp"] = goodsService.GetGoodsAttributeValueInfo(drOrderDetails["SaleProp"].ToString());
                }
                if (dr["Status"].ToString() == "1" || dr["Status"].ToString() == "2")
                {
                    this.BtnAgree.Visible = false;
                    this.BtnRefuse.Visible = false;
                }
                OrderDetail = dtOrderDetail;
                //strOrderDetal = JsonConvert.SerializeObject(dtOrderDetail);
                //绑定相关图片信息
                if (!string.IsNullOrEmpty(dr["Pic"].ToString()))
                {
                    this.imgPic.ImageUrl = dr["Pic"].ToString().Split(',')[0];
                }
            }

        }

        protected void BtnAgree_Click(object sender, EventArgs e)
        {
            int res = orderService.DealOrderSale(_ID, UserInfo.ID.ToString(), this.txtRemark.Text, "1");
            if (res > 0)
            {
                orderService.SaveSysLog(_ID.ToString(), EnumModule.OrderManage, EnumOperation.AgreeSale, UserInfo.ID, "同意售后申请");
                ShowMsg("/Order/OrderSaleList.aspx", "恭喜你，操作成功！");
            }
            else
                ShowMsg("/Order/OrderSaleList.aspx", "很抱歉，操作失败！");
        }

        protected void BtnRefuse_Click(object sender, EventArgs e)
        {
            int res = orderService.DealOrderSale(_ID, UserInfo.ID.ToString(), this.txtRemark.Text, "2");
            if (res > 0)
            {
                orderService.SaveSysLog(_ID.ToString(), EnumModule.OrderManage, EnumOperation.RefuseSale, UserInfo.ID, "拒绝售后申请");
                ShowMsg("/Order/OrderSaleList.aspx", "恭喜你，操作成功！");
            }
            else
                ShowMsg("/Order/OrderSaleList.aspx", "很抱歉，操作失败！");
        }
    }
}
