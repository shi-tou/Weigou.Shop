<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogisticsAdd.aspx.cs" Inherits="Weigou.Admin.Sys.LogisticsAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物流添加-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="infotable">
                <tr>
                    <td style="width: 100px;" class="tr">物流公司名称：</td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" CssClass="easyui-validatebox txt" Width="200px" data-options="required:true" missingmessage="请输入物流公司名称"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="tr">备注：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtRemark" CssClass="txt" Width="200px" Height="50px" Font-Size="13px" TextMode="MultiLine"></asp:TextBox>
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
