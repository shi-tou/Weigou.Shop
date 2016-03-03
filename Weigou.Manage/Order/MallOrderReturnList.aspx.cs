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
    public partial class MallOrderReturnList : ManagePage
    {
        protected int TabIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckAuth("MallOrderReturn"))
                    return;
                BindWx();
                BindUnionpay();
                BindList();
                if (Request["tab"] != null)
                {
                    TabIndex = Convert.ToInt32(Request["tab"]);
                }
            }
        }

        #region 支付宝绑定
        /// <summary>
        /// 绑定用户数据
        /// </summary>
        public void BindList()
        {
            TabIndex = 1;
            Hashtable hs = new Hashtable();
            if (this.txtOrderNo_AliPay.Text.Trim() != "")
            {
                hs.Add("OrderNo", this.txtOrderNo_AliPay.Text.Trim());
            } 
            if (this.txtConsigneeMobileNo_AliPay.Text.Trim() != "")
            {
                hs.Add("MobileNo", this.txtConsigneeMobileNo_AliPay.Text.Trim());
            }
            if (this.txtMinTime_AliPay.Text.Trim() != "")
            {
                hs.Add("MinTime", this.txtMinTime_AliPay.Text.Trim());
            }
            if (this.txtMaxTime_AliPay.Text.Trim() != "")
            {
                hs.Add("MaxTime", this.txtMaxTime_AliPay.Text.Trim());
            }
            hs.Add("PayType", 2);

            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "a.ApplyTime desc");
            orderService.GetMallOrderReturnList(p, hs);
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
        protected void btnSearch_Alipay_Click(object sender, EventArgs e)
        {
            BindList();
        }

        protected void btnRefundAliPay_Click(object sender, EventArgs e)
        {
            string orderNos = Request.Form["cbAliPayOrderNo[]"];
            if (orderNos != null)
            {
                Response.Redirect("AliPayRefund.aspx?OrderNo=" + orderNos);
            }
        } 
        #endregion

        #region 微信绑定
        /// <summary>
        /// 绑定用户数据
        /// </summary>
        public void BindWx()
        {
            TabIndex = 2;
            Hashtable hs = new Hashtable();
            if (this.txtOrderNo_Wx.Text.Trim() != "")
            {
                hs.Add("OrderNo", this.txtOrderNo_Wx.Text.Trim());
            }
            if (this.txtConsigneeMobileNo_Wx.Text.Trim() != "")
            {
                hs.Add("MobileNo", this.txtConsigneeMobileNo_Wx.Text.Trim());
            }
            if (this.txtMinTime_Wx.Text.Trim() != "")
            {
                hs.Add("MinTime", this.txtMinTime_Wx.Text.Trim());
            }
            if (this.txtMaxTime_Wx.Text.Trim() != "")
            {
                hs.Add("MaxTime", this.txtMaxTime_Wx.Text.Trim());
            }
            hs.Add("PayType", 1);

            Pager p = new Pager(this.AspNetPagerWx.PageSize, this.AspNetPagerWx.CurrentPageIndex, "a.ApplyTime desc");
            orderService.GetMallOrderReturnList(p, hs);
            this.AspNetPagerWx.RecordCount = p.ItemCount;//记录总数
            this.repOrderWx.DataSource = p.DataSource;
            this.repOrderWx.DataBind();
        }
        /// <summary>
        /// 分页控件事件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void AspNetPagerWx_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPagerWx.CurrentPageIndex = e.NewPageIndex;
            BindWx();
        }

        protected void btnSearch_Wx_Click(object sender, EventArgs e)
        {
            BindWx();
        }

        protected void btnRefundWeixin_Click(object sender, EventArgs e)
        {
            string orderNos = Request.Form["cbWxOrderNo[]"];
            if (orderNos != null)
            {
                Response.Redirect("WeixinRefund.aspx?OrderNo=" + orderNos);
            }
        }

        #endregion

        #region 银联绑定

        /// <summary>
        /// 绑定用户数据
        /// </summary>
        public void BindUnionpay()
        {
            TabIndex = 3;
            Hashtable hs = new Hashtable();
            if (this.txtOrderNo_Unionpay.Text.Trim() != "")
            {
                hs.Add("OrderNo", this.txtOrderNo_Unionpay.Text.Trim());
            }
            if (this.txtConsigneeMobileNo_Unionpay.Text.Trim() != "")
            {
                hs.Add("MobileNo", this.txtConsigneeMobileNo_Unionpay.Text.Trim());
            }
            if (this.txtMinTime_Unionpay.Text.Trim() != "")
            {
                hs.Add("MinTime", this.txtMinTime_Unionpay.Text.Trim());
            }
            if (this.txtMaxTime_Unionpay.Text.Trim() != "")
            {
                hs.Add("MaxTime", this.txtMaxTime_Unionpay.Text.Trim());
            }
            hs.Add("PayType", 3);
            Pager p = new Pager(this.AspNetPagerUnionpay.PageSize, this.AspNetPagerUnionpay.CurrentPageIndex, "a.ApplyTime desc");
            orderService.GetMallOrderReturnList(p, hs);
            this.AspNetPagerUnionpay.RecordCount = p.ItemCount;//记录总数
            this.repOrderUnionpay.DataSource = p.DataSource;
            this.repOrderUnionpay.DataBind();
        }
        /// <summary>
        /// 分页控件事件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void AspNetPagerUnionpay_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPagerUnionpay.CurrentPageIndex = e.NewPageIndex;
            BindUnionpay();
        }
           
        protected void btnSearch_Unionpay_Click(object sender, EventArgs e)
        {
            BindUnionpay();
        }
        protected void btnRefundUnionpay_Click(object sender, EventArgs e)
        {
            string orderNos = Request.Form["cbUnionpayOrderNo[]"];
            if (orderNos != null)
            {
                Response.Redirect("UnionpayRefund.aspx?OrderNo=" + orderNos);
            }
        }
       
        #endregion

        #region 数据转换显示方法
        public string FormatStatus(object v)
        {
            var res = "";
            switch (Convert.ToInt32(v))
            {
                case 0:
                    res = "买家申请退款";
                    break;
                case 1:
                    res = "<span style=\"color:red;\">卖家同意退款</span>";
                    break;
                case 2:
                    res = "<span style=\"color:green;\">订单已退款</span>";
                    break;
                case 3:
                    res = "<span style=\"color:green;\">卖家已发货</span>";
                    break;
                case 4:
                    res = "<span style=\"color:red\">同意退款(卖家超24小时未处理)</span>";
                    break;
                case 5:
                    res = "<span style=\"color:red\">申请退款(已付款超15天未发货)</span>";
                    break;               
            }
            return res;
        }

        #endregion
    }
}