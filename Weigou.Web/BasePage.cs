using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Threading;
using System.Globalization;
using System.Net;
using System.IO;
using Weigou.Common;
using Weigou.Service;
using Weigou.Model;
using System.Text;

namespace Weigou.Web
{
    public class BasePage : System.Web.UI.Page
    {

        /// <summary>
        /// 返回Json字符串
        /// </summary>
        public string strJson = "";

        #region IOC注入
        public static ISysService sysService;
        public ISysService SysService
        {
            set { sysService = value; }
            get { return sysService; }
        }
        //注入
        public static IGoodsService goodsService;
        public IGoodsService GoodsService
        {
            set { goodsService = value; }
        }       
        //注入
        public static IMemberService memberService;
        public IMemberService MemberService
        {
            set { memberService = value; }
        }
        //注入
        public static IReportService reportService;
        public IReportService ReportService
        {
            set { reportService = value; }
        }
        //注入
        public static ISmsService smsService;
        public ISmsService SmsService
        {
            set { smsService = value; }
        }
       
        //注入
        public static IContentService contentService;
        public IContentService ContentService
        {
            set { contentService = value; }
        }
      
        //注入
        public static IOrderService orderService;
        public IOrderService OrderService
        {
            set { orderService = value; }
        }
        #endregion

        /// <summary>
        /// 构建JS引用
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public HtmlGenericControl RegistJavaScript(string src)
        {
            HtmlGenericControl generic = new HtmlGenericControl("script");
            generic.Attributes["type"] = "text/javascript";
            generic.Attributes["src"] = src;
            return generic;
        }

        /// <summary>
        /// 构建CSS样式引用
        /// </summary>
        /// <param name="href">链接地址</param>
        /// <returns></returns>
        public static HtmlLink RegistCSS(string href)
        {
            HtmlLink generic = new HtmlLink();
            generic.Href = href;
            generic.Attributes.Add("rel", "stylesheet");
            generic.Attributes.Add("type", "text/css");
            generic.Attributes["href"] = href;
            return generic;
        }

        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="message"></param>
        public void Alert(string message)
        {
            //System.Web.UI.ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), " ", "alert('" + message + "');", true);
            this.ClientScript.RegisterStartupScript(Page.GetType(), null, "alert('" + message + "');", true);
        }
        /// <summary>
        /// 提示消息并跳转
        /// </summary>
        /// <param name="strScript"></param>
        public void AlertAndJump(string message, string url)
        {
            this.ClientScript.RegisterStartupScript(Page.GetType(), null, "alert('" + message + "');window.location.href='"+ url +"';", true);
        }
        /// <summary>
        /// 输出JavaScript
        /// </summary>
        /// <param name="strScript"></param>
        public void InvokeScript(string strScript)
        {
            this.ClientScript.RegisterStartupScript(Page.GetType(), null, strScript, true);
        }
        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="message"></param>
        public void AlertInfoAndJump(string message,string url)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript((System.Web.UI.Page)HttpContext.Current.CurrentHandler, typeof(System.Web.UI.Page), " ", "alert('" + message + "');window.location.href='" + url + "'", true);
        }
       
        /// <summary>
        /// 获得request参数的string类型值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>参数的string类型值</returns>
        public string GetRequest(string strName, string defaultValue)
        {
            string vaule = Convert.ToString(HttpContext.Current.Request[strName]);
            if (vaule != null && vaule != "")
            {
                return vaule;
            }
            else
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 获得request参数的int类型值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>参数的int类型值</returns>
        public int GetRequest(string strName, int defaultValue)
        {
            string vaule = HttpContext.Current.Request[strName];
            if (vaule != null && vaule != "")
                return Convert.ToInt16(HttpContext.Current.Request[strName]);
            else
                return defaultValue;
        }
        /// <summary>
        /// 获得request参数的bool类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public bool GetRequest(string strName, bool defaultValue)
        {
            string vaule = HttpContext.Current.Request[strName];
            if (vaule != null && vaule != "")
                return Convert.ToBoolean(HttpContext.Current.Request[strName]);
            else
                return defaultValue;

        }
        /// <summary>
        /// 获得request参数的decimal值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的decimal类型值</returns>
        public decimal GetRequest(string strName,decimal defaultValue)
        {
            string vaule = HttpContext.Current.Request[strName];
            if (vaule != null && vaule != "")
                return Convert.ToDecimal(HttpContext.Current.Request[strName]);
            else
                return defaultValue;
        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fuload"></param>
        /// <param name="oldPath"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string Upload(FileUpload fuload, string oldPath, string type)
        {
            if (!fuload.HasFile)
            {
                return oldPath;
            }
            //string picturePath = Utils.GetConfig("PictureSavePath");
            string strTime = DateTime.Now.ToString("yyyyMM");
            string filePath = Server.MapPath("\\upload\\" + type + "\\" + strTime + "\\");//路径
            //string filePath = picturePath + "/" + type + "/" + strTime + "/";//路径
            //
            string ext = Path.GetExtension(fuload.FileName).ToLower();//扩展名
            string guid = Guid.NewGuid().ToString();//文件名
            string fileName = filePath + guid + ext;//要保存的文件全路径     
            try
            {
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                //上传文件
                fuload.PostedFile.SaveAs(fileName);
                if (string.IsNullOrEmpty(oldPath) == false && File.Exists(Server.MapPath(oldPath)))
                {
                    File.Delete(Server.MapPath(oldPath));
                }
            }
            catch
            {
                return "";
            }
            return "/upload/" + type + "/" + strTime + "/" + guid + ext;
        }

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

        #region 根据上级获取省市县级数据
        public string GetAreaHTML(string strParentID, string strTypeID,string strDefault,string strSelID)
        {
            StringBuilder strresult = new StringBuilder();
            if (strDefault != "")
            {
                strresult.Append("<option value=''>" + strDefault + "</option>");
            }
            DataTable dt = GetAreatData(strParentID, strTypeID);
            if (dt.Rows.Count > 0 && dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (strSelID   == dt.Rows[i]["ID"].ToString())
                    {
                        strresult.AppendFormat("<option selected='selected' value='{0}' ", dt.Rows[i]["ID"]);
                        strresult.AppendFormat(">{0}</option>", dt.Rows[i]["Name"]);
                        continue;
                    }
                    strresult.AppendFormat("<option value='{0}'", dt.Rows[i]["ID"]);
                    strresult.AppendFormat(">{0}</option>", dt.Rows[i]["Name"]);
                }
            }
            return strresult.ToString();
        }

        public DataTable GetAreatData(string strParentID, string strTypeID)
        {
            DataTable dt=null;
            string strName="";
            switch (strTypeID)
            {
                case "1": //获取省级
                    dt= memberService.GetData("T_Province");
                    strName="ProvinceName";
                    break;
                case "2": //获取市级
                    dt = memberService.GetDataByKey("T_City", "ProvinceID", strParentID);
                    strName="CityName";
                    break;
                case"3":
                    dt = memberService.GetDataByKey("T_District", "CityID", strParentID);
                    strName="DistrictName";
                    break;
            }
            dt.Columns.Add("Name");
            foreach (DataRow item in dt.Rows)
            {
                item["Name"] = item[strName];
            }
            return dt;
        }
        #endregion
    }

}
