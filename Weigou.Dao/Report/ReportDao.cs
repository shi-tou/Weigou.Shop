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
    public class ReportDao : BaseDao, IReportDao
    {
        #region 会员统计报表
        /// <summary>
        /// 会员统计报表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetMemberReport(Pager p, Hashtable hs)
        {
            string sql = "";
            IDbParameters param;
            GetMemberReportWhere(hs, out sql, out param);
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 会员统计报表(导出xls)
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetMemberReport(Hashtable hs)
        {
            string sql = "";
            IDbParameters param;
            GetMemberReportWhere(hs, out sql, out param);
            return AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param).Tables[0];
        }
        /// <summary>
        /// 会员统查询条件
        /// </summary>
        /// <param name="hs"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        public void GetMemberReportWhere(Hashtable hs, out string sql, out IDbParameters param)
        {
            sql = @"select a.*,b.Name as CreateUser,d.ProvinceName,e.CityName,f.DistrictName,isnull(g.Balance,0) as MemberScore from T_Member as a
                            left join T_User as b on b.ID=a.CreateBy
                            left join T_Province d on d.ID=a.ProvinceID
                            left join T_City e on e.ID=a.CityID
                            left join T_District f on f.ID=a.DistrictID
                            left join (
                                            select top 1 MemberID,Balance from T_Score where Type=1 order by CreateTime desc
                                        ) g on g.MemberID=a.ID
                            where 1=1";
            param = AdoTemplate.CreateDbParameters();

            if (hs.ContainsKey("Name"))
            {
                sql += "  and a.Name like @Name";
                param.AddWithValue("Name", "%" + hs["Name"].ToString() + "%");
            }
            if (hs.ContainsKey("MobileNo"))
            {
                sql += "  and a.MobileNo like @MobileNo";
                param.AddWithValue("MobileNo", "%" + hs["MobileNo"].ToString() + "%");
            }
            if (hs.ContainsKey("Sex"))
            {
                sql += "  and a.Sex=@Sex";
                param.AddWithValue("Sex", hs["Sex"].ToString());
            }
            if (hs.ContainsKey("Status"))
            {
                sql += "  and a.Status=@Status";
                param.AddWithValue("Status", hs["Status"].ToString());
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
        }
        #endregion

        #region 积分统计报表
        /// <summary>
        /// 积分统计报表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetScoreReport(Pager p, Hashtable hs)
        {
            string sql = "";
            IDbParameters param;
            GetScoreReportWhere(hs, out sql, out param);
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 积分统计报表(导出xls)
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetScoreReport(Hashtable hs)
        {
            string sql = "";
            IDbParameters param;
            GetScoreReportWhere(hs, out sql, out param);
            return AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param).Tables[0];
        }
        /// <summary>
        /// 积分统计查询条件
        /// </summary>
        /// <param name="hs"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        public void GetScoreReportWhere(Hashtable hs, out string sql, out IDbParameters param)
        {
            sql = @"select a.*,b.Name as MemberName,d.Name as CreateName from T_Score a 
                    left join T_Member b on b.ID=a.MemberID
                    left join T_User d on d.ID=a.CreateBy
                    where 1=1";
            param = AdoTemplate.CreateDbParameters();

            if (hs.ContainsKey("MemberName"))
            {
                sql += "  and b.Name like @MemberName";
                param.AddWithValue("MemberName", "%" + hs["MemberName"].ToString() + "%");
            }           
            if (hs.ContainsKey("Type"))
            {
                sql += "  and a.Type=@Type";
                param.AddWithValue("Type", hs["Type"]);
            }
            if (hs.ContainsKey("ScoreType"))
            {
                sql += "  and a.ScoreType=@ScoreType";
                param.AddWithValue("ScoreType", hs["ScoreType"]);
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
        }
        #endregion

        #region 商品统计报表
        /// <summary>
        /// 商品统计报表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetGoodsSaleReport(Pager p, Hashtable hs)
        {
            string sql = "";
            IDbParameters param;
            GetGoodsReportWhere(hs, out sql, out param);
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 商品统计报表(导出xls)
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetGoodsReport(Hashtable hs, string strOrderBy)
        {
            string sql = "";
            IDbParameters param;
            GetGoodsReportWhere(hs, out sql, out param);
            sql += strOrderBy;
            return AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql,param).Tables[0];
        }
        /// <summary>
        /// 商品统查询条件
        /// </summary>
        /// <param name="hs"></param>=
        /// <param name="sql"></param>
        /// <param name="param"></param>
        public void GetGoodsReportWhere(Hashtable hs, out string sql, out IDbParameters param)
        {
            sql = @"select a.*,b.Name as TypeName,c.Name as CreateName,d.Name as ApprovedName,isnull (f.Star,5) as GoodsStar,
                  (select COUNT(g.ID) from T_MallOrder f left join T_MallOrderDetail g on 
                  f.OrderNo=g.OrderNo  where f.OrderStatus=" + (int)EnumMallOrderStatus.Received + @" and g.GoodsID=a.ID) as Sales  
                  from T_Goods a 
                  inner join T_GoodsType b on a.Type=b.ID and b.Status<>" + (int)EnumStatus.Delete + @"
            	  left join T_User c on c.ID=a.CreateBy
            	  left join T_User d on d.ID=a.ApprovedBy
                  left  join (select TargetID, AVG(Star) as Star from 
                  dbo.T_Comment where [Type]=2 group by TargetID) f on f.TargetID=a.ID  where a.Status<>" + ((int)EnumStatus.Delete).ToString();
            param = AdoTemplate.CreateDbParameters();
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
        }
        #endregion

        #region 订单统计报表
        /// <summary>
        /// 订单统计报表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetOrderReport(Pager p, Hashtable hs)
        {
            string sql = "";
            IDbParameters param;
            GetOrderReportWhere(hs, out sql, out param);
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 订单统计报表(导出xls)
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetOrderReport(Hashtable hs)
        {
            string sql = "";
            IDbParameters param;
            GetOrderReportWhereXLS(hs, out sql, out param);
            return AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param).Tables[0];
        }
        /// <summary>
        /// 订单统计查询条件
        /// </summary>
        /// <param name="hs"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        public void GetOrderReportWhereXLS(Hashtable hs, out string sql, out IDbParameters param)
        {
            sql = @"select m.GoodsName,m.Count,d.Name as MemberName,d.MobileNo as MemberMobile,a.TotalMoney as GoodsPrice,a.*,c.PayType as PayMentType,e.Name as LogisticsName
	                       from T_MallOrder a
                           left join T_Member d on d.ID=a.MemberID
                           left join T_Payment c on a.OrderNo=c.OrderNos
                           left join T_Logistics e on e.Code=a.LogisticsCode
                           left join T_MallOrderDetail m on m.OrderNo=a.OrderNo
                           where 1=1  ";
            param = AdoTemplate.CreateDbParameters();

            //if (hs.Contains("IsView"))
            //{
            //    sql += " and a.QueryStatus=0";
            //}
            if (hs.Contains("MemberID"))
            {
                sql += " and a.MemberID=@MemberID";
                param.AddWithValue("MemberID", hs["MemberID"]);
            }
            if (hs.Contains("OrderNo"))
            {
                sql += " and a.OrderNo=@OrderNo";
                param.AddWithValue("OrderNo", hs["OrderNo"]);
            }
            if (hs.Contains("MemberMobileNo"))
            {
                sql += " and d.MobileNo=@MemberMobileNo";
                param.AddWithValue("MemberMobileNo", hs["MemberMobileNo"]);
            }
            if (hs.Contains("OrderStatus"))
            {
                sql += " and a.OrderStatus=@OrderStatus";
                param.AddWithValue("OrderStatus", hs["OrderStatus"]);
            }
          
            if (hs.Contains("MinTime"))
            {
                sql += " and a.OrderTime>=@MinTime";
                param.AddWithValue("MinTime", hs["MinTime"]);
            }
            if (hs.Contains("MaxTime"))
            {
                sql += " and a.OrderTime<=@MaxTime";
                param.AddWithValue("MaxTime", hs["MaxTime"]);
            }
        }
        /// <summary>
        /// 订单统计查询条件
        /// </summary>
        /// <param name="hs"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        public void GetOrderReportWhere(Hashtable hs, out string sql, out IDbParameters param)
        {
            sql = @"select d.Name as MemberName,d.MobileNo as MemberMobile,a.TotalMoney as GoodsPrice,a.*,c.PayType as PayMentType,e.Name as LogisticsName
	                       from T_MallOrder a
                           left join T_Member d on d.ID=a.MemberID
                           left join T_Payment c on a.OrderNo=c.OrderNos
                           left join T_Logistics e on e.Code=a.LogisticsCode
                           where 1=1 ";
            param = AdoTemplate.CreateDbParameters();

            //if (hs.Contains("IsView"))
            //{
            //    sql += " and a.QueryStatus=0";
            //}
            if (hs.Contains("MemberID"))
            {
                sql += " and a.MemberID=@MemberID";
                param.AddWithValue("MemberID", hs["MemberID"]);
            }
            if (hs.Contains("OrderNo"))
            {
                sql += " and a.OrderNo=@OrderNo";
                param.AddWithValue("OrderNo", hs["OrderNo"]);
            }
            if (hs.Contains("MemberMobileNo"))
            {
                sql += " and d.MobileNo=@MemberMobileNo";
                param.AddWithValue("MemberMobileNo", hs["MemberMobileNo"]);
            }
            if (hs.Contains("OrderStatus"))
            {
                sql += " and a.OrderStatus=@OrderStatus";
                param.AddWithValue("OrderStatus", hs["OrderStatus"]);
            }            
            if (hs.Contains("MinTime"))
            {
                sql += " and a.OrderTime>=@MinTime";
                param.AddWithValue("MinTime", hs["MinTime"]);
            }
            if (hs.Contains("MaxTime"))
            {
                sql += " and a.OrderTime<=@MaxTime";
                param.AddWithValue("MaxTime", hs["MaxTime"]);
            }
        }
        #endregion
    }
}
