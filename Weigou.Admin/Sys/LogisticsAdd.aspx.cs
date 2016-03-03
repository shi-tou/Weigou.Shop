using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Admin.Sys
{
    public partial class LogisticsAdd : AdminPage
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
            DataTable dt = sysService.GetDataByKey("T_Logistics", "ID", _ID);
            DataRow dr = dt.Rows[0];
            this.txtName.Text = Convert.ToString(dr["Name"]);
            this.txtRemark.Text = Convert.ToString(dr["Remark"]);
        }

        /// <summary>
        /// 添加
        /// </summary>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = sysService.GetDataByKey("T_Logistics", "ID", _ID);
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
            dr["Name"] = this.txtName.Text;
            dr["Remark"] = this.txtRemark.Text;
            if (dt.Rows.Count == 0)
                dt.Rows.Add(dr);
            int res = sysService.UpdateDataTable(dt);
            if (res > 0)
                InvokeScript("CloseWin('操作成功！',parent.GetList)");
            else
                InvokeScript("CloseWin('操作失败！')");
        }
    }
}