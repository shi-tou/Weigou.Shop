<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintOrder.aspx.cs" Inherits="Weigou.Manage.Order.PrintOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="contentstart">
            <div>
            </div>
            <table class="form form-horizontal table-bordered">
                <tr>
                    <td>
                        <img src="/Style/images/printlogo.png" width="200" height="60" /></td>
                    <td colspan="6" style="text-align: center;"><span style="font-size: 38px;">商品发货单</span></td>
                </tr>
                <asp:Repeater ID="repOrder" runat="server" OnItemDataBound="repOrder_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="width: 50%;" colspan="2">
                                <span >订单编号：<%#Eval("OrderNo") %></span>
                            </td>
                            <td style="width: 50%;" colspan="5">下单时间：<%#Eval("OrderTime") %>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%;">序号</td>
                            <td style="width: 40%;">商品名称</td>
                            <td style="width: 10%;">商品编码</td>
                            <td style="width: 10%;">单价</td>
                            <td style="width: 10%;"><%--关税--%></td>
                            <td style="width: 10%;">数量</td>
                            <td style="width: 10%;">金额(元)</td>
                        </tr>
                        <asp:Repeater ID="repOrderDetail" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Container.ItemIndex+1 %></td>
                                    <td><%#Eval("GoodsName") %> <%#Eval("SaleProp") %></td>
                                    <td><%--<%#Eval("Code") %>--%></td>
                                    <td>￥<%#Eval("SalePrice") %></td>
                                    <td><%--￥<%#Eval("Tariff") %>--%></td>
                                    <td><%#Eval("Count") %></td>
                                    <td>￥<%#Convert.ToDecimal(Eval("SalePrice"))*Convert.ToDecimal(Eval("Count")) %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr>
                            <td>合计</td>
                            <td colspan="4"></td>
                            <td><%#Eval("TotalCount") %></td>
                            <td>￥<%#Convert.ToDecimal(Eval("TotalMoney")) %></td>
                        </tr>
                        <tr>
                            <td colspan="2">物流费用：￥<%--<%#Eval("LogisticsPrice") %>--%></td>
                            <td colspan="3"><%--购物积分抵扣：￥<%#Eval("GiftScore") %>--%></td>
                            <td>订单总额：</td>
                            <td>￥<%#Convert.ToDecimal(Eval("TotalMoney"))%></td>
                        </tr>
                        <tr>
                            <td colspan="5"></td>
                            <td>实付金额：</td>
                            <td>￥<%#Eval("TotalMoney") %></td>
                        </tr>
                        <tr>
                            <td>收货人：</td>
                            <td colspan="6"><%#Eval("ConsigneeName") %></td>
                        </tr>
                        <tr>
                            <td>收货地址：</td>
                            <td colspan="6"><%#Eval("DeliverAddress") %></td>
                        </tr>
                        <tr>
                            <td>买家备注：</td>
                            <td colspan="6"><%#Eval("OrderRemark") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td></td>
                    <td>
            <asp:Button runat="server" ID="btnCancel" Text="打印订单" CssClass="btn btn-success" OnClientClick="PrintThisPage()" />
             <a href="MallOrderList.aspx" class="btn btn-white">返回列表</a>

                    </td>
                </tr>
            </table>
        </div>  
    </form>
</body>
</html>
