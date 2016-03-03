<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsTemplateAdd.aspx.cs" Inherits="Weigou.Manage.Sys.SmsTemplateAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>短信模板添加-<%=PageTitle %></title>
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
            <h5>短信模板添加/编辑</h5>
        </div>
        <div class="ibox-content">
            <!--form-horizontal:水平排列的表单-->
            <table class="form form-horizontal">
                <tr>
                    <td>模板编号：</td>
                    <td>
                        <asp:TextBox ID="txtCode" runat="server" CssClass="form-control  required" title="请输入编号"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>模板内容：</td>
                    <td>
                        <asp:TextBox ID="txtContent" runat="server" CssClass="form-control required" title="请输入内容请输入内容" TextMode="MultiLine" Style="width: 280px; height: 100px; font-size: 12px;"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="tr">简述：</td>
                    <td>
                        <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" Width="250px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="tr">类型：</td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rblIsSystem" RepeatLayout="Table" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Selected="True">系统模板</asp:ListItem>
                            <asp:ListItem Value="0">自定义模板</asp:ListItem>
                        </asp:RadioButtonList>&nbsp;&nbsp;<span class="red">注：系统模板不允许删除</span>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                        <a href="SmsTemplateList.aspx" class="btn btn-white">返回列表</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>