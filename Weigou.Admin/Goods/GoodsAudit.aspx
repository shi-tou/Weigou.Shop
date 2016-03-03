<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsAudit.aspx.cs" Inherits="Weigou.Admin.Goods.GoodsAudit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style>
        /*表格样式*/
        table#process {
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
            text-align: center;
        }

            table#process th {
                text-align: center;
                border-width: 1px;
                padding: 8px;
                border-style: solid;
                border-color: #666666;
                background-color: #dedede;
            }

            table#process td {
                text-align: center;
                border-width: 1px;
                padding: 8px;
                border-style: solid;
                border-color: #666666;
                background-color: #ffffff;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="hideID" runat="server" />
        <div>
            <table class="infotable" cellpadding="0px" cellspacing="1px">
                <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">商品编码：</td>
                    <td>
                        <asp:Label ID="labCode" runat="server"></asp:Label>
                    </td>
                    <td style="width: 150px;" class="tr">商品类别：</td>
                    <td>
                        <asp:Label ID="labTypeName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">商品名称：</td>
                    <td style="width: 200px;">
                        <asp:Label ID="labName" runat="server"></asp:Label>
                    </td>
                    <td class="tr">上架状态：</td>
                    <td>
                        <asp:Label ID="labShelvesStatus" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>

                    <td class="tr">销售价格：</td>
                    <td>
                        <asp:Label ID="labSalePrice" runat="server"></asp:Label>
                    </td>
                    <td class="tr">市场价格：</td>
                    <td>
                        <asp:Label ID="labMarketPrice" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tr">商品库存：</td>
                    <td>
                        <asp:Label ID="labStock" runat="server"></asp:Label>
                    </td>
                    <td class="tr">审核状态：</td>
                    <td>
                        <asp:Label ID="labStatus" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tr">简单介绍：</td>
                    <td colspan="3">
                        <asp:Label ID="labSimpleDesc" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tr">审核备注：</td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtApprovedRemark" CssClass="txt" Width="500px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div class="action">
                <asp:Button runat="server" ID="BtnSubmit" Text="通过" CssClass="btn" OnClick="BtnSubmit_Click" />
                <asp:Button runat="server" ID="BtnUnSubmit" Text="不通过" CssClass="btn" OnClick="BtnUnSubmit_Click" />
                <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
            </div>
        </div>
    </form>
</body>
</html>
