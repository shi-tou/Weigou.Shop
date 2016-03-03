using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Api.Model
{
    /// <summary>
    /// 收货人信息管理
    /// </summary>
    public class AppDeliverAddress
    {
        /// <summary>
        /// 收货地址ID
        /// </summary>
        public int ID;
        /// <summary>
        /// 是否默认地址
        /// </summary>
        public int IsDefault;
        /// <summary>
        /// 收货人手机
        /// </summary>
        public string ConsigneeMobileNo;
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ConsigneeName;
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address;
        /// <summary>
        /// 邮编
        /// </summary>
        public string ZipCode;
    }
}
