using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Weigou.Api.Base;
using System.ComponentModel;
using System.Collections;
using Weigou.Common;
using System.Data;
using Weigou.Api.Model;
using Weigou.Pay.Alipay;
using Weigou.Pay.Alipay.Pay;
using Weigou.Pay.Weixin;
using Weigou.Pay.Weixin.Pay;
using Weigou.Pay.Unionpay.Pay;
using System.Text;

namespace Weigou.Api.SDK
{
    /// <summary>
    /// 支付模块Api
    /// </summary>
    public class PaymentApi : BaseApi
    {
        #region 方法描述
        public const string GetAlipayInfo_Desc = "OrderNo=订单号&OrderType=订单类型(1:车辆，2:商品)&OrderPrice=订单金额";
        public const string GetWeixinInfo_Desc = "OrderNo=订单号&OrderType=订单类型(1:租车，2:商城)&OrderPrice=订单金额";
        public const string GetUnionpayInfo_Desc = "OrderNo=订单号&OrderType=订单类型(1:租车，2:商城)&OrderPrice=订单金额";
        #endregion

        #region 支付宝支付
        /// <summary>
        /// 获取支付宝信息
        /// </summary>
        [Description(GetAlipayInfo_Desc)]
        public Result GetAlipayInfo(MyHashtable hs)
        {
            string orderNo = GetParam(hs, "OrderNo", "");
            int orderType = GetParam(hs, "OrderType", 0);
            string paymentNo = Utils.CreateOrderNo();
            string dOrderPrice = GetParam(hs, "OrderPrice", "0");
            if (string.IsNullOrEmpty(orderNo))
            {
                result.msg = "订单不存在！";
                result.status = RT.RESULT_NOT_EXIST;
                return result;
            }

            #region 数据
            //构造待签名数据
            SortedDictionary<string, string> dic = new SortedDictionary<string, string>();
            dic.Add("partner", AlipayConfig.Partner);
            dic.Add("seller_id", AlipayConfig.Seller);
            dic.Add("out_trade_no", paymentNo);
            dic.Add("subject", "为订单" + paymentNo + "进行付款");
            dic.Add("body", "为订单" + paymentNo + "进行付款");

            // dic.Add("total_fee", orderprice); 真实环境请替换
            dic.Add("total_fee", "0.01");//订单金额,测试金额默认0.01元

            dic.Add("notify_url", AlipayConfig.Notify_Url);
            dic.Add("service", "mobile.securitypay.pay");
            dic.Add("payment_type", "1");
            dic.Add("input_charset", "utf-8");
            dic.Add("it_b_pay", "30m");

            //生成签名
            dic.Add("sign", AlipayCore.CreateSign(dic));
            dic.Add("sign_type", "RSA");
            dic.Add("privatekey", AlipayConfig.Private_key.Replace("+", "@"));
            #endregion

            //插入数据至支付流水表
            DataTable dt = orderService.GetDataByKey("T_Payment", "ID", 0);
            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);
            dr["PaymentNo"] = paymentNo;
            dr["OrderType"] = orderType;
            dr["OrderNos"] = orderNo;
            dr["Amount"] = dOrderPrice;
            dr["PayType"] = "1"; //支付方式为支付宝
            dr["PayTime"] = DateTime.Now;
            dr["PaySource"] = 2;
            dr["PayStatus"] = 0;
            dr["CreateTime"] = DateTime.Now;
            orderService.UpdateDataTable(dt);

            result.status = RT.SUCCESS;
            result.data = dic;
            return result;
        }
        #endregion

