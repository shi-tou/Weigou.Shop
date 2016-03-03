using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Service;
using System.Data;
using System.Collections;
using Weigou.Common;
using System.Text;
using Weigou.Model;
using Weigou.Model.Enum;

namespace Weigou.Admin.Content
{
    public partial class BannerAdd : AdminPage
    {
        /// <summary>
        /// Banner图ID
        /// </summary>
        public int _ID
        {
            get
            {
                return GetRequest("ID", 0);
            }
        }
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = goodsService.GetDataByKey("T_BaseData", "ParentID", BaseDataConst.BannerType);
                DataBind(dt, "DropDownList", ddlType, "Name", "Value");
                BindInfo();
            }
        }
        /// <summary>
        /// 绑定
        /// </summary>
        public void BindInfo()
        {
            if (_ID == 0)
                return;
            DataTable dt = contentService.GetDataByKey("T_Banner", "ID", _ID);
            DataRow dr = dt.Rows[0];
            this.ddlType.SelectedValue = Convert.ToString(dr["Type"]);
            this.txtTitle.Text = Convert.ToString(dr["Title"]);
            this.txtSimpleDesc.Text = Convert.ToString(dr["SimpleDesc"]);
            this.hfPicture.Value = Convert.ToString(dr["Picture"]);
            this.imgPicture.ImageUrl = Convert.ToString(dr["Picture"]);
            this.txtUrl.Text = Convert.ToString(dr["Url"]);
        }
      
        /// <summary>
        /// 添加
        /// </summary>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = contentService.GetDataByKey("T_Banner", "ID", _ID);
            DataRow dr = null;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["CreateBy"] = UserInfo.ID;
                dr["CreateTime"] = DateTime.Now;
            }
            else
            {
                dr = dt.Rows[0];
            }
            dr["Type"] = this.ddlType.SelectedValue;
            dr["Title"] = this.txtTitle.Text;
            dr["SimpleDesc"] = this.txtSimpleDesc.Text;
            dr["Url"] = this.txtUrl.Text;
            dr["Picture"] = Upload(this.fuPicture, this.hfPicture.Value, "Banner");
            if (dt.Rows.Count == 0)
                dt.Rows.Add(dr);
            int res = contentService.UpdateDataTable(dt);
            if (res > 0)
            {
                contentService.SaveSysLog(_ID.ToString(),
                    EnumModule.ContentManage, _ID == 0 ? EnumOperation.Add : EnumOperation.Edit, UserInfo.ID,
                    (_ID == 0 ? "添加" : "修改") + "了Banner图信息");
                InvokeScript("CloseWin('保存成功！',parent.GetList)");
            }
            else
                InvokeScript("CloseWin('保存失败！')");
        }
    }
}