using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using System.Data;
using Weigou.Common;
using Weigou.Model;
using System.Collections;
using Weigou.Model.Enum;

namespace Weigou.Admin.Ajax
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Report : BaseAjax, IHttpHandler, IRequiresSessionState
    {
        #region 会员统计管理
        /// <summary>
        /// 会员统计
        /// </summary>
        /// <param name="hc"></param>
        public void GetMemberReport(HttpContext hc)
        {
            Hashtable hs = GetMemberReportWhere(hc);
            Pager p = new Pager(PageSize, PageIndex, "a.ID desc");
            reportService.GetMemberReport(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 会员导出Execl
        /// </summary>
        /// <param name="hc"></param>
        public void ExportMemberReport(HttpContext hc)
        {
            Hashtable hs = GetMemberReportWhere(hc);
            DataTable dt = reportService.GetMemberReport(hs);
            string[] fields = ExcelTemplate.ExportMemberField;
            string[] cells = ExcelTemplate.ExportMemberCells;
            DataTable dtReport = NpoiHelper.GetRportTemplate(cells);
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drNew = dtReport.NewRow();
                for (int i = 0; i < cells.Length; i++)
                {
                    //性别
                    if (fields[i].ToLower() == "sex")
                    {
                        drNew[cells[i]] = EnumHelper.GetSex(dr[fields[i]]);
                        continue;
                    }
                    //状态
                    if (fields[i].ToLower() == "status")
                    {
                        drNew[cells[i]] = EnumHelper.GetStatus(dr[fields[i]]);
                        continue;
                    }
                    //生日
                    if (fields[i].ToLower() == "birthday")
                    {
                        if (Convert.ToString(dr[fields[i]]).Trim() == "")
                        {
                            drNew[cells[i]] = "";
                        }
                        else
                        {
                            drNew[cells[i]] = Convert.ToDateTime(dr[fields[i]]).ToString("MM-dd");
                        }
                        continue;
                    }
                    drNew[cells[i]] = dr[fields[i]];
                }
                dtReport.Rows.Add(drNew);
            }
            DateTime date = DateTime.Now;
            ExportExcel(hc, dtReport, "MemberRepor-" + date.Year + "-" + date.Month + "-" + date.Day + ".xls.xls", "会员统计报表");
        }
        /// <summary>
        ///获取会员搜索条件
        /// </summary>
        /// <param name="hc"></param>
        /// <returns></returns>
        public Hashtable GetMemberReportWhere(HttpContext hc)
        {
            string name = GetRequest("Name", "");
            string email = GetRequest("Email", "");
            string mobileNo = GetRequest("MobileNo", "");
            string sex = GetRequest("Sex", "");
            string status = GetRequest("Status", "");
            string minTime = GetRequest("MinTime", "");
            string maxTime = GetRequest("MaxTime", "");
            Hashtable hs = new Hashtable();
            if (name != "")
            {
                hs.Add("Name", name);
            }
            if (email != "")
            {
                hs.Add("Email", email);
            }
            if (mobileNo != "")
            {
                hs.Add("MobileNo", mobileNo);
            }
            if (sex != "")
            {
                hs.Add("Sex", sex);
            }
            if (status != "")
            {
                hs.Add("Status", status);
            }
            if (minTime != "")
            {
                hs.Add("MinTime", minTime);
            }
            if (maxTime != "")
            {
                hs.Add("MaxTime", maxTime);
            }
            return hs;
        }
        #endregion

        #region 积分统计管理
        /// <summary>
        /// 积分统计
        /// </summary>
        /// <param name="hc"></param>
        public void GetScoreReport(HttpContext hc)
        {
            Hashtable hs = GetScoreReportWhere(hc);
            Pager p = new Pager(PageSize, PageIndex, "a.ID desc");
            reportService.GetScoreReport(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 积分导出Execl
        /// </summary>
        /// <param name="hc"></param>
        public void ExportScoreReport(HttpContext hc)
        {
            Hashtable hs = GetScoreReportWhere(hc);
            DataTable dt = reportService.GetScoreReport(hs);
            string[] fields = ExcelTemplate.ExportScoreField;
            string[] cells = ExcelTemplate.ExportScoreCells;
            DataTable dtReport = NpoiHelper.GetRportTemplate(cells);
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drNew = dtReport.NewRow();
                for (int i = 0; i < cells.Length; i++)
                {
                    //性别
                    if (fields[i].ToLower() == "scoretype")
                    {
                        drNew[cells[i]] = drNew[cells[i]] = EnumHelper.GetScoreType(dr[fields[i]]);
                        continue;
                    }
                    drNew[cells[i]] = dr[fields[i]];
                }
                dtReport.Rows.Add(drNew);
            }
            DateTime date = DateTime.Now;
            ExportExcel(hc, dtReport, "ScoreReport-" + date.Year + "-" + date.Month + "-" + date.Day + ".xls", "积分统计报表");
        }
        /// <summary>
        ///获取积分搜索条件
        /// </summary>
        /// <param name="hc"></param>
        /// <returns></returns>
        public Hashtable GetScoreReportWhere(HttpContext hc)
        {
            string membername = GetRequest("MemberName", "");
            string type = GetRequest("Type", "");
            string scoretype = GetRequest("ScoreType", "");
            string minTime = GetRequest("MinTime", "");
            string maxTime = GetRequest("MaxTime", "");
            Hashtable hs = new Hashtable();
            if (membername != "")
            {
                hs.Add("MemberName", membername);
            }           
            if (type != "")
            {
                hs.Add("Type", type);
            }
            if (scoretype != "")
            {
                hs.Add("ScoreType", scoretype);
            }
            if (minTime != "")
            {
                hs.Add("MinTime", minTime);
            }
            if (maxTime != "")
            {
                hs.Add("MaxTime", maxTime);
            }
            return hs;
        }
        #endregion

        #region 商品统计管理
        /// <summary>
        /// 会员统计
        /// </summary>
        /// <param name="hc"></param>
        public void GetGoodsReport(HttpContext hc)
        {
            Hashtable hs = GetGoodsReportWhere(hc);
            Pager p = new Pager(PageSize, PageIndex, "a.CreateTime desc");
            reportService.GetGoodsSaleReport(p, hs);
            ResponseWrite(hc, p);
        }

        /// <summary>
        /// 会员导出Execl
        /// </summary>
        /// <param name="hc"></param>
        public void ExportGoodsReport(HttpContext hc)
        {
            Hashtable hs = GetGoodsReportWhere(hc);
            DataTable dt = reportService.GetGoodsReport(hs, " order by a.CreateTime desc");
            string[] fields = ExcelTemplate.ExportGoodsField;
            string[] cells = ExcelTemplate.ExportGoodsCells;
            DataTable dtReport = NpoiHelper.GetRportTemplate(cells);
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drNew = dtReport.NewRow();
                for (int i = 0; i < cells.Length; i++)
                {
                    //审核状态
                    if (fields[i].ToLower() == "status")
                    {
                        drNew[cells[i]] = EnumHelper.GetGoodsStatus(dr[fields[i]]);
                        continue;
                    }
                    //上架状态
                    if (fields[i].ToLower() == "shelvesstatus")
                    {
                        drNew[cells[i]] = EnumHelper.GetShelvesStatus(dr[fields[i]]);
                        continue;
                    }
                    //审核时间
                    if (fields[i].ToLower() == "approvedtime")
                    {
                        if (Convert.ToString(dr[fields[i]]).Trim() == "")
                        {
                            drNew[cells[i]] = "";
                        }
                        else
                        {
                            drNew[cells[i]] = Convert.ToDateTime(dr[fields[i]]);
                        }
                        continue;
                    }
                    drNew[cells[i]] = dr[fields[i]];
                }
                dtReport.Rows.Add(drNew);
            }
            DateTime date = DateTime.Now;
            ExportExcel(hc, dtReport, "GoodsReport-" + date.Year + "-" + date.Month + "-" + date.Day + ".xls", "商品统计报表");
        }


        /// <summary>
        ///获取商品搜索条件
        /// </summary>
        /// <param name="hc"></param>
        /// <returns></returns>
        public Hashtable GetGoodsReportWhere(HttpContext hc)
        {
            string GoodsName = GetRequest("GoodsName", "");
            string GoodsType = GetRequest("GoodsType", "");
            string GoodsStatus = GetRequest("GoodsStatus", "");
            string GoodsShelvesStatus = GetRequest("GoodsShelvesStatus", "");
            Hashtable hs = new Hashtable();
            if (!string.IsNullOrEmpty(GoodsName))
            {
                hs.Add("Name", GoodsName);
            }
            if (!string.IsNullOrEmpty(GoodsType))
            {
                hs.Add("Type", GoodsType);
            }
            if (!string.IsNullOrEmpty(GoodsStatus))
            {
                hs.Add("Status", GoodsStatus);
            }
            if (!string.IsNullOrEmpty(GoodsShelvesStatus))
            {
                hs.Add("ShelvesStatus", GoodsShelvesStatus);
            }
            return hs;
        }
        #endregion

        #region 订单统计管理
        /// <summary>
        /// 订单统计
        /// </summary>
        /// <param name="hc"></param>
        public void GetOrderReport(HttpContext hc)
        {
            Hashtable hs = GetOrderReportWhere(hc);
            Pager p = new Pager(PageSize, PageIndex, "a.ID desc");
            reportService.GetOrderReport(p, hs);
            ResponseWrite(hc, p);
        }
        /// <summary>
        /// 订单导出Execl
        /// </summary>
        /// <param name="hc"></param>
        public void ExportOrderReport(HttpContext hc)
        {
            Hashtable hs = GetOrderReportWhere(hc);
            DataTable dt = reportService.GetOrderReport(hs);
            string[] fields = ExcelTemplate.ExportOrderField;
            string[] cells = ExcelTemplate.ExportOrderCells;
            DataTable dtReport = NpoiHelper.GetRportTemplate(cells);
            foreach (DataRow dr in dt.Rows)
            {
                DataRow drNew = dtReport.NewRow();
                for (int i = 0; i < cells.Length; i++)
                {
                    //订单状态
                    if (fields[i].ToLower() == "orderstatus")
                    {
                        drNew[cells[i]] = drNew[cells[i]] = EnumHelper.GetMallOrderStatuss(dr[fields[i]]);
                        continue;
                    }
                    //支付类型
                    if (fields[i].ToLower() == "paymenttype")
                    {
                        drNew[cells[i]] = drNew[cells[i]] = EnumHelper.GetPayMent(dr[fields[i]]);
                        continue;
                    }
                    //订单来源
                    if (fields[i].ToLower() == "source")
                    {
                        drNew[cells[i]] = drNew[cells[i]] = EnumHelper.GetSourceName(dr[fields[i]]);
                        continue;
                    }
                    drNew[cells[i]] = dr[fields[i]];
                }
                dtReport.Rows.Add(drNew);
            }
            DateTime date = DateTime.Now;
            reportService.SaveSysLog("", EnumModule.ReportManage, EnumOperation.Export, UserInfo.ID, "导出订单统计表");
            ExportExcel(hc, dtReport, "OrderReport-" + date.Year + "-" + date.Month + "-" + date.Day + ".xls", "订单统计报表");
        }
        /// <summary>
        ///获取订单搜索条件
        /// </summary>
        /// <param name="hc"></param>
        /// <returns></returns>
        public Hashtable GetOrderReportWhere(HttpContext hc)
        {
            string orderno = GetRequest("OrderNo", "");
            string membername = GetRequest("MemberName", "");
            string merchantname = GetRequest("MerchantName", "");
            string membermobileno = GetRequest("MemberMobileNo", "");
            string orderstatus = GetRequest("OrderStatus", "");
            string minTime = GetRequest("MinTime", "");
            string maxTime = GetRequest("MaxTime", "");
            Hashtable hs = new Hashtable();
            if (membername != "")
            {
                hs.Add("MemberName", membername);
            }
            if (merchantname != "")
            {
                hs.Add("MerchantName", merchantname);
            }
            if (orderno != "")
            {
                hs.Add("OrderNo", orderno);
            }
            if (membermobileno != "")
            {
                hs.Add("MemberMobileNo", membermobileno);
            }
            if (orderstatus != "")
            {
                hs.Add("OrderStatus", orderstatus);
            }
            if (minTime != "")
            {
                hs.Add("MinTime", minTime);
            }
            if (maxTime != "")
            {
                hs.Add("MaxTime", maxTime);
            }
            return hs;
        }
        #endregion
    }
}