        #region 微信支付
        /// <summary>
        /// 获取微信信息
        /// </summary>
        [Description(GetWeixinInfo_Desc)]
        public Result GetWeixinInfo(MyHashtable hs)
        {
            string orderNo = GetParam(hs, "OrderNo", "");
            int iOrderType = GetParam(hs, "OrderType", 0);
            string paymentNo = Utils.CreateOrderNo();
            string dOrderPrice = GetParam(hs, "OrderPrice", "");
            if (string.IsNullOrEmpty(orderNo))
            {
                result.msg = "订单不存在！";
                result.status = RT.RESULT_NOT_EXIST;
                return result;
            }
            //构造待签名数据
            SortedDictionary<string, string> dic = new SortedDictionary<string, string>();
            dic.Add("appid", WxPayConfig.AppID);//公众账号ID
            dic.Add("mch_id", WxPayConfig.MchID);//商户号
            dic.Add("nonce_str", WxPayApi.GenerateNonceStr());//随机字符串
            dic.Add("body", "为订单" + paymentNo + "进行付款");//商品描述
            dic.Add("out_trade_no", paymentNo);//商户订单号

            //dic.Add("total_fee", (Convert.ToDecimal(orderprice)*100).ToString());//订单总金额 真实请替换
            dic.Add("total_fee", "0.1");//订单金额,测试金额默认1分

            dic.Add("spbill_create_ip", WxPayConfig.Ip);//终端IP
            dic.Add("notify_url", WxPayConfig.PayNotify_Url);//异步通知地址
            dic.Add("trade_type", "APP");//交易类型
            dic.Add("key", WxPayConfig.Key);//支付密钥
            //dic.Add("apikey", WxPayConfig.ApiKey);//支付密钥

            //插入数据至支付流水表
            DataTable dt = orderService.GetDataByKey("T_Payment", "ID", 0);
            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);
            dr["PaymentNo"] = paymentNo;
            dr["OrderType"] = iOrderType;
            dr["OrderNos"] = orderNo;
            dr["Amount"] = dOrderPrice;
            dr["PayType"] = "2"; //支付方式为微信
            dr["PayTime"] = DateTime.Now;
            dr["PaySource"] = 2;
            dr["PayStatus"] = 0;
            dr["CreateTime"] = DateTime.Now;
            orderService.UpdateDataTable(dt);

