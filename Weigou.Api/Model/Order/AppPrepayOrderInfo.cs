using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Weigou.Api.Model
{
    public class AppPrepayOrderInfo
    {
        /// <summary>
        /// 订单商品清单
        /// </summary>
        public List<CartGoodsInfo> GoodsList;
        /// <summary>
        /// 默认地址
        /// </summary>
        public AppDeliverAddress DefaultAddress;
        /// <summary>
        /// 运费
        /// </summary>
        public string ExpressFee;
        /// <summary>
        /// 商品总数量
        /// </summary>
        public int TotalCount;
        /// <summary>
        /// 商品总金额
        /// </summary>
        public string TotalMoney;
    }
}