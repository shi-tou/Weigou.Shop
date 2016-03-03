using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Weigou.Model;
using Weigou.Dao;
using Weigou.Common;
using System.Collections;
using Spring.Transaction.Interceptor;

namespace Weigou.Service
{
    public class ReportService : BaseService, IReportService
    {
        private IReportDao reportDao;
        public IReportDao ReportDao
        {
            set
            {
                reportDao = value;
            }
        }

        #region 会员统计报表
        /// <summary>
        /// 会员统计报表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetMemberReport(Pager p, Hashtable hs)
        {
            return reportDao.GetMemberReport(p, hs);
        }
        /// <summary>
        /// 会员统计报表(导出xls)
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetMemberReport(Hashtable hs)
        {
            return reportDao.GetMemberReport(hs);
        }
        #endregion

        #region 积分统计报表
        /// <summary>
        /// 积分统计报表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetScoreReport(Pager p, Hashtable hs)
        {
            return reportDao.GetScoreReport(p, hs);
        }
        /// <summary>
        /// 积分统计报表(导出xls)
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetScoreReport(Hashtable hs)
        {
            return reportDao.GetScoreReport(hs);
        }
        #endregion

        #region 商品统计报表
        /// <summary>
        /// 商品统计报表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetGoodsSaleReport(Pager p, Hashtable hs)
        {
            return reportDao.GetGoodsSaleReport(p, hs);
        }
        /// <summary>
        /// 商品统计报表(导出xls)
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetGoodsReport(Hashtable hs, string strOrderBy)
        {
            return reportDao.GetGoodsReport(hs, strOrderBy);
        }
        #endregion

        #region 订单统计报表
        /// <summary>
        /// 订单统计报表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        public int GetOrderReport(Pager p, Hashtable hs)
        {
            return reportDao.GetOrderReport(p, hs);
        }
        /// <summary>
        /// 订单统计报表(导出xls)
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public DataTable GetOrderReport(Hashtable hs)
        {
            return reportDao.GetOrderReport(hs);
        }
        #endregion
    }
}
