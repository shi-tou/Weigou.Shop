<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MallRemindSendList.aspx.cs" Inherits="Weigou.Manage.Order.MallRemindSendList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商城订单管理-<%=PageTitle %></title>
    <script src="js/mallremindsend.js" type="text/javascript"></script>
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <!--列表-->
    <table id="ListTable" data-options="toolbar:'#tb',iconCls:'icon-tip'">
    </table>
    <!--工具栏-->
    <div id="tb">
        <div>
           
        </div>
        <table>
            <tr>
                <th>订单号：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtOrderNo" class="form-control"></asp:TextBox>
                </td>
                <th>订单时间：</th>
                <td colspan="2">
                   <asp:TextBox runat="server" ID="txtMinTime" class="form-control" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                   ~<asp:TextBox runat="server" ID="txtMaxTime" class="form-control" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                </td>
                <th></th>
                <td></td>
                <td colspan="3">&nbsp;<asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="查 询" OnClick="btnSearch_Click" /></td>
            </tr>
        </table>
    </div>
    <!--弹出窗-->
    <div id="win" class="easyui-window" style=" " data-options="iconCls:'icon-save',top:'20px',closed:true,minimizable:false,maximizable:false,collapsible:false">
        <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
    </div>
    
    </form>
</body>
</html>
