<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MallOrderSaleList.aspx.cs" Inherits="Weigou.Manage.Order.MallOrderSaleList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %> 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>售后申请-<%=PageTitle %></title> 
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
       <div class="ibox">
            <div class="ibox-title">
                <h5>售后服务列表</h5>
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
                        <asp:TextBox runat="server" ID="txtOrderNo" class="form-control" Width="150"></asp:TextBox>
                    </td>
                    <th>收货人手机号：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtConsigneeMobileNo" class="form-control" Width="130"></asp:TextBox>
                    </td> 
                    <th>处理状态：</th>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Cssclass="form-control">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="0">申请售后</asp:ListItem>
                            <asp:ListItem Value="1">同意处理</asp:ListItem>
                            <asp:ListItem Value="2">拒绝处理</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                     
                    <th>申请时间：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtMinTime" class="form-control" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox> 
                    </td>
                    <td>~</td>
                    <td><asp:TextBox runat="server" ID="txtMaxTime" class="form-control" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox></td>
                    <th></th>
                    <td>
                        &nbsp;<asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="查 询" OnClick="btnSearch_Click" /> 
                    </td>
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
                    <th>售后类型</th>
                    <th>商品名称</th>
                    <th>申请数量</th>
                    <th>收货人手机号</th>
                    <th>处理状态</th>
                    <th>处理人</th>
                    <th>处理时间</th>
                    <th style="width:280px;">操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repOrder">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Eval("OrderNo") %></td>
                            <td><%#GetType(Eval("Type")) %></td>
                            <td><%#Eval("GoodsName") %></td>
                            <td><%#Eval("ApplyNumber") %></td>
                            <td><%#Eval("ConsigneeMobileNo") %></td>
                            <td><%#GetStatus(Eval("Status")) %></td>
                            <td><%#Eval("DealName") %></td> 
                            <td><%#FormatDateTime(Eval("DealTime")) %></td> 
                            <td>
                                <%if (CheckAuth("ViewSaleDetail"))
                                  { %> 
                                <a href="MallOrderSaleDetail.aspx?ID=<%#Eval("ID") %>" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>   
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
        <asp:HiddenField runat="server" ID="hfEdit" Value="0" />
    </form>
</body>
</html>
