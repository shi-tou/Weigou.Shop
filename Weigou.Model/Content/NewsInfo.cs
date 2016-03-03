using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Weigou.Model.Content
{
    /// <summary>
    /// 文章实体
    /// </summary>
    public class NewsInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public int ID;
        /// <summary>
        /// 类别
        /// </summary>
        public int Type;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title;
        /// <summary>
        /// 内容
        /// </summary>
        public string Description;
        /// <summary>
        /// 图片
        /// </summary>
        public string Picture;
        /// <summary>
        /// 浏览量
        /// </summary>
        public int Hits;
        /// <summary>
        /// 状态
        /// </summary>
        public int Status;
        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime;
    }
}
