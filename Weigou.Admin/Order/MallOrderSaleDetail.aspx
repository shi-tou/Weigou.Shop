<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MallOrderSaleDetail.aspx.cs" Inherits="Weigou.Admin.Order.MallOrderSaleDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function AgreeDeal() {
            var flag=false;
            if ($('#form1').form('validate')) {
                if(confirm("是否同意处理该售后申请？"))
                {
                    flag=true;
                }
            }
            return flag;
        }
        function RefuseDeal() {
            var flag=false;
            if ($('#form1').form('validate')) {
                if(confirm("是否拒绝处理该售后申请？"))
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
        <div class="easyui-panel p10" data-options="title:'基本信息',iconCls:'icon-tip'">
            <table class="infotable">
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
                        <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Width="500" Height="80" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入处理备注"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">相关图片：</td>
                    <td colspan="3">
                        <a target="_blank" href="/SlidePicture.aspx?Type=3&ID=<%=_ID %>">
                            <asp:Image ID="imgPic" runat="server" Height="120" Width="200" /></a></td>
                </tr>
            </table>
            <div class="action">
                <asp:Button runat="server" ID="BtnAgree" Text="同意处理" CssClass="btn" OnClick="BtnAgree_Click" OnClientClick="return AgreeDeal();" />
                <asp:Button runat="server" ID="BtnRefuse" Text="拒绝处理" CssClass="btn" OnClick="BtnRefuse_Click" OnClientClick="return RefuseDeal();" />
                <asp:Button runat="server" ID="btnCancel" Text="返回" CssClass="btn" OnClientClick="CloseWin()" />
            </div>
            <table id="ListTable_OrderDetail">
            </table>
        </div>
        <script type="text/javascript">
            $(function () {
                GetList();
            });

            //获取订单详情
            function GetList() {
                var tab = $('#ListTable_OrderDetail');
                tab.datagrid({
                    title: '售后服务商品清单',
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

