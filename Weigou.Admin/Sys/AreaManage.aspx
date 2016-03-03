<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaManage.aspx.cs" Inherits="Weigou.Admin.Sys.AreaManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register src="../UControl/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>地区数据管理-<%=PageTitle %></title>
    <script src="js/area.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="AreaTab" class="easyui-tabs">
        <div title="省管理" data-options="iconCls:'icon-menu'" style="padding: 10px">
            <!--列表-->
            <table id="ProvinceTable" data-options="toolbar:'#tb_p',iconCls:'icon-tip'">
            </table>
        </div>
        <div title="市管理" data-options="iconCls:'icon-menu'" style="padding: 10px;">
            <!--列表-->
            <table id="CityTable" data-options="toolbar:'#tb_c',iconCls:'icon-tip'">
            </table>
        </div>
        <div title="区管理" data-options="iconCls:'icon-menu'" style="padding: 10px">
            <!--列表-->
            <table id="DistrictTable" data-options="toolbar:'#tb_d',iconCls:'icon-tip'">
            </table>
        </div>
    </div>
    <!--工具栏-->
    <div id="tb_p">
        <div>            
            <uc1:ToolBar ID="ToolBar_P" runat="server" />            
        </div>
    </div>
    <!--工具栏-->
    <div id="tb_c">
        <div>            
            <uc1:ToolBar ID="ToolBar_C" runat="server" />            
        </div>
    </div>
    <!--工具栏-->
    <div id="tb_d">
        <div>            
            <uc1:ToolBar ID="ToolBar_D" runat="server" />            
        </div>
    </div>
    <input id="hdTabID" value="1" type="hidden" />
    <!--弹出窗-->
    <div id="win" class="easyui-window" style=" " data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
        <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
    </div>
    </form>
</body>
</html>
