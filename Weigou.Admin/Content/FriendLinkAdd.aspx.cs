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
    public partial class FriendLinkAdd : AdminPage
    {
        //ID
        private int _ID
        {
            get { return GetRequest("ID", 0); }
        }
        
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            DataTable dt = sysService.GetDataByKey("T_FriendLink", "ID", _ID);
            DataRow dr = dt.Rows[0];
            this.ddlType.SelectedValue = dr["Type"].ToString();
            this.imgPicture.ImageUrl = Convert.ToString(dr["Picture"]);
            this.txtTitle.Text = Convert.ToString(dr["Title"]);
            this.txtUrl.Text = Convert.ToString(dr["Url"]);
        }
            
        /// <summary>
        /// 添加
        /// </summary>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = sysService.GetDataByKey("T_FriendLink", "ID", _ID);
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
            string pic = Upload(this.fuPicture, this.hfPicture.Value, "FriendLink");
            dr["Type"] = this.ddlType.SelectedValue;
            dr["Title"] = this.txtTitle.Text;
            dr["Url"] = this.txtUrl.Text;
            dr["Picture"] = pic;
            if (dt.Rows.Count == 0)
                dt.Rows.Add(dr);
            int res = contentService.UpdateDataTable(dt);
            if (res > 0)
                InvokeScript("CloseWin('添加成功！',parent.GetList)");
            else
                InvokeScript("CloseWin('添加失败！')");
        }
    }
}