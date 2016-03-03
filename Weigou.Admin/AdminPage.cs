using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Weigou.Model;
using Weigou.Common;
using Weigou.Service;
using System.Collections;
using Weigou.Model.Enum;

namespace Weigou.Admin
{
    public class AdminPage : BasePage
    {
        public string UserCookieName = "SysUserInfo";
        /// <summary>
        /// 当前登录管理员
        /// </summary>
        public UserInfo UserInfo
        {
            get
            {
                if (System.Web.HttpContext.Current.Request.Cookies[UserCookieName] == null)
                {
                    return null;
                }
                string str = DESEncrypt.Decrypt(System.Web.HttpContext.Current.Request.Cookies[UserCookieName].Value);
                return (UserInfo)Newtonsoft.Json.JsonConvert.DeserializeObject(str, typeof(UserInfo));
            }
        }
        /// <summary>
        /// PageTitle
        /// </summary>
        public string PageTitle
        {
            get { return Utils.GetConfig("PageTitle"); }
        }
        /// <summary>
        /// 图片服务器地址
        /// </summary>
        public string PictureServerPath
        {
            get { return Utils.GetConfig("PictureServerPath"); }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            RegisterIncScriptBlock();
            IsLogin();
            base.OnInit(e);
        }
        /// <summary>
        /// 判断是否登录）
        /// </summary>
        public void IsLogin()
        {
            if (UserInfo == null)
            {
                RegistScript("alert('未登录或登录已失效！');window.parent.location.href='/Login.aspx';");
            }
            return;
        }
        /// <summary>
        /// 注册INC脚本块
        /// </summary>
        public void RegisterIncScriptBlock()
        {
            this.Header.Controls.AddAt(1, RegistCSS("/JScript/easyui/themes/ui-cupertino/easyui.css"));
            this.Header.Controls.AddAt(2, RegistCSS("/JScript/easyui/themes/icon.css"));
            this.Header.Controls.AddAt(3, RegistCSS("/Style/admin.css"));

            this.Header.Controls.AddAt(4, RegistJavaScript("/JScript/easyui/jquery-1.8.0.min.js"));
            this.Header.Controls.AddAt(5, RegistJavaScript("/JScript/easyui/jquery.easyui.min.js"));
            this.Header.Controls.AddAt(6, RegistJavaScript("/JScript/layer/layer.min.js"));
            this.Header.Controls.AddAt(7, RegistJavaScript("/JScript/global.js"));
            this.Header.Controls.AddAt(8, RegistJavaScript("/JScript/customValidate.js"));
            this.Header.Controls.AddAt(9, RegistJavaScript("/JScript/euiHelper.js"));
            this.Header.Controls.AddAt(10, RegistJavaScript("/JScript/common.js"));
        }
        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="message"></param>
        public void AlertInfo(string message)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>AlertInfo('" + message + "')</script>");
        }
        /// <summary>
        /// 输出JavaScript
        /// </summary>
        /// <param name="strScript"></param>
        public void RegistScript(string strScript)
        {
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>" + strScript + "</script>");
        }

        #region 商品类别/礼品类别/商户类别
        /// <summary>
        /// 商品类别
        /// </summary>
        /// <param name="ddl"></param>
        public void BindGoodsType(DropDownList ddl, string blank)
        {
            DataTable dt = goodsService.GetData("T_GoodsType");
            int index = 0;
            BindType(ddl, dt, 0, ref index);
            if (!string.IsNullOrEmpty(blank))
            {
                ddl.Items.Insert(0, new ListItem(blank, ""));
            }
        }
        /// <summary>
        /// 递归绑定子父类别
        /// </summary>
        public void BindType(DropDownList ddl, DataTable dt, int pid, ref int index)
        {
            index++;
            DataTable dtTemp = Utils.SelectDataTable(dt, "ParentID=" + pid);
            foreach (DataRow dr in dtTemp.Rows)
            {
                ddl.Items.Add(new ListItem(GetBlank(index) + dr["Name"].ToString(), dr["ID"].ToString()));
                BindType(ddl, dt, Convert.ToInt32(dr["ID"]), ref index);
            }
            index--;
        }
        public string GetBlank(int index)
        {
            string blank = "";
            int i = 0;
            while (i < index)
            {
                blank += "--";
                i++;
            }
            return blank;
        }
        #endregion

        #region 数据绑定到CheckBoxList,DropDownList,ListBox,RadioButtonList
        /// <summary>
        /// 数据绑定到CheckBoxList,DropDownList,ListBox,RadioButtonList
        /// </summary>
        /// <param name="dt">内存数据中的一个表</param>
        /// <param name="objType">控件类型,如CheckBoxList,DropDownList,ListBox,RadioButtonList,可从Config.DataBindObjTypeCollection中获取</param>
        /// <param name="obj">数据控件名</param>
        /// <param name="TF">文本域字段名</param>
        /// <param name="VF">值字段名</param>
        public static void DataBind(DataTable dt, string objType, object obj, string TF, string VF)
        {
            switch (objType)
            {
                case "CheckBoxList":
                    ((CheckBoxList)obj).DataTextField = TF;
                    ((CheckBoxList)obj).DataValueField = VF;
                    ((CheckBoxList)obj).DataSource = dt;
                    ((CheckBoxList)obj).DataBind();
                    break;
                case "DropDownList":
                    ((DropDownList)obj).DataTextField = TF;
                    ((DropDownList)obj).DataValueField = VF;
                    ((DropDownList)obj).DataSource = dt;
                    ((DropDownList)obj).DataBind();
                    break;
                case "ListBox":
                    ((ListBox)obj).DataTextField = TF;
                    ((ListBox)obj).DataValueField = VF;
                    ((ListBox)obj).DataSource = dt;
                    ((ListBox)obj).DataBind();
                    break;
                case "RadioButtonList":
                    ((RadioButtonList)obj).DataTextField = TF;
                    ((RadioButtonList)obj).DataValueField = VF;
                    ((RadioButtonList)obj).DataSource = dt;
                    ((RadioButtonList)obj).DataBind();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region 绑定BaseData字典值
        /// <summary>
        /// 绑定BaseData字典值
        /// </summary>
        /// <param name="ddl"></param>
        /// <param name="pid"></param>
        /// <param name="_blank"></param>
        public void BindBasedataToList(DropDownList ddl, int pid, string _blank)
        {
            DataTable dt = sysService.GetDataByKey("T_BaseData", "ParentID", pid);
            ddl.DataSource = dt;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Value";
            ddl.DataBind();
            if (!string.IsNullOrEmpty(_blank))
            {
                ddl.Items.Insert(0, new ListItem(_blank, ""));
            }
        }
        #endregion

        #region 验证权限
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="value"></param>
        /// <returns>0-没有权限 1-有权限</returns>
        public int CheckAuth(string privilegeCode)
        {
            if (UserInfo.UserName.ToLower() == "admin")
            {
                return 1;
            }
            //其他管理员
            DataTable dtPrivilege = sysService.GetUserPrivilege(UserInfo.ID);
            foreach (DataRow dr in dtPrivilege.Rows)
            {
                if (dr["Code"].ToString() == privilegeCode)
                {
                    return 1;
                }
            }
            return 0;
        }
        #endregion
    }
}
