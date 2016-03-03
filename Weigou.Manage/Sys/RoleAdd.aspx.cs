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

namespace Weigou.Manage.Sys
{
    public partial class RoleAdd : ManagePage
    {
        //ID
        private int _ID
        {
            get { return GetRequest("ID", 0); }
        }
        public string PrivilegeJson = "[]";
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInfo();
                GetPrivilegeJson();
            }
        }
        /// <summary>
        /// 绑定
        /// </summary>
        public void BindInfo()
        {
            if (_ID == 0)
                return;
            DataTable dt = sysService.GetDataByKey("T_Role", "ID", _ID);
            DataRow dr = dt.Rows[0];
            this.txtRoleName.Text = Convert.ToString(dr["Name"]);
            this.txtRoleInfo.Text = Convert.ToString(dr["Remark"]);
        }
        /// <summary>
        /// 获取Ztree需要的json字符
        /// </summary>
        public void GetPrivilegeJson()
        {
            Hashtable hs = new Hashtable();
            hs.Add("Status", EnumStatus.Normal);
            DataTable dt = sysService.GetPrivilegeList(hs);
            DataTable dtRolePrivilege = sysService.GetDataByKey("T_RolePrivilege", "RoleID", _ID);
            List<string> list = GetPrivilege(dtRolePrivilege);

            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                if (sb.Length != 0)
                    sb.Append(",");
                sb.Append("{");
                if (_ID != 0)
                {
                    sb.AppendFormat("\"id\":\"{0}\",\"pId\":\"{1}\",\"name\":\"{2}\",\"value\":\"{3}\",\"checked\":{4}", dr["Code"], dr["ParentCode"], dr["Name"], dr["ID"], list == null ? "false" : list.Contains(dr["ID"].ToString()).ToString().ToLower());
                }
                else
                {
                    sb.AppendFormat("\"id\":\"{0}\",\"pId\":\"{1}\",\"name\":\"{2}\",\"value\":\"{3}\"", dr["Code"], dr["ParentCode"], dr["Name"], dr["ID"]);
                }
                sb.Append("}");
            }
            PrivilegeJson = "[" + sb.ToString() + "]";
        }
            
        /// <summary>
        /// 添加
        /// </summary>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = sysService.GetDataByKey("T_Role", "ID", _ID);
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
            dr["Name"] = this.txtRoleName.Text;
            dr["Remark"] = this.txtRoleInfo.Text;
            if (dt.Rows.Count == 0)
                dt.Rows.Add(dr);
            int res = sysService.SaveRole(dt, GetPrivilege(), UserInfo.ID);
            if (res > 0)
                ShowMsg("/Sys/RoleList.aspx", "添加成功！");
            else
                ShowMsg("/Sys/RoleList.aspx", "添加失败！");
        }
        /// <summary>
        /// 添加时获取权限
        /// </summary>
        /// <returns></returns>
        public List<string> GetPrivilege()
        {
            List<string> list = new List<string>();
            string action = this.hfPrivilege.Value;
            foreach (string s in action.Split(",".ToCharArray()))
            {
                if (!string.IsNullOrEmpty(s))
                    list.Add(s.Trim());
            }
            return list;
        }
        /// <summary>
        /// 绑定量获取权限
        /// </summary>
        /// <param name="dt">组对应的权限</param>
        /// <returns></returns>
        public List<string> GetPrivilege(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return null;
            List<string> list = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                string s = Convert.ToString(dr["PrivilegeID"]);
                if (!string.IsNullOrEmpty(s))
                    list.Add(s.Trim());
            }
            return list;
        }

    }
}