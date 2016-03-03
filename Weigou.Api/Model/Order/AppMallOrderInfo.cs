using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Weigou.Model
{
    //[Serializable]
    //public class AppGroupOrderInfo
    //{
    //    public string OrderTotalPrice;
    //    public decimal GiftScore;
    //    public List<AppOrderInfo> OrderList;
    //}

    //public class AppOrderInfo
    //{
    //    public string MerchantID;
    //    public string MerchantName;
    //    public string FullSend;
    //    public string LogisticsPrice;
    //    public string GoodsTotalPrice;
    //    public string OrderRemark;
    //    public int IsInternational;
    //    public List<OrderGoodsInfo> GoodsList;
    //}

    //[Serializable]
    //public class OrderGoodsInfo
    //{
    //    public string CartID;
    //    public string GoodsID;
    //    public string GoodsName;
    //    public string Number;
    //    public string PropName;
    //    public string SalePrice;
    //    public string SmallPicture;
    //    public string FullReduction;
    //    public string Tariff;
    //}

    [Serializable]
    public class MallOrderDetailsGoodsInfo
    {
        public string OrderID;
        public string GoodsID;
        public string GoodsName;
        public string Number;
        public string PropName;
        public string SalePrice;
        public string SmallPicture;
        //public string MerchantID;
        //public string MerchantName;
        // public string SupportSaleType;
        //public int IsCanSale;
        //public string Tariff;
    }

    [Serializable]
    public class MallOrderInfos
    {
        // public string TradeNo;
        // public string LogisticsPrice;
        // public string GoodsPrice;
        // public string GiftScore;
        // public string Tariff;
        public string PayMentType;
        public string OrderNo;
        public string OrderTime;
        public string LogisticsName;
        public string OrderStatus;
        public string TotalMoney;
        public string DetailsAddress;
        public string ConsigneeMobileNo;
        public string ConsigneeName;
        public string ZipCode;
        public string LogisticsNo;
        public string LogisticsCode;
    }

    [Serializable]
    public class MallOrderDetailsInfo
    {
        public MallOrderInfos MallOrderInfo;
        public List<MallOrderDetailsGoodsInfo> GoodsDetailsList;
    }


    //public class AppMemberOrderInfo
    //{
    //    public string MerchantID;
    //    public string MerchantName;
    //    public string MerchantLogo;
    //    public string OrderNo;
    //    public string TradeNo;
    //    public string PayMentType;
    //    public string OrderStatus;
    //    public string FullSend;
    //    public string LogisticsPrice;
    //    public string OrderTotalPrice;
    //    public string OrderTotalNumber;
    //    public string LogisticsCode;
    //    public string LogisticsNo;
    //    public int IsCanSenBonus;
    //    public int IsCanComment;
    //    public string ShareUrl;
    //    public List<MemberOrderGoodsInfo> GoodsList;
    //}

    //[Serializable]
    //public class MemberOrderGoodsInfo
    //{
    //    public string OrderID;
    //    public string GoodsID;
    //    public string GoodsName;
    //    public string Number;
    //    public string PropName;
    //    public string SalePrice;
    //    public string SmallPicture;
    //    public string FullReduction;
    //    public string Tariff;
    //}

    //[Serializable]
    //public class ReturnOrder
    //{
    //    public string OrderNo;
    //    public string TradeNo;
    //    public decimal OrderPrice;
    //}

    [Serializable]
    public class MallOrderSaleInfo
    {
        public string OrderSaleID;
        public string MerchantID;
        public string MerchantLogo;
        public string MerchantName;
        public string SmallPicture;
        public string GoodsName;
        public string PropName;
        public string SalePrice;
        public string ApplyNumber;
        public decimal ReturnMoney;
        public string SaleStatus;

    }

    [Serializable]
    public class MallOrderSaleTrackInfo
    {
        public string CreateTime;
        public string Remark;
        public string Title;
    }

    //[Serializable]
    //public class MyStyleInfo
    //{
    //    public string MemberHead;
    //    public string MemberMobile;
    //    public string PublishCount;
    //    public string PraiseCount;
    //    public DataTable PublishList;
    //    public DataTable PraiseList;
    //}

    //[Serializable]
    //public class MyStyleDetailsInfo
    //{
    //    public string MemberHead;
    //    public string MemberMobile;
    //    public string PraiseCount;
    //    public string CommentCount;
    //    public int IsRedHeart;
    //    public string Description;
    //    public string PublishTime;
    //    public string GoodsID;
    //    public string GoodsPicPath;
    //    public DataTable PictureList;
    //    public DataTable CommentList;
    //}

    //[Serializable]
    //public class OrderStatusCountInfo
    //{
    //    public int NoPayCount;//待付款 
    //    public int PayCount;//待发货
    //    public int ShippedCount;//待收货
    //    public int SignedCount;//待评价
    //    public int SaleCount;//售后
    //    public int InsideLetterCount;//站内信数量
    //    public string TelPhone;//丝阁客服电话
    //}

}