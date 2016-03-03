using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Manage.Goods
{
    public partial class GoodsBrandAdd : ManagePage
    {
        /// <summary>
        /// 品牌ID
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
            DataTable dt = goodsService.GetDataByKey("T_Brand", "ID", _ID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.txtName.Text = dr["Name"].ToString();
                this.hfAvatar.Value = dr["Logo"].ToString();
                this.AvatarUrl.ImageUrl = dr["Logo"].ToString();
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = goodsService.GetDataByKey("T_Brand", "ID", _ID);
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
            dr["Logo"] = Upload(this.txtLogo, this.hfAvatar.Value, "Brand");

            int res = goodsService.UpdateDataTable(dt);
            if (res > 0)
                ShowMsg("/Goods/GoodsBrandList.aspx", "操作成功");
            else
                ShowMsg("/Goods/GoodsBrandList.aspx", "操作失败");
        }
    }
}