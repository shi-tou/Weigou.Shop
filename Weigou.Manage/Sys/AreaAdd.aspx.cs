using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Service;
using System.Data;

namespace Weigou.Manage.Sys
{
    public partial class AreaAdd : ManagePage
    {
        #region 参数
        /// <summary>
        /// 编号
        /// </summary>
        public int _ID
        {
            get
            {
                return GetRequest("ID", 0);
            }
        }
        /// <summary>
        /// 1-省  2-市  3-区
        /// </summary>
        public int Type
        {
            get
            {
                return GetRequest("Type", 0);
            }
        }
        #endregion

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAllInfo();
            }
        }

        #region 绑定
        /// <summary>
        /// 绑定对应信息
        /// </summary>
        public void BindAllInfo()
        {
            if (Type == 1)
            {
                BindInfoP();
                this.panel_P.Visible = true;
            }
            else if (Type == 2)
            {
                BindInfoC();
                this.panel_C.Visible = true;
            }
            else if (Type == 3)
            {
                BindInfoD();
                this.panel_D.Visible = true;
            }
            BindProvince();
            BindCity(this.ddlProvince_D.SelectedValue);
        }
        /// <summary>
        /// 绑定省下拉列表
        /// </summary>
        public void BindProvince()
        {
            DataTable dt = sysService.GetData("T_Province");
            this.ddlProvince_C.DataSource = dt;
            this.ddlProvince_C.DataTextField = "ProvinceName";
            this.ddlProvince_C.DataValueField = "ID";
            this.ddlProvince_C.DataBind();
            this.ddlProvince_D.DataSource = dt;
            this.ddlProvince_D.DataTextField = "ProvinceName";
            this.ddlProvince_D.DataValueField = "ID";
            this.ddlProvince_D.DataBind();
        }
        /// <summary>
        /// 绑定市下拉列表
        /// </summary>
        public void BindCity(string ProviceID)
        {
            DataTable dt = sysService.GetDataByKey("T_City", "ProvinceID",ProviceID);
            this.ddlCity.DataSource = dt;
            this.ddlCity.DataTextField = "CityName";
            this.ddlCity.DataValueField = "ID";
            this.ddlCity.DataBind();
        }
        /// <summary>
        /// 绑定省信息
        /// </summary>
        public void BindInfoP()
        {
            if (_ID == 0)
                return;
            DataTable dt = sysService.GetDataByKey("T_Province","ID",_ID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.txtProvinceName.Text = Convert.ToString(dr["ProvinceName"]);
                this.txtSpell_P.Text = Convert.ToString(dr["Spell"]);
                this.txtFirstLetter_P.Text = Convert.ToString(dr["FirstLetter"]);
                this.txtShortName.Text = Convert.ToString(dr["ShortName"]);
            }
        }
        /// <summary>
        /// 绑定城市信息
        /// </summary>
        public void BindInfoC()
        {
            if (_ID == 0)
                return;
            DataTable dt = sysService.GetDataByKey("T_City", "ID", _ID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.txtCityName.Text = Convert.ToString(dr["CityName"]);
                this.txtSpell_C.Text = Convert.ToString(dr["Spell"]);
                this.txtFirstLetter_C.Text = Convert.ToString(dr["FirstLetter"]);
                this.ddlProvince_C.SelectedValue = Convert.ToString(dr["ProvinceID"]);
            }
        }
        /// <summary>
        /// 绑定区域信息
        /// </summary>
        public void BindInfoD()
        {
            if (_ID == 0)
                return;
            DataTable dt = sysService.GetDataByKey("T_District", "ID", _ID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.txtDistrictName.Text = Convert.ToString(dr["DistrictName"]);
                DataTable dtC = sysService.GetDataByKey("T_City", "ID", dr["CityID"]);
                this.ddlProvince_D.SelectedValue = Convert.ToString(dtC.Rows[0]["ProvinceID"]);
                BindCity(this.ddlProvince_D.SelectedValue);
                this.ddlCity.SelectedValue = Convert.ToString(dr["CityID"]);
            }
        }

        /// <summary>
        /// 选择省事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProvinceD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pid = this.ddlProvince_D.SelectedValue;
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
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string btnName = btn.ID;
            if (btnName == "BtnSubmitP")
            {
                AddProvince();
            }
            else if (btnName == "BtnSubmitC")
            {
                AddCity();
            }
            else
            {
                AddDistrict();
            }
            RegistScript("CloseWin('操作成功！',parent.GetList);");
        }
        /// <summary>
        /// 添加省
        /// </summary>
        public void AddProvince()
        {
            DataTable dt = sysService.GetDataByKey("T_Province", "ID", _ID);
            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
            }
            else
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            dr["ProvinceName"] = this.txtProvinceName.Text;
            dr["Spell"] = this.txtSpell_P.Text;
            dr["FirstLetter"] = this.txtFirstLetter_P.Text;
            dr["ShortName"] = this.txtShortName.Text;
            int res = sysService.UpdateDataTable(dt);
            if (res > 0)
                InvokeScript("CloseWin('操作成功！',parent.GetList)");
            else
                InvokeScript("CloseWin('操作失败！')");
        }
        /// <summary>
        /// 添加市 
        /// </summary>
        public void AddCity()
        {
            DataTable dt = sysService.GetDataByKey("T_City", "ID", _ID);
            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
            }
            else
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            dr["CityName"] = this.txtProvinceName.Text;
            dr["Spell"] = this.txtSpell_P.Text;
            dr["FirstLetter"] = this.txtFirstLetter_P.Text;
            dr["ProvinceID"] = this.ddlProvince_C.SelectedValue;
            int res = sysService.UpdateDataTable(dt);
            if (res > 0)
                InvokeScript("CloseWin('操作成功！',parent.GetList)");
            else
                InvokeScript("CloseWin('操作失败！')");
        }
        /// <summary>
        /// 添加区域
        /// </summary>
        public void AddDistrict()
        {
            DataTable dt = sysService.GetDataByKey("T_District", "ID", _ID);
            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
            }
            else
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            dr["DistrictName"] = this.txtProvinceName.Text;
            dr["CityID"] = this.ddlCity.SelectedValue;
            int res = sysService.UpdateDataTable(dt);
            if (res > 0)
                InvokeScript("CloseWin('操作成功！',parent.GetList)");
            else
                InvokeScript("CloseWin('操作失败！')");
        }
    }
}
