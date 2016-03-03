using Weigou.Api.Base;
using Weigou.Api.Model;
using Weigou.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace Weigou.Api.SDK
{
    /// <summary>
    /// 购物车模块Api
    /// </summary>
    public class ShoppingCartsApi : BaseApi
    {
        #region 方法描述
        public const string GetShoppingCartList_Desc = "MemberID=会员ID&PagerSize=页大小&PagerIndex=页索引";
        public const string AddShoppingCart_Desc = "GoodsID=商品ID&MemberID=会员ID&Count=购买数量&SaleProp=商品属性";
        public const string UpdateShoppingCart_Desc = "CartID=购物车ID&Count=数量&MemberID=会员ID";
        public const string DeleteShoppingCart_Desc = "CartIDs=购物车ID(多个请用','拼接)";
        public const string DirectBuyGoods_Desc = "GoodsID=商品ID&MemberID=会员ID&Count=购买数量&SaleProp=商品属性";
        #endregion

        #region 购物车相关
        /// <summary>
        /// 获取购物车列表
        /// </summary>
        [Description(GetShoppingCartList_Desc)]
        public Result GetShoppingCartList(MyHashtable hs)
        {
            int memberID = GetParam(hs, "MemberID", 0);
            Pager p = GetPager(hs, "a.CreateTime desc ");
            Hashtable hsQuery = new Hashtable();
            hsQuery.Add("MemberID", memberID);
            goodsService.GetShoppingCartList(p, hsQuery);
            List<CartGoodsInfo> list = new List<CartGoodsInfo>();
            foreach (DataRow dr in p.DataSource.Rows)
            {
                CartGoodsInfo goodsinfo = new CartGoodsInfo();
                goodsinfo.CartID = Convert.ToInt32(dr["ID"]);
                goodsinfo.GoodsID = Convert.ToInt32(dr["GoodsID"]);
                goodsinfo.GoodsName = dr["GoodsName"].ToString();
                goodsinfo.Count = Convert.ToInt32(dr["Count"]);
                goodsinfo.SalePrice = dr["SalePrice"].ToString();
                goodsinfo.PropName = goodsService.GetGoodsAttributeValueInfo(dr["SaleProp"].ToString());
                goodsinfo.SmallPicture = GetServerPicture(dr["SmallPicture"].ToString());
                list.Add(goodsinfo);
            }
            result.data = list;
            result.total = p.ItemCount;
            result.status = RT.SUCCESS;
            return result;
        }
        /// <summary>
        /// 添加商品至购物车
        /// </summary>
        [Description(AddShoppingCart_Desc)]
        public Result AddShoppingCart(MyHashtable hs)
        {
            string goodsid = GetParam(hs, "GoodsID", "");
            string memberid = GetParam(hs, "MemberID", "");
            string number = GetParam(hs, "Count", "1");
            string saleprop = GetParam(hs, "SaleProp", "");
            result.status = goodsService.AddShoppingCart(Convert.ToInt32(goodsid), Convert.ToInt32(memberid), Convert.ToInt32(number), saleprop);
            if (result.status == RT.SUCCESS)
            {
                result.msg = "添加成功";
            }
            else if (result.status == RT.RESULT_STOCK_INSUFFICIENT)
            {
                result.msg = "库存不足";
            }
            else if (result.status == RT.RESULT_NOT_EXIST)
            {
                result.msg = "商品或者商品属性不存在";
            }
            return result;
        }       
        /// <summary>
        /// 更新购物车的商品数量
        /// </summary>
        [Description(UpdateShoppingCart_Desc)]
        public Result UpdateShoppingCart(MyHashtable hs)
        {
            int cartID = GetParam(hs, "CartID", 0);
            int memberID = GetParam(hs, "MemberID", 0);
            int count = GetParam(hs, "Count", 1);
            result.status = goodsService.UpdateShoppingCart(memberID, cartID, count);
            if (result.status == RT.SUCCESS)
            {
                Dictionary<string, decimal> dic = new Dictionary<string, decimal>();
                result.msg = "更新成功";
            }
            else if (result.status == RT.RESULT_STOCK_INSUFFICIENT)
            {
                result.msg = "库存不足";
            }
            return result;
        }

        /// <summary>
        /// 删除购物车的商品信息
        /// </summary>
        [Description(DeleteShoppingCart_Desc)]
        public Result DeleteShoppingCart(MyHashtable hs)
        {
            string cartIDs = GetParam(hs, "CartIDs", "");
            int res = goodsService.DeleteShoppingCart(cartIDs);
            if (res == RT.SUCCESS)
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
        #endregion
    }
}