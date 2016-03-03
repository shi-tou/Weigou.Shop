using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using System.Data;
using Weigou.Common;
using Weigou.Model;
using System.Collections;
using Weigou.Model.Enum;

namespace Weigou.Admin.Ajax
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Order : BaseAjax, IHttpHandler, IRequiresSessionState
    {
        #region 商城订单管理
        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetMallOrderList(HttpContext hc)
        {
            Hashtable hs = GetWhereForMallOrder(hc);
            Pager p = new Pager(PageSize, PageIndex, "a.OrderTime desc");
            orderService.GetMallOrderList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 获取订单查询条件
        /// </summary>
        /// <param name="hc"></param>
        /// <returns></returns>
        public Hashtable GetWhereForMallOrder(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            string orderNo = GetRequest("OrderNo", "");
            string memberName = GetRequest("MemberName", "");
            string mobileNo = GetRequest("MobileNo", "");
            string minTime = GetRequest("MinTime", "");
            string maxTime = GetRequest("MaxTime", "");
            string orderStatus = GetRequest("OrderStatus", "");
            if (!string.IsNullOrEmpty(orderNo))
            {
                hs.Add("OrderNo", orderNo);
            }
            if (!string.IsNullOrEmpty(memberName))
            {
                hs.Add("MemberName", memberName);
            }
            if (!string.IsNullOrEmpty(mobileNo))
            {
                hs.Add("MobileNo", mobileNo);
            }
            if (orderStatus != "")
            {
                hs.Add("OrderStatus", orderStatus);
            }
            if (minTime != "")
            {
                hs.Add("MinTime", minTime);
            }
            if (maxTime != "")
            {
                hs.Add("MaxTime", maxTime);
            }
            return hs;
        }
        #endregion

        #region 提醒发货
        /// <summary>
        /// 提醒发货列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetMallRemindSendList(HttpContext hc)
        {
            Hashtable hs = GetWhereForMallOrder(hc);
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            orderService.GetMallRemindSendList(p, hs);
            ResponseWrite(hc, p);
        }

        /// <summary>
        /// 获取提醒信息数量
        /// </summary>
        /// <param name="hc"></param>
        public void GetMallRemindSendCount(HttpContext hc)
        {
            ResponseWrite(hc, orderService.GetMallRemindSendCount());
        }
        #endregion

        #region 售后服务管理
        /// <summary>
        /// 售后服务列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetMallOrderSaleList(HttpContext hc)
        {
            Hashtable hs = GetWhereForOrderSale(hc);
            Pager p = new Pager(PageSize, PageIndex, "a.ApplyTime desc");
            orderService.GetOrderSaleList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 获取售后服务查询条件
        /// </summary>
        /// <param name="hc"></param>
        /// <returns></returns>
        public Hashtable GetWhereForOrderSale(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            string consigneemobileNo = GetRequest("ConsigneeMobileNo", "");
            string merchantname = GetRequest("MerchantName", "");
            string orderno = GetRequest("OrderNo", "");
            string minTime = GetRequest("MinTime", "");
            string maxTime = GetRequest("MaxTime", "");
            string ordertype = GetRequest("OrderType", "");
            string status = GetRequest("Status", "");
            if (!string.IsNullOrEmpty(consigneemobileNo))
            {
                hs.Add("ConsigneeMobileNo", consigneemobileNo);
            }
            if (!string.IsNullOrEmpty(merchantname))
            {
                hs.Add("MerchantName", merchantname);
            }
            if (!string.IsNullOrEmpty(orderno))
            {
                hs.Add("OrderNo", orderno);
            }
            if (!string.IsNullOrEmpty(ordertype))
            {
                hs.Add("OrderType", ordertype);
            }
            if (!string.IsNullOrEmpty(status))
            {
                hs.Add("Status", status);
            }
            if (minTime != "")
            {
                hs.Add("MinTime", minTime);
            }
            if (maxTime != "")
            {
                hs.Add("MaxTime", maxTime);
            }
            return hs;
        }
        #endregion

        #region 订单退款管理
        /// <summary>
        /// 退款列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetMallOrderReturnList(HttpContext hc)
        {
            Hashtable hs = GetWhereForOrderRerurn(hc);
            Pager p = new Pager(PageSize, PageIndex, "a.ApplyTime desc");
            orderService.GetMallOrderReturnList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 退款列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetPlatOrderReturnList(HttpContext hc)
        {
            Hashtable hs = GetWhereForPlatOrderRerurn(hc);
            Pager p = new Pager(PageSize, PageIndex, "a.ApplyTime desc");
            orderService.GetMallOrderReturnList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 获取退款列表查询条件
        /// </summary>
        /// <param name="hc"></param>
        /// <returns></returns>
        public Hashtable GetWhereForOrderRerurn(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            string mobileNo = GetRequest("MobileNo", "");
            string paymenttype = GetRequest("PayType", "");
            string orderno = GetRequest("OrderNo", "");
            string minTime = GetRequest("MinTime", "");
            string maxTime = GetRequest("MaxTime", "");
            if (!string.IsNullOrEmpty(mobileNo))
            {
                hs.Add("MobileNo", mobileNo);
            }
            if (!string.IsNullOrEmpty(paymenttype))
            {
                hs.Add("PayType", paymenttype);
            }
            if (!string.IsNullOrEmpty(orderno))
            {
                hs.Add("OrderNo", orderno);
            }
            if (minTime != "")
            {
                hs.Add("MinTime", minTime);
            }
            if (maxTime != "")
            {
                hs.Add("MaxTime", maxTime);
            } 
            return hs;
        }
        ///<summary>
        /// 获取退款列表查询条件
        /// </summary>
        /// <param name="hc"></param>
        /// <returns></returns>
        public Hashtable GetWhereForPlatOrderRerurn(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            string mobileNo = GetRequest("MobileNo", "");
            string paymenttype = GetRequest("PayType", "");
            string orderno = GetRequest("OrderNo", "");
            string minTime = GetRequest("MinTime", "");
            string maxTime = GetRequest("MaxTime", "");
            string status = GetRequest("Status", "");
            if (!string.IsNullOrEmpty(mobileNo))
            {
                hs.Add("MobileNo", mobileNo);
            }
            if (!string.IsNullOrEmpty(paymenttype))
            {
                hs.Add("PayType", paymenttype);
            }
            if (!string.IsNullOrEmpty(orderno))
            {
                hs.Add("OrderNo", orderno);
            }
            if (!string.IsNullOrEmpty(status))
            {
                hs.Add("Status", status);
            }
            if (minTime != "")
            {
                hs.Add("MinTime", minTime);
            }
            if (maxTime != "")
            {
                hs.Add("MaxTime", maxTime);
            }
            hs.Add("MerchantID", "0");
            return hs;
        }
        #endregion
    }
}
