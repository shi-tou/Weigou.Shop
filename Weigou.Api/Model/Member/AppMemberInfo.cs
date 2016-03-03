using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Api.Model
{
    [Serializable]
    public class AppMemberInfo
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
        public string Name = "";
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex;
        /// <summary>
        /// 手机
        /// </summary>
        public string MobileNo = "";
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email = "";
        /// <summary>
        /// 会员头像
        /// </summary>
        public string Photo = "";
        /// <summary>
        /// 租客认证状态(0-未认证 1-已认证)
        /// </summary>
        public int TenantAuthStatus;
        /// <summary>
        /// 车主认证状态(0-未认证 1-已认证)
        /// </summary>
        public int OwnerAuthStatus;
        /// <summary>
        /// 学历
        /// </summary>
        public int Education;
        public string EducationName = "";
        /// <summary>
        /// 职业
        /// </summary>
        public int Occupation;
        public string OccupationName = "";
        /// <summary>
        /// 兴趣爱好
        /// </summary>
        public string Hobby = "";
        /// <summary>
        /// 个性签名
        /// </summary>
        public string Signature = "";
        /// <summary>
        /// 令牌(每次登录返回信息不一样)
        /// </summary>
        public string Token = "";
    }
}