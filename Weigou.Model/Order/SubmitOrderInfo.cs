using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Model.Order
{
    public class SubmitOrderInfo
    {

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID { get; set; }

        /// <summary>
        /// 车辆ID
        /// </summary>
        public int CarID { get; set; }

        /// <summary>
        /// 取车时间
        /// </summary>
        public DateTime StarTime { get; set; }

        /// <summary>
        /// 还车时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 1-原油位还车  2-按里程计费
        /// </summary>
        public int OilFeeType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
