using System;
using System.Collections.Generic;
using System.Text;

namespace Weigou.Model.Enum
{
    /// <summary>
    /// 商城订单支付状态 1-待发货  2-已发货 3-已收货 4-申请换货 5-已换货
    /// </summary>
    public enum EnumLogisticsStatus
    {
        /// <summary>
        /// 1-待发货  
        /// </summary>
        ToShip = 1,
        /// <summary>
        /// 2-已发货
        /// </summary>
        Shipped = 2,
        /// <summary>
        /// 3-已收货
        /// </summary>
        Received = 3,
    }
}
