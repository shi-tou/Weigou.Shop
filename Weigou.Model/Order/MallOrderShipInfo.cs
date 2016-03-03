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
    public class MallOrderShipInfo
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo;
        /// <summary>
        /// 快递公司Code
        /// </summary>
        public string LogisticsCode;
        /// <summary>
        /// 快递单号
        /// </summary>
        public string LogisticsNo;
        /// <summary>
        /// 操作人
        /// </summary>
        public int CreateBy;
    }
}