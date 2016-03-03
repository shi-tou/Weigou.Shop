using Weigou.Common;
using Weigou.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Weigou.Api.Base;
using System.Xml.Serialization;
using System.ComponentModel;
using Spring.Context;
using Spring.Context.Support;
using Weigou.Service;
using Weigou.Api.Model;
using Weigou.Model.Enum;

namespace Weigou.Api.SDK
{
    public class MemberApi : BaseApi
    {
        #region 方法描述
        public const string Register_Desc = "MobileNo=手机号&Password=密码&VerifyCode=验证码";
        public const string Login_Desc = "MobileNo=手机号&Password=密码";
        public const string GetMemberInfo_Desc = "MemberID=会员ID";
        public const string GetVerifyCodeForReg_Desc = "MobileNo=手机号";
        public const string GetVerifyCodeForUpCardNo_Desc = "MobileNo=手机号";
        public const string GetVerifyCodeForFindPwd_Desc = "MobileNo=手机号";
        public const string GetVerifyCodeForUpPwd_Desc = "MobileNo=会员手机号";
        public const string CheckVerifyCode_Desc = "MobileNo=手机号&VerifyCode=验证码";
        public const string UpdatePwdByOldPwd_Desc = "MemberID=会员ID&OldPassword=旧密码&NewPassword=新密码";
        public const string UpdateMemberCardNo_Desc = "MemberID=会员ID&MobileNo=会员手机号&CardNo=身份证号码&VerifyCode=验证码";
        public const string UpdatePwdByMobile_Desc = "NewPassword=新密码&MobileNo=会员手机号&VerifyCode=验证码";
        public const string UpdateReserveMobileNo_Desc = "MemberID=会员ID&OldPassword=旧密码&NewPassword=新密码";
        //收藏
        public const string AddFavorite_Desc = "TargetID=商品ID/店铺ID/试衣间ID&MemberID=会员ID&Type=收藏类型(1-车辆 2-商品)";
        public const string DeleteFavorite_Desc = "ID=收藏ID";
        public const string GetFavoriteList_Desc = "MemberID=会员ID&Type=收藏类型(1-车辆 2-商品)&PagerSize=页大小&PagerIndex=页索引";
        //评论
        public const string AddComment_Desc = "MemberID=会员ID&OrderID=订单ID&Type=评论类别(1-车辆 2-商品)&Star=评分(1-5分)&Content=评论内容";
        public const string UpdateMemberInfo_Desc = "MemberID=会员ID&Name=姓名&Sex=性别(1-男 2-女)&Birthday=生日&ProvinceID=省&CityID=市&DistrictID=区&Education=学历&Occupation=职业&Hobby=兴趣爱好&Signature=个性签名&Photo=头像";
        public const string SaveDeliverAddress_Desc = "ID=收货地址记录ID(添加时赋0，修改时赋原ID)&MemberID=会员ID&ProvinceID=省&CityID=市&DistrictID=区&Address=街道地址&ZipCode=邮编&ConsigneeName=收货人&ConsigneeMobileNo=收货人手机&IsDefault=是否默认地址(0-否 1-是)";
        public const string GetDeliverAddress_Desc = "MemberID=会员ID";
        public const string SetDefaultAddress_Desc = "ID=收货地址记录ID&MemberID=会员ID";
        //会员账户
        public const string GetAccountBanlance_Desc = "MemberID=会员ID";
        public const string GetAccountList_Desc = "MemberID=会员ID&Type=类型(1-收入 2-支出)&PagerSize=页大小&PagerIndex=页索引&MinTime=最小时间&MaxTime=最大时间";
        //提交会员认证信息
        public const string SaveMemberInfoForAuth_Desc = "MemberID=会员ID&Name=姓名&IdCard=身份证&IssueDate=驾照初领日期&CarClass=准驾车型&LicenseNo=驾照档案编码";
        #endregion

