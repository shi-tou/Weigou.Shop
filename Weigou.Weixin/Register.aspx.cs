using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Weixin
{
    public partial class Register : WeixinPage
    {
        /// <summary>
        /// 推荐人ID
        /// </summary>
        public int RecommendMemberID
        {
            get { return GetRequest("mid",0); }
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.hfRecommendMemberID.Value = RecommendMemberID.ToString();
            }
        }

    }
}
