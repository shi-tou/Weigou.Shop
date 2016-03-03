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

namespace Weigou.Manage.Weixin
{
    public partial class WeixinKeywordAdd : ManagePage
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
            DataTable dt = sysService.GetDataByKey("T_WeixinKeyword", "ID", _ID);
            DataRow dr = dt.Rows[0];
            this.txtName.Text = Convert.ToString(dr["Name"]);
        }

        /// <summary>
        /// 添加
        /// </summary>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = sysService.GetDataByKey("T_WeixinKeyword", "ID", _ID);
            DataRow dr = null;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dr["CreateBy"] = UserInfo.ID;
                dr["CreateTime"] = DateTime.Now;
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.Rows[0];
            }
            dr["Name"] = this.txtName.Text;
            int res = weixinService.UpdateDataTable(dt);
            if (res > 0)
                ShowMsg("/Weixin/WeixinKeywordList.aspx", "添加成功！");
            else
                ShowMsg("/Weixin/WeixinKeywordList.aspx", "添加失败！");
        }
    }
}