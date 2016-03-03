using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Web.App
{
    public partial class ServiceList : AppBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //租客相关
                this.repNewsType1.DataSource = contentService.GetDataByKey("T_NewsType", "ParentID", 1);
                this.repNewsType1.DataBind();
                //车东相关
                this.repNewsType2.DataSource = contentService.GetDataByKey("T_NewsType", "ParentID", 2);
                this.repNewsType2.DataBind();
            }
        }
    }
}