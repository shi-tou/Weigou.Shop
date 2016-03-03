<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MallOrderSaleList.aspx.cs" Inherits="Weigou.Admin.Order.MallOrderSaleList" %>

<%@ Register Src="../UControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>售后申请-<%=PageTitle %></title>
    <script src="js/mallordersale.js" type="text/javascript"></script>
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
                        <asp:TextBox runat="server" ID="txtOrderNo" class="txt" Width="130"></asp:TextBox>
                    </td>
                    <th>收货人手机号：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtConsigneeMobileNo" class="txt" Width="130"></asp:TextBox>
                    </td>
                    <th><%--订单类型：--%></th>
                    <td>
                       <%-- <asp:DropDownList runat="server" ID="ddlOrderType" class="txt" Width="135">
                            <asp:ListItem Value="">-全部-</asp:ListItem>
                            <asp:ListItem Value="1">公益馆售后</asp:ListItem>
                            <asp:ListItem Value="2">商家售后</asp:ListItem>
                        </asp:DropDownList>--%>
                    </td>
                </tr>
                <tr>
                    <th>处理状态：</th>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="txt">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="0">申请售后</asp:ListItem>
                            <asp:ListItem Value="1">同意处理</asp:ListItem>
                            <asp:ListItem Value="2">拒绝处理</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th><%--店铺名称：--%></th>
                    <td>
                       <%-- <asp:TextBox runat="server" ID="txtMerchantName" class="txt" Width="130"></asp:TextBox>--%>
                    </td>
                    <th>申请时间：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtMinTime" class="txt" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                        ~<asp:TextBox runat="server" ID="txtMaxTime" class="txt" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                    </td>
                    <th></th>
                    <td>
                        <input type="button" id="btnSearch" onclick="GetList()" class="btn" value="查询" />
                        <input type="button" id="Button1" onclick="ResetSearchForm();" class="btn" value="重置" />
                    </td>
                </tr>
            </table>
        </div>
        <!--弹出窗-->
        <div id="win" class="easyui-window" style="" data-options="iconCls:'icon-save',top:'20px',closed:true,minimizable:false,maximizable:false,collapsible:false">
            <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
        </div>
        <asp:HiddenField runat="server" ID="hfEdit" Value="0" />
    </form>
</body>
</html>
