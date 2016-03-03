using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.ComponentModel;
using System.Collections;
using Weigou.Api.Base;
using Weigou.Common;
using Weigou.Api.Model;
using Weigou.Config;

namespace Weigou.Api.SDK
{
    /// <summary>
    /// 系统模块Api
    /// </summary>
    public class SystemApi : BaseApi
    {
        #region 方法描述
        public const string GetProvinceList_Desc = "ID=省份ID&PagerSize=页大小&PagerIndex=页索引";
        public const string GetCityList_Desc = "ProvinceID=省份ID&PagerSize=页大小&PagerIndex=页索引&IsHot=是否热门城市（-1全部  0-否  1-是）&IsOpen=是否开放（-1全部  0-否  1-是）";
        public const string GetDistrictList_Desc = "CityID=城市ID&PagerSize=页大小&PagerIndex=页索引";
        public const string GetAppVersion_Desc = "Type=App类型(1-Android 2-IOS)";
        public const string _strBindBankCard_Desc = "MemberID=会员ID&BankType=绑定类型(1:银行卡，2:信用卡)&BankName=银行名称&BankID=卡号&ValidityPeriod=有效期&SafetyCode=安全码&Cardholder=持卡人&IDcard=身份证号&MobileNo=手机号";
        public const string GetBannerImage_Desc = "Type=广告图类型（1：首页banner，2：商城Banner，3：开机启动画面）";
        #endregion

        #region 地区相关
        /// <summary>
        /// 获取省份列表
        /// </summary>
        [Description(GetProvinceList_Desc)]
        public Result GetProvinceList(MyHashtable hs)
        {
            int provinceid = GetParam(hs, "ID", 0);
            Hashtable hss = new Hashtable();
            if (provinceid != 0)
            {
                hss.Add("ID", provinceid);
            }
            Pager p = GetPager(hs, 1, 999, "ID asc");
            result.status = sysService.GetProvinceList(p, hss);
            result.data = p.DataSource;
            result.total = p.ItemCount;
            return result;
        }
        /// <summary>
        /// 获取城市列表
        /// </summary>
        [Description(GetCityList_Desc)]
        public Result GetCityList(MyHashtable hs)
        {
            int pid = GetParam(hs, "ProvinceID", -1);
            int isHot = GetParam(hs, "IsHot", -1);
            int isOpen = GetParam(hs, "IsOpen", -1);
            Hashtable hss = new Hashtable();
            if (pid > -1)
            {
                hss.Add("ProvinceID", pid);
            }
            if (isHot > -1)
            {
                hss.Add("IsHot", isHot);
            }
            if (isOpen > -1)
            {
                hss.Add("IsOpen", isOpen);
            }
            Pager p = GetPager(hs, 1, 999, "a.ID asc ");
            result.status = sysService.GetCityList(p, hss);
            result.data = p.DataSource;
            result.total = p.ItemCount;
            return result;
        }
        /// <summary>
        /// 获取地区列表
        /// </summary>
        [Description(GetDistrictList_Desc)]
        public Result GetDistrictList(MyHashtable hs)
        {
            int cityid = GetParam(hs, "CityID", 0);
            Hashtable hss = new Hashtable();
            if (cityid != 0)
            {
                hss.Add("CityID", cityid);
            }
            Pager p = GetPager(hs, 1, 999, "a.ID asc ");
            result.status = sysService.GetDistrictList(p, hss);
            result.data = p.DataSource;
            result.total = p.ItemCount;
            return result;
        }
        #endregion

        #region 教育程度、职位、广告图分类
        /// <summary>
        /// 获取教育程度
        /// </summary>
        /// <returns></returns>
        public Result GetEducationList(MyHashtable hs)
        {
            DataTable dt = sysService.GetEducationList();
            result.data = dt;
            result.total = dt.Rows.Count;
            result.status = RT.SUCCESS;
            return result;
        }
        /// <summary>
        /// 获取职位信息
        /// </summary>
        /// <returns></returns>
        public Result GetOccupationList(MyHashtable hs)
        {
            DataTable dt = sysService.GetOccupationList();
            result.data = dt;
            result.total = dt.Rows.Count;
            result.status = RT.SUCCESS;
            return result;
        }
        /// <summary>
        /// 广告图分类
        /// </summary>
        /// <returns></returns>          
        public Result GetBannerTypeList(MyHashtable hs)
        {
            DataTable dt = sysService.GetBannerTypeList();
            result.data = dt;
            result.total = dt.Rows.Count;
            result.status = RT.SUCCESS;
            return result;
        } 

        #endregion

        #region App版本信息
        /// <summary>
        /// 获取APP版本信息
        /// </summary>
        [Description(GetAppVersion_Desc)]
        public Result GetAppVersion(MyHashtable hs)
        {
            string type = GetParam(hs, "Type", "1");
            DataTable dt = sysService.GetAppVersionInfo(type);
            result.status = RT.SUCCESS;
            result.data = dt;
            return result;
        }
        #endregion
        
        #region 广告图片
        /// <summary>
        /// 获取广告图
        /// </summary>
        /// <returns></returns>
        [Description(GetBannerImage_Desc)]
        public Result GetBannerImageList(MyHashtable hs)
        {
            int type = GetParam(hs, "Type", 1);
            Hashtable hsQuery = new Hashtable(); 
            hsQuery.Add("Type", type);
            DataTable dt = sysService.GetBannerImageList(hsQuery);

            foreach (DataRow dr in dt.Rows)
            {
                dr["Picture"] = GetServerPicture(dr["Picture"].ToString());
            }
            result.data = dt;
            result.total = dt.Rows.Count;
            result.status = RT.SUCCESS;
            return result;
        } 

        #endregion

        #region 绑定银行卡

        /// <summary>
        /// 绑定银行卡或信用卡
        /// </summary>
        [Description(_strBindBankCard_Desc)]
        public Result BindBankCard(MyHashtable hs)
        {
            int iMemberID = GetParam(hs, "MemberID", 0);
            int iBankType = GetParam(hs, "BankType", 0);
            string strName = GetParam(hs, "BankName", "");
            string strBankID = GetParam(hs, "BankID", "");
            string strValidityPeriod = GetParam(hs, "ValidityPeriod", "");
            string strSafetyCode = GetParam(hs, "SafetyCode", "");
            string strCardholder = GetParam(hs, "Cardholder", "");
            string strIDcard = GetParam(hs, "IDcard", "");
            string strMobileNo = GetParam(hs, "MobileNo", "");
            if (iMemberID == 0 || iBankType == 0 || string.IsNullOrEmpty(strName) || string.IsNullOrEmpty(strBankID))
            {
                result.status = RT.RESULT_ERROR_PARAMS;
                result.msg = "参数不全！";
                return result;
            }
            Hashtable hsAdd = new Hashtable();
            hsAdd.Add("MemberID", iMemberID);
            hsAdd.Add("BankType", iBankType);
            hsAdd.Add("BankName", strName);
            hsAdd.Add("BankID", strBankID);
            hsAdd.Add("ValidityPeriod", strValidityPeriod);
            hsAdd.Add("SafetyCode", strSafetyCode);
            hsAdd.Add("Cardholder", strCardholder);
            hsAdd.Add("IDcard", strIDcard);
            hsAdd.Add("MobileNo", strMobileNo);

            if (sysService.BindBankCard(hsAdd) > 0)
            {
                result.status = RT.SUCCESS;
                result.msg = "绑定成功！";
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "绑定失败！";
            }
            return result;
        }

        #endregion
    }
}