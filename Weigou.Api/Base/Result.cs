using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Weigou.Api.Base
{
    public class Result
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 数据列
        /// </summary>
        public object data { get; set; }
        /// <summary>
        /// 错误提示信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 记录总数
        /// </summary>
        public int total { get; set; }
        //string str="{\"status\":0,  data:[{\"id\":\"1\",\"pic\":[{},{}]  }  ,  {\"id\":\"1\",\"pic\":[{},{}]  }],    \"msg\":\"获取成功\"}";}
    }
}