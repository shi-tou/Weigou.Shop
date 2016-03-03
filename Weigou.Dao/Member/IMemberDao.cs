using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Common;
using System.Collections;
using System.Data;

namespace Weigou.Dao
{
    public interface IMemberDao : IBaseDao
    {
        #region 会员管理
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
        /// <param name="p"></param>
        /// <param name="hs"></param>
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
        /// 会员申请解冻列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetMemberUnlockList(Pager p, Hashtable hs);
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

        #region 会员收藏
        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        int GetFavoriteList(Pager p, Hashtable hs);
        #endregion

        #region 会员评论
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
        /// 获取默认收货地址
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        DataTable GetDeliverAddress(Hashtable hs);

        #endregion

        #region 购物车相关
        /// <summary>
        /// 获取购物车列表
        /// </summary>
        /// <param name="strMemberID">会员ID</param>
        /// <returns></returns>
        DataTable GetShoppingCartList(int strMemberID);
        #endregion

        #region 订单管理
        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetOrderList(Pager p, Hashtable hs);

        #endregion

        #region 会员级别
        /// <summary>
        /// 会员级别列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        int GetMemberLevelList(Pager p, Hashtable hs);
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
