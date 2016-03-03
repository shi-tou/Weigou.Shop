using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Model.Enum;

namespace Weigou.Model
{
    public class PasswordInfo
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID;
        /// <summary>
        /// 类型 1-修改密码 2-重置密码
        /// </summary>
        public int Type;
        /// <summary>
        /// 原密码
        /// </summary>
        public string OldPassword;
        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPassword;
        /// <summary>
        /// 修改人
        /// </summary>
        public int UserID;
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileNo;

    }
}