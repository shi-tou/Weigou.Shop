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
using Weigou.Model.Content;

namespace Weigou.Service
{
    public class ContentService : BaseService, IContentService
    {
        private IContentDao contentDao;
        public IContentDao ContentDao
        {
            set
            {
                contentDao = value;
            }
        }
        /// <summary>
        /// 友链列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetFriendLinkList(Pager p, Hashtable hs)
        {
            return contentDao.GetFriendLinkList(p, hs);
        }
        /// <summary>
        /// 公告列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetNotifyList(Pager p, Hashtable hs)
        {
            return contentDao.GetNotifyList(p, hs);
        }
        /// <summary>
        ///  敏感词列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetSwearWordList(Pager p, Hashtable hs)
        {
            return contentDao.GetSwearWordList(p, hs);
        }
        

        #region 文章管理
        /// <summary>
        ///  资讯列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int SaveNewsInfo(NewsInfo info, List<string> keywords)
        {
            DataTable dt = GetDataByKey("T_News", "ID", info.ID);
            DataRow dr = null;
            if (dt.Rows.Count == 0)
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr["CreateBy"] = info.CreateBy;
                dr["CreateTime"] = DateTime.Now;
            }
            else
            {
                dr = dt.Rows[0];
            }
            dr["Type"] = info.Type;
            dr["Title"] = info.Title;
            dr["Description"] = info.Description;
            dr["Picture"] = info.Picture;
            int newsID = 0;
            int res = 0;
            if (info.ID == 0)
            {
                res = Insert(dt);
                newsID = res;
            }
            else
            {
                res = UpdateDataTable(dt);
                newsID = info.ID;
            }
            if (res > 0)
            {
                Delete("T_NewsKeyword", "NewsID", newsID);
                DataTable dtKey = GetDataByKey("T_NewsKeyword", "ID", 0);
                foreach (string k in keywords)
                {
                    DataRow drKey = dtKey.NewRow();
                    drKey["NewsID"] = newsID;
                    drKey["KeywordID"] = k;
                    dtKey.Rows.Add(drKey);
                }
                UpdateDataTable(dtKey);
            }
            return res;
        }
        /// <summary>
        ///  资讯列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetNewsList(Pager p, Hashtable hs)
        {
            return contentDao.GetNewsList(p, hs);
        }
        /// <summary>
        ///  文章分类
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetNewsTypeList(Pager p, Hashtable hs)
        {
            return contentDao.GetNewsTypeList(p, hs);
        }
        /// <summary>
        /// 根据微信关键词取文章列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetNewsListByKey(string key)
        {
            return contentDao.GetNewsListByKey(key);
        }
        #endregion

        #region 广告Banner管理
        /// <summary>
        ///  Banner图片列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetBannerList(Pager p, Hashtable hs)
        {
            return contentDao.GetBannerList(p, hs);
        }
        #endregion
    }
}
