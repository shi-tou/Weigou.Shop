using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Model.Enum;

namespace Weigou.Model
{
    public class MallOrderInfo
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo;
        /// <summary>
        /// 商品总金额
        /// </summary>
        public decimal TotalMoney;
        /// <summary>
        /// 商品总数量
        /// </summary>
        public decimal TotalCount;
        /// <summary>
        /// 付款状态
        /// </summary>
        public int PayStatus;
        /// <summary>
        /// 物流状态
        /// </summary>
        public int LogisticsStatus;
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus;
        /// <summary>
        /// 订单备注
        /// </summary>
        public string OrderRemark;
        /// <summary>
        /// 快递公司编码
        /// </summary>
        public string LogisticsCode;
        /// <summary>
        /// 快递单号
        /// </summary>
        public string LogisticsNo;
        /// <summary>
        /// 订单时间
        /// </summary>
        public string OrderTime;
        public List<MallOrderDetailInfo> MallOrderDetail;
    }
}