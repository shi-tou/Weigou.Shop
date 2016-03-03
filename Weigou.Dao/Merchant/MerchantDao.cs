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
    public class MerchantDao : BaseDao, IMerchantDao
    {
        /// <summary>
        /// 商户列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetMerchantList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as CreateName,a.ID as MerchantID,a.Name as MerchantName from T_Merchant a 
	                       left join T_User b on b.ID=a.CreateBy
                           where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("Name"))
            {
                sql += " and a.Name like @Name";
                param.AddWithValue("Name", "%" + hs["Name"] + "%");
            }
            if (hs.Contains("MerchantID"))
            {
                sql += " and a.ID=@MerchantID";
                param.AddWithValue("MerchantID", hs["MerchantID"]);
            }
            if (hs.Contains("GoodsTypeID"))
            {
                sql += " and a.ID in (select MerchantID from T_MerchantGoodsType where GoodsTypeID=@GoodsTypeID)";
                param.AddWithValue("GoodsTypeID", hs["GoodsTypeID"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 图片置顶
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="goodsID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public int SetPictureTop(int ID, string goodsID, int Type)
        {
            string sql = @"update T_Picture set IsTop=0 where Code=@Code and Type=@Type;
                           update T_Picture set IsTop=1 where ID=@ID";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("ID", ID);
            param.AddWithValue("Code", goodsID);
            param.AddWithValue("Type", Type);
            return AdoTemplate.ExecuteNonQuery(CommandType.Text, sql, param);
        }
        /// <summary>
        ///  图片列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetPictureList(string code, int type)
        {
            string sql = @"select *from T_Picture where Type=@Type and Code=@Code order by IsTop desc";

            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("Type", type);
            param.AddWithValue("Code", code);
            return AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
        }
        /// <summary>
        /// 回访列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetMerchantVisitList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as CreateName from T_MerchantVisit a
                           join T_User b on a.CreateBy=b.ID 
                           where 1=1 ";

            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("Title"))
            {
                sql += " and a.Title like @Title";
                param.AddWithValue("Title", "%" + hs["Title"] + "%");
            }
            if (hs.Contains("Content"))
            {
                sql += " and a.Content like @Content";
                param.AddWithValue("Content", "%" + hs["Content"] + "%");
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 商户入驻列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetMerchantSettledList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as ApprovedName  from T_MerchantApply a 
	                        left join T_User b on b.ID=a.ApprovedBy
                            where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("CompanyName"))
            {
                sql += " and a.CompanyName like @CompanyName";
                param.AddWithValue("CompanyName", "%" + hs["CompanyName"] + "%");
            }
            if (hs.Contains("Status"))
            {
                sql += " and a.Status=@Status";
                param.AddWithValue("Status", hs["Status"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        #region 获取商户运费模版
        /// <summary>
        /// 获取商户运费模版
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetMerchantLogisticsList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as LogisticsName from T_LogisticsTemplate a
                           inner join T_Logistics b on a.LogisticsID=b.ID";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 获取运费相关省份
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetProvince(Hashtable hs)
        {
            string sql = @" select ProvinceName,a.MerchantLogisticsID,Price from T_LogisticsProvince a
                            inner join T_Province b on a.ProvinceID=b.ID where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("MerchantLogisticsID"))
            {
                sql += " and a.MerchantLogisticsID=@MerchantLogisticsID";
                param.AddWithValue("MerchantLogisticsID", hs["MerchantLogisticsID"]);
            }
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        #endregion

        /// <summary>
        /// 获取商户经营类目集合
        /// </summary>
        /// <returns></returns>
        public DataTable GetMerchantGoodsTypeList(string MerchantID)
        {
            string sql = @"select a.*,b.Name as GoodsTypeName from T_MerchantGoodsType a 
                         inner join T_GoodsType b on b.ID=a.GoodsTypeID
                         where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (!string.IsNullOrEmpty(MerchantID))
            {
                sql += " and a.MerchantID=@MerchantID";
                param.AddWithValue("MerchantID", MerchantID);
            }
            return AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
        }

        /// <summary>
        /// 获取专场封面图片
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetSpecialPicture(Hashtable hs)
        {
            string sql = @"select a.Picture from T_Banner a
                           where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("MerchantID"))
            {
                sql += " and a.MerchantID=@MerchantID";
                param.AddWithValue("MerchantID", hs["MerchantID"]);
            }
            if (hs.Contains("Type"))
            {
                sql += " and a.Type=@Type";
                param.AddWithValue("Type", hs["Type"]);
            }
            return AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
        }
        /// <summary>
        /// 获取专场的店铺列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetSpecialMerchantList(Pager p, Hashtable hs)
        {
            string sql = @"select a.MerchantID,c.Picture from T_ActivityGoods a
                           left join T_Merchant b on b.ID=a.MerchantID
                           left join T_Banner c on c.MerchantID=b.ID
                           where 1=1 and c.Type=3 group by a.MerchantID,c.Picture";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }

        /// <summary>
        /// 获取店铺设计列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetShopSignList(Hashtable hs)
        {
            string sql = "select * from T_Region where IsHome=0  ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("ParentID"))
            {
                sql += " and ParentID=@ParentID";
                param.AddWithValue("ParentID", hs["ParentID"]);
            }
            if (hs.Contains("MerchantID"))
            {
                sql += " and MerchantID=@MerchantID";
                param.AddWithValue("MerchantID", hs["MerchantID"]);
            }
            sql += " order by Sort";
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询公益基金和娱乐积分的返送比例
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        public DataTable GetScale(string MerchantID)
        {
            string sql = "select *from T_MerchantParam a where 1=1 ";
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
        /// 检查该店铺是否为..国际
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        public int CheckIsInternational(string MerchantID)
        {
            string sql = "select *from T_Merchant where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (!string.IsNullOrEmpty(MerchantID))
            {
                sql += " and ID=@MerchantID";
                param.AddWithValue("MerchantID", MerchantID);
            }
            DataTable dt = AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
            if (dt.Rows.Count == 0)
                return 0;
            return Convert.ToInt32(dt.Rows[0]["IsInternational"]);
        }
        /// <summary>
        /// 检查该店铺是否支持试衣间功能
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        public DataTable CheckMerchantSupportFitting(string MerchantID)
        {
            string sql = @"select *from T_GoodsType a
                           left join T_MerchantGoodsType b on a.ID=b.GoodsTypeID
                           where a.IsSupportFitting=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (!string.IsNullOrEmpty(MerchantID))
            {
                sql += " and b.MerchantID=@MerchantID";
                param.AddWithValue("MerchantID", MerchantID);
            }
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取店铺评分
        /// </summary>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        public decimal GetMerchantStar(string MerchantID)
        {
            string sql = @"select cast(isnull(AVG((SurviceStar+LogisticsStar+DescriptionStar)/3),5) as decimal(18,1) ) as MerchantStar  from T_Comment a 
                           inner join T_Goods b on b.ID=a.GoodsID
                           where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (!string.IsNullOrEmpty(MerchantID))
            {
                sql += " and b.MerchantID=@MerchantID";
                param.AddWithValue("MerchantID", MerchantID);
            }
            DataTable dt = AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
            if (dt.Rows.Count == 0)
                return 0;
            return Convert.ToDecimal(dt.Rows[0]["MerchantStar"]);
        }
    }
}
