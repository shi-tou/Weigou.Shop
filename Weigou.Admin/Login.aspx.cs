using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using Weigou.Model;
using Weigou.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Service;
using System.IO;
using System.Text;
using Spring.Context;
using Spring.Context.Support;

namespace Weigou.Admin
{
    public partial class Login : System.Web.UI.Page
    {
        public string UserCookieName = "SysUserInfo";
        //注入
        IApplicationContext ctx;
        private static ISysService sysService;
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie cookie = new HttpCookie(UserCookieName);
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
                ctx = ContextRegistry.GetContext();
                sysService = (ISysService)ctx["sysService"];
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = this.txtUserName.Text;
            string password = this.txtPassword.Text;
            if (username == "" || password == "")
            {
                this.lblMsg.Text = "账号/密码不能为空！";
                return;
            }
            if (this.txtUserName.Text == "" || this.txtPassword.Text == "")
            {
                this.lblMsg.Text="账号/密码不能为空！";
                return;
            }
            int res = sysService.UserLogin(this.txtUserName.Text, this.txtPassword.Text);
            if (res == 1)
            {
                this.lblMsg.Text = "账号不存在！";
                this.txtUserName.Text = "";
                this.txtPassword.Text = "";
                this.txtUserName.Focus();
                return;
            }
            if (res == 2)
            {
                this.lblMsg.Text = "账号已禁用，请与管理员联系！";
                return;
            }
            if (res == 3)
            {
                this.lblMsg.Text = "密码不正确，请重新输入！";
                this.txtPassword.Text = "";
                this.txtPassword.Focus();
                return;
            }
            SetLoginInfo(username);
            Response.Redirect("main.aspx");
        }
        /// <summary>
        /// 设置登录状态及登录信息
        /// </summary>
        /// <param name="username"></param>
        public void SetLoginInfo(string username)
        {
            DataTable dt = sysService.GetDataByKey("T_User", "UserName", username);
            DataRow dr = dt.Rows[0];
            UserInfo minfo = new UserInfo();
            minfo.ID = Convert.ToInt32(dr["ID"]);
            minfo.UserName = Convert.ToString(dr["UserName"]);
            minfo.Name = Convert.ToString(dr["Name"]);
            minfo.CreateTime = Convert.ToDateTime(dr["CreateTime"]);
            minfo.LastLoginTime = Convert.ToDateTime(dr["LastLoginTime"]);
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(minfo);
            HttpCookie cookie = new HttpCookie(UserCookieName);
            cookie.Expires = DateTime.Now.AddDays(1);
            cookie.Value = DESEncrypt.Encrypt(json);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}