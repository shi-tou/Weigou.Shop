using Weigou.Model.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;

namespace Weigou.Manage.Goods
{
    public partial class GoodsCommentList : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {  
            if (!IsPostBack)
            {
                if (!CheckAuth("GoodsCommentList"))
                    return;
                DataTable dt = goodsService.GetDataByKey("T_GoodsType", "ParentID", 0);
                BindGoodsType(this.ddlType1, "全部类别");
                BindList();
            }
            this.hfReply.Value = CheckAuth("ReplyGoodsComment").ToString();
        }
        /// <summary>
        /// 绑定用户数据
        /// </summary>
        public void BindList()
        {
            Hashtable hs = new Hashtable();
            if (this.txtName.Text.Trim() != "")
            {
                hs.Add("GoodsName", this.txtName.Text.Trim());
            }
            if (this.ddlReply.SelectedValue.Trim() != "")
            {
                hs.Add("Reply", this.ddlReply.SelectedValue.Trim());
            }
            if (this.ddlType1.SelectedValue.Trim() != "")
            {
                hs.Add("GoodsTypeID", this.ddlType1.SelectedValue.Trim());
            }
            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "a.CreateTime desc");
            goodsService.GetGoodsCommentList(p, hs);
            this.AspNetPager.RecordCount = p.ItemCount;//记录总数
            this.repGoods.DataSource = p.DataSource;
            this.repGoods.DataBind();
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
        /// <summary>
        /// 查询事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindList();
        }
        /// <summary>
        /// 列表行操作事件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repGoods_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            if (e.CommandName == "Delete")
            {
                int res = 0;
                DataTable dt = goodsService.GetDataByKey("T_Comment", "ID", id);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                    res = goodsService.UpdateDataTable(dt);
                }
                if (res > 0)
                    ShowMsg(this.Request.Url.ToString(), "删除商品成功");
                else
                    ShowMsg(this.Request.Url.ToString(), "删除商品失败");
            }

        }

        public string GetReplyStatus(string v)
        {
            if (v != "0")
            {
                return "<span style=\"color:green;\">已回复</span>";
            }
            else
            {
                return "<span style=\"color:red;\">未回复</span>";
            }
        }
    }
}