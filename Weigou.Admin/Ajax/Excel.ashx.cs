using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.SessionState;
using System.Data;
using Weigou.Common;
using Weigou.Model;

namespace Weigou.Admin.Ajax
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Excel : BaseAjax, IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 生成导入会员Excel模板文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CreateExcelTempForMem(HttpContext hc)
        {
            string remark = "特别说明：";
            DataTable dt = memberService.GetData("T_Level");
            remark += "\n会员等级编号：";
            foreach (DataRow dr in dt.Rows)
            {
                remark += dr["ID"].ToString() + "-" + dr["Name"].ToString() + ";";
            }
            remark += "\n性别：0-保密 1-男 2-女";
            remark += "\n请严格按照相关格式填写，相关金额、数量、积分请填写数字，如果不填写系统将默认“0”。会员生日格式形如2014-10-10";
            string[] cells = ExcelTemplate.ImportMemberCells;
            ExportExcel(hc, NpoiHelper.CreateExecl( null,"会员信息批量录入模板", remark, cells), "会员导入数据模板.xls");
        }
    }
}
