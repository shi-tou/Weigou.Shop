using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;
using Weigou.Model.Enum;
using System.Data;
using System.Collections;
using System;

namespace Weigou.Manage.Sys
{
    public partial class AreaManage : ManagePage
    { 
        protected int TabIndex = 1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProvinceCity();
                BindProvinceDistrict();
                BindCity();
                BindDistrict();
                BindList();
                if (Request["Type"] != null)
                {
                    TabIndex = Convert.ToInt32(Request["Type"]);
                }
            }
        }

        #region 省绑定
        /// <summary>
        /// 绑定用户数据
        /// </summary>
        public void BindList()
        {
            TabIndex = 1;
            Hashtable hs = new Hashtable();

            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "FirstLetter asc");
            sysService.GetProvinceList(p, hs);
            this.AspNetPager.RecordCount = p.ItemCount;//记录总数
            this.repOrder.DataSource = p.DataSource;
            this.repOrder.DataBind();
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
        #endregion

        #region 市绑定
        /// <summary>
        /// 绑定用户数据
        /// </summary>
        public void BindCity()
        {
            TabIndex = 2;
            Hashtable hs = new Hashtable();
            if (this.ddlProvice_City.SelectedValue.Trim() != "")
            {
                hs.Add("ProvinceID", this.ddlProvice_City.SelectedValue.Trim());
            }
            Pager p = new Pager(this.AspNetPagerCity.PageSize, this.AspNetPagerCity.CurrentPageIndex, "a.FirstLetter asc");
            sysService.GetCityList(p, hs);
            this.AspNetPagerCity.RecordCount = p.ItemCount;//记录总数
            this.repOrderWx.DataSource = p.DataSource;
            this.repOrderWx.DataBind();
        }
        /// <summary>
        /// 分页控件事件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void AspNetPagerCity_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPagerCity.CurrentPageIndex = e.NewPageIndex;
            BindCity();
        }

        protected void btnSearch_City_Click(object sender, EventArgs e)
        {
            BindCity();
        }
 
        #endregion

        #region 区绑定

        /// <summary>
        /// 绑定用户数据
        /// </summary>
        public void BindDistrict()
        {
            TabIndex = 3;
            Hashtable hs = new Hashtable();
            if (this.ddlProvice_District.SelectedValue.Trim() != "")
            {
                hs.Add("ProviceID", this.ddlProvice_District.SelectedValue.Trim());
            }
            if (this.ddlCity.SelectedValue.Trim() != "")
            {
                hs.Add("CityID", this.ddlCity.SelectedValue.Trim());
            }

            Pager p = new Pager(this.AspNetPagerDistrict.PageSize, this.AspNetPagerDistrict.CurrentPageIndex, "CityID asc");
            sysService.GetDistrictList(p, hs);
            this.AspNetPagerDistrict.RecordCount = p.ItemCount;//记录总数
            this.repDistrict.DataSource = p.DataSource;
            this.repDistrict.DataBind();
        }
        /// <summary>
        /// 分页控件事件
        /// </summary>
        /// <param name="src"></param>
        /// <param name="e"></param>
        protected void AspNetPagerDistrict_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPagerDistrict.CurrentPageIndex = e.NewPageIndex;
            BindDistrict();
        }

        protected void btnSearch_District_Click(object sender, EventArgs e)
        {
            BindDistrict();
        }
       
        #endregion

        #region 省市区管理
        public void BindProvinceCity()
        {
            DataTable dt = memberService.GetData("T_Province");
            this.ddlProvice_District.DataSource = dt;
            this.ddlProvice_District.DataTextField = "ProvinceName";
            this.ddlProvice_District.DataValueField = "ID";
            this.ddlProvice_District.DataBind();
            this.ddlProvice_District.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        public void BindProvinceDistrict()
        {
            DataTable dt = memberService.GetData("T_Province");
            this.ddlProvice_City.DataSource = dt;
            this.ddlProvice_City.DataTextField = "ProvinceName";
            this.ddlProvice_City.DataValueField = "ID";
            this.ddlProvice_City.DataBind();
            this.ddlProvice_City.Items.Insert(0, new ListItem("-请选择-", ""));
        }

        public void BindCity(string provinceID)
        {
            DataTable dt = memberService.GetDataByKey("T_City", "ProvinceID", provinceID);
            this.ddlCity.DataSource = dt;
            this.ddlCity.DataTextField = "CityName";
            this.ddlCity.DataValueField = "ID";
            this.ddlCity.DataBind();  
        }        
        /// <summary>
        /// 选择省事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pid = this.ddlProvice_District.SelectedValue;
            if (pid == "")
            {
                this.ddlCity.Items.Clear();
                this.ddlCity.Items.Insert(0, new ListItem("-请选择-", ""));
                return;
            }
            BindCity(pid);
        }
        
        #endregion
        /// <summary>
        /// 处理地区数据
        /// </summary>
        public void DealArea()
        {
            DataTable dt = sysService.GetData("T_Province");
            foreach (DataRow dr in dt.Rows)
            {
                dr["Spell"] = Chinese2Spell.ConvertWithSplitChar(dr["ProvinceName"].ToString().Replace("市","").Replace("自治区","").Replace("省",""), "");
                dr["FirstLetter"] = dr["Spell"].ToString().Substring(0, 1);
            }
            sysService.UpdateDataTable(dt);
        }
    }
}
