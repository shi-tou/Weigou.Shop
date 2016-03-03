using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Collections;
using System.Data;
using Weigou.Api.Base;
using Weigou.Common;
using Weigou.Api.Model;
using Weigou.Model.Enum;
using Weigou.Model;

namespace Weigou.Api.SDK
{
    /// <summary>
    /// 商品模块Api
    /// </summary>
    public class GoodsApi : BaseApi
    {
        #region 方法描述
        public const string GetGoodsTypeList_Desc = "ParentID=父类别节点ID(-1：查询所有  0-一级类别)";
        public const string GetGoodsList_Desc = "PagerSize=页大小&PagerIndex=页索引&GoodsName=商品名称TypeID=类别ID&BrandID=品牌ID&MinPrice=最小价格&MaxPrice=最大价格&SortType=排序类型（1-价格升序 2-价格降序 3-人气升序 4-人气降序 5-上架时间降序）&PicWidth=图片宽度&PicHeigth=图片高度";
        public const string GetGoodsDetail_Desc = "GoodsID=商品ID";
        public const string GetGoodsStockAndPriceByProp_Desc = "ID=商品ID&SaleProp=商品属性ID(多个请用冒号连接,例如 3:21:24)";
        public const string GetLogistics_Desc = "ID=店铺ID&ProvinceID=省份ID";
        public const string GetGoodsCommentInfoByID_Desc = "GoodsID=商品ID&PagerSize=页大小&PagerIndex=页索引";
        public const string AddGoodsComment_Desc = "MemberID=会员ID&OrderID=订单详情表ID&Star=商品评分&Content=评论内容";
        public const string AppendGoodsComment_Desc = "ParentID=首次评论ID&Content=评论内容"; 

        #endregion

        #region 商品分类、品牌
        /// <summary>
        /// 获取商品分类列表
        /// </summary>
        [Description(GetGoodsTypeList_Desc)]
        public Result GetGoodsTypeList(MyHashtable hs)
        {
            int pid = GetParam(hs, "ParentID", -1);
            Hashtable hsTemp = new Hashtable();
            if (pid != -1)
            {
                hsTemp.Add("ParentID", pid);
            }
            Pager p = new Pager(999, 1, "ID desc");
            goodsService.GetGoodsType(p, hsTemp);
            result.status = RT.SUCCESS;
            result.data = p.DataSource;
            result.total = p.ItemCount;
            return result;
        }
        /// <summary>
        /// 获取商品品牌列表
        /// </summary>
        public Result GetBrandList(MyHashtable hs)
        {
            DataTable dt = goodsService.GetData("T_Brand");
            result.data = dt;
            result.status = RT.SUCCESS;
            return result;
        }
        #endregion

