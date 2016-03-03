using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Data.Common;
using Weigou.Common;
using System.Data;
using System.Collections;
using Weigou.Model.Enum;
using Weigou.Dao;

namespace Weigou.Service
{
    public class WeixinService : BaseService, IWeixinService
    {
        private IWeixinDao weixinDao;
        public IWeixinDao WeixinDao
        {
            set
            {
                weixinDao = value;
            }
        }

        /// <summary>
        /// 获取微信菜单 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetWeixinMenuList(Hashtable hs)
        {
            return weixinDao.GetWeixinMenuList(hs);
        }
        /// <summary>
        /// 获取微信关键词 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetWeixinKeywordList(Pager p, Hashtable hs)
        {
            return weixinDao.GetWeixinKeywordList(p, hs);
        }
    }
}
