using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;
using Weigou.Model;
using Weigou.Service;
using Spring.Context;
using Spring.Context.Support;

namespace Weigou.Web.Member
{
    public partial class MyAddress : WebPage
    {
        /// <summary>
        /// 修改ID
        /// </summary>
        public int DeliverAddressID
        {
            get { return GetRequest("id",0); }
        }
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProvince();
                BindInfo();
            }
        }
        /// <summary>
        /// 绑定列表
        /// </summary>
        public void BindList()
        {
            Hashtable hs = new Hashtable();
            hs.Add("MemberID",LoginMemberInfo.ID);
            Pager p = new Pager(this.AspNetPager.PageSize, this.AspNetPager.CurrentPageIndex, "a.CreateTime");
            memberService.GetDeliverAddressList(p, hs);
            this.AspNetPager.RecordCount = p.ItemCount;//记录总数  
            this.repDeliver.DataSource = p.DataSource;
            this.repDeliver.DataBind();
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
        /// 绑定收货信息
        /// </summary>
        public void BindInfo()
        {
            if (DeliverAddressID == 0)
                return;
            this.btnSave.Text = "保存修改";
            DataTable dt = memberService.GetDataByKey("T_DeliverAddress","ID",DeliverAddressID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string pid = Convert.ToString(dr["ProvinceID"]);
                string cid = Convert.ToString(dr["CityID"]);
                string did = Convert.ToString(dr["DistrictID"]);
                if (pid != "")
                {
                    this.ddlProvince.SelectedValue = pid;
                    BindCity(pid);
                    this.ddlCity.SelectedValue = cid;
                    BindDistrict(cid);
                    this.ddlDistrict.SelectedValue = did;
                }
                this.txtAddress.Text = Convert.ToString(dr["Address"]);
                this.txtConsigneeName.Text = Convert.ToString(dr["ConsigneeName"]);
                this.txtConsigneeMobileNo.Text = Convert.ToString(dr["ConsigneeMobileNo"]);
                this.txtZipCode.Text = Convert.ToString(dr["ZipCode"]);
                this.cbIsDefault.Checked = Convert.ToBoolean(dr["IsDefault"]);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DeliverAddressInfo info = new DeliverAddressInfo();
            info.ID = DeliverAddressID;
            info.MemberID = LoginMemberInfo.ID;
            info.ProvinceID = Convert.ToInt32(this.ddlProvince.SelectedValue);
            info.CityID = Convert.ToInt32(this.ddlCity.SelectedValue);
            info.DistrictID = Convert.ToInt32(this.ddlDistrict.SelectedValue);
            info.Address = this.txtAddress.Text;
            info.ZipCode = this.txtZipCode.Text;
            info.ConsigneeName = this.txtConsigneeName.Text;
            info.ConsigneeMobileNo = this.txtConsigneeMobileNo.Text;
            info.IsDefault = this.cbIsDefault.Checked ? 1 : 0;
            if (memberService.SaveDeliverAddress(info) == RT.SUCCESS)
            {
                AlertAndJump("保存成功！", "MyAddress.aspx");
            }
            else
            {
                Alert("保存失败，请重试！");
            }
        }

        protected void repDeliver_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            if (e.CommandName == "DeleteGoodsFav")
            {
                if (memberService.Delete("T_Favorite", "ID", id) > 0)
                {
                    Alert("设置成功！");
                    BindList();
                }
                else
                {
                    Alert("设置失败！");
                }
            }
        }

        #region 省市区管理
        public void BindProvince()
        {
            DataTable dt = memberService.GetData("T_Province");
            this.ddlProvince.DataSource = dt;
            this.ddlProvince.DataTextField = "ProvinceName";
            this.ddlProvince.DataValueField = "ID";
            this.ddlProvince.DataBind();
            BindCity(this.ddlProvince.SelectedValue);
        }
        public void BindCity(string provinceID)
        {
            DataTable dt = memberService.GetDataByKey("T_City", "ProvinceID", provinceID);
            this.ddlCity.DataSource = dt;
            this.ddlCity.DataTextField = "CityName";
            this.ddlCity.DataValueField = "ID";
            this.ddlCity.DataBind();
            BindDistrict(this.ddlCity.SelectedValue);
        }
        public void BindDistrict(string cityID)
        {
            DataTable dt = memberService.GetDataByKey("T_District", "CityID", cityID);
            this.ddlDistrict.DataSource = dt;
            this.ddlDistrict.DataTextField = "DistrictName";
            this.ddlDistrict.DataValueField = "ID";
            this.ddlDistrict.DataBind();
        }
        /// <summary>
        /// 选择省事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pid = this.ddlProvince.SelectedValue;
            BindCity(pid);
        }
        /// <summary>
        /// 选择市事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDistrict(this.ddlCity.SelectedValue);
        }
        #endregion

        
    }
}