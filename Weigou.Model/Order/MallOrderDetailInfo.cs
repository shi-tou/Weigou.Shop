using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Model.Enum;

namespace Weigou.Model
{
    public class MallOrderDetailInfo
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo;
        /// <summary>
        /// 商品ID
        /// </summary>
        public int GoodsID;
        /// <summary>
        /// 商品名
        /// </summary>
        public string GoodsName;
        /// <summary>
        /// 单价
        /// </summary>
        public string SalePrice;
        /// <summary>
        /// 数量
        /// </summary>
        public int Count;
        /// <summary>
        /// 小计金额
        /// </summary>
        public string TotalPrice;
        /// <summary>
        /// 图片
        /// </summary>
        public string SmallPicture;
        /// <summary>
        /// 销售属性
        /// </summary>
        public string SaleProp;
    }
}