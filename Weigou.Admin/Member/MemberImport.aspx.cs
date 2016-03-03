using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Weigou.Common;
using System.Data;
using Weigou.Model;

namespace Weigou.Admin.Member
{
    public partial class MemberImport : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            int res = 0;

            #region 验证控件
            //是否有文件
            if (!this.fileUpload.HasFile)
            {
                AlertInfo("请洗择要导入的文件");
                return;
            }
            //是否是Excel文件
            bool fileOK = false;
            string fileExtension = Path.GetExtension(this.fileUpload.FileName).ToLower();
            string[] allowExtension = { ".xls", ".xlsx" };
            foreach (string s in allowExtension)
            {
                if (fileExtension == s)
                {
                    fileOK = true;
                    break;
                }
            }
            if (!fileOK)
            {
                AlertInfo("请洗择正确的Excel文件格式");
                return;
            }
            #endregion

            //读取文件数据
            string uploadDir = Server.MapPath("~/Upload/");
            string fileName = uploadDir + this.fileUpload.FileName;
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                this.fileUpload.SaveAs(fileName);
                DataTable dtExcel = NpoiHelper.Import(fileName, 2);
                if (dtExcel.Rows.Count == 0)
                {
                    AlertInfo("选择的Excel文件没有数据");
                    return;
                }
                
                DataTable dtM = memberService.GetDataByKey("T_Member", "ID", 0);
                DataRow drM;
                DateTime registTime = DateTime.Now;
                string[] fields = ExcelTemplate.ImportMemberField;
                string[] cells = ExcelTemplate.ImportMemberCells;
                for (int i = 0; i < dtExcel.Rows.Count; i++)
                {
                    DataRow dr = dtExcel.Rows[i];
                    string row = (i + 2).ToString();
                    //验证数据
                    if (!ValidMemberData(dr, row))
                    {
                        return;
                    }
                    //填充数据表
                    drM = dtM.NewRow();
                    for (int j = 0; j < fields.Length; j++)
                    {
                        if (Convert.ToString(dr[cells[j]]) != "")
                        {
                            drM[fields[j]] = dr[cells[j]];
                        }
                    }
                    drM["Status"] = 1;
                    drM["RegistTime"] = registTime;
                    drM["CreateBy"] = UserInfo.ID;
                    dtM.Rows.Add(drM);
                }
               res= memberService.UpdateDataTable(dtM);
            }
            catch (Exception ex)
            {
                Utils.SaveLog("批量导入会员", ex.Message);
                res = 0;
            }
            if (res == 0)
            {
                AlertInfo("导入失败，请与管理员联系！");
            }
            else
            {
                AlertInfo("导入成功，请到会员列表查看数据！");
            }
        }
        /// <summary>
        /// 验证数据的有效性
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool ValidMemberData(DataRow dr,string row)
        {
            string nullMsg = "Excel表格第{0}行的【{1}】为空，请修改后再进行操作";
            string invalidMsg = "Excel表格第{0}行的【{1}】数据无效，请修改后再进行操作";
            //会员卡
            if (Convert.ToString(dr["会员卡号"]) == "")
            {
                AlertInfo(string.Format(nullMsg, row, "会员卡号"));
                return false;
            }
            //密码
            if (Convert.ToString(dr["卡密码"]) == "")
            {
                AlertInfo(string.Format(nullMsg, row, "卡密码"));
                return false;
            }
            //姓名
            if (Convert.ToString(dr["姓名"]) == "")
            {
                AlertInfo(string.Format(nullMsg, row, "姓名"));
                return false;
            }
            //性别
            string sex = Convert.ToString(dr["性别"]);
            if (sex == "")
            {
                AlertInfo(string.Format(nullMsg, row, "性别"));
                return false;
            }
            else if (!RegexHelper.ValidInteger(sex))
            {
                AlertInfo(string.Format(invalidMsg, row, "性别"));
                return false;
            }
            //生日
            string birthday = Convert.ToString(dr["生日"]);
            if (birthday == "")
            {
                AlertInfo(string.Format(nullMsg, row, "生日"));
                return false;
            }
            else if (!RegexHelper.ValidDateTime(birthday))
            {
                AlertInfo(string.Format(invalidMsg, row, "生日"));
                return false;
            }
            //手机
            string mobileNo = Convert.ToString(dr["手机号"]);
            if (mobileNo != "" && !RegexHelper.ValidMobileNo(mobileNo))
            {
                AlertInfo(string.Format(invalidMsg, row, "手机号"));
                return false;
            }
            //邮箱
            string email = Convert.ToString(dr["常用邮箱"]);
            if (email != "" && !RegexHelper.ValidEmail(email))
            {
                AlertInfo(string.Format(invalidMsg, row, "常用邮箱"));
                return false;
            }
            //等级
            string level = Convert.ToString(dr["会员等级编号"]);
            if (level == "")
            {
                AlertInfo(string.Format(nullMsg, row, "会员等级编号"));
                return false;
            }
            else if (!RegexHelper.ValidInteger(level))
            {
                AlertInfo(string.Format(invalidMsg, row, "会员等级编号"));
                return false;
            }
            //金额
            string amount = Convert.ToString(dr["初始金额"]);
            if (amount != "" && !RegexHelper.ValidNumber(amount))
            {
                AlertInfo(string.Format(invalidMsg, row, "初始金额"));
                return false;
            }
            //积分
            string score = Convert.ToString(dr["初始积分"]);
            if (score != "" && !RegexHelper.ValidNumber(score))
            {
                AlertInfo(string.Format(invalidMsg, row, "初始积分"));
                return false;
            }
            return true;
        }
    }
}
