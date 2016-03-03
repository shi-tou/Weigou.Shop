<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JsApiPayPage.aspx.cs" Inherits="WxPayAPI.JsApiPayPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>微信支付样例-JSAPI支付</title>
</head>
<script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
<script type="text/javascript">

    /*
          * 注意：
          * 1. 所有的JS接口只能在公众号绑定的域名下调用，公众号开发者需要先登录微信公众平台进入“公众号设置”的“功能设置”里填写“JS接口安全域名”。
          * 2. 如果发现在 Android 不能分享自定义内容，请到官网下载最新的包覆盖安装，Android 自定义分享接口需升级至 6.0.2.58 版本及以上。
          * 3. 常见问题及完整 JS-SDK 文档地址：http://mp.weixin.qq.com/wiki/7/aaa137b55fb2e0456bf8dd9148dd613f.html
          *
          * 开发中遇到问题详见文档“附录5-常见错误及解决办法”解决，如仍未能解决可通过以下渠道反馈：
          * 邮箱地址：weixin-open@qq.com
          * 邮件主题：【微信JS-SDK反馈】具体问题
          * 邮件内容说明：用简明的语言描述问题所在，并交代清楚遇到该问题的场景，可附上截屏图片，微信团队会尽快处理你的反馈。
          */
    wx.config({
        debug: true,
        appId: 'wx94d65eaa711a0bc0',
        timestamp: '<%=timestamp%>',
        nonceStr: '<%=nonceStr%>',
        signature: '<%=signature%>',
        jsApiList: ['chooseWXPay',]
    });


    //调用微信JS api 支付
    function jsApiCall() {
        var json=<%=wxJsApiParam%>;
        wx.chooseWXPay({
            timestamp: json.timeStamp, // 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
            nonceStr: json.nonceStr, // 支付签名随机串，不长于 32 位
            package: json.package, // 统一支付接口返回的prepay_id参数值，提交格式如：prepay_id=***）
            signType: 'MD5', // 签名方式，默认为'SHA1'，使用新版支付需传入'MD5'
            paySign: json.paySign, // 支付签名
            success: function (res) {
                alert(res);
                // 支付成功后的回调函数
                window.location.href='/Mall/index.aspx';
            },
            cancel: function (res) {
                alert(123);
            }
        });
    }



</script>

<body>
    <form runat="server">
        <br />
        <div align="center">
            <br />
            <br />
            <br />
            <input type="button" onclick="jsApiCall();" style="width: 210px; height: 50px; border-radius: 15px; background-color: #00CD00; border: 0px #FE6714 solid; cursor: pointer; color: white; font-size: 16px;" />
           
        </div>
    </form>
</body>
</html>
