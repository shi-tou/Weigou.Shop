﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassAdd.aspx.cs" Inherits="Weigou.Admin.Sys.ClassAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>栏目添加-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="infotable" >
            <tr>
                <th>上一级：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtParentCode" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="tr">栏目编码：</th>
                <td><asp:TextBox runat="server" ID="txtCode"  CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入编码"></asp:TextBox></td>
            </tr>
            <tr>
                <th class="tr">类型：</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlClassPropertyID" CssClass="txt">
                        <asp:ListItem Value="1">头部导航</asp:ListItem>
                        <asp:ListItem Value="2">底部导航</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="width:100px;" class="tr">栏目名称：</th>
                <td><asp:TextBox ID="txtName" runat="server" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入资源名称"></asp:TextBox></td>
            </tr>
            
            <tr>
                <th class="tr">链接：</th>
                <td><asp:TextBox ID="txtUrl" runat="server" CssClass="txt"></asp:TextBox></td>
            </tr>
            <tr>
                <th class="tr">排序：</th>
                <td><asp:TextBox ID="txtSort" runat="server" CssClass="easyui-numberbox txt" min="0"></asp:TextBox></td>
            </tr>
            <tr>
                <th class="tr">禁用：</th>
                <td><asp:CheckBox ID="cbDisabled" runat="server"/></td>
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
