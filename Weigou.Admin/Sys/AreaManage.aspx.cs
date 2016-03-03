using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Service;
using System.Data;
using Weigou.Common;

namespace Weigou.Admin.Sys
{
    public partial class AreaManage : AdminPage
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ToolBar_P.PrivilegeCode = "ProvinceList";
                this.ToolBar_C.PrivilegeCode = "CityList";
                this.ToolBar_D.PrivilegeCode = "DistrictList";
            }
        }
        /// <summary>
        /// 处理地区数据
        /// </summary>
        public void DealArea()
        {
            DataTable dt = sysService.GetData("T_Province");
            foreach (DataRow dr in dt.Rows)
            {
                dr["Spell"] = Chinese2Spell.ConvertWithSplitChar(dr["ProvinceName"].ToString().Replace("市","").Replace("自治区","").Replace("省",""), "");
                dr["FirstLetter"] = dr["Spell"].ToString().Substring(0, 1);
            }
            sysService.UpdateDataTable(dt);
        }
    }
}
