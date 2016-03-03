using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Data.Common;
using System.Data;
using System.Collections;
using Weigou.Common;

namespace Weigou.Dao
{
    public class SmsDao : BaseDao, ISmsDao
    {
        /// <summary>
        /// 短信模板
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetSmsTemplateList(Pager p, Hashtable hs)
        {
            string sql = @"select * from T_SmsTemplate where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.ContainsKey("Code"))
            {
                sql += "  and Code like @Code";
                param.AddWithValue("Code", "%" + hs["Code"].ToString() + "%");
            }
            if (hs.ContainsKey("Content"))
            {
                sql += "  and Content like @Content";
                param.AddWithValue("Content", "%" + hs["Content"].ToString() + "%");
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
        /// <summary>
        /// 短信记录
        /// </summary>
        public int GetSmsLogList(Pager p, Hashtable hs)
        {
            string sql = @"select a.*,b.Name as CreateUser from T_SmsSend a 
                            left join T_User b on b.ID=a.CreateBy
                            where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            if (hs.ContainsKey("MobileNo"))
            {
                sql += "  and a.MobileNo like @MobileNo";
                param.AddWithValue("MobileNo", "%" + hs["MobileNo"].ToString() + "%");
            }
            if (hs.ContainsKey("Content"))
            {
                sql += "  and a.Content like @Content";
                param.AddWithValue("Content", "%" + hs["Content"].ToString() + "%");
            }
            //if (hs.ContainsKey("Status"))
            //{
            //    sql += "  and a.Status = @Status";
            //    param.AddWithValue("Status", hs["Status"].ToString());
            //}
            //if (hs.ContainsKey("SendType"))
            //{
            //    sql += "  and a.SendType = @SendType";
            //    param.AddWithValue("SendType", hs["SendType"].ToString());
            //}
            //if (hs.ContainsKey("Source"))
            //{
            //    sql += "  and a.Source = @Source";
            //    param.AddWithValue("Source", hs["Source"].ToString());
            //}
            if (hs.Contains("MinTime"))
            {
                sql += " and datediff(day,a.CreateTime,@MinTime)<=0 ";
                param.AddWithValue("MinTime", hs["MinTime"].ToString());
            }
            if (hs.Contains("MaxTime"))
            {
                sql += " and datediff(day,a.CreateTime,@MaxTime)>=0";
                param.AddWithValue("MaxTime", hs["MaxTime"].ToString());
            }
            sql = PagerSql(sql, p);
            DataSet ds = AdoTemplate.DataSetCreateWithParams(CommandType.Text, sql, param);
            p.DataSource = ds.Tables[0];
            p.ItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            return RT.SUCCESS;
        }
    }
}
