﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Spring.Data.Core;
using System.Text.RegularExpressions;
using Weigou.Common;
using Spring.Data.Common;
using System.Collections;

namespace Weigou.Dao
{
    /// <summary>
    /// 数据访问基类
    /// </summary>
    public class BaseDao : AdoDaoSupport, IBaseDao
    {
        #region Insert/Update/Delete
        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        public int Insert(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                string tableName = dt.TableName;
                IDbParameters param = AdoTemplate.CreateDbParameters();
                string sql = "insert into " + tableName + "(";
                string cols = "";
                string vals = "";
                DataRow dr = dt.Rows[0];
                foreach (DataColumn col in dt.Columns)
                {
                    string key = col.ColumnName;

                    if (dr[key] != DBNull.Value && key != "_is_add")
                    {
                        cols += key + ",";
                        vals += "@" + key + ",";
                        param.AddWithValue(key, dr[key]);
                    }
                }
                cols = cols.Substring(0, cols.Length - 1);
                vals = vals.Substring(0, vals.Length - 1);
                sql += cols + ")values(" + vals + ") select @@identity";
                Object objValue = AdoTemplate.ExecuteScalar(CommandType.Text, sql, param);
                if (objValue == null)
                    return 0;
                else
                    return Convert.ToInt32(objValue);
            }
            else { return 0; }
        }
        /// <summary>
        /// 修改记录
        /// </summary>
        public int Update( DataTable dt, string where)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat("update {0} set ", dt.TableName);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (i != 0)
                {
                    sbSql.Append(",");
                }
                sbSql.AppendFormat("[{0}]='{1}'", dt.Columns[i].ColumnName, dt.Rows[0][i].ToString());
            }
            sbSql.AppendFormat(" where {0}", where);
            return AdoTemplate.ExecuteNonQuery(CommandType.Text, sbSql.ToString());
        }
        /// <summary>
        /// 更新DataTable
        /// </summary>
        /// <param name="dt">要更新的DataTable</param>
        /// <returns>返回影响行数</returns>
        public int UpdateDataTable(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                return 0;
            }
            int i = 0;
            try
            {
                string cols = "";//只处理影响到的字段。
                DataTable dte = GetEmptyTable(dt.TableName);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string col = dt.Columns[j].ColumnName;
                    if (dte.Columns.Contains(col))
                    {
                        if (cols != "")
                        {
                            cols += ",";
                        }
                        cols += "[" + col + "]";
                    }
                }
                string sql = "select " + cols + " from " + dt.TableName;
                i = AdoTemplate.DataTableUpdateWithCommandBuilder(dt, CommandType.Text, sql, null, dt.TableName);
            }
            catch (Exception e)
            {
                throw e;
            }
            return i;
        }
        public DataTable GetEmptyTable(string tablename)
        {
            string sql = "select name,xtype,typestat from syscolumns where id=object_id('" + tablename + "')";
            DataTable dt = AdoTemplate.DataTableCreate(CommandType.Text, sql);
            DataTable dtRes = new DataTable(tablename);
            foreach (DataRow dr in dt.Rows)
            {
                /*
                    34 image； 48 tinyint； 56 int；  61 datetime； 104 bit； 106 decimal
                    165 varbinary；167 varchar； 231 nvarchar；239 nchar
                */
                Type type = typeof(string);
                byte t = (byte)dr["xtype"];
                if (t == 48)
                {
                    type = typeof(byte);
                }
                else if (t == 56)
                {
                    type = typeof(int);
                }
                else if (t == 61)
                {
                    type = typeof(DateTime);
                }
                else if (t == 104)
                {
                    type = typeof(bool);
                }
                else if (t == 106)
                {
                    type = typeof(decimal);
                }
                else if (t == 167 || t == 231 || t == 239)
                {
                    type = typeof(string);
                }
                else if (t == 165 || t == 34)
                {
                    type = typeof(byte[]);
                }
                dtRes.Columns.Add(dr["name"].ToString(), type);
            }
            return dtRes;
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(string tableName)
        {
            return Delete(tableName, "");
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(string tableName, string where)
        {
            StringBuilder sbSql = new StringBuilder();
            if (where == "")
                sbSql.AppendFormat("delete from {0}", tableName);
            else
                sbSql.AppendFormat("delete from {0} where {1}", tableName, where);
            return AdoTemplate.ExecuteNonQuery(CommandType.Text, sbSql.ToString());
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(string tableName, string keycol, object obj)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendFormat("delete from {0} where {1}=@{1}", tableName, keycol);
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue(keycol, obj);
            return AdoTemplate.ExecuteNonQuery(CommandType.Text, sbSql.ToString(), param);
        }
        #endregion

        #region DataTable
        /// <summary>
        /// 获取DataTable表数据
        /// </summary>
        public DataTable GetData(string tableName)
        {
            DataTable dt = GetData(tableName, "*", "");
            dt.TableName = tableName;
            return dt;
        }
        /// <summary>
        /// 获取DataTable表数据
        /// </summary>
        public DataTable GetData(string tableName, string fields)
        {
            return GetData(tableName, fields, "");
        }
        /// <summary>
        /// 获取DataTable数据集
        /// </summary>
        /// <param name="fields">数据字段，如：string fields="ID,Name,Sex";如为"*",则为所有字段</param>
        /// <param name="where">条件，如：string fields="ID=1"</param>
        public DataTable GetData(string tableName, string fields, string where)
        {
            StringBuilder sbSql = new StringBuilder();
            if (where == "")
                sbSql.AppendFormat("select {0} from {1}", fields, tableName);
            else
                sbSql.AppendFormat("select {0} from {1} where {2}", fields, tableName, where);
            return AdoTemplate.DataTableCreate(CommandType.Text, sbSql.ToString());
        }
        public DataTable GetDataByKey(string tablename, string field, object obj)
        {
            string sql = "select * from " + tablename + " where " + field + "=@" + field;
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue(field, obj);
            DataTable dt = AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
            dt.TableName = tablename;
            return dt;
        }
        public DataTable GetDataByWhere(string tablename, Hashtable hs)
        {
            string sql = "select * from " + tablename + " where 1=1";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            foreach (string key in hs.Keys)
            {
                sql += string.Format(" and {0}=@{0}", key);
                param.AddWithValue(key, hs[key]);
            }
            DataTable dt = AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
            dt.TableName = tablename;
            return dt;
        }
        /// <summary>
        /// 获取排序数据
        /// </summary>
        public DataTable GetSortDataByKey(string tablename, string field, bool asc)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select * from {0} order by [{1}]  {2}", tablename, field, asc ? "asc" : "desc");
            DataTable dt = AdoTemplate.DataTableCreate(CommandType.Text, sb.ToString());
            dt.TableName = tablename;
            return dt;
        }
        #endregion

        #region 执行Sql语句
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>影响记录数</returns>
        public int ExecteSql(CommandType cType, string sql)
        {
            return AdoTemplate.ExecuteNonQuery(cType, sql);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>结果集DataSet</returns>
        public DataSet ExecteSqlGetDataSet(CommandType cType, string sql)
        {
            return AdoTemplate.DataSetCreate(CommandType.Text, sql);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>表数据DataTable</returns>
        public DataTable ExecteSqlGetDataTable(CommandType cType, string sql)
        {
            return AdoTemplate.DataTableCreate(CommandType.Text, sql);
        }
        #endregion

        /// <summary>
        /// 将普通sql语句拼成分页用的sql语句
        /// </summary>
        /// <param name="str"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="strOrder"></param>
        /// <returns></returns>
        public string PagerSql(string oldsql, Pager pager)
        {
            int iBegin = (pager.PageIndex - 1) * pager.PageSize + 1;
            int iEnd = pager.PageIndex * pager.PageSize;
            Regex se = new Regex(@"^\s*select", RegexOptions.IgnoreCase);
            oldsql = se.Replace(oldsql, "SELECT ROW_NUMBER() OVER (ORDER BY " + pager.OrderKey + ") AS RowID,");
            string sql = "SELECT * FROM "
                        + " (" + oldsql + ") as list "
                        + " WHERE RowID between " + iBegin.ToString() + " and " + iEnd.ToString() + " ";
            sql += " select count(*) from (" + oldsql + ") as tmp";
            return sql;
        }

        /// <summary>
        /// 获取验证码（默认30分钟内有效）
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        public DataTable CheckVerifyCode(string mobileNo, string verifyCode)
        {
            string sql = "select * from T_VerifyCode where MobileNo=@MobileNo and VerifyCode=@VerifyCode and IsUsed=0";
            IDbParameters param = AdoTemplate.CreateDbParameters();
            param.AddWithValue("mobileNo", mobileNo);
            param.AddWithValue("VerifyCode", verifyCode);
            return AdoTemplate.DataTableCreateWithParams(CommandType.Text, sql, param);
        }
    }
}
