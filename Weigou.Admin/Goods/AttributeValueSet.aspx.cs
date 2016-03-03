using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weigou.Model.Enum;

namespace Weigou.Admin.Goods
{
    public partial class AttributeValueSet : AdminPage
    {
        /// <summary>
        /// 属性名ID
        /// </summary>
        public int AttributeID
        {
            get { return GetRequest("AttributeID", 0); }
        }
        /// <summary>
        /// 属性别名
        /// </summary>
        public string AttributeAlias
        {
            get { return GetRequest("AttributeAlias", ""); }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ToolBar1.PrivilegeCode = "AttributeValueSet";
                this.hfAttributeID.Value = AttributeID.ToString();
                this.labAttribute.Text = AttributeAlias;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = goodsService.GetDataByKey("T_GoodsAttributeValue", "ID", this.hfValueID.Value);
            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
            }
            else
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr["CreateTime"] = DateTime.Now;
                dr["CreateBy"] = UserInfo.ID;
            }
            dr["AttributeID"] = AttributeID;
            dr["Value"] = this.txtValue.Text;
            dr["Status"] = ((int)EnumMemStatus.Normal).ToString();
            dr["CreateBy"] = UserInfo.ID;
            dr["Sort"] = this.txtSort.Text;
            dr["CreateTime"] = DateTime.Now;

            int res = goodsService.UpdateDataTable(dt);
            if (res > 0)
            {
                Response.Redirect(Request.Url.ToString());
            }
            else
                InvokeScript("CloseWin('操作失败！')");
        }
    }
}