using Weigou.Model.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Manage.Sys
{
    public partial class SysVersionAdd : ManagePage
    {
        //ID
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
                BindInfo();
            }
        }
        /// <summary>
        /// 绑定
        /// </summary>
        public void BindInfo()
        {
            if (_ID == 0)
                return;
            DataTable dt = sysService.GetDataByKey("T_AppVersion", "ID", _ID);
            DataRow dr = dt.Rows[0];
            this.radType.SelectedValue = dr["Type"].ToString();
            this.txtVersionCode.Text = dr["VersionCode"].ToString();
            this.txtVersionName.Text = dr["VersionName"].ToString();
            this.txtContent.Text = dr["Content"].ToString();
            this.radForceUpdate.SelectedValue = dr["ForceUpdate"].ToString()=="False"?"0":"1";
            this.txtAppUrl.Text = dr["AppUrl"].ToString();
            //this.radOpenIcon.SelectedValue = dr["OpenIcon"].ToString();
        }

        /// <summary>
        /// 添加
        /// </summary>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = sysService.GetDataByKey("T_AppVersion", "ID", _ID);
            DataRow dr = null;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["CreateBy"] = UserInfo.ID;
                dr["CreateTime"] = DateTime.Now;
            }
            else
            {
                dr = dt.Rows[0];
            }

            dr["Type"] = this.radType.SelectedValue;
            dr["VersionCode"] = this.txtVersionCode.Text;
            dr["VersionName"] = this.txtVersionName.Text;
            dr["Content"] = this.txtContent.Text;
            dr["ForceUpdate"] = this.radForceUpdate.SelectedValue == "0" ? false : true;
           // dr["OpenIcon"] = this.radOpenIcon.SelectedValue;
            dr["AppUrl"] = this.txtAppUrl.Text;
            if (dt.Rows.Count == 0)
                dt.Rows.Add(dr);
            int res = sysService.UpdateDataTable(dt);
            if (res > 0)
            {
                sysService.SaveSysLog(_ID.ToString(), EnumModule.SystemManage, _ID != 0 ? EnumOperation.Edit : EnumOperation.Add, UserInfo.ID, _ID != 0 ? "修改版本信息" : "添加版本信息");
                ShowMsg("/Sys/SysVersionList.aspx", "操作成功");
            }
            else
                ShowMsg("/Sys/SysVersionList.aspx", "操作失败");
        }
    }
}