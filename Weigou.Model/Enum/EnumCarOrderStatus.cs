using System;
using System.Collections.Generic;
using System.Text;

namespace Weigou.Model.Enum
{
    /// <summary>
    /// 租车订单状态
    /// </summary>
    public enum EnumCarOrderStatus
    {
        /// <summary>
        /// 0-租客提交订单(预定)
        /// </summary>
        Submit = 0,
            
        /// <summary>
        /// 15-车辆押金(未付)
        /// </summary>
        Paid = 15,

        /// <summary>
        /// 20-违章押金(未付)
        /// </summary>
        Illegal = 20,

        /// <summary>
        /// 30-行程开始
        /// </summary>
        TravelStart = 30,

        /// <summary>
        /// 40-取消订单
        /// </summary>
        Cancel = 40,

        /// <summary>
        /// 43-自动取消(响应超时)
        /// </summary>
        AutoCancel = 43,

        /// <summary>
        /// 45-平台拒绝
        /// </summary>
        Refuse = 45,

        /// <summary>
        /// 50-事故保险
        /// </summary>
        Accident = 50,

        /// <summary>
        /// 100-订单完成
        /// </summary>
        Finish = 100
    }
}