            result.status = RT.SUCCESS;
            result.data = dic;
            return result;
        }
        #endregion

        #region 银联支付

        /// <summary>
        /// 获取银联信息（消费）
        /// </summary>
        [Description(GetUnionpayInfo_Desc)]
        public Result GetUnionpayInfo(MyHashtable hs)
        {
            string orderNo = GetParam(hs, "OrderNo", "");
            int iOrderType = GetParam(hs, "OrderType", 0);
            string paymentNo = Utils.CreateOrderNo();
            string dOrderPrice = GetParam(hs, "OrderPrice", "0");
            if (string.IsNullOrEmpty(orderNo))
            {
                result.msg = "订单不存在！";
                result.status = RT.RESULT_NOT_EXIST;
                return result;
            }
            //构造待签名数据
            Dictionary<string, string> param = new Dictionary<string, string>();
            //填写参数

            param["version"] = "5.0.0";//版本号
            param["encoding"] = "UTF-8";//编码方式
            param["certId"] = CertUtil.GetSignCertId();//证书ID
            param["txnType"] = TxnType.Consume;//交易类型
            param["txnSubType"] = "01";//交易子类
            param["bizType"] = "000000";//业务类型
            param["backUrl"] = SDKConfig.BackTransUrl;  //后台异步通知地址	
            param["signMethod"] = "01";//签名方法
            param["channelType"] = "08";//渠道类型，05-语音 07-互联网，08-手机
            param["accessType"] = "0";//接入类型。0：普通商户直连接入 2：平台类商户接入
            param["merId"] = SDKConfig.MerId;//商户号，请改成自己的商户号
            param["orderId"] = paymentNo;//商户订单号,业务订单号
            param["txnTime"] = DateTime.Now.ToString("yyyyMMddHHmmss");//订单发送时间
            //param["txnAmt"]=(Convert.ToDecimal(orderprice)*100).ToString();//订单总金额 真实请替换
            param["txnAmt"] = "1";//交易金额，单位分
            param["currencyCode"] = "156";//交易币种
            param["orderDesc"] = "为订单" + paymentNo + "进行付款";//订单描述，可不上送，上送时控件中会显示该信息

            SDKUtil.Sign(param, Encoding.UTF8);  // 签名
            //Response.Write("\n" + "请求报文=[" + SDKUtil.PrintDictionaryToString(param) + "]\n");

            // 初始化通信处理类
            HttpClient hc = new HttpClient(SDKConfig.AppRequestUrl);
            //// 发送请求获取通信应答
            int status = hc.Send(param, Encoding.UTF8);
            // 返回结果
            string results = hc.Result;
            if (status == 200)
            {
                //Response.Write("返回报文=[" + results + "]\n");
                Dictionary<string, string> resData = SDKUtil.CoverstringToDictionary(results);

                string respcode = resData["respCode"];

                if (SDKUtil.Validate(resData, Encoding.UTF8))
                {
                    //插入数据至支付流水表
                    DataTable dt = orderService.GetDataByKey("T_Payment", "ID", 0);
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                    dr["PaymentNo"] = paymentNo;
                    dr["OrderType"] = iOrderType;
                    dr["OrderNos"] = orderNo;
                    dr["Amount"] = dOrderPrice;
                    dr["PayType"] = "3"; //支付方式为银联
                    dr["PayTime"] = DateTime.Now;
                    dr["PaySource"] = 2;
                    dr["PayStatus"] = 0;
                    dr["CreateTime"] = DateTime.Now;
                    orderService.UpdateDataTable(dt);
                    try
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("tn", resData["tn"]);
                        result.status = RT.SUCCESS;
                        result.data = dic;
                    }
                    catch
                    {
                        result.status = RT.FAILED;
                        result.msg = resData["respMsg"];
                    }
                }
                else
                {
                    result.status = RT.FAILED;
                    result.msg = "商户端验证返回报文签名失败\n";
                }
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "请求失败\n" + "返回报文=[" + results + "]\n";
            }
            return result;
        }

        /// <summary>
        /// 获取银联信息（预授权支付）
        /// </summary>
        [Description(GetUnionpayInfo_Desc)]
        public Result GetUnionpayPrePayInfo(MyHashtable hs)
        {
            string orderNo = GetParam(hs, "OrderNo", "");
            int iOrderType = GetParam(hs, "OrderType", 0);
            string paymentNo = Utils.CreateOrderNo();
            string dOrderPrice = GetParam(hs, "OrderPrice", "0");
            if (string.IsNullOrEmpty(orderNo))
            {
                result.msg = "订单不存在！";
                result.status = RT.RESULT_NOT_EXIST;
                return result;
            }
            //构造待签名数据
            Dictionary<string, string> param = new Dictionary<string, string>();
            //填写参数

            param["version"] = "5.0.0";//版本号
            param["encoding"] = "UTF-8";//编码方式
            param["certId"] = CertUtil.GetSignCertId();//证书ID
            param["txnType"] = TxnType.PrePay;//交易类型
            param["txnSubType"] = "01";//交易子类 01：预授权
            param["bizType"] = "000000";//业务类型    
            param["backUrl"] = SDKConfig.BackTransUrl;  //后台异步通知地址	
            param["signMethod"] = "01";//签名方法,01：表示采用RSA
            param["channelType"] = "08";//渠道类型，07-PC，08-手机
            param["accessType"] = "0";//接入类型,0：商户直连接入 1：收单机构接入 2：平台商户接入 
            param["merId"] = SDKConfig.MerId;//商户号，请改成自己的商户号
            Utils.SaveLog("merid",SDKConfig.MerId);
            param["orderId"] = paymentNo;//商户订单号
            param["txnTime"] = DateTime.Now.ToString("yyyyMMddHHmmss");//订单发送时间
            //param["txnAmt"]=(Convert.ToDecimal(orderprice)*100).ToString();//订单总金额 真实请替换
            param["txnAmt"] = "1";//交易金额，单位分
            param["currencyCode"] = "156";//交易币种
            param["orderDesc"] = "为订单" + paymentNo + "进行付款";//订单描述，可不上送，上送时控件中会显示该信息

            SDKUtil.Sign(param, Encoding.UTF8);  // 签名
            //Response.Write("\n" + "请求报文=[" + SDKUtil.PrintDictionaryToString(param) + "]\n");

            // 初始化通信处理类
            HttpClient hc = new HttpClient(SDKConfig.AppRequestUrl);
            //// 发送请求获取通信应答
            int status = hc.Send(param, Encoding.UTF8);
            // 返回结果
            string results = hc.Result;
            if (status == 200)
            {
                Dictionary<string, string> resData = SDKUtil.CoverstringToDictionary(results);
                string respcode = resData["respCode"];
                if (SDKUtil.Validate(resData, Encoding.UTF8))
                {
                    //插入数据至支付流水表
                    DataTable dt = orderService.GetDataByKey("T_Payment", "ID", 0);
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(dr);
                    dr["PaymentNo"] = paymentNo;
                    dr["OrderType"] = iOrderType;
                    dr["OrderNos"] = orderNo;
                    dr["Amount"] = dOrderPrice;
                    dr["PayType"] = "3"; //支付方式为银联
                    dr["PayTime"] = DateTime.Now;
                    dr["PaySource"] = 2;
                    dr["PayStatus"] = 0;
                    dr["CreateTime"] = DateTime.Now;
                    orderService.UpdateDataTable(dt);
                    try
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("tn", resData["tn"]);
                        result.status = RT.SUCCESS;
                        result.data = dic;
                    }
                    catch
                    {
                        result.status = RT.FAILED;
                        result.msg = resData["respMsg"];
                    }
                }
                else
                {
                    result.status = RT.FAILED;
                    result.msg = "商户端验证返回报文签名失败\n";
                }
            }
            else
            {
                result.status = RT.FAILED;
                result.msg = "请求失败\n" + "返回报文=[" + results + "]\n";
            }
            return result;
        }

        #endregion
    }
}