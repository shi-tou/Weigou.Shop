using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Weigou.Manage.Sys
{
    public partial class PlatformLogisticsAdd : ManagePage
    {
        /// <summary>
        /// 商户ID
        /// </summary>
        public int _MerchantID
        {
            get { return UserInfo.MerchantID; }
        }
        /// <summary>
        /// ID
        /// </summary>
        public int _ID
        {
            get { return GetRequest("ID", 0); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindInfo();
            }
        }

        private void BindInfo()
        {
            Hashtable hs = new Hashtable();
            DataTable dtLogistics = sysService.GetLogisticsList(hs);
            DataBind(dtLogistics, "DropDownList", ddlLogistics, "Name", "ID");


            DataTable dtProvince = sysService.GetProvinceList(hs);
            DataBind(dtProvince, "CheckBoxList", CheckBoxProvince, "ProvinceName", "ID");

            if (_ID != 0)
            {
                DataTable dt = sysService.GetDataByKey("T_LogisticsTemplate", "ID", _ID);
                if (dt.Rows.Count > 0)
                {
                    ddlLogistics.SelectedValue = dt.Rows[0]["LogisticsID"].ToString();
                    DataRow row = dt.Rows[0];
                    txtName.Text = row["Name"].ToString();
                    txtDefaultPrice.Text = row["DefaultPrice"].ToString();
                    txtRemark.Text = row["Remark"].ToString();
                    DataTable dtMerchantProvince = sysService.GetDataByKey("T_LogisticsProvince", "MerchantLogisticsID", _ID);
                    if (dtMerchantProvince.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtMerchantProvince.Rows)
                        {
                            for (int i = 0; i < CheckBoxProvince.Items.Count; i++)
                            {
                                if (item["ProvinceID"].ToString() == CheckBoxProvince.Items[i].Value)
                                {
                                    CheckBoxProvince.Items[i].Selected = true;
                                }
                            }
                        }
                        txtPrice.Text = dtMerchantProvince.Rows[0]["Price"].ToString();
                    }
                }
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = goodsService.GetDataByKey("T_LogisticsTemplate", "ID", _ID);
            DataRow dr;
            if (dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
            }
            else
            {
                dr = dt.NewRow();
                dt.Rows.Add(dr);
                dr = dt.Rows[0];
                dr["CreateTime"] = DateTime.Now;

            }
            dr["Name"] = txtName.Text.Trim();
            dr["LogisticsID"] = ddlLogistics.SelectedValue;
            dr["DefaultPrice"] = txtDefaultPrice.Text.Trim();
            dr["Remark"] = txtRemark.Text.Trim();
            int merchantLogisticsID = _ID;
            DataTable logisticsDt = sysService.GetMerchantLogistics("");
            if (logisticsDt.Rows.Count == 0 && _ID ==0)
            {
                dr["IsDefault"] = 1;
            }
            int res;
            if (merchantLogisticsID > 0)
            {
                res = goodsService.UpdateDataTable(dt);
            }
            else
            {
                merchantLogisticsID = goodsService.Insert(dt);
                res = 1;
            }
            if (res > 0)
            {
                SaveAttr(merchantLogisticsID);
                RegistScript("CloseWin('操作成功！',parent.GetList);");
            }
            else
            {
                RegistScript("ShowMsg('操作失败');");
            }
        }

        // 操作省份运费
        private void SaveAttr(int merchantLogisticsID)
        {
            if (merchantLogisticsID > 0)
            {
                //删除以前的运费设置
                sysService.Delete("T_LogisticsProvince", " MerchantLogisticsID=" + merchantLogisticsID);


                //验证是否勾选，及是否填写了运费
                string strIDs = "";
                for (int i = 0; i < CheckBoxProvince.Items.Count; i++)
                {
                    if (CheckBoxProvince.Items[i].Selected == true)
                    {
                        strIDs += CheckBoxProvince.Items[i].Value + ",";
                    }
                }
                if (strIDs != "" && txtPrice.Text != "")
                {
                    string[] strID = strIDs.Substring(0, strIDs.Length - 1).Split(',');
                    //循环勾选省份
                    DataTable dt;
                    DataRow dr;
                    for (int i = 0; i < strID.Length; i++)
                    {
                        dt = sysService.GetDataByKey("T_LogisticsProvince", "ID", 0);
                        dr = dt.NewRow();
                        dt.Rows.Add(dr);
                        dr = dt.Rows[0];
                        dr["MerchantLogisticsID"] = merchantLogisticsID;
                        dr["ProvinceID"] = strID[i];
                        dr["Price"] = txtPrice.Text.Trim();
                        goodsService.Insert(dt);
                    }
                }
            }
        }
    }
}