using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Weigou.Service;
using Weigou.Common;
using System.Collections.Generic;
using Weigou.Model;
using Weigou.Model.Enum;

namespace Weigou.Admin.Sys
{
    public partial class UserAdd : AdminPage
    {
        private int _ID
        {
            get { return GetRequest("ID", 0); }
        }

        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRole();
                if(_ID!=0)
                    BindInfo();
            }
        }
        /// <summary>
        /// 绑定用户信息
        /// </summary>
        public void BindInfo()
        {
            DataTable dt = sysService.GetDataByKey("T_User", "ID", _ID);
            DataRow dr = dt.Rows[0];
            this.txtUserName.Text = Convert.ToString(dr["UserName"]);
            this.txtPassword.Text = "";
            this.txtPassword.Attributes.Remove("data-options");
            this.pwdTip.Attributes.CssStyle.Remove("display");
            this.txtName.Text = Convert.ToString(dr["Name"]);
            this.ddlRole.SelectedValue = Convert.ToString(dr["RoleID"]);
            this.cbDisabled.Checked = Convert.ToInt16(dr["Status"]) == (int)EnumStatus.Disabled ? true : false;
        }
        /// <summary>
        /// 绑定权限组
        /// </summary>
        public void BindRole()
        {
            //获取Type=1（系统角色）
            DataTable dt = sysService.GetData("T_Role");
            ddlRole.DataSource = dt;
            ddlRole.DataTextField = "Name";
            ddlRole.DataValueField = "ID";
            ddlRole.DataBind();
        }
        /// <summary>
        /// 添加
        /// </summary>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = sysService.GetDataByKey("T_User", "ID", _ID);
            DataRow dr = null;
            if (dt.Rows.Count == 0)
            {
                Hashtable hs = new Hashtable();
                hs.Add("UserName", this.txtUserName.Text);
                DataTable dtD = sysService.GetDataByWhere("T_User", hs);
                if (dtD.Rows.Count > 0)
                {
                    RegistScript("AlertInfo('用户名已存在');");
                    return;
                }
                dr = dt.NewRow();
                dr["CreateBy"] = UserInfo.ID;
                dr["CreateTime"] = DateTime.Now;
            }
            else
            {
                dr = dt.Rows[0];
            }
            dr["UserName"] = this.txtUserName.Text;
            if (this.txtPassword.Text != "")
                dr["Password"] = DESEncrypt.Encrypt(this.txtPassword.Text);
            dr["Name"] = this.txtName.Text;
            dr["RoleID"] = this.ddlRole.SelectedValue;
            dr["Status"] = this.cbDisabled.Checked ? EnumStatus.Disabled : EnumStatus.Normal;
            if (dt.Rows.Count == 0)
                dt.Rows.Add(dr);
            int res = sysService.UpdateDataTable(dt);
            if (res > 0)
                InvokeScript("CloseWin('添加成功！',parent.GetList)");
            else
                InvokeScript("CloseWin('添加失败！')");
        }
    }
}
