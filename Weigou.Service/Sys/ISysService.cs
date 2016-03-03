using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Common;
using System.Collections;
using System.Data;

namespace Weigou.Service
{
    public interface ISysService : IBaseService
    {
        #region 用户相关
        /// <summary>
        /// 用户登录(type:1-系统用户 2-商家用户)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        int UserLogin(string username, string password);
        /// <summary>
        /// 用户列表(type:1-系统用户 2-商家用户)
        /// </summary>
        int GetUserList(Pager p, Hashtable hs);
        /// <summary>
        /// 添加用户
        /// </summary>
        int SaveUserInfo(DataTable dt, List<string> group, int adminID);
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        DataTable GetPrivilegeList(Hashtable hs);
        /// 权限列表
        /// </summary>
        int GetPrivilegeList(Pager p, Hashtable hs);
        /// <summary>
        /// 更新权限信息
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        int UpdatePrivilege(DataTable dt, int adminID);
        /// </summary>
        /// 角色列表
        /// </summary>
        int GetRoleList(Pager p, Hashtable hs);
        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="dtRole">角色</param>
        /// <param name="dtRolePrivilege">权限</param>
        /// <returns></returns>
        int SaveRole(DataTable dtRole, List<string> groupPrivilege, int adminID);
        /// <summary>
        /// 获取用户权限
        /// </summary>
        DataTable GetUserPrivilege(int UserID);
        #endregion

        /// <summary>
        /// 获取数据字典
        /// </summary>
        /// <returns></returns>
        DataTable GetDictionaryList();

        #region 地区管理
        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetProvinceList(Pager p, Hashtable hs);
        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        DataTable GetProvinceList(Hashtable hs);
        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetCityList(Pager p, Hashtable hs);
        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetDistrictList(Pager p, Hashtable hs);
        #endregion

        #region 教育程度、职位、广告图类型
        /// <summary>
        /// 获取教育程度
        /// </summary>
        /// <returns></returns>
        DataTable GetEducationList();
        /// <summary>
        /// 职位
        /// </summary>
        /// <returns></returns>
        DataTable GetOccupationList();
        /// <summary>
        /// 广告图分类
        /// </summary>
        /// <returns></returns>
        DataTable GetBannerTypeList();
        #endregion

        #region 日志管理
        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetLogList(Pager p, Hashtable hs);
        #endregion

        #region 物流管理
        /// <summary>
        /// 获取物流列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetLogisticsList(Pager p, Hashtable hs);
        /// <summary>
        /// 获取物流列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        DataTable GetLogisticsList(Hashtable hs);
        /// <summary>
        /// 获取默认物流运费模板
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        DataTable GetMerchantLogistics(string MerchantID);
        /// <summary>
        /// 设置默认物流运费模板
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <param name="LogisticsID"></param>
        /// <returns></returns>
        int SetDefaultLogistics(string MerchantID, string LogisticsID);
        #endregion

        #region 栏目管理
        /// <summary>
        /// 获取栏目列表
        /// </summary>
        /// <returns></returns>
        DataTable GetClassList(Hashtable hs);
        #endregion
         
        #region App版本管理
        /// <summary>
        /// 获取版本列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetSysVersionList(Pager p, Hashtable hs);
        /// <summary>
        /// 获取APP版本更新信息
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        DataTable GetAppVersionInfo(string Type);
        #endregion

        #region 备份管理
        /// <summary>
        /// 获取备份列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetBackupList(Pager p, Hashtable hs);
        /// <summary>
        /// 手动备份
        /// </summary>
        /// <returns></returns>
        int BackDatabase();
        #endregion

        #region 广告图片
        /// <summary>
        /// 获取广告图
        /// </summary>
        /// <returns></returns>          
        DataTable GetBannerImageList(Hashtable hs);
        #endregion

        #region 绑定银行卡

        /// <summary>
        /// 绑定银行卡或信用卡
        /// </summary>       
        /// <param name="hs"></param>
        /// <returns></returns>
        int BindBankCard(Hashtable hs);

        #endregion
    }
}
