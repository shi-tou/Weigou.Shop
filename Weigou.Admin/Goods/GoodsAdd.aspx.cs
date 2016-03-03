using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Service;
using System.Data;
using Weigou.Common;
using Weigou.Model.Enum;
using System.Collections;

namespace Weigou.Admin.Goods
{
    public partial class GoodsAdd : AdminPage
    {
        /// <summary>
        /// 商品ID
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
                if (_ID != 0)
                {
                    BindInfo();
                }
            }
            if (this.hfTypeID.Value != "")
            {
                LoadGoodsExtControl();
                LoadGoodsBrandControl();
            }
        }
        /// <summary>
        /// 加载商品属性
        /// </summary>
        private void LoadGoodsExtControl()
        {
            this.Table1.Rows.Clear();
            DataTable dt = goodsService.GetGoodsAttribute(Convert.ToInt32(this.hfTypeID.Value));
            foreach (DataRow dr in dt.Rows)
            {
                TableRow row = new TableRow();
                TableCell cellHead = new TableCell();
                TableCell cellContent = new TableCell();

                Label lab = new Label();
                lab.Text = Convert.ToString(dr["Name"]) + "：";
                lab.CssClass = "Father_Title";
                lab.Attributes.Add("alt", dr["ID"].ToString());
                cellHead.Controls.Add(lab);

                CheckBoxList cbl = new CheckBoxList();
                cbl.ID = "cblAttr_" + Convert.ToString(dr["ID"]);
                cbl.RepeatDirection = RepeatDirection.Horizontal;
                cbl.RepeatColumns = 10;
                //商品属性的值集合
                DataTable dtValue = goodsService.GetDataByKey("T_GoodsAttributeValue", "AttributeID", dr["ID"]);
                DataBind(dtValue, "CheckBoxList", cbl, "Value", "ID");
                foreach (ListItem item in cbl.Items)
                {
                    item.Attributes.Add("alt", item.Value);
                    item.Attributes.Add("altText", item.Text);
                }
                cellContent.Controls.Add(cbl);
                row.Cells.Add(cellHead);
                row.Cells.Add(cellContent);
                this.Table1.Rows.Add(row);
            }

            //获取当前商品属性
            DataTable dtGoodsSaleProp = goodsService.GetDataByKey("T_GoodsSaleProp", "GoodsID", _ID);
            if (dtGoodsSaleProp.Rows.Count > 0)
            {
                strJson = "{\"GoodsSaleProp\":" + Newtonsoft.Json.JsonConvert.SerializeObject(dtGoodsSaleProp) + "}";
            }
            else
            {
                strJson = "0";
            }
        }
        /// <summary>
        /// 加载商品品牌
        /// </summary>
        private void LoadGoodsBrandControl()
        {
            this.pBrand.Controls.Clear();
            DataTable dt = goodsService.GetBrandByTypeID(this.hfTypeID.Value);

            RadioButtonList radlist = new RadioButtonList();
            radlist.ID = "radBrand";
            radlist.RepeatDirection = RepeatDirection.Horizontal;
            radlist.RepeatColumns = 10;
            foreach (DataRow dr in dt.Rows)
            {
                radlist.Items.Add(new ListItem(Convert.ToString(dr["Name"]), Convert.ToString(dr["BrandID"])));
            }
            if (_ID > 0)
            {
                DataTable dt2 = goodsService.GetDataByKey("T_Goods", "ID", _ID);
                if (dt2.Rows.Count > 0)
                {
                    foreach (ListItem item in radlist.Items)
                    {
                        if (item.Value == Convert.ToString(dt2.Rows[0]["BrandID"]))
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
            pBrand.Controls.Add(radlist);
        }
        /// <summary>
        /// 绑定商品信息
        /// </summary>
        public void BindInfo()
        {
            DataTable dt = goodsService.GetDataByKey("T_Goods", "ID", _ID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.txtCode.Text = dr["Code"].ToString();
                this.txtName.Text = dr["Name"].ToString();
                this.hfTypeID.Value = dr["Type"].ToString();
                this.txtTypeName.Text = goodsService.GetDataByKey("T_GoodsType", "ID", dr["Type"].ToString()).Rows[0]["Name"].ToString();
                this.txtSalePrice.Text = dr["SalePrice"].ToString();
                this.txtSalePrice.Text = dr["SalePrice"].ToString();
                this.txtMarketPrice.Text = dr["MarketPrice"].ToString();
                this.txtStock.Text = dr["Stock"].ToString();
                this.txtSimpleDesc.Text = Convert.ToString(dr["SimpleDesc"]);
                //this.txtDescriptionPC.Text = dr["DescriptionPC"].ToString();
                this.txtDescription.Text = dr["Description"].ToString();
                this.ddlShelvesStatus.SelectedValue = dr["ShelvesStatus"].ToString();
                if (dr["Status"].ToString() == "0")
                {
                    this.txtStatus.Text = "待审核";
                }
                else if (dr["Status"].ToString() == "1")
                {
                    this.txtStatus.Text = "审核通过";
                }
                else
                {
                    this.txtStatus.Text = "审核未通过";
                }
                IsViewStatus.Visible = true;

            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = goodsService.GetDataByKey("T_Goods", "ID", _ID);
            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
            }
            else
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr = dt.Rows[0];
                dr["CreateTime"] = DateTime.Now;
                dr["CreateBy"] = UserInfo.ID;
                dr["Status"] = Convert.ToInt16(EnumStatus.Disabled);
            }

            dr["Code"] = this.txtCode.Text;
            dr["Name"] = this.txtName.Text;
            dr["Type"] = this.hfTypeID.Value;
            dr["SalePrice"] = this.txtSalePrice.Text;
            dr["MarketPrice"] = this.txtMarketPrice.Text;
            dr["Stock"] = this.txtStock.Text;
            dr["SimpleDesc"] = this.txtSimpleDesc.Text;
            dr["Description"] = this.txtDescription.Text;
            dr["ShelvesStatus"] = this.ddlShelvesStatus.SelectedValue;
            if (this.ddlShelvesStatus.SelectedValue == "1")
            {
                dr["ShelvesTime"] = DateTime.Now;
            }
            //获取品牌信息
            bool flag = false;
            foreach (Control c in this.pBrand.Controls)
            {
                if (c.GetType().ToString() == "System.Web.UI.WebControls.RadioButtonList")
                {
                    foreach (ListItem item in ((RadioButtonList)c).Items)
                    {
                        if (item.Selected)
                        {
                            dr["BrandID"] = item.Value;
                            flag = true;
                        }
                    }
                }
            }
            if (flag == false)
            {
                dr["BrandID"] = "0";
            }

            int goodsID = _ID;
            int res;
            if (goodsID > 0)
            {
                res = goodsService.UpdateDataTable(dt);
            }
            else
            {
                goodsID = goodsService.Insert(dt);
                res = 1;
            }

            if (res > 0)
            {
                SaveAttr(goodsID);
                RegistScript("CloseWin('操作成功！',parent.GetList);");
            }
            else
            {
                RegistScript("ShowMsg('操作失败');");
            }
        }

        /// <summary>
        /// 保存属性
        /// </summary>
        public void SaveAttr(int goodsID)
        {
            string strSaleProps = Request.Form["hideSaleProp"];
            string strQuantitys = Request.Form["Txt_Stock"];//Quantity
            string strSalePrices = Request.Form["Txt_SalePrice"];
            string strMarketPrices = Request.Form["Txt_MarketPrice"];
            string strGoodsSalePropIDs = Request.Form["hideGoodsSalePropID"];

            if (!string.IsNullOrEmpty(strSaleProps))
            {
                //截取每个文本框输入值
                string[] strSaleProp = strSaleProps.Split(',');
                string[] strQuantity = strQuantitys.Split(',');
                string[] strSalePrice = strSalePrices.Split(',');
                string[] strMarketPrice = strMarketPrices.Split(',');
                Hashtable hsIndex = new Hashtable();
                if (strGoodsSalePropIDs != null)
                {
                    string[] strGoodsSalePropID = strGoodsSalePropIDs.Split(',');
                    //循环获取已有ID值，获取下标对应表单
                    for (int i = 0; i < strGoodsSalePropID.Length; i++)
                    {
                        string[] strs = strGoodsSalePropID[i].Split('_');
                        hsIndex.Add(strs[0], strs[1]);
                    }
                }

                DataTable dt = goodsService.GetDataByKey("T_GoodsSaleProp", "ID", 0);
                DataRow dr;
                for (int i = 0; i < strSaleProp.Length; i++)
                {
                    dr = dt.NewRow();
                    dr["GoodsID"] = goodsID;
                    dr["SaleProp"] = strSaleProp[i];
                    dr["Stock"] = strQuantity[i];
                    dr["SalePrice"] = strSalePrice[i];
                    dr["MarketPrice"] = strMarketPrice[i];
                    dr["MinPrice"] = strMarketPrice[i];
                    dr["CreateBy"] = UserInfo.ID;
                    dr["CreateTime"] = DateTime.Now;
                    dt.Rows.Add(dr);
                }
                //先删除属性表
                goodsService.Delete("T_GoodsSaleProp", "GoodsID", goodsID);
                goodsService.UpdateDataTable(dt);
            }
        }

        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            LoadGoodsExtControl();
            LoadGoodsBrandControl();
        }

    }
}
