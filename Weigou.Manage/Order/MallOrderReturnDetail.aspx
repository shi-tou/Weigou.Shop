<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MallOrderReturnDetail.aspx.cs" Inherits="Weigou.Manage.Order.MallOrderReturnDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function AgreeRefund() {
            var flag=false;
            if ($('#form1').form('validate')) {
                if(confirm("是否同意该退款申请？"))
                {
                    flag=true;
                }
            }
            return flag;
        }
        function AgreeShip() {
            var flag=false;
            if ($('#form1').form('validate')) {
                if(confirm("是否立即发货？"))
                {
                    flag=true;
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
                    <td style="width: 150px;" class="tr">申请时间：</td>
                    <td style="width: 300px;">
                        <asp:Label runat="server" ID="labApplyTime"></asp:Label></td>
                    <td style="width: 150px;" class="tr">处理时间：</td>
                    <td>
                        <asp:Label runat="server" ID="labDealTime"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">支付类型：</td>
                    <td style="width: 300px;">
                        <asp:Label runat="server" ID="labPayMentType"></asp:Label></td>
                    <td style="width: 150px;" class="tr">退款金额：</td>
                    <td>
                        <asp:Label runat="server" ID="labOrderTotalMoney"></asp:Label></td>
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
                    <td style="width: 150px;" class="tr">订单状态：</td>
                    <td style="width: 300px;">
                        <asp:Label runat="server" ID="labOrderStatus"></asp:Label></td>
                    <td style="width: 150px;" class="tr">退款原因：</td>
                    <td>
                        <asp:Label runat="server" ID="labReason"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">卖家退款备注：</td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Width="400px" Height="100px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">温馨提示：</td>
                    <td colspan="3">
                        <span style="color: red;">买家提交申请开始至24小时后,若卖家未对该请求做处理，系统则默认为卖家已同意</span></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="BtnAgree" Text="同意退款" CssClass="btn btn-success" OnClick="BtnAgree_Click" OnClientClick="return AgreeRefund();" />
                        <asp:Button runat="server" ID="BtnShip" Text="立即发货" CssClass="btn btn-success" OnClick="BtnShip_Click" OnClientClick="return AgreeShip();" />
                        <a href="MAllOrderReturnList.aspx?tab=<%=Tab %>" class="btn btn-white">返回列表</a>

                    </td>
                </tr>
            </table>
            <table id="ListTable_OrderDetail">
            </table>
        </div>
        <script type="text/javascript">
            $(function () {
                GetList();
            });
            //获取订单详情列表
            function GetList() {
                var tab = $('#ListTable_OrderDetail');
                tab.datagrid({
                    title: '订单商品清单',
                    data: <%=strOrderDetal%>,
                    columns: [[
                        { field: 'GoodsName', title: '商品名称', width: 180, align: 'center' },
                        { field: 'SmallPicture', title: '商品图片', width: 120, align: 'center', formatter: FormatPic },
                        { field: 'SalePrice', title: '单价', width: 100, align: 'center',formatter:SetRed },
                        { field: 'Count', title: '数量', width: 80, align: 'center',formatter:SetBlue },
                        { field: 'SaleProp', title: '商品属性', width: 300, align: 'center',formatter:SetGreen },
                    ]],
                    loadMsg: '正在加载数据，请稍候……',
                    rownumbers: true, //显示记录数
                    singleSelect: true,
                    nowrap: false
                });
            }

            function FormatPic(v) {
                if (v == '' || v == null) {
                    return '暂无图片';
                }
                else
                    return '<img src="' + v + '" style="width:60px; height:55px;" />';
            }
        </script>
    </form>
</body>
</html>

