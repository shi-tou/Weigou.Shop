<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Weigou.Admin.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户登录-<%=Weigou.Common.Utils.GetConfig("PageTitle") %></title>
    <link href="/Style/login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="box">
        <div class="box_title">微车联盟平台运营管理系统</div>
        <div class="box_main">
            <p><asp:TextBox ID="txtUserName" runat="server" Text="admin" class="txt"></asp:TextBox></p>
            <p><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" class="txt"></asp:TextBox></p>
            <p><asp:Button ID="btnLogin" runat="server" Text="登录" class="btn" onclick="btnLogin_Click" /></p>
            <p><asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></p>
            <p style="color:#bbb;">Copyright©2009-2015 移商网</p>
        </div> 
    </div>
    </form>
</body>
</html>
