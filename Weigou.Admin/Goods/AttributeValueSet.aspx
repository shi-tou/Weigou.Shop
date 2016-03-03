<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttributeValueSet.aspx.cs" Inherits="Weigou.Admin.Goods.AttributeValueSet" %>
<%@ Register Src="../UControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="js/attributeValue.js"></script>
</head>
<body>
    <form id="form1" runat="server">
         <div>
            <uc1:ToolBar ID="ToolBar1" runat="server" />
        </div>
        <table class="infotable" style="width: 50%">
            <tr>
                <th style="width: 150px;">属性名：</th>
                <td>
                    <asp:Label runat="server" Font-Bold="true" ForeColor="Red" ID="labAttribute"></asp:Label></td>
            </tr>
            <tr>
                <th>属性值：</th>
                <td>
                    <asp:TextBox ID="txtValue" runat="server" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入属性值"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tr" style="width: 100px;">排序号：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtSort" runat="server" Width="300" Text="0" CssClass="easyui-numberbox txt" min="0" data-options="required:true" missingmessage="请输入排序号"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button runat="server" ID="BtnSubmit" Text="添加" CssClass="btn" OnClientClick="return $('#form1').form('validate')" OnClick="BtnSubmit_Click" />
                    <asp:Button runat="server" ID="btnCancel" Text="返回" CssClass="btn" OnClientClick="CloseWin()" />
                </td>
            </tr>
            <asp:HiddenField runat="server" ID="hfValueID" />
        </table>
        <!--列表-->
        <table id="ListTable" data-options="toolbar:'#tb',iconCls:'icon-tip'"></table>
        <asp:HiddenField ID="hfAttributeID" runat="server" />
    </form>
</body>
</html>
