using System;
using System.Collections.Generic;
using System.Text;

namespace Weigou.Model.Enum
{
    /// <summary>
    /// 商城订单支付状态 1-未支付、待付款 2-已付款 3-申请退款 4-已退款
    /// </summary>
    public enum EnumPayStatus
    {
        /// <summary>
        /// 1-未支付
        /// </summary>
        ToPay = 1,
        /// <summary>
        /// 2-已付款
        /// </summary>
        Paid = 2,
        /// <summary>
        /// 3-申请退款
        /// </summary>
        ApplyRefund = 3,
        /// <summary>
        /// 4-已退款
        /// </summary>
        Refund = 4
    }

    public enum EnumPayMent
    {
        /// <summary>
        /// 微信
        /// </summary>
        WeChat = 1,
        /// <summary>
        /// 支付宝
        /// </summary>
        AliPay = 2,
        /// <summary>
        /// 银联
        /// </summary>
        ChinaPay = 3
    }
}
