using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;
using System.Data;

namespace Weigou.Manage.Goods
{
    public partial class GoodsSelect : ManagePage
    {
        /// <summary>
        /// 商品状态
        /// </summary>
        public string Status
        {
            get { return GetRequest("Status", ""); }
        }
        /// <summary>
        /// 上架状态
        /// </summary>
        public string ShelvesStatus
        {
            get { return GetRequest("ShelvesStatus", ""); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.hfStatus.Value = Status;
                this.hfShelvesStatus.Value = ShelvesStatus;
            }
        }
    }
}
