using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Model.Enum;

namespace Weigou.Model
{
    [Serializable]
    public class DeliverAddressInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID;
        /// <summary>
        /// 会员
        /// </summary>
        public int MemberID;
        /// <summary>
        /// 省
        /// </summary>
        public int ProvinceID;
        public string ProvinceName;
        /// <summary>
        /// 市
        /// </summary>
        public int CityID;
        public string CityName;
        /// <summary>
        /// 区
        /// </summary>
        public int DistrictID;
        public string DistrictName;
        /// <summary>
        /// 街道地址
        /// </summary>
        public string Address;
        /// <summary>
        /// 邮编
        /// </summary>
        public string ZipCode;
        /// <summary>
        /// 收货人
        /// </summary>
        public string ConsigneeName;
        /// <summary>
        /// 收货人
        /// </summary>
        public string ConsigneeMobileNo;
        /// <summary>
        /// 默认地址
        /// </summary>
        public int IsDefault;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime;

    }
}