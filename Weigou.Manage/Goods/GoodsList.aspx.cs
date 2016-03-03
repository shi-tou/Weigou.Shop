using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;
using Weigou.Model.Enum;
using System.Data;
using System.Collections;

namespace Weigou.Manage.Goods
{
    public partial class GoodsList : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!CheckAuth("GoodsList"))
                    return;
                DataTable dt = goodsService.GetDataByKey("T_GoodsType", "ParentID",0);
                BindGoodsType(this.ddlType1,"选择商品类别");
                BindList();
            }
        }
        /// <summary>
        /// 绑定用户数据
        /// </summary>
        public void BindList()
        {
            Hashtable hs = new Hashtable();
            if (this.txtName.Text.Trim() != "")
            {
                hs.Add("Name", this.txtName.Text.Trim());
            }
            if (this.ddlStatus.SelectedValue.Trim() != "")
            {
                hs.Add("Status", this.ddlStatus.SelectedValue.Trim());
            }
            if (this.ddlShelvesStatus.SelectedValue.Trim() != "")
            {
                hs.Add("ShelvesStatus", this.ddlShelvesStatus.SelectedValue.Trim());
            }
            if (this.ddlType1.SelectedValue.Trim() != "")
            {
                if (this.ddlType1.SelectedItem.Text.Trim().Contains("∟"))
                {
                    hs.Add("Type", this.ddlType1.SelectedValue.Trim());
                }
                else
                {
                    hs.Add("ParentID", this.ddlType1.SelectedValue.Trim()); 
                }
            } 
            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "a.CreateTime desc");
            goodsService.GetGoodsList(p, hs);
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
                DataTable dt = goodsService.GetDataByKey("T_Goods", "ID", id);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                    res = goodsService.UpdateDataTable(dt);
                }
                if (res > 0)
                    ShowMsg(this.Request.Url.ToString(), "删除商品成功");
                else
                    ShowMsg("/Goods/GoodsList.aspx", "删除商品失败");
            }

        }
        #region 数据转换显示方法

        //审核状态
        public string GetStatus(object s)
        {
            switch (Convert.ToInt32(s))
            {
                case 0:
                    return "待审核";
                case 1:
                    return "<span style=\"color:green;\">审核通过</span>";
                case 2:
                    return "<span style=\"color:red;\">审核未通过</span>";
                default: return "";
            }
        }

        //上架状态
        public string GetShelvesStatus(object s)
        {
            switch (Convert.ToInt32(s))
            {
                case 0:
                    return "下架";
                case 1:
                    return "<span style=\"color:green;\">上架</span>";
                default: return "";
            }
        }
       

        //设置颜色
        public string SetColor(object v,string color)
        {
            //if (Convert.ToInt32(v) > 10)
            //    return v.ToString();
            return "<span style=\"color:"+color+"\">" + v + "</span>";
        }
          
        #endregion
    }
}
