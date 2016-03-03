using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Model.Enum;

namespace Weigou.Model
{
    /// <summary>
    /// 订单发货信息实体类
    /// </summary>
    public class MallOrderTrackInfo
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo;
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus;
        /// <summary>
        /// 付款状态
        /// </summary>
        public int PayStatus;
        /// <summary>
        /// 快递状态
        /// </summary>
        public int LogisticsStatus;
        /// <summary>
        /// 操作人
        /// </summary>
        public int CreateBy;
    }
}