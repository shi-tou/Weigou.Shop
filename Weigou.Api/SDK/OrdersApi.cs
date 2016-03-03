using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weigou.Api.Base;
using System.ComponentModel;
using System.Data;
using Weigou.Common;
using System.Collections;
using Weigou.Api.Model;
using Newtonsoft.Json;
using Weigou.Model;
using Weigou.Model.Order;
using System.Web.UI.WebControls;
using Weigou.Model.Enum; 

namespace Weigou.Api.SDK
{
    /// <summary>
    /// 订单模块Api
    /// </summary>
    public class OrderApi : BaseApi
    {
        #region 方法描述
        public const string SubmitCarOrder_Desc = "MemberID=会员ID&CarID=车辆ID&OilFeeType=(1-原油位还车 2-按里程计费)&StartTime=取车时间&EndTime=还车时间&CarVouchers=代金券&IntegralVouchers=积分代金券&Deductible=不计免赔";
        public const string GetCarOrderList_Desc = "PageIndex=页数&PageSize=每页大小&MemberID=会员ID&OrderStatus=订单状态(0-订单提交(租客) 15-车辆押金(未付) 20-违章押金(未付) 30-行程开始  40-订单取消(租客)  45-订单拒接(平台)  50-事故保险  100-订单完成)&CarOrderNo=订单号&MinTime=最小订单时间&MaxTime=最大订单时间";
        public const string GetCarOrderDetail_Desc = "CarOrderNo=订单号";
        public const string CancelOrder_Desc = "CarOrderNo=订单号(多个用','号连接)&CancelReason=取消原因";
        public const string DeleteOrder_Desc = "CarOrderNo=订单号(多个用','号连接)";
        public const string AddCarDriveArchives_Desc = "ID=驾照档案库ID(0表示新增)&MemberID=会员ID&Name=姓名(昵称)&IDcard=身份证号码&EarlyDate=驾照初领日期&QuasiDriveType=准驾车型&DriveRecordNo=驾照档案编号";
        public const string GetPrepayOrderInfo_Desc = "MemberID=会员ID&CartIDs=购物车ID集合,多个用','拼接";
        public const string SubmitMallOrder_Desc = "MemberID=会员ID&DeliverAddressID=收货地址ID&CartIDs=购物车ID集合,多个用','拼接";
        public const string GetMallOrderList_Desc = "MemberID=会员ID&Status=订单状态(0-全部 1-待付款 2-待发货 3-待收货 4-待评价)";
        public const string ApplyOrderReturn_Desc = "MallOrderNo=订单号&Remark=退款原因";
        public const string CancelMallOrder_Desc = "MallOrderNo=订单号(多个用','号连接)&CancelReason=取消原因";
        public const string DeleteMallOrder_Desc = "MallOrderNo=订单号(多个用','号连接)";
        
        public const string ApplyMallOrderSale_Desc = "Type=服务类型(1:退货 2:换货 3:维修)&OrderID=订单ID&ApplyNumber=申请数量&Description=描述&Pic=图片路径(多个用','号连接)";
        public const string GetMallOrderSaleList_Desc = "MemberID=会员ID&PagerSize=页大小&PagerIndex=页索引";
        public const string GetMallOrderSaleInfo_Desc = "OrderSaleID=售后服务ID";
        public const string GetMallOrderDetailsInfo_Desc = "OrderNo=订单号";
        public const string AddMallRemindSend_Desc = "OrderNo=订单号";
        #endregion

        #region 商城订单相关

