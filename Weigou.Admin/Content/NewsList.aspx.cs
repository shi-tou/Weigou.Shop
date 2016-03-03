using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Model;

namespace Weigou.Admin.Content
{
    public partial class NewsList : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ToolBar1.PrivilegeCode = "NewsList";
                DataTable dt = goodsService.GetDataByKey("T_BaseData", "ParentID", 0);
                DataBind(dt, "DropDownList", ddlType, "Name", "ID");
                ddlType.Items.Insert(0, new ListItem("全部",""));
            }
        }
    }
}