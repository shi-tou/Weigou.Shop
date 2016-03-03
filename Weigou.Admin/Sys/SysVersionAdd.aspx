<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysVersionAdd.aspx.cs" Inherits="Weigou.Admin.Sys.SysVersionAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>版本添加-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="infotable">
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
                        <asp:TextBox ID="txtVersionCode" runat="server" CssClass="easyui-numberbox txt" min="1" precision="0" Width="300" data-options="required:true" missingmessage="请输入版本号"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="tr">版本名称：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtVersionName" CssClass="easyui-validatebox txt" Width="300" data-options="required:true" missingmessage="请输入版本名称"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tr">更新内容：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtContent" CssClass="txt" Width="400px" Height="100px" TextMode="MultiLine"></asp:TextBox>
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
                        <asp:TextBox runat="server" ID="txtAppUrl" CssClass="easyui-validatebox txt" Width="300" data-options="required:true" missingmessage="请输入下载地址"></asp:TextBox>
                    </td>
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
