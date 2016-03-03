using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Model
{

    [Serializable]
    public class MemberInfo
    {
        /// <summary>
        ///  ID
        /// </summary>
        public int ID;
        /// <summary>
        ///  会员卡号
        /// </summary>
        public string CardNo;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name;
        /// <summary>
        /// 会员级别
        /// </summary>
        public int LevelID;
        /// <summary>
        /// 手机
        /// </summary>
        public string MobileNo;
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password;
        /// <summary>
        /// 性别(1-男 2-女)
        /// </summary>
        public int Sex;
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday;
        /// <summary>
        /// 省
        /// </summary>
        public int ProvinceID;
        /// <summary>
        /// 市
        /// </summary>
        public int CityID;
        /// <summary>
        /// 区
        /// </summary>
        public int DistrictID;
        /// <summary>
        /// 会员头像
        /// </summary>
        public string Photo;
        /// <summary>
        /// 学历
        /// </summary>
        public int Education;
        /// <summary>
        /// 职业
        /// </summary>
        public int Occupation;
        /// <summary>
        /// 兴趣爱好
        /// </summary>
        public string Hobby;
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCard;
        /// <summary>
        /// 个性签名
        /// </summary>
        public string Signature;
        /// <summary>
        ///  状态
        /// </summary>
        public int Status;     
        /// <summary>
        ///  来源(1-Web  2-App 3-其他)
        /// </summary>
        public int Source;
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
        /// <summary>
        /// 令牌(每次登录返回信息不一样)
        /// </summary>
        public string Token;
    }
}