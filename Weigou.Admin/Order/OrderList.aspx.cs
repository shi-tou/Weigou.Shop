using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weiche.Admin.Order
{
    public partial class OrderList : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                this.ToolBar1.PrivilegeCode = "OrderList";
            }
        }
    }
}