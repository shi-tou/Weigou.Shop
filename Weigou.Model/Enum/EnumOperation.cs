using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Model.Enum
{
    public enum EnumOperation
    {
        /// <summary>
        /// 0-登录
        /// </summary>
        Login = 0,
        /// <summary>
        /// 1-添加
        /// </summary>
        Add = 1,
        /// <summary>
        /// 2-修改
        /// </summary>
        Edit = 2,
        /// <summary>
        /// 3-删除
        /// </summary>
        Delete = 3,
        /// <summary>
        /// 4-审核
        /// </summary>
        Audit = 4,
        /// <summary>
        /// 5-导入
        /// </summary>
        Import = 5,
        /// <summary>
        /// 6-导出
        /// </summary>
        Export = 6,
        /// <summary>
        /// 7-订单发货
        /// </summary>
        Ship = 7,
        /// <summary>
        /// 8-同意售后申请
        /// </summary>
        AgreeSale = 8,
        /// <summary>
        /// 9-拒绝售后申请
        /// </summary>
        RefuseSale = 9,
        /// <summary>
        /// 10-退款
        /// </summary>
        RefundMoney = 10,
        /// <summary>
        /// 98-登出
        /// </summary>
        LoginOut = 98,
        /// <summary>
        /// 99-其他
        /// </summary>
        Other = 99
    }
}
