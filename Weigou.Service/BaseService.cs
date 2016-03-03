using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Weigou.Dao;
using Weigou.Model;
using Weigou.Common;
using Weigou.Model.Enum;
using System.Collections;
using System.Text.RegularExpressions;  

namespace Weigou.Service
{
    public class BaseService : IBaseService
    {
        private IBaseDao baseDao;
        public IBaseDao BaseDao
        {
            set
            {
                baseDao = value;
            }
        }
        public IMemberService memberService;
        public IMemberService MemberService
        {
            set
            {
                memberService = value;
            }
        }
        public IGoodsService goodsService;
        public IGoodsService GoodsService
        {
            set
            {
                goodsService = value;
            }
        }

        #region Insert/Update/Delete
        /// <summary>
        /// 插入记录
        /// </summary>
        public int Insert(DataTable dt)
        {
            return baseDao.Insert(dt);
        }
        /// <summary>
        /// 修改记录
        /// </summary>
        public int Update(DataTable dt, string where)
        {
            return baseDao.Update(dt, where);
        }
        /// <summary>
        /// 更新DataTable
        /// </summary>
        public int UpdateDataTable(DataTable dt)
        {
            return baseDao.UpdateDataTable(dt);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(string tableName)
        {
            return baseDao.Delete(tableName);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        public int Delete(string tableName, string where)
        {
            return baseDao.Delete(tableName, where);
        }
        public int Delete(string tableName, string field, object obj)
        {
            return baseDao.Delete(tableName, field, obj);
        }
        #endregion

        #region DataTable
        /// <summary>
        /// 获取DataTable表数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>DataTable表数据</returns>
        public DataTable GetData(string tableName)
        {
            return baseDao.GetData(tableName);
        }
        /// <summary>
        /// 获取DataTable表数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">数据字段，如：string fields="ID,Name,Sex";如为"*",则为所有字段</param>
        /// <returns>DataTable表数据</returns>
        public DataTable GetData(string tableName, string fields)
        {
            return baseDao.GetData(tableName, fields);
        }
        /// <summary>
        /// 获取DataTable表数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fields">数据字段，如：string fields="ID,Name,Sex";如为"*",则为所有字段</param>
        /// <param name="where">查询条件</param>
        /// <returns>DataTable表数据</returns>
        public DataTable GetData(string tableName, string fields, string where)
        {
            return baseDao.GetData(tableName, fields, where);
        }
        public DataTable GetDataByKey(string tablename, string field, object obj)
        {
            return baseDao.GetDataByKey(tablename, field, obj);
        }

        public DataTable GetSortDataByKey(string tablename, string field, bool asc)
        {
            return baseDao.GetSortDataByKey(tablename, field, asc);
        }
        public DataTable GetDataByWhere(string tablename, Hashtable hs)
        {
            return baseDao.GetDataByWhere(tablename, hs);
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
            return baseDao.ExecteSql(cType, sql);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>结果集DataSet</returns>
        public DataSet ExecteSqlGetDataSet(CommandType cType, string sql)
        {
            return baseDao.ExecteSqlGetDataSet(cType, sql);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="cType">执行类型</param>
        /// <param name="sql">sql语句</param>
        /// <returns>表数据DataTable</returns>
        public DataTable ExecteSqlGetDataTable(CommandType cType, string sql)
        {
            return baseDao.ExecteSqlGetDataTable(cType, sql);
        }
        #endregion

        #region 公用
        /// <summary>
        /// 记录用户操作日志
        /// </summary>
        /// <param name="code"></param>
        /// <param name="m"></param>
        /// <param name="o"></param>
        /// <param name="userID"></param>
        /// <param name="content"></param>
        public void SaveSysLog(string code, EnumModule m, EnumOperation o, int userID, string content)
        {
            DataTable dt = GetDataByKey("T_SysLog", "ID", 0);
            DataRow dr = dt.NewRow();
            dr["Module"] = (int)m;
            dr["Operation"] = (int)o;
            dr["Code"] = code;
            dr["CreateBy"] = userID;
            dr["Content"] = content;
            dr["IP"] = Utils.GetIp();
            dr["CreateTime"] = DateTime.Now;
            dt.Rows.Add(dr);
            UpdateDataTable(dt);
        }
        /// <summary>
        /// 系统自动发送短信
        /// </summary>
        /// <param name="mobileNo">手机号(多个用";"分隔)</param>
        /// <param name="smsTemplateCode">内容</param>
        /// <param name="presetTime">预定时间</param>
        public int SendSms(string mobileNo, string smsTemplateCode, string[] param)
        {
            return SendSms(mobileNo, smsTemplateCode, param, 0);
        }
        /// <summary>
        /// 人工发送短信
        /// </summary>
        /// <param name="mobileNo">手机号(多个用";"分隔)</param>
        /// <param name="smsTemplateCode">模板Code</param>
        /// <param name="param">参数【勿必与模板预设参数个数一致】</param>
        /// <param name="adminID">发送人：0-表示系统</param>
        public int SendSms(string mobileNo, string smsTemplateCode, string[] param, int adminID)
        {
            string smsContent = GetSmsTemplate(smsTemplateCode);

            smsContent = string.Format(smsContent, param);

            if (string.IsNullOrEmpty(smsContent))
            {
                return RT.FAILED;
            }
            
            return SendSms(mobileNo, smsContent, adminID);
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobileNo">手机号(多个用";"分隔)</param>
        /// <param name="content">内容</param>
        /// <param name="adminID">发送人：0-表示系统</param>
        public int SendSms(string mobileNo, string smsContent, int adminID)
        {
            try
            {
                bool flag = false; 
                string errMsg = "";
                SMSHelper sms = new SMSHelper();                   
                int resultCode =sms.SendSMS(mobileNo,smsContent);

                if (resultCode != RT.RESULT_SMS_OK)
                {
                    if (resultCode == 102)
                    {
                        errMsg = "失败码：102,短信不足，请充值！";
                    }
                    else
                    {
                        errMsg = "失败码：" + resultCode;
                    }
                }
                else
                {
                    flag = true;
                }

                //在这里可以调用短信商接口发送短信,调用成功后，添加短信记录到数据库

                DataTable dt = GetDataByKey("T_SmsSend", "ID", 0);
                DataRow dr = dt.NewRow();
                DateTime time = DateTime.Now;
                dr["MobileNo"] = mobileNo;
                dr["Content"] = smsContent;
                dr["Count"] = mobileNo.Split(";".ToCharArray()).Length;
                dr["Status"] = 1;
                dr["CreateBy"] = adminID;
                dr["CreateTime"] = time;
                dt.Rows.Add(dr);

                int MessageID = Insert(dt);
                if (MessageID > 0)
                {
                    DataTable dtnew = GetDataByKey("T_SmsSend", "ID", MessageID);
                    //对应更新数据库的短信记录发送状态
                    if (flag)//更新状态为已发送
                    {
                        if (dtnew.Rows.Count > 0)
                        {
                            dtnew.Rows[0]["Status"] = 1;
                            UpdateDataTable(dtnew);
                        }
                        return RT.SUCCESS;

                    }
                    else//记录错误信息到T_SmsSend
                    {
                        if (dtnew.Rows.Count > 0)
                        {
                            dtnew.Rows[0]["ErrMsg"] = errMsg;
                            UpdateDataTable(dtnew);
                        }
                        return RT.FAILED;
                    }
                   
                }
                else
                {
                    return RT.FAILED;
                }
                 
            }
            catch (Exception ex)
            {
                Utils.SaveLog("发送短信", ex.Message);
                return RT.FAILED;
            }
        }
        
        /// <summary>
        /// 获取模板内容
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetSmsTemplate(string code)
        {
            DataTable dt = GetDataByKey("T_SmsTemplate", "Code", code);
            if (dt.Rows.Count == 0)
                return "";
            return Convert.ToString(dt.Rows[0]["Content"]);
        }
        /// <summary>
        /// 添加验证码
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        public int SaveVerifyCode(string mobileNo, string verifyCode)
        {
            DataTable dt = GetDataByKey("T_VerifyCode", "ID", 0);
            DataRow dr = dt.NewRow();
            dr["MobileNo"] = mobileNo;
            dr["VerifyCode"] = verifyCode;
            dr["IsUsed"] = 0;
            dr["CreateTime"] = DateTime.Now;
            dt.Rows.Add(dr);
            if (UpdateDataTable(dt) > 0)
                return RT.SUCCESS;
            else
                return RT.FAILED;
        }
        /// <summary>
        /// 检查验证码(默认20分钟内有效)
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <param name="verifyCode"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public bool CheckVerifyCode(string mobileNo, string verifyCode,int minutes)
        {
            DataTable dt = baseDao.CheckVerifyCode(mobileNo,verifyCode);
            if (dt.Rows.Count == 0)
                return false;
            if (verifyCode != Convert.ToString(dt.Rows[0]["VerifyCode"]))
            {
                return false;
            }
            TimeSpan ts = DateTime.Now - DateTime.Parse(dt.Rows[0]["CreateTime"].ToString());
            if (ts.Minutes > minutes)
                return false;
            
            return true;
        }

        /// <summary>
        /// 更新验证码为已使用
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        public int UpdateVerifyCode(string mobileNo, string verifyCode)
        {
            Hashtable hs = new Hashtable();
            hs.Add("MobileNo", mobileNo);
            hs.Add("VerifyCode", verifyCode);
            hs.Add("IsUsed", 0);
            DataTable dt = GetDataByWhere("T_VerifyCode", hs);
            if (dt.Rows.Count == 0)
                return RT.FAILED;
            dt.Rows[0]["IsUsed"] = 1;
            if (UpdateDataTable(dt) > 0)
                return RT.SUCCESS;
            else
                return RT.FAILED;
        }
        #endregion
    }
}
