using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Admin.Order
{
    public partial class MallOrderSaleList : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ToolBar1.PrivilegeCode = "OrderSaleList";
                this.hfEdit.Value = CheckAuth("ViewSaleDetail").ToString();//编辑权限
            }
        }
    }
}