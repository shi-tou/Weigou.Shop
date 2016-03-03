<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SwearWordAdd.aspx.cs" Inherits="Weigou.Admin.Content.SwearWordAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>敏感词添加-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="infotable">
                <tr id="tr1">
                    <td class="tr">敏感词：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtSwearWord" Width="260" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入敏感词"></asp:TextBox>
                    </td>
                </tr>
                <tr id="tr3">
                    <td class="tr">替换词：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtReplaceWord" Width="260" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入替换词"></asp:TextBox>
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
