using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Admin.Member
{
    public partial class MemberSelect : AdminPage
    {
        /// <summary>
        /// 会员ID控件名
        /// </summary>
        public string IDControl
        {
            get { return GetRequest("IDControl", ""); }
        }
        /// <summary>
        /// 会员名控件名
        /// </summary>
        public string NameControl
        {
            get { return GetRequest("NameControl", ""); }
        }
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.hfIDControl.Value = IDControl;
                this.hfNameControl.Value = NameControl;
            }
        }
    }
}
