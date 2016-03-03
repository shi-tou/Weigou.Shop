using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using Weigou.Common;
using Weigou.Model.Enum;
using Weigou.Model;

namespace Weigou.Dao
{
    public interface IOrderDao
    { 
        #region 商城订单
        /// <summary>
        /// 订单列表
        /// </summary>
        /// <returns></returns>
        int GetMallOrderList(Pager p, Hashtable hs);
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        DataSet GetMallOrderDetail(string orderNo);

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="strCarOrderNo"></param>
        /// <param name="strCancelReason"></param>
        /// <param name="iOrderStatus"></param>
        /// <returns></returns>
        int UpdateMallOrder(string strCarOrderNo, string strCancelReason, int iOrderStatus);

 

        /// <summary>
        /// 结算订单详情
        /// </summary>       
        /// <param name="strCarOrderNo"></param>
        /// <returns></returns>
        DataTable SettlementMallOrder(string strCarOrderNo);

        /// <summary>
        /// 检查买家已付款卖家未发货(15天)/买家申请退款卖家未处理(24小时)
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        DataSet CheckSellerShip(string OrderNo);
        #endregion        

        #region 提醒发货
        /// <summary>
        /// 提醒发货列表
        /// </summary>
        /// <returns></returns>
        int GetMallRemindSendList(Pager p, Hashtable hs);

        /// <summary>
        /// 提醒发货订单数量
        /// </summary>
        /// <returns></returns>
        int GetMallRemindSendCount();

        #endregion 

        #region 售后服务管理
        /// <summary>
        /// 售后服务列表
        /// </summary>
        /// <returns></returns>
        int GetOrderSaleList(Pager p, Hashtable hs);
        /// <summary>
        /// 查看售后服务详细信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        DataSet GetOrderSaleDetailsInfo(string ID);
        /// <summary>
        /// 查看售后服务信息
        /// </summary>
        /// <param name="OrderSaleID"></param>
        /// <returns></returns>
        DataTable GetOrderSaleInfo(string OrderSaleID);
        ///<summary>
        /// 获取该会员售后状态下的数量
        /// </summary>
        /// <param name="OrderStatus"></param>
        /// <returns></returns>
        int GetSaleStatusCount(string MemberID);
        #endregion

        #region 订单退款管理
        /// <summary>
        /// 订单退款列表
        /// </summary>
        /// <returns></returns>
        int GetMallOrderReturnList(Pager p, Hashtable hs);
        /// <summary>
        /// 查看退款详细信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        DataSet GetMallOrderReturnDetailsInfo(string ID, string OrderNo);
        /// <summary>
        /// 获取订单流水信息
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        DataSet GetMallOrderReturnTradeNo(string OrderNo);
        #endregion
    }
}