        #region 商品列表/详情
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(GetGoodsList_Desc)]
        public Result GetGoodsList(MyHashtable hs)
        {
            string goodsName = GetParam(hs, "GoodsName", "");
            int typeID = GetParam(hs, "TypeID", 0);
            int brandID = GetParam(hs, "BrandID", 0);
            string minPrice = GetParam(hs, "MinPrice", "");
            string maxPrice = GetParam(hs, "MaxPrice", "");
            int sortType = GetParam(hs, "SortType", 0);
            int picWidth = GetParam(hs, "PicWidth", 150);
            int picHeigth = GetParam(hs, "PicHeigth", 150);
            Hashtable hsQuery = new Hashtable();


            hsQuery.Add("Status", 1);//APP前端显示商品必须为已审核通过
            hsQuery.Add("ShelvesStatus", 1);//APP前端显示商品必须为已上架

            if (!string.IsNullOrEmpty(goodsName))
            {
                hsQuery.Add("Name", goodsName);
            }
            if (typeID > 0)
            {
                hsQuery.Add("Type", typeID);
            }
            if (brandID > 0)
            {
                hsQuery.Add("BrandID", brandID);
            }
            if (!string.IsNullOrEmpty(minPrice))
            {
                hsQuery.Add("MinPrice", minPrice);
            }
            if (!string.IsNullOrEmpty(maxPrice))
            {
                hsQuery.Add("MaxPrice", maxPrice);
            }
            Pager p = GetPager(hs, "a.CreateTime desc");
            if (sortType == 1)
            {
                p.OrderKey = "a.SalePrice asc";
            }
            else if (sortType == 2)
            {
                p.OrderKey = "a.SalePrice desc";
            }
            else if (sortType == 3)
            {
                p.OrderKey = "a.ShelvesTime desc";
            }
            result.status = goodsService.GetGoodsList(p, hsQuery);
            List<AppGoodsInfo> list = new List<AppGoodsInfo>();
            foreach (DataRow dr in p.DataSource.Rows)
            {
                AppGoodsInfo info = new AppGoodsInfo();
                info.ID = Convert.ToInt32(dr["ID"]);
                info.Name = Convert.ToString(dr["Name"]);
                info.SalePrice = Convert.ToString(dr["SalePrice"]);
                info.MarketPrice = Convert.ToString(dr["MarketPrice"]);
                info.Stock = Convert.ToInt32(dr["Stock"]);
                info.SimpleDesc = Convert.ToString(dr["SimpleDesc"]);
                info.SmallPicture = GetServerPicture(ImageHelper.GetThumbnail(Convert.ToString(dr["SmallPicture"]), picWidth, picHeigth));
                list.Add(info);
            }
            result.data = list;
            result.total = p.ItemCount;
            return result;
        }
        /// <summary>
        /// 获取商品详情信息
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(GetGoodsDetail_Desc)]
        public Result GetGoodsDetail(MyHashtable hs)
        {
            int goodsID = GetParam(hs, "GoodsID", 0);
            DataTable dt = goodsService.GetDataByKey("T_Goods", "ID", goodsID);
            if (dt.Rows.Count == 0)
            {
                result.msg = "找不到指定ID的商品";
                result.status = RT.FAILED;
                return result;
            }
            DataRow dr = dt.Rows[0];
            AppGoodsDetailInfo info = new AppGoodsDetailInfo();
            info.ID = Convert.ToInt32(dr["ID"]);
            info.Name = dr["Name"].ToString();
            info.SalePrice = dr["SalePrice"].ToString();
            info.MarketPrice = dr["MarketPrice"].ToString();
            info.Stock = Convert.ToInt32(dr["Stock"]);
            info.SimpleDesc = dr["SimpleDesc"].ToString();
            info.Description = dr["Description"].ToString();
            info.Picture = new List<PictureInfo>();
            DataTable dtPic = goodsService.GetPictureList(info.ID.ToString(), (int)EnumPicture.Goods);
            foreach (DataRow drPic in dtPic.Rows)
            {
                PictureInfo p = new PictureInfo();
                p.SmallPicture = GetServerPicture(Convert.ToString(drPic["SmallPicture"]));
                p.LargePicture = GetServerPicture(Convert.ToString(drPic["LargePicture"]));
                p.IsTop = Convert.ToInt32(drPic["IsTop"]);
                info.Picture.Add(p);
            }
            info.SalePropList = goodsService.GetGoodsSalePropByGoodsID(Convert.ToInt32(goodsID));
            result.status = RT.SUCCESS;
            result.data = info;
            return result;
        }
        #endregion

        #region 商品属性对应价格
        /// <summary>
        /// 根据属性获取商品对应的库存和价格
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(GetGoodsStockAndPriceByProp_Desc)]
        public Result GetGoodsStockAndPriceByProp(MyHashtable hs)
        {
            int goodsID = GetParam(hs, "GoodsID", 0);
            string saleProp = GetParam(hs, "SaleProp", "");
            DataTable dtStockPrice = goodsService.GetGoodsStockAndPrice(goodsID, saleProp);
            int stock = Convert.ToInt32(dtStockPrice.Rows[0]["Stock"]);
            string price = Convert.ToString(dtStockPrice.Rows[0]["SalePrice"]);
            StockAndPrice info = new StockAndPrice();
            info.Price = price;
            info.Stock = stock;
            result.data = info;
            result.status = RT.SUCCESS;
            return result;
        }
        /// <summary>
        /// 获取快递费用
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(GetLogistics_Desc)]
        public Result GetLogistics(MyHashtable hs)
        {
            string id = GetParam(hs, "ID", "");
            string provinceid = GetParam(hs, "ProvinceID", "");
            DataTable dt = goodsService.GetLogistics(id, provinceid);
            if (dt.Rows.Count > 0)
            {
                result.status = RT.SUCCESS;
                result.data = dt;
                result.msg = "success";
            }
            else
            {
                DataTable defaultdt = goodsService.GetDefaultLogistics(id);
                if (defaultdt.Rows.Count > 0)
                {
                    result.status = RT.SUCCESS;
                    result.data = defaultdt;
                    result.msg = "success";
                }
                else
                {
                    result.status = RT.FAILED;
                    result.msg = "failed";
                }
            }
            return result;
        }
        #endregion

        #region 商品评论相关
        /// <summary>
        /// 获取商品评论信息
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(GetGoodsCommentInfoByID_Desc)]
        public Result GetGoodsCommentInfoByID(MyHashtable hs)
        {
            int goodsID = GetParam(hs, "GoodsID", 0);
            Hashtable hsQuery = new Hashtable();
            if (goodsID > 0)
            {
                hsQuery.Add("GoodsID", goodsID);
            }
            Pager p = GetPager(hs, "a.CreateTime desc");
            result.status = memberService.GetGoodsCommentInfoByID(p, hsQuery);
            DataTable dt = p.DataSource;
            List<GoodsCommentInfo> list = new List<GoodsCommentInfo>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    GoodsCommentInfo info = new GoodsCommentInfo();
                    info.Star = dr["Star"].ToString();
                    info.MobileNo = Utils.ReplaceMobileNo(dr["MobileNo"].ToString());
                    info.CommentTime = dr["CommentTime"].ToString();
                    info.CommentContent = dr["CommentContent"].ToString();
                    info.ReplyContent = dr["ReplyContent"].ToString();
                    info.ReplyTime = dr["ReplyTime"].ToString();
                    info.SalePropName = goodsService.GetGoodsAttributeValueInfo(dr["SaleProp"].ToString());
                    list.Add(info);
                }
            }
            result.data = list;
            result.total = p.ItemCount;
            return result;
        }

        /// <summary>
        /// 添加商品评论
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(AddGoodsComment_Desc)]
        public Result AddGoodsComment(MyHashtable hs)
        {
            Hashtable queryhs = new Hashtable();

            string memberid = GetParam(hs, "MemberID", "");
            string orderid = GetParam(hs, "OrderID", "");
            string star = GetParam(hs, "Star", "5");
            //string survicestar = GetParam(hs, "SurviceStar", "5");
            //string logisticsstar = GetParam(hs, "LogisticsStar", "5");
            //string descriptionstar = GetParam(hs, "DescriptionStar", "5");
            string content = GetParam(hs, "Content", "");
            if (!string.IsNullOrEmpty(memberid))
            {
                queryhs.Add("MemberID", memberid);
            }
            DataTable dt = orderService.GetDataByKey("T_MallOrderDetail", "ID", orderid);
            if (dt.Rows.Count > 0)
            {
                queryhs.Add("GoodsID", dt.Rows[0]["GoodsID"]);
                queryhs.Add("SaleProp", dt.Rows[0]["SaleProp"]);
            }
            queryhs.Add("OrderID", orderid);
            if (!string.IsNullOrEmpty(star))
            {
                queryhs.Add("Star", star);
            }
            //queryhs.Add("SurviceStar", survicestar);
            //queryhs.Add("LogisticsStar", logisticsstar);
            //queryhs.Add("DescriptionStar", descriptionstar);
            queryhs.Add("Content", content);
            result.status = goodsService.AddGoodsComment(queryhs);
            if (result.status > 0)
            {
                result.status = RT.SUCCESS;
                result.msg = "评价成功";
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "评价失败";
            }
            return result;
        }
        ///// <summary>
        ///// 追加商品评论
        ///// </summary>
        ///// <param name="hs"></param>
        ///// <returns></returns>
        //[Description(AppendGoodsComment_Desc)]
        //public Result AppendGoodsComment(MyHashtable hs)
        //{
        //    string parentid = GetParam(hs, "ParentID", "");
        //    string content = GetParam(hs, "Content", "");
        //    result.status = goodsService.AppendGoodsComment(parentid, content);
        //    if (result.status > 0)
        //    {
        //        result.status = RT.SUCCESS;
        //        result.msg = "评价成功";
        //    }
        //    else
        //    {
        //        result.status = RT.FAILED;
        //        result.msg = "评价失败";
        //    }
        //    return result;
        //}
        #endregion

    }
}