using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Api.Model
{
    /// <summary>
    /// 账户余额信息
    /// </summary>
    [Serializable]
    public class AppBalanceInfo
    {
        /// <summary>
        ///  账户余额
        /// </summary>
        public string AccountBalnace="0.00";
        /// <summary>
        /// 冻结金额
        /// </summary>
        public string FrozenAccount= "0.00";
        /// <summary>
        /// 可用金额
        /// </summary>
        public string AvailableAccount = "0.00";
    }
}