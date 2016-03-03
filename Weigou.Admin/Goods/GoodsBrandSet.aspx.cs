using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Admin.Goods
{
    public partial class GoodsBrandSet : AdminPage
    {
        /// <summary>
        /// 商品分类ID
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
                BindBrandInfo();
            }
        }

        /// <summary>
        /// 绑定商户类别信息
        /// </summary>
        public void BindInfo()
        {
            if (_ID == 0)
                return;
            DataTable dt = goodsService.GetDataByKey("T_GoodsType", "ID", _ID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.labName.Text = dr["Name"].ToString();
            }
        }
        /// <summary>
        /// 绑定品牌信息
        /// </summary>
        public void BindBrandInfo()
        {
            DataTable dt = goodsService.GetData("T_Brand");
            DataBind(dt, "CheckBoxList", cheBrand, "Name", "ID");
            List<string> list = new List<string>();
            DataTable dt2 = goodsService.GetDataByKey("T_GoodsTypeBrand", "GoodsTypeID", _ID);
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    list.Add(Convert.ToString(dt2.Rows[i]["BrandID"]));
                }
                foreach (ListItem item in cheBrand.Items)
                {
                    if (list.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }

            }

        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = goodsService.GetDataByKey("T_GoodsTypeBrand", "ID", 0);
            DataRow dr;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.Rows[0];
            }

            goodsService.Delete("T_GoodsTypeBrand", "GoodsTypeID", _ID);
            foreach (ListItem item in cheBrand.Items)
            {
                if (item.Selected)
                {
                    dr["GoodsTypeID"] = _ID;
                    dr["BrandID"] = item.Value;
                    goodsService.Insert(dt);
                }
            }
            RegistScript("CloseWin('操作成功！',parent.ReLoad);");
        }
    }
}