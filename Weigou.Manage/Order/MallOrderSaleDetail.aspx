<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MallOrderSaleDetail.aspx.cs" Inherits="Weigou.Manage.Order.MallOrderSaleDetail" %>

<%@ Import Namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function AgreeDeal() {
            var flag = false;
            if ($('#form1').form('validate')) {
                if (confirm("是否同意处理该售后申请？")) {
                    flag = true;
                }
            }
            return flag;
        }
        function RefuseDeal() {
            var flag = false;
            if ($('#form1').form('validate')) {
                if (confirm("是否拒绝处理该售后申请？")) {
                    flag = true;
                }
            }
            return flag;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ibox-content">
            <table class="form form-horizontal">
                <tr>
                    <td style="width: 150px;" class="tr">订单号：</td>
                    <td colspan="3">
                        <asp:Label runat="server" ID="labOrderNo"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">申请时间：</td>
                    <td style="width: 300px;">
                        <asp:Label runat="server" ID="labApplyTime"></asp:Label></td>
                    <td style="width: 150px;" class="tr">申请数量：</td>
                    <td>
                        <asp:Label runat="server" ID="labApplyNumber"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">会员昵称：</td>
                    <td style="width: 300px;">
                        <asp:Label runat="server" ID="labMemberName"></asp:Label></td>
                    <td style="width: 150px;" class="tr">会员手机号：</td>
                    <td>
                        <asp:Label runat="server" ID="labMemberMobile"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">收货人：</td>
                    <td style="width: 300px;">
                        <asp:Label runat="server" ID="labConsigneeName"></asp:Label></td>
                    <td style="width: 150px;" class="tr">收货人手机：</td>
                    <td>
                        <asp:Label runat="server" ID="labConsigneeMobileNo"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">详细收货地址：</td>
                    <td style="width: 300px;">
                        <asp:Label runat="server" ID="labDetailsAddress"></asp:Label></td>
                    <td style="width: 150px;" class="tr">邮政编码：</td>
                    <td>
                        <asp:Label runat="server" ID="lblZipCode"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">问题描述：</td>
                    <td colspan="3">
                        <asp:Label runat="server" ID="labDescription"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">处理备注：</td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Width="500" Height="80" CssClass="form-control" title="请输入处理备注"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">相关图片：</td>
                    <td colspan="3">
                        <a target="_blank" href="/SlidePicture.aspx?Type=3&ID=<%=_ID %>">
                            <asp:Image ID="imgPic" runat="server" Height="120" Width="200" /></a></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="BtnAgree" Text="同意处理" CssClass="btn btn-success" OnClick="BtnAgree_Click" OnClientClick="return AgreeDeal();" />
                        <asp:Button runat="server" ID="BtnRefuse" Text="拒绝处理" CssClass="btn btn-success" OnClick="BtnRefuse_Click" OnClientClick="return RefuseDeal();" />
                        <a href="MallOrderSaleList.aspx" class="btn btn-white">返回列表</a></td>
                </tr>
            </table>
            <table class="table table-hover table-bordered ">
                <thead>
                    <tr>
                        <th style="width: 50px">#</th>
                        <th class="hidden">ID</th>
                        <th>图片</th>
                        <th>商品名</th>
                        <th>商品属性</th>
                        <th>价格</th>
                        <th>数量</th>
                        <th>小计</th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (DataRow dr in OrderDetail.Rows)
                       {%>
                    <tr>
                        <td><%= OrderDetail.Rows.IndexOf(dr)+1 %></td>
                        <td class="hidden"><%= dr["ID"] %></td>
                        <td><a href="#">
                            <img style="width: 65px; height: 50px" src="<%=dr["SmallPicture"] %>"></a></td>
                        <td><%=dr["GoodsName"] %></td>
                        <td><%=dr["SaleProp"] %></td>
                        <td><%=dr["SalePrice"] %></td>
                        <td><%=dr["Count"] %></td>
                        <td><%=dr["TotalPrice"] %></td>
                    </tr>
                    <%      
               } %>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>

