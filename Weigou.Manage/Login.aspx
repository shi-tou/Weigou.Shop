<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Weigou.Manage.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>微购屋-后台管理系统框架 - 登录</title>
    <link href="/Hui_3.2/css/bootstrap.min.css?v=3.4.0" rel="stylesheet"/>
    <link href="/Hui_3.2/css/font-awesome.min.css?v=4.3.0" rel="stylesheet" />
    <link href="/Hui_3.2/css/animate.min.css" rel="stylesheet"/>
    <link href="/Hui_3.2/css/style.min.css?v=3.2.0" rel="stylesheet"/>
    <link href="/Style/css.css" rel="stylesheet" />
    <script>if (window.top !== window.self) { window.top.location = window.location; }</script>
</head>
<body class="gray-bg">
    <form id="form1" runat="server">
        <div class="middle-box text-center loginscreen  animated fadeInDown">
            <div class="mt100">
                <div>
                    <h2>微购屋-运营管理平台</h2>
                </div>
                <div class="m-t">
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtUserName" Text="admin" class="form-control" placeholder="用户名" required="required"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtPassword" Text="123" TextMode="Password" class="form-control" placeholder="密码" required="required"></asp:TextBox>
                    </div>
                    <asp:Button runat="server" ID="btnLogin" Text="登 录" class="btn btn-primary block full-width m-b" OnClick="btnLogin_Click" />
                    <asp:Label runat="server" ID="lblMsg"></asp:Label>
                </div>
                 <p class="text-muted text-center"><small>power by 微购屋.</small></p>
            </div>
        </div>
    </form>
    <!-- 全局js -->
    <script src="/Hui_3.2/js/jquery-2.1.1.min.js"></script>
    <script src="/Hui_3.2/js/bootstrap.min.js?v=3.4.0"></script>
</body>
</html>
