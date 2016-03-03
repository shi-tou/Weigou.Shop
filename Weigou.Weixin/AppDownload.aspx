<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppDownload.aspx.cs" Inherits="Weigou.Weixin.AppDownload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/JScript/jquery-1.8.0.min.js"></script>
    <script type="text/javascript">
        /*
        智能机浏览器版本信息:
        */
        var browser = {
            versions: function () {
                var u = navigator.userAgent, app = navigator.appVersion;
                return {//移动终端浏览器版本信息
                    trident: u.indexOf('Trident') > -1, //IE内核
                    presto: u.indexOf('Presto') > -1, //opera内核
                    webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核
                    gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核
                    mobile: !!u.match(/AppleWebKit.*Mobile.*/) || !!u.match(/AppleWebKit/), //是否为移动终端
                    ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端
                    android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或者uc浏览器
                    iPhone: u.indexOf('iPhone') > -1 || u.indexOf('Mac') > -1, //是否为iPhone或者QQHD浏览器
                    iPad: u.indexOf('iPad') > -1, //是否iPad
                    webApp: u.indexOf('Safari') == -1 //是否web应该程序，没有头部与底部
                };
            }(),
            language: (navigator.browserLanguage || navigator.language).toLowerCase()
        }
        $(function () {
            Show();
        });

        function Show() {
            if (browser.versions.ios) {
                $('.ios').show();
            }
            else {
                $('.android').show();
            }
            if (is_weixn()) {
                $('.app_weixin_url').show();
            }
            else {
                $('.app_url').show();
            }
        }

        //微信
        function is_weixn() {
            var ua = navigator.userAgent.toLowerCase();
            if (ua.match(/MicroMessenger/i) == "micromessenger") {
                return true;
            } else {
                return false;
            }
        }
        function showover() {
            $('#mcover').show()
        }
        function hideover() {
            $('#mcover').hide()
        }
    </script>
    <style type="text/css">
        .android,.ios {
            width: 100%;
            margin-top: 100px;
            text-align: center;
            display:none;
        }
        .android h3,.ios h3 {
            font-size: 22px;
            font-weight: lighter;
        }
        .android .box,.ios .box  {
            width: 60%;
            height: auto;
            margin: 50px auto 30px auto;
        }
        .android .box img,.android .box img {
            width: 100%;
        }
        .android p,.ios p  {
            font-size: 18px;
            line-height: 30px;
            margin-top: 10px;
        }
        .app_url,.app_weixin_url{ display:none;}
        #mcover {
            background: none repeat scroll 0 0 rgba(0, 0, 0, 0.7);
            display: none;
            height: 100%;
            left: 0;
            position: fixed;
            top: 0;
            width: 100%;
            z-index: 20000;
        }
        #mcover img {
            height: 180px !important;
            position: fixed;
            right: 18px;
            top: 5px;
            width: 260px !important;
            z-index: 20001;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="mcover" onclick='hideover()'><img src="style/img/guide.png" /></div>
        <!--安卓下载-->
        <div class="android" >
            <h3>客户端下载</h3>
            <div class="box">
                <a runat="server" class="app_url"  >
                    <img src="style/img/android.jpg" alt="android" />
                </a>
                <a class="app_weixin_url" onclick="showover()">
                    <img src="style/img/android.jpg" alt="android" />
                </a>
            </div>
            <p>支持系统版本：安卓2.1及以上版本。</p>
        </div>

        <!--苹果下载-->
        <div class="ios" >
            <h3>客户端下载</h3>
            <div class="box">
                <a  class="app_url" >
                    <img src="style/img/iphone.jpg" alt="android" />
                </a>
                <a class="app_weixin_url" onclick="showover()">
                    <img src="style/img/iphone.jpg" alt="android" />
                </a>
            </div>
            <p>支持系统版本：iOS 7.0及以上版本。</p>
        </div>
    </form>
</body>
</html>
