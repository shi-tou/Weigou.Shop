using Weigou.Model.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Admin.Goods
{
    public partial class GoodsAudit : AdminPage
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        private int _ID
        {
            get { return GetRequest("ID", 0); }
        }

        /// <summary>
        /// 商品类别ID
        /// </summary>
        private int _TypeID;

        /// <summary>
        /// 商品类别名称
        /// </summary>
        private string _TypeName
        {
            get { return GetRequest("TypeName", ""); }
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (_ID != 0)
                {
                    BindInfo();
                }
            }
        }
        /// <summary>
        /// 绑定商品信息
        /// </summary>
        public void BindInfo()
        {
            DataTable dt = goodsService.GetDataByKey("T_Goods", "ID", _ID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.labCode.Text = dr["Code"].ToString();
                this.labName.Text = dr["Name"].ToString();
                this.labTypeName.Text = _TypeName;
                this.labSalePrice.Text = dr["SalePrice"].ToString();
                this.labSalePrice.Text = dr["SalePrice"].ToString();
                this.labMarketPrice.Text = dr["MarketPrice"].ToString();
                this.labStock.Text = dr["Stock"].ToString();
                this.labSimpleDesc.Text = Convert.ToString(dr["SimpleDesc"]);
                this.txtApprovedRemark.Text = Convert.ToString(dr["ApprovedRemark"]);
                if (dr["Status"].ToString() == "0")
                {
                    this.labStatus.Text = "待审核";
                }
                else if (dr["Status"].ToString() == "1")
                {
                    this.labStatus.Text = "审核通过";
                }
                else
                {
                    this.labStatus.Text = "审核未通过";
                }
                if (dr["ShelvesStatus"].ToString() == "0")
                {
                    this.labShelvesStatus.Text = "下架";
                }
                else
                {
                    this.labShelvesStatus.Text = "上架";
                }
                _TypeID = Convert.ToInt32(dr["Type"]);
            }
        }
        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = goodsService.GetDataByKey("T_Goods", "ID", _ID);
            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
            }
            else
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr = dt.Rows[0];
                dr["CreateTime"] = DateTime.Now;
                dr["CreateBy"] = UserInfo.ID;
            }
            dr["Status"] = Convert.ToInt16(EnumStatus.Normal);
            dr["ApprovedBy"] = UserInfo.ID;
            dr["ApprovedTime"] = DateTime.Now;
            dr["ApprovedRemark"] = this.txtApprovedRemark.Text;
            int res = goodsService.UpdateDataTable(dt);
            if (res > 0)
            {
                RegistScript("CloseWin('操作成功！',parent.GetList);");
            }
            else
                RegistScript("ShowMsg('操作失败');");

        }
        /// <summary>
        /// 审核不通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnUnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = goodsService.GetDataByKey("T_Goods", "ID", _ID);
            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
            }
            else
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr = dt.Rows[0];
                dr["CreateTime"] = DateTime.Now;
                dr["CreateBy"] = UserInfo.ID;
            }
            dr["Status"] = Convert.ToInt16(EnumStatus.DisAudit);
            dr["ApprovedBy"] = UserInfo.ID;
            dr["ApprovedTime"] = DateTime.Now;
            dr["ApprovedRemark"] = this.txtApprovedRemark.Text;
            int res = goodsService.UpdateDataTable(dt);
            if (res > 0)
            {
                RegistScript("CloseWin('操作成功！',parent.GetList);");
            }
            else
                RegistScript("ShowMsg('操作失败');");

        }
    }
}