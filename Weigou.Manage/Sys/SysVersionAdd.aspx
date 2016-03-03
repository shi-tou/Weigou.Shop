<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysVersionAdd.aspx.cs" Inherits="Weigou.Manage.Sys.SysVersionAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>版本添加-<%=PageTitle %></title>
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
            <h5>APP版本添加/编辑</h5>
        </div>
        <div class="ibox-content">
            <!--form-horizontal:水平排列的表单-->
            <table class="form form-horizontal">
                <tr>
                    <td style="width: 100px;" class="tr">设备类型：</td>
                    <td>
                        <asp:RadioButtonList ID="radType" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Selected="True">Android</asp:ListItem>
                            <asp:ListItem Value="2">IOS</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td style="width: 100px;" class="tr">版本号：</td>
                    <td>
                        <asp:TextBox ID="txtVersionCode" runat="server" CssClass="form-control required" min="1" precision="0" Width="300" title="请输入版本号"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="tr">版本名称：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtVersionName" CssClass="form-control required" Width="300" title="请输入版本名称"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tr">更新内容：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtContent" CssClass="form-control required" Width="400px" Height="100px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tr">强制更新：</td>
                    <td>
                        <asp:RadioButtonList ID="radForceUpdate" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">不强制</asp:ListItem>
                            <asp:ListItem Value="1">强制</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <%--<tr>
                    <td class="tr">开启图标：</td>
                    <td>
                        <asp:RadioButtonList ID="radOpenIcon" runat="server" RepeatColumns="2" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">不开启</asp:ListItem>
                            <asp:ListItem Value="1">开启</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>--%>
                <tr>
                    <td class="tr">App下载地址：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtAppUrl" CssClass="form-control required" Width="300" title="请输入下载地址"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                         
                <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                <a href="SysVersionList.aspx" class="btn btn-white">返回列表</a>
                    </td>
                </tr>
            </table>
            </div>
        </div>
    </form>
</body>
</html>
