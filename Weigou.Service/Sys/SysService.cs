using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Weigou.Model;
using Weigou.Dao;
using Weigou.Common;
using System.Collections;
using Spring.Transaction.Interceptor;
using System.Web;
using System.IO;
using Weigou.Model.Enum;

namespace Weigou.Service
{
    public class SysService : BaseService, ISysService
    {
        private ISysDao sysDao;
        public ISysDao SysDao
        {
            set
            {
                sysDao = value;
            }
        }

        #region 用户相关
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>0-成功 1-用户名不存在 2-禁用 3-密码不正确</returns>
        public int UserLogin(string username, string password)
        {
            Hashtable hs = new Hashtable();
            hs.Add("UserName", username);
            DataTable dt = GetDataByWhere("T_User", hs);
            if (dt.Rows.Count == 0)
                return 1;
            DataRow dr = dt.Rows[0];
            if (Convert.ToInt16(dr["Status"]) == (int)EnumStatus.Disabled)
                return 2;
            if (Convert.ToString(dr["Password"]) != DESEncrypt.Encrypt(password))
                return 3;
            SaveSysLog(dt.Rows[0]["ID"].ToString(), EnumModule.SystemManage, EnumOperation.Login, Convert.ToInt32(dt.Rows[0]["ID"]), "用户登录");
            return 0;
        }
        /// 用户列表
        /// </summary>
        public int GetUserList(Pager p, Hashtable hs)
        {
            return sysDao.GetUserList(p, hs);
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        [Transaction]
        public int SaveUserInfo(DataTable dtUser, List<string> group, int adminID)
        {
            int res = 0;
            int aid = 0;
            bool isNew = (dtUser.Rows[0].RowState == DataRowState.Added);
            if (isNew)
            {
                aid = Convert.ToInt32(dtUser.Rows[0]["ID"].ToString());
                res = UpdateDataTable(dtUser);
            }
            else
            {
                aid = Insert(dtUser);
            }
            //删除角色权限
            Delete("T_UserRole", "UserID=" + aid);
            //添加用户关系组
            DataTable dt = GetDataByKey("T_UserRole", "ID", 0);
            group.ForEach(g =>
            {
                DataRow dr = dt.NewRow();
                dr["RoleID"] = g;
                dr["UserID"] = aid;
                dt.Rows.Add(dr);
            });
            UpdateDataTable(dt);
            //记录日志
            if (res > 0)
            {
                SaveSysLog(aid.ToString(), EnumModule.SystemManage, isNew ? EnumOperation.Add : EnumOperation.Edit, adminID, "添加用户");
            }
            return res;
        }
        /// <summary>
        /// 获取资源目列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetPrivilegeList(Hashtable hs)
        {
            DataTable dt = sysDao.GetPrivilegeList(hs);
            dt.Columns.Add("StatusName", typeof(string));
            foreach (DataRow dr in dt.Rows)
            {
                dr["StatusName"] = EnumHelper.GetStatus(dr["Status"]);
            }
            return dt;
        }
        /// 权限列表
        /// </summary>
        public int GetPrivilegeList(Pager p, Hashtable hs)
        {
            return sysDao.GetPrivilegeList(p, hs);
        }
        /// <summary>
        /// 更新资源信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [Transaction]
        public int UpdatePrivilege(DataTable dt, int adminID)
        {
            int res = 0;
            int aid = 0;
            bool isNew = (dt.Rows[0].RowState == DataRowState.Added);
            if (isNew)
            {
                aid = sysDao.Insert(dt);
                res = aid;
            }
            else
            {
                aid = Convert.ToInt32(dt.Rows[0]["ID"]);
                res = sysDao.UpdateDataTable(dt);
            }
            if (res > 0)
            {
                SaveSysLog(aid.ToString(), EnumModule.SystemManage, isNew ? EnumOperation.Add : EnumOperation.Edit, adminID, "编辑菜单");
            }
            return res;
        }
        /// 角色列表
        /// </summary>
        public int GetRoleList(Pager p, Hashtable hs)
        {
            return sysDao.GetRoleList(p, hs);
        }
        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="dtRole">角色</param>
        /// <param name="dtRolePrivilege">权限</param>
        /// <returns></returns>
        [Transaction]
        public int SaveRole(DataTable dtRole, List<string> rolePrivilege, int userID)
        {
            int res = 0;
            int roleID = 0;
            bool isNew = (dtRole.Rows[0].RowState == DataRowState.Added);
            if (isNew)
            {
                roleID = Insert(dtRole);
                res = roleID;
            }
            else
            {
                roleID = Convert.ToInt32(dtRole.Rows[0]["ID"].ToString());
                res = UpdateDataTable(dtRole);
            }
            if (res > 0)
            {
                //删除角色权限
                Delete("T_RolePrivilege", "RoleID", roleID);
                //添加角色权限
                DataTable dt = GetDataByKey("T_RolePrivilege", "ID", 0);
                rolePrivilege.ForEach(g =>
                {
                    DataRow dr = dt.NewRow();
                    dr["PrivilegeID"] = g;
                    dr["RoleID"] = roleID;
                    dt.Rows.Add(dr);
                });
                UpdateDataTable(dt);
                SaveSysLog(roleID.ToString(), EnumModule.SystemManage, isNew ? EnumOperation.Add : EnumOperation.Edit, userID, "添加角色权限");
            }
            return res;
        }
        /// <summary>
        /// 获取用户权限
        /// </summary>
        public DataTable GetUserPrivilege(int UserID)
        {
            return sysDao.GetUserPrivilege(UserID);
        }
        #endregion

        #region 地区管理
        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetProvinceList(Pager p, Hashtable hs)
        {
            return sysDao.GetProvinceList(p, hs);
        }
        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetProvinceList(Hashtable hs)
        {
            return sysDao.GetProvinceList(hs);
        }
        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetCityList(Pager p, Hashtable hs)
        {
            return sysDao.GetCityList(p, hs);
        }
        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetDistrictList(Pager p, Hashtable hs)
        {
            return sysDao.GetDistrictList(p, hs);
        }
        #endregion

        #region 教育程度、职位、广告图分类
        /// <summary>
        /// 获取教育程度
        /// </summary>
        /// <returns></returns>
        public DataTable GetEducationList()
        {
            DataTable dt = GetDataByKey("T_BaseData", "ParentID", BaseDataConst.Education);
            return dt;
        }
        /// <summary>
        /// 职位
        /// </summary>
        /// <returns></returns>
        public DataTable GetOccupationList()
        {
            DataTable dt = GetDataByKey("T_BaseData", "ParentID", BaseDataConst.Occupation);
            return dt;
        }
        /// <summary>
        /// 广告图分类
        /// </summary>
        /// <returns></returns>
        public DataTable GetBannerTypeList()
        {
            DataTable dt = GetDataByKey("T_BaseData", "ParentID", BaseDataConst.BannerType);
            return dt;
        }

        #endregion

        #region 日志管理
        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetLogList(Pager p, Hashtable hs)
        {
            return sysDao.GetLogList(p, hs);
        }
        #endregion

        #region 物流管理
        /// <summary>
        /// 获取物流列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetLogisticsList(Pager p, Hashtable hs)
        {
            return sysDao.GetLogisticsList(p, hs);
        }
        /// <summary>
        /// 获取物流列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetLogisticsList(Hashtable hs)
        {
            return sysDao.GetLogisticsList(hs);
        }
        /// <summary>
        /// 获取默认物流运费模板
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        public DataTable GetMerchantLogistics(string MerchantID)
        {
            return sysDao.GetMerchantLogistics(MerchantID);
        }
        /// <summary>
        /// 设置默认物流运费模板
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <param name="LogisticsID"></param>
        /// <returns></returns>
        public int SetDefaultLogistics(string MerchantID, string LogisticsID)
        {
            return sysDao.SetDefaultLogistics(MerchantID, LogisticsID);
        }
        #endregion

        #region 栏目管理
        /// <summary>
        /// 获取栏目列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetClassList(Hashtable hs)
        {
            DataTable dt = sysDao.GetClassList(hs);
            dt.Columns.Add("StatusName", typeof(string));
            foreach (DataRow dr in dt.Rows)
            {
                dr["StatusName"] = EnumHelper.GetStatus(dr["Status"]);
            }
            return dt;
        }
        #endregion
         
        #region App版本管理
        /// <summary>
        /// 获取版本列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetSysVersionList(Pager p, Hashtable hs)
        {
            return sysDao.GetSysVersionList(p, hs);
        }
        /// <summary>
        /// 获取APP版本更新信息
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public DataTable GetAppVersionInfo(string Type)
        {
            return sysDao.GetAppVersionInfo(Type);
        }
        #endregion

        #region 备份管理
        /// <summary>
        /// 获取备份列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetBackupList(Pager p, Hashtable hs)
        {
            return sysDao.GetBackupList(p, hs);
        }
         /// <summary>
        /// 手动备份
        /// </summary>
        /// <returns></returns>
        public int BackDatabase()
        {
            return sysDao.BackDatabase();
        }
        #endregion

        #region 广告图片
        /// <summary>
        /// 获取广告图
        /// </summary>
        /// <returns></returns>          
        public DataTable GetBannerImageList(Hashtable hs)
        {
            return sysDao.GetBannerImageList(hs);
        }
        #endregion
        
        #region 绑定银行卡

        /// <summary>
        /// 绑定银行卡或信用卡
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int BindBankCard(Hashtable hs)
        {
            return sysDao.BindBankCard(hs);
        }

        #endregion
    }
}

