using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using Weigou.Common;

namespace Weigou.Service
{
    public interface ISmsService : IBaseService
    {
        /// <summary>
        /// 短信模板
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetSmsTemplateList(Pager p, Hashtable hs);
        /// <summary>
        /// 短信记录
        /// </summary>
        int GetSmsLogList(Pager p, Hashtable hs);
    }
}
