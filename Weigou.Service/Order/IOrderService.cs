using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using Weigou.Common;
using Weigou.Model;
using Spring.Transaction.Interceptor;
using Weigou.Model.Enum;
using Weigou.Model.Order;

namespace Weigou.Service
{
    public interface IOrderService : IBaseService
    {
        #region 商城订单
        /// <summary>
        /// 订单列表
        /// </summary>
        /// <returns></returns>
        int GetMallOrderList(Pager p, Hashtable hs);
        /// <summary>
        /// 订单列表(App)
        /// </summary>
        /// <returns></returns>
        List<MallOrderInfo> GetMallOrderForApp(Pager p, Hashtable hs);
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        DataSet GetMallOrderDetail(string orderNo);
        /// <summary>
        /// 提交商城订单
        /// </summary>
        /// <param name="info">订单信息</param>
        /// <param name="orderNo">订单号(输出)</param>
        /// <returns></returns>
        int SubmitMallOrder(SubmitMallOrderInfo info,out string orderNo);
        /// <summary>
        /// 确认订单发货
        /// </summary>
        /// <param name="info">发货信息</param>
        /// <returns></returns>
        int ConfirmMallOrderShipped(MallOrderShipInfo info);
        /// <summary>
        /// 提交订单跟踪表
        /// </summary>
        /// <param name="OrderNo">订单跟踪信息</param>
        /// <returns></returns>
        int AddMallOrderTrack(MallOrderTrackInfo info);

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="strMallOrderNo">订单号</param>
        int ConfirmMallOrderReceived(string strMallOrderNo);

        /// <summary>
        /// 修改商品订单状态
        /// </summary>
        /// <param name="strCarOrderNo"></param>       
        /// <param name="iOrderStatus"></param>
        /// <returns></returns>
        int UpdateMallOrder(string strCarOrderNo, int iOrderStatus);

        /// <summary>
        /// 修改商品订单状态
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

        /// <summary>
        /// 提醒发货
        /// </summary>
        /// <param name="OrderNo">订单编号</param>
        /// <returns></returns>
        int AddMallRemindSend(string OrderNo);
        
        #endregion

        #region 售后服务管理
        /// <summary>
        /// 申请售后服务
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        int ApplyOrderSale(Hashtable hs);
        /// <summary>
        /// 处理售后申请
        /// </summary>
        /// <returns></returns>
        int DealOrderSale(string ID, string UserID, string Remark, string Status);
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
        #endregion

        #region 全额退款管理
        /// <summary>
        /// 全额退款申请
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        int ApplyOrderReturn(string OrderNo, string Remark);
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
        /// 处理退款申请
        /// </summary>
        /// <returns></returns>
        int DealOrderReturn(string ID, string Remark, string UserID);
        /// <summary>
        /// 获取订单流水信息
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        DataSet GetMallOrderReturnTradeNo(string OrderNo);
        #endregion

        #region 支付异步通知

        /// <summary>
        /// 支付异步通知
        /// </summary>
        /// <param name="out_trade_no">商户订单号</param>
        /// <param name="notify_trade_no">支付后台返回的订单号</param>
        /// <returns></returns>
        void PayNotify(string out_trade_no, string notify_trade_no);

        #endregion

        #region 退款异步通知

        /// <summary>
        /// 退款异步通知
        /// </summary>
        /// <param name="batch_no">退款批次号</param>
        /// <returns></returns>
        void RefundNotify(string batch_no);

        #endregion
    }
}
