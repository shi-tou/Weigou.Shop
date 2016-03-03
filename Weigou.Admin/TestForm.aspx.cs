using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weiche.Common;
using Weiche.Dao;

namespace Weiche.Admin
{
    public partial class TestForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            MerchantDao merchantDao = new MerchantDao();

            Hashtable hs = new Hashtable();
            hs["MerchantID"] = 0;
            Pager p = new Pager(20, 0, "a.CreateTime desc");
            int result = merchantDao.GetMerchantLogisticsList(p, hs);
            if (result > 0)
            {
                Response.Write("good!");
            }
        }
    }
}