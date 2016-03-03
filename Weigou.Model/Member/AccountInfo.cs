using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Model.Enum;

namespace Weigou.Model
{
    /// <summary>
    /// 账户余额信息
    /// </summary>
    public class AccountInfo
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int ID;
        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID;
        /// 订单号
        /// </summary>
        public string OrderNo="";
        /// <summary>
        /// 变更金额
        /// </summary>
        public string Account="";
        /// <summary>
        /// 账户余额
        /// </summary>
        public string Balance = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark = "";
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime="";
    }
}