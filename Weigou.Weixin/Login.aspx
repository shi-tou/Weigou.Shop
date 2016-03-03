<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Weigou.Weixin.Login" %>
<%@ Register src="uc/Footer.ascx" tagname="Footer" tagprefix="uc1" %>
<%@ Register src="uc/Header.ascx" tagname="Header" tagprefix="uc2" %>
<!doctype html>
<html class="no-js" >
<head runat="server">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta name="renderer" content="webkit">
    <meta http-equiv="Cache-Control" content="no-siteapp"/>
    <script src="js/login.js" type="text/javascript"></script>
    <title>会员登录</title>
</head>
<body>
    
    <!--header-->
    <uc2:Header ID="Header1" runat="server" HeaderTitle="会员登录" IsShow="false" />
    <!--登录表单--> 
    <form id="form1" runat="server" class="am-form">
    <div class="am-g">
        <div class="am-u-lg-6 am-u-md-8 am-u-sm-centered">
            <br />
            <label for="txtMobileNo">手机号码:</label>
            <input type="text" id="txtMobileNo" />
            <label for="txtPassword">登录密码:</label>
            <input type="password" id="txtPassword" />
            <br />
            <div class="am-cf">
                <input type="button" value="登录" class="am-btn am-btn-primary am-btn-block" onclick="Login()" >
                <a href="Register.aspx?backurl=<%=BackUrl %>" class="am-btn am-btn-warning am-btn-block">还未注册?去注册 ^_^?</a>
            </div>
        </div>
    </div>
    </form>
    <uc1:Footer ID="Footer1" runat="server" />
    
</body>
</html>
