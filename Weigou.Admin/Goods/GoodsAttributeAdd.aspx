<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsAttributeAdd.aspx.cs" Inherits="Weigou.Admin.Goods.GoodsAttributeAdd" ValidateRequest ="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>商品类别-<%=PageTitle %></title>
    <script>
        $(function () {
            $('#ddlType').bind('change', function () {
                ChangeType();
            });
            ChangeType();
        });
        function ChangeType() {
            var t = $('#ddlType').val();
            if (t == '1') {
                $('#trValue').show();
                $('#txtValue').validatebox('reduce');
            }
            else {
                $('#trValue').hide();
                $('#txtValue').validatebox('remove');
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="infotable" cellpadding="0px" cellspacing="1px" >
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td style="width:100px;" class="tr">属性名称：</td>
                <td><asp:TextBox runat="server" ID="txtName"  CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入属性名称"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:100px;" class="tr">别名：</td>
                <td><asp:TextBox runat="server" ID="txtAlias"  CssClass="easyui-validatebox txt" ></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:100px;" class="tr">排序号：</td>
                <td><asp:TextBox runat="server" ID="txtSort"  CssClass="easyui-validatebox txt" ></asp:TextBox></td>
            </tr>
        </table>
        <div class="action">
            <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn" OnClientClick="return $('#form1').form('validate')" OnClick="BtnSubmit_Click" />
            <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
        </div>
    </div>
    </form>
</body>
</html>
