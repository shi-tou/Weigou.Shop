<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsTypeAdd.aspx.cs" Inherits="Weigou.Admin.Content.NewsTypeAdd" %>

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
                    <asp:DropDownList runat="server" ID="ddlParentType" CssClass="txt"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="width:100px;" class="tr">文章分类名称：</th>
                <td><asp:TextBox ID="txtName" runat="server" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入文章分类名称"></asp:TextBox></td>
            </tr>
        </table>
        <div class="action">
            <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn" OnClientClick="return $('#form1').form('validate')" OnClick="BtnSubmit_Click" />
            <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
        </div>
    </div>
    </form>
</body>
</html>
