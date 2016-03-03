<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BannerList.aspx.cs" Inherits="Weigou.Admin.Content.BannerList" %>

<%@ Register src="../UControl/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Banner图管理-<%=PageTitle %></title>
    <script src="js/banner.js" type="text/javascript"></script>
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
                <th>广告Banner分类：</th>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" class="txt" style="width:auto;" onchange="GetList()"></asp:DropDownList>
                    </td>
                <td><input type="button" id="btnSearch" onclick="GetList()" class="btn" value="查询" /></td>
            </tr>
        </table>
    </div>
    <!--弹出窗-->
    <div id="win" class="easyui-window" style=" " data-options="iconCls:'icon-save',top:'10px',closed:true,minimizable:false,maximizable:false,collapsible:false">
        <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
    </div>
    </form>
</body>
</html>
