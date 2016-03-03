using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Admin
{
    public partial class LoginOut : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (UserInfo != null)
                {
                    HttpCookie cookie = new HttpCookie(UserCookieName);
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);

                    DataTable dt = sysService.GetDataByKey("T_User", "ID", UserInfo.ID);
                    if (dt.Rows.Count > 0)
                    {
                        dt.Rows[0]["LastLoginTime"] = DateTime.Now;
                        if (sysService.UpdateDataTable(dt) > 0)
                        {
                            sysService.SaveSysLog(UserInfo.ID.ToString() , Model.Enum.EnumModule.SystemManage, Model.Enum.EnumOperation.Login, UserInfo.ID, "用户退出");
                        }
                    }
                }
                Response.Redirect("/Login.aspx");
            }
        }
    }
}