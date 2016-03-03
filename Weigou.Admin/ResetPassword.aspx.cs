using Weigou.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Admin
{
    public partial class ResetPassword : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 修改密码
        /// </summary>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = sysService.GetDataByKey("T_User", "ID", UserInfo.ID);
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
            dr["PassWord"] = DESEncrypt.Encrypt(this.txtNewPwd.Text);
            int res = sysService.UpdateDataTable(dt);
            if (res > 0)
                InvokeScript("CloseWin('修改成功！',parent.GetList)");
            else
                InvokeScript("CloseWin('修改失败！')");
        }
    }
}