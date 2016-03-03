using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Model;

namespace Weigou.Api.Model
{
    [Serializable]
    public class AppGoodsDetailInfo
    {
        /// <summary>
        ///  ID
        /// </summary>
        public int ID;
        /// <summary>
        /// 产品名
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
        /// 详情描述
        /// </summary>
        public string Description;
        /// <summary>
        /// 图片
        /// </summary>
        public List<PictureInfo> Picture;
        /// <summary>
        /// 商品属性信息
        /// </summary>
        public List<GoodsAttrInfo> SalePropList;
    }
}