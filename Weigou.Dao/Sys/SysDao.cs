using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Data.Common;
using Weigou.Common;
using System.Data;
using System.Collections;
using Weigou.Model.Enum;

namespace Weigou.Dao
{
    public class SysDao : BaseDao, ISysDao
    {

        #region  用户相关
        /// </summary>
        /// 用户列表
        /// </summary>
        public int GetUserList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as RoleName from T_User a
                            left join T_Role b on b.ID=a.RoleID
                            where a.Status <>" + (int)EnumStatus.Delete;
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("UserID"))
            {
                sql += " and a.CreateBy=@UserID";
                param.AddWithValue("UserID", hs["UserID"]);
            }
            if (hs.Contains("Name"))
            {
                sql += " and a.Name like @Name";
                param.AddWithValue("Name", "%" + hs["Name"] + "%");
            }
            if (hs.Contains("UserName"))
            {
                sql += " and a.UserName like @UserName";
                param.AddWithValue("UserName", "%" + hs["UserName"] + "%");
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }

        /// </summary>
        /// 权限列表
        /// </summary>
        public DataTable GetPrivilegeList(Hashtable hs)
        {
            string sql = "select * from T_Privilege where 1=1  ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("Code"))
            {
                sql += " and Code = @Code";
                param.AddWithValue("Code", hs["Code"]);
            }
            if (hs.Contains("PrivilegeType"))
            {
                sql += " and PrivilegeType=@PrivilegeType";
                param.AddWithValue("PrivilegeType", hs["PrivilegeType"]);
            }
            if (hs.Contains("ParentCode"))
            {
                sql += " and ParentCode=@ParentCode";
                param.AddWithValue("ParentCode", hs["ParentCode"]);
            }
            if (hs.Contains("Status"))
            {
                sql += " and Status=@Status ";
                param.AddWithValue("Status", hs["Status"]);
            }
            sql += " order by Sort";
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }

