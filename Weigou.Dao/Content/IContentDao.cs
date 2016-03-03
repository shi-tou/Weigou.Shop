using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using Weigou.Common;

namespace Weigou.Dao
{
    public interface IContentDao
    {
        /// <summary>
        /// 友链列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetFriendLinkList(Pager p, Hashtable hs);
        /// <summary>
        /// 公告列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetNotifyList(Pager p, Hashtable hs);
        /// <summary>
        ///  敏感词列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetSwearWordList(Pager p, Hashtable hs);

        #region 文章管理
        /// <summary>
        ///  文章列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetNewsList(Pager p, Hashtable hs);
        /// <summary>
        ///  文章分类
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetNewsTypeList(Pager p, Hashtable hs);
        /// <summary>
        /// 根据微信关键词取文章列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        DataTable GetNewsListByKey(string key);
        #endregion

        #region 广告Banner管理
        /// <summary>
        ///  Banner图片列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetBannerList(Pager p, Hashtable hs);
        #endregion
    }
}
