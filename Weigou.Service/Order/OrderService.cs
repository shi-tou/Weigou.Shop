using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using Spring.Transaction.Interceptor;
using Weigou.Dao;
using Weigou.Common;
using Weigou.Model;
using Weigou.Model.Enum;
using Weigou.Model.Order;

namespace Weigou.Service
{
    public class OrderService : BaseService, IOrderService
    {
        private IOrderDao orderDao;
        public IOrderDao OrderDao
        {
            set
            {
                orderDao = value;
            }
        }


        #region 商城订单管理
        /// <summary>
        /// 订单列表
        /// </summary>
        /// <returns></returns>
        public int GetMallOrderList(Pager p, Hashtable hs)
        {
            return orderDao.GetMallOrderList(p, hs);
        }
        /// <summary>
        /// 订单列表(App)
        /// </summary>
        /// <returns></returns>
        public List<MallOrderInfo> GetMallOrderForApp(Pager p, Hashtable hs)
        {
            List<MallOrderInfo> list = new List<MallOrderInfo>();
            hs.Add("DeleteOrder", 9);
            orderDao.GetMallOrderList(p, hs);
            foreach (DataRow dr in p.DataSource.Rows)
            {
                MallOrderInfo info = new MallOrderInfo();
                info = ObjectHelper.CopyToObject<MallOrderInfo>(dr);
                DataTable dt = GetDataByKey("T_MallOrderDetail", "OrderNo", info.OrderNo);
                info.MallOrderDetail = ObjectHelper.CopyToObjects<MallOrderDetailInfo>(dt).ToList();
                list.Add(info);
            }
            return list;
        }
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public DataSet GetMallOrderDetail(string orderNo)
        {
            return orderDao.GetMallOrderDetail(orderNo);
        }

