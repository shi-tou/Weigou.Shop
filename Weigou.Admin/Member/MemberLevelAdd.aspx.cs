using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Service;
using System.Data;
using Weigou.Model;
using Weigou.Common;
using Weigou.Model.Enum;

namespace Weigou.Admin.Member
{
    public partial class MemberLevelAdd : AdminPage
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        private int _ID
        {
            get { return GetRequest("ID", 0); }
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
        /// 绑定修改的会员
        /// </summary>
        public void BindInfo()
        {
            DataTable dt = memberService.GetDataByKey("T_MemberLevel", "ID", _ID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr =dt.Rows[0];
                this.txtName.Text = dr["Name"].ToString();
                this.txtMoney.Text = dr["Money"].ToString();
                this.txtYear.Text = dr["Year"].ToString();
                this.txtMoney.Text = dr["Money"].ToString();
                this.txtPrice.Text = dr["Price"].ToString();
                this.txtRentDiscount.Text = dr["RentDiscount"].ToString();
                this.txtStartTime.Text = TimeSpan.Parse(dr["StartTime"].ToString()).ToString();//@"hh\:mm"
                this.txtEndTime.Text = TimeSpan.Parse(dr["EndTime"].ToString()).ToString();//@"hh\:mm"
                this.txtShoppingDiscount.Text = dr["ShoppingDiscount"].ToString();
                this.txtBuyCarDiscount.Text = dr["BuyCarDiscount"].ToString();
                this.txtConsumerPoint.Text = dr["ConsumerPoint"].ToString();
                this.txtMileagePoint.Text = dr["MileagePoint"].ToString();
                this.txtPoint.Text = dr["Point"].ToString();
                 
            }
        }
        /// <summary>
        /// 保存会员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = memberService.GetDataByKey("T_MemberLevel", "ID", _ID);

             DataRow dr;
             if (dt.Rows.Count > 0)
             {
                 dr = dt.Rows[0];
             }
             else
             {
                 dr = dt.NewRow();
                 dt.Rows.Add(dr);
                 dr["CreateTime"] = DateTime.Now;
                 dr["CreateBy"] = UserInfo.ID;
             }

             dr["Name"] = this.txtName.Text.Trim();
             dr["Money"] = this.txtMoney.Text.Trim();
             dr["Year"] = this.txtYear.Text.Trim();
             dr["Price"] = this.txtPrice.Text.Trim();
             dr["RentDiscount"] = this.txtRentDiscount.Text.Trim();
             dr["StartTime"] = TimeSpan.Parse(this.txtStartTime.Text.Trim()).ToString();//@"hh\:mm"
             dr["EndTime"] = TimeSpan.Parse(this.txtEndTime.Text.Trim()).ToString();//@"hh\:mm"
             dr["ShoppingDiscount"] = this.txtShoppingDiscount.Text.Trim();
             dr["BuyCarDiscount"] = this.txtBuyCarDiscount.Text.Trim();
             dr["ConsumerPoint"] = this.txtConsumerPoint.Text.Trim();
             dr["MileagePoint"] = this.txtMileagePoint.Text.Trim();
             dr["Point"] = this.txtPoint.Text.Trim();

            int res = memberService.UpdateDataTable(dt);

            if (res >0 )
            {
                RegistScript("CloseWin('保存成功！',parent.GetList);");
            }
            else
            {
                RegistScript("ShowMsg('保存失败,请与管理员联系！');");
            }
        }
        
    }
}
