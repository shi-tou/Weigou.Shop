using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weigou.Api.Model
{
    /// <summary>
    /// 购物车商品信息
    /// </summary>
    [Serializable]
    public class CartGoodsInfo
    {
        /// <summary>
        /// 购物车Id
        /// </summary>
        public int CartID;
        /// <summary>
        /// 商品id
        /// </summary>
        public int GoodsID;
        /// <summary>
        /// 商品名
        /// </summary>
        public string GoodsName;
        /// <summary>
        /// 购买数量
        /// </summary>
        public int Count;
        /// <summary>
        /// 属性信息
        /// </summary>
        public string PropName;
        /// <summary>
        /// 价格
        /// </summary>
        public string SalePrice;
        /// <summary>
        /// 图片
        /// </summary>
        public string SmallPicture;
    }
}