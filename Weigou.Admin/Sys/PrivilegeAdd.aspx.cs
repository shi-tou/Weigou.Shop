using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Service;
using System.Data;
using Weigou.Model;
using Weigou.Model.Enum;
using System.Collections;

namespace Weigou.Admin.Sys
{
    public partial class PrivilegeAdd : AdminPage
    {
        
        /// <summary>
        /// 修改的资源编码
        /// </summary>
        private int _ID
        {
            get { return GetRequest("ID", 0); }
        }
        /// <summary>
        /// 添加子节点的父编码
        /// </summary>
        private string ParentCode
        {
            get { return GetRequest("ParentCode", ""); }
        }
        /// <summary>
        /// 页面加载
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
                if (ParentCode != "")
                {
                    this.txtParentCode.Text = ParentCode;
                }
                this.txtParentCode.ReadOnly = true;
                this.txtParentCode.Attributes.Add("style", "background:#eee;");
            }
        }
        /// <summary>
        /// 绑定资源信息
        /// </summary>
        private void BindInfo()
        {
            DataTable dt=sysService.GetDataByKey("T_Privilege","ID",_ID);
            if (dt.Rows.Count == 0)
                return;
            DataRow dr = dt.Rows[0];
            this.txtParentCode.Text = Convert.ToString(dr["ParentCode"]);
            this.ddlPrivilegeType.SelectedValue = Convert.ToString(dr["PrivilegeType"]);
            this.txtCode.Text = Convert.ToString(dr["Code"]);
            this.txtName.Text = Convert.ToString(dr["Name"]);
            this.txtUrl.Text = Convert.ToString(dr["Url"]);
            this.txtFunc.Text = Convert.ToString(dr["Func"]);
            this.txtIcon.Text = Convert.ToString(dr["Icon"]);
            this.txtSort.Text = Convert.ToString(dr["Sort"]);
            this.cbDisabled.Checked = Convert.ToInt16(dr["Status"]) == (int)EnumStatus.Disabled ? true : false;
        }
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = sysService.GetDataByKey("T_Privilege", "ID", _ID);
            DataRow dr;
            if (dt.Rows.Count == 0)
            {
                Hashtable hs = new Hashtable();
                hs.Add("Code",this.txtCode.Text);
                DataTable dtD = sysService.GetDataByWhere("T_Privilege", hs);
                if (dtD.Rows.Count > 0)
                {
                    RegistScript("AlertInfo('重复编码');");
                    return;
                }
                dr = dt.NewRow();
            }
            else
                dr = dt.Rows[0];
            dr["PrivilegeType"] = this.ddlPrivilegeType.SelectedValue;
            dr["Code"] = this.txtCode.Text;
            dr["Name"] = this.txtName.Text;
            dr["Url"] = this.txtUrl.Text;
            dr["Func"] = this.txtFunc.Text;
            dr["Icon"] = this.txtIcon.Text;
            if (this.txtParentCode.Text != "")
            {
                dr["ParentCode"] = this.txtParentCode.Text;
            }
            dr["Sort"] = this.txtSort.Text == "" ? "0" : this.txtSort.Text;
            dr["Status"] = this.cbDisabled.Checked ? EnumStatus.Disabled : EnumStatus.Normal;
            if (dt.Rows.Count == 0)
                dt.Rows.Add(dr);
            int res = sysService.UpdatePrivilege(dt, UserInfo.ID);
            if (res > 0)
                RegistScript("CloseWin('操作成功！',parent.GetList);");
            else
                RegistScript("alert('操作失败');");
        }
    }
}