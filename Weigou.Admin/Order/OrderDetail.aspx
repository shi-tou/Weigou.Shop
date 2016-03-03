<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="Weiche.Admin.Order.OrderDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="easyui-panel p10"  data-options="title:'订单基本信息',iconCls:'icon-tip'" >
            <table class="infotable">
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
            </table>
            <table id="ListTable_OrderDetail">
            </table>
        </div>
        <script type="text/javascript" >
            $(function () {
                GetList();
            });

            //获取用户列表
            function GetList() {
                var tab = $('#ListTable_OrderDetail');
                tab.datagrid({
                    title: '订单商品清单',
                    data: <%=strOrderDetal%>,
                    columns: [[
                        { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
                        { field: 'OrderNo', title: '订单号', width: 120, align: 'center' },
                        { field: 'GoodsName', title: '商品名', width: 180, align: 'center' },
                        { field: 'Price', title: '价格', width: 100, align: 'center',formatter:SetRed },
                        { field: 'Count', title: '数量', width: 80, align: 'center',formatter:SetBlue },
                        { field: 'TotalPrice', title: '小计', width: 80, align: 'center',formatter:SetGreen }
                    ]],
                    loadMsg: '正在加载数据，请稍候……',
                    rownumbers: true, //显示记录数
                    singleSelect: true,
                    nowrap: false
                });
            }
    </script>
    </form>
</body>
</html>
