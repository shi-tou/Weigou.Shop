using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Model.Enum
{
    public enum EnumStatus
    {
        /// <summary>
        /// 0-不可用(待审核)
        /// </summary>
        Disabled = 0,
        /// <summary>
        /// 1-可用、启用（审核通过）
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 2-停用、审核不通过
        /// </summary>
        DisAudit = 2,
        /// <summary>
        /// 9-删除(逻辑删除)
        /// </summary>
        Delete = 9
    }
}
