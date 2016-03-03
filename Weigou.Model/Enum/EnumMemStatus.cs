using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Model.Enum
{
    public enum EnumMemStatus
    {
        /// <summary>
        /// 0-冻结
        /// </summary>
        Locked = 0,
        /// <summary>
        /// 1-可用
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 9-删除(逻辑删除)
        /// </summary>
        Delete = 9
    }
}
