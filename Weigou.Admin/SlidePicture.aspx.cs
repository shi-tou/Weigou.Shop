using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Admin
{
    public partial class SlidePicture : AdminPage
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        private int _ID
        {
            get { return GetRequest("ID", 0); }
        }
        /// <summary>
        /// 类型ID(1:商品图片轮播)
        /// </summary>
        private int _Type
        {
            get { return GetRequest("Type", 1); }
        }

        public string CssStyle = "width:800px;height:600px;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        public void BindInfo()
        {
            if (_Type == 1)
            {
                Hashtable queryHs = new Hashtable();
                queryHs.Add("Type", 2);
                queryHs.Add("Code", _ID);
                DataTable picDt = goodsService.GetDataByWhere("T_Picture", queryHs);
                if (picDt.Rows.Count > 0)
                {
                    this.repList.DataSource = picDt;
                    this.repList.DataBind();
                }
            }
            else if (_Type == 2)
            {
                Hashtable queryHs = new Hashtable();
                queryHs.Add("Type", 3);
                queryHs.Add("Code", _ID);
                DataTable picDt = goodsService.GetDataByWhere("T_Picture", queryHs);
                if (picDt.Rows.Count > 0)
                {
                    this.repList.DataSource = picDt;
                    this.repList.DataBind();
                }
            }
            else if (_Type == 3)
            {

                DataTable picDt = goodsService.GetDataByKey("T_MallOrderSale", "ID", _ID);
                if (picDt.Rows.Count > 0)
                {
                    DataRow picDr = picDt.Rows[0];
                    string pic = picDr["Pic"].ToString();

                    DataTable dTemp = new DataTable();
                    dTemp.Columns.Add(new DataColumn("LargePicture"));
                   
                    foreach (string item in pic.Split(','))
                    {
                        DataRow dr_Temp = dTemp.NewRow();
                        dr_Temp["LargePicture"] = item;
                        dTemp.Rows.Add(dr_Temp);
                    }
                    this.repList.DataSource = dTemp;
                    this.repList.DataBind();
                }
            }
        }
    }
}