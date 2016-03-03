using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Manage.Order
{
    public partial class MallRemindSendList : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //this.ToolBar1.PrivilegeCode = "MallOrderList";
            }
        }
    }
}