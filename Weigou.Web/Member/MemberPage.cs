using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Weigou.Model;
using Weigou.Common;
using Weigou.Service;
using System.Collections;
using Weigou.Model.Enum;

namespace Weigou.Web
{
    public class MemberPage : BasePage
    {
        /// <summary>
        /// 会员登录信息Cookie名
        /// </summary>
        public string MemberCookieName = "MemberInfo";

        /// <summary>
        /// 当前登录管理员
        /// </summary>
        public MemberInfo MemberInfo
        {
            get
            {
                if (System.Web.HttpContext.Current.Request.Cookies[MemberCookieName] == null)
                {
                    return null;
                }
                string str = DESEncrypt.Decrypt(System.Web.HttpContext.Current.Request.Cookies[MemberCookieName].Value);
                return (MemberInfo)Newtonsoft.Json.JsonConvert.DeserializeObject(str, typeof(MemberInfo));
            }
        }
        /// <summary>
        /// PageTitle
        /// </summary>
        public string PageTitle
        {
            get { return Utils.GetConfig("PageTitle"); }
        }
        /// <summary>
        /// 图片服务器地址
        /// </summary>
        public string PictureServerPath
        {
            get { return Utils.GetConfig("PictureServerPath"); }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            RegisterIncScriptBlock();
            if (Request.Url.ToString().Contains("/Member/"))
            {
                IsLogin();
            }
            base.OnInit(e);
        }
        /// <summary>
        /// 判断是否登录）
        /// </summary>
        public void IsLogin()
        {
            if (MemberInfo == null)
            {
                RegistScript("alert('未登录或登录已失效！');window.parent.location.href='/Login.aspx';");
            }
            return;
        }
        /// <summary>
        /// 注册INC脚本块
        /// </summary>
        public void RegisterIncScriptBlock()
        {
            //this.Header.Controls.AddAt(1, RegistCSS("/JScript/easyui/themes/ui-cupertino/easyui.css"));
            //this.Header.Controls.AddAt(2, RegistCSS("/JScript/easyui/themes/icon.css"));
            //this.Header.Controls.AddAt(3, RegistCSS("/Style/admin.css"));

            //this.Header.Controls.AddAt(4, RegistJavaScript("/JScript/easyui/jquery-1.8.0.min.js"));
            //this.Header.Controls.AddAt(5, RegistJavaScript("/JScript/easyui/jquery.easyui.min.js"));
            //this.Header.Controls.AddAt(6, RegistJavaScript("/JScript/layer/layer.min.js"));
            //this.Header.Controls.AddAt(7, RegistJavaScript("/JScript/global.js"));
            //this.Header.Controls.AddAt(8, RegistJavaScript("/JScript/customValidate.js"));
            //this.Header.Controls.AddAt(9, RegistJavaScript("/JScript/euiHelper.js"));
            //this.Header.Controls.AddAt(10, RegistJavaScript("/JScript/common.js"));
        }
        /// <summary>
        /// 输出JavaScript
        /// </summary>
        /// <param name="strScript"></param>
        public void RegistScript(string strScript)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>" + strScript + "</script>");
        }
    }
}
