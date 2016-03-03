using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Weigou.Service;

namespace Weigou.Admin.Sys
{
    public partial class DictionaryAdd :AdminPage
    {
        /// <summary>
        /// 字典编码
        /// </summary>
        public int _ID
        {
            get
            {
                return GetRequest("ID",0);
            }
        }
        /// <summary>
        /// 添加子节点的父编码
        /// </summary>
        private int ParentID
        {
            get { return GetRequest("ParentID", 0); }
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
                if (_ID != 0)
                {
                    BindInfo();
                }
                if (ParentID != 0)
                {
                    this.txtParentName.Text = GetParentName(ParentID.ToString());
                }
                this.txtParentName.ReadOnly = true;
                this.txtParentName.Attributes.Add("style", "background:#eee;");
            }
        }
        /// <summary>
        /// 绑定字典信息
        /// </summary>
        private void BindInfo()
        {
            DataTable dt = sysService.GetDataByKey("T_Dictionary", "ID", _ID);
            if (dt.Rows.Count == 0)
                return;
            DataRow dr = dt.Rows[0];
            this.txtParentName.Text = GetParentName(Convert.ToString(dr["ParentID"]));
            this.txtName.Text = Convert.ToString(dr["Name"]);
            this.txtValue.Text = Convert.ToString(dr["Value"]);
            this.txtCode.Text = Convert.ToString(dr["Code"]);
            this.txtRemark.Text = Convert.ToString(dr["Remark"]);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = sysService.GetDataByKey("T_Dictionary", "ID", _ID);
            DataRow dr;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
            }
            else
            {
                dr = dt.Rows[0];
            }
            dr["Name"] = this.txtName.Text;
            dr["Value"] = this.txtValue.Text;
            dr["Code"] = this.txtCode.Text;
            dr["ParentID"] = ParentID;
            dr["Remark"] = this.txtRemark.Text;
            if (dt.Rows.Count == 0)
                dt.Rows.Add(dr);
            int res = sysService.UpdateDataTable(dt);
            if (res > 0)
                RegistScript("CloseWin('操作成功！',parent.GetList);");
            else
                RegistScript("ShowMsg('操作失败');");
        }
        /// <summary>
        /// 获取上一级名称
        /// </summary>
        /// <returns></returns>
        public string GetParentName(string pid)
        {
            DataTable dt=sysService.GetDataByKey("T_Dictionary", "ID",pid );
            if(dt.Rows.Count>0)
            {
                return dt.Rows[0]["Name"].ToString();
            }
            return "";
        }
    }
}
