using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Weigou.Model;
using Weigou.Dao;
using Weigou.Common;
using System.Collections;
using Spring.Transaction.Interceptor;
using Newtonsoft.Json;
using Weigou.Model.Goods;


namespace Weigou.Service
{
    public class GoodsService : BaseService, IGoodsService
    {
        private IGoodsDao goodsDao;
        public IGoodsDao GoodsDao
        {
            set
            {
                goodsDao = value;
            }
        }

        #region 商品类别
        /// <summary>
        /// 商品类别
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetGoodsType(Pager p, Hashtable hs)
        {
            return goodsDao.GetGoodsType(p, hs);
        }
       /// <summary>
        /// 获取指定商品类别ID下面的所有商品分类ID
        /// </summary>
        /// <param name="typeID"></param>
        /// <param name="self">是否包含自己</param>
        /// <returns></returns>
        public DataTable GetAllSubGoodsType(int typeID, bool self)
        {
            return goodsDao.GetAllSubGoodsType(typeID, self);
        }
        /// <summary>
        /// 获取指定类别的所有父分类
        /// </summary>
        /// <param name="typeID"></param>
        /// <param name="self">是否包含自己</param>
        /// <returns></returns>
        public DataTable GetAllParentGoodsType(int typeID, bool self)
        {
            return goodsDao.GetAllParentGoodsType(typeID, self);
        }
        #endregion

        #region 商品图片
        /// <summary>
        /// 图片置顶
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="goodsID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public int SetPictureTop(int ID, string targetID, int Type)
        {
            return goodsDao.SetPictureTop(ID, targetID, Type);
        }
        /// <summary>
        ///  图片列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable GetPictureList(string targetID, int type)
        {
            return goodsDao.GetPictureList(targetID, type);
        }
        #endregion

        #region 商品列表/详情
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetGoodsList(Pager p, Hashtable hs)
        {
            return goodsDao.GetGoodsList(p, hs);
        }
        /// <summary>
        /// 商品详细
        /// </summary>
        /// <param name="goodsID">商品ID</param>
        /// <returns></returns>
        public DataTable GetGoodsDetails(int goodsID)
        {
            return goodsDao.GetGoodsDetails(goodsID);
        }
        #endregion

