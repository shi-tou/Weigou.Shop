using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Service;
using System.Data;
using System.Collections;
using Weigou.Common;
using System.Text;
using Weigou.Model;
using Weigou.Model.Enum;

namespace Weigou.Manage.Weixin
{
    public partial class WeixinMenuAdd : ManagePage
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        private int _ID
        {
            get { return GetRequest("ID", 0); }
        }
        /// <summary>
        /// 菜单父ID
        /// </summary>
        private int ParentID
        {
            get { return GetRequest("ParentID",0); }
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
            DataTable dt = sysService.GetDataByKey("T_WeixinMenu", "ID", _ID);
            DataRow dr = dt.Rows[0];
            this.txtName.Text = Convert.ToString(dr["Name"]);
            this.ddlType.SelectedValue = Convert.ToString(dr["Type"]);
            this.txtKey.Text = Convert.ToString(dr["Key"]);
            this.txtUrl.Text = Convert.ToString(dr["Url"]);

            DataTable dtP = sysService.GetDataByKey("T_WeixinMenu", "ID", ParentID);
            if (dtP.Rows.Count > 0)
            {
                this.txtParentName.Text = dtP.Rows[0]["Name"].ToString();
            }
            else
            {
                this.txtParentName.Text = "根节点";
            }
            this.txtParentName.Enabled = false;

        }
       
        /// <summary>
        /// 添加
        /// </summary>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = sysService.GetDataByKey("T_WeixinMenu", "ID", _ID);
            DataRow dr = null;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr["ParentID"] = ParentID;
                dr["CreateBy"] = UserInfo.ID;
                dr["CreateTime"] = DateTime.Now;
            }
            else
            {
                dr = dt.Rows[0];
            }
            dr["Type"] = this.ddlType.SelectedValue;
            dr["Name"] = this.txtName.Text;
            dr["Key"] = this.txtKey.Text;
            dr["Url"] = this.txtUrl.Text;
            int res = weixinService.UpdateDataTable(dt);
            if (res > 0)
                ShowMsg("/Weixin/WeixinMenuList.aspx", "恭喜您,保存成功！");
            else
                ShowMsg("/Weixin/WeixinMenuList.aspx", "很抱歉，操作失败,请重试！");
        }
    }
}