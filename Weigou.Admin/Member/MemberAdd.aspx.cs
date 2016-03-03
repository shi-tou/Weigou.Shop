using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Service;
using System.Data;
using Weigou.Model;
using Weigou.Common;
using Weigou.Model.Enum;

namespace Weigou.Admin.Member
{
    public partial class MemberAdd : AdminPage
    {
        /// <summary>
        /// 会员ID
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
                BindProvince();
                CarClassBind();
                if (_ID != 0)
                {
                    BindInfo();
                }
            }
        }

        /// <summary>
        /// 绑定修改的会员
        /// </summary>
        public void BindInfo()
        {
            DataTable dt = memberService.GetDataByKey("T_Member", "ID", _ID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr =dt.Rows[0];
                this.txtName.Text = dr["Name"].ToString();
                this.txtMobileNo.Text = dr["MobileNo"].ToString();
                this.txtEmail.Text = dr["Email"].ToString();
                this.ddlSex.SelectedValue = dr["Sex"].ToString();
                this.AvatarUrl.ImageUrl = dr["Photo"].ToString();
                this.hfAvatar.Value = dr["Photo"].ToString();
                this.txtBirthday.Text = Convert.ToString(dr["Birthday"]) == "" ? "" : (Convert.ToDateTime(dr["Birthday"]).ToString("yyyy-MM-dd"));
                this.txtSignature.Text = dr["Signature"].ToString();
                string pid = Convert.ToString(dr["ProvinceID"]);
                string cid = Convert.ToString(dr["CityID"]);
                string did = Convert.ToString(dr["DistrictID"]);
                this.ddlStatus.SelectedValue = dr["Status"].ToString();
                if (pid != "")
                {
                    this.ddlProvince.SelectedValue = pid;
                    BindCity(pid);
                    this.ddlCity.SelectedValue = cid;
                    BindDistrict(cid);
                    this.ddlDistrict.SelectedValue = did;
                }
                this.trPwd.Visible = false;
                
                //认证信息             
                labAuthStatus.Text = dr["TenantAuthStatus"].ToString() == "0" ? "未认证" : "已认证";
                ddlCarClass.SelectedValue = dr["CarClass"].ToString();
                txtIdCard.Text = dr["IdCard"].ToString();
                txtIssueDate.Text = Convert.ToString(dr["IssueDate"]) == "" ? "" : (Convert.ToDateTime(dr["IssueDate"]).ToString("yyyy-MM-dd"));
                txtLicenseNo.Text = dr["LicenseNo"].ToString(); 


            }
        }

        void CarClassBind()
        {
            DataTable dtCar = memberService.GetDataByKey("T_BaseData", "ParentID", BaseDataConst.Car_Class);
            this.ddlCarClass.DataSource = dtCar;
            this.ddlCarClass.DataTextField = "Name";
            this.ddlCarClass.DataValueField = "Value";
            this.ddlCarClass.DataBind();
            this.ddlCarClass.Items.Insert(0, new ListItem("-请选择-", ""));
        }
        /// <summary>
        /// 保存会员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            MemberInfo info = new MemberInfo();

            info.Password = this.txtPassword.Text;
            info.Status = (int)EnumMemStatus.Normal;
            info.CreateTime = DateTime.Now;
            info.ID = _ID;
            info.CreateBy = UserInfo.ID;
            info.Name = this.txtName.Text;
            if (this.txtBirthday.Text != "")
            {
                info.Birthday = this.txtBirthday.Text;
            }
            info.Sex = Convert.ToInt32(this.ddlSex.SelectedValue);
            info.MobileNo = this.txtMobileNo.Text;
            info.Email = this.txtEmail.Text;
            //info.CompanyName = this.txtCompanyName.Text;
            //info.Address = this.txtAddress.Text;
            info.Photo = Upload(this.fuAvatar, this.hfAvatar.Value, "Member");
            info.ProvinceID = this.ddlProvince.SelectedValue == "" ? 0 : Convert.ToInt32(this.ddlProvince.SelectedValue);
            info.CityID = this.ddlCity.SelectedValue == "" ? 0 : Convert.ToInt32(this.ddlCity.SelectedValue);
            info.DistrictID = this.ddlDistrict.SelectedValue == "" ? 0 : Convert.ToInt32(this.ddlDistrict.SelectedValue);
            info.Status = Convert.ToInt16(this.ddlStatus.SelectedValue);
            info.Source = (int)EnumMemberSource.User;
            //info.Remark = this.txtRemark.Text;
            info.IdCard = txtIdCard.Text.Trim();
           

            int res = 0;
            if (_ID == 0)
            {
                res = memberService.AddMember(info);
            }
            else
            {
                res = memberService.UpdateMember(info);
            }

            if (res == RT.SUCCESS)
            {
                RegistScript("CloseWin('保存成功！',parent.GetList);");
            }
            else if (res == RT.RESULT_EMAIL_EXIST)
            {
                RegistScript("ShowMsg('邮箱已被使用，请重新输入！');");
                this.txtEmail.Text = "";
            }
            else if (res == RT.RESULT_MOBILENO_EXIST)
            {
                RegistScript("ShowMsg('手机号被使用，请重新输入！');");
                this.txtMobileNo.Text = "";
            }
            else
            {
                RegistScript("ShowMsg('保存失败,请与管理员联系！');");
            }
        }
        #region 省市区管理
        public void  BindProvince()
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
    }
}
