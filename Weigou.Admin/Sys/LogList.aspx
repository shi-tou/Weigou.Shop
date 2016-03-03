<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogList.aspx.cs" Inherits="Weigou.Admin.Sys.LogList" %>

<%@ Register src="../UControl/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>操作日志-<%=PageTitle %></title>
    <script src="js/log.js" type="text/javascript"></script>
    <script src="../JScript/My97DatePicker/WdatePicker.js"></script>
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
                <th>模块：</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlModule">
                    </asp:DropDownList>
                </td>
                 <th>类型：</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlOperation">
                    </asp:DropDownList>
                </td>
                <th>内容：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtContent" class="txt"></asp:TextBox>
                </td>
                <th>时间：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtMinTime" class="txt" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                    ~<asp:TextBox runat="server" ID="txtMaxTime" class="txt" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                </td>
                <td><input type="button" id="btnSearch" onclick="GetList()" class="btn" value="查询" /></td>
            </tr>
        </table>
    </div>
    <!--弹出窗-->
    <div id="win" class="easyui-window" style=" " data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
        <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
    </div>
    </form>
</body>
</html>
