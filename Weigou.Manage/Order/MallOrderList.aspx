<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MallOrderList.aspx.cs" Inherits="Weigou.Manage.Order.MallOrderList" %>
 
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %> 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商城订单管理-<%=PageTitle %></title>
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div class="ibox">
            <div class="ibox-title">
                <h5>商品列表</h5>
                <div class="ibox-tools">                  
                    <a class="btn btn-success btn-sm collapse-link">
                        <i class="fa fa-chevron-up"></i>&nbsp;收/开
                    </a>
                </div>
            </div>
           <div class="ibox-content">
           <table class="form-group">
            <tr>
                <th>订单号：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtOrderNo" class="form-control"></asp:TextBox>
                </td>
                <th>会员手机：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtMobileNo" class="form-control"></asp:TextBox>
                </td>
                <th>订单时间：</th>
                <td>
                   <asp:TextBox runat="server" ID="txtMinTime" class="form-control" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                </td>
                <td> ~ </td>
                <td>
                    <asp:TextBox runat="server" ID="txtMaxTime" class="form-control" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                </td>
                <th>订单状态：</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlOrderStatus" class="form-control">
                        <asp:ListItem Value="">-全部-</asp:ListItem>
                        <asp:ListItem Value="9">已删除</asp:ListItem>
                        <asp:ListItem Value="10">待付款</asp:ListItem>
                        <asp:ListItem Value="20">已付款</asp:ListItem>
                        <asp:ListItem Value="30">待收货</asp:ListItem>
                        <asp:ListItem Value="40">已收货</asp:ListItem>
                        <asp:ListItem Value="50">申请退款</asp:ListItem>
                        <asp:ListItem Value="60">交易取消</asp:ListItem>
                        <asp:ListItem Value="70">交易完成</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;&nbsp;<asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="查 询" OnClick="btnSearch_Click" /></td>
            </tr>
        </table>
    </div>    
    </div> 
     <!--列表-->
        <table class="table table-hover table-bordered ">
            <thead>
                <tr>
                    <th style="width: 50px">#</th>
                    <th class="hidden">ID</th>
                    <th>订单号</th>
                    <th>收货地址</th>
                    <th>收货手机号</th>
                    <th>收货人</th>
                    <th>会员手机号</th>
                    <th>商品总数</th>
                    <th>订单总价</th>
                    <th>订单状态</th>
                    <th>下单时间</th>
                    <th>支付时间</th>
                    <th>订单备注</th>
                    <th style="width:280px;">操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repOrder" OnItemCommand="repOrder_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Eval("OrderNo") %></td>
                            <td><%#Eval("DeliverAddress") %></td>
                            <td><%#Eval("ConsigneeMobileNo") %></td>
                            <td><%#Eval("ConsigneeName") %></td>
                            <td><%#Eval("MobileNo") %></td>
                            <td><%#Eval("TotalCount") %></td>
                            <td><%#Eval("TotalMoney") %></td>
                            <td><%#FormatOrderStatus(Eval("OrderStatus")) %></td>
                            <td><%#FormatDateTime(Eval("OrderTime")) %></td>
                            <td><%#FormatDateTime(Eval("PayTime")) %></td> 
                            <td><%#Eval("OrderRemark") %></td>
                            <td>
                                <%if(CheckAuth("EditOrder")){ %> 
                                <a href="MallOrderDetail.aspx?OrderNo=<%#Eval("OrderNo") %>" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>   
                                <%} if (CheckAuth("PrintOrder"))
                                  {  %>                             
                                <a href="PrintOrder.aspx?OrderNo=<%#Eval("OrderNo") %>" class="btn btn-danger btn-sm" ><i class="fa fa-edit"></i>&nbsp;预览订单</a>
                                  <%} if (CheckAuth("DeleteOrder"))
                                  {  %>  
                                <asp:LinkButton runat="server" ID="lbDelete" CssClass="btn btn-danger btn-sm" CommandArgument='<%#Eval("ID") %>' CommandName="Delete" OnClientClick="return confirm('确认删除操作？')"><i class="fa fa-times"></i>&nbsp;删除</asp:LinkButton>
                                <%} %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <webdiyer:AspNetPager ID="AspNetPager" runat="server" PageSize="10" OnPageChanging="AspNetPager_PageChanging" CssClass="pager" CurrentPageButtonClass="cpb"
            PagingButtonSpacing="0" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页">
        </webdiyer:AspNetPager>
    </form>
</body>
</html>