        #region 商品库存、价格
        /// <summary>
        /// 根据商品ID获取价格及库存等值
        /// </summary>
        /// <param name="goodsID"></param>
        /// <returns></returns>
        public string GetGoodsPriceByGoodsID(int goodsID)
        {
            string strJson = "0";
            DataTable dt = goodsDao.GetDataByKey("T_GoodsSaleProp", "GoodsID", goodsID);
            if (dt.Rows.Count > 0)
            {
                strJson = "{";
                foreach (DataRow row in dt.Rows)
                {
                    strJson += "\"" + row["SaleProp"] + "\":{\"SalePrice\":" + row["SalePrice"] + ",\"MarketPrice\":" + row["MarketPrice"] + ",\"Quantity\":" + row["Quantity"] + "},";
                }
                if (dt.Rows.Count > 0)
                {
                    strJson = strJson.Substring(0, strJson.Length - 1);
                }
                strJson += "}";
            }

            return strJson;
        }
        /// <summary>
        /// 根据销售属性及商品ID更新库存信息
        /// </summary>
        /// <param name="goodsID">商品ID</param>
        /// <param name="saleProp">销售属性值</param>
        /// <param name="count">更改数量（正为加，负为减）</param>
        /// <returns></returns>
        public int UpdateGoodsStock(int goodsID, string saleProp, int count)
        {
            int res = 0;
            int stock = GetGoodsStock(goodsID, saleProp);
            //销售属性不为空，则更新属性库存在
            if (!string.IsNullOrEmpty(saleProp))
            {
                Hashtable hs = new Hashtable();
                hs.Add("GoodsID", goodsID);
                hs.Add("SaleProp", saleProp);
                DataTable dt = GetDataByWhere("T_GoodsSaleProp", hs);
                DataRow dr = dt.Rows[0];
                dr["Stock"] = stock + count;
                res = UpdateDataTable(dt);
            }
            else
            {
                DataTable dt = GetDataByKey("T_Goods", "ID", goodsID);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    dr["Stock"] = stock + count;
                    res = UpdateDataTable(dt);
                }
            }
            return res > 0 ? RT.SUCCESS : RT.FAILED;
        }
        /// <summary>
        /// 根据销售属性及商品ID查询库存、价格信息
        /// </summary>
        /// <param name="goodsID">商品ID</param>
        /// <param name="saleProp">销售属性值</param>
        /// <returns></returns>
        public DataTable GetGoodsStockAndPrice(int goodsID, string saleProp)
        {
            return goodsDao.GetGoodsStockAndPrice(goodsID, saleProp);
        }
        /// <summary>
        /// 根据销售属性及商品ID查询库存
        /// </summary>
        /// <param name="goodsID">商品Id</param>
        /// <param name="saleProp">销售属性值</param>
        /// <returns></returns>
        public int GetGoodsStock(int goodsID, string saleProp)
        {
            DataTable dt= GetGoodsStockAndPrice(goodsID,saleProp);
            return Convert.ToInt32(dt.Rows[0]["Stock"]);
        }
        /// <summary>
        /// 根据销售属性及商品ID查询价格
        /// </summary>
        /// <param name="goodsID">商品Id</param>
        /// <param name="saleProp">销售属性值</param>
        /// <returns></returns>
        public decimal GetGoodsPrice(int goodsID, string saleProp)
        {
            DataTable dt = GetGoodsStockAndPrice(goodsID, saleProp);
            return Convert.ToDecimal(dt.Rows[0]["SalePrice"]);
        }
        #endregion

        #region 商品属性
        /// <summary>
        /// 商品属性列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetGoodsAttributeList(Pager p, Hashtable hs)
        {
            return goodsDao.GetGoodsAttributeList(p, hs);
        }
        /// <summary>
        /// 获取商品属性值
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetAttributeValueList(Pager p, Hashtable hs)
        {
            return goodsDao.GetAttributeValueList(p, hs);
        }
        /// <summary>
        /// 根据商品类别获取商品属性
        /// </summary>
        /// <returns></returns>
        public DataTable GetGoodsAttribute(int goodsTypeID)
        {
            return goodsDao.GetGoodsAttribute(goodsTypeID);
        }
        /// <summary>
        /// 获取商品销售属性
        /// </summary>
        /// <param name="goodsID"></param>
        /// <returns></returns>
        public List<GoodsAttrInfo> GetGoodsSalePropByGoodsID(int goodsID)
        {
            List<GoodsAttrInfo> list = new List<GoodsAttrInfo>();
            //商品属性
            DataTable dtProp = GetDataByKey("T_GoodsSaleProp", "GoodsID", goodsID);
            //商品属性及属性值
            DataTable dtAttrValue = new DataTable();
            Hashtable hs = new Hashtable();

            foreach (DataRow dr in dtProp.Rows)
            {
                string[] arrProp = dr["SaleProp"].ToString().Split(':');
                for (int i = 0; i < arrProp.Length; i++)
                {
                    //查询销售属性
                    dtAttrValue = GetAttributeValueByValueID(Convert.ToInt32(arrProp[i]));
                    if (dtAttrValue.Rows.Count > 0)
                    {
                        //规则 属性名ID_属性名为key  属性值ID_属性值名多个,隔开
                        string strAttributeID = dtAttrValue.Rows[0]["ID"].ToString();
                        string strAttributeName = dtAttrValue.Rows[0]["Name"].ToString();

                        string strValueID = dtAttrValue.Rows[0]["ValueID"].ToString();
                        string strValueName = dtAttrValue.Rows[0]["Value"].ToString();
                        string strValueSort = dtAttrValue.Rows[0]["ValueSort"].ToString();

                        string strKeys = strAttributeID + "_" + strAttributeName;
                        string strValues = strValueID + "_" + strValueName + "_" + strValueSort;

                        //属性名作为Key
                        if (hs.ContainsKey(strKeys))
                        {
                            if (hs[strKeys].ToString().IndexOf(strValues) == -1)
                                hs[strKeys] = hs[strKeys] + "," + strValues;
                        }
                        else
                        {
                            hs.Add(strKeys, strValues);
                        }
                    }
                }
            }

            //接续hashtable，拼凑成json
            foreach (string key in hs.Keys)
            {
                GoodsAttrInfo attrInfo = new GoodsAttrInfo();
                if (key != "")
                {
                    string[] strKeys = key.Split('_');
                    string[] strValues = hs[key].ToString().Split(',');

                    attrInfo.AttributeID = strKeys[0];
                    attrInfo.AttributeName = strKeys[1];

                    for (int i = 0; i < strValues.Length; i++)
                    {
                        string[] strValueIDandName = strValues[i].Split('_');
                        AttrValueInfo vinfo = new AttrValueInfo();
                        vinfo.ValueID = strValueIDandName[0];
                        vinfo.ValueName = strValueIDandName[1];
                        vinfo.ValueSort = Convert.ToInt32(strValueIDandName[2]);
                        attrInfo.Values.Add(vinfo);
                    }
                    var query = from items in attrInfo.Values orderby items.ValueSort select items;
                    attrInfo.Values = query.ToList();
                    list.Add(attrInfo);
                }

            }
            return list;
        }

