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

namespace Weigou.Admin.Sms
{
    public partial class SmsSend : AdminPage
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
                
            }
        }
        /// <summary>
        /// 添加手机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.txtMobile.Text == "")
            {
                InvokeScript("AlertInfo('操作提示','手机号不能为空');");
                return;
            }
            string mobile = this.txtMobile.Text.Replace("\n", ",");
            string[] arr = mobile.Split(",".ToCharArray());
            List<string> list = new List<string>();

            foreach (string s in arr)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(s.Trim(), @"^[1]\d{10}$"))
                {
                    if (!list.Contains(s))
                    {
                        list.Add(s);
                        this.lbMobileNo.Items.Add(new ListItem(s.Trim(), s.Trim()));
                    }
                }
            }
            this.lbMobileNo.SelectedIndex = this.lbMobileNo.Items.Count - 1;
            this.txtMobile.Text = "";
            this.lblCount.Text = this.lbMobileNo.Items.Count.ToString();
        }
        /// <summary>
        /// 删除手机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDel_Click(object sender, EventArgs e)
        {
            this.lbMobileNo.Items.Remove(this.lbMobileNo.SelectedValue);
            this.lblCount.Text = this.lbMobileNo.Items.Count.ToString();
            this.lbMobileNo.SelectedIndex = this.lbMobileNo.Items.Count - 1;
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            //手机号
            List<string> list = GetMobileNo();
            //预定时间
            DateTime? presetTime = null;
            if (this.rblSendType.SelectedValue == "2")//定时
            {
                presetTime = Convert.ToDateTime(this.txtPresetTime.Text);
            }
            try
            {
                foreach (string m in list)
                {
                    smsService.SendSms(m, this.txtContent.Text,UserInfo.ID);
                }
                AlertInfo("发送成功！");
            }
            catch
            {
                AlertInfo("发送失败");
            }
        }
        /// <summary>
        /// 将手机号分几组
        /// </summary>
        /// <returns></returns>
        public List<string> GetMobileNo()
        {
            List<string> list = new List<string>();
            int total = this.lbMobileNo.Items.Count;
            int count = (total - 1) / 100 + 1;
            for (int i = 0; i < count; i++)
            {
                string mobileno = "";
                for (int j = i * 100; j < total && j < (i + 1) * 100; j++)
                {
                    if (mobileno != "")
                        mobileno += ";";
                    mobileno += this.lbMobileNo.Items[j].Value.Trim();
                }
                list.Add(mobileno);
            }
            return list;
        }        
    }
}