        /// <summary>
        /// 权限列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetPrivilegeList(Pager p, Hashtable hs)
        {
            string sql = @"select * from T_Privilege where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("Code"))
            {
                sql += " and Code like @Code";
                param.AddWithValue("Code", "%" + hs["Code"] + "%");
            }
            if (hs.Contains("ParentCode"))
            {
                sql += " and isnull(ParentCode ,'')=@ParentCode";
                param.AddWithValue("ParentCode", hs["ParentCode"]);
            }
            if (hs.Contains("Name"))
            {
                sql += " and Name like @Name";
                param.AddWithValue("Name", "%" + hs["Name"] + "%");
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }

        /// </summary>
        /// 角色列表
        /// </summary>
        public int GetRoleList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as CreateName from T_Role a
                           left join T_User b on b.ID=a.CreateBy 
                           where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("Name"))
            {
                sql += " and a.Name like @Name";
                param.AddWithValue("Name", hs["Name"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetUserPrivilege(int UserID)
        {
            string sql = @"select * from T_Privilege where Status=1 and ID in
                            (
                                select PrivilegeID from T_RolePrivilege  where RoleID in 
                                (select RoleID from T_User where ID=@UserID )
                            ) order by Sort";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("UserID", UserID);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
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
            string sql = "select * from T_Province where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("Name"))
            {
                sql += " and ProvinceName like @Name";
                param.AddWithValue("Name", hs["Name"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 获取省份列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetProvinceList(Hashtable hs)
        {
            string sql = "select * from T_Province where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("Name"))
            {
                sql += " and ProvinceName like @Name";
                param.AddWithValue("Name", hs["Name"]);
            }
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetCityList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.ProvinceName from T_City a 
                            left join T_Province b on b.ID=a.ProvinceID
                            where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("Name"))
            {
                sql += " and a.CityName like @Name";
                param.AddWithValue("Name", hs["Name"]);
            }
            if (hs.Contains("ProvinceID"))
            {
                sql += " and a.ProvinceID = @ProvinceID";
                param.AddWithValue("ProvinceID", hs["ProvinceID"]);
            }
            if (hs.Contains("IsHot"))
            {
                sql += " and a.IsHot = @IsHot";
                param.AddWithValue("IsHot", hs["IsHot"]);
            }
            if (hs.Contains("IsOpen"))
            {
                sql += " and a.IsOpen = @IsOpen";
                param.AddWithValue("IsOpen", hs["IsOpen"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 获取区域列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetDistrictList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.CityName,c.ProvinceName from T_District a
                            left join T_City b on b.ID=a.CityID
                            left join T_Province c on c.ID=b.ProvinceID
                            where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("Name"))
            {
                sql += " and a.DistrictName like @Name";
                param.AddWithValue("Name", hs["Name"]);
            }
            if (hs.Contains("ProvinceID"))
            {
                sql += " and b.ProvinceID = @ProvinceID";
                param.AddWithValue("ProvinceID", hs["ProvinceID"]);
            }
            if (hs.Contains("CityID"))
            {
                sql += " and a.CityID = @CityID";
                param.AddWithValue("CityID", hs["CityID"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
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
            string sql = @"select a.*,b.Name as CreateUser from T_SysLog a
	                        left join T_User b on b.ID=a.CreateBy
                            where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("Module"))
            {
                sql += " and Module=@Module ";
                param.AddWithValue("Module", hs["Module"]);
            }
            if (hs.Contains("Operation"))
            {
                sql += " and Operation=@Operation ";
                param.AddWithValue("Operation", hs["Operation"]);
            }
            if (hs.Contains("Content"))
            {
                sql += " and Content like @Content ";
                param.AddWithValue("Content", "%" + hs["Content"] + "%");
            }
            if (hs.Contains("MinTime"))
            {
                sql += " and datediff(day, @MinTime, a.CreateTime) >= 0";
                param.AddWithValue("MinTime", hs["MinTime"]);
            }
            if (hs.Contains("MaxTime"))
            {
                sql += " and datediff(day, a.CreateTime, @MaxTime) >= 0";
                param.AddWithValue("MaxTime", hs["MaxTime"]);
            }
            if (hs.Contains("MerchantID"))
            {
                sql += " and b.MerchantID=@MerchantID";
                param.AddWithValue("MerchantID", hs["MerchantID"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
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
            string sql = @"select a.*,b.Name as CreateName from T_Logistics a
	                        left join T_User b on b.ID=a.CreateBy
                            where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
        /// <summary>
        /// 获取物流列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetLogisticsList(Hashtable hs)
        {
            string sql = @"select a.*,b.Name as CreateName from T_Logistics a
	                        left join T_User b on b.ID=a.CreateBy
                            where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取默认物流运费模板
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        public DataTable GetMerchantLogistics(string MerchantID)
        {
            string sql = @"select *from T_LogisticsTemplate a
                            where IsDefault=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (!string.IsNullOrEmpty(MerchantID))
            {
                sql += " and a.MerchantID=@MerchantID";
                param.AddWithValue("MerchantID", MerchantID);
            }
            else
            {
                sql += " and isnull(a.MerchantID,'')=''";
            }
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        /// <summary>
        /// 设置默认物流运费模板
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <param name="LogisticsID"></param>
        /// <returns></returns>
        public int SetDefaultLogistics(string MerchantID, string LogisticsID)
        {
            string sql = @"update T_LogisticsTemplate set IsDefault=0
                           where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (!string.IsNullOrEmpty(MerchantID))
            {
                sql += " and MerchantID=@MerchantID;";
                param.AddWithValue("MerchantID", MerchantID);
            }
            else
            {
                sql += " and isnull(MerchantID,'')='';";
            }
            sql += "update T_LogisticsTemplate set IsDefault=1 where 1=1";
            if (!string.IsNullOrEmpty(LogisticsID))
            {
                sql += " and ID=@ID;";
                param.AddWithValue("ID", LogisticsID);
            }
            int result = AdoTemplate.ExecuteNonQuery(CommandType.Text, sql, param);
            return result;
        }
        #endregion

        #region 栏目管理
        /// <summary>
        /// 获取栏目列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetClassList(Hashtable hs)
        {
            string sql = "select * from T_Class where 1=1  ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("ParentCode"))
            {
                sql += " and ParentCode=@ParentCode";
                param.AddWithValue("ParentCode", hs["ParentCode"]);
            }
            if (hs.Contains("Status"))
            {
                sql += " and Status=@Status ";
                param.AddWithValue("Status", hs["Status"]);
            }
            if (hs.Contains("ClassPropertyID"))
            {
                sql += " and ClassPropertyID=@ClassPropertyID ";
                param.AddWithValue("ClassPropertyID", hs["ClassPropertyID"]);
            }
            sql += " order by Sort";
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
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
            string sql = @"select a.*,b.Name as CreateName from T_AppVersion a
	                        left join T_User b on b.ID=a.CreateBy
                            where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
        /// <summary>
        /// 获取APP版本更新信息
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        public DataTable GetAppVersionInfo(string Type)
        {
            string sql = "select top 1 VersionCode,VersionName,Content,ForceUpdate,AppUrl from T_AppVersion where 1=1  ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (!string.IsNullOrEmpty(Type))
            {
                sql += " and Type=@Type order by CreateTime desc";
                param.AddWithValue("Type", Type);
            }
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
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
            string sql = "select * from T_BackupData a where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();

            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }

        /// <summary>
        /// 手动备份
        /// </summary>
        /// <returns></returns>
        public int BackDatabase()
        {
            AdoTemplate.DataSetCreate(CommandType.StoredProcedure, "sp_BackData");
            return 0;
        }
        #endregion

        #region 广告图片
        /// <summary>
        /// 获取广告图
        /// </summary>
        /// <returns></returns>          
        public DataTable GetBannerImageList(Hashtable hs)
        {
            string sql = "select * from T_Banner where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("Type"))
            {
                sql += " and Type=@Type";
                param.AddWithValue("Type", hs["Type"]);
            }
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        #endregion

        #region 绑定银行卡

        /// <summary>
        /// 绑定银行卡或信用卡
        /// </summary>       
        /// <param name="hs"></param>
        /// <returns></returns>
        public int BindBankCard(Hashtable hs)
        {
            string sql = "insert into T_MemberBankCard(MemberID,BankType,BankName,BankID,ValidityPeriod,SafetyCode,Cardholder,IDcard,MobileNo) " +
                         "values(@MemberID,@BankType,@BankName,@BankID,@ValidityPeriod,@SafetyCode,@Cardholder,@IDcard,@MobileNo)";

            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("MemberID", hs["MemberID"]);
            param.AddWithValue("BankType", hs["BankType"]);
            param.AddWithValue("BankName", hs["BankName"]);
            param.AddWithValue("BankID", hs["BankID"]);
            param.AddWithValue("ValidityPeriod", hs["ValidityPeriod"]);
            param.AddWithValue("SafetyCode", hs["SafetyCode"]);
            param.AddWithValue("Cardholder", hs["Cardholder"]);
            param.AddWithValue("IDcard", hs["IDcard"]);
            param.AddWithValue("MobileNo", hs["MobileNo"]);
            return AdoTemplate.ExecuteNonQuery(CommandType.Text, sql, param);
        }

        #endregion
    }
}
