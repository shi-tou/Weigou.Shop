<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaAdd.aspx.cs" Inherits="Weigou.Admin.Sys.AreaAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <!--省-->
    <asp:Panel runat="server" ID="panel_P" Visible="false">
        <table class="infotable" >
            <tr>
                <td class="tr" style="width:100px;">省名称</td>
                <td>
                    <asp:TextBox runat="server" ID="txtProvinceName" CssClass="easyui-validatebox txt"  data-options="required:true" missingmessage="请输入省名称"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">全拼：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtSpell_P" CssClass="txt" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">首字母：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtFirstLetter_P" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">简称：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtShortName" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
        </table>
         <div class="action">
            <asp:Button runat="server" ID="BtnSubmitP" Text="确认" CssClass="btn" OnClientClick="return $('#form1').form('validate')" OnClick="BtnSubmit_Click" />
            <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
        </div>
    </asp:Panel>
    <!--市-->
    <asp:Panel runat="server" ID="panel_C" Visible="false">
        <table class="infotable" >
            <tr>
                <td class="tr" style="width:100px;">城市名称</td>
                <td>
                    <asp:TextBox runat="server" ID="txtCityName" CssClass="easyui-validatebox txt"  data-options="required:true" missingmessage="请输入省名称"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">全拼：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtSpell_C" CssClass="txt" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">首字母：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtFirstLetter_C" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">所在省：</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProvince_C" CssClass="txt"></asp:DropDownList>
                </td>
            </tr>
        </table>
         <div class="action">
            <asp:Button runat="server" ID="BtnSubmitC" Text="确认" CssClass="btn" OnClientClick="return $('#form1').form('validate')" OnClick="BtnSubmit_Click" />
            <asp:Button runat="server" ID="Button2" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
        </div>
    </asp:Panel>
    <!--区-->
    <asp:Panel runat="server" ID="panel_D" Visible="false" >
        <table class="infotable" >
            <tr>
                <td class="tr" style="width:100px;">地区名称</td>
                <td>
                    <asp:TextBox runat="server" ID="txtDistrictName" CssClass="easyui-validatebox txt"  data-options="required:true" missingmessage="请输入省名称"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">所在省：</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProvince_D" CssClass="txt"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tr">所在市：</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCity" CssClass="txt"></asp:DropDownList>
                </td>
            </tr>
        </table>
         <div class="action">
            <asp:Button runat="server" ID="BtnSubmitD" Text="确认" CssClass="btn" OnClientClick="return $('#form1').form('validate')" OnClick="BtnSubmit_Click" />
            <asp:Button runat="server" ID="Button4" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
        </div>
    </asp:Panel>
    </form>
</body>
</html>
