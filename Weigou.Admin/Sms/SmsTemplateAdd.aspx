<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsTemplateAdd.aspx.cs" Inherits="Weigou.Admin.Sys.SmsTemplateAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>短信模板添加-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="infotable" >
            <tr>
                <td style="width:70px;" class="tr">模板编号：</td>
                <td><asp:TextBox ID="txtCode" runat="server" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入编号"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tr">模板内容：</td>
                <td><asp:TextBox ID="txtContent" runat="server" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入内容请输入内容" TextMode="MultiLine" style="width:280px;height:100px; font-size:12px;" ></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tr">简述：</td>
                <td><asp:TextBox ID="txtDesc" runat="server" CssClass="txt" Width="250px" ></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tr">类型：</td>
                <td>
                    <asp:RadioButtonList runat="server" ID="rblIsSystem" RepeatLayout="Table" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected=True>系统模板</asp:ListItem>
                        <asp:ListItem Value="0">自定义模板</asp:ListItem>
                    </asp:RadioButtonList>&nbsp;&nbsp;<span class="red">注：系统模板不允许删除</span>
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