        /// <summary>
        /// 获取订单详情
        /// </summary>
        [Description(GetMallOrderDetailsInfo_Desc)]
        public Result GetMallOrderDetailsInfo(MyHashtable hs)
        {
            string orderno = GetParam(hs, "OrderNo", "");
            if (!string.IsNullOrEmpty(orderno))
            {
                MallOrderDetailsInfo info = new MallOrderDetailsInfo();
                DataSet ds = orderService.GetMallOrderDetail(orderno);
                if (ds.Tables.Count > 0)
                {
                    MallOrderInfos orderinfo = new MallOrderInfos();
                    //订单主表数据
                    DataTable dtOrder = ds.Tables[0];
                    DataRow drOrder = dtOrder.Rows[0];
                    //orderinfo.LogisticsName = drOrder["LogisticsName"].ToString();
                    //orderinfo.LogisticsPrice = drOrder["LogisticsPrice"].ToString();
                    //orderinfo.GoodsPrice = drOrder["GoodsPrice"].ToString();
                    //orderinfo.GiftScore = drOrder["GiftScore"].ToString();
                    orderinfo.TotalMoney = drOrder["TotalMoney"].ToString();
                    orderinfo.OrderStatus = EnumHelper.GetMallOrderStatus(drOrder["OrderStatus"]);
                    orderinfo.OrderNo = drOrder["OrderNo"].ToString();
                    orderinfo.OrderTime = Convert.ToDateTime(drOrder["OrderTime"]).ToString("yyyy-MM-dd HH:mm:ss");
                    orderinfo.DetailsAddress = drOrder["DeliverAddress"].ToString();
                    orderinfo.ConsigneeName = drOrder["ConsigneeName"].ToString();
                    orderinfo.ConsigneeMobileNo = drOrder["ConsigneeMobileNo"].ToString();
                    orderinfo.ZipCode = drOrder["ZipCode"].ToString();
                    orderinfo.LogisticsNo = Convert.ToString(drOrder["LogisticsNo"]);
                    orderinfo.LogisticsCode = Convert.ToString(drOrder["LogisticsCode"]); 
                       
                    //decimal Tariff = 0;
                    //订单详细表数据
                    DataTable dtOrderDetails = ds.Tables[1];
                    List<MallOrderDetailsGoodsInfo> list = new List<MallOrderDetailsGoodsInfo>();
                    foreach (DataRow drOrderDetails in dtOrderDetails.Rows)
                    {
                        MallOrderDetailsGoodsInfo orderdetailsinfo = new MallOrderDetailsGoodsInfo();
                        orderdetailsinfo.OrderID = drOrderDetails["ID"].ToString();
                        orderdetailsinfo.GoodsID = drOrderDetails["GoodsID"].ToString();
                        orderdetailsinfo.GoodsName = drOrderDetails["GoodsName"].ToString();
                        orderdetailsinfo.Number = drOrderDetails["Count"].ToString();
                        orderdetailsinfo.SalePrice = drOrderDetails["SalePrice"].ToString();
                        orderdetailsinfo.PropName = goodsService.GetGoodsAttributeValueInfo(drOrderDetails["SaleProp"].ToString());
                        orderdetailsinfo.SmallPicture = GetServerPicture(drOrderDetails["SmallPicture"].ToString());
                         
                        //if (drOrder["OrderStatus"].ToString() == "3" || drOrder["OrderStatus"].ToString() == "4")
                        //{
                        //    orderdetailsinfo.IsCanSale = goodsService.CheckGoodsSale(orderdetailsinfo.OrderID);
                        //}
                        //orderdetailsinfo.Tariff = drOrderDetails["Tariff"].ToString();
                        //Tariff += Convert.ToDecimal(orderdetailsinfo.Tariff);
                        //DataTable goodsDt = goodsService.GetDataByKey("T_Goods", "ID", orderdetailsinfo.GoodsID);
                        //if (goodsDt.Rows.Count > 0)
                        //{
                        //    if (goodsDt.Rows[0]["PurchaseQuantity"].ToString() == "0")
                        //    {
                        //        orderdetailsinfo.SupportSaleType = "2,3";
                        //    }
                        //    else
                        //    {
                        //        orderdetailsinfo.SupportSaleType = goodsDt.Rows[0]["SupportSaleType"].ToString();
                        //    }
                        //}
                        list.Add(orderdetailsinfo);
                    }
                    //orderinfo.Tariff = Tariff.ToString();
                    info.MallOrderInfo = orderinfo;
                    info.GoodsDetailsList = list;
                }
                result.status = RT.SUCCESS;
                result.data = info;
                result.msg = "success";
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "failed";
            }
            return result;
        }
        /// <summary>
        /// 获取预付订单详情
        /// </summary>
        [Description(GetPrepayOrderInfo_Desc)]
        public Result GetPrepayOrderInfo(MyHashtable hs)
        {
            AppPrepayOrderInfo info = new AppPrepayOrderInfo();
            int memberID = GetParam(hs, "MemberID", 0);
            string cartIDs = GetParam(hs, "CartIDs", "");
            Hashtable hsQuery = new Hashtable();
            hsQuery.Add("MemberID", memberID);
            hsQuery.Add("CartIDs", cartIDs);
            Pager p = new Pager(999, 1, "a.CreateTime desc");
            goodsService.GetShoppingCartList(p, hsQuery);

            decimal totalMoney = 0;
            int totalCount = 0;
            //商品清单
            List<CartGoodsInfo> listGoods = new List<CartGoodsInfo>();
            foreach (DataRow dr in p.DataSource.Rows)
            {
                CartGoodsInfo ginfo = new CartGoodsInfo();
                ginfo.CartID = Convert.ToInt32(dr["ID"]);
                ginfo.GoodsID = Convert.ToInt32(dr["GoodsID"]);
                ginfo.GoodsName = dr["GoodsName"].ToString();
                ginfo.Count = Convert.ToInt32(dr["Count"]);
                ginfo.SalePrice = dr["SalePrice"].ToString();
                ginfo.PropName = goodsService.GetGoodsAttributeValueInfo(dr["SaleProp"].ToString());
                ginfo.SmallPicture = GetServerPicture(dr["SmallPicture"].ToString());
                listGoods.Add(ginfo);
                totalMoney += Convert.ToInt32(ginfo.Count) * Convert.ToDecimal(ginfo.SalePrice);
                totalCount += Convert.ToInt32(ginfo.Count);
            }
            info.GoodsList = listGoods;
            info.TotalMoney = totalMoney.ToString();
            info.TotalCount = totalCount;
            //默认收货地址
            Hashtable hsAddr = new Hashtable();
            hsAddr.Add("IsDefault", 1);
            hsAddr.Add("MemberID", memberID);
            DataTable dtAddress = memberService.GetDeliverAddress(hsAddr);
            if (dtAddress.Rows.Count == 0)
            {
                info.DefaultAddress = null;
            }
            else
            {
                AppDeliverAddress address = new AppDeliverAddress();
                DataRow dr = dtAddress.Rows[0];
                address.ID = Convert.ToInt32(dr["ID"]);
                address.ConsigneeName = Convert.ToString(dr["ConsigneeName"]);
                address.ConsigneeMobileNo = Convert.ToString(dr["ConsigneeMobileNo"]);
                address.IsDefault = Convert.ToInt32(dr["IsDefault"]);
                address.ZipCode = Convert.ToString(dr["ZipCode"]);
                address.Address = Convert.ToString(dr["ProvinceName"]) + Convert.ToString(dr["CityName"]) + Convert.ToString(dr["DistrictName"]) + Convert.ToString(dr["Address"]);
                info.DefaultAddress = address;
            }
            info.ExpressFee = "10.0";//运费
            result.data = info;
            result.status = RT.SUCCESS;
            return result;
        }

