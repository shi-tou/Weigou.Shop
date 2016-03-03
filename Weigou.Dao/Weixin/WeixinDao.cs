using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Data.Common;
using Weigou.Common;
using System.Data;
using System.Collections;
using Weigou.Model.Enum;

namespace Weigou.Dao
{
    public class WeixinDao : BaseDao, IWeixinDao
    {

        /// </summary>
        /// 获取微信菜单
        /// </summary>
        public DataTable GetWeixinMenuList(Hashtable hs)
        {
            string sql = @"select a.*, b.Name as ParentName, isnull(c.Count,0) as SubMenuCount from T_WeixinMenu a
                            left join T_WeixinMenu b on b.ID=a.ParentID
                            left join (select ParentID, COUNT(*) as Count from T_WeixinMenu group by ParentID) c on c.ParentID=a.ID
                            where 1=1 ";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("ParentID"))
            {
                sql += " and a.ParentID=@ParentID";
                param.AddWithValue("ParentID", hs["ParentID"]);
            }
            DataTable dt = AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
            dt.TableName = "T_WeixinMenu";
            return dt;
        }
        /// <summary>
        /// 获取微信关键词 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetWeixinKeywordList(Pager p, Hashtable hs)
        {
            string sql = "select * from T_WeixinKeyword where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.Contains("Name"))
            {
                sql += " and Name like @Name";
                param.AddWithValue("Name", hs["Name"]);
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return 0;
        }
    }
}
