using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Weigou.Service;
using Weigou.Common;
using System.Text;
using System.Web.SessionState;
using Weigou.Model;
using Weigou.Model.Enum;

namespace Weigou.Manage.Ajax
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Sys : BaseAjax, IHttpHandler, IRequiresSessionState
    {
        #region 用户相关
        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="hc"></param>
        public void CheckUserName(HttpContext hc)
        {
            string userName = GetRequest("UserName", "");
            DataTable dt = sysService.GetDataByKey("T_User", "UserName", userName);
            ResponseWrite(hc, dt.Rows.Count > 0 ? "0" : "1");
        }
       
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteUser(HttpContext hc)
        {
            int id = GetRequest("ID", 0);
            int res = RT.FAILED;//-1
            if (id == UserInfo.ID)
            {
                res = 1;//您正在登录此账户，不允许删除!
            }
            else
            {
                DataTable dt = sysService.GetDataByKey("T_User", "ID", id);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["UserName"].ToString().ToUpper() == "User")
                    {
                        res = 2;//User用户不能删除
                    }
                    else
                    {
                        dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                        if (sysService.UpdateDataTable(dt) > 0)
                        {
                            res = RT.SUCCESS;//0-成功
                        }
                    }
                }
            }
            ResponseWrite(hc, res.ToString());
        }
        /// <summary>
        /// 修改当前管理员密码
        /// </summary>
        public void ChangeUserPwd(HttpContext hc)
        {
            string oldPwd = GetRequest("OldPwd", "");
            string newPwd = GetRequest("NewPwd", "");
            DataTable dt = sysService.GetDataByKey("T_User", "ID", UserInfo != null ? UserInfo.ID : 0);
            int res = 0;
            if (dt.Rows.Count > 0)
            {
                if (DESEncrypt.Encrypt(oldPwd) != dt.Rows[0]["Password"].ToString())
                {
                    res = 2;//旧密码不正确
                }
                else
                {
                    dt.Rows[0]["Password"] = DESEncrypt.Encrypt(newPwd);
                    res = sysService.UpdateDataTable(dt);
                }
            }
            ResponseWrite(hc, res > 0 ? RT.SUCCESS : RT.FAILED);
        }
        /// <summary>
        /// 修改当前管理员密码
        /// </summary>
        public void ModifyUserPwdByID(HttpContext hc)
        {
            int userID = GetRequest("ID", 0);
            string newPwd = GetRequest("NewPwd", "");
            DataTable dt = sysService.GetDataByKey("T_User", "ID", userID);
            int res =0;
            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["Password"] = DESEncrypt.Encrypt(newPwd);
                res = sysService.UpdateDataTable(dt);
            }
            ResponseWrite(hc, res > 0 ? RT.SUCCESS : RT.FAILED);
        }
       
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="hc"></param>
        public void DeletePrivilege(HttpContext hc)
        {
            int id = GetRequest("ID", 0);
            try
            {
                res = sysService.Delete("T_Privilege", "ID", id);
            }
            catch { }
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }
        
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteRole(HttpContext hc)
        {
            int id = GetRequest("ID", 0);
            try
            {
                res = sysService.Delete("T_Role", "ID", id.ToString());
            }
            catch { }
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }
        #endregion

        #region 生产商品编号
        /// <summary>
        /// 生成编码
        /// </summary>
        /// <param name="hc"></param>
        public void GenerateCode(HttpContext hc)
        {
            int type = GetRequest("Type", 1);
            string str = "";
            DateTime t = DateTime.Now;
            if (type == 1)
            {
                str = t.ToString("yyMMmmss") + t.Millisecond.ToString();
                ResponseWrite(hc, str);
            }
            else
            {
                str = t.ToString("yyddmmss") + t.Millisecond.ToString();
                ResponseWrite(hc, str);
            }
        }

        #endregion
    }
}
