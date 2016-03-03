<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsBrandAdd.aspx.cs" Inherits="Weigou.Admin.Goods.GoodsBrandAdd" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加品牌-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="infotable" cellpadding="0px" cellspacing="1px">
                <tr>
                    <td style="width: 150px;" class="tr">品牌名称：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtName" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入品牌名称"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="tr">品牌LOGO：</td>
                    <td>
                        <asp:FileUpload runat="server" ID="txtLogo" /></td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Image ImageAlign="AbsMiddle" runat="server" ID="AvatarUrl" Width="200px" Height="125px" onerror="this.src='/Style/images/nopic.jpg'" />
                        <asp:HiddenField runat="server" ID="hfAvatar" />
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
