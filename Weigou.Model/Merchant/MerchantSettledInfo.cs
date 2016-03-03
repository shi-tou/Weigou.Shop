using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Model.Enum;

namespace Weigou.Model
{
    public class MerchantSettledInfo
    {
        /// <summary>
        /// 入驻ID
        /// </summary>
        public int ID;
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName;
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName;
        /// <summary>
        /// 联系手机
        /// </summary>
        public string ContactPhone;
        /// <summary>
        /// 联系地址
        /// </summary>
        public string ContactAddress;
        /// <summary>
        /// 银行类型
        /// </summary>
        public int BankType;
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankNo;
        /// <summary>
        /// 经营类目
        /// </summary>
        public string BussineType;
        /// <summary>
        /// 上传的文档路径
        /// </summary>
        public string DocUrl;
        /// <summary>
        /// 审核状态
        /// </summary>
        public int Status;
        /// <summary>
        /// 申请时间
        /// </summary>
        public string ApplyTime;
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