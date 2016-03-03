<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogisticsAdd.aspx.cs" Inherits="Weigou.Manage.Sys.LogisticsAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物流添加-<%=PageTitle %></title>
    <script type="text/javascript">
        $(function () {
            $("#form1").validate();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
         <div class="ibox">
        <div class="ibox-title">
            <h5>物流添加/编辑</h5>
        </div>
        <div class="ibox-content">
            <!--form-horizontal:水平排列的表单-->
            <table class="form form-horizontal">
                <tr>
                    <td style="width: 100px;" class="tr">物流公司名称：</td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control required" Width="200px" title="请输入物流公司名称"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="tr">备注：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtRemark" CssClass="form-control" Width="200px" Height="50px" Font-Size="13px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                         
                <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                <a href="LogisticsList.aspx" class="btn btn-white">返回列表</a>
                    </td>
                </tr>
            </table>
            </div>
        </div>
    </form>
</body>
</html>
