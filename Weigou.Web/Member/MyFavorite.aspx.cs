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
    public partial class MyFavorite : WebPage
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
                //BindList(EnumFavType.Goods);
                //BindList(EnumFavType.Car);
            }
        }
        /// <summary>
        /// 绑定列表
        /// </summary>
        public void BindList(EnumFavType type)
        {
            Hashtable hs = new Hashtable();
            hs.Add("Type", (int)type);
            hs.Add("MemberID", LoginMemberInfo.ID);

            if (type == EnumFavType.Goods)
            {
                Pager p = new Pager(this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex, "a.CreateTime");
                memberService.GetFavoriteList(p, hs);
                this.AspNetPager1.RecordCount = p.ItemCount;//记录总数  
                this.repFavorite1.DataSource = p.DataSource;
                this.repFavorite1.DataBind();
            }
            else
            {
                Pager p = new Pager(this.AspNetPager2.PageSize, this.AspNetPager2.CurrentPageIndex, "a.CreateTime");
                memberService.GetFavoriteList(p, hs);
                this.AspNetPager2.RecordCount = p.ItemCount;//记录总数  
                this.repFavorite2.DataSource = p.DataSource;
                this.repFavorite2.DataBind();
            }
        }

        /// <summary>
        /// 分页控件事件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanging1(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            BindList(EnumFavType.Goods);
        }
        /// <summary>
        /// 分页控件事件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanging2(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager2.CurrentPageIndex = e.NewPageIndex;
            //BindList(EnumFavType.Merchant);
        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repFavorite_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            if (e.CommandName == "DeleteGoodsFav")
            {
                if (memberService.Delete("T_Favorite", "ID", id) > 0)
                {
                    Alert("删除成功");
                    //BindList(EnumFavType.Goods);
                }
                else
                {
                    Alert("删除失败");
                }
            }
            else
            {
                if (memberService.Delete("T_Favorite", "ID", id) > 0)
                {
                    Alert("删除成功");
                    //BindList(EnumFavType.Merchant);
                }
                else
                {
                    Alert("删除失败");
                }
            }

        }
    }
}