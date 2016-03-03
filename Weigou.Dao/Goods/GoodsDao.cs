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
    public class GoodsDao : BaseDao, IGoodsDao
    {
        #region 商品类别
        /// <summary>
        /// 商品类别
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetGoodsType(Pager p, Hashtable hs)
        {
            string sql = @"select *from T_GoodsType a 
                            where a.Status<>" + ((int)EnumStatus.Delete).ToString();
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("ParentID"))
            {
                sql += " and a.ParentID = @ParentID";
                param.AddWithValue("ParentID", hs["ParentID"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 获取指定分类的所有子分类
        /// </summary>
        /// <param name="typeID"></param>
        /// <param name="self">是否包含自己</param>
        /// <returns></returns>
        public DataTable GetAllSubGoodsType(int typeID,bool self)
        {
            IDbParameters param = AdoTemplate.CreateDbParameters();
            string sql = @"with t as
                                    (
                                        select * from t_GoodsType where ID=@TypeID or ParentID=@TypeID
                                        union all
                                        select a.* from t_GoodsType a, t where a.parentid=t.id
                                    )
                         select * from t_GoodsType where id in(select id from t)  ";
            if (!self)
            {
                sql += " and ID<>@TypeID";
               
            }
            param.AddWithValue("TypeID", typeID);
            return AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
        }
        /// <summary>
        /// 获取指定类别的所有父分类
        /// </summary>
        /// <param name="typeID"></param>
        /// <param name="self">是否包含自己</param>
        /// <returns></returns>
        public DataTable GetAllParentGoodsType(int typeID, bool self)
        {
            IDbParameters param = AdoTemplate.CreateDbParameters();
            string sql = @"with t as
                            (
                                select * from t_GoodsType where ID=@TypeID
                                union all
                                select a.* from t_GoodsType a, t where a.id=t.ParentID
                            )
                            select * from t_GoodsType where id in(select id from t) ";
            if (!self)
            {
                sql += " and ID <> @TypeID";
            }

            param.AddWithValue("TypeID", typeID);
            return AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);

        }
        #endregion

        #region 商品图片
        /// <summary>
        /// 图片置顶
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="goodsID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public int SetPictureTop(int ID, string targetID, int Type)
        {
            string sql = @"update T_Picture set IsTop=0 where TargetID=@TargetID and Type=@Type;
                           update T_Picture set IsTop=1 where ID=@ID";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("ID", ID);
            param.AddWithValue("TargetID", targetID);
            param.AddWithValue("Type", Type);
            return AdoTemplate.ExecuteNonQuery(CommandType.Text, sql, param);
        }
        /// <summary>
        ///  图片列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetPictureList(string targetID, int type)
        {
            string sql = @"select *from T_Picture where Type=@Type and TargetID=@TargetID order by IsTop desc";

            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("Type", type);
            param.AddWithValue("TargetID", targetID);
            return AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
        }
        #endregion

        #region 商品列表/详情
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetGoodsList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as TypeName,c.Name as CreateName,d.Name as ApprovedName,e.SmallPicture from T_Goods a 
            	                        inner join T_GoodsType b on a.Type=b.ID and b.Status<>" + (int)EnumStatus.Delete + @"
            	                        left join T_User c on c.ID=a.CreateBy
            	                        left join T_User d on d.ID=a.ApprovedBy
                                        left join T_Picture e on e.TargetID=a.ID and e.Type=@PicType and e.IsTop=1
                                       where a.Status<>" + ((int)EnumStatus.Delete).ToString();
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("PicType", (int)EnumPicture.Goods);
            if (hs.Contains("Name"))
            {
                sql += " and a.Name like @Name";
                param.AddWithValue("Name", "%" + hs["Name"] + "%");
            }
            if (hs.Contains("Type"))
            {
                sql += " and a.Type=@Type";
                param.AddWithValue("Type", hs["Type"]);
            }
            if (hs.Contains("ParentID"))
            {
                sql += " and b.ParentID=@ParentID";
                param.AddWithValue("ParentID", hs["ParentID"]);
            }         
            if (hs.Contains("Status"))
            {
                sql += " and a.Status=@Status";
                param.AddWithValue("Status", hs["Status"]);
            }
            if (hs.Contains("ShelvesStatus"))
            {
                sql += " and a.ShelvesStatus=@ShelvesStatus";
                param.AddWithValue("ShelvesStatus", hs["ShelvesStatus"]);
            }

            if (hs.Contains("MinPrice"))
            {
                sql += " and a.SalePrice>=@MinPrice";
                param.AddWithValue("MinPrice", hs["MinPrice"]);
            }
            if (hs.Contains("MaxPrice"))
            {
                sql += " and a.SalePrice<=@MaxPrice";
                param.AddWithValue("MaxPrice", hs["MaxPrice"]);
            }

            sql = PagerSql(sql, p);

            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 商品详细
        /// </summary>
        /// <param name="goodsID">商品ID</param>
        /// <returns></returns>
        public DataTable GetGoodsDetails(int goodsID)
        {
            string sql = @"select a.*,b.Name as TypeName,c.SmallPicture  from T_Goods a 
            	            inner join T_GoodsType b on a.Type=b.ID
                            left join T_Picture c on c.TargetID=a.ID and c.Type=2 and c.IsTop=1
                            where a.ID=@GoodsID";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("GoodsID", goodsID);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        #endregion

        #region 商品库存、价格
        /// <summary>
        /// 根据销售属性及商品ID查询库存、价格信息
        /// </summary>
        /// <param name="strGoodsID">商品ID</param>
        /// <param name="strSaleProp">销售属性值</param>
        /// <returns></returns>
        public DataTable GetGoodsStockAndPrice(int goodsID, string saleProp)
        {
            string sql = @" select a.ID,ISNULL(b.Stock,a.Stock) as Stock,ISNULL(b.SalePrice,a.SalePrice) as SalePrice from T_Goods a
                            left join T_GoodsSaleProp b on b.ID=a.ID and b.SaleProp=@SaleProp
                            where a.ID=@GoodsID";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("GoodsID", goodsID);
            param.AddWithValue("SaleProp", saleProp);
            return AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
            
        }

        /// <summary>
        /// 根据销售属性及商品ID查询库存信息
        /// </summary>
        /// <param name="strGoodsID">商品ID</param>
        /// <param name="strSaleProp">销售属性值</param>
        /// <param name="strType">返回库存参数，1：属性价格 2：商品表价格</param>
        /// <returns></returns>
        public string GetGoodsPrice(int strGoodsID, string strSaleProp, out int strType)
        {
            string sql = @" select * from T_GoodsSaleProp where GoodsID=@GoodsID and SaleProp=@SaleProp";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("GoodsID", strGoodsID);
            param.AddWithValue("SaleProp", strSaleProp);
            DataTable dt = AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
            if (dt.Rows.Count > 0)
            {
                strType = 1;
                return dt.Rows[0]["SalePrice"].ToString();
            }
            else
            {
                sql = @" select * from T_Goods where ID=@GoodsID ";
                param = AdoTemplate.CreateDbParameters();
                param.AddWithValue("GoodsID", strGoodsID);
                dt = AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
                strType = 2;
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["SalePrice"].ToString();
                }
                else
                {
                    return "0";
                }
            }
        }
        #endregion

        #region 商品评论
        /// <summary>
        /// 商品评论列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetGoodsCommentList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,c.Name as MemberName,d.Name as GoodsName,e.Name as GoodsType,isnull(g.ReplyBy,0) as ReplyBys  from  T_Comment a 
                           inner join T_Member c on c.ID=a.MemberID
                           inner join T_Goods d on d.ID=a.TargetID
                           inner join T_GoodsType e on e.ID=d.Type
                           left join T_Comment g on g.ParentID=a.ID and isnull(g.ReplyBy,0)<>0
                           where 1=1 and a.ParentID=0 and a.Type=2";

            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("GoodsName"))
            {
                sql += " and d.Name like @Name";
                param.AddWithValue("Name", "%" + hs["GoodsName"] + "%");
            }
            if (hs.Contains("GoodsID"))
            {
                sql += " and a.TargetID=@TargetID";
                param.AddWithValue("TargetID", hs["GoodsID"]);
            }
            if (hs.Contains("GoodsTypeID"))
            {
                sql += " and e.ID=@GoodsTypeID";
                param.AddWithValue("GoodsTypeID", hs["GoodsTypeID"]);
            } 
            if (hs.ContainsKey("Reply"))
            {
                if (hs["Reply"].ToString() == "1")
                {
                    sql += " and g.ReplyBy<>0";
                }
                else
                {
                    sql += " and isnull(g.ReplyBy,0)=0";
                }
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        #endregion

        #region 商品品牌
        /// <summary>
        /// 商品品牌
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetGoodsBrandList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as CreateName from T_Brand a
                            left join T_User b on b.ID=a.CreateBy
                            left join T_GoodsTypeBrand c on c.BrandID=a.ID
                            where 1=1";

            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("Name"))
            {
                sql += " and a.Name like @Name";
                param.AddWithValue("Name", "%" + hs["Name"] + "%");
            }
            if (hs.Contains("Code"))
            {
                sql += " and a.Code=@Code";
                param.AddWithValue("Code", hs["Code"]);
            }
            if (hs.Contains("GoodsTypeID"))
            {
                sql += " and c.GoodsTypeID=@GoodsTypeID";
                param.AddWithValue("GoodsTypeID", hs["GoodsTypeID"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 获取指定商品类别的关联品牌
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetBrandByTypeID(string typeID)
        {
            string sql = @"select b.Name,a.BrandID,b.Spell from T_GoodsTypeBrand a
                            left join T_Brand b on b.ID=a.BrandID
                            where GoodsTypeID in(" + typeID + ") group by a.BrandID,b.Name,b.Spell";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        #endregion

        #region 购物车
        /// <summary>
        /// 匹配购物车信息
        /// </summary>
        /// <param name="strMemberID"></param>
        /// <param name="strGoodsID"></param>
        /// <param name="strSaleProp"></param>
        /// <returns></returns>
        public DataTable AddMatchingCart(int strMemberID, int strGoodsID, string strSaleProp)
        {
            string sql = @"select * from T_ShoppingCart where MemberID=@MemberID
                             and GoodsID=@GoodsID and SaleProp=@SaleProp";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("MemberID", strMemberID);
            param.AddWithValue("GoodsID", strGoodsID);
            param.AddWithValue("SaleProp", strSaleProp);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            DataTable table = ds.Tables[0];
            table.TableName = "T_ShoppingCart";
            return table;
        }

        /// <summary>
        /// 获取购物车列表
        /// </summary>
        /// <param name="strMemberID">会员ID</param>
        /// <returns></returns>
        public int GetShoppingCartList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,c.Name as GoodsName,isnull(d.SalePrice,c.SalePrice) as SalePrice,e.SmallPicture from T_ShoppingCart a
                           inner join T_Member b on a.MemberID=b.ID
                           inner join T_Goods c on a.GoodsID=c.ID
                           left join T_GoodsSaleProp d on d.SaleProp=a.SaleProp and d.GoodsID=a.GoodsID
                           left join T_Picture e on c.ID=e.TargetID and e.Type=" + ((int)EnumPicture.Goods).ToString() + @" and e.IsTop=1
                           where c.Status=" + ((int)EnumStatus.Normal).ToString();
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("MemberID"))
            {
                sql += " and b.ID=@MemberID";
                param.AddWithValue("MemberID", hs["MemberID"]);
            }
            if (hs.Contains("CartIDs"))//购物车ID集合
            {
                sql += " and a.ID in (" + hs["CartIDs"] + ")";
            }
            sql = PagerSql(sql, p);

            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        #endregion

        #region 物流、快递
        /// <summary>
        /// 获取平台物流模板
        /// </summary>
        /// <returns></returns>
        public DataTable GetMerchantLogistics(string MerchantID)
        {
            string sql = @"select *from T_LogisticsTemplate where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (!string.IsNullOrEmpty(MerchantID))
            {
                sql += " and MerchantID=@MerchantID";
                param.AddWithValue("MerchantID", MerchantID);
            }
            else
            {
                sql += " and isnull(MerchantID,'')=''";
            }
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据店铺ID和省份ID获取快递费用
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ProvinceID"></param>
        /// <returns></returns>
        public DataTable GetLogistics(string MerchantID, string ProvinceID)
        {
            string sql = @"select b.Price as LogisticsPrice from T_Logistics a 
                           left join T_LogisticsProvince b on a.ID=b.MerchantLogisticsID 
                           where a.IsDefault=1 ";
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
            if (!string.IsNullOrEmpty(ProvinceID))
            {
                sql += " and b.ProvinceID=@ProvinceID";
                param.AddWithValue("ProvinceID", ProvinceID);
            }
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据店铺ID获取默认快递费用
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ProvinceID"></param>
        /// <returns></returns>
        public DataTable GetDefaultLogistics(string MerchantID)
        {
            string sql = @"select a.DefaultPrice as LogisticsPrice,b.Code as LogisticsCode from T_LogisticsTemplate a
                           left join T_Logistics b on b.ID=a.LogisticsID  
                           where a.IsDefault=1";
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
        #endregion
 
        #region 商品属性
        /// <summary>
        /// 商品属性
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetGoodsAttributeList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as CreateName from T_GoodsAttribute a
                            left join T_User b on b.ID=a.CreateBy
                            where 1=1";

            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("Name"))
            {
                sql += " and a.Name like @Name";
                param.AddWithValue("Name", "%" + hs["GoodsName"] + "%");
            }
            if (hs.Contains("Type"))
            {
                sql += " and a.Type=@Type";
                param.AddWithValue("Type", hs["Type"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 获取商品属性值
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetAttributeValueList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name,c.Name as CreateName,a.ID as NewID from T_GoodsAttributeValue  a
                           left join T_GoodsAttribute b on a.AttributeID=b.ID 
                           left join T_User c on c.ID=a.CreateBy
                           where 1=1";

            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("AttributeID"))
            {
                sql += " and a.AttributeID=@AttributeID";
                param.AddWithValue("AttributeID", hs["AttributeID"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }       
        /// <summary>
        /// 根据类别获取商品的属性名
        /// </summary>
        /// <returns></returns>
        public DataTable GetGoodsAttribute(int goodsTypeID)
        {
            string sql = @"select b.* from T_GoodsTypeAttribute a
                            inner join T_GoodsAttribute b on a.AttributeID=b.ID
                            where a.GoodsTypeID=@GoodsTypeID
                            order by b.Sort,b.ID";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("GoodsTypeID", goodsTypeID);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取商品属性
        /// </summary>
        /// <param name="goodsID"></param>
        /// <returns></returns>
        public DataTable GetAttributeByGoodsID(int goodsID)
        {
            string sql = @"select a.ID,a.SalePrice,a.MarketPrice,a.Stock,b.SaleProp,b.Stock sQuantity,b.SalePrice sSalePrice,b.MarketPrice sMarketPrice from T_Goods a
                          left join T_GoodsSaleProp b on a.ID=b.GoodsID
                          where a.ID=@GoodsID";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("GoodsID", goodsID);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据属性值ID获取商品属性及属性值
        /// </summary>
        /// <param name="valueID"></param>
        /// <returns></returns>
        public DataTable GetAttributeValueByValueID(int valueID)
        {
            string sql = @"select a.*,b.Value,b.ID as ValueID,b.Sort as ValueSort from T_GoodsAttribute a
                            inner join T_GoodsAttributeValue b
                            on a.ID=b.AttributeID where b.ID=@ID ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("ID", valueID);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds.Tables[0];
        }
        #endregion


    }
}