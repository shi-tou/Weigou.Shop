using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Weigou.Common;
using Weigou.Model;
using Weigou.Model.Enum;

namespace Weigou.Weixin
{
    /// <summary>
    /// car 的摘要说明
    /// </summary>
    public class mem : BaseAjax, IHttpHandler
    {
        #region 会员管理
        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="hc"></param>
        public void ValidMobile(HttpContext hc)
        {
            string mobileNo = GetRequest("MobileNo", "");
            DataTable dt = memberService.GetDataByKey("T_Member", "MobileNo", mobileNo);
            dt = Utils.SelectDataTable(dt, "Status<>" + ((int)EnumStatus.Delete).ToString());
            int res = RT.FAILED;
            if (dt.Rows.Count > 0)
            {
                res = RT.RESULT_MOBILENO_EXIST;
            }
            ResponseWrite(hc, res.ToString());
        }
        /// <summary>
        /// 注册发送手机验证码
        /// </summary>
        public void AskCheckCodeForReg(HttpContext hc)
        {
            string mobileNo = GetRequest("MobileNo", "");
            int res = memberService.GetVerifyCodeForReg(mobileNo);
            ResponseWrite(hc, res.ToString());
        }
        /// <summary>
        /// 校验验证码
        /// </summary>
        public void CheckValidCode(HttpContext hc)
        {
            string mobileNo = GetRequest("MobileNo", "");
            string validCode = GetRequest("ValidCode", "");
            bool res = memberService.CheckVerifyCode(mobileNo, validCode);
            ResponseWrite(hc, res ? RT.SUCCESS.ToString() : RT.RESULT_ERROR_VERIFYCODE.ToString());
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="hc"></param>
        public void Register(HttpContext hc)
        {
            string mobileNo = GetRequest("MobileNo", "");
            string name = GetRequest("Name", "");
            string pwd = GetRequest("Password", "");
            string validCode = GetRequest("ValidCode", "");
            MemberInfo info = new MemberInfo();
            info.MobileNo = mobileNo;
            info.Password = pwd;
            info.Name = name;
            int res = memberService.Register(info, validCode);
            ResponseWrite(hc, res.ToString());
            //if (res == RT.SUCCESS)
            //{
            //    "恭喜您,注册成功！";
            //}
            //else if (res == RT.RESULT_ERROR_VERIFYCODE)
            //{
            //    "验证码不正确！";
            //}
            //else if (res == RT.RESULT_MOBILENO_EXIST)
            //{
            //    "手机号已被注册！";
            //}
            //else
            //{
            //    "注册失败";
            //}
        }
        #endregion
    }
}