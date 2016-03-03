using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Model.Enum;

namespace Weigou.Model
{
    public class SubmitMallOrderInfo
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID;
        /// <summary>
        /// 收货地址id
        /// </summary>
        public int DeliverAddressID;
        /// <summary>
        /// 购物车ID(多个用逗号','拼接;如：1,2,3)
        /// </summary>
        public string CartIDs;
        /// <summary>
        /// 订单备注
        /// </summary>
        public string OrderRemark;
        /// <summary>
        /// 订单来源 1-web 2-app
        /// </summary>
        public int Source;
        

    }
}