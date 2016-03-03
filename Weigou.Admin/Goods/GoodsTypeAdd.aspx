<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsTypeAdd.aspx.cs" Inherits="Weigou.Admin.Goods.GoodsTypeAdd" ValidateRequest ="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>商品类别-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="infotable" cellpadding="0px" cellspacing="1px" >
            <tr>
                <td style="width:150px;" class="tr">类别名称：</td>
                <td><asp:TextBox runat="server" ID="txtName"  CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入类别名称"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:150px;" class="tr">简拼：</td>
                <td><asp:TextBox runat="server" ID="txtSpell"  CssClass="txt"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:150px;" class="tr">上级类别：</td>
                <td><asp:DropDownList runat="server" ID="ddlParentType"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width:150px;" class="tr">描述：</td>
                <td><asp:TextBox runat="server" ID="txtRemark" CssClass="txt" TextMode="MultiLine" style="width:300px;height:50px; font-size:12px; line-height:25px;"></asp:TextBox></td>
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
