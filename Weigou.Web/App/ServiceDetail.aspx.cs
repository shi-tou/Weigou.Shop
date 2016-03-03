using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Web.App
{
    public partial class ServiceDetail : AppBasePage
    {
        /// <summary>
        /// T_News.ID
        /// </summary>
        public int Type
        {
            get { return GetRequest("type", 0); }
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
                BindData();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void BindData()
        {
            DataTable dt = contentService.GetDataByKey("T_NewsType", "ID", Type);
            if (dt.Rows.Count > 0)
            {
                this.labTitle.Text = Convert.ToString(dt.Rows[0]["Name"]);
            }
            DataTable dtNews = contentService.GetDataByKey("T_News","Type",Type);
            this.repNews.DataSource = dtNews;
            this.repNews.DataBind();
        }
    }
}