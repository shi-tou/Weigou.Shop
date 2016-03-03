using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;
using Weigou.Model;
using Weigou.Model.Enum;
using Weigou.Service;
using Spring.Context;
using Spring.Context.Support;

namespace Weigou.Web.Member
{
    public partial class MyComment : WebPage
    {
       
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
        /// 绑定列表
        /// </summary>
        public void BindList()
        {
            Hashtable hs = new Hashtable();
            hs.Add("MemberID", LoginMemberInfo.ID);
            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "a.CreateTime");
            memberService.GetCommentsList(p, hs);
            this.AspNetPager.RecordCount = p.ItemCount;//记录总数  
            this.repComments.DataSource = p.DataSource;
            this.repComments.DataBind();
        }

        /// <summary>
        /// 分页控件事件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager.CurrentPageIndex = e.NewPageIndex;
            BindList();
        }
    }
}