using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Model;
using Weigou.Model.Enum;

namespace Weigou.Common
{
    public class EnumHelper
    {
        /// <summary>
        /// 通用状态 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetStatus(object v)
        {
            int sex = Convert.ToInt16(v);
            if (sex == (int)EnumStatus.Disabled)
            {
                return "不可用(待审核)";
            }
            else if (sex == (int)EnumStatus.Normal)
            {
                return "启用(审核通过)";
            }
            else if (sex == (int)EnumStatus.DisAudit)
            {
                return "审核不通过";
            }
            return "";
        }

        /// <summary>
        /// 会员状态 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMemberStatus(object v)
        {
            int sex = Convert.ToInt16(v);
            if (sex == (int)EnumMemStatus.Locked)
            {
                return "冻结";
            }
            else if (sex == (int)EnumMemStatus.Normal)
            {
                return "启用";
            }
            return "";
        }

        /// <summary>
        /// 获取功能操作对应名称
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMallOrderStatuss(object o)
        {
            string res = "";
            int s = Convert.ToInt16(o);
            switch ((EnumMallOrderStatus)s)
            {
                case EnumMallOrderStatus.ToPay:
                    res = "未付款待支付";
                    break;
                case EnumMallOrderStatus.Paid:
                    res = "已付款待发货";
                    break;
                case EnumMallOrderStatus.Shipped:
                    res = "已发货待收货";
                    break;
                case EnumMallOrderStatus.Received:
                    res = "已收货待评价";
                    break;
                case EnumMallOrderStatus.HasComment:
                    res = "已收货已评价";
                    break;
                case EnumMallOrderStatus.ApplyRefund:
                    res = "买家申请退款";
                    break;
                case EnumMallOrderStatus.AgreeReturn:
                    res = "卖家同意退款";
                    break;
                case EnumMallOrderStatus.FinishReturn:
                    res = "已退款";
                    break;
                case EnumMallOrderStatus.Canceled:
                    res = "取消订单";
                    break;
                case EnumMallOrderStatus.Deleted:
                    res = "订单已删除";
                    break;

            }
            return res;
        }

        /// <summary>
        /// 获取功能操作对应名称
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetPayMent(object o)
        {
            string res = "";
            int s = 0;            
            int.TryParse(o.ToString(),out s);
            switch (s)
            {
                case (int)EnumPayMent.WeChat:
                    res = "微信";
                    break;
                case (int)EnumPayMent.AliPay:
                    res = "支付宝";
                    break;
                case (int)EnumPayMent.ChinaPay:
                    res = "银联";
                    break;
                default: 
                    res = "";
                    break;
            }
            return res;
        }

        /// <summary>
        /// 获取性别
        /// </summary>
        /// <param name="v"></param>
        public static string GetSex(object v)
        {
            int sex = Convert.ToInt16(v);
            if (sex == (int)EnumSex.M)
            {
                return "男";
            }
            else if (sex == (int)EnumSex.F)
            {
                return "女";
            }
            return "";
        }

        /// <summary>
        /// 获取系统模块对应名称
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetModule(object m)
        {
            string res = "";
            switch ((EnumModule)m)
            {
                case EnumModule.Other:
                    res = "其他";
                    break;
                case EnumModule.SystemManage:
                    res = "系统管理";
                    break;
                case EnumModule.MemberManage:
                    res = "会员管理";
                    break;
                case EnumModule.MerchantManage:
                    res = "商户管理";
                    break;
                case EnumModule.GoodsManage:
                    res = "商品管理";
                    break;
                case EnumModule.SmsManage:
                    res = "短信管理";
                    break;
                case EnumModule.ReportManage:
                    res = "报表管理";
                    break;
                case EnumModule.ScoreManage:
                    res = "积分管理";
                    break;
                case EnumModule.ContentManage:
                    res = "内容管理";
                    break;
            }
            return res;
        }

        /// <summary>
        /// 获取功能操作对应名称
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetOperation(object o)
        {
            string res = "";
            switch ((EnumOperation)o)
            {
                case EnumOperation.Other:
                    res = "其他";
                    break;
                case EnumOperation.Add:
                    res = "添加";
                    break;
                case EnumOperation.Edit:
                    res = "编辑";
                    break;
                case EnumOperation.Delete:
                    res = "删除";
                    break;
                case EnumOperation.Audit:
                    res = "审核";
                    break;
                case EnumOperation.Import:
                    res = "导入";
                    break;
                case EnumOperation.Export:
                    res = "导出";
                    break;
                case EnumOperation.Login:
                    res = "登录";
                    break;
                case EnumOperation.LoginOut:
                    res = "登出";
                    break;
            }
            return res;
        }

