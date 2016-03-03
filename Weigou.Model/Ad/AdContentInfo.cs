using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Model
{
    [Serializable]
    public class AdContentInfo
    {
        /// <summary>
        ///  ID
        /// </summary>
        public int ID;
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo;
        /// <summary>
        /// 商户
        /// </summary>
        public int MerchantID;
        /// <summary>
        /// 广告位价格
        /// </summary>
        public string AdPositionName;
        /// <summary>
        /// 广告位价格
        /// </summary>
        public int AdPriceID;
        /// <summary>
        /// 广告位日期
        /// </summary>
        public string AdDate;
        /// <summary>
        /// 商品
        /// </summary>
        public int GoodsID;
        /// <summary>
        /// 广告语
        /// </summary>
        public string Title;
        /// <summary>
        /// 链接
        /// </summary>
        public string Url;
        /// <summary>
        /// 图片
        /// </summary>
        public string Picture;
        /// <summary>
        /// 周期类别（1-按分 2-按时 3-按天 3-按月）
        /// </summary>
        public int TimesType;
        /// <summary>
        /// 周期
        /// </summary>
        public int ShowTimes;
        /// <summary>
        /// 积分
        /// </summary>
        public int NeedScore;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime;
        /// <summary>
        /// 状态
        /// </summary>
        public int Status;
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime;
        /// <summary>
        /// 审核人
        /// </summary>
        public int ApprovedBy;
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime ApprovedTime;
        /// <summary>
        /// 审核备注
        /// </summary>
        public string ApprovedRemark;
    }
}