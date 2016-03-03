using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Model
{
    public class ExcelTemplate
    {
        /// <summary>
        /// 会员导入模板
        /// </summary>
        public static string[] ImportMemberCells = { "会员姓名", "会员卡号", "性别", "生日", "会员积分", "手机号", "常用邮箱",  "省份", "城市", "地区", "注册时间" };
        public static string[] ImportMemberField = { "Name", "CardNo", "Sex", "Birthday", "MemberScore", "MobileNo", "Email",  "ProvinceName", "CityName", "DistrictName", "CreateTime" };
        /// <summary>
        /// 会员导出模板
        /// </summary>
        public static string[] ExportMemberCells = { "会员姓名", "会员卡号", "性别", "生日", "会员积分", "手机号", "常用邮箱",  "省份", "城市", "地区",  "注册时间" };
        public static string[] ExportMemberField = { "Name", "CardNo", "Sex", "Birthday", "MemberScore", "MobileNo", "Email",  "ProvinceName", "CityName", "DistrictName", "CreateTime" };

        /// <summary>
        /// 会员积分明细导出模板
        /// </summary>
        public static string[] ExportScoreCells = { "用户帐户", "变动积分", "剩余积分", "积分来源", "订单号", "备注", "操作时间", "操作人" };
        public static string[] ExportScoreField = { "MemberName", "Score", "Balance", "ScoreType", "OrderNo", "Remark", "CreateTime", "CreateName" };

        /// <summary>
        /// 订单导出模板
        /// </summary>
        public static string[] ExportOrderCells = { "订单号", "用户帐户", "支付类型", "商品总数量", "订单总金额", "商品名称", "商品价格", "商品数量", "收货地址", "邮编", "收货手机号", "收货人", "订单备注", "订单状态", "下单时间", "支付时间", "订单来源", "发货快递", "发货物流编号" };
        public static string[] ExportOrderField = { "OrderNo", "MemberName", "PayMentType", "TotalCount", "TotalMoney", "GoodsName", "GoodsPrice", "Count", "DeliverAddress", "ZipCode", "ConsigneeMobileNo", "ConsigneeName", "OrderRemark", "OrderStatus", "OrderTime", "PayTime", "Source", "LogisticsName", "LogisticsNo" };


        /// <summary>
        /// 商品导出模板
        /// </summary>
        public static string[] ExportGoodsCells = { "商品名称", "商品类别", "销售量", "评分", "销售价", "市场价", "商品库存", "审核状态", "上架状态", "创建人", "创建时间", "审核人", "审核时间" };
        public static string[] ExportGoodsField = { "Name", "TypeName", "Sales", "GoodsStar", "SalePrice", "MarketPrice", "Stock", "Status", "ShelvesStatus", "CreateName", "CreateTime", "ApprovedName", "ApprovedTime" };
    }
}