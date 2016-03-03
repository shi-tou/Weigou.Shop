using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;
using System.Data;

namespace Weigou.Admin.Goods
{
    public partial class GoodsAttributeList : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ToolBar1.PrivilegeCode = "GoodsAttributeList";
            }
        }
    }
}
