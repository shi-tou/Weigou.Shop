using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Service;
using Weigou.Common;
using System.Data;

namespace Weigou.Admin.Sys
{
    public partial class Setting : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ToolBar1.PrivilegeCode = "Setting";
                Bindsetting();
            }
        }
        /// <summary>
        ///  绑定设置
        /// </summary>
        public void Bindsetting()
        {
            string code = "Setting";
            DataTable dt = sysService.GetDataByKey("T_Setting", "Code", code);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.txtSiteName.Text = Convert.ToString(dr["SiteName"]);
                this.txtSiteUrl.Text = Convert.ToString(dr["SiteUrl"]);
                this.txtCompanyName.Text = Convert.ToString(dr["CompanyName"]);
                this.txtAddress.Text = Convert.ToString(dr["Address"]);
                this.txtContactName.Text = Convert.ToString(dr["ContactName"]);
                this.txtMobileNo.Text = Convert.ToString(dr["MobileNo"]);
                this.txtPhoneNo.Text = Convert.ToString(dr["PhoneNo"]);
                this.txtFax.Text = Convert.ToString(dr["Fax"]);
                this.txtQQ.Text = Convert.ToString(dr["QQ"]);
                this.txtEmail.Text = Convert.ToString(dr["Email"]);
                this.txtPageTitle.Text = Convert.ToString(dr["PageTitle"]);
                this.txtPageKeyword.Text = Convert.ToString(dr["PageKeyword"]);
                this.txtPageDescription.Text = Convert.ToString(dr["PageDescription"]);
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string code = "Setting";
            DataTable dt = sysService.GetDataByKey("T_Setting", "Code", code);
            DataRow dr ;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
            }
            else
            {
                dr = dt.NewRow();
                dr["Code"] = "Setting";
                dt.Rows.Add(dr);
                dr = dt.Rows[0];
            }
            dr["SiteName"] = this.txtSiteName.Text;
            dr["SiteUrl"] = this.txtSiteUrl.Text;
            dr["CompanyName"] = this.txtCompanyName.Text;
            dr["Address"] = this.txtAddress.Text;
            dr["ContactName"] = this.txtContactName.Text;
            dr["MobileNo"] = this.txtMobileNo.Text;
            dr["PhoneNo"] = this.txtPhoneNo.Text;
            dr["Fax"] = this.txtFax.Text;
            dr["QQ"] = this.txtQQ.Text;
            dr["Email"] = this.txtEmail.Text;
            dr["PageTitle"] = this.txtPageTitle.Text;
            dr["PageKeyword"] = this.txtPageKeyword.Text;
            dr["PageDescription"] = this.txtPageDescription.Text;
            int res = sysService.UpdateDataTable(dt);
            if (res > 0)
            {
                RegistScript("ShowMsg('操作成功！')");
            }
            else
            {
                RegistScript("ShowMsg('操作失败，请重试！')");
            }
        }
    }
}
