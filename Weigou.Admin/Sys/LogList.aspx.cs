using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Admin.Sys
{
    public partial class LogList : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ToolBar1.PrivilegeCode = "LogList";
                Func.BindModule(this.ddlModule, "-全部-");
                Func.BindOperation(this.ddlOperation, "-全部-");
            }
        }
    }
}