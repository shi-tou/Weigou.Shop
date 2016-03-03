using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Model;

namespace Weigou.Admin.Content
{
    public partial class BannerList : AdminPage
    {
        /// <summary>
        /// Banner图ID
        /// </summary>
        public int _ID
        {
            get
            {
                return GetRequest("ID", 0);
            }
        }
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ToolBar1.PrivilegeCode = "BannerList";
                DataTable dt = goodsService.GetDataByKey("T_BaseData", "ParentID", BaseDataConst.BannerType);
                BindBasedataToList(this.ddlType, BaseDataConst.BannerType, "=全部=");
            }
        }
    }
}