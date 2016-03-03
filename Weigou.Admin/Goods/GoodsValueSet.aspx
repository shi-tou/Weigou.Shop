<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsValueSet.aspx.cs" Inherits="Weiche.Admin.Goods.GoodsValueSet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        $(function () {
            GetValueList();
        });
        var url = '/Ajax/Goods.ashx';
        function ClickBtn() {
            $.ajax({
                url: url,
                data: { 'action': 'SetValue', 'ID': $("#hideID").val(), "Value": $("#txtValue").val() },
                dataType: 'json',
                type: 'POST',
                success: function (data) {
                    if (data.res == 0) {
                        AlertInfo('置顶成功！');
                        GetValueList();
                    }
                    else {
                        AlertInfo('操作失败');
                    }
                }
            });
        }

        function GetValueList()
        {
            $.ajax({
                url: url,
                data: 'action=GetValueList&ID=' + $('#hideID').val(),
                dataType: 'json',
                type: 'POST',
                success: function (data) {
                    var html = '';
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            html += '<li>' + data[i].Value + '<span class="b2" onclick="DeleteAttributeValue(' + data[i].ID + ')">删除</span></li>';
                        }
                    }
                    $('#ValueList').html(html);
                }
            });
        }

        function DeleteAttributeValue(id)
        {
            $.messager.confirm('操作提示', '是否确认删除操作？', function (r) {
                if (r) {
                    $.ajax({
                        url: url,
                        data: { 'action': 'DeleteAttributeValue', 'ID': id },
                        dataType: 'json',
                        type: 'POST',
                        success: function (data) {
                            if (data.res == 0) {
                                AlertInfo('删除成功！');
                                GetValueList();
                            }
                            else {
                                AlertInfo('删除失败，请与管理员联系！');
                            }
                        }
                    });
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table class="infotable" style="width: 50%">
            <tr>
                <td>属性名：</td>
                <td><asp:Label runat="server" Font-Bold="true" ForeColor="Red" ID="labAttribute"></asp:Label></td>
            </tr>
            <tr>
                <td>属性值：</td>
                <td>
                    <asp:TextBox ID="txtValue" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <input onclick="javascript: ClickBtn()" type="button" class="btn" value="添加" />
                    <input onclick="javascript: CloseWin()" type="button" class="btn" value="取消" />
                </td>
            </tr>
            <asp:HiddenField runat="server" ID="hideID" />
        </table>
        <div>
            <ul  id="ValueList">

            </ul>
        </div>
    </form>
</body>
</html>
