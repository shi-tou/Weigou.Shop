using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Common;
using System.Collections;
using System.Data;

namespace Weigou.Service
{
    public interface IWeixinService : IBaseService
    {
        /// <summary>
        /// 获取微信菜单 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        DataTable GetWeixinMenuList(Hashtable hs);
        /// <summary>
        /// 获取微信关键词 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetWeixinKeywordList(Pager p, Hashtable hs);
    }
}
