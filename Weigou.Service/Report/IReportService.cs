using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Weigou.Common;
using System.Collections;
using System.Data;

namespace Weigou.Service
{
    public interface IReportService : IBaseService
    {
        #region 会员统计报表
        /// <summary>
        /// 会员统计报表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetMemberReport(Pager p, Hashtable hs);
        /// <summary>
        /// 会员统计报表(导出xls)
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        DataTable GetMemberReport(Hashtable hs);
        #endregion

        #region 积分统计报表
        /// <summary>
        /// 积分统计报表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetScoreReport(Pager p, Hashtable hs);
        /// <summary>
        /// 积分统计报表(导出xls)
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        DataTable GetScoreReport(Hashtable hs);
        #endregion

        #region 商品统计报表
        /// <summary>
        /// 商品统计报表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetGoodsSaleReport(Pager p, Hashtable hs);
        /// <summary>
        /// 商品统计报表(导出xls)
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        DataTable GetGoodsReport(Hashtable hs, string strOrderBy);
        #endregion

        #region 订单统计报表
        /// <summary>
        /// 订单统计报表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="hs"></param>
        /// <returns></returns>
        int GetOrderReport(Pager p, Hashtable hs);
        /// <summary>
        /// 订单统计报表(导出xls)
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        DataTable GetOrderReport(Hashtable hs);
        #endregion
    }
}
