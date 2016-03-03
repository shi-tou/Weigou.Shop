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

namespace Weigou.Manage.Sys
{
    public partial class UserPwd : ManagePage
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
            this.txtUserName.Enabled = false;
        }
        
        /// <summary>
        /// 添加
        /// </summary>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = sysService.GetDataByKey("T_User", "ID", _ID);
            int res = 0;
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["Password"] = DESEncrypt.Encrypt(this.txtPassword.Text);
                res = sysService.UpdateDataTable(dt);
            }
            if (res > 0)
                ShowMsg("/Sys/UserList.aspx","恭喜您，密码修改成功！");
            else
                ShowMsg("/Sys/UserList.aspx", "很抱歉，密码修改失败！");
        }
    }
}