        /// <summary>
        /// 根据属性值ID获取商品属性及属性值
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public DataTable GetAttributeValueByValueID(int strID)
        {
            return goodsDao.GetAttributeValueByValueID(strID);
        }
        public string GetGoodsAttributeValueInfo(string saleProp)
        {
            if (string.IsNullOrEmpty(saleProp))
            {
                return "";
            }
            string result = "";
            foreach (string strprop in saleProp.Split(':'))
            {
                DataTable propdt = GetAttributeValueByValueID(Convert.ToInt32(strprop));
                if (propdt.Rows.Count > 0)
                {
                    result += propdt.Rows[0]["Alias"] + ":" + propdt.Rows[0]["Value"] + " ";
                }
            }
            return result;
        }
        #endregion

        #region 商品品牌
        /// <summary>
        /// 商品品牌
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetGoodsBrandList(Pager p, Hashtable hs)
        {
            return goodsDao.GetGoodsBrandList(p, hs);
        }
        /// <summary>
        /// 获取指定商品类别的关联品牌
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetBrandByTypeID(string typeID)
        {
            return goodsDao.GetBrandByTypeID(typeID);
        }
        #endregion

        #region 商品评论
        /// <summary>
        /// 评论列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetGoodsCommentList(Pager p, Hashtable hs)
        {
            return goodsDao.GetGoodsCommentList(p, hs);
        }       
        /// <summary>
        /// 添加商品评论
        /// </summary>
        /// <param name="MemberID"></param>
        /// <param name="GoodsID"></param>
        /// <param name="Star"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public int AddGoodsComment(Hashtable hs)
        {
            DataTable dt = GetDataByKey("T_Comment", "ID", 0);
            DataRow dr;
            int res = 0;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr["CreateTime"] = DateTime.Now;
                dr["ParentID"] = 0;
                dr["Type"] = 2; // Type=2表示为商品ID，Type=1表示为车辆ID
                dr["TargetID"] = hs["GoodsID"].ToString();
                dr["MemberID"] = hs["MemberID"].ToString();
                dr["OrderID"] = hs["OrderID"].ToString();
                dr["Star"] = hs["Star"].ToString();
                dr["Content"] = hs["Content"].ToString();
                dr["SaleProp"] = hs["SaleProp"].ToString();
                //dr["SurviceStar"] = hs["SurviceStar"].ToString();
                //dr["LogisticsStar"] = hs["LogisticsStar"].ToString();
                //dr["DescriptionStar"] = hs["DescriptionStar"].ToString();
                res = UpdateDataTable(dt);
            }
            return res;
        }
        /// <summary>
        /// 检查商品是否能够评论
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <returns></returns>
        public int CheckGoodsComment(string GoodsID, string MemberID, string OrderID)
        {
            int res = 0;
            Hashtable hs = new Hashtable();
            hs.Add("TargetID", GoodsID);
            hs.Add("MemberID", MemberID);
            hs.Add("OrderID", OrderID);

            DataTable dt = GetDataByWhere("T_Comment", hs);
            if (dt.Rows.Count == 0)
            {
                res = 1;
            }
            return res;
        }
        /// <summary>
        /// 检查商品是否能够申请售后
        /// </summary>
        /// <param name="GoodsID"></param>
        /// <returns></returns>
        public int CheckGoodsSale(string GoodsID)
        {
            int res = 1;
            //DataTable dt = GetDataByKey("T_Goods", "ID", GoodsID);
            //if (dt.Rows.Count > 0)
            //{
            //    DataRow dr = dt.Rows[0];
            //    //海外购
            //    if (Convert.ToInt32(dr["PurchaseQuantity"]) > 0)
            //    {
            //        if (!string.IsNullOrEmpty(dr["SupportSaleType"].ToString()))
            //        {
            //            res = 1;
            //        }
            //        else
            //        {
            //            res = 0;
            //        }
            //    }
            //    //国内都支持退换货
            //    else
            //    {
            //        res = 1;
            //    }
            //}
            return res;
        }
        /// <summary>
        /// 追加商品评论
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public int AppendGoodsComment(string ParentID, string Content)
        {
            DataTable dt = GetDataByKey("T_Comment", "ID", ParentID);
            DataRow dr;
            int res = 0;
            if (dt.Rows.Count > 0)
            {
                DataTable dt2 = GetDataByKey("T_Comment", "ID", 0);
                dr = dt2.NewRow();
                dt2.Rows.Add(dr);
                dr["CreateTime"] = DateTime.Now;
                dr["ParentID"] = ParentID;
                dr["TargetID"] = Convert.ToString(dt.Rows[0]["TargetID"]);
                dr["MemberID"] = Convert.ToString(dt.Rows[0]["MemberID"]);
                dr["OrderID"] = Convert.ToString(dt.Rows[0]["OrderID"]);
                dr["Star"] = Convert.ToString(dt.Rows[0]["Star"]);
                dr["Type"] = 2;
                dr["Content"] = Content;
                res = UpdateDataTable(dt2);
            }
            return res;
        }
        #endregion

        #region 购物车相关
        /// <summary>
        /// 添加购物车入口
        /// </summary>
        /// <param name="goodsID">商品ID</param>
        /// <param name="memberID">会员ID</param>
        /// <param name="count">购买数量</param>
        /// <param name="saleProp">销售属性</param>
        /// <returns></returns>
        [Transaction]
        public int AddShoppingCart(int goodsID, int memberID, int count, string saleProp)
        {
            DataTable dtGoods = GetDataByKey("T_Goods", "ID", goodsID);
            if (dtGoods.Rows.Count == 0)
            {
                return RT.RESULT_NOT_EXIST; //商品不存在
            }
            //检测库存是否够
            int stock = GetGoodsStock(goodsID, saleProp);
            if (count > stock)
            {
                //库存不足
                return RT.RESULT_STOCK_INSUFFICIENT;
            }
            DataTable dtCart = GetDataByKey("T_ShoppingCart", "ID", 0);
            DataRow drCart = dtCart.NewRow();
            drCart["MemberID"] = memberID;
            drCart["GoodsID"] = goodsID;
            drCart["Count"] = count;
            drCart["SaleProp"] = saleProp;
            drCart["CreateTime"] = DateTime.Now;
            dtCart.Rows.Add(drCart);
            goodsDao.UpdateDataTable(dtCart);
            return RT.SUCCESS;
        }

        /// <summary>
        /// 获取购物车列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetShoppingCartList(Pager p, Hashtable hs)
        {
            return goodsDao.GetShoppingCartList(p, hs);
        }

        /// <summary>
        /// 修改购物车数量
        /// </summary>
        /// <param name="memberID"></param>
        /// <param name="cartID"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [Transaction]
        public int UpdateShoppingCart(int memberID, int cartID, int count)
        {
            DataTable dt = GetDataByKey("T_ShoppingCart", "ID", cartID);
            if (dt.Rows.Count == 0)
                return RT.FAILED;
            DataRow dr = dt.Rows[0];
            //检测库存是否够
            int stock = GetGoodsStock(Convert.ToInt32(dr["GoodsID"]), Convert.ToString(dr["SaleProp"]));
            if (count > stock)
            {
                return RT.RESULT_STOCK_INSUFFICIENT;//库存不足
            }

            if (dt.Rows.Count > 0)
            {
                dt.Rows[0]["Count"] = count;
                UpdateDataTable(dt);
            }
            return RT.SUCCESS;
        }
        /// <summary>
        /// 删除购物车数据
        /// </summary>
        /// <param name="CartID">多个Id用‘,’拼接,如：1,2,3</param>
        /// <returns></returns>
        public int DeleteShoppingCart(string CartID)
        {
            foreach (string str in CartID.Split(','))
            {
                Delete("T_ShoppingCart", "ID", str);
            }
            return RT.SUCCESS;
        }
        #endregion

        #region 物流、快递
        /// <summary>
        /// 获取物流模板
        /// </summary>
        /// <returns></returns>
        public DataTable GetMerchantLogistics(string MerchantID)
        {
            return goodsDao.GetMerchantLogistics(MerchantID);
        }
        /// <summary>
        /// 根据店铺ID和省份ID获取快递费用
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ProvinceID"></param>
        /// <returns></returns>
        public DataTable GetLogistics(string ID, string ProvinceID)
        {
            return goodsDao.GetLogistics(ID, ProvinceID);
        }
        /// <summary>
        /// 根据店铺ID获取默认快递费用
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ProvinceID"></param>
        /// <returns></returns>
        public DataTable GetDefaultLogistics(string ID)
        {
            return goodsDao.GetDefaultLogistics(ID);
        }
        /// <summary>
        /// 根据店铺ID和收货地址ID获取快递费用
        /// </summary>
        /// <param name="AddressID"></param>
        /// <param name="MerchantID"></param>
        /// <returns></returns>
        public string GetLogisticsForAddressID(string AddressID, string MerchantID)
        {
            string result = "0";
            DataTable addressdt = GetDataByKey("T_DeliverAddress", "ID", AddressID);
            if (addressdt.Rows.Count > 0)
            {
                DataTable logisticsdt = GetLogistics(MerchantID, addressdt.Rows[0]["ProvinceID"].ToString());
                if (logisticsdt.Rows.Count > 0)
                {
                    result = logisticsdt.Rows[0]["LogisticsPrice"].ToString();
                }
                else
                {
                    DataTable defaultdt = GetDefaultLogistics(MerchantID);
                    if (defaultdt.Rows.Count > 0)
                    {
                        result = defaultdt.Rows[0]["LogisticsPrice"].ToString();
                    }
                }
            }
            else
            {
                DataTable defaultdt = GetDefaultLogistics(MerchantID);
                if (defaultdt.Rows.Count > 0)
                {
                    result = defaultdt.Rows[0]["LogisticsPrice"].ToString();
                }
            }
            return result;
        }
        #endregion
    }
}