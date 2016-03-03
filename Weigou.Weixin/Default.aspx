<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Weigou.Weixin._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>ID</td>
                <td>ParentID</td>
                <td>Name</td>
                <td>Type</td>
                <td>Key</td>
                <td>Url</td>
            </tr>
            <asp:Repeater runat="server" ID="repMenu">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("ID") %></td>
                        <td><%#Eval("ParentID") %></td>
                        <td><%#Eval("Name") %></td>
                        <td><%#Eval("Type") %></td>
                        <td><%#Eval("Key") %></td>
                        <td><%#Eval("Url") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
        1、菜单设置最多为两级，一级菜单和二级菜单
        2、一级菜单可添加最多3个，每个一级菜单下可添加最多5个子菜单
        3、添加子菜单后，一级菜单的内容将被清除
        <asp:Button runat="server" ID="btnCreateMenu" Text="同步菜单至微信" OnClick="btnCreateMenu_Click" />
    </form>
</body>
</html>
