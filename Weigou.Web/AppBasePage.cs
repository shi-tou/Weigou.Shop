using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Weigou.Common;
using System.IO;
using Weigou.Service;
using System.Text;

namespace Weigou.Web
{
    public class AppBasePage : BasePage
    {
        public int index = 0;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            RegisterIncScriptBlock();
            base.OnInit(e);
        }
        /// <summary>
        /// 注册INC脚本块
        /// </summary>
        public void RegisterIncScriptBlock()
        {
            this.Header.Controls.AddAt(index++, RegistCSS("/App/AmazeUI/css/amazeui.min.css"));

            this.Header.Controls.AddAt(index++, RegistJavaScript("/JScript/jquery-1.8.0.min.js"));
            this.Header.Controls.AddAt(index++, RegistJavaScript("/App/AmazeUI/js/amazeui.min.js"));
        }
    }
}
