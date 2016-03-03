using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;
using Weigou.Model;

namespace Weigou.Web
{
    public partial class Login : WebPage
    {
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie cookie = new HttpCookie(MemberCookieName);
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            MemberInfo info = new MemberInfo();
            string username = this.txtMobileOrEmail.Text;
            string password = this.txtPassword.Text;
            if (username == "" || password == "")
            {
                this.labMsg.Text = "账号/密码不能为空！";
                return;
            }
            int res = memberService.MemberLogin(username,password,out info);
            if (res == RT.RESULT_NOT_EXIST)
            {
                this.labMsg.Text = "账号不存在！";
                this.txtMobileOrEmail.Text = "";
                this.txtPassword.Text = "";
                this.txtMobileOrEmail.Focus();
                return;
            }
            if (res == RT.RESULT_LOCK)
            {
                this.labMsg.Text = "账号已冻结，请与管理员联系！";
                return;
            }
            if (res == RT.RESULT_ERROR_PASSWORD)
            {
                this.labMsg.Text = "密码不正确，请重新输入！";
                this.txtPassword.Text = "";
                this.txtPassword.Focus();
                return;
            }
            SetLoginInfo(info);
            Response.Redirect("/Member/Main.aspx");
        }
        /// <summary>
        /// 设置登录状态及登录信息
        /// </summary>
        /// <param name="username"></param>
        public void SetLoginInfo(MemberInfo info)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(info);
            HttpCookie cookie = new HttpCookie(MemberCookieName);
            cookie.Expires = DateTime.Now.AddDays(1);
            cookie.Value = DESEncrypt.Encrypt(json);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}