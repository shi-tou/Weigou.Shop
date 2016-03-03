using Weigou.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Admin.Content
{
    public partial class NewsAdd : AdminPage
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
            int index = 0;
            BindType(this.ddlType, contentService.GetData("T_NewsType"), 0, ref index);
            if (_ID == 0)
                return;
            DataTable dt2 = sysService.GetDataByKey("T_News", "ID", _ID);
            DataRow dr = dt2.Rows[0];
            this.ddlType.SelectedValue = dr["Type"].ToString();
            this.txtTitle.Text = Convert.ToString(dr["Title"]);
            this.txtContent.Text = Convert.ToString(dr["Description"]);
        }

        /// <summary>
        /// 添加
        /// </summary>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = sysService.GetDataByKey("T_News", "ID", _ID);
            DataRow dr = null;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr["CreateBy"] = UserInfo.ID;
                dr["CreateTime"] = DateTime.Now;
            }
            else
            {
                dr = dt.Rows[0];
            }
            dr["Type"] = this.ddlType.SelectedValue;
            dr["Title"] = this.txtTitle.Text;
            dr["Description"] = this.txtContent.Text;
            int res = contentService.UpdateDataTable(dt);
            if (res > 0)
                InvokeScript("CloseWin('添加成功！',parent.GetList)");
            else
                InvokeScript("CloseWin('添加失败！')");
        }
    }
}