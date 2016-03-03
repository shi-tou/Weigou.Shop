<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceDetail.aspx.cs" Inherits="Weigou.Web.App.ServiceDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="max-width: 640px; margin: 0px auto;">
            <div style="padding: 10px; text-align:center;">
                <asp:Label runat="server" ID="labTitle"></asp:Label>
            </div>
            <div style="background:#eee; font-size:13px;">
                <asp:Repeater runat="server" ID="repNews">
                    <ItemTemplate>
                        <div style="padding:10px; font-weight:bold;"><%#Eval("Title") %></div>
                        <div style="padding:0 10px 20px;"><%#Eval("Description") %></div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
