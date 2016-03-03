using System;
using System.Collections.Generic;
using System.Text;

namespace Weigou.Model.Enum
{
    /// <summary>
    /// 订单状态 
    /// 10-待付款：先款后货，客户还未付款的订单
    /// 20-已付款/待发货：先款后货，客户已经付款的订单,等待发货
    /// 30-已发货/待收货：已发货，等待客户签收的订单
    /// 40-已收货/待评价：客户已经签收的订单
    /// 50-申请退款：客户申请退款或退货的订单
    /// 60-交易取消：已取消的订单
    /// 70-交易完成：已完成的订单
    /// 9-删除
    /// </summary>
    public enum EnumMallOrderStatus
    {
        /// <summary>
        /// 10-待付款 
        /// </summary>
        ToPay = 10,
        /// <summary>
        /// 20-已付款/待发货
        /// </summary>
        Paid = 20,
        /// <summary>
        /// 30-已发货，待收货
        /// </summary>
        Shipped = 30,
        /// <summary>
        /// 40-已收货
        /// </summary>
        Received = 40,
        /// <summary>
        /// 已评价
        /// </summary>
        HasComment = 41,
        /// <summary>
        /// 50-申请退款
        /// </summary>
        ApplyRefund = 50,
        /// <summary>
        /// 卖家同意退款
        /// </summary>
        AgreeReturn = 51,
        /// <summary>
        /// 已退款
        /// </summary>
        FinishReturn = 52,
        /// <summary>
        /// 60-交易取消
        /// </summary>
        Canceled = 60,
        /// <summary>
        /// 70-交易完成
        /// </summary>
        Finished = 70,
        /// <summary>
        /// 9-订单删除
        /// </summary>
        Deleted = 9
    }
    public enum EnumMemberOrderStatus
    {
        /// <summary>
        /// 未付款待支付
        /// </summary>
        NoPay = 0,
        /// <summary>
        /// 已付款待发货
        /// </summary>
        Pay = 1,
        /// <summary>
        /// 已发货待收货
        /// </summary>
        Shipped = 2,
        /// <summary>
        /// 已收货待评价
        /// </summary>
        Signed = 3,
        /// <summary>
        /// 已评价
        /// </summary>
        HasComment = 4,
        /// <summary>
        /// 买家取消订单
        /// </summary>
        Cancel = 5,
        /// <summary>
        /// 买家申请退款
        /// </summary>
        ApplyReturn = 6,
        /// <summary>
        /// 卖家同意退款
        /// </summary>
        AgreeReturn = 7,
        /// <summary>
        /// 已退款
        /// </summary>
        FinishReturn = 8,
        /// <summary>
        /// 删除订单
        /// </summary>
        Delete = 9
    }
}
