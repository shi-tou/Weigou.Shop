using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Config
{
    /// <summary>
    /// 微信自动回复 配置信息类
    /// </summary>
    [Serializable]
    public class WeixinReplyConfigInfo : IConfigInfo
    {
        #region 私有字段

        private string subscribeReply;
        /// <summary>
        /// 关注回复内容 
        /// </summary>
        public string SubscribeReply
        {
            get { return subscribeReply; }
            set { subscribeReply = value; }
        }

        private string defaultReply;
        /// <summary>
        /// 默认回复内容
        /// </summary>
        public string DefaultReply
        {
            get { return defaultReply; }
            set { defaultReply = value; }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailConfigInfo"/> class.
        /// </summary>
        public WeixinReplyConfigInfo()
        {
        }

    }
}
