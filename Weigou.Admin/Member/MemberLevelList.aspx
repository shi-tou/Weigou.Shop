<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberLevelList.aspx.cs" Inherits="Weigou.Admin.Member.MemberLevelList" %>

<%@ Register Src="../UControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员级别列表-<%=PageTitle %></title>
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="js/memberlevellist.js" type="text/javascript"></script>
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
        </div>
        <!--弹出窗-->
        <div id="win" class="easyui-window" data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
            <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
        </div>
    </form>
</body>
</html>
