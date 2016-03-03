<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="Weiche.Admin.Order.OrderList" %>
<%@ Register src="../UControl/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单管理-<%=PageTitle %></title>
    <script src="order.js" type="text/javascript"></script>
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
            <uc1:ToolBar ID="ToolBar1" runat="server" />
        </div>
        <table>
            <tr>
                <th>订单号：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtOrderNo" class="txt"></asp:TextBox>
                </td>
                <th>商户名：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtMerchantName" class="txt"></asp:TextBox>
                </td>
                <th>会员名：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtMemberName" class="txt"></asp:TextBox>
                </td>
                <th>会员手机：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtMobileNo" class="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>订单状态：</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlStatus" class="txt" cnchange="GetList()">
                        <asp:ListItem Value="">-全部-</asp:ListItem>
                        <asp:ListItem Value="0">订单提交</asp:ListItem>
                        <asp:ListItem Value="1">已付款</asp:ListItem>
                        <asp:ListItem Value="2">申请退款</asp:ListItem>
                        <asp:ListItem Value="3">订单取消</asp:ListItem>
                        <asp:ListItem Value="4">订单完成</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>订单时间：</th>
                <td colspan="4">
                   <asp:TextBox runat="server" ID="txtMinTime" class="txt" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                   ~<asp:TextBox runat="server" ID="txtMaxTime" class="txt" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                </td>
                <td><input type="button" id="btnSearch" onclick="GetList()" class="btn" value="查询" /></td>
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
