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
    public partial class MallOrderSaleList : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckAuth("OrderSaleList"))
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
            if (this.txtConsigneeMobileNo.Text.Trim() != "")
            {
                hs.Add("ConsigneeMobileNo", this.txtConsigneeMobileNo.Text.Trim());
            }
            if (this.ddlStatus.SelectedValue.Trim() != "")
            {
                hs.Add("Status", this.ddlStatus.SelectedValue.Trim());
            }
            if (this.txtMinTime.Text.Trim() != "")
            {
                hs.Add("MinTime", this.txtMinTime.Text.Trim());
            }
            if (this.txtMaxTime.Text.Trim() != "")
            {
                hs.Add("MaxTime", this.txtMaxTime.Text.Trim());
            }

            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "a.ApplyTime desc");
            orderService.GetOrderSaleList(p, hs);
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
        //售后类型
        protected string GetType(object s)
        {
            switch (Convert.ToInt32(s))
            {
                case 1:
                    return "退货";
                case 2:
                    return "换货";
                case 3:
                    return "维修";
                default: return "";
            }
        }
        //处理状态
        protected string GetStatus(object s)
        {
            switch (Convert.ToInt32(s))
            {
                case 0:
                    return "买家申请处理";
                case 1:
                    return "<span style=\"color:green;\">卖家同意处理</span>";
                case 2:
                    return "<span style=\"color:green;\">卖家拒绝处理</span>";
                default: return "";
            }
        }
    }
}