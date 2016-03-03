using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Admin.Goods
{
    public partial class GoodsCommentList : AdminPage
    {
        /// <summary>
        /// 回复状态
        /// </summary>
        public string ReplyStatus
        {
            get
            {
                return GetRequest("ReplyStatus", "");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ToolBar1.PrivilegeCode = "GoodsCommentList";
            this.hfReply.Value = CheckAuth("ReplyGoodsComment").ToString();
            this.ddlReply.SelectedValue = ReplyStatus;
        }
    }
}