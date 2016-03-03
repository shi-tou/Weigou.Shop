using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Weigou.Common
{
    /// <summary>
    /// 短信操作类
    /// </summary>
    public class SMSHelper
    {
        public SMSHelper() { }

        /// <summary>
        /// 发送短信 sendsms
        /// </summary>
        /// <param name="Mobile"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public int SendSMS(string Mobile, string Content)
        {
            StringBuilder sb = new StringBuilder();
            string uid = Utils.GetConfig("sms_uid");
            string pwd = Utils.GetConfig("sms_pwd");
            string url = Utils.GetConfig("sms_url");
            string sign = Utils.GetConfig("sms_sign");
            string method = "sendsms";

            Content = System.Web.HttpUtility.UrlEncode(Content + sign, Encoding.GetEncoding("GBK"));
            pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("wclm123", "MD5").ToLower();
                        
            sb.Append("uid=" + uid);
            sb.Append("&pwd=" + pwd);
            sb.Append("&mobile=" + Mobile);
            sb.Append("&content=" + Content);
             
            byte[] bTemp = System.Text.Encoding.GetEncoding("GBK").GetBytes(sb.ToString());
            string postReturn = doPostRequest(url + method, bTemp); 

            return int.Parse(postReturn);
        }

        /// <summary>
        /// 获取短信金额
        /// </summary>
        /// <returns></returns>
        public string GetBalance()
        {
            StringBuilder sb = new StringBuilder();
            string uid = Utils.GetConfig("sms_uid");
            string pwd = Utils.GetConfig("sms_pwd");
            string url = Utils.GetConfig("sms_url");
            string method = "querymoney";
            pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("wclm123", "MD5").ToLower();
            sb.Append("uid=" + uid);
            sb.Append("&pwd=" + pwd);
            byte[] bTemp = System.Text.Encoding.GetEncoding("GBK").GetBytes(sb.ToString());

            string postReturn = doPostRequest(url + method, bTemp);
            return postReturn.Split('&')[1];
        }
         
        private static String doPostRequest(string url, byte[] bData)
        {
            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "POST";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
                hwRequest.ContentLength = bData.Length;
                
                System.IO.Stream smWrite = hwRequest.GetRequestStream();
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();
            }
            catch (System.Exception err)
            {
                Utils.SaveLog("短息发送时出现异常" + url, err.ToString());
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                Utils.SaveLog("短息接受时出现异常" + url, err.ToString());
            }

            return strResult;
        }


        #region 已删除方法

        //WeicheSms.Soap57ProviderService _service = new WeicheSms.Soap57ProviderService();

        ///// <summary>
        ///// 发送(提交)短信
        ///// </summary>
        ///// <param name="strContent"></param>
        ///// <param name="strMobile"></param>
        ///// <returns></returns>
        //public string Submit(string strContent, string strMobile)
        //{
        //    try
        //    {
        //        return _service.Submit(Utils.GetConfig("sms_SpID"), Utils.GetConfig("sms_Password"), Utils.GetConfig("sms_AccessCode"), strContent + Utils.GetConfig("sms_Sign"), strMobile);
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        ///// <summary>
        ///// 查询余额
        ///// </summary>       
        ///// <returns></returns>
        //public string QueryMo()
        //{
        //    try
        //    {
        //        return _service.QueryMo(Utils.GetConfig("sms_SpID"), Utils.GetConfig("sms_Password"));
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        ///// <summary>
        ///// 短信状态报告
        ///// </summary>     
        ///// <returns></returns>
        //public string QueryReport()
        //{
        //    try
        //    {
        //        return _service.QueryReport(Utils.GetConfig("sms_SpID"), Utils.GetConfig("sms_Password"));
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        ///// <summary>
        ///// 回复信息获取
        ///// </summary>      
        ///// <returns></returns>
        //public string RetrieveAll()
        //{
        //    try
        //    {
        //        return _service.RetrieveAll(Utils.GetConfig("sms_SpID"), Utils.GetConfig("sms_Password"));
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        #endregion
    }
}
