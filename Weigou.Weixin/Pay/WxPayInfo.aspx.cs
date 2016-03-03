using Weigou.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxPayAPI;

namespace Weigou.Weixin.Pay
{
    public partial class WxPayInfo : System.Web.UI.Page
    {
        /// <summary>
        /// 调用js获取收货地址时需要传入的参数
        /// 格式：json串
        /// 包含以下字段：
        ///     appid：公众号id
        ///     scope: 填写“jsapi_address”，获得编辑地址权限
        ///     signType:签名方式，目前仅支持SHA1
        ///     addrSign: 签名，由appid、url、timestamp、noncestr、accesstoken参与签名
        ///     timeStamp：时间戳
        ///     nonceStr: 随机字符串
        /// </summary>
      

        protected void Page_Load(object sender, EventArgs e)
        {
            //Log.Info(this.GetType().ToString(), "page load");
            if (!IsPostBack)
            {
                JsApiPay jsApiPay = new JsApiPay(this);
                try
                {
                    //调用【网页授权获取用户信息】接口获取用户的openid和access_token
                    jsApiPay.GetOpenidAndAccessToken();
                   
                    ViewState["openid"] = jsApiPay.openid;

                    if (ViewState["openid"] != null)
                    {
                        string openid = ViewState["openid"].ToString();
                        string url = "/Pay/JsApiPayPage.aspx?openid=" + openid + "&total_fee=" + 2;
                        Response.Redirect(url);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面加载出错，请重试" + "</span>");
                    //Button1.Visible = false;
                    //Button2.Visible = false;
                    //Label1.Visible = false;
                    //Label2.Visible = false;
                }
            }
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    string total_fee = "1";
        //    if (ViewState["openid"] != null)
        //    {
        //        string openid = ViewState["openid"].ToString();
        //        string url = "http://paysdk.weixin.qq.com/example/JsApiPayPage.aspx?openid=" + openid + "&total_fee=" + total_fee;
        //        Response.Redirect(url);
        //    }
        //    else
        //    {
        //        Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面缺少参数，请返回重试" + "</span>");
        //        Button1.Visible = false;
        //        Button2.Visible = false;
        //        Label1.Visible = false;
        //        Label2.Visible = false;
        //    }
        //}

        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    string total_fee = "2";
        //    if (ViewState["openid"] != null)
        //    {
        //        string openid = ViewState["openid"].ToString();
        //        string url = "/Pay/JsApiPayPage.aspx?openid=" + openid + "&total_fee=" + total_fee;
        //        Response.Redirect(url);
        //    }
        //    else
        //    {
        //        Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面缺少参数，请返回重试" + "</span>");
        //        Button1.Visible = false;
        //        Button2.Visible = false;
        //        Label1.Visible = false;
        //        Label2.Visible = false;
        //    }
        //}
    }
}