using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;

namespace Weigou.Manage.News
{
    public partial class NewsTypeList : ManagePage
    {
        /// <summary>
        /// 父ID
        /// </summary>
        public int ParentID
        {
            get { return GetRequest("ParentID", 0); }
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
                BindList();
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void BindList()
        {
            Hashtable hs = new Hashtable();
            hs.Add("ParentID", ParentID);
            Pager p=new Pager(100,1,"a.CreateTime desc");
            contentService.GetNewsTypeList(p, hs);
            this.repRole.DataSource = p.DataSource;
            this.repRole.DataBind();
            //添加按键、返回上一级
            DataTable dtP = sysService.GetDataByKey("T_NewsType", "ID", ParentID);
            if (dtP.Rows.Count > 0)
            {
                this.backUrl.HRef = "NewsTypeList.aspx?ParentID=" + dtP.Rows[0]["ParentID"];
                this.addUrl.HRef = "NewsTypeAdd.aspx?ParentID=" + ParentID;
            }
            else
            {
                this.backUrl.Visible = false;
            }
        }
    }
}