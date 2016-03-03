using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Model.Enum;

namespace Weigou.Model
{
    public class CommentInfo
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 评论类别（ 1-车辆 2-商品）
        /// </summary>
        public int Type;
        /// <summary>
        /// 上级记录ID
        /// </summary>
        public int ParentID;
        /// <summary>
        /// 订单主键
        /// </summary>
        public int OrderID;
        /// <summary>
        /// 评论目标ID
        /// </summary>
        public int TargetID;
        /// <summary>
        /// 会员ID
        /// </summary>
        public int MemberID;
        /// <summary>
        /// 评分（1-5 星）
        /// </summary>
        public int Star;
        /// <summary>
        /// 评论内容/回复内容
        /// </summary>
        public string Content;
        /// <summary>
        /// 评论时间
        /// </summary>
        public string CreateTime;
        /// <summary>
        /// 回复人
        /// </summary>
        public int ReplyBy;
    }
}