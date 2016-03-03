using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Data.Common;
using System.Data;
using System.Collections;
using Weigou.Common;
using Weigou.Model.Enum;
using Weigou.Model;

namespace Weigou.Dao
{
    public class OrderDao : BaseDao, IOrderDao
    { 
        #region 商城订单管理
        /// <summary>
        /// 订单列表
        /// </summary>
        /// <returns></returns>
        public int GetMallOrderList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as MemberName,b.MobileNo from T_MallOrder a
                            left join T_Member b on b.ID=a.MemberID
                            where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();

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
            if (hs.Contains("MobileNo"))
            {
                sql += " and b.MobileNo=@MobileNo";
                param.AddWithValue("MobileNo", hs["MobileNo"]);
            }
            //支付状态
            if (hs.Contains("PayStatus"))
            {
                sql += " and a.PayStatus=@PayStatus";
                param.AddWithValue("PayStatus", hs["PayStatus"]);
            }
            //物流状态
            if (hs.Contains("LogisticsStatus"))
            {
                sql += " and a.LogisticsStatus=@LogisticsStatus";
                param.AddWithValue("LogisticsStatus", hs["LogisticsStatus"]);
            }
            //订单状态
            if (hs.Contains("OrderStatus"))
            {
                sql += " and a.OrderStatus=@OrderStatus";
                param.AddWithValue("OrderStatus", hs["OrderStatus"]);
            }
            //订单删除
            if (hs.Contains("DeleteOrder"))
            {
                sql += " and a.OrderStatus<>@DeleteOrder";
                param.AddWithValue("DeleteOrder", hs["DeleteOrder"]);
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

            sql = PagerSql(sql, p);

            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public DataSet GetMallOrderDetail(string orderNo)
        {
            string sql = @"select a.*,b.Name as MemberName,b.MobileNo,c.Name as LogisticsName from T_MallOrder a 
                            inner join T_Member b on b.ID=a.MemberID
                            left join T_Logistics c on c.Code=a.LogisticsCode
                            where a.OrderNo=@OrderNo;
                           select a.* from T_MallOrderDetail a where a.OrderNo=@OrderNo;";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (!string.IsNullOrEmpty(orderNo))
            {
                param.AddWithValue("OrderNo", orderNo);
            }
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds;
        }
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="strCarOrderNo"></param>
        /// <param name="strCancelReason"></param>
        /// <param name="iOrderStatus"></param>
        /// <returns></returns>
        public int UpdateMallOrder(string strCarOrderNo, string strCancelReason, int iOrderStatus)
        {
            IDbParameters param = AdoTemplate.CreateDbParameters();

            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("update T_MallOrder set ");
            sbSql.Append("OrderStatus=@OrderStatus ");
            if (!string.IsNullOrEmpty(strCancelReason))
                sbSql.Append(",CancelReason=@CancelReason ");
            sbSql.Append(" where OrderNo in(@OrderNo) ");

            param.AddWithValue("OrderNo", strCarOrderNo);
            param.AddWithValue("CancelReason", strCancelReason);
            param.AddWithValue("OrderStatus", iOrderStatus);
            int i = AdoTemplate.ExecuteNonQuery(CommandType.Text, sbSql.ToString(), param);
            if (i > 0)
                return RT.SUCCESS;
            return RT.FAILED;
        }
        /// <summary>
        /// 结算订单详情
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable SettlementMallOrder(string strCarOrderNo)
        {
            string sql = @"select c.Name as CarName,c.CarNo,cr.StarTime,cr.ActualStarTime,cr.EndTime,cr.ActualEndTime, 
                            cr.OilFeeType,cr.Kilometers,cr.CarVouchers,cr.IntegralVouchers,cr.Deductible,
                            cr.Price,cr.InsurancePrice,cr.RentDepositPrice,cr.IllegalDepositPrice,cr.TotalRentPrice,
                            cr.CreateTime 
                            from T_MallOrder cr 
                            inner join T_Goods c on c.ID=cr.CarID 
                            where cr.Status=" + (int)EnumStatus.Normal;
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (!string.IsNullOrEmpty(strCarOrderNo))
            {
                sql += " and cr.OrderNo=@OrderNo ";
                param.AddWithValue("OrderNo", strCarOrderNo);
            }

            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            if (ds.Tables.Count < 1)
                return null;
            return ds.Tables[0];
        }

        /// <summary>
        /// 检查买家已付款卖家未发货(15天)/买家申请退款卖家未处理(24小时)
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public DataSet CheckSellerShip(string OrderNo)
        {
            string sql = @"select * from T_MallOrder a 
                           left join T_MallOrderReturn b on b.OrderNo=a.OrderNo
	                       where a.OrderStatus=1 and b.Status=5 and datediff(hour, a.PayTime,GETDATE()) >= 24*15
                           and a.OrderNo=@OrderNo;
                           select * from T_MallOrderReturn a  where a.Status=0 and  datediff(hour, a.ApplyTime,GETDATE()) >= 24
                           and a.OrderNo=@OrderNo;";

            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (!string.IsNullOrEmpty(OrderNo))
            {
                param.AddWithValue("OrderNo", OrderNo);
            }

            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds;
        }

        #endregion

        #region 提醒发货
        /// <summary>
        /// 提醒发货列表
        /// </summary>
        /// <returns></returns>
        public int GetMallRemindSendList(Pager p, Hashtable hs)
        {
            string sql = @"select b.*,c.Name as MemberName,c.MobileNo
                           from T_MallRemindSend a
                           left join T_MallOrder b on b.OrderNo=a.OrderNo 
                           left join T_Member c on b.ID=b.MemberID
                           where  b.OrderStatus=20 and b.LogisticsStatus=1 and b.PayStatus=2";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("OrderNo"))
            {
                sql += " and b.OrderNo like @OrderNo";
                param.AddWithValue("OrderNo", "%" + hs["OrderNo"] + "%");
            }
            //if (hs.Contains("MobileNo"))
            //{
            //    sql += " and b.MobileNo=@MobileNo";
            //    param.AddWithValue("MobileNo", hs["MobileNo"]);
            //}
            if (hs.Contains("MinTime"))
            {
                sql += " and b.OrderTime>=@MinTime";
                param.AddWithValue("MinTime", hs["MinTime"]);
            }
            if (hs.Contains("MaxTime"))
            {
                sql += " and b.OrderTime<=@MaxTime";
                param.AddWithValue("MaxTime", hs["MaxTime"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }

        /// <summary>
        /// 提醒发货订单数量
        /// </summary>
        /// <returns></returns>
        public int GetMallRemindSendCount()
        {
            string sql = @"select count(*) as TotalCount from T_MallRemindSend a
                        left join T_MallOrder b on b.OrderNo=a.OrderNo 
                        where b.OrderStatus=20 and b.LogisticsStatus=1 and b.PayStatus=2 ;";
            IDbParameters param = AdoTemplate.CreateDbParameters();
             
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }
        #endregion

        #region 订单退款管理
        /// <summary>
        /// 订单退款列表
        /// </summary>
        /// <returns></returns>
        public int GetMallOrderReturnList(Pager p, Hashtable hs)
        {
            string sql = @"select a.ID,a.OrderNo,a.ApplyTime,DATEADD(day,1,a.ApplyTime) as LastTime,a.Status,b.Name as DealName,a.DealTime,d.MobileNo,d.Name as MemberName,e.NotifyTradeNo,e.PayType
                           from T_MallOrderReturn a
                           left join T_User b on b.ID=a.DealBy
                           left join T_MallOrder c on c.OrderNo=a.OrderNo
                           left join T_Member d on d.ID=c.MemberID 
                           left join T_Payment e on e.OrderNos=a.OrderNo and e.PayStatus=1
                           where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();

            if (hs.Contains("OrderNo"))
            {
                sql += " and c.OrderNo like @OrderNo";
                param.AddWithValue("OrderNo", "%" + hs["OrderNo"] + "%");
            }
            if (hs.Contains("MobileNo"))
            {
                sql += " and d.MobileNo like @MobileNo";
                param.AddWithValue("MobileNo", "%" + hs["MobileNo"] + "%");
            }
            if (hs.Contains("PayType"))
            {
                sql += " and e.PayType=@PayType";
                param.AddWithValue("PayType", hs["PayType"]);
            }
            if (hs.Contains("Status"))
            {
                sql += " and a.Status=@Status";
                param.AddWithValue("Status", hs["Status"]);
            } 
            if (hs.Contains("MemberID"))
            {
                sql += " and d.MemberID=@MemberID";
                param.AddWithValue("MemberID", hs["MemberID"]);
            } 
            if (hs.Contains("MinTime"))
            {
                sql += " and a.ApplyTime>=@MinTime";
                param.AddWithValue("MinTime", hs["MinTime"]);
            }
            if (hs.Contains("MaxTime"))
            {
                sql += " and a.ApplyTime<=@MaxTime";
                param.AddWithValue("MaxTime", hs["MaxTime"]);
            }
            sql = PagerSql(sql, p);

            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        /// <summary>
        /// 查看退款详细信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataSet GetMallOrderReturnDetailsInfo(string ID, string OrderNo)
        {
            string sql = @"select a.ID,a.Remark,a.Reason,a.ApplyTime,a.Status,a.DealTime,c.Name as MemberName,c.MobileNo as MemberMobileNo,b.ConsigneeName,b.ConsigneeMobileNo,b.DeliverAddress,b.ZipCode,b.TotalMoney,b.OrderStatus,b.PayStatus,b.LogisticsStatus,e.NotifyTradeNo,e.PayType from T_MallOrderReturn a
                           left join T_MallOrder b on b.OrderNo=a.OrderNo
                           left join T_Member c on c.ID=b.MemberID
                           left join T_Payment e on e.OrderNos=a.OrderNo and e.PayStatus=1
                           where a.ID=@ID;
                           select a.ID, a.GoodsID,a.SmallPicture,a.GoodsName,a.SalePrice,a.Count,a.SaleProp,a.Count  from T_MallOrderDetail a                           
                           where a.OrderNo=@OrderNo;";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (!string.IsNullOrEmpty(ID))
            {
                param.AddWithValue("ID", ID);
            }
            if (!string.IsNullOrEmpty(OrderNo))
            {
                param.AddWithValue("OrderNo", OrderNo);
            }
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds;
        }
        /// <summary>
        /// 获取订单流水信息
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public DataSet GetMallOrderReturnTradeNo(string OrderNo)
        {
            string sql = @"select b.TotalMoney,e.NotifyTradeNo,e.PaymentNo as TradeNo from T_MallOrderReturn a
                           left join T_MallOrder b on b.OrderNo=a.OrderNo                           
                           left join T_Payment e on e.OrderNos=a.OrderNo and e.PayStatus=1
                           where a.OrderNo=@OrderNo";
            IDbParameters param = AdoTemplate.CreateDbParameters();
             
            if (!string.IsNullOrEmpty(OrderNo))
            {
                param.AddWithValue("OrderNo", OrderNo);
            }
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds;
        }
        #endregion

        #region 售后服务管理
        /// <summary>
        /// 售后服务列表
        /// </summary>
        /// <returns></returns>
        public int GetOrderSaleList(Pager p, Hashtable hs)
        {
            string sql = @"select c.OrderNo,a.ID,a.Type,a.ApplyNumber,a.Description,a.ApplyTime,a.Status,b.Name as DealName,a.DealTime,c.GoodsName,d.ConsigneeMobileNo,
                           c.SmallPicture,c.SalePrice,c.SaleProp from T_MallOrderSale a
                           left join T_User b on b.ID=a.DealBy
                           left join T_MallOrderDetail c on c.ID=a.OrderID
                           left join T_MallOrder d on d.OrderNo=c.OrderNo
                           left join T_Member e on e.ID=d.MemberID                           
                           where 1=1 ";  // f.Logo as MerchantLogo,f.Name as MerchantName,f.ID as MerchantID,| left join T_Merchant f on f.ID=d.MerchantID
            IDbParameters param = AdoTemplate.CreateDbParameters();

            if (hs.Contains("Type"))
            {
                sql += " and a.Type=@Type";
                param.AddWithValue("Type", hs["Type"]);
            }
            if (hs.Contains("OrderNo"))
            {
                sql += " and c.OrderNo like @OrderNo";
                param.AddWithValue("OrderNo", "%" + hs["OrderNo"] + "%");
            }
            if (hs.Contains("ConsigneeMobileNo"))
            {
                sql += " and d.ConsigneeMobileNo like @ConsigneeMobileNo";
                param.AddWithValue("ConsigneeMobileNo", "%" + hs["ConsigneeMobileNo"] + "%");
            }
            //if (hs.Contains("MerchantName"))
            //{
            //    sql += " and f.Name like @MerchantName";
            //    param.AddWithValue("MerchantName", '%' + hs["MerchantName"].ToString() + '%');
            //}
            if (hs.Contains("MemberID"))
            {
                sql += " and d.MemberID=@MemberID";
                param.AddWithValue("MemberID", hs["MemberID"]);
            }
            //if (hs.Contains("MerchantID"))
            //{
            //    if (!string.IsNullOrEmpty(hs["MerchantID"].ToString()))
            //    {
            //        sql += " and c.MerchantID=@MerchantID";
            //        param.AddWithValue("MerchantID", hs["MerchantID"]);
            //    }
            //    else
            //    {
            //        sql += " and isnull(c.MerchantID,'')=''";
            //    }
            //}
            //if (hs.Contains("OrderType"))
            //{
            //    if (hs["OrderType"].ToString() == "1")
            //    {
            //        sql += " and isnull(d.MerchantID,'')=''";
            //    }
            //    else
            //    {
            //        sql += " and d.MerchantID<>0";
            //    }
            //}
            if (hs.Contains("Status"))
            {
                sql += " and a.Status=@Status";
                param.AddWithValue("Status", hs["Status"]);
            }
            if (hs.Contains("MinTime"))
            {
                sql += " and a.ApplyTime>=@MinTime";
                param.AddWithValue("MinTime", hs["MinTime"]);
            }
            if (hs.Contains("MaxTime"))
            {
                sql += " and a.ApplyTime<=@MaxTime";
                param.AddWithValue("MaxTime", hs["MaxTime"]);
            }
            sql = PagerSql(sql, p);

            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
        public DataSet GetOrderSaleDetailsInfo(string ID)
        {
            string sql = @"select c.OrderNo,a.Pic,e.Name as MemberName,e.MobileNo as MemberMobileNo,d.ConsigneeName,d.ConsigneeMobileNo,d.DeliverAddress,d.ZipCode,a.Description, a.ID,a.Type,a.Remark,a.ApplyNumber,a.ApplyTime,a.Status,a.DealTime,c.GoodsName  from T_MallOrderSale a
                           left join T_MallOrderDetail c on c.ID=a.OrderID
                           left join T_MallOrder d on d.OrderNo=c.OrderNo
                           left join T_Member e on e.ID=d.MemberID
                           where a.ID=@ID;
                           select *from T_MallOrderDetail a 
                           left join T_MallOrder b on b.OrderNo=a.OrderNo
                           left join T_MallOrderSale c on c.OrderID=a.ID
                           where c.ID=@ID ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (!string.IsNullOrEmpty(ID))
            {
                param.AddWithValue("ID", ID);
            }
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            return ds;
        }
        /// <summary>
        /// 查看售后服务信息
        /// </summary>
        /// <param name="OrderSaleID"></param>
        /// <returns></returns>
        public DataTable GetOrderSaleInfo(string OrderSaleID)
        {
            string sql = @"select a.ApplyTime,a.DealTime,a.Type,a.Description,a.Remark,b.Status from T_MallOrderSale a
                           left join T_MallOrderSaleTrack b on b.OrderSaleID=a.ID
                           where 1=1 ";

            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (!string.IsNullOrEmpty(OrderSaleID))
            {
                sql += " and b.OrderSaleID=@OrderSaleID";
                param.AddWithValue("OrderSaleID", OrderSaleID);
            }

            return AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
        }
        ///<summary>
        /// 获取该会员售后状态下的数量
        /// </summary>
        /// <param name="OrderStatus"></param>
        /// <returns></returns>
        public int GetSaleStatusCount(string MemberID)
        {
            string sql = @"select count(*) as Count from T_MallOrderSale a
                           left join T_MallOrderDetail b on b.ID=a.OrderID
                           left join T_MallOrder c on c.OrderNo=b.OrderNo
                           where a.Status=0 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (!string.IsNullOrEmpty(MemberID))
            {
                sql += " and c.MemberID=@MemberID";
                param.AddWithValue("MemberID", MemberID);
            }
            DataTable dt = AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
            if (dt.Rows.Count == 0)
                return 0;
            return Convert.ToInt32(dt.Rows[0]["Count"]);
        }
        #endregion
    }
}