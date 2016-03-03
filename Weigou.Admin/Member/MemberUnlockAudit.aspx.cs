using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Service;
using System.Data;
using Weigou.Model;
using Weigou.Common;
using System.Drawing;
using Weigou.Model.Enum;

namespace Weigou.Admin.Member
{
    public partial class MemberUnlockAudit : AdminPage
    {
        /// <summary>
        /// ID
        /// </summary>
        private int _ID
        {
            get { return GetRequest("ID", 0); }
        }
        /// <summary>
        /// 会员ID
        /// </summary>
        private int _MemberID
        {
            get { return GetRequest("MemberID", 0); }
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
        /// 绑定审核的记录
        /// </summary>
        public void BindInfo()
        {
            DataTable dt = memberService.GetDataByKey("T_MemberUnlock", "ID", _ID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.litRemark.Text = Convert.ToString(dr["Remark"]);
                int status = Convert.ToInt16(dr["Status"]);
                if (status == 0)
                {
                    this.labSatus.Text = "待审核";
                    this.labSatus.ForeColor = Color.Blue;
                }
                else if (status == 1)
                {
                    this.labSatus.Text = "审核通过";
                    this.labSatus.ForeColor = Color.Green;
                }
                else if (status == 2)
                {
                    this.labSatus.Text = "审核不通过";
                    this.labSatus.ForeColor = Color.Red;
                }
                DataTable dtM = memberService.GetDataByKey("T_Member", "ID", _MemberID);
                this.litMemberName.Text = Convert.ToString(dtM.Rows[0]["Name"]);
                this.litMobileNo.Text = Convert.ToString(dtM.Rows[0]["MobileNo"]);
            }
        }
        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAudit_Click(object sender, EventArgs e)
        {

            int res = memberService.AuditMemberUnlock(_MemberID, 1, UserInfo.ID, this.txtRemark.Text);
            if (res > 0)
            {
                RegistScript("CloseWin('操作成功！',parent.GetList);");
            }
            else
            {
                RegistScript("ShowMsg('操作失败！');");
            }
        }
        /// <summary>
        /// 审核不通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDisAudit_Click(object sender, EventArgs e)
        {
            int res = memberService.AuditMemberUnlock(_MemberID, 1, UserInfo.ID, this.txtRemark.Text);
            if (res > 0)
            {
                memberService.SaveSysLog(_ID.ToString(), EnumModule.MemberManage, EnumOperation.Audit, UserInfo.ID, "会员解冻审核通过");
                RegistScript("CloseWin('操作成功！',parent.GetList);");
            }
            else
            {
                RegistScript("ShowMsg('操作失败！');");
            }
        }
    }
}
