using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Model.Enum
{
    public enum EnumGoodsStatus
    {
        /// <summary>
        /// 0-待审核
        /// </summary>
        Disabled = 0,
        /// <summary>
        /// 1-审核通过
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 2-审核不通过
        /// </summary>
        DisAudit = 2,
        /// <summary>
        /// 9-删除(逻辑删除)
        /// </summary>
        Delete = 9
    }
}