        /// <summary>
        /// 提交商城订单
        /// </summary>
        /// <param name="info">订单信息</param>
        /// <param name="orderNo">订单号(输出)</param>
        /// <returns></returns>
        [Transaction]
        public int SubmitMallOrder(SubmitMallOrderInfo info, out string orderNo)
        {
            orderNo = "";
            //收货地址
            Hashtable hs = new Hashtable();
            hs.Add("ID", info.DeliverAddressID);
            DataTable dtAddress = memberService.GetDeliverAddress(hs);
            if (dtAddress.Rows.Count == 0)
                return RT.FAILED;
            DataRow drAddress = dtAddress.Rows[0];
            //根据购物车Id查询商品
            Hashtable hsQuery = new Hashtable();
            hsQuery.Add("MemberID", info.MemberID);
            hsQuery.Add("CartIDs", info.CartIDs);
            Pager p = new Pager(999, 1, "a.CreateTime desc");
            goodsService.GetShoppingCartList(p, hsQuery);
            DataTable dtGoods = p.DataSource;
            if (dtGoods.Rows.Count == 0)
                return RT.FAILED;
            //订单号
            orderNo = Utils.CreateOrderNo();
            //商品清单
            decimal totalMoney = 0;
            int totalCount = 0;
            DataTable dtOrderDetail = GetDataByKey("T_MallOrderDetail", "ID", "0");
            foreach (DataRow drGoods in dtGoods.Rows)
            {
                DataRow drOrderDetail = dtOrderDetail.NewRow();
                int count = Convert.ToInt32(drGoods["Count"]);//商品数量
                decimal salePrice = Convert.ToDecimal(drGoods["SalePrice"]);//商品价格
                decimal totalPrice = salePrice * count;//商品小计
                drOrderDetail["OrderNo"] = orderNo;
                drOrderDetail["GoodsID"] = drGoods["GoodsID"];
                drOrderDetail["GoodsName"] = drGoods["GoodsName"];
                drOrderDetail["Count"] = count;
                drOrderDetail["SaleProp"] = drGoods["SaleProp"]; ;
                drOrderDetail["SalePrice"] = salePrice;
                drOrderDetail["TotalPrice"] = totalPrice;
                drOrderDetail["SmallPicture"] = drGoods["SmallPicture"];
                dtOrderDetail.Rows.Add(drOrderDetail);
                totalMoney += totalPrice;
                totalCount += count;
            }
            //订单主信息
            DataTable dtOrder = GetDataByKey("T_MallOrder", "ID", 0);
            DataRow drOrder = dtOrder.NewRow();
            drOrder["OrderNo"] = orderNo;
            drOrder["MemberID"] = info.MemberID;
            drOrder["TotalCount"] = totalCount;
            drOrder["TotalMoney"] = totalMoney;
            drOrder["DeliverAddress"] = Convert.ToString(drAddress["ProvinceName"]) + Convert.ToString(drAddress["CityName"]) + Convert.ToString(drAddress["DistrictName"]) + Convert.ToString(drAddress["Address"]);
            drOrder["ConsigneeName"] = drAddress["ConsigneeName"];
            drOrder["ConsigneeMobileNo"] = drAddress["ConsigneeMobileNo"];
            drOrder["ZipCode"] = drAddress["ZipCode"];
            drOrder["Source"] = info.Source;
            drOrder["PayStatus"] = EnumPayStatus.ToPay;
            drOrder["LogisticsStatus"] = EnumLogisticsStatus.ToShip;
            drOrder["OrderStatus"] = EnumMallOrderStatus.ToPay;
            drOrder["OrderRemark"] = info.OrderRemark;
            drOrder["OrderTime"] = DateTime.Now;
            dtOrder.Rows.Add(drOrder);
            //执行入库
            if (UpdateDataTable(dtOrder) > 0)
            {
                UpdateDataTable(dtOrderDetail);
                //删除购物车信息
                goodsService.DeleteShoppingCart(info.CartIDs);
                //更新库存
                foreach (DataRow drGoods in dtGoods.Rows)
                {
                    int count = Convert.ToInt32(drGoods["Count"]);//商品数量
                    goodsService.UpdateGoodsStock(Convert.ToInt32(drGoods["GoodsID"]), Convert.ToString(drGoods["SaleProp"]), -count);
                }
            }
            return RT.SUCCESS;
        }
        /// <summary>
        /// 确认订单发货
        /// </summary>
        /// <param name="info">确认发货信息</param>
        /// <returns></returns>
        [Transaction]
        public int ConfirmMallOrderShipped(MallOrderShipInfo info)
        {
            DataTable dt = GetDataByKey("T_MallOrder", "OrderNo", info.OrderNo);
            DataRow dr = dt.Rows[0];
            dr["OrderStatus"] = (int)EnumMallOrderStatus.Shipped;
            dr["LogisticsStatus"] = (int)EnumLogisticsStatus.Shipped;
            dr["LogisticsCode"] = info.LogisticsCode;
            dr["LogisticsNo"] = info.LogisticsNo;
            if (UpdateDataTable(dt) > 0)
            {
                //订单状态跟踪
                MallOrderTrackInfo trackInfo = new MallOrderTrackInfo();
                trackInfo.OrderNo = info.OrderNo;
                trackInfo.OrderStatus = (int)EnumMallOrderStatus.Shipped;
                trackInfo.LogisticsStatus = (int)EnumLogisticsStatus.Shipped;
                trackInfo.PayStatus = (int)EnumPayStatus.Paid;
                trackInfo.CreateBy = info.CreateBy;
                AddMallOrderTrack(trackInfo);
                return RT.SUCCESS;
            }
            else
                return RT.FAILED;
        }
        /// <summary>
        /// 订单跟踪表
        /// </summary>
        /// <param name="info">订单跟踪信息</param>
        /// <returns></returns>
        public int AddMallOrderTrack(MallOrderTrackInfo info)
        {
            DataTable dt = GetDataByKey("T_MallOrderTrack", "ID", 0);
            DataRow dr = dt.NewRow();
            dr["OrderNo"] = info.OrderNo;
            dr["OrderStatus"] = info.OrderStatus;
            dr["PayStatus"] = info.PayStatus;
            dr["LogisticsStatus"] = info.LogisticsStatus;
            dr["CreateBy"] = info.CreateBy;
            dr["CreateTime"] = DateTime.Now;
            dt.Rows.Add(dr);
            if (UpdateDataTable(dt) > 0)
            {
                return RT.SUCCESS;
            }
            else
            {
                return RT.FAILED;
            }
        }

