using System;
using System.Collections.Generic;
using System.Text;
using Weigou.Model;
using System.Web.UI.WebControls;
using Weigou.Model.Enum;
using System.Data;

namespace Weigou.Manage
{
    /// <summary>
    /// 功能类
    /// </summary>
    public class Func
    {    
        #region 绑定相关
        /// <summary>
        /// 绑定数据到下拉框(公用)
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="t"></param>
        /// <param name="v"></param>
        /// <param name="blank"></param>
        public static void BindDropDownList(DropDownList ddl, DataTable dt, string dataTextField, string dataValueField, string blankName)
        {
            if (!string.IsNullOrEmpty(blankName))
            {
                ddl.Items.Add(new ListItem(blankName, ""));
            }
            foreach (DataRow dr in dt.Rows)
            {
                ddl.Items.Add(new ListItem(dr["Name"].ToString(), dr["ID"].ToString()));
            }
        }
        /// <summary>
        /// 绑定性别
        /// </summary>
        public static void BindSex(DropDownList ddl, string blankName)
        {
            if (!string.IsNullOrEmpty(blankName))
            {
                ddl.Items.Add(new ListItem(blankName, ""));
            }
            ddl.Items.Add(new ListItem("保密", ((int)EnumSex.Unknown).ToString()));
            ddl.Items.Add(new ListItem("男", ((int)EnumSex.M).ToString()));
            ddl.Items.Add(new ListItem("女", ((int)EnumSex.F).ToString()));
        }
        /// <summary>
        /// 绑定会员状态
        /// </summary>
        public static void BindMemStatus(DropDownList ddl, string blankName)
        {
            if (!string.IsNullOrEmpty(blankName))
            {
                ddl.Items.Add(new ListItem(blankName, ""));
            }
            ddl.Items.Add(new ListItem("启用", ((int)EnumMemStatus.Normal).ToString()));
            ddl.Items.Add(new ListItem("冻结", ((int)EnumMemStatus.Locked).ToString()));
        }
        /// <summary>
        /// 绑定系统功能模块
        /// </summary>
        public static void BindModule(DropDownList ddl, string blankName)
        {
            if (!string.IsNullOrEmpty(blankName))
            {
                ddl.Items.Add(new ListItem(blankName, ""));
            }
            ddl.Items.Add(new ListItem("商户管理", ((int)EnumModule.MerchantManage).ToString()));
            ddl.Items.Add(new ListItem("会员管理", ((int)EnumModule.MemberManage).ToString()));
            ddl.Items.Add(new ListItem("商品管理", ((int)EnumModule.GoodsManage).ToString()));
            ddl.Items.Add(new ListItem("积分管理", ((int)EnumModule.ScoreManage).ToString()));
            ddl.Items.Add(new ListItem("内容管理", ((int)EnumModule.ContentManage).ToString()));
            ddl.Items.Add(new ListItem("报表管理", ((int)EnumModule.ReportManage).ToString()));
            ddl.Items.Add(new ListItem("短信管理", ((int)EnumModule.SmsManage).ToString()));
            ddl.Items.Add(new ListItem("系统管理", ((int)EnumModule.SystemManage).ToString()));
            ddl.Items.Add(new ListItem("其他", ((int)EnumModule.Other).ToString()));
        }
        /// <summary>
        /// 绑定功能操作类型
        /// </summary>
        public static void BindOperation(DropDownList ddl, string blankName)
        {
            if (!string.IsNullOrEmpty(blankName))
            {
                ddl.Items.Add(new ListItem(blankName, ""));
            }
            ddl.Items.Add(new ListItem("登录", ((int)EnumOperation.Login).ToString()));
            ddl.Items.Add(new ListItem("添加", ((int)EnumOperation.Add).ToString()));
            ddl.Items.Add(new ListItem("编辑", ((int)EnumOperation.Edit).ToString()));
            ddl.Items.Add(new ListItem("删除", ((int)EnumOperation.Delete).ToString()));
            ddl.Items.Add(new ListItem("审核", ((int)EnumOperation.Audit).ToString()));
            ddl.Items.Add(new ListItem("导入", ((int)EnumOperation.Import).ToString()));
            ddl.Items.Add(new ListItem("导出", ((int)EnumOperation.Export).ToString()));
            
            ddl.Items.Add(new ListItem("其他", ((int)EnumOperation.Other).ToString()));
        }
 
        #endregion
    }
}
