using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Service;
using System.Data;
using Weigou.Common;
using Weigou.Model.Enum;

namespace Weigou.Manage.Goods
{
    public partial class GoodsAttributeAdd : ManagePage
    {
        /// <summary>
        /// 商品属性ID
        /// </summary>
        private int _ID
        {
            get { return GetRequest("ID", 0); }
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
                BindInfo();
            }
        }
        
        /// <summary>
        /// 绑定商户类别信息
        /// </summary>
        public void BindInfo()
        {
            if (_ID == 0)
                return;
            DataTable dt = goodsService.GetDataByKey("T_GoodsAttribute", "ID", _ID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.txtName.Text = dr["Name"].ToString();
                this.txtSort.Text = dr["Sort"].ToString();
                this.txtAlias.Text = dr["Alias"].ToString();
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = goodsService.GetDataByKey("T_GoodsAttribute", "ID", _ID);
            DataRow dr;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr["CreateBy"] = UserInfo.ID;
                dr["CreateTime"] = DateTime.Now;
            }
            else
            {
                dr = dt.Rows[0];
            }
            dr["Name"] = this.txtName.Text;
            dr["Alias"] = this.txtAlias.Text.Trim();
            dr["Sort"] = this.txtSort.Text.Trim();
            dr["Status"] = (int)EnumMemStatus.Normal;//默认可用
            int res = goodsService.UpdateDataTable(dt);
            if (res > 0)
            {
                goodsService.SaveSysLog(_ID.ToString(),
                    EnumModule.GoodsManage, _ID == 0 ? EnumOperation.Add : EnumOperation.Edit, UserInfo.ID,
                    (_ID == 0 ? "添加" : "修改") + "了名为【" + this.txtName.Text + "】的商品属性");
                ShowMsg("/Goods/GoodsAttributeList.aspx", "操作成功");
            }
            else
                ShowMsg("/Goods/GoodsAttributeList.aspx", "操作失败");
        }
    }
}
