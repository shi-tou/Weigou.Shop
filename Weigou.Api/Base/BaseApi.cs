using Newtonsoft.Json;
using Weigou.Common;
using Weigou.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weigou.Api.Base
{
    public class BaseApi
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
        public static IContentService contentService;
        public IContentService ContentService
        {
            set { contentService = value; }
        }       
        //注入
        public static IOrderService orderService;
        public IOrderService OrderService
        {
            set { orderService = value; }
        }
       
        #endregion

        /// <summary>
        /// 返回结果
        /// </summary>
        public Result result;
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseApi()
        {
            result = new Result();
            result.status = RT.FAILED;
            result.data = "{}";
            result.msg = "";
        }

        #region 公用方法
        /// <summary>
        /// 获取hashtable值
        /// </summary>
        public string GetParam(MyHashtable hs, string key, string defaultValue)
        {
            try
            {
                if (hs == null) return defaultValue;
                key = key.ToLower();
                if (hs.Contains(key)) return Convert.ToString(hs[key]);
                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 获取hashtable值
        /// </summary>
        public int GetParam(MyHashtable hs, string key, int defaultValue)
        {
            try
            {
                if (hs == null) return defaultValue;
                key = key.ToLower();
                if (hs.Contains(key)) return Convert.ToInt32(hs[key]);
                return defaultValue;
            }
            catch { return defaultValue; }
        }
        /// <summary>
        /// 获取hashtable值
        /// </summary>
        public bool GetParam(MyHashtable hs, string key, bool defaultValue)
        {
            try
            {
                if (hs == null) return defaultValue;
                key = key.ToLower();
                if (hs.Contains(key)) return Convert.ToBoolean(hs[key]);
                return defaultValue;
            }
            catch { return defaultValue; }
        }
        /// <summary>
        /// hashtable 转 object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hs"></param>
        /// <returns></returns>
        public T HasToObject<T>(MyHashtable hs)
        {
            T t = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(hs));
            return t;
        }
        #endregion

        #region 分页相关
        public Pager GetPager(MyHashtable hs, string orderBy)
        {
            int PagerIndex = GetParam(hs, "PagerIndex", 1);
            int PagerSize = GetParam(hs, "PagerSize", 20);
            Pager p = new Pager(PagerSize, PagerIndex, orderBy);
            return p;
        }
        public Pager GetPager(MyHashtable hs,int pageIndex,int pageSize, string orderBy)
        {
            int PagerIndex = GetParam(hs, "PagerIndex", pageIndex);
            int PagerSize = GetParam(hs, "PagerSize", pageSize);
            Pager p = new Pager(PagerSize, PagerIndex, orderBy);
            return p;
        }
        #endregion

        #region 图片相关
        /// <summary>
        /// 返回服务器图片路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetServerPicture(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return "";
            }
            return Utils.GetConfig("ServerPicPath") + url;
        }
        #endregion

    }
}