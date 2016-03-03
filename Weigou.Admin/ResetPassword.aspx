<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Weigou.Admin.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="infotable">
                <tr>
                    <td style="width: 100px;" class="tr">旧密码：</td>
                    <td>
                        <asp:TextBox ID="txtOldPwd" runat="server" CssClass="easyui-validatebox txt" TextMode="Password" Width="200px" data-options="required:true" missingmessage="请输入旧密码"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tr">新密码：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNewPwd" CssClass="easyui-validatebox txt" Width="200px" TextMode="Password" data-options="required:true" missingmessage="请输入新密码"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tr">确认新密码：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtConfirmNewPwd" CssClass="easyui-validatebox txt" Width="200px" TextMode="Password" data-options="required:true" missingmessage="请输入确认密码" validType="EqualTo['#txtNewPwd']"></asp:TextBox>
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
