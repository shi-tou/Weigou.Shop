using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Service;
using System.Data;
using Weigou.Model;
using Weigou.Model.Enum;
using System.Collections;

namespace Weigou.Manage.Content
{
    public partial class NewsTypeAdd : ManagePage
    {
        
        /// <summary>
        /// 修改的资源编码
        /// </summary>
        private int _ID
        {
            get { return GetRequest("ID", 0); }
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
                int index = 0;
                BindType(this.ddlParentType, contentService.GetData("T_NewsType"), 0, ref index);
                this.ddlParentType.Items.Insert(0, new ListItem("=根节点=",""));
                if (_ID != 0)
                {
                    BindInfo();
                }
            }
        }
        /// <summary>
        /// 绑定资源信息
        /// </summary>
        private void BindInfo()
        {
            DataTable dt=sysService.GetDataByKey("T_NewsType","ID",_ID);
            if (dt.Rows.Count == 0)
                return;
            DataRow dr = dt.Rows[0];
            this.ddlParentType.SelectedValue = Convert.ToString(dr["ParentID"]);
            this.txtName.Text = Convert.ToString(dr["Name"]);
        }
        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = contentService.GetDataByKey("T_NewsType", "ID", _ID);
            DataRow dr;
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
            dr["ParentID"] = this.ddlParentType.SelectedValue == "" ? "0" : this.ddlParentType.SelectedValue;
            dr["Name"] = this.txtName.Text;
            int res = contentService.UpdateDataTable(dt);
            if (res > 0)
                RegistScript("CloseWin('操作成功！',parent.GetList);");
            else
                RegistScript("alert('操作失败');");
        }
    }
}