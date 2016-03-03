<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsAudit.aspx.cs" Inherits="Weigou.Manage.Goods.GoodsAudit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title> 
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="hideID" runat="server" />
        <div class="ibox">
            <div class="ibox-title">
                <h5>用户添加/编辑</h5>
            </div>
            <div class="ibox-content">
                <!--form-horizontal:水平排列的表单-->
                <table class="form form-horizontal">
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
                        <asp:TextBox runat="server" ID="txtApprovedRemark" CssClass="form-control" Width="500px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tr"></td>
                    <td colspan="3">
                        <asp:Button runat="server" ID="BtnSubmit" Text="通过" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                <asp:Button runat="server" ID="BtnUnSubmit" Text="不通过" CssClass="btn btn-success" OnClick="BtnUnSubmit_Click" />
                 <a href="GoodsList.aspx" class="btn btn-white">返回列表</a>
                    </td>
                </tr>
            </table>
          </div> 
        </div>
    </form>
</body>
</html>
