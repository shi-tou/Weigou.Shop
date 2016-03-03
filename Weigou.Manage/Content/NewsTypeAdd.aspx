<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsTypeAdd.aspx.cs" Inherits="Weigou.Manage.Content.NewsTypeAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源添加-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="infotable" >
            <tr>
                <th colspan="2">&nbsp;</th>
            </tr>
            <tr>
                <th>上一级：</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlParentType" Cssclass="form-control"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="width:100px;" class="tr">文章分类名称：</th>
                <td><asp:TextBox ID="txtName" runat="server" CssClass="form-control" title="请输入文章分类名称"></asp:TextBox></td>
            </tr>
        </table>
        <div class="action">
            <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
            <a href="#" class="btn btn-white">返回列表</a>
        </div>
    </div>
    </form>
</body>
</html>
