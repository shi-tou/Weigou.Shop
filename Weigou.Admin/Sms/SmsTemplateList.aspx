<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsTemplateList.aspx.cs" Inherits="Weigou.Admin.Sys.SmsTemplateList" %>
<%@ Register src="../UControl/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>短信模板管理-<%=PageTitle %></title>
    <script src="sms.js" type="text/javascript"></script>
    <script>
        $(function() {
            GetSmsTemplateList();
        });
        function Add() {
            AddSmsTemplate();
        }
        function Edit() {
            EditSmsTemplate();
        }
        function Delete() {
            DeleteSmsTemplate();
        }
    </script>
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
                <th>模板编号：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtCode" class="txt"></asp:TextBox>
                </td>
                <th>内容：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtContent" class="txt"></asp:TextBox>
                </td>
                <td><input type="button" id="btnSearch" onclick="GetSmsTemplateList()" class="btn" value="查询" /></td>
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
