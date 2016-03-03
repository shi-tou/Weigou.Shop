<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MallOrderReturnList.aspx.cs" Inherits="Weigou.Manage.Order.MallOrderReturnList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>退款列表-<%=PageTitle %></title>
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function selectAll(tabId) {
            $("#" + tabId + " input[type='checkbox']").each(function () {
                if ($(this).prop("checked")) {
                    $(this).prop("checked", '');
                }
                else {
                    $(this).prop("checked", 'checked');
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div class="ibox">
            <ul class="nav nav-tabs">
                <li class="<%=TabIndex==1?"active":"" %>"><a data-toggle="tab" href="MallOrderReturnList.aspx#tab-1" aria-expanded="<%=TabIndex==1?"true":"false" %>">支付宝退款管理 </a>
                </li>
                <li class="<%=TabIndex==2?"active":"" %>"><a data-toggle="tab" href="MallOrderReturnList.aspx#tab-2" aria-expanded="<%=TabIndex==2?"true":"false" %>">微信退款管理</a>
                </li>
                <li class="<%=TabIndex==3?"active":"" %>"><a data-toggle="tab" href="MallOrderReturnList.aspx#tab-3" aria-expanded="<%=TabIndex==3?"true":"false" %>">银联退款管理</a>
                </li>
            </ul>
            <div class="tab-content">
                <div id="tab-1" class="tab-pane <%=TabIndex==1?"active":"" %>">
                    <div class="panel-body">
                        <table class="form-group">
                            <tr>
                                <th></th>
                                <td>
                                    <asp:Button runat="server" ID="btnRefundAliPay" CssClass="btn btn-primary" Text="支付宝批量退款" OnClick="btnRefundAliPay_Click" />&nbsp;
                                </td>
                                <th>订单号：</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtOrderNo_AliPay" class="form-control" Width="150"></asp:TextBox>
                                </td>
                                <th>会员手机号：</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtConsigneeMobileNo_AliPay" class="form-control" Width="130"></asp:TextBox>
                                </td>
                                <th>申请时间：</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtMinTime_AliPay" class="form-control" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                </td>
                                <td>~</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtMaxTime_AliPay" class="form-control" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox></td>
                                <td>&nbsp;<asp:Button runat="server" ID="btnSearch_AliPay" CssClass="btn btn-primary" Text="查 询" OnClick="btnSearch_Alipay_Click" />
                                    <%--<input type="button" id="Button3" onclick="ResetSearchFormB();" class="btn" value="重置" />--%>
                                </td>
                            </tr>
                        </table>
                        <table class="table table-hover table-bordered ">
                            <thead>
                                <tr>
                                    <th style="width: 50px">#</th>
                                    <th><a onclick="selectAll('tab-1')" href="javascript:(0);">全选</a></th>
                                    <th>订单号</th>
                                    <th>流水号</th>
                                    <th>会员手机</th>
                                    <th>会员姓名</th>
                                    <th>订单状态</th>
                                    <th>申请时间</th>
                                    <th>处理人</th>
                                    <th>处理时间</th>
                                    <th style="width: 280px;">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="repOrder">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Container.ItemIndex+1 %></td>
                                            <td>
                                                <input type="checkbox" name="cbAliPayOrderNo[]" value="<%#Eval("OrderNo") %>" /></td>
                                            <td><%#Eval("OrderNo") %></td>
                                            <td><%#Eval("NotifyTradeNo") %></td>
                                            <td><%#Eval("MobileNo") %></td>
                                            <td><%#Eval("MemberName") %></td>
                                            <td><%#FormatStatus(Eval("Status")) %></td>
                                            <td><%#FormatDateTime(Eval("ApplyTime")) %></td>
                                            <td><%#Eval("DealName") %></td>
                                            <td><%#FormatDateTime(Eval("DealTime")) %></td>
                                            <td>
                                                <%if (CheckAuth("ViewReturnAliPayDetail"))
                                                  { %>
                                                <a href="MallOrderReturnDetail.aspx?ID=<%#Eval("ID") %>&OrderNo=<%#Eval("OrderNo") %>&tab=1" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>
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
                    </div>
                </div>
                <div id="tab-2" class="tab-pane <%=TabIndex==2?"active":"" %>">
                    <div class="panel-body">
                        <table class="form-group">
                            <tr>
                                <th></th>
                                <td>
                                    <asp:Button runat="server" ID="btnRefundWeixin" CssClass="btn btn-primary" Text="微信批量退款" OnClick="btnRefundWeixin_Click" />&nbsp;
                                </td>
                                <th>订单号：</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtOrderNo_Wx" class="form-control" Width="150"></asp:TextBox>
                                </td>
                                <th>会员手机号：</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtConsigneeMobileNo_Wx" class="form-control" Width="130"></asp:TextBox>
                                </td>
                                <th>申请时间：</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtMinTime_Wx" class="form-control" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>

                                </td>
                                <td>~</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtMaxTime_Wx" class="form-control" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox></td>
                                <td>&nbsp;
                                    <asp:Button runat="server" ID="btnSearch_Wx" CssClass="btn btn-primary" Text="查 询" OnClick="btnSearch_Wx_Click" />
                                    <%-- <input type="button" id="Button2" onclick="ResetSearchFormA();" class="btn" value="重置" />--%>
                                </td>
                            </tr>
                        </table>
                        <table class="table table-hover table-bordered ">
                            <thead>
                                <tr>
                                    <th style="width: 50px">#</th>
                                    <th><a onclick="selectAll('tab-2')" href="javascript:(0);">全选</a></th>
                                    <th>订单号</th>
                                    <th>流水号</th>
                                    <th>会员手机</th>
                                    <th>会员姓名</th>
                                    <th>订单状态</th>
                                    <th>申请时间</th>
                                    <th>处理人</th>
                                    <th>处理时间</th>
                                    <th style="width: 280px;">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="repOrderWx">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Container.ItemIndex+1 %></td>
                                            <td>
                                                <input type="checkbox" name="cbWxOrderNo[]" value="<%#Eval("OrderNo") %>" /></td>
                                            <td><%#Eval("OrderNo") %></td>
                                            <td><%#Eval("NotifyTradeNo") %></td>
                                            <td><%#Eval("MobileNo") %></td>
                                            <td><%#Eval("MemberName") %></td>
                                            <td><%#FormatStatus(Eval("Status"))  %></td>
                                            <td><%#FormatDateTime(Eval("ApplyTime")) %></td>
                                            <td><%#Eval("DealName") %></td>
                                            <td><%#FormatDateTime(Eval("DealTime")) %></td>
                                            <td>
                                                <%if (CheckAuth("ViewReturnWeixinDetail"))
                                                  { %>
                                                <a href="MallOrderReturnDetail.aspx?ID=<%#Eval("ID") %>&OrderNo=<%#Eval("OrderNo") %>&tab=2" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>
                                                <%} %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <webdiyer:AspNetPager ID="AspNetPagerWx" runat="server" PageSize="10" OnPageChanging="AspNetPagerWx_PageChanging" CssClass="pager" CurrentPageButtonClass="cpb"
                            PagingButtonSpacing="0" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
                <div id="tab-3" class="tab-pane <%=TabIndex==3?"active":"" %>">
                    <div class="panel-body">
                        <table class="form-group">
                            <tr>
                                <th></th>
                                <td>
                                    <asp:Button runat="server" ID="btnRefundUnionpay" CssClass="btn btn-primary" Text="银联批量退款" OnClick="btnRefundUnionpay_Click" />&nbsp;
                                </td>
                                <th>订单号：</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtOrderNo_Unionpay" class="form-control" Width="150"></asp:TextBox>
                                </td>
                                <th>会员手机号：</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtConsigneeMobileNo_Unionpay" class="form-control" Width="130"></asp:TextBox>
                                </td>
                                <th>申请时间：</th>
                                <td>
                                    <asp:TextBox runat="server" ID="txtMinTime_Unionpay" class="form-control" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>

                                </td>
                                <td>~</td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtMaxTime_Unionpay" class="form-control" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox></td>
                                <td>&nbsp;
                                    <asp:Button runat="server" ID="btnSearch_Unionpay" CssClass="btn btn-primary" Text="查 询" OnClick="btnSearch_Unionpay_Click" />
                                    <%--    <input type="button" id="Button4" onclick="ResetSearchFormC();" class="btn" value="重置" />--%>
                                </td>
                            </tr>
                        </table>
                        <table class="table table-hover table-bordered ">
                            <thead>
                                <tr>
                                    <th style="width: 50px">#</th>
                                    <th><a onclick="selectAll('tab-3')" href="javascript:(0);">全选</a></th>
                                    <th>订单号</th>
                                    <th>流水号</th>
                                    <th>会员手机</th>
                                    <th>会员姓名</th>
                                    <th>订单状态</th>
                                    <th>申请时间</th>
                                    <th>处理人</th>
                                    <th>处理时间</th>
                                    <th style="width: 280px;">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="repOrderUnionpay">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Container.ItemIndex+1 %></td>
                                            <td>
                                                <input type="checkbox" name="cbUnionpayOrderNo[]" value="<%#Eval("OrderNo") %>" /></td>
                                            <td><%#Eval("OrderNo") %></td>
                                            <td><%#Eval("NotifyTradeNo") %></td>
                                            <td><%#Eval("MobileNo") %></td>
                                            <td><%#Eval("MemberName") %></td>
                                            <td><%#FormatStatus(Eval("Status"))  %></td>
                                            <td><%#FormatDateTime(Eval("ApplyTime")) %></td>
                                            <td><%#Eval("DealName") %></td>
                                            <td><%#FormatDateTime(Eval("DealTime")) %></td>
                                            <td>
                                                <%if (CheckAuth("ViewReturnUnionpayDetail"))
                                                  { %>
                                                <a href="MallOrderReturnDetail.aspx?ID=<%#Eval("ID") %>&OrderNo=<%#Eval("OrderNo") %>&tab=3" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>
                                                <%} %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <webdiyer:AspNetPager ID="AspNetPagerUnionpay" runat="server" PageSize="10" OnPageChanging="AspNetPagerUnionpay_PageChanging" CssClass="pager" CurrentPageButtonClass="cpb"
                            PagingButtonSpacing="0" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
            </div>
        </div>
    </form>

</body>
</html>
