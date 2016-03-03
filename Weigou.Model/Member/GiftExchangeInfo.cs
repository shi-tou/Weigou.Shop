using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Model.Enum;

namespace Weigou.Model
{
    [Serializable]
    public class GiftExchangeInfo
    {
        /// <summary>
        ///  ID
        /// </summary>
        public int ID;
        /// <summary>
        /// 兑换单号
        /// </summary>
        public string OrderNo;
        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID;
        /// <summary>
        /// 礼品ID
        /// </summary>
        public int GiftID;
        /// <summary>
        /// 兑换数据
        /// </summary>
        public int Count;
        /// <summary>
        /// 需要总积分
        /// </summary>
        public int TotalScore;
        /// <summary>
        /// 操作类型
        /// </summary>
        public int Status;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark;
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime;
    }
}