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
using Weigou.Easemob;

namespace Weigou.Manage.Easemob
{
    public partial class EasemobGroupAdd : ManagePage
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        private string GroupID
        {
            get { return GetRequest("GroupID", ""); }
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
                if (GroupID != "")
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
            DataTable dt = memberService.GetDataByKey("T_EasemobGroup", "GroupID", GroupID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.hfPhoto.Value = Convert.ToString(dr["hfPhoto"]);
                this.ImgPhoto.ImageUrl = Convert.ToString(dr["hfPhoto"]);
                this.txtGroupName.Text = Convert.ToString(dr["GroupName"].ToString());
                this.txtGroupDesc.Text = Convert.ToString(dr["GroupDesc"].ToString());
                this.cbIsPublic.Checked = Convert.ToBoolean(dr["IsPublic"]);
                this.cbApproval.Checked = Convert.ToBoolean(dr["Approval"]);
                this.txtMaxUsers.Text = Convert.ToString(dr["MaxUsers"]);
                this.txtOwner.Text = Convert.ToString(dr["Owner"]);
                this.txtOwner.Enabled = false;
            }
        }
        /// <summary>
        /// 保存会员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = memberService.GetDataByKey("T_EasemobGroup", "GroupID", GroupID);
            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
            }
            else
            {
                dr = dt.NewRow();
                dr["GroupID"] = "g" + DateTime.Now.ToString("yyyyMMddHHmmsss");
                dr["CreateBy"] = UserInfo.ID;
                dr["CreateTime"] = DateTime.Now;
            }
            dr["GroupName"] = this.txtGroupName.Text;
            dr["GroupDesc"] = this.txtGroupDesc.Text;
            dr["Photo"] = Upload(this.fuPhoto, this.hfPhoto.Value, "Group");
            dr["IsPublic"] = this.cbIsPublic.Checked;
            dr["Approval"] = this.cbApproval.Checked;
            dr["MaxUsers"] = this.txtMaxUsers.Text;
            dr["Owner"] = this.txtOwner.Text;
            if (memberService.UpdateDataTable(dt) > 0)
            {
                ShowMsg("/Member/EasemobGroupList.aspx", "保存成功!");
            }
            else
            {
                ShowMsg("/Member/EasemobGroupList.aspx", "保存失败,请与管理员联系！");
            }
        }
    }
}

