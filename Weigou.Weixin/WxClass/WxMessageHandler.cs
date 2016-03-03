using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP;
using System.Xml.Linq;
using Weigou.Config;
using System.Data;

namespace Weigou.Weixin
{
    /// <summary>
    /// 自定义微信消息处理类
    /// </summary>
    public class WxMessageHandler : MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>
    {
        /// <summary>
        /// 微信自动回复配置
        /// </summary>
        private WeixinReplyConfigInfo WeixinReplyConfigInfo = WeixinReplyConfigs.GetConfig();
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestDoc"></param>
        public WxMessageHandler(XDocument requestDoc)
            : base(requestDoc)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestMessage"></param>
        public WxMessageHandler(RequestMessageBase requestMessage)
            : base(requestMessage)
        {
        }

        /// <summary>
        /// 重写【默认】消息处理事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responeMessage = this.CreateResponseMessage<ResponseMessageText>();
            responeMessage.Content = WeixinReplyConfigInfo.DefaultReply;
            return responeMessage;
        }
        /// <summary>
        /// 重写【文本】消息处理事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            string key = requestMessage.Content.Trim();//关键字
            var responeMessage = this.CreateResponseMessage<ResponseMessageNews>();
            WxClass wxClass = new WxClass();
            List<Article> list = wxClass.GetNewsListByKey(key);
            responeMessage.Articles = list;
            responeMessage.ArticleCount = list.Count;
            responeMessage.CreateTime = DateTime.Now;
            responeMessage.ToUserName = responeMessage.FromUserName;
            return responeMessage;
        }
        /// <summary>
        /// 重写【位置】消息处理事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_LocationSelectRequest(RequestMessageEvent_Location_Select requestMessage)
        {
            var responeMessage = this.CreateResponseMessage<ResponseMessageText>();
            responeMessage.Content = "OnEvent_LocationSelectRequest";
            return responeMessage;
        }
        /// <summary>
        /// 重写【订阅】消息事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            var responeMessage = this.CreateResponseMessage<ResponseMessageText>();
            responeMessage.Content = WeixinReplyConfigInfo.SubscribeReply;
            return responeMessage;
        }
        /// <summary>
        /// 重写【退订】消息事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            var responeMessage = this.CreateResponseMessage<ResponseMessageText>();
            responeMessage.Content = "OnEvent_UnsubscribeRequest";
            return responeMessage;
        }
        /// <summary>
        /// 重写【点击】消息事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            var responeMessage = this.CreateResponseMessage<ResponseMessageText>();
            responeMessage.Content = "OnEvent_ClickRequest,EventKey:" + requestMessage.EventKey;
            return responeMessage;
        }

        
    }
}