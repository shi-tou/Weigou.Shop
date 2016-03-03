<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberSelect.aspx.cs" Inherits="Weigou.Admin.Member.MemberSelect" %>
<%@ Register src="../UControl/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script src="js/memberselect.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
     <!--列表-->
    <table id="ListTable" data-options="toolbar:'#tb',iconCls:'icon-tip'">
    </table>
    <!--工具栏-->
    <div id="tb">
        <table>
            <tr>
                <th>姓名：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtName" class="txt" Width="60px"></asp:TextBox>
                </td>
                <th>手机：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtMobileNo" class="txt" Width="70px"></asp:TextBox>
                </td>
                <th>性别：</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlSex">
                        <asp:ListItem Value="">-全部-</asp:ListItem>
                        <asp:ListItem Value="0">保密</asp:ListItem>
                        <asp:ListItem Value="1">男</asp:ListItem>
                        <asp:ListItem Value="2">女</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><input type="button" id="btnSearch" onclick="GetList()" class="btn" value="查询" /></td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfIDControl" runat="server" />
    <asp:HiddenField ID="hfNameControl" runat="server" />
    </form>
</body>
</html>
