using Weigou.Common;
using Senparc.Weixin.MessageHandlers;
using Senparc.Weixin.MP;
using Senparc.Weixin.XmlUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weigou.Weixin
{
    /// <summary>
    /// 微信处理服务类
    /// </summary>
    public class WxService 
    {
        /// <summary>
        /// 微信令牌Token
        /// </summary>
        private string Token
        {
            get { return Utils.GetConfig("WeixinToken"); }
        }
        /// <summary>
        /// 请求
        /// </summary>
        private HttpRequest Request { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="request"></param>
        public WxService(HttpRequest request)
        {
            this.Request = request;
        }
        /// <summary>
        /// 处理请求，产生响应
        /// </summary>
        /// <returns></returns>
        public string Response()
        {
            string method = Request.HttpMethod.ToUpper();
            //验证签名
            if (method == "GET")
            {
                string signature = Request.QueryString["signature"];//签名
                string timestamp = Request.QueryString["timestamp"];//时间戳
                string nonce = Request.QueryString["nonce"];//现时标识
                string echostr = Request.QueryString["echostr"];//验证成功后原样返回信息

                if (CheckSignature.Check(signature, timestamp, nonce, Token))
                {
                    return echostr;
                }
                else
                {
                    return "error";
                }
            }

            //处理消息
            if (method == "POST")
            {
                WxMessageHandler messageHandler = new WxMessageHandler(XmlUtility.Convert(Request.InputStream));
                messageHandler.Execute();
                string responseXml = messageHandler.ResponseDocument.ToString();
                Utils.SaveLog("responseXml", responseXml);
                return messageHandler.ResponseDocument.ToString();
            }
            else
            {
                return "无法处理";
            }
        }
    }
}