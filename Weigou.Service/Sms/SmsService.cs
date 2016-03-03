using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using Spring.Transaction.Interceptor;
using Weigou.Dao;
using Weigou.Common;

namespace Weigou.Service
{
    public class SmsService : BaseService, ISmsService
    {
        private ISmsDao smsDao;
        public ISmsDao SmsDao
        {
            set
            {
                smsDao = value;
            }
        }
        /// <summary>
        /// 短信模板
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetSmsTemplateList(Pager p, Hashtable hs)
        {
            return smsDao.GetSmsTemplateList(p, hs);
        }
        /// <summary>
        /// 短信记录
        /// </summary>
        public int GetSmsLogList(Pager p, Hashtable hs)
        {
            return smsDao.GetSmsLogList(p, hs);
        }
    }
}
