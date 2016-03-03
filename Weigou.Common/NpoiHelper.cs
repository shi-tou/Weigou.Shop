using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using System.IO;
using System.Data;
using System.Web;
using NPOI.HPSF;
using NPOI.SS.Util;
using System.Drawing;

namespace Weigou.Common
{
    /// <summary>
    /// Npoi帮助类
    /// </summary>
    public class NpoiHelper
    {
        public static MemoryStream CreateExecl(string sheetName,string title,string remark, string[] cells)
        {
            try
            {
                HSSFWorkbook wb = new HSSFWorkbook();
                //创建表  
                ISheet sh = wb.CreateSheet(string.IsNullOrEmpty(sheetName) ? "sheet1" : sheetName);

                #region 右击文件 属性信息
                {
                    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                    dsi.Company = "深圳市移商网信息技术有限公司";
                    dsi.Category = "深圳市移商网信息技术有限公司";
                    wb.DocumentSummaryInformation = dsi;

                    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                    si.Author = "深圳市移商网信息技术有限公司"; //填加xls文件作者信息
                    si.ApplicationName = "丝阁平台运营管理系统"; //填加xls文件创建程序信息
                    si.LastAuthor = "深圳市移商网信息技术有限公司"; //填加xls文件最后保存者信息
                    si.Comments = "深圳市移商网信息技术有限公司"; //填加xls文件作者信息
                    si.Title = title; //填加xls文件标题信息
                    si.Subject = title;//填加文件主题信息
                    si.CreateDateTime = DateTime.Now;
                    wb.SummaryInformation = si;
                }
                #endregion

                //CellRangeAddress() 该方法的参数次序是：开始行号，结束行号，开始列号，结束列号。
                sh.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, cells.Length));
                
                #region 标题
                int rowIndex = 0;
                if (!string.IsNullOrEmpty(title))
                {
                    IRow rowTitle = sh.CreateRow(rowIndex);
                    rowTitle.Height = 30 * 20;
                    ICell cellTitle = rowTitle.CreateCell(0);
                    cellTitle.CellStyle = Getcellstyle(wb, stylexls.标题);
                    cellTitle.SetCellValue(title);
                    rowIndex++;
                }
                #endregion

