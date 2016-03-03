<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsAttributeList.aspx.cs" Inherits="Weigou.Admin.Goods.GoodsAttributeList" %>

<%@ Register src="../UControl/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>扩展属性列表-<%=PageTitle %></title>
    <script src="js/attribute.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
     <!--列表-->
    <table id="ListTable" data-options="toolbar:'#tb',iconCls:'icon-tip'">
    </table>
    <!--工具栏-->
    <div id="tb">
        <div>           
            <uc1:ToolBar ID="ToolBar1" runat="server" />
        </div>
        <table>
            <tr>
                <th>名称：</th>
                <td><asp:TextBox runat="server" ID="txtName" class="txt"></asp:TextBox></td>
                <th>类别：</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlType" class="txt">
                        <asp:ListItem Value="">-全部-</asp:ListItem>
                        <asp:ListItem Value="1">选项</asp:ListItem>
                        <asp:ListItem Value="2">文本框</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td><input type="button" id="btnSearch" onclick="GetList()" class="btn" value="查询" /></td>
            </tr>
        </table>
    </div>
    <!--弹出窗-->
    <div id="win" class="easyui-window" data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
        <iframe id="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
    </div>
    </form>
</body>
</html>
