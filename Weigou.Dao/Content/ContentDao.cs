using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Data.Common;
using System.Data;
using System.Collections;
using Weigou.Common;
using Weigou.Model;

namespace Weigou.Dao
{
    public class ContentDao : BaseDao, IContentDao
    {
        /// <summary>
        /// 友链列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetFriendLinkList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as CreateName from T_FriendLink a
                            left join T_User b on b.ID=a.CreateBy
                            where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.ContainsKey("Title"))
            {
                sql += "  and a.Title like @Title";
                param.AddWithValue("Title", "%" + hs["Title"].ToString() + "%");
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
        /// <summary>
        /// 公告列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetNotifyList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*, b.Name as MemberName,c.Name as CreateName from T_Notify a
                            left join T_Member b on b.ID=a.MemberID
                            left join T_User c on c.ID=a.CreateBy
                            where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.ContainsKey("Type"))
            {
                sql += "  and a.Type = @Type";
                param.AddWithValue("Type", hs["Type"].ToString());
            }
            if (hs.ContainsKey("Title"))
            {
                sql += "  and a.Title like @Title";
                param.AddWithValue("Title", "%" + hs["Title"].ToString() + "%");
            }
            if (hs.ContainsKey("Content"))
            {
                sql += "  and a.Content like @Content";
                param.AddWithValue("Content", "%" + hs["Content"].ToString() + "%");
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
        /// <summary>
        /// 敏感词列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetSwearWordList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as CreateName from T_SwearWord a
                            left join T_User b on b.ID=a.CreateBy
                            where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.ContainsKey("SwearWord"))
            {
                sql += "  and a.SwearWord like @SwearWord";
                param.AddWithValue("SwearWord", "%" + hs["SwearWord"].ToString() + "%");
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
        #region 文章管理
        /// <summary>
        ///  资讯列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetNewsList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as CreateName,c.Name as TypeName from T_News a
                            left join T_User b on b.ID=a.CreateBy
                            inner join T_NewsType c on c.ID=a.Type
                            where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.ContainsKey("Title"))
            {
                sql += "  and a.Title like @Title";
                param.AddWithValue("Title", "%" + hs["Title"].ToString() + "%");
            }
            if (hs.ContainsKey("Type"))
            {
                sql += "  and a.Type=@Type";
                param.AddWithValue("Type", hs["Type"].ToString());
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
        /// <summary>
        ///  文章分类
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetNewsTypeList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as ParentName from T_NewsType a
                            left join T_NewsType b on b.ID=a.ParentID
                            where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("ParentID"))
            {
                sql += " and a.ParentID=@ParentID";
                param.AddWithValue("ParentID", hs["ParentID"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
        /// <summary>
        /// 根据微信关键词取文章列表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetNewsListByKey(string key)
        {
            string sql = @"select top 8 * from T_News where ID in
                            (select NewsID from T_NewsKeyword a
                                inner join T_WeixinKeyword b on b.ID=a.KeywordID
                                where b.Name like @Keyword
                            ) order by CreateTime desc
                            ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("Keyword", "%" + key + "%");
            return AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
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
            string sql = @"select a.*,b.Name as CreateName,c.Name as TypeName from T_Banner a
                            left join T_User b on b.ID=a.CreateBy
                            inner join T_BaseData c on c.Value=a.Type and ParentID=" + BaseDataConst.BannerType.ToString() + @"
                            where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.ContainsKey("Title"))
            {
                sql += "  and a.Title like @Title";
                param.AddWithValue("Title", "%" + hs["Title"].ToString() + "%");
            }
            if (hs.ContainsKey("Type"))
            {
                sql += "  and a.Type=@Type";
                param.AddWithValue("Type", hs["Type"].ToString());
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
         #endregion
    }
}
