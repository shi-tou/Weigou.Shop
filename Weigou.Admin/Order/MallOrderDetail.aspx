<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MallOrderDetail.aspx.cs" Inherits="Weigou.Admin.Order.MallOrderDetail" %>

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
                <asp:Panel runat="server" ID="panelShip">
                <tr>
                    <td class="tr">快递公司：</td>
                    <td><asp:DropDownList runat="server" ID="ddlLogistics" Visible="false" CssClass="txt"></asp:DropDownList>
                        <asp:Label runat="server" ID="labLogisticsName"></asp:Label>
                          <asp:HiddenField ID="hfLogisticsCode" runat="server" /></td>
                    <td class="tr">快递单号：</td>
                    <td><asp:TextBox runat="server" ID="txtLogisticsNo" Visible="false" CssClass="txt"></asp:TextBox><asp:Label runat="server" ID="labLogisticsNo"></asp:Label> 
                    </td>
                </tr>
                </asp:Panel>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button runat="server" ID="btnShipped" CssClass="btn" Text="确认发货" OnClick="btnShipped_Click"  Visible="false"/>
                        <asp:Button runat="server" ID="BtnViewLogistics" Text="查看物流" CssClass="btn" Visible="false" OnClientClick="return ViewLogistics();" />
                        <asp:Button runat="server" ID="btnCancel" Text="返回" CssClass="btn" OnClientClick="CloseWin()" />
                    </td>
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
                        { field: 'SmallPicture', title: '图片', width: 120, align: 'center',formatter:FormatPic },
                        { field: 'GoodsName', title: '商品名', width: 250, align: 'center' },
                        { field: 'SaleProp', title: '商品属性', width: 300, align: 'center',formatter:SetGreen }, 
                        { field: 'SalePrice', title: '价格', width: 100, align: 'center',formatter:SetRed },
                        { field: 'Count', title: '数量', width: 80, align: 'center',formatter:SetBlue },
                        { field: 'TotalPrice', title: '小计', width: 80, align: 'center',formatter:SetGreen }
                    ]],
                    loadMsg: '正在加载数据，请稍候……',
                    rownumbers: true, //显示记录数
                    singleSelect: true,
                    nowrap: false
                });
            }
            function FormatPic(v){
                if(v!='')
                    v='<a href="#"><img style="width:65px; heigth:50px" src="'+ v +'"></a>'
                return v;
            }
    </script>
    </form>
</body>
</html>
