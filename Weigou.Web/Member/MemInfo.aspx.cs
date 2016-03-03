using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Common;
using Weigou.Model;

namespace Weigou.Web.Member
{
    public partial class MemInfo : WebPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProvince();
                BindInfo();
            }
        }
        /// <summary>
        /// 绑定修改的会员
        /// </summary>
        public void BindInfo()
        {
            DataTable dt = memberService.GetDataByKey("T_Member", "ID", LoginMemberInfo.ID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                this.txtName.Text = dr["Name"].ToString();
                this.ddlSex.SelectedValue = dr["Sex"].ToString();
                this.txtBirthday.Text = Convert.ToString(dr["Birthday"]) == "" ? "" : (Convert.ToDateTime(dr["Birthday"]).ToString("MM-dd"));
                this.txtCompanyName.Text = dr["CompanyName"].ToString();
                this.txtAddress.Text = dr["Address"].ToString();
                this.AvatarUrl.ImageUrl = dr["Photo"].ToString();
                this.hfAvatar.Value = dr["Photo"].ToString();
                this.txtRemark.Text = dr["Remark"].ToString();
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
            this.ddlProvince.Items.Insert(0, new ListItem("-请选择-", ""));
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
            if (pid == "")
            {
                this.ddlCity.Items.Clear();
                this.ddlCity.Items.Insert(0, new ListItem("-请选择-", ""));
                this.ddlDistrict.Items.Clear();
                this.ddlDistrict.Items.Insert(0, new ListItem("-请选择-", ""));
                return;
            }
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

        protected void btnUpdateMem_Click(object sender, EventArgs e)
        {

            MemberInfo info = new MemberInfo();
            info.ID = LoginMemberInfo.ID;
            info.Name = this.txtName.Text;
            if (this.txtBirthday.Text != "")
            {
                info.Birthday = this.txtBirthday.Text;
            }
            info.Sex = Convert.ToInt16(this.ddlSex.SelectedValue);
            //info.CompanyName = this.txtCompanyName.Text;
            //info.Address = this.txtAddress.Text;
            info.Photo = Upload(this.fuAvatar, this.hfAvatar.Value, "Member");
            info.ProvinceID = this.ddlProvince.SelectedValue == "" ? 0 : Convert.ToInt32(this.ddlProvince.SelectedValue);
            info.CityID = this.ddlCity.SelectedValue == "" ? 0 : Convert.ToInt32(this.ddlCity.SelectedValue);
            info.DistrictID = this.ddlDistrict.SelectedValue == "" ? 0 : Convert.ToInt32(this.ddlDistrict.SelectedValue);
            //info.Remark = this.txtRemark.Text;
            int res = memberService.UpdateMember(info);
            if (res == RT.SUCCESS)
            {
                AlertAndJump("保存成功！", "/Member/MemInfo.aspx");
            }
            else if (res == RT.FAILED)
            {
                Alert("保存失败，请重试！");
            }
        }
    }
}