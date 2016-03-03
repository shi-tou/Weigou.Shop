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

namespace Weigou.Manage
{
    public class ManagePage : BasePage
    {
        public string UserCookieName = "SysUserInfo";
        public int HeaderIndex = 0;
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
            if (!IsLogin())
            {
                return;
            }
            RegisterIncScriptBlock();
            base.OnInit(e);
        }
        /// <summary>
        /// 判断是否登录）
        /// </summary>
        public bool IsLogin()
        {
            if (UserInfo == null)
            {
                RegistScript("alert('未登录或登录已失效！');window.parent.location.href='/Login.aspx';");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 注册INC脚本块
        /// </summary>
        public void RegisterIncScriptBlock()
        {
            this.Header.Controls.AddAt(HeaderIndex++, RegistCSS("/Hui_3.2/css/bootstrap.min.css?v=3.4.0"));
            this.Header.Controls.AddAt(HeaderIndex++, RegistCSS("/Hui_3.2/css/font-awesome.min.css?v=4.3.0"));
            this.Header.Controls.AddAt(HeaderIndex++, RegistCSS("/Hui_3.2/css/animate.min.css"));
            this.Header.Controls.AddAt(HeaderIndex++, RegistCSS("/Hui_3.2/css/style.min.css?v=3.2.0"));
            this.Header.Controls.AddAt(HeaderIndex++, RegistCSS("/Style/css.css"));

            this.Header.Controls.AddAt(HeaderIndex++, RegistJavaScript("/Hui_3.2/js/jquery-2.1.1.min.js"));
            this.Header.Controls.AddAt(HeaderIndex++, RegistJavaScript("/Hui_3.2/js/bootstrap.min.js?v=3.4.0"));
            this.Header.Controls.AddAt(HeaderIndex++, RegistJavaScript("/Hui_3.2/js/content.min.js"));
            this.Header.Controls.AddAt(HeaderIndex++, RegistJavaScript("/Hui_3.2/js/plugins/validate/jquery.validate.min.js"));
            this.Header.Controls.AddAt(HeaderIndex++, RegistJavaScript("/Hui_3.2/js/plugins/validate/messages_zh.min.js"));
            this.Header.Controls.AddAt(HeaderIndex++, RegistJavaScript("/Hui_3.2/js/plugins/layer/layer.min.js"));
            
            this.Header.Controls.AddAt(HeaderIndex++, RegistJavaScript("/JScript/common.js"));
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
        /// <summary>
        /// 操作后的消息提醒
        /// </summary>
        /// <param name="url"></param>
        /// <param name="msg"></param>
        public void ShowMsg(string url, string msg)
        {
            Response.Redirect(string.Format("/Msg.aspx?url={0}&msg={1}", url, msg));
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
            BindType(ddl, dt, 0,ref index);
            if (!string.IsNullOrEmpty(blank))
            {
                ddl.Items.Insert(0, new ListItem(blank, ""));
            }
        }
        /// <summary>
        /// 递归绑定子父类别
        /// </summary>
        public void BindType(DropDownList ddl, DataTable dt, int pid,ref int index)
        {
            index++;
            DataTable dtTemp = Utils.SelectDataTable(dt, "ParentID=" + pid);
            foreach (DataRow dr in dtTemp.Rows)
            {
                ddl.Items.Add(new ListItem(GetBlank(index) + dr["Name"].ToString(), dr["ID"].ToString()));
                BindType(ddl, dt, Convert.ToInt32(dr["ID"]),ref index);
            }
            index--;
        }
        public string GetBlank(int index)
        {
            string s = "";
            if (index > 0)
            {
                s += " └";
                for (int i = 0; i < index; i++)
                {
                    s += "──";
                }
                return s;
            }
            else
            {
                return s;//☰
            }
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
        public bool CheckAuth(string privilegeCode)
        {
            if (UserInfo.UserName.ToLower() == "admin")
            {
                return true;
            }
            //其他管理员
            DataTable dtPrivilege = sysService.GetUserPrivilege(UserInfo.ID);
            foreach (DataRow dr in dtPrivilege.Rows)
            {
                if (dr["Code"].ToString() == privilegeCode)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 格式化显示
        //时间
        public string FormatDateTime(object datetime)
        {
            if (datetime == DBNull.Value)
            {
                return "";
            }
            else
            {
                return Convert.ToDateTime(datetime).ToString("yyyy-MM-dd HH:mm");
            }
        }

        //图片
        public string FormatPic(object v)
        {
            if (v.ToString() == "" || v == null)
            {
                return "暂无置顶图片";
            }
            else
                return "<img src=" + v + " style=\"width:80px; height:60px;\" />";
        }

        public string FormatOrderStatus(object v)
        {
            var res = "";
            switch (Convert.ToInt32(v))
            {
                case 9:
                    res = "<span style=\"color:gray\">已删除</span>";
                    break;
                case 10:
                    res = "<span style=\"color:blue\">待付款</span>";
                    break;
                case 20:
                    res = "<span style=\"color:#6aa84f\">已付款</span>";
                    break;
                case 30:
                    res = "<span style=\"color:#674ea7\">待收货</span>";
                    break;
                case 40:
                    res = "<span style=\"color:#20124d\">已收货</span>";
                    break;
                case 50:
                    res = "<span style=\"color:red\">申请退款</span>";
                    break;
                case 51:
                    res = "<span style=\"color:red\">同意退款</span>";
                    break;
                case 52:
                    res = "<span style=\"color:red\">已退款</span>";
                    break;
                case 60:
                    res = "<span style=\"color:gray\">交易取消</span>";
                    break;
                case 70:
                    res = "<span style=\"color:green\">交易完成</span>";
                    break;
            }
            return res;
        }
        #endregion

    }
}
