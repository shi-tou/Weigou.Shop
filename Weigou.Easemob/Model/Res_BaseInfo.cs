using System;
using System.Collections.Generic;
using System.Web;

namespace Weigou.Easemob
{
    /// <summary>
    /// 环信返回结果基类
    /// </summary>
    public class Res_BaseInfo
    {
        public string action;
        public string application;
        public string Params;
        public string path;
        public string uri;
        public string timestamp;
        public string duration;
        public string organization;
        public string applicationName;
        public int count;
    }
}