        #region 商城订单相关状态
        /// <summary>
        /// 获取商城订单状态
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMallOrderStatus(object o)
        {
            string res = "";
            int s = Convert.ToInt16(o);
            switch ((EnumMallOrderStatus)s)
            {
                case EnumMallOrderStatus.ToPay:
                    res = "待付款";
                    break;
                case EnumMallOrderStatus.Paid:
                    res = "已付款";
                    break;
                case EnumMallOrderStatus.Shipped:
                    res = "待收货";
                    break;
                case EnumMallOrderStatus.Received:
                    res = "已收货";
                    break;
                case EnumMallOrderStatus.ApplyRefund:
                    res = "申请退款";
                    break;
                case EnumMallOrderStatus.Canceled:
                    res = "交易取消";
                    break;
                case EnumMallOrderStatus.Finished:
                    res = "交易完成";
                    break;
                case EnumMallOrderStatus.Deleted:
                    res = "订单删除";
                    break;
            }
            return res;
        }
        /// <summary>
        /// 获取商城物流状态
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMallLogisticsStatus(object o)
        {
            string res = "";
            int s = Convert.ToInt16(o);
            switch ((EnumLogisticsStatus)s)
            {
                case EnumLogisticsStatus.ToShip:
                    res = "待发货";
                    break;
                case EnumLogisticsStatus.Shipped:
                    res = "已发货";
                    break;
                case EnumLogisticsStatus.Received:
                    res = "已收货";
                    break;
            }
            return res;
        }
        /// <summary>
        /// 获取商城物流状态
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMallPayStatus(object o)
        {
            string res = "";
            int s = Convert.ToInt16(o);
            switch ((EnumPayStatus)s)
            {
                case EnumPayStatus.ToPay:
                    res = "待付款";
                    break;
                case EnumPayStatus.Paid:
                    res = "已付款";
                    break;
                case EnumPayStatus.ApplyRefund:
                    res = "申请退款";
                    break;
                case EnumPayStatus.Refund:
                    res = "已退款";
                    break;
            }
            return res;
        }
        #endregion

        /// <summary>
        /// 获取租车状态对应名称
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetCarOrderStatus(object o)
        {
            string res = "";
            int s = Convert.ToInt16(o);
            switch ((EnumCarOrderStatus)s)
            {
                case EnumCarOrderStatus.Submit:
                    res = "租客提交订单(预定)";
                    break;              
                case EnumCarOrderStatus.Paid:
                    res = "车辆押金";
                    break;
                case EnumCarOrderStatus.Illegal:
                    res = "违章押金";
                    break;
                case EnumCarOrderStatus.TravelStart:
                    res = "行程开始";
                    break;
                case EnumCarOrderStatus.Cancel:
                    res = "取消订单";
                    break;
                case EnumCarOrderStatus.Refuse:
                    res = "平台拒绝";
                    break;
                case EnumCarOrderStatus.Accident:
                    res = "事故保险";
                    break;
                case EnumCarOrderStatus.Finish:
                    res = "订单完成";
                    break;
            }
            return res;
        }

        /// <summary>
        /// 商品审核状态 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetGoodsStatus(object v)
        {
            int res = Convert.ToInt16(v);
            if (res == (int)EnumGoodsStatus.Disabled)
            {
                return "待审核";
            }
            else if (res == (int)EnumGoodsStatus.Normal)
            {
                return "审校通过";
            }
            else if (res == (int)EnumGoodsStatus.DisAudit)
            {
                return "审核不通过";
            }
            return "";
        }
        /// <summary>
        /// 售后状态 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetOrderSaleStatus(object v)
        {
            int status = Convert.ToInt16(v);
            if (status == 1)
            {
                return "退货";
            }
            else if (status == 2)
            {
                return "换货";
            }
            else if (status == 3)
            {
                return "维修";
            }
            return "";
        }
        /// <summary>
        /// 商品上架状态 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetShelvesStatus(object v)
        {
            int res = Convert.ToInt16(v);
            if (res == (int)EnumShelves.Out)
            {
                return "下架";
            }
            else if (res == (int)EnumShelves.Putaway)
            {
                return "上架";
            }
            return "";
        }

        /// <summary>
        /// 积分来源
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetScoreType(object v)
        {
            int res = Convert.ToInt16(v);
            if (res == (int)EnumScoreType.MerchantAdScore)
            {
                return "商户广告积分";
            }
            else if (res == (int)EnumScoreType.MerchantEntertainmentScore)
            {
                return "商户娱乐积分";
            }
            else if (res == (int)EnumScoreType.MemberGoldScore)
            {
                return "会员金币积分";
            }
            else if (res == (int)EnumScoreType.MemberGiftScore)
            {
                return "会员赠品积分";
            }
            else if (res == (int)EnumScoreType.MemberEntertainmentScore)
            {
                return "会员娱乐积分";
            }
            return "";
        }

        /// <summary>
        /// 获取来源名称
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetSourceName(object o)
        {
            string res = "";
            int s = Convert.ToInt16(o);
            switch ((EnumSource)s)
            {
                case EnumSource.Web:
                    res = "Web";
                    break;
                case EnumSource.App:
                    res = "App";
                    break;
            }
            return res;
        }
    }
}
