<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsBrandSet.aspx.cs" Inherits="Weigou.Admin.Goods.GoodsBrandSet" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>设置商品类别品牌-<%=PageTitle %></title>
    <script src="/JScript/highlight.js"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $('#txtKey').bind('keyup change', function (ev) {
                // pull in the new value
                var searchTerm = $(this).val();
                // remove any old highlighted terms
                $('.infotable').removeHighlight();
                // disable highlighting if empty
                if (searchTerm) {
                    // highlight the new term
                    $('.infotable').highlight(searchTerm);
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="infotable" cellpadding="0px" cellspacing="1px">
                <tr>
                    <td style="width: 150px;" class="tr">类别名称：</td>
                    <td>
                        <asp:Label ID="labName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">输入品牌名称高亮查找：</td>
                    <td>
                        <input id="txtKey" type="text" class="txt" />
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">选择品牌名称：</td>
                    <td>
                        <asp:CheckBoxList ID="cheBrand" runat="server" RepeatDirection="Horizontal" RepeatColumns="5"></asp:CheckBoxList></td>
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
