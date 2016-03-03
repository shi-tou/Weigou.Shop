using Newtonsoft.Json;
using SigeShop.Common;
using SigeShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SigeShop.Api
{
    public class AppService
    {
        #region IOC注入
        public static ISysService sysService;
        public ISysService SysService
        {
            set { sysService = value; }
            get { return sysService; }
        }
        //注入
        public static IGoodsService goodsService;
        public IGoodsService GoodsService
        {
            set { goodsService = value; }
        }
        //注入
        public static IMemberService memberService;
        public IMemberService MemberService
        {
            set { memberService = value; }
        }
        //注入
        public static IReportService reportService;
        public IReportService ReportService
        {
            set { reportService = value; }
        }
        //注入
        public static ISmsService smsService;
        public ISmsService SmsService
        {
            set { smsService = value; }
        }
        //注入
        public static IScoreService scoreService;
        public IScoreService ScoreService
        {
            set { scoreService = value; }
        }
        #endregion

        /// <summary>
        /// 返回结果
        /// </summary>
        public Result result;
        /// <summary>
        /// http上下文
        /// </summary>
        public HttpContext context;
        /// <summary>
        /// 构造函数
        /// </summary>
        public AppService()
        {
            result = new Result();
            result.status = RT.FAILED;
            context = HttpContext.Current;
        }

        #region 公用方法
        /// <summary>
        /// 获取hashtable值
        /// </summary>
        public string GetParam(MyHashtable hs, string key, string defaultValue)
        {
            if (hs == null) return defaultValue;
            if (hs.Contains(key)) return Convert.ToString(hs[key]);
            return defaultValue;
        }
        /// <summary>
        /// 获取hashtable值
        /// </summary>
        public int GetParam(MyHashtable hs, string key, int defaultValue)
        {
            if (hs == null) return defaultValue;
            if (hs.Contains(key)) return Convert.ToInt32(hs[key]);
            return defaultValue;
        }
        /// <summary>
        /// 获取hashtable值
        /// </summary>
        public bool GetParam(MyHashtable hs, string key, bool defaultValue)
        {
            if (hs == null) return defaultValue;
            if (hs.Contains(key)) return Convert.ToBoolean(hs[key]);
            return defaultValue;
        }
        /// <summary>
        /// 输出json结果
        /// </summary>
        public void ResposeWrite()
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(JsonConvert.SerializeObject(result));
        }
        #endregion
    }
}