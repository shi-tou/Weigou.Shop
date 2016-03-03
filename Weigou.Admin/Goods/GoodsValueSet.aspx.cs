using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weiche.Admin.Goods
{
    public partial class GoodsValueSet : AdminPage
    {
        /// <summary>
        /// 属性名ID
        /// </summary>
        public int _ID
        {
            get { return GetRequest("ID", 0); }
        }
        /// <summary>
        /// 属性别名
        /// </summary>
        public string Alias
        {
            get { return GetRequest("Alias", ""); }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hideID.Value = _ID.ToString();
                labAttribute.Text = Alias;
            }
        }
    }
}