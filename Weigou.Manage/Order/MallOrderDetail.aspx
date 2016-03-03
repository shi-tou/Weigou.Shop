<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MallOrderDetail.aspx.cs" Inherits="Weigou.Manage.Order.MallOrderDetail" %>
<%@ Import Namespace="System.Data" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function ConfirmShip() {
            var flag=false;
            if ($('#form1').form('validate')) {
                if(confirm("是否确认发货操作？"))
                {
                    flag=true;
                }
            }
            return flag;
        }

        function UpdateLoLogistics()
        {
            var flag=false;
            if ($('#form1').form('validate')) {
                if(confirm("邮费如有变更,会发站内信及推送给会员"))
                {
                    flag=true;
                }
            }
            return flag;
        }

        function ViewLogistics() {
            window.open("http://m.kuaidi100.com/index_all.html?type="+$("#hfLogisticsCode").val()+"&postid="+$("#labLogisticsNo").text());
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ibox-content">
            <table class="form form-horizontal  table-bordered ">
                <tr>
                    <td class="tr">订单号：</td>
                    <td colspan="3" ><asp:Label runat="server" ID="labOrderNo" Font-Bold="true" ForeColor="Blue"></asp:Label></td>
                   
                </tr>
                <tr>
                    <td style="width:150px;" class="tr">订单时间：</td><td style="width:200px;"><asp:Label runat="server" ID="labOrderTime"></asp:Label></td>
                    <td style="width:150px;" class="tr">订单状态：</td><td><asp:Label runat="server" ID="labStatus"></asp:Label></td>
                </tr>
                <tr>
                    <td class="tr">会员名称：</td><td><asp:Label runat="server" ID="labMemberName"></asp:Label></td>
                    <td class="tr">会员手机号：</td><td><asp:Label runat="server" ID="labMobileNo"></asp:Label></td>
                </tr>
                <tr>
                    <td class="tr">商品总数：</td><td><asp:Label runat="server" ID="labTotalCount"></asp:Label></td>
                    <td class="tr">订单总金额：</td><td><asp:Label runat="server" ID="labTotalMoney"></asp:Label></td>
                </tr>
                <tr>
                    <td class="tr">收货人：</td><td><asp:Label runat="server" ID="labConsigneeName"></asp:Label></td>
                    <td class="tr">收货人手机：</td><td><asp:Label runat="server" ID="labConsigneeMobileNo"></asp:Label></td>
                </tr>
                <tr>
                    <td class="tr">收货地址：</td><td colspan="3"><asp:Label runat="server" ID="labDeliverAddress"></asp:Label></td>
                </tr>
                <asp:Panel runat="server" ID="panelShip">
                <tr>
                    <td class="tr">快递公司：</td>
                    <td><asp:DropDownList runat="server" ID="ddlLogistics" Visible="false" Cssclass="form-control"></asp:DropDownList>
                        <asp:Label runat="server" ID="labLogisticsName"></asp:Label>
                          <asp:HiddenField ID="hfLogisticsCode" runat="server" /></td>
                    <td class="tr">快递单号：</td>
                    <td><asp:TextBox runat="server" ID="txtLogisticsNo" Visible="false" Cssclass="form-control"></asp:TextBox><asp:Label runat="server" ID="labLogisticsNo"></asp:Label> 
                    </td>
                </tr>
                </asp:Panel>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button runat="server" ID="btnShipped" CssClass="btn btn-success" Text="确认发货" OnClick="btnShipped_Click"  Visible="false"/>
                        <asp:Button runat="server" ID="BtnViewLogistics" Text="查看物流" CssClass="btn btn-success" Visible="false" OnClientClick="return ViewLogistics();" />
                        <a href="MallOrderList.aspx" class="btn btn-white">返回列表</a>
                    </td>
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
                            <td><a href="#"><img style="width:65px; height:50px" src="<%=dr["SmallPicture"] %>"></a></td>
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
