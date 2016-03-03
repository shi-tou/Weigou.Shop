using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Weigou.Service;
using Weigou.Common;
using System.Text;
using System.Web.SessionState;
using Weigou.Model;
using Weigou.Model.Enum;
using System.Collections.Generic;

namespace Weigou.Admin.Ajax
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Content : BaseAjax, IHttpHandler, IRequiresSessionState
    {
        #region 友链管理
        /// <summary>
        /// 友链列表
        /// </summary>
        public void GetFriendLinkList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            string title = GetRequest("Title", "");
            if (!string.IsNullOrEmpty(title))
            {
                hs.Add("Title", title);
            }
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            contentService.GetFriendLinkList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteFriendLink(HttpContext hc)
        {
            int id = GetRequest("ID", 0);
            int res = contentService.Delete("T_FriendLink", "ID", id);
            ResponseWrite(hc, res > 0 ? RT.SUCCESS : RT.FAILED);
        }
        #endregion

        #region 公告管理
        /// <summary>
        /// 公告列表
        /// </summary>
        public void GetNotifyList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            int type = GetRequest("Type", 1);
            string title = GetRequest("Title", "");
            string content = GetRequest("Content", "");
            hs.Add("Type", type);
            if (!string.IsNullOrEmpty(title))
            {
                hs.Add("Title", title);
            }
            if (!string.IsNullOrEmpty(content))
            {
                hs.Add("Content", content);
            }
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            contentService.GetNotifyList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteNotify(HttpContext hc)
        {
            int id = GetRequest("ID", 0);
            int res = contentService.Delete("T_Notify", "ID", id);
            ResponseWrite(hc, res > 0 ? RT.SUCCESS : RT.FAILED);
        }
        #endregion

        #region 敏感词管理
        /// <summary>
        /// 敏感词管理
        /// </summary>
        public void GetSwearWordList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            string swearword = GetRequest("SwearWord", "");
            if (!string.IsNullOrEmpty(swearword))
            {
                hs.Add("SwearWord", swearword);
            }
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            contentService.GetSwearWordList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 删除敏感词
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteSwearWord(HttpContext hc)
        {
            int id = GetRequest("ID", 0);
            int res = contentService.Delete("T_SwearWord", "ID", id);
            ResponseWrite(hc, res > 0 ? RT.SUCCESS : RT.FAILED);
        }
        #endregion

        #region 文章管理
        /// <summary>
        /// 文章分类列表
        /// </summary>
        /// <param name="hc"></param>
        public void GetNewsTypeList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            Pager p = new Pager(999, 1, "a.ID desc");
            contentService.GetNewsTypeList(p, hs);
            strJson = Utils.CreateTreeJsonInt(p.DataSource, "ID", "ParentID");
            ResponseWrite(hc);
        }
        /// <summary>
        /// 资讯列表
        /// </summary>
        public void GetNewsList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            string title = GetRequest("Title", "");
            int type = GetRequest("Type", 0);
            if (!string.IsNullOrEmpty(title))
            {
                hs.Add("Title", title);
            }
            if (type > 0)
            {
                hs.Add("Type", type);
            }
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            contentService.GetNewsList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 删除资讯
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteNews(HttpContext hc)
        {
            int id = GetRequest("ID", 0);
            int res = contentService.Delete("T_Info", "ID", id);
            ResponseWrite(hc, res > 0 ? RT.SUCCESS : RT.FAILED);
        }
        #endregion

        #region Banner图管理
        /// <summary>
        /// Banner图列表
        /// </summary>
        public void GetBannerList(HttpContext hc)
        {
            Hashtable hs = new Hashtable();
            string type = GetRequest("Type", "");
            if (!string.IsNullOrEmpty(type))
            {
                hs.Add("Type", type);
            }
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            contentService.GetBannerList(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 删除Banner图
        /// </summary>
        /// <param name="hc"></param>
        public void DeleteBanner(HttpContext hc)
        {
            int id = GetRequest("ID", 0);
            int res = contentService.Delete("T_Banner", "ID", id);
            ResponseWrite(hc, res > 0 ? RT.SUCCESS : RT.FAILED);
        }
        #endregion
    }
}
