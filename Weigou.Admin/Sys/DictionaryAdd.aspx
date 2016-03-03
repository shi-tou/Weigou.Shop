<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DictionaryAdd.aspx.cs" Inherits="Weigou.Admin.Sys.DictionaryAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>数据字典编辑-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="infotable" >
            <tr>
                <td class="tr" style="width:100px;">上一级：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtParentName" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">名称：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtName" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入名称"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">字典值：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtValue" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">编码：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtCode" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">备注：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtRemark" CssClass="txt" TextMode="MultiLine" style="width:300px;height:50px; font-size:12px; line-height:25px;"></asp:TextBox>
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
