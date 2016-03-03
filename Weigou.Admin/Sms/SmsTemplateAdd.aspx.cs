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
using Weigou.Common;
using System.Collections.Generic;
using Weigou.Model;
using Weigou.Model.Enum;

namespace Weigou.Admin.Sys
{
    public partial class SmsTemplateAdd : AdminPage
    {
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
        /// 绑定用户信息
        /// </summary>
        public void BindInfo()
        {
            if (_ID == 0)
                return;
            DataTable dt = sysService.GetDataByKey("T_SmsTemplate", "ID", _ID);
            DataRow dr = dt.Rows[0];
            this.txtCode.Text = Convert.ToString(dr["Code"]);
            this.txtCode.Enabled = false;
            this.txtContent.Text = Convert.ToString(dr["Content"]);
            this.txtDesc.Text = Convert.ToString(dr["Description"]);
            this.rblIsSystem.SelectedValue = Convert.ToBoolean(dr["IsSystem"]) ? "1" : "0";
        }
        /// <summary>
        /// 添加/修改
        /// </summary>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            int res = 0;
            DataTable dt = sysService.GetDataByKey("T_SmsTemplate", "ID", _ID);
            DataRow dr = null;
            if (dt.Rows.Count == 0)
            {
                DataTable dtD = sysService.GetDataByKey("T_SmsTemplate", "Code", this.txtCode.Text);
                if (dtD.Rows.Count > 0)
                {
                    RegistScript("AlertInfo('短信模板编号已存在');");
                    return;
                }
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr["CreateBy"] = UserInfo.ID;
                dr["CreateTime"] = DateTime.Now;
            }
            else
            {
                dr = dt.Rows[0];
            }
            dr["Code"] = this.txtCode.Text;
            dr["Content"] = this.txtContent.Text;
            dr["Description"] = this.txtDesc.Text;
            dr["IsSystem"] = this.rblIsSystem.SelectedValue == "1" ? true : false;
            if (_ID > 0)
            {
                if (sysService.UpdateDataTable(dt) > 0)
                    res = _ID;
            }
            else
            {
                res = sysService.Insert(dt);
            }
            if (res > 0)
            {
                InvokeScript("CloseWin('" + (_ID == 0 ? "添加" : "修改") + "成功！',parent.GetSmsTemplateList)");
                smsService.SaveSysLog(_ID.ToString(), EnumModule.SmsManage,
                    (_ID == 0 ? EnumOperation.Add : EnumOperation.Edit), UserInfo.ID,
                    string.Format("{0}编码为[{1}]的短信模板", (_ID == 0 ? "添加" : "修改"), this.txtCode.Text));
            }
            else
            {
                InvokeScript("CloseWin('添加失败！')");
            }
        }
    }
}
