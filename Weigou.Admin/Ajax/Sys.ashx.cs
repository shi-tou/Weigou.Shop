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

namespace Weigou.Admin.Ajax
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
        /// 用户列表
        /// </summary>
        public void GetUserList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            if (UserInfo.UserName.ToLower() != "admin")
            {
                hs.Add("UserID", UserInfo.ID);
            }
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            sysService.GetUserList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteUser(HttpContext hc)
        {
            int id = GetRequest("ID", 0);
            int res = RT.FAILED;
            if (id == UserInfo.ID)
            {
                res = 2;//您正在登录此账户，不允许删除!
            }
            else
            {
                DataTable dt = sysService.GetDataByKey("T_User", "ID", id);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["UserName"].ToString().ToUpper() == "User")
                    {
                        res = 3;//User用户不能删除
                    }
                    else
                    {
                        dt.Rows[0]["Status"] = (int)EnumStatus.Delete;
                        if (sysService.UpdateDataTable(dt) > 0)
                        {
                            res = RT.SUCCESS;
                        }
                    }
                }
            }
            ResponseWrite(hc, res.ToString());
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        public void ChangeMasterPwd(HttpContext hc)
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
                    dt.Rows[0]["Password"] = DESEncrypt.Encrypt(oldPwd);
                    res = sysService.UpdateDataTable(dt);
                }
            }
            ResponseWrite(hc, res.ToString());
        }
        /// <summary>
        /// 权限列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetPrivilegeList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            //hs.Add("Status",(int)EnumStatus.Normal);
            DataTable dt = sysService.GetPrivilegeList(hs);
            strJson = Utils.CreateTreeJson(dt, "Code", "ParentCode");
            ResponseWrite(hc);
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
        /// 角色列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetRoleList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            sysService.GetRoleList(p, hs);
            ResponseWrite(hc, p);
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

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteButton(HttpContext hc)
        {
            int id = GetRequest("ID", 0);
            try
            {
                res = sysService.Delete("T_Button", "ID", id.ToString());
            }
            catch { }
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }
        #endregion

        #region 数据字典管理
        /// <summary>
        /// 获取字典列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetDictionaryList(HttpContext hc)
        {
            DataTable dt = sysService.GetDictionaryList();
            strJson = Utils.CreateTreeJson(dt, "ID", "ParentID");
            ResponseWrite(hc);
        }
        /// <summary>
        /// 删除字典数据
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteDictionary(HttpContext hc)
        {
            string id = GetRequest("ID", "");
            int res = 0;
            try
            {
                res = sysService.Delete("T_Dictionary", "ID", id);
            }
            catch
            {
                res = 0;
            }
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }
        #endregion

        #region 地区管理
        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetProvinceList(HttpContext hc)
        {
            string name = GetRequest("Name", "");
            Hashtable hs = new Hashtable();
            if (name != "")
            {
                hs.Add("Name", name);
            }
            Pager p = new Pager(PageSize, PageIndex, "ID asc");
            sysService.GetProvinceList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetCityList(HttpContext hc)
        {
            string name = GetRequest("Name", "");
            int pID = GetRequest("ProvinceID", 0);
            Hashtable hs = new Hashtable();
            if (name != "")
            {
                hs.Add("Name", name);
            }
            if (pID != 0)
            {
                hs.Add("ProvinceID", name);
            }
            Pager p = new Pager(PageSize, PageIndex, "a.ID asc");
            sysService.GetCityList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetDistrictList(HttpContext hc)
        {
            string name = GetRequest("Name", "");
            int pID = GetRequest("ProvinceID", 0);
            int cID = GetRequest("CityID", 0);
            Hashtable hs = new Hashtable();
            if (name != "")
            {
                hs.Add("Name", name);
            }
            if (pID != 0)
            {
                hs.Add("ProvinceID", pID);
            }
            if (cID != 0)
            {
                hs.Add("CityID", cID);
            }
            Pager p = new Pager(PageSize, PageIndex, "a.ID asc");
            sysService.GetDistrictList(p, hs);
            ResponseWrite(hc, p);
        }
        #endregion

        #region 日志管理
        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetLogList(HttpContext hc)
        {
            string module = GetRequest("Module", "");
            string operation = GetRequest("Operation", "");
            string content = GetRequest("Content", "");
            string minTime = GetRequest("MinTime", "");
            string maxTime = GetRequest("MaxTime", "");
            Hashtable hs = new Hashtable();
            if (module != "")
            {
                hs.Add("Module", module);
            }
            if (operation != "")
            {
                hs.Add("Operation", operation);
            }
            if (content != "")
            {
                hs.Add("Content", content);
            }
            if (minTime != "")
            {
                hs.Add("MinTime", minTime);
            }
            if (maxTime != "")
            {
                hs.Add("MaxTime", maxTime);
            }
            Pager p = new Pager(PageSize, PageIndex, "a.ID asc");
            sysService.GetLogList(p, hs);
            ResponseWrite(hc, p);
        }
        #endregion

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

        #region 物流管理
        /// <summary>
        /// 物流列表
        /// </summary>
        public void GetLogisticsList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            sysService.GetLogisticsList(p, hs);
            ResponseWrite(hc, p);
        }
        public void DeleteLogistics(HttpContext hc)
        {
            int res = 0;
            int ID = GetRequest("ID", 0);
            res = sysService.Delete("T_Logistics", "ID", ID);
            if (res > 0)
                sysService.SaveSysLog(ID.ToString(), EnumModule.SystemManage, EnumOperation.Delete, UserInfo.ID, "删除物流名称");
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }
        #endregion

        #region 栏目管理
        /// <summary>
        /// 栏目列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetClassList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            DataTable dt = sysService.GetClassList(hs);
            strJson = Utils.CreateTreeJson(dt, "Code", "ParentCode");
            ResponseWrite(hc);
        }
        /// <summary>
        /// 删除栏目
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteClass(HttpContext hc)
        {
            int id = GetRequest("ID", 0);
            try
            {
                res = sysService.Delete("T_Class", "ID", id);
            }
            catch { }
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }
        #endregion
        
        #region 平台物流管理
        public void GetPlatformLogisticsList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            hs["MerchantID"] = UserInfo.MerchantID;
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            //merchantService.GetMerchantLogisticsList(p, hs);
            merchantService.GetMerchantLogisticsList(p, hs);
            ResponseWrite(hc, p);
        }

        public void DeletePlatformLogistics(HttpContext hc)
        {
            int strPlatformLogisticsID = GetRequest("ID", 0);
            int res = 0;
            DataTable dt = merchantService.GetDataByKey("T_LogisticsTemplate", "ID", strPlatformLogisticsID);
            if (dt.Rows.Count > 0)
            {
                //省份运费信息删除
                DataTable dtMerchantProvince = merchantService.GetDataByKey("T_LogisticsProvince", "MerchantLogisticsID", strPlatformLogisticsID);
                if (dtMerchantProvince.Rows.Count > 0)
                {
                    merchantService.Delete("T_LogisticsProvince", " MerchantLogisticsID=" + strPlatformLogisticsID);
                }

                res = merchantService.Delete("T_LogisticsTemplate", " ID=" + strPlatformLogisticsID);
                if (res > 0)
                    merchantService.SaveSysLog(strPlatformLogisticsID.ToString(), EnumModule.MerchantManage, EnumOperation.Delete, UserInfo.ID, "删除商户运费模版");
            }
            ResponseWrite(hc, res > 0 ? RT.SUCCESS.ToString() : RT.FAILED.ToString());
        }

        public void SetDefaultLogistics(HttpContext hc)
        {
            int strPlatformLogisticsID = GetRequest("ID", 0);
            int res = sysService.SetDefaultLogistics("", strPlatformLogisticsID.ToString());

            ResponseWrite(hc, res > 0 ? RT.SUCCESS.ToString() : RT.FAILED.ToString());
        }
        #endregion

        #region 版本管理
        /// <summary>
        /// 获取版本列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetSysVersionList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            sysService.GetSysVersionList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 删除版本
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteSysVersion(HttpContext hc)
        {
            int id = GetRequest("ID", 0);
            try
            {
                res = sysService.Delete("T_AppVersion", "ID", id);
            }
            catch { }
            ResponseWrite(hc, res > 0 ? "0" : "1");
        }
        #endregion

        #region 备份管理
        /// <summary>
        /// 获取备份列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetBackupList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            sysService.GetBackupList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 手动备份数据库
        /// </summary>
        /// <param name="hc"></param>
        public void BackUpData(HttpContext hc)
        {
            int res = sysService.BackDatabase();
            ResponseWrite(hc, res);
        }
        #endregion
    }
}
