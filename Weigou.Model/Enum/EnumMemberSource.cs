using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Model.Enum
{
    public enum EnumMemberSource
    {
        /// <summary>
        /// 0-其他
        /// </summary>
        Other = 0,
        /// <summary>
        /// 1-admin后台
        /// </summary>
        User = 1,
        /// <summary>
        /// 2-Web前台注册
        /// </summary>
        Web = 2,
        /// <summary>
        /// 3-App注册
        /// </summary>
        App = 3
    }
}