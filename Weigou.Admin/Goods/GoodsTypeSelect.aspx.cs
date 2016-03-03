using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;

namespace Weigou.Admin.Goods
{
    public partial class GoodsTypeSelect : AdminPage
    {
        /// <summary>
        /// 商户ID控件名
        /// </summary>
        public string IDControl
        {
            get { return GetRequest("IDControl", ""); }
        }

        /// <summary>
        /// 商户Name控件名
        /// </summary>
        public string NameControl
        {
            get { return GetRequest("NameControl", ""); }
        }

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
