using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Admin.Member
{
    public partial class MemberLevelList : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ToolBar1.PrivilegeCode = "MemberLevelList";
            }
        }
    }
}
