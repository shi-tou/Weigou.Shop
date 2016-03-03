using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Weigou.Model;
using Weigou.Model.Enum;
using System.Collections;

namespace Weigou.Service
{
    public interface IBaseService
    {
        #region Insert/Update/Delete
        /// <summary>
        /// 插入记录(返回自增ID)
        /// </summary>
        int Insert( DataTable dt);
        /// <summary>
        /// 修改记录
        /// </summary>
        int Update( DataTable dt, string where);
        /// <summary>
        /// 更新DataTable
        /// </summary>
        int UpdateDataTable(DataTable dt);
        /// <summary>
        /// 删除记录
        /// </summary>
        int Delete(string tableName);
        /// <summary>
        /// 删除记录
        /// </summary>>
        int Delete(string tableName, string where);
        int Delete(string tableName, string keycol, object obj);
        #endregion

        #region DataTable
        /// <summary>
        /// 获取DataTable表数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>DataTable表数据</returns>
        DataTable GetData(string tableName);
        /// <summary>
        /// 获取DataTable表数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">数据字段，如：string fields="ID,Name,Sex";如为"*",则为所有字段</param>
        /// <returns>DataTable表数据</returns>
        DataTable GetData(string tableName, string fields);
        /// <summary>
        /// 获取DataTable表数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">数据字段，如：string fields="ID,Name,Sex";如为"*",则为所有字段</param>
        /// <param name="where">查询条件</param>
        /// <returns>DataTable表数据</returns>
        DataTable GetData(string tableName, string fields, string where);
        DataTable GetDataByKey(string tablename, string field, object obj);
        DataTable GetDataByWhere(string tablename, Hashtable hs);
        DataTable GetSortDataByKey(string tablename, string field, bool asc);
        #endregion

        #region 执行Sql语句
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>影响记录数</returns>
        int ExecteSql(CommandType cType, string sql);
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>结果集DataSet</returns>
        DataSet ExecteSqlGetDataSet(CommandType cType, string sql);
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>表数据DataTable</returns>
        DataTable ExecteSqlGetDataTable(CommandType cType, string sql);
        #endregion

        #region 公用
         /// <summary>
        /// 记录用户操作日志
        /// </summary>
        /// <param name="code"></param>
        /// <param name="m"></param>
        /// <param name="o"></param>
        /// <param name="adminID"></param>
        /// <param name="content"></param>
        void SaveSysLog(string code, EnumModule m, EnumOperation o, int adminID, string content);
        /// 系统自动发送短信
        /// </summary>
        /// <param name="mobileNo">手机号(多个用";"分隔)</param>
        /// <param name="content">内容</param>
        /// <param name="presetTime">预定时间</param>
        int SendSms(string mobileNo, string content, string[] param);
        /// <summary>
        /// 人工发送短信
        /// </summary>
        /// <param name="mobileNo">手机号(多个用";"分隔)</param>
        /// <param name="content">内容</param>
        /// <param name="presetTime">预定时间</param>
        /// <param name="adminID">发送人：0-表示系统</param>
        /// <param name="sendType">发送方式： -1-系统发送 2-人工发送 </param>
        int SendSms(string mobileNo, string content, string[] param, int adminID);
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobileNo">手机号(多个用";"分隔)</param>
        /// <param name="content">内容</param>
        /// <param name="adminID">发送人：0-表示系统</param>
        int SendSms(string mobileNo, string smsContent, int adminID);
        /// <summary>
        /// 获取模板内容
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        string GetSmsTemplate(string code);
        /// <summary>
        /// 添加验证码
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        int SaveVerifyCode(string mobileNo, string verifyCode);
        /// <summary>
        /// 检查验证码(默认30分钟内有效)
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <param name="verifyCode"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        bool CheckVerifyCode(string mobileNo, string verifyCode, int minutes = 30);
        /// <summary>
        /// 更新验证码为已使用
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        int UpdateVerifyCode(string mobileNo, string verifyCode);
        #endregion
    }
}