        /// <summary>
        /// 提交商城订单
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(SubmitMallOrder_Desc)]
        public Result SubmitMallOrder(MyHashtable hs)
        {
            int memberID = GetParam(hs, "MemberID", 0);
            string cartIDs = GetParam(hs, "CartIDs", "");
            int deliverAddressID = GetParam(hs, "DeliverAddressID", 0);
            string orderRemark = GetParam(hs, "OrderRemark", "");
            SubmitMallOrderInfo info = new SubmitMallOrderInfo();
            info.CartIDs = cartIDs;
            info.MemberID = memberID;
            info.OrderRemark = orderRemark;
            info.Source = 2;
            info.DeliverAddressID = deliverAddressID;
            string orderNo = "";
            result.status = orderService.SubmitMallOrder(info, out orderNo);
            result.data = orderNo;
            return result;
        }

        /// <summary>
        /// 获取商城订单列表
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Description(GetMallOrderList_Desc)]
        public Result GetMallOrderList(MyHashtable hs)
        {
            int memberID = GetParam(hs, "MemberID", 0);
            int status = GetParam(hs, "Status", 0);
            Hashtable hsQuery = new Hashtable();
            hsQuery.Add("MemberID", memberID);
            switch (status)
            {
                case 1://待付款
                    hsQuery.Add("OrderStatus", (int)EnumMallOrderStatus.ToPay);
                    break;
                case 2://已付款/待发货
                    hsQuery.Add("OrderStatus", (int)EnumMallOrderStatus.Paid);
                    break;
                case 3://已发货/待收货
                    hsQuery.Add("OrderStatus", (int)EnumMallOrderStatus.Shipped);
                    break;
                case 4://已收货/待评价
                    hsQuery.Add("OrderStatus", (int)EnumMallOrderStatus.Received);
                    break;
            }
            Pager p = GetPager(hs, "a.OrderTime desc");
            List<MallOrderInfo> list = (List<MallOrderInfo>)orderService.GetMallOrderForApp(p, hsQuery);
            //处理图片路径
            foreach (MallOrderInfo info in list)
            {
                foreach (MallOrderDetailInfo i in info.MallOrderDetail)
                {
                    i.SmallPicture = GetServerPicture(i.SmallPicture);
                }
            }
            result.data = list;
            result.status = RT.SUCCESS;
            result.total = list.Count;
            return result;
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        [Description(CancelMallOrder_Desc)]
        public Result CancelMallOrder(MyHashtable hs)
        {
            string strMallOrderNo = GetParam(hs, "MallOrderNo", "");
            string strCancelReason = GetParam(hs, "CancelReason", "");
            if (string.IsNullOrEmpty(strMallOrderNo))
            {
                result.status = RT.RESULT_NOT_EXIST;
                result.msg = "找不到指定的MallOrderNo！";
                return result;
            }

            int irt = orderService.UpdateMallOrder(strMallOrderNo, strCancelReason, (int)EnumMallOrderStatus.Canceled);

            if (irt == RT.SUCCESS)
            {
                //订单状态跟踪
                MallOrderTrackInfo trackInfo = new MallOrderTrackInfo();
                trackInfo.OrderNo = strMallOrderNo;
                trackInfo.OrderStatus = (int)EnumMallOrderStatus.Canceled;
                trackInfo.LogisticsStatus = (int)EnumLogisticsStatus.ToShip;
                trackInfo.PayStatus = (int)EnumPayStatus.ToPay;
                trackInfo.CreateBy = 0;
                orderService.AddMallOrderTrack(trackInfo);
                result.status = RT.SUCCESS;
                result.msg = "取消成功！";
            }
            else if (irt == RT.RESULT_NOT_EXIST)
            {
                result.status = RT.RESULT_NOT_EXIST;
                result.msg = "没有查询到订单号信息！";
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "取消失败！";
            }
            return result;
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        [Description(DeleteMallOrder_Desc)]
        public Result DeleteMallOrder(MyHashtable hs)
        {
            string strMallOrderNo = GetParam(hs, "MallOrderNo", "");
            if (string.IsNullOrEmpty(strMallOrderNo))
            {
                result.status = RT.RESULT_NOT_EXIST;
                result.msg = "找不到指定的MallOrderNo！";
                return result;
            }

            int irt = orderService.UpdateMallOrder(strMallOrderNo, (int)EnumMallOrderStatus.Deleted);

            if (irt == RT.SUCCESS)
            {
                //订单状态跟踪
                MallOrderTrackInfo trackInfo = new MallOrderTrackInfo();
                trackInfo.OrderNo = strMallOrderNo;
                trackInfo.OrderStatus = (int)EnumMallOrderStatus.Deleted;
                trackInfo.LogisticsStatus = (int)EnumLogisticsStatus.Received;
                trackInfo.PayStatus = (int)EnumPayStatus.Paid;
                trackInfo.CreateBy = 0;
                orderService.AddMallOrderTrack(trackInfo);
                result.status = RT.SUCCESS;
                result.msg = "删除成功！";
            }
            else if (irt == RT.RESULT_NOT_EXIST)
            {
                result.status = RT.RESULT_NOT_EXIST;
                result.msg = "没有查询到订单号信息！";
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "删除失败！";
            }
            return result;
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        [Description("MallOrderNo=订单号")]
        public Result ConfirmMallGoods(MyHashtable hs)
        {
            string strMallOrderNo = GetParam(hs, "MallOrderNo", "");
            if (string.IsNullOrEmpty(strMallOrderNo))
            {
                result.status = RT.RESULT_NOT_EXIST;
                result.msg = "找不到指定的MallOrderNo！";
                return result;
            }

            int irt = orderService.ConfirmMallOrderReceived(strMallOrderNo);

            if (irt == RT.SUCCESS)
            {
                result.status = RT.SUCCESS;
                result.msg = "修改成功！";
            }
            else if (irt == RT.RESULT_NOT_EXIST)
            {
                result.status = RT.RESULT_NOT_EXIST;
                result.msg = "没有查询到订单号信息！";
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "修改失败！";
            }
            return result;
        }

        /// <summary>
        /// 结算订单详情
        /// </summary>
        [Description("MallOrderNo=订单号")]
        public Result SettlementMallOrder(MyHashtable hs)
        {
            string strMallOrderNo = GetParam(hs, "MallOrderNo", "");
            if (string.IsNullOrEmpty(strMallOrderNo))
            {
                result.status = RT.RESULT_NOT_EXIST;
                result.msg = "找不到指定的MallOrderNo！";
                return result;
            }

            DataTable dt = orderService.SettlementMallOrder(strMallOrderNo);
            if (dt.Rows.Count > 0)
            {
                result.status = RT.SUCCESS;
                result.data = dt;
                result.msg = "查询成功！";
            }
            else
            {
                result.status = RT.RESULT_NOT_EXIST;
                result.msg = "没有查询到订单号信息！";
            }
            return result;
        }


        /// <summary>
        /// 查看物流信息
        /// </summary>
        [Description("Type=物流公司编号&postid=快递单号&callbackurl=点击'返回'跳转的地址")]
        public Result GetLogisticsMallOrder(MyHashtable hs)
        {
            string type = GetParam(hs, "type", "");
            string postid = GetParam(hs, "postid", "");
            string callbackurl = GetParam(hs, "callbackurl", "");

            string url = "http://m.kuaidi100.com/index_all.html?type={0}&postid={1}&callbackurl={2}";
            url = string.Format(url, type, postid, callbackurl);

            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(type))
            {
                result.status = RT.RESULT_NOT_EXIST;
                result.msg = "找不到指定的物流公司！";
                return result;
            }

            if (!string.IsNullOrEmpty(url))
            {
                dic.Add("LogisticsUrl", url);
                result.data = dic;
                result.status = RT.SUCCESS;
                result.msg = "查询成功！";
            }
            else
            {
                result.status = RT.RESULT_NOT_EXIST;
                result.msg = "没有查询到物流信息！";
            }
            return result;
        }

        #endregion

        #region 提醒发货
        /// <summary>
        /// 提醒发货
        /// </summary>
        [Description(AddMallRemindSend_Desc)]
        public Result AddMallRemindSend(MyHashtable hs)
        {
            string orderno = GetParam(hs, "OrderNo", ""); 
            int res = orderService.AddMallRemindSend(orderno);
            if (res == RT.SUCCESS)
            {
                result.status = RT.SUCCESS;
                result.msg = "success";
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "failed";
            }
            return result;
        }

        #endregion

        #region 全额退款相关
        /// <summary>
        /// 全额退款申请
        /// </summary>
        [Description(ApplyOrderReturn_Desc)]
        public Result ApplyOrderReturn(MyHashtable hs)
        {
            string orderno = GetParam(hs, "MallOrderNo", "");
            string remark = GetParam(hs, "Remark", "");
            int res = orderService.ApplyOrderReturn(orderno, remark);
            if (res > 0)
            {
                result.status = RT.SUCCESS;
                result.msg = "success";
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "failed";
            }
            return result;
        }
        #endregion

        #region 售后服务相关
        /// <summary>
        /// 售后服务申请
        /// </summary>
        [Description(ApplyMallOrderSale_Desc)]
        public Result ApplyOrderSale(MyHashtable hs)
        {
            Hashtable queryhs = new Hashtable();
            string type = GetParam(hs, "Type", "");
            string orderid = GetParam(hs, "OrderID", "");
            string applynumber = GetParam(hs, "ApplyNumber", "");
            string description = GetParam(hs, "Description", "");
            string pic = GetParam(hs, "Pic", "");
            if (!string.IsNullOrEmpty(type))
            {
                queryhs.Add("Type", type);
            }
            if (!string.IsNullOrEmpty(orderid))
            {
                queryhs.Add("OrderID", orderid);
            }
            if (!string.IsNullOrEmpty(applynumber))
            {
                queryhs.Add("ApplyNumber", applynumber);
            }
            if (!string.IsNullOrEmpty(description))
            {
                queryhs.Add("Description", description);
            }
            if (!string.IsNullOrEmpty(pic))
            {
                queryhs.Add("Pic", pic);
            }
            int res = orderService.ApplyOrderSale(queryhs);
            if (res > 0)
            {
                result.status = RT.SUCCESS;
                result.msg = "success";
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "failed";
            }
            return result;
        }
        /// <summary>
        /// 售后服务列表
        /// </summary>
        [Description(GetMallOrderSaleList_Desc)]
        public Result GetMallOrderSaleList(MyHashtable hs)
        {
            string memberid = GetParam(hs, "MemberID", "");
            List<MallOrderSaleInfo> list = new List<MallOrderSaleInfo>();
            Pager p = GetPager(hs, "a.ApplyTime desc ");
            Hashtable queryhs = new Hashtable();
            if (!string.IsNullOrEmpty(memberid))
            {
                queryhs.Add("MemberID", memberid);
            }
            result.status = orderService.GetOrderSaleList(p, queryhs);
            DataTable dt = p.DataSource;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    MallOrderSaleInfo info = new MallOrderSaleInfo();
                    info.OrderSaleID = dr["ID"].ToString();
                    info.ApplyNumber = dr["ApplyNumber"].ToString();
                    info.SmallPicture = dr["SmallPicture"].ToString();
                    info.GoodsName = dr["GoodsName"].ToString();
                    info.PropName = goodsService.GetGoodsAttributeValueInfo(dr["SaleProp"].ToString());
                    info.SalePrice = dr["SalePrice"].ToString();
                    if (dr["Status"].ToString() == "0")
                    {
                        info.SaleStatus = "卖家处理中";
                    }
                    else
                    {
                        info.SaleStatus = "处理完成";
                    }
                    info.ReturnMoney = Convert.ToDecimal(dr["SalePrice"].ToString()) * Convert.ToDecimal(dr["ApplyNumber"].ToString());
                    list.Add(info);
                }

            }
            result.data = list;
            result.total = p.ItemCount;
            return result;
        }
        /// <summary>
        /// 查看售后信息
        /// </summary>
        [Description(GetMallOrderSaleInfo_Desc)]
        public Result GetMallOrderSaleInfo(MyHashtable hs)
        {
            string ordersaleid = GetParam(hs, "OrderSaleID", "");
            if (!string.IsNullOrEmpty(ordersaleid))
            {
                result.status = RT.SUCCESS;
                result.msg = "success";
                DataTable dt = orderService.GetOrderSaleInfo(ordersaleid);
                List<MallOrderSaleTrackInfo> list = new List<MallOrderSaleTrackInfo>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        MallOrderSaleTrackInfo info = new MallOrderSaleTrackInfo();
                        if (dr["Status"].ToString() == "0")
                        {
                            info.Title = "买家发起了" + EnumHelper.GetOrderSaleStatus(dr["Type"]) + "申请";
                            info.Remark = dr["Description"].ToString();
                            info.CreateTime = dr["ApplyTime"].ToString();
                        }
                        else if (dr["Status"].ToString() == "1")
                        {
                            info.Title = "卖家同意了" + EnumHelper.GetOrderSaleStatus(dr["Type"]) + "申请";
                            info.Remark = dr["Remark"].ToString();
                            info.CreateTime = dr["DealTime"].ToString();
                        }
                        else if (dr["Status"].ToString() == "2")
                        {
                            info.Title = "卖家拒绝了" + EnumHelper.GetOrderSaleStatus(dr["Type"]) + "申请";
                            info.Remark = dr["Remark"].ToString();
                            info.CreateTime = dr["DealTime"].ToString();
                        }
                        list.Add(info);
                    }
                    result.data = list;
                }
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "failed";
            }
            return result;
        }
        #endregion
    }
}
