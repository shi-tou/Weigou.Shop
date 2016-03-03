using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Common;
using System.Collections;
using System.Data;

using Weigou.Model;

//using Weigou.Model.Goods;

namespace Weigou.Service
{
    public interface IGoodsService : IBaseService
    {
        #region 商品类别
        /// <summary>
        /// 商品类别
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetGoodsType(Pager p, Hashtable hs);
        /// <summary>
        /// 获取指定商品类别ID下面的所有商品分类ID
        /// </summary>
        /// <param name="typeID"></param>
        /// <param name="self">是否包含自己</param>
        /// <returns></returns>
        DataTable GetAllSubGoodsType(int typeID, bool self);
        /// <summary>
        /// 获取指定类别的所有父分类
        /// </summary>
        /// <param name="typeID"></param>
        /// <param name="self">是否包含自己</param>
        /// <returns></returns>
        DataTable GetAllParentGoodsType(int typeID, bool self);
        #endregion

        #region 商品图片
        /// <summary>
        /// 图片置顶
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="goodsID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        int SetPictureTop(int ID, string targetID, int Type);
        /// <summary>
        ///  图片列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        DataTable GetPictureList(string targetID, int type);
        #endregion

        #region 商品列表/详情
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetGoodsList(Pager p, Hashtable hs);
        /// <summary>
        /// 商品详细
        /// </summary>
        /// <param name="goodsID">商品ID</param>
        /// <returns></returns>
        DataTable GetGoodsDetails(int goodsID);
        #endregion

        #region 商品属性
        /// <summary>
        /// 商品属性
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetGoodsAttributeList(Pager p, Hashtable hs);
        /// <summary>
        /// 获取商品属性值
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetAttributeValueList(Pager p, Hashtable hs);
        /// <summary>
        /// 根据类别获取商品的属性名
        /// </summary>
        /// <returns></returns>
        DataTable GetGoodsAttribute(int goodsTypeID);
        /// <summary>
        /// 获取商品属性
        /// </summary>
        /// <param name="goodsID"></param>
        /// <returns></returns>
        List<GoodsAttrInfo> GetGoodsSalePropByGoodsID(int goodsID);
        /// <summary>
        /// 根据属性值ID获取商品属性及属性值
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        DataTable GetAttributeValueByValueID(int strID);

        string GetGoodsAttributeValueInfo(string arrSaleProp);
        #endregion

        #region 商品品牌
        /// <summary>
        /// 商品品牌
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetGoodsBrandList(Pager p, Hashtable hs);
        /// <summary>
        /// 获取指定商品类别的关联品牌
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        DataTable GetBrandByTypeID(string typeID);
        #endregion

        #region 商品评论
        /// <summary>
        /// 评论列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetGoodsCommentList(Pager p, Hashtable hs);/// <summary>
        /// 添加商品评论
        /// </summary>
        /// <param name="MemberDd"></param>
        /// <param name="GoodsID"></param>
        /// <param name="Star"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        int AddGoodsComment(Hashtable hs);
        /// <summary>
        /// 检查商品是否能够评论
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <returns></returns>
        int CheckGoodsComment(string GoodsID, string MemberID, string OrderID);
        /// <summary>
        /// 检查商品是否能够申请售后
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <returns></returns>
        int CheckGoodsSale(string GoodsID);
        /// <summary>
        /// 追加商品评论
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        int AppendGoodsComment(string ParentID, string Content);
        #endregion

        #region 商品库存、价格
        /// <summary>
        /// 根据商品ID获取价格及库存等值
        /// </summary>
        /// <param name="goodsID"></param>
        /// <returns></returns>
        string GetGoodsPriceByGoodsID(int goodsID);
        /// <summary>
        /// 根据销售属性及商品ID更新库存信息
        /// </summary>
        /// <param name="goodsID">商品ID</param>
        /// <param name="saleProp">销售属性值</param>
        /// <param name="count">更改数量（正为加，负为减）</param>
        /// <returns></returns>
        int UpdateGoodsStock(int goodsID, string saleProp, int count);
        /// <summary>
        /// 根据销售属性及商品ID查询库存信息
        /// </summary>
        /// <param name="strGoodsID">商品ID</param>
        /// <param name="strSaleProp">销售属性值</param>
        /// <returns></returns>
        DataTable GetGoodsStockAndPrice(int goodsID, string saleProp);
        /// <summary>
        /// 根据销售属性及商品ID查询库存
        /// </summary>
        /// <param name="goodsID">商品Id</param>
        /// <param name="saleProp">销售属性值</param>
        /// <returns></returns>
        int GetGoodsStock(int goodsID, string saleProp);
        /// <summary>
        /// 根据销售属性及商品ID查询价格
        /// </summary>
        /// <param name="goodsID">商品Id</param>
        /// <param name="saleProp">销售属性值</param>
        /// <returns></returns>
        decimal GetGoodsPrice(int goodsID, string saleProp);
        #endregion

        #region 购物车
        /// <summary>
        /// 获取购物车列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetShoppingCartList(Pager p, Hashtable hs);

        /// <summary>
        /// 添加购物车入口
        /// </summary>
        /// <param name="goodsID">商品ID</param>
        /// <param name="memberID">会员ID</param>
        /// <param name="count">购买数量</param>
        /// <param name="saleProp">销售属性</param>
        /// <returns></returns>
        int AddShoppingCart(int goodsID, int memberID, int count, string saleProp);
        /// <summary>
        /// 更新购物车
        /// </summary>
        /// <param name="strMemberID"></param>
        /// <param name="strCartID"></param>
        /// <param name="strNumber"></param>
        /// <returns></returns>
        int UpdateShoppingCart(int memberID, int cartID, int count);
        /// <summary>
        /// 删除购物车数据
        /// </summary>
        /// <param name="CartID">多个Id用‘,’拼接,如：1,2,3</param>
        /// <returns></returns>
        int DeleteShoppingCart(string CartID);
        #endregion

        #region 物流、快递
        /// <summary>
        /// 获取物流模板
        /// </summary>
        /// <returns></returns>
        DataTable GetMerchantLogistics(string MerchantID);
        /// <summary>
        /// 根据店铺ID和省份ID获取快递费用
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ProvinceID"></param>
        /// <returns></returns>
        DataTable GetLogistics(string ID, string ProvinceID);
        /// <summary>
        /// 根据店铺ID获取默认快递费用
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ProvinceID"></param>
        /// <returns></returns>
        DataTable GetDefaultLogistics(string ID);
        /// <summary>
        /// 根据店铺ID和收货地址ID获取快递费用
        /// </summary>
        /// <param name="AddressID"></param>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        string GetLogisticsForAddressID(string AddressID, string MerchantID);
        #endregion

    }
}
