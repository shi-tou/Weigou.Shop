using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Model.Enum;
using System.Collections;
using Weigou.Common;

namespace Weigou.Manage.Goods
{
    public partial class AttributeValueSet : ManagePage
    {
        /// <summary>
        /// 属性名ID
        /// </summary>
        public int AttributeID
        {
            get { return GetRequest("AttributeID", 0); }
        }
        /// <summary>
        /// 属性别名
        /// </summary>
        public string AttributeAlias
        {
            get { return GetRequest("AttributeAlias", ""); }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
                this.hfAttributeID.Value = AttributeID.ToString();
                this.labAttribute.Text = AttributeAlias;
            }
        }
        /// <summary>
        /// 绑定用户数据
        /// </summary>
        public void BindList()
        {
            Hashtable hs = new Hashtable();

            hs.Add("AttributeID", AttributeID);
            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "a.CreateTime desc");
            goodsService.GetAttributeValueList(p, hs);
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
                DataTable dt = goodsService.GetDataByKey("T_GoodsAttributeValue", "ID", id);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                    res = goodsService.UpdateDataTable(dt);
                }
                if (res > 0)
                    ShowMsg(this.Request.Url.ToString(), "删除属性值成功");
                else
                    ShowMsg(this.Request.Url.ToString(), "删除属性值失败");
            }

        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = goodsService.GetDataByKey("T_GoodsAttributeValue", "ID", this.hfValueID.Value);
            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
            }
            else
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr["CreateTime"] = DateTime.Now;
                dr["CreateBy"] = UserInfo.ID;
            }
            dr["AttributeID"] = AttributeID;
            dr["Value"] = this.txtValue.Text;
            dr["Status"] = ((int)EnumMemStatus.Normal).ToString();
            dr["CreateBy"] = UserInfo.ID;
            dr["Sort"] = this.txtSort.Text;
            dr["CreateTime"] = DateTime.Now;

            int res = goodsService.UpdateDataTable(dt);
            if (res > 0)
            {
                Response.Redirect(Request.Url.ToString());
            }
            else
                InvokeScript("CloseWin('操作失败！')");
        }
    }
}