                #region 表格说明
                sh.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 0, cells.Length));
                if (!string.IsNullOrEmpty(remark))
                {
                    IRow rowRemark = sh.CreateRow(rowIndex);
                    rowRemark.Height = 60 * 20;
                    ICell cellRemark = rowRemark.CreateCell(0);
                    cellRemark.CellStyle = Getcellstyle(wb, stylexls.说明);
                    cellRemark.SetCellValue(remark);
                    rowIndex++;
                }
                #endregion

                #region 设置表头
                IRow rowHead = sh.CreateRow(rowIndex);
                rowHead.Height = 20 * 20;
                for (int j = 0; j < cells.Length; j++)
                {
                    //设置单元的宽度  
                    sh.SetColumnWidth(j, (cells[j].Length + 10) * 256);
                    //单元
                    ICell cellHead = rowHead.CreateCell(j);
                    cellHead.CellStyle = Getcellstyle(wb, stylexls.头);
                    cellHead.SetCellValue(cells[j]);
                }
                rowIndex++;
                #endregion

                #region 设置留白样式
                ICellStyle blankStyle = Getcellstyle(wb, stylexls.默认);
                for (int i = rowIndex; i < 999; i++)
                {
                    IRow row = sh.CreateRow(i);
                    for (int j = 0; j < cells.Length; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.CellStyle = blankStyle;
                        cell.SetCellValue("");
                    }
                }
                #endregion

                using (var ms = new MemoryStream())
                {
                    wb.Write(ms);
                    ms.Flush();
                    ms.Position = 0;
                    return ms;
                }
            }
            catch (Exception ex)
            {
                Utils.SaveLog("NpoiHelper.CreateExecl", ex.Message);
                return null;
            }
        }
        /// <summary>
        ///导出Execl(内存流MemoryStream)
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static MemoryStream Export(DataTable dtSource,string sheetName, string title)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(string.IsNullOrEmpty(sheetName) ? "sheet1" : sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "深圳市移商网信息技术有限公司";
                dsi.Category = "深圳市移商网信息技术有限公司";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "深圳市移商网信息技术有限公司"; //填加xls文件作者信息
                si.ApplicationName = "微车平台运营管理系统"; //填加xls文件创建程序信息
                si.LastAuthor = "深圳市移商网信息技术有限公司"; //填加xls文件最后保存者信息
                si.Comments = "深圳市移商网信息技术有限公司"; //填加xls文件作者信息
                si.Title = title; //填加xls文件标题信息
                si.Subject = title;//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }

                    #region 标题
                    IRow titleRow = sheet.CreateRow(0);
                    titleRow.HeightInPoints = 25;

                    ICell titleCell = titleRow.CreateCell(0);
                    titleCell.SetCellValue(title);
                    titleCell.CellStyle = Getcellstyle(workbook, stylexls.头);

                    sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));
                    rowIndex++;
                    #endregion

                    #region 列头及样式
                    IRow headRow = sheet.CreateRow(1);
                    ICellStyle headStyle = Getcellstyle(workbook, stylexls.头);
                    foreach (DataColumn dc in dtSource.Columns)
                    {
                        ICell headCell = headRow.CreateCell(dc.Ordinal);
                        headCell.SetCellValue(dc.ColumnName);
                        headCell.CellStyle = headStyle;

                        //设置列宽
                        sheet.SetColumnWidth(dc.Ordinal, (arrColWidth[dc.Ordinal] + 1) * 256);
                    }
                    rowIndex++;
                    #endregion
                }
                #endregion

                #region 填充内容
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    ICell newCell = dataRow.CreateCell(column.Ordinal);
                    string drValue = row[column].ToString();
                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                }
                #endregion

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        /// <summary>
        /// 用于Web导出
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">文件名</param>
        public static void ExportByWeb(DataTable dtSource, string sheetName, string title, string fileName)
        {
            HttpContext curContext = HttpContext.Current;

            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));

            curContext.Response.BinaryWrite(Export(dtSource, sheetName, title).GetBuffer());
            curContext.Response.End();
        }
        /// <summary>
        /// 读取Excel文件
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <param name="headIndex">标头索引</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName)
        {
            return Import(strFileName, 0);
        }
        /// <summary>
        /// 读取Excel文件
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <param name="headIndex">标头索引</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName, int headIndex)
        {
            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            //取标头，并构造Datatable结构
            IRow headerRow = sheet.GetRow(headIndex);
            int cellCount = headerRow.LastCellNum;
            DataTable dt = new DataTable();
            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                cell.SetCellType(CellType.STRING);
                dt.Columns.Add(cell.ToString(), typeof(string));
            }
            //开始填充数据
           
            for (int i = (sheet.FirstRowNum + headIndex + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                ICell cell = row.GetCell(row.FirstCellNum);
                if (string.IsNullOrEmpty(cell.StringCellValue))
                {
                    continue;
                }
                DataRow dr = dt.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    cell = row.GetCell(j);
                    if (!string.IsNullOrEmpty(cell.StringCellValue))
                    {
                        dr[j] = cell.StringCellValue;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        #region 定义单元格常用到样式的枚举
        public enum stylexls
        {
            标题,
            头,
            说明,
            url,
            时间,
            数字,
            钱,
            百分比,
            中文大写,
            科学计数法,
            默认
        }
        #endregion

        #region 定义单元格常用到样式
        static ICellStyle Getcellstyle(IWorkbook wb, stylexls str)
        {
            ICellStyle cellStyle = wb.CreateCellStyle();
            IDataFormat dataFormat = wb.CreateDataFormat();
            //默认字体属性，特殊的的另外处理
            IFont font = wb.CreateFont();
            font.FontName = "宋体";
            font.FontHeightInPoints = 10;
            //水平对齐  
            cellStyle.Alignment = HorizontalAlignment.LEFT;
            //垂直对齐  
            cellStyle.VerticalAlignment = VerticalAlignment.CENTER;
            //自动换行  
            cellStyle.WrapText = true;
            //缩进;当设置为1时，前面留的空白太大了。希旺官网改进。或者是我设置的不对  
            cellStyle.Indention = 0;
            cellStyle.DataFormat = dataFormat.GetFormat("@");
            //上面基本都是公共的设置  
            //下面列出了常用的字段类型  
            
            switch (str)
            {
                case stylexls.标题:
                    font.Boldweight = 700;
                    font.FontHeightInPoints = 20;
                    font.FontName = "微软雅黑";
                    cellStyle.Alignment = HorizontalAlignment.CENTER;
                    cellStyle.SetFont(font);
                    break;
                case stylexls.头:
                    cellStyle.Alignment = HorizontalAlignment.CENTER;
                    cellStyle.BorderBottom = BorderStyle.THIN;
                    cellStyle.BorderLeft = BorderStyle.THIN;
                    cellStyle.BorderRight = BorderStyle.THIN;
                    cellStyle.BorderTop = BorderStyle.THIN;
                    font.FontName = "微软雅黑";
                    font.Color = HSSFColor.OLIVE_GREEN.RED.index;
                    cellStyle.SetFont(font);
                    break;
                case stylexls.说明:
                    font.Color = HSSFColor.OLIVE_GREEN.RED.index;
                    cellStyle.SetFont(font);
                    break;
                case stylexls.时间:
                    cellStyle.DataFormat = dataFormat.GetFormat("yyyy/mm/dd");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.数字:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.钱:
                    IDataFormat format = wb.CreateDataFormat();
                    cellStyle.DataFormat = format.GetFormat("￥#,##0");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.url:
                    font.Underline = 1;
                    font.Color = HSSFColor.OLIVE_GREEN.BLUE.index;
                    font.IsItalic = true;//下划线  
                    cellStyle.SetFont(font);
                    break;
                case stylexls.百分比:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00%");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.中文大写:
                    cellStyle.DataFormat = dataFormat.GetFormat("[DbNum2][$-804]0");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.科学计数法:
                    cellStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("0.00E+00");
                    cellStyle.SetFont(font);
                    break;
                case stylexls.默认:
                    cellStyle.SetFont(font);
                    break;
            }
            return cellStyle;


        }
        #endregion  

        /// <summary>
        /// 构造导出模板
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static DataTable GetRportTemplate(string[] arr)
        {
            DataTable dt = new DataTable();
            foreach (string s in arr)
            {
                dt.Columns.Add(s, typeof(string));
            }
            return dt;
        }
    }
}