        /// <summary>
        /// 确认订单收货
        /// </summary>
        /// <param name="info">确认收货信息</param>
        /// <returns></returns>
        [Transaction]
        public int ConfirmMallOrderReceived(string strMallOrderNo)
        {
            DataTable dt = GetDataByKey("T_MallOrder", "OrderNo", strMallOrderNo);
            DataRow dr = dt.Rows[0];
            dr["OrderStatus"] = (int)EnumMallOrderStatus.Received;
            dr["LogisticsStatus"] = (int)EnumLogisticsStatus.Received;
            if (UpdateDataTable(dt) > 0)
            {
                //订单状态跟踪
                MallOrderTrackInfo trackInfo = new MallOrderTrackInfo();
                trackInfo.OrderNo = strMallOrderNo;
                trackInfo.OrderStatus = (int)EnumMallOrderStatus.Received;
                trackInfo.LogisticsStatus = (int)EnumLogisticsStatus.Received;
                trackInfo.PayStatus = (int)EnumPayStatus.Paid;
                trackInfo.CreateBy = 0;
                AddMallOrderTrack(trackInfo);
                return RT.SUCCESS;
            }
            else
                return RT.FAILED;
        }

        /// <summary>
        /// 修改商品订单状态
        /// </summary>
        /// <param name="strCarOrderNo"></param>      
        /// <param name="iOrderStatus"></param>
        /// <returns></returns>
        public int UpdateMallOrder(string strMallOrderNo, int iOrderStatus)
        {
            return UpdateMallOrder(strMallOrderNo, null, iOrderStatus);
        }

        /// <summary>
        /// 修改商品订单状态
        /// </summary>
        /// <param name="strCarOrderNo"></param>
        /// <param name="strCancelReason"></param>
        /// <param name="iOrderStatus"></param>
        /// <returns></returns>
        public int UpdateMallOrder(string strMallOrderNo, string strCancelReason, int iOrderStatus)
        {
            return orderDao.UpdateMallOrder(strMallOrderNo, strCancelReason, iOrderStatus);
        }


        /// <summary>
        /// 结算订单详情
        /// </summary>
        /// <param name="strCarOrderNo"></param>      
        /// <returns></returns>
        public DataTable SettlementMallOrder(string strMallOrderNo)
        {
            return orderDao.SettlementMallOrder(strMallOrderNo);
        }
               

        /// <summary>
        /// 检查买家已付款卖家未发货(15天)/买家申请退款卖家未处理(24小时)
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public DataSet CheckSellerShip(string OrderNo)
        {
            return orderDao.CheckSellerShip(OrderNo);
        }

        #endregion

        #region 提醒发货

        /// <summary>
        /// 提醒发货列表
        /// </summary>
        /// <returns></returns>
        public int GetMallRemindSendList(Pager p, Hashtable hs)
        {
            return orderDao.GetMallRemindSendList(p, hs);
        }

        /// <summary>
        /// 提醒发货订单数量
        /// </summary>
        /// <returns></returns>
        public int GetMallRemindSendCount()
        {
            return orderDao.GetMallRemindSendCount();
        }
        
        /// <summary>
        /// 提醒发货
        /// </summary>
        /// <param name="OrderNo">订单编号</param>
        /// <returns></returns>
        public int AddMallRemindSend(string OrderNo)
        {


            DataTable dtr = GetDataByKey("T_MallRemindSend", "OrderNo", OrderNo);
            if (dtr.Rows.Count > 0) 
            {
                return RT.SUCCESS;
            }

            DataTable dt = GetDataByKey("T_MallRemindSend", "ID", 0);
            DataRow dr = dt.NewRow();
            dr["OrderNo"] = OrderNo;
            dr["CreateTime"] = DateTime.Now;
            dt.Rows.Add(dr);
            if (UpdateDataTable(dt) > 0)
            {
                return RT.SUCCESS;
            }
            else
            {
                return RT.FAILED;
            }
        }
        #endregion

