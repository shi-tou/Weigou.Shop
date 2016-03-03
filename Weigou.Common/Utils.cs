using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Data;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;

namespace Weigou.Common
{
    public class Utils
    {
        #region 构造TreeGrid树型表格json数据
        /// <summary>
        /// 构造TreeGrid树型表格jsono数据
        /// </summary>
        /// <param name="dt">数据</param>
        /// <param name="filed">子字段</param>
        /// <param name="pFiled">父字段</param>
        /// <returns></returns>
        public static string CreateTreeJson(DataTable dt, string subField, string parentField)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append("[");
            CreateTree(dt, "", subField, parentField, ref sb);
            sb.Append("]");
            return sb.ToString();
        }
        public static string CreateTreeJsonInt(DataTable dt, string subField, string parentField)
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append("[");
            CreateTree(dt, "0", subField, parentField, ref sb);
            sb.Append("]");
            return sb.ToString();
        }
        /// <summary>
        /// 递归
        /// </summary>
        public static void CreateTree(DataTable dt, string parentCode, string subField, string parentField, ref StringBuilder sb)
        {
            DataTable dtTemp = Utils.SelectDataTable(dt, string.Format("isnull({0},'')='{1}' ", parentField, parentCode));
            if (dtTemp.Rows.Count == 0)
                return;
            foreach (DataRow dr in dtTemp.Rows)
            {
                sb.Append("{");
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    if (i != 0)
                        sb.Append(",");
                    sb.Append("\"" + dr.Table.Columns[i].ColumnName + "\":\"" + dr[i].ToString() + "\"");
                }
                sb.Append(",\"children\":[");
                CreateTree(dt, Convert.ToString(dr[subField]), subField, parentField, ref sb);
                sb.Append("]},");
            }
            sb.Remove(sb.Length - 1, 1);
        }
        #endregion

        #region cookie操作相关
        /// <summary>
        /// Cookies赋值
        /// </summary>
        /// <param name="strName">主键</param>
        /// <param name="strValue">键值</param>
        /// <param name="strDay">有效天数</param>
        /// <returns></returns>
        public static void SetCookie(string strName, string strValue)
        {
            HttpCookie Cookie = new HttpCookie(strName);
            //Cookie.Domain = ".xxx.com";//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
            Cookie.Expires = DateTime.Now.AddDays(1);
            Cookie.Value = strValue;
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
        }

        /// <summary>
        /// 读取Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>

        public static string GetCookie(string strName)
        {
            HttpCookie Cookie = System.Web.HttpContext.Current.Request.Cookies[strName];
            if (Cookie != null)
            {
                return Convert.ToString(Cookie.Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除Cookies
        /// </summary>
        /// <param name="strName">主键</param>
        /// <returns></returns>
        public static void DelCookie(string strName)
        {
            HttpCookie Cookie = new HttpCookie(strName);
            //Cookie.Domain = ".xxx.com";//当要跨域名访问的时候,给cookie指定域名即可,格式为.xxx.com
            Cookie.Expires = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cookies.Add(Cookie);
        }
        #endregion

        #region 系统文件日志/邮件发送
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="strTitle"></param>
        /// <param name="strContent"></param>
        public static void SaveLog(string strTitle, string strContent)
        {
            try
            {
                string Path = AppDomain.CurrentDomain.BaseDirectory + "Log/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/";
                string FilePath = Path + DateTime.Now.Day + "_Log.txt";
                if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);
                if (!File.Exists(FilePath))
                {
                    FileStream FsCreate = new FileStream(FilePath, FileMode.Create);
                    FsCreate.Close();
                    FsCreate.Dispose();
                }
                FileStream FsWrite = new FileStream(FilePath, FileMode.Append, FileAccess.Write);
                StreamWriter SwWrite = new StreamWriter(FsWrite);
                SwWrite.WriteLine(string.Format("{0}{1}[{2}]{3}", "--------------------------------", strTitle, DateTime.Now.ToString("HH:mm"), "--------------------------------"));
                SwWrite.Write(strContent);
                SwWrite.WriteLine("\r\n");
                SwWrite.WriteLine(" ");
                SwWrite.Flush();
                SwWrite.Close();
            }
            catch { }
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="recipient"></param>
        /// <param name="recipientname"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public static void SendEmail(string recipient, string recipientname, string subject, string body)
        {
            try
            {
                SmtpClient client = new SmtpClient(GetConfig("Stmp"));
                MailAddress from = new MailAddress(GetConfig("SenderUserName"), GetConfig("SenderName"), Encoding.UTF8);
                MailAddress to = new MailAddress(recipient, recipientname, Encoding.UTF8);
                MailMessage message = new MailMessage(from, to)
                {
                    Subject = subject,
                    SubjectEncoding = Encoding.UTF8,
                    Body = body,
                    BodyEncoding = Encoding.UTF8
                };
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = false;
                client.UseDefaultCredentials = false;
                NetworkCredential credential = new NetworkCredential(GetConfig("SenderUserName"), GetConfig("SenderPassword"));
                client.Credentials = credential;
                client.Send(message);
            }
            catch (Exception exception)
            {
                SaveLog("发送邮件异常", exception.Message);
            }
        }
        #endregion

        #region 字符相关
        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string content, string split)
        {
            if (!string.IsNullOrEmpty(content))
            {
                if (content.IndexOf(split) < 0)
                {
                    string[] tmp = { content };
                    return tmp;
                }
                return Regex.Split(content, Regex.Escape(split), RegexOptions.IgnoreCase);
            }
            else
            {
                return new string[0] { };
            }
        }
        /// <summary>
        /// 把字符插入字符串中
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="cr">插入字符</param>
        /// <returns></returns>
        public static string CharToNewString(string str, char cr)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            char[] arr = str.ToCharArray();
            string strNew = string.Empty;
            for (int i = 0; i < arr.Length; i++)
            {
                if (i != 0)
                    strNew += "," + arr[i];
                else
                    strNew = arr[i].ToString();
            }
            return strNew;
        }
        /// <summary>
        /// 生成验证码字符串
        /// </summary>
        /// <param name="codeLen">验证码字符长度</param>
        /// <returns>返回验证码字符串</returns>
        public static string CreateVerityCode(int codeLen)
        {
            if (codeLen < 1)
            {
                return string.Empty;
            }
            int number;
            string checkCode = string.Empty;
            Random random = new Random();
            for (int index = 0; index < codeLen; index++)
            {
                number = random.Next();
                checkCode += (char)('0' + (char)(number % 10));     //生成数字
            }
            return checkCode;
        }
        /// <summary>
        /// 生成十六进制
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public static string GetHexCode(int length)
        {
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            string code = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                code += Pattern[rnd];
            }
            return code;
        }
        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        public static string CreateOrderNo()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
        /// <summary>
        /// 生成令牌
        /// </summary>
        /// <returns></returns>
        public static string CreateToken()
        {
            string token = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            return token;
        }
        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        #endregion

        #region 文件相关
        /// <summary>
        /// 获取文件后缀名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFileExtension(string fileName)
        {
            int index = fileName.LastIndexOf(".");
            if (index == -1)
                return "";
            string ext = fileName.Substring(index);
            return ext;
        }
        #endregion

        #region 其他方法
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }
        /// <summary>
        /// 获取配置文件的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfig(string key)
        {
            try { return ConfigurationManager.AppSettings[key].ToString(); }
            catch { return ""; }
        }
        /// <summary>
        /// 获取ini文件的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetIniFile(string section, string key)
        {
            IniFile ini = new IniFile(HttpContext.Current.Server.MapPath("/Config/config.ini"));
            try { return ini.IniReadValue(section, key); }
            catch { return ""; }
        }
        /// <summary>
        /// 获取ini文件的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void SetIniFile(string section, string key, string value)
        {
            IniFile ini = new IniFile(HttpContext.Current.Server.MapPath("/Config/config.ini"));
            try { ini.IniWriteValue(section, key, value); }
            catch { }
        }
        /// <summary>
        /// 从一个Datatable中按条件分离出另一个Datatable
        /// </summary>
        public static DataTable SelectDataTable(DataTable dt, string strWhere)
        {
            DataView view = new DataView();
            view.Table = dt;
            view.RowFilter = strWhere;
            return view.ToTable();
        }
        /// <summary>
        /// Datatable排序
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static DataTable GetSortDataTable(DataTable dt, string orderBy)
        {
            DataView dv = dt.DefaultView;
            dv.Sort = orderBy;
            DataTable dtTemp = dv.ToTable();
            return dtTemp;
        }
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIp()
        {
            string strHostName = Dns.GetHostName();   //得到本机的主机名
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName); //取得本机IP
            string userIP = "";
            foreach (IPAddress ip in ipEntry.AddressList)
            {
                if (Regex.IsMatch(ip.ToString(), "^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$"))
                {
                    userIP = ip.ToString();
                }
            }
            return "127.0.0.1";
        }
        /// <summary>
        /// 检查路径是否存在，不存在则创建
        /// </summary>
        /// <param name="dir"></param>
        public static void CheckDir(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        /// <summary>
        /// 过滤html标签
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilterHtml(string str)
        {
            str = Regex.Replace(str, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "&(iexcl|#161);", "\x00a1", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "&(cent|#162);", "\x00a2", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "&(pound|#163);", "\x00a3", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "&(copy|#169);", "\x00a9", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "-->", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "<!--.*", "", RegexOptions.IgnoreCase);
            str.Replace("<", "");
            str.Replace(">", "");
            str.Replace("\r\n", "");
            str = HttpContext.Current.Server.HtmlEncode(str).Trim();
            return str;
        }
        /// <summary>
        /// 替换手机号第4-第7位
        /// </summary>
        /// <param name="MobileNo"></param>
        /// <returns></returns>
        public static string ReplaceMobileNo(string MobileNo)
        {
            if (MobileNo.Length == 11)
            {
                return MobileNo.Substring(0, 3) + "****" + MobileNo.Substring(7, 4);
            }
            return MobileNo;
        }
        #endregion

        #region ========Base64编码========
        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="code_type">编码类型</param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string EncodeBase64(string codeType, string code)
        {
            return EncodeBase64(Encoding.GetEncoding(codeType), code);
        }
        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="code_type">编码类型</param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string EncodeBase64(Encoding encoding, string code)
        {
            string encode = "";
            byte[] bytes = encoding.GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }
        #endregion

        #region ========Base64解码========
        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="code_type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string DecodeBase64(string codeType, string code)
        {
            return DecodeBase64(Encoding.GetEncoding(codeType), code);
        }
        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="code_type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string DecodeBase64(Encoding encoding, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = encoding.GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }
        #endregion

        #region 相关转换
        /// <summary>
        /// 十六进制转字符
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string HexToString(string hex)
        {
            int intValue = Convert.ToInt32(hex, 16);
            string strValue = Char.ConvertFromUtf32(intValue);
            if (string.IsNullOrEmpty(strValue))
            {
                return "";
            }
            return strValue;
        }
        /// <summary>
        /// 十六进制转十进制
        /// </summary>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static int HexToDecimal(string hex)
        {
            return Convert.ToInt32(hex, 16);
        }
        #endregion
    }
}

