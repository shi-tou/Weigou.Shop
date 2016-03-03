using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Model.Enum;

namespace Weigou.Model
{
    /// <summary>
    /// 商品属性值信息
    /// </summary>
    public class GoodsAttrInfo
    {
        /// <summary>
        /// 属性ID
        /// </summary>
        public string AttributeID;
        /// <summary>
        /// 属性名称
        /// </summary>
        public string AttributeName;
        /// <summary>
        /// 属性值
        /// </summary>
        public List<AttrValueInfo> Values = new List<AttrValueInfo>();
    }
}
