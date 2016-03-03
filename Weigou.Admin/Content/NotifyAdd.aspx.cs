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

namespace Weigou.Admin.Content
{
    public partial class NotifyAdd : AdminPage
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
            DataTable dt = contentService.GetDataByKey("T_Notify", "ID", _ID);
            DataRow dr = dt.Rows[0];
            int type = Convert.ToInt32(dr["Type"]);
            this.ddlType.SelectedValue = type.ToString();
            this.txtTitle.Text = Convert.ToString(dr["Content"]);
            this.txtContent.Text = Convert.ToString(dr["Content"]);
            if (type == 1)
            {
                this.txtMemberName.Text = contentService.GetDataByKey("T_Member", "ID", dr["MemberID"]).Rows[0]["Name"].ToString();
            }
            else
            {
                this.txtMemberName.Text = contentService.GetDataByKey("T_Merchant", "ID", dr["MerchantID"]).Rows[0]["Name"].ToString();
            }
        }
            
        /// <summary>
        /// 添加
        /// </summary>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = sysService.GetDataByKey("T_Notify", "ID", _ID);
            DataRow dr = null;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["CreateBy"] = UserInfo.ID;
                dr["CreateTime"] = DateTime.Now;
                dr["Status"] = 0;
            }
            else
            {
                dr = dt.Rows[0];
            }
            string type = this.ddlType.SelectedValue;
            dr["Type"] = type;
            dr["Title"] = this.txtTitle.Text;
            dr["Content"] = this.txtContent.Text;
            if (type == "1")
            {
                if(this.hfMemberID.Value == "")
                    dr["MemberID"] = DBNull.Value;
                else
                    dr["MemberID"] = this.hfMemberID.Value;
            }
            else
            {
                dr["MemberID"] = DBNull.Value;
            }
            if (dt.Rows.Count == 0)
                dt.Rows.Add(dr);
            int res = contentService.UpdateDataTable(dt);
            if (res > 0)
                InvokeScript("CloseWin('添加成功！',parent.GetList)");
            else
                InvokeScript("CloseWin('添加失败！')");
        }
    }
}