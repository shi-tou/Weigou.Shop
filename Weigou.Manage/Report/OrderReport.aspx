<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderReport.aspx.cs" Inherits="Weigou.Manage.Report.OrderReport" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="report.js" type="text/javascript"></script>
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            GetOrderReport();
        });
        function Export() {
            ExportOrderReport();
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
                    <th>订单号：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtOrderNo" class="txt" Width="130"></asp:TextBox>
                    </td>
                    <th>会员手机号：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtMemberMobileNo" class="txt" Width="130"></asp:TextBox>
                    </td>
                    <th></th>
                    <td>
                       
                    </td>
                </tr>
                <tr>
                    <th>订单状态：</th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlOrderStatus" class="txt" Width="135">
                            <asp:ListItem Value="">-全部-</asp:ListItem>
                            <asp:ListItem Value="10">未付款</asp:ListItem>
                            <asp:ListItem Value="20">已付款</asp:ListItem>
                            <asp:ListItem Value="30">已发货</asp:ListItem>
                            <asp:ListItem Value="40">已收货</asp:ListItem>
                            <asp:ListItem Value="50">买家申请退款</asp:ListItem>
                            <asp:ListItem Value="51">卖家同意退款</asp:ListItem>
                            <asp:ListItem Value="52">已退款</asp:ListItem>
                            <asp:ListItem Value="60">买家取消订单</asp:ListItem>
                            <asp:ListItem Value="9">买家删除订单</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>下单时间：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtMinTime" class="txt" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                        ~<asp:TextBox runat="server" ID="txtMaxTime" class="txt" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                    </td>
                    <td>
                        <input type="button" id="btnSearch" onclick="GetOrderReport()" class="btn" value="查询" />
                        <input type="button" id="Button1" onclick="ResetSearchForm();" class="btn" value="重置" />
                    </td>
                </tr>
            </table>
        </div>
        <!--弹出窗-->
        <div id="win" class="easyui-window" data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
            <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
        </div>
    </form>
</body>
</html>