        #region 全额退款管理
        /// <summary>
        /// 申请全额退款
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        [Transaction]
        public int ApplyOrderReturn(string OrderNo, string Remark)
        {
            int res = 0;
            DataTable orderDt = GetDataByKey("T_MallOrder", "OrderNo", OrderNo);

            if (orderDt.Rows.Count > 0)
            {
                //更改订单主表的订单状态
                DataRow orderDr = orderDt.Rows[0];
                if (Convert.ToInt32(orderDr["OrderStatus"]) == (int)EnumMallOrderStatus.Paid 
                    || Convert.ToInt32(orderDr["OrderStatus"]) == (int)EnumMallOrderStatus.Shipped 
                    || Convert.ToInt32(orderDr["OrderStatus"]) == (int)EnumMallOrderStatus.Received
                    || Convert.ToInt32(orderDr["OrderStatus"]) == (int)EnumMallOrderStatus.HasComment)
                {
                    orderDr["OrderStatus"] = EnumMallOrderStatus.ApplyRefund;
                    orderDr["PayStatus"] = EnumPayStatus.ApplyRefund;

                    UpdateDataTable(orderDt);

                    //订单状态跟踪
                    MallOrderTrackInfo trackInfo = new MallOrderTrackInfo();
                    trackInfo.OrderNo = OrderNo;
                    trackInfo.OrderStatus = (int)EnumMallOrderStatus.ApplyRefund;
                    trackInfo.LogisticsStatus = Convert.ToInt32(orderDr["LogisticsStatus"]);
                    trackInfo.PayStatus = (int)EnumPayStatus.ApplyRefund;
                    trackInfo.CreateBy = Convert.ToInt32(orderDr["MemberID"]);
                    AddMallOrderTrack(trackInfo);

                    //新增退款记录至订单退款表
                    DataTable orderReturnDt = GetDataByKey("T_MallOrderReturn", "ID", 0);
                    DataRow orderReturnDr = orderReturnDt.NewRow();
                    orderReturnDt.Rows.Add(orderReturnDr);
                    orderReturnDr["OrderNo"] = OrderNo;
                    orderReturnDr["Status"] = 0;
                    orderReturnDr["Reason"] = Remark;
                    orderReturnDr["ApplyTime"] = DateTime.Now;
                    res = UpdateDataTable(orderReturnDt);
                }
            }
            return res;
        }
        /// <summary>
        /// 处理退款申请-卖家同意退款
        /// </summary>
        /// <returns></returns>
        [Transaction]
        public int DealOrderReturn(string ID, string Remark, string UserID)
        {
            int res = 0;
            //更新订单退款表状态
            DataTable dt = GetDataByKey("T_MallOrderReturn", "ID", ID);
            DataRow dr;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr["ApplyTime"] = DateTime.Now;
            }
            else
            {
                dr = dt.Rows[0];
            }                       
            dr["Status"] = 1;  //卖家同意退款，等待平台管理员退款
            dr["DealBy"] = UserID;
            dr["DealTime"] = DateTime.Now;
            dr["Remark"] = Remark;
            UpdateDataTable(dt);

            DataTable orderDt = GetDataByKey("T_MallOrder", "OrderNo", dr["OrderNo"].ToString());
            DataRow orderDr = orderDt.Rows[0];

            //订单状态跟踪
            MallOrderTrackInfo trackInfo = new MallOrderTrackInfo();
            trackInfo.OrderNo = dr["OrderNo"].ToString();
            trackInfo.OrderStatus = (int)EnumMallOrderStatus.AgreeReturn;
            trackInfo.LogisticsStatus = Convert.ToInt32(orderDr["LogisticsStatus"]);
            trackInfo.PayStatus = Convert.ToInt32(orderDr["PayStatus"]);
            trackInfo.CreateBy = int.Parse(UserID);
            AddMallOrderTrack(trackInfo);