        #region 会员注册/会员登录/会员信息
        /// <summary>
        /// 会员注册
        /// </summary>
        [Description(Register_Desc)]
        public Result Register(MyHashtable hs)
        {
            string verifyCode = GetParam(hs, "VerifyCode", "");
            string mobileNo = GetParam(hs, "MobileNo", "");
            string password = GetParam(hs, "Password", "");
            MemberInfo info = new MemberInfo();
            info.MobileNo = mobileNo;
            info.Password = password;
            result.status = memberService.Register(info, verifyCode);
            if (result.status == RT.SUCCESS)
            {
                result.msg = "恭喜您,注册成功！";
            }
            else if (result.status == RT.RESULT_ERROR_VERIFYCODE)
            {
                result.msg = "验证码不正确！";
            }
            else if (result.status == RT.RESULT_MOBILENO_EXIST)
            {
                result.msg = "手机号已被注册！";
            }
            else
            {
                result.msg = "注册失败";
            }
            return result;
        }
        /// <summary>
        /// 会员注册获取验证码
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(GetVerifyCodeForReg_Desc)]
        public Result GetVerifyCodeForReg(MyHashtable hs)
        {
            string mobileNo = GetParam(hs, "MobileNo", "");
            if (string.IsNullOrEmpty(mobileNo))
            {
                result.status = RT.FAILED;
                result.msg = "手机号不能为空";
                return result;
            }
            //获取验证码
            result.status = memberService.GetVerifyCodeForReg(mobileNo);
            if (result.status == RT.RESULT_MOBILENO_EXIST)
            {
                result.msg = "手机号已被注册！";
            }
            else
            {
                result.msg = "验证码已发送至您的手机！";
            }
            return result;
        }
        /// <summary>
        /// 会员修改密码获取验证码
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(GetVerifyCodeForUpPwd_Desc)]
        public Result GetVerifyCodeForUpPwd(MyHashtable hs)
        {
            string mobileNo = GetParam(hs, "MobileNo", "");
            if (string.IsNullOrEmpty(mobileNo))
            {
                result.status = RT.FAILED;
                result.msg = "手机号不能为空";
                return result;
            }
            //获取验证码
            result.status = memberService.GetVerifyCodeForUpPwd(mobileNo);
            if (result.status != RT.RESULT_MOBILENO_EXIST)
            {
                result.msg = "验证码已发送至您的手机！";
            }
            return result;
        }
        /// <summary>
        /// 会员修改身份证号获取验证码
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(GetVerifyCodeForUpCardNo_Desc)]
        public Result GetVerifyCodeForUpCardNo(MyHashtable hs)
        {
            string mobileNo = GetParam(hs, "MobileNo", "");
            if (string.IsNullOrEmpty(mobileNo))
            {
                result.status = RT.FAILED;
                result.msg = "手机号不能为空";
                return result;
            }
            //获取验证码
            result.status = memberService.GetVerifyCodeForUpCardNo(mobileNo);
            if (result.status != RT.RESULT_MOBILENO_EXIST)
            {
                result.msg = "验证码已发送至您的手机！";
            }
            return result;
        }
        /// <summary>
        /// 会员找回密码获取验证码
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(GetVerifyCodeForFindPwd_Desc)]
        public Result GetVerifyCodeForFindPwd(MyHashtable hs)
        {
            string mobileNo = GetParam(hs, "MobileNo", "");
            if (string.IsNullOrEmpty(mobileNo))
            {
                result.status = RT.FAILED;
                result.msg = "手机号不能为空";
                return result;
            }
            //获取验证码
            result.status = memberService.GetVerifyCodeForFindPwd(mobileNo);
            if (result.status == RT.RESULT_MOBILENO_EXIST)
            {
                result.msg = "手机号不存在！";
            }
            else
            {
                result.msg = "验证码已发送至您的手机！";
            }
            return result;
        }
        /// <summary>
        /// 检查验证码(默认30分钟内有效)
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(CheckVerifyCode_Desc)]
        public Result CheckVerifyCode(MyHashtable hs)
        {
            string mobileNo = GetParam(hs, "MobileNo", "");
            string verifyCode = GetParam(hs, "VerifyCode", "");
            if (string.IsNullOrEmpty(mobileNo) || string.IsNullOrEmpty(verifyCode))
            {
                result.status = RT.FAILED;
                result.msg = "手机号和验证码不能为空";
                return result;
            }
            //检查验证码
            result.status = memberService.CheckVerifyCode(mobileNo, verifyCode) ? RT.SUCCESS : RT.RESULT_ERROR_VERIFYCODE;
            if (result.status == RT.RESULT_ERROR_VERIFYCODE)
            {
                result.msg = "验证码不正确或已过期！";
            }
            else
            {
                result.msg = "验证通过！";
            }
            return result;
        }

        /// <summary>
        /// 会员登录
        /// </summary>
        [Description(Login_Desc)]
        public Result Login(MyHashtable hs)
        {
            string mobileNo = GetParam(hs, "MobileNo", "");
            string password = GetParam(hs, "Password", "");
            MemberInfo info;
            result.status = memberService.MemberLogin(mobileNo, password, out info);
            if (result.status == RT.SUCCESS)
            {
                AppMemberInfo appInfo = new AppMemberInfo();
                ObjectHelper.Copy(appInfo, info);
                //会员头像
                appInfo.Photo = GetServerPicture(appInfo.Photo);
                appInfo.Token = Utils.CreateToken();
                result.data = appInfo;
                DataTable dtToken = sysService.GetDataByKey("T_AppToken", "MemberID", info.ID);
                if (dtToken.Rows.Count == 0)
                {
                    dtToken.Rows.Add(dtToken.NewRow());
                }
                dtToken.Rows[0]["MemberID"] = appInfo.ID;
                dtToken.Rows[0]["Token"] = appInfo.Token;
                dtToken.Rows[0]["RefreshTime"] = DateTime.Now;
                sysService.UpdateDataTable(dtToken);

            }
            else if (result.status == RT.RESULT_NOT_EXIST)
            {
                result.msg = "用户不存在！";
            }
            else if (result.status == RT.RESULT_ERROR_PASSWORD)
            {
                result.msg = "密码不正确！";
            }
            else if (result.status == RT.RESULT_LOCK)
            {
                result.msg = "用户已被冻结！";
            }
            return result;
        }
        /// <summary>
        /// 获取会员资料
        /// </summary>
        /// <param name="hs"></param>
        [Description(GetMemberInfo_Desc)]
        public Result GetMemberInfo(MyHashtable hs)
        {
            int memberID = GetParam(hs, "MemberID", 0);
            result = new Result();
            DataTable dt = memberService.GetMemberInfo(memberID);
            if (dt.Rows.Count > 0)
            {
                AppMemberInfo info = ObjectHelper.CopyToObject<AppMemberInfo>(dt.Rows[0]);
                info.Photo = GetServerPicture(Convert.ToString(info.Photo));
                result.status = RT.SUCCESS;
                result.data = info;
            }
            else
            {
                result.msg = "没有找到会员";
            }
            return result;
        }
        /// <summary>
        /// 修改会员资料
        /// </summary>
        /// <param name="hs"></param>
        [Description(UpdateMemberInfo_Desc)]
        public Result UpdateMemberInfo(MyHashtable hs)
        {
            int memberID = GetParam(hs, "MemberID", 0);
            hs.Add("ID", memberID);
            MemberInfo info = HasToObject<MemberInfo>(hs);
            #region
            //string name = GetParam(hs, "Name", "");
            //string mobileNo = GetParam(hs, "MobileNo", "");
            //string email = GetParam(hs, "Email", "");
            //string password = GetParam(hs, "Password", "");
            //int sex = GetParam(hs, "Sex", 0);
            //string birthday = GetParam(hs, "Birthday", "");
            //int provinceID = GetParam(hs, "ProvinceID", 0);
            //int cityID = GetParam(hs, "CityID", 0);
            //int districtID = GetParam(hs, "DistrictID", 0);
            //int education = GetParam(hs, "Education", 0);
            //int occupation = GetParam(hs, "Occupation", 0);
            //string Hobby = GetParam(hs, "Hobby", "");
            //string idCard = GetParam(hs, "IdCard", "");
            //string IssueDate = GetParam(hs, "IssueDate", "");
            //string CarClass = GetParam(hs, "CarClass", "");
            //string LicenseNo = GetParam(hs, "LicenseNo", "");
            //string Signature = GetParam(hs, "Signature", "");
            #endregion
            if (memberService.UpdateMember(info) == RT.SUCCESS)
            {
                result.status = RT.SUCCESS;
                result.msg = "修改成功";
                return result;
            }
            else
            {
                result.msg = "修改失败";
                return result;
            }
        }
        /// <summary>
        /// 修改会员密码（通过旧密码）
        /// </summary>
        /// <param name="hs"></param>
        [Description(UpdatePwdByOldPwd_Desc)]
        public Result UpdatePwdByOldPwd(MyHashtable hs)
        {
            //string verifyCode = GetParam(hs, "VerifyCode", ""); 
            string oldpwd = GetParam(hs, "OldPwd", "");
            string newpwd = GetParam(hs, "NewPwd", "");
            string memberid = GetParam(hs, "ID", "");
            PasswordInfo info = new PasswordInfo();
            info.OldPassword = oldpwd;
            info.NewPassword = newpwd;
            info.MemberID = Convert.ToInt32(memberid);
            info.Type = 1;
            result.status = memberService.ChangePassword(info);
            if (result.status == RT.SUCCESS)
            {
                result.msg = "success";
            }
            else if (result.status == RT.RESULT_ERROR_PASSWORD)
            {
                result.msg = "旧密码错误";
            }
            else
            {
                result.msg = "failed";
            }
            return result;
        }

        /// <summary>
        /// 找回会员密码
        /// </summary>
        /// <param name="hs"></param>
        [Description(UpdatePwdByMobile_Desc)]
        public Result UpdatePwdByMobile(MyHashtable hs)
        {
            string verifyCode = GetParam(hs, "VerifyCode", "");
            string mobileNo = GetParam(hs, "MobileNo", "");
            string newpwd = GetParam(hs, "NewPassword", "");
            PasswordInfo info = new PasswordInfo();
            info.NewPassword = newpwd;
            info.MobileNo = mobileNo;
            result.status = memberService.FindPassword(info, verifyCode);
            if (result.status == RT.SUCCESS)
            {
                result.msg = "success";
            }
            else
            {
                result.msg = "failed";
            }
            return result;
        }

        /// <summary>
        /// 修改备用手机号
        /// </summary>
        /// <param name="hs"></param>
        [Description(UpdateReserveMobileNo_Desc)]
        public Result UpdateReserveMobileNo(MyHashtable hs)
        {
            string memberID = GetParam(hs, "ID", "");
            string reserveMobileNo = GetParam(hs, "ReserveMobileNo", "");
            result.status = memberService.ReserveMemberNo(memberID, reserveMobileNo);
            if (result.status == RT.SUCCESS)
            {
                result.msg = "success";
            }
            else
            {
                result.msg = "failed";
            }
            return result;
        }
        #endregion

        #region 会员认证信息
        /// <summary>
        /// 提交会员认证信息
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(SaveMemberInfoForAuth_Desc)]
        public Result SaveMemberInfoForAuth(MyHashtable hs)
        {
            int memberID = GetParam(hs, "MemberID", 0);
            string name = GetParam(hs, "Name", "");
            string idCard = GetParam(hs, "IdCard", "");
            string issueDate = GetParam(hs, "IssueDate", "");
            string carClass = GetParam(hs, "CarClass", "");
            string licenseNo = GetParam(hs, "LicenseNo", "");
            if (memberID > 0)
            {
                MemberInfo info = new MemberInfo();
                info.ID = memberID;
                info.Name = name;
                info.IdCard = idCard;

                result.status = memberService.UpdateMember(info);
                if (result.status == RT.SUCCESS)
                    result.msg = "更新成功！";
                else
                    result.msg = "更新失败！";
            }
            else
            {
                result.status = RT.RESULT_NOT_EXIST;
                result.msg = "会员不存在！";
            }
            return result;
        }


        #endregion

        #region 账户相关
        /// <summary>
        /// 根据会员ID获取账户余额
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(GetAccountBanlance_Desc)]
        public Result GetAccountBanlance(MyHashtable hs)
        {
            int memberID = GetParam(hs, "MemberID", 0);
            AppBalanceInfo info = new AppBalanceInfo();

            if (memberID > 0)
            {
                info.AccountBalnace = memberService.GetAccountBalance(memberID).ToString();
                result.status = RT.SUCCESS;
                result.data = info;
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "获取失败";
            }
            return result;

        }
        /// <summary>
        /// 根据会员ID和账户来源类型获取账户信息
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(GetAccountList_Desc)]
        public Result GetAccountList(MyHashtable hs)
        {
            int memberID = GetParam(hs, "MemberID", 0);
            int type = GetParam(hs, "Type", 0);
            string minTime = GetParam(hs, "MinTime", "");
            string maxTime = GetParam(hs, "MaxTime", "");
            Hashtable hsQuery = new Hashtable();
            hsQuery.Add("MemberID", memberID);
            if (type > 0)
            {
                hsQuery.Add("Type", type);
            }
            if (!string.IsNullOrEmpty(minTime))
            {
                hsQuery.Add("MinTime", minTime);
            }
            if (!string.IsNullOrEmpty(maxTime))
            {
                hsQuery.Add("MaxTime", maxTime);
            }
            Pager p = GetPager(hs, "a.CreateTime desc ");
            result.status = memberService.GetAccountList(p, hsQuery);
            List<AccountInfo> accountList = ObjectHelper.CopyToObjects<AccountInfo>(p.DataSource).ToList<AccountInfo>();
            result.data = accountList;
            result.total = p.ItemCount;
            return result;
        }
        #endregion

        #region 收藏/评论相关
        /// <summary>
        /// 添加商品/店铺/试衣间收藏
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(AddFavorite_Desc)]
        public Result AddFavorite(MyHashtable hs)
        {
            string targetid = GetParam(hs, "TargetID", "");
            int memberid = GetParam(hs, "MemberID", 0);
            string type = GetParam(hs, "Type", "");
            result.status = memberService.AddFavorite(targetid, type, memberid);
            if (result.status != RT.RESULT_EXIST)
            {
                result.status = RT.SUCCESS;
                result.msg = "收藏成功";
            }
            else
            {
                result.msg = "已收藏";
            }
            return result;
        }
        /// <summary>
        /// 根据会员ID获取会员收藏
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(GetFavoriteList_Desc)]
        public Result GetFavoriteList(MyHashtable hs)
        {
            int memberID = GetParam(hs, "MemberID", 0);
            int type = GetParam(hs, "Type", 0);
            Hashtable hsQuery = new Hashtable();
            hsQuery.Add("MemberID", memberID);
            hsQuery.Add("Type", type);
            Pager p = GetPager(hs, "a.CreateTime desc ");
            result.status = memberService.GetFavoriteList(p, hsQuery);
            foreach (DataRow dr in p.DataSource.Rows)
            {
                dr["Picture"] = GetServerPicture(Convert.ToString(dr["Picture"]));
            }
            result.data = p.DataSource;
            result.total = p.ItemCount;
            return result;
        }

        /// <summary>
        /// 根据收藏ID删除会员收藏
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(DeleteFavorite_Desc)]
        public Result DeleteFavorite(MyHashtable hs)
        {
            int ID = GetParam(hs, "ID", 0);
            result.status = memberService.Delete("T_Favorite", "ID", ID);
            if (result.status > RT.SUCCESS)
            {
                result.status = RT.SUCCESS;
                result.msg = "删除成功";
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "删除失败";
            }
            return result;
        }
        /// <summary>
        /// 添加评论（车辆/商品）
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(AddComment_Desc)]
        public Result AddGoodsComment(MyHashtable hs)
        {
            Hashtable queryhs = new Hashtable();
            int memberID = GetParam(hs, "MemberID", 0);
            int type = GetParam(hs, "Type", 1);
            int orderID = GetParam(hs, "OrderID", 0);
            int targetID = GetParam(hs, "TargetID", 0);
            int star = GetParam(hs, "Star", 5);
            string content = GetParam(hs, "Content", "");
            CommentInfo info = new CommentInfo();
            info.MemberID = memberID;
            info.Type = type;
            info.OrderID = orderID;
            info.ParentID = 0;
            info.TargetID = targetID;
            info.Star = star;
            result.status = memberService.AddComment(info);
            if (result.status > 0)
            {
                result.status = RT.SUCCESS;
                result.msg = "success";
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "failed";
            }
            return result;
        }
        #endregion

        #region 收货地址

        /// <summary>
        /// 添加/修改收货地址
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(SaveDeliverAddress_Desc)]
        public Result SaveDeliverAddress(MyHashtable hs)
        {
            DeliverAddressInfo info = new DeliverAddressInfo();
            info.ID = GetParam(hs, "ID", 0);
            info.MemberID = GetParam(hs, "MemberID", 0);
            info.ProvinceID = GetParam(hs, "ProvinceID", 0);
            info.CityID = GetParam(hs, "CityID", 0);
            info.DistrictID = GetParam(hs, "DistrictID", 0);
            info.Address = GetParam(hs, "Address", "");
            info.ZipCode = GetParam(hs, "ZipCode", "");
            info.ConsigneeName = GetParam(hs, "ConsigneeName", "");
            info.ConsigneeMobileNo = GetParam(hs, "ConsigneeMobileNo", "");
            info.IsDefault = GetParam(hs, "IsDefault", 0);
            if (memberService.SaveDeliverAddress(info) == RT.SUCCESS)
            {
                result.status = RT.SUCCESS;
                result.msg = "保存成功";
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "保存失败";
            }
            return result;
        }

        /// <summary>
        /// 添加/修改收货地址
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(SetDefaultAddress_Desc)]
        public Result SetDefaultAddress(MyHashtable hs)
        {
            int id = GetParam(hs, "ID", 0);
            int memberID = GetParam(hs, "MemberID", 0);
            if (memberService.SetDefaultAddress(id, memberID) == RT.SUCCESS)
            {
                result.status = RT.SUCCESS;
                result.msg = "设置成功";
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "设置失败";
            }
            return result;
        }

        /// <summary>
        ///  获取会员收货地址
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(GetDeliverAddress_Desc)]
        public Result GetDeliverAddress(MyHashtable hs)
        {
            int memberID = GetParam(hs, "MemberID", 0);
            if (memberID == 0)
            {
                result.msg = "找不到指定ID的会员";
                return result;
            }
            Pager p = new Pager(999, 1, "a.IsDefault desc,a.ID desc");
            Hashtable hsTemp = new Hashtable();
            hsTemp.Add("MemberID", memberID);
            memberService.GetDeliverAddressList(p, hsTemp);
            result.status = RT.SUCCESS;
            result.data = p.DataSource;
            return result;
        }

        /// <summary>
        ///  获取收货表ID获取收货信息
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description("ID=收货地址表ID")]
        public Result GetDeliverAddressInfo(MyHashtable hs)
        {
            int iId = GetParam(hs, "ID", 0);
            if (iId == 0)
            {
                result.msg = "找不到指定ID的收货地址";
                return result;
            }
            Pager p = new Pager(999, 1, "a.IsDefault desc,a.ID desc");
            Hashtable hsTemp = new Hashtable();
            hsTemp.Add("ID", iId);
            memberService.GetDeliverAddressList(p, hsTemp);
            result.status = RT.SUCCESS;
            result.data = p.DataSource;
            result.total = p.ItemCount;
            return result;
        }

        /// <summary>
        ///  获取默认收货地址
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description("MemberID=会员ID")]
        public Result GetDefaultAddress(MyHashtable hs)
        {
            int memberID = GetParam(hs, "MemberID", 0);
            if (memberID == 0)
            {
                result.msg = "找不到指定ID的会员";
                return result;
            }
            Hashtable hsAddr = new Hashtable();
            hsAddr.Add("IsDefault", 1);
            hsAddr.Add("MemberID", memberID);
            DataTable dt = memberService.GetDeliverAddress(hsAddr);
            result.status = RT.SUCCESS;
            result.data = dt;
            result.total = dt.Rows.Count;
            return result;
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description("ID=收货地址记录ID")]
        public Result DeleteDeliverAddress(MyHashtable hs)
        {
            int id = GetParam(hs, "ID", 0);
            if (memberService.DeleteDeliverAddress(id) == RT.SUCCESS)
            {
                result.status = RT.SUCCESS;
                result.msg = "删除成功！";
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "删除失败！";
            }
            return result;
        }

        #endregion
       
    }
}