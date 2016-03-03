using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Common;
using System.Collections;
using System.Data;
using Weigou.Model;
using Spring.Transaction.Interceptor;
using Weigou.Model.Enum;

namespace Weigou.Service
{
    public interface IMemberService : IBaseService
    {
        #region 会员管理
        /// <summary>
        /// 会员注册
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        int Register(MemberInfo info,string verifyCode);
        /// <summary>
        /// 会员注册获取验证码
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        int GetVerifyCodeForReg(string mobileNo);
        /// <summary>
        /// 修改密码获取验证码
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        int GetVerifyCodeForUpPwd(string mobileNo);
        /// <summary>
        /// 修改身份证号获取验证码
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        int GetVerifyCodeForUpCardNo(string mobileNo);
        /// <summary>
        /// 找回密码获取验证码
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        int GetVerifyCodeForFindPwd(string mobileNo);    
        /// <summary>
        /// 会员列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetMemberList(Pager p, Hashtable hs);
        /// <summary>
        /// 根据id获取会员
        /// </summary>
        /// <returns></returns>
        DataTable GetMemberInfo(int memberID);
        /// <summary>
        /// 根据Email或手机号获取会员
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        DataTable GetMemberInfo(string mobileOrEmail);
        /// <summary>
        /// 添加会员
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int AddMember(MemberInfo info);
        /// <summary>
        /// 更新会员信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns>0-成功 1-失败 </returns>
        int UpdateMember(MemberInfo info);
        /// <summary>
        /// 获取准驾车型信息
        /// </summary>
        /// <returns></returns>
        DataTable GetCarClassList();
        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mobileNo"></param>
        /// <returns>0- 邮箱可用 1-邮箱已注册</returns>
        int CheckEmail(int id, string email);
        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mobileNo"></param>
        /// <returns>0- 手机可用 1-手机已注册</returns>
        bool CheckMobile(int id, string mobileNo);
        /// <summary>
        /// 检查登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        int MemberLogin(string mobileOrEmail, string password, out MemberInfo info);
        /// <summary>
        /// 修改/重置密码
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="type"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        int ChangePassword(PasswordInfo info);
        /// <summary>
        ///  找回密码
        /// </summary>
        /// <param name="info"></param> 
        /// <returns></returns>
        int FindPassword(PasswordInfo info, string verifyCode);
        /// <summary>
        ///  修改备用手机号
        /// </summary>
        /// <param name="memberID"></param> 
        /// <returns></returns>
        int ReserveMemberNo(string memberID, string reserveMemberNo);
        /// <summary>
        /// 校验会员密码
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        int CheckMemberPwd(int memberID, string pwd);
        /// <summary>
        /// 会员申请解冻列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetMemberUnlockList(Pager p, Hashtable hs);
        /// <summary>
        /// 审核申请解冻会员
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        int AuditMemberUnlock(int memberID, int status, int approvedBy, string remark);
        #endregion

        #region 会员账户管理
        /// <summary>
        /// 会员账户余额
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roletype"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        decimal GetAccountBalance(int memberID);
        /// <summary>
        /// 获取会员账户明细
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetAccountList(Pager p, Hashtable hs);
        #endregion

        #region 会员收藏（商品/店铺）
        /// <summary>
        /// 添加商品/店铺收藏
        /// </summary>
        /// <param name="TargetID"></param>
        /// <param name="Type"></param>
        /// <param name="MemberID"></param>
        /// <returns></returns>
        int AddFavorite(string TargetID, string Type, int MemberID);
        /// <summary>
        /// 检验是否收藏
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="type"></param>
        /// <param name="targetID"></param>
        /// <returns></returns>
        bool IsFavorite(int memberID, EnumFavType type, string targetID);
        /// <summary>
        /// 收藏列表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int GetFavoriteList(Pager p, Hashtable hs);
        #endregion

        #region 会员评论
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <returns></returns>
        int AddComment(CommentInfo info);
        /// <summary>
        /// 会员评论
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetCommentsList(Pager p, Hashtable hs);
        /// <summary>
        /// 会员评论
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetGoodsCommentInfoByID(Pager p, Hashtable hs);
        #endregion

        #region 收货地址管理

        /// <summary>
        /// 获取收货地址列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetDeliverAddressList(Pager p, Hashtable hs);

        /// <summary>
        /// 保存收货地址
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int SaveDeliverAddress(DeliverAddressInfo info);

        /// 设置默认收货地址
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int SetDefaultAddress(int id, int memberID);

        /// <summary>
        /// 获取默认收货地址
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        DataTable GetDeliverAddress(Hashtable hs);

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        int DeleteDeliverAddress(int id);

        #endregion

        #region 购物车相关
        /// <summary>
        /// 添加购物车入口
        /// </summary>
        /// <param name="strGoodsID">商品ID</param>
        /// <param name="strMemberID">会员ID</param>
        /// <param name="strNumber">购买数量</param>
        /// <param name="strSaleProp">销售属性</param>
        /// <returns></returns>
        int AddShoppingCart(int goodsID, int memberID, int count);
        /// <summary>
        /// 修改购物车数量
        /// </summary>
        /// <param name="strMemberID"></param>
        /// <param name="strCartID"></param>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        int UpdateShoppingCart(int cartID, int count);
        /// <summary>
        /// 获取购物车列表
        /// </summary>
        /// <param name="strMemberID">会员ID</param>
        /// <returns></returns>
        DataTable GetShoppingCartList(int memberID);
        #endregion
        
        #region 会员级别
        /// <summary>
        /// 会员级别列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        int GetMemberLevelList(Pager p, Hashtable hs);
        /// <summary>
        /// 获取会员级别名
        /// </summary>
        /// <param name="LevelID"></param>
        /// <returns></returns>
        string GetLevelName(string LevelID);
        #endregion

        #region 环信IM管理
        /// <summary>
        /// 环信用户列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetEasemobUserList(Pager p, Hashtable hs);
        /// <summary>
        /// 获取环信群组
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetEasemobGroupList(Pager p, Hashtable hs);
        #endregion
    }
}
