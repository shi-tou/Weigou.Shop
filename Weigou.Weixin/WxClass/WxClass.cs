using Senparc.Weixin.MP.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Weigou.Common;
using Weigou.Service;

namespace Weigou.Weixin
{

    /// <summary>
    /// 微信数据
    /// </summary>
    public class WxClass
    {
        //注入
        public static IContentService contentService;
        public IContentService ContentService
        {
            set { contentService = value; }
        }
        /// <summary>
        /// 根据微信关键词获取文章
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<Article> GetNewsListByKey(string key)
        {
            List<Article> list = new List<Article>();
            DataTable dt = contentService.GetNewsListByKey(key);
            foreach (DataRow dr in dt.Rows)
            {
                Article info = new Article();
                info.Title = Convert.ToString(dr["Title"]);
                info.Description = Convert.ToString(dr["Description"]);
                info.PicUrl = GetServerPicture(Convert.ToString(dr["Picture"]));
                info.Url = "http://www.baidu.com";
                list.Add(info);
            }
            return list;
        }
        #region 图片相关
        /// <summary>
        /// 返回服务器图片路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetServerPicture(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return "";
            }
            return Utils.GetConfig("ServerPicPath") + url;
        }
        #endregion
    }
}