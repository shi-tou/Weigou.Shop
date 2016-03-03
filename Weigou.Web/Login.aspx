<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Weigou.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .form-signin {margin: 0 auto;max-width: 330px;padding: 15px;}
    </style>
</head>
<body>
    <div class="container">
        <form id="form1" runat="server" class="form-signin">
            <h2 class="form-signin-heading">会员登录</h2>
            <label for="inputEmail" class="sr-only">手机号/邮箱：</label>
            <asp:TextBox runat="server" ID="txtMobileOrEmail" type="email" class="form-control" placeholder="手机号/邮箱" required autofocus></asp:TextBox>
            <label for="inputPassword" class="sr-only">登录密码：</label>
            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password"  class="form-control" placeholder="登录密码" required autofocus></asp:TextBox>
            <div class="checkbox">
                <label>
                    <input type="checkbox" value="remember-me">记住我
                </label>
            </div>
            <asp:Button runat="server" ID="btnLogin" Text="登录" OnClick="btnLogin_Click" class="btn btn-lg btn-primary btn-block" />
            <asp:Label runat="server" ID="labMsg" ForeColor="Red"></asp:Label>
        </form>
    </div>

</body>
</html>
