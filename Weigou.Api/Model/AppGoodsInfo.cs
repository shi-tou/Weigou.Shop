using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Api.Model
{
    [Serializable]
    public class AppGoodsInfo
    {
        /// <summary>
        ///  ID
        /// </summary>
        public int ID;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name;
        /// <summary>
        /// 销售价
        /// </summary>
        public string SalePrice;
        /// <summary>
        /// 市场价
        /// </summary>
        public string MarketPrice;
        /// <summary>
        /// 库存
        /// </summary>
        public int Stock;
        /// <summary>
        /// 简单描述
        /// </summary>
        public string SimpleDesc;
        /// <summary>
        /// 图片
        /// </summary>
        public string SmallPicture;
    }
    /// <summary>
    /// 商品库存在及价格
    /// </summary>
    [Serializable]
    public class StockAndPrice
    {
        public string Price;
        public int Stock;
    }
    [Serializable]
    public class GoodsCommentInfo
    {
        public string Star;
        public string MobileNo;
        public string CommentTime;
        public string CommentContent;
        public string ReplyContent;
        public string ReplyTime;
        public string SalePropName;
    }
}