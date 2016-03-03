<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyOrder.aspx.cs" Inherits="Weigou.Web.Member.MyOrder" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="/UControl/MemHeader.ascx" TagName="MemHeader" TagPrefix="uc1" %>
<%@ Register Src="/UControl/MemMenu.ascx" TagName="MemMenu" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="s1"></asp:ScriptManager>
        <uc1:MemHeader ID="MemHeader1" runat="server" />
        <div class="container mem_container">
            <div class="row">
                <div class="col-md-2">
                    <uc2:MemMenu ID="MemMenu1" runat="server" />
                </div>
                <div class="col-md-10 mem_main">
                    <div class="panel panel-default">
                        <div class="panel-heading">我的订单</div>
                        <div class="panel-body">
                            <table class="table table-striped">
                                <tr>
                                    <th></th>
                                    <th style="width: 30%">商品</th>
                                    <th>单价(元)</th>
                                    <th>数量</th>
                                    <th>实付款</th>
                                    <th>交易状态</th>
                                    <th>交易操作</th>
                                </tr>
                            </table>
                            <!--列表-->
                            <table class="table table-striped">

                                <asp:Repeater runat="server" ID="repOrder" OnItemDataBound="repOrder_ItemDataBound">
                                    <ItemTemplate>
                                        <tbody>
                                            <tr>
                                                <td style="width: 80%"><%#Convert.ToDateTime(Eval("OrderTime")).ToString("yyyy-MM-dd") %> 订单号:<%#Eval("OrderNo") %></td>
                                                <td>商家：<%#Eval("MerchantName") %></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style="width: 100%">
                                                        <asp:Repeater runat="server" ID="repOrderDetail">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="width: 45%"><%#Eval("GoodsName") %></td>
                                                                    <td style="width: 20%"><%#Eval("Price") %></td>
                                                                    <td style="width: 10%"><%#Eval("Count") %></td>
                                                                    <%#Container.ItemIndex==0 ? "<td rowspan='" + Count +"'>" + TotalPrice+"</td>":"" %>
                                                                    <%#Container.ItemIndex==0 ? "<td style='width:15%' rowspan='" + Count +"'>" + OrderStatus+"</td>":"" %>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <!--分页-->
                            <div style="width: 500px;">
                                <webdiyer:AspNetPager ID="AspNetPager" runat="server" PageSize="6" OnPageChanging="AspNetPager_PageChanging"
                                    ShowFirstLast="false" HorizontalAlign="Center" ShowPageIndexBox="Never" CssClass="pages" CurrentPageButtonClass="cpb" AlwaysShow="True">
                                </webdiyer:AspNetPager>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
