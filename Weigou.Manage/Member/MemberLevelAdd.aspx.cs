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

namespace Weigou.Manage.Member
{
    public partial class MemberLevelAdd : ManagePage
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
            DataTable dt = memberService.GetDataByKey("T_MemberLevel", "ID", _ID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr =dt.Rows[0];
                this.txtName.Text = dr["Name"].ToString();
                this.txtScore.Text = dr["NeedScore"].ToString();
               
                 
            }
        }
        /// <summary>
        /// 保存会员等级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = memberService.GetDataByKey("T_MemberLevel", "ID", _ID);
             DataRow dr;
             if (dt.Rows.Count > 0)
             {
                 dr = dt.Rows[0];
             }
             else
             {
                 dr = dt.NewRow();
                 dt.Rows.Add(dr);
                 dr["CreateTime"] = DateTime.Now;
                 dr["CreateBy"] = UserInfo.ID;
             }

             dr["Name"] = this.txtName.Text.Trim();
             dr["NeedScore"] = this.txtScore.Text;
              

            int res = memberService.UpdateDataTable(dt);

            if (res >0 )
            { 
                sysService.SaveSysLog(_ID.ToString(), EnumModule.MemberManage, _ID != 0 ? EnumOperation.Edit : EnumOperation.Add, UserInfo.ID, _ID != 0 ? "修改了会员级别[" + this.txtName.Text.Trim() + "]" : "添加了会员级别为[" + this.txtName.Text.Trim() + "]");
               ShowMsg("/Member/MemberLevelList.aspx", "保存成功!");
            }
            else
            {
                ShowMsg("/Member/MemberLevelList.aspx", "保存失败,请与管理员联系!");
            }
        }
        
    }
}