            //更新订单主表状态
            orderDr["OrderStatus"] = (int)EnumMallOrderStatus.AgreeReturn; 
            res = UpdateDataTable(orderDt);

            return res;
        }
        /// <summary>
        /// 订单退款列表
        /// </summary>
        /// <returns></returns>
        public int GetMallOrderReturnList(Pager p, Hashtable hs)
        {
            return orderDao.GetMallOrderReturnList(p, hs);
        }
        /// <summary>
        /// 查看退款详细信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataSet GetMallOrderReturnDetailsInfo(string ID, string OrderNo)
        {
            return orderDao.GetMallOrderReturnDetailsInfo(ID, OrderNo);
        }
        /// <summary>
        /// 获取订单流水信息
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        public DataSet GetMallOrderReturnTradeNo(string OrderNo)
        {
            return orderDao.GetMallOrderReturnTradeNo(OrderNo);
        }
        #endregion

        #region 售后服务管理
        /// <summary>
        /// 申请售后服务
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        [Transaction]
        public int ApplyOrderSale(Hashtable hs)
        {
            int res = 0;
            //插入数据至售后主表
            DataTable dt = GetDataByKey("T_MallOrderSale", "ID", 0);
            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);
            dr["Type"] = hs["Type"].ToString();
            dr["OrderID"] = hs["OrderID"].ToString();
            dr["ApplyNumber"] = hs["ApplyNumber"].ToString();
            dr["Status"] = 0;
            dr["ApplyTime"] = DateTime.Now;
            if (hs.ContainsKey("Description"))
            {
                dr["Description"] = hs["Description"].ToString();
            }
            if (hs.ContainsKey("Pic"))
            {
                dr["Pic"] = hs["Pic"].ToString();
            }
            int OrderSaleID = Insert(dt);
            //插入数据至售后跟踪表
            DataTable trackdt = GetDataByKey("T_MallOrderSaleTrack", "ID", 0);
            DataRow trackdr = trackdt.NewRow();
            trackdt.Rows.Add(trackdr);
            trackdr["OrderSaleID"] = OrderSaleID;
            trackdr["Status"] = 0;
            res = UpdateDataTable(trackdt);
            return res;
        }
        /// <summary>
        /// 处理售后申请
        /// </summary>
        /// <returns></returns>
        [Transaction]
        public int DealOrderSale(string ID, string UserID, string Remark, string Status)
        {
            int res = 0;
            //更新售后主表状态
            DataTable dt = GetDataByKey("T_MallOrderSale", "ID", ID);
            DataRow dr;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr["ApplyTime"] = DateTime.Now;
            }
            else
            {
                dr = dt.Rows[0];
            }
            dr["DealBy"] = UserID;
            dr["DealTime"] = DateTime.Now;
            dr["Remark"] = Remark;
            dr["Status"] = Status;
            res = UpdateDataTable(dt);
            //新增记录至售后跟踪表
            DataTable trackdt = GetDataByKey("T_MallOrderSaleTrack", "ID", 0);
            DataRow trackdr = trackdt.NewRow();
            trackdt.Rows.Add(trackdr);
            trackdr["OrderSaleID"] = ID;
            trackdr["Status"] = Status;
            res = UpdateDataTable(trackdt);
            return res;
        }
        /// <summary>
        /// 售后服务列表
        /// </summary>
        /// <returns></returns>
        public int GetOrderSaleList(Pager p, Hashtable hs)
        {
            return orderDao.GetOrderSaleList(p, hs);
        }
        public DataSet GetOrderSaleDetailsInfo(string ID)
        {
            return orderDao.GetOrderSaleDetailsInfo(ID);
        }
        /// <summary>
        /// 查看售后服务信息
        /// </summary>
        /// <param name="OrderSaleID"></param>
        /// <returns></returns>
        public DataTable GetOrderSaleInfo(string OrderSaleID)
        {
            return orderDao.GetOrderSaleInfo(OrderSaleID);
        }
        ///<summary>
        /// 获取该会员售后状态下的数量
        /// </summary>
        /// <param name="OrderStatus"></param>
        /// <returns></returns>
        public int GetSaleStatusCount(string MemberID)
        {
            return orderDao.GetSaleStatusCount(MemberID);
        }
        #endregion

        #region 支付异步通知

        /// <summary>
        /// 支付异步通知
        /// </summary>
        /// <param name="out_trade_no">商户订单号</param>
        /// <param name="notify_trade_no">支付后台返回的订单号</param>
        /// <returns></returns>
        [Transaction]
        public void PayNotify(string out_trade_no, string notify_trade_no)
        {
            DataTable dt = GetDataByKey("T_Payment", "PaymentNo", out_trade_no);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string orderNos = dr["OrderNos"].ToString();
                //异步通知成功以后，程序最后会有输出SUCCESS,为防止支付后台重发通知，所以得做判断
                if (dr["PayStatus"].ToString() != "1")
                {
                    int orderType = Convert.ToInt32(dr["OrderType"]);       //1租车，2商品
                    int payType = Convert.ToInt32(dr["PayType"]);           //1支付宝，2微信，3银联

                    if (orderType == 1 && payType == 3)   //租车
                    {
                        DataTable dtOrder = GetDataByKey("T_CarOrder", "OrderNo", orderNos);
                        DataRow drOrder = dtOrder.Rows[0];
                        //0-订单提交(租客) 15-车辆押金(未付) 20-违章押金(未付) 30-行程开始  40-订单取消(租客)   43-自动取消(时间失效)  45-订单拒接(平台)  50-事故保险  100-订单完成
                        int iOrderSts = Convert.ToInt32(drOrder["OrderStatus"]);
                        if (iOrderSts == (int)EnumCarOrderStatus.Paid)
                            drOrder["OrderStatus"] = (int)EnumCarOrderStatus.Illegal;
                        if (iOrderSts == (int)EnumCarOrderStatus.Illegal)
                            drOrder["OrderStatus"] = (int)EnumCarOrderStatus.TravelStart;
                        //更新订单主表
                        UpdateDataTable(dtOrder);
                        //添加跟踪记录
                        //**********
                    }
                    else if (orderType == 2)               //商品
                    {
                        foreach (string orderNo in orderNos.Split(','))
                        {
                            DataTable dtOrder = GetDataByKey("T_MallOrder", "OrderNo", orderNo);
                            DataRow drOrder = dtOrder.Rows[0];
                            drOrder["PayStatus"] = EnumPayStatus.Paid;
                            drOrder["OrderStatus"] = EnumMallOrderStatus.Paid;
                            drOrder["PayTime"] = DateTime.Now;
                            //更新订单主表
                            UpdateDataTable(dtOrder);
                            //添加跟踪记录
                            MallOrderTrackInfo info = new MallOrderTrackInfo();
                            info.OrderNo = orderNo;
                            info.OrderStatus = (int)EnumMallOrderStatus.Paid;
                            info.PayStatus = (int)EnumPayStatus.Paid;
                            info.LogisticsStatus = (int)EnumLogisticsStatus.ToShip;
                            info.CreateBy = 0;
                            AddMallOrderTrack(info);
                        }
                    }
                    dr["NotifyTradeNo"] = notify_trade_no;
                    dr["PayTime"] = DateTime.Now;
                    dr["PayStatus"] = 1;
                    //更新支付流水表
                    UpdateDataTable(dt);
                }
            }
        }

        #endregion

        #region 退款异步通知

        /// <summary>
        /// 退款异步通知
        /// </summary>
        /// <param name="batch_no">退款批次号</param>
        /// <returns></returns>
        [Transaction]
        public void RefundNotify(string batch_no)
        {
            DateTime now = DateTime.Now;
            DataTable dt = GetDataByKey("T_Refund", "BatchNo", batch_no);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //更新退款流水表
                    dr["RefundTime"] = now;
                    dr["RefundStatus"] = 1;
                    UpdateDataTable(dt);
                }
            }
        }

        #endregion
    }
}