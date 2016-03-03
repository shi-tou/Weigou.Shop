<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsCommentReply.aspx.cs" Inherits="Weigou.Admin.Goods.GoodsCommentReply" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="font-size: 13px; font-weight: bold; padding: 5px 20px">该商品的首次评论信息：</div>
            <table class="infotable" cellpadding="0px" cellspacing="1px">
                <tr>
                    <td style="width: 150px;" class="tr">评论时间：</td>
                    <td colspan="3">
                        <asp:Label ID="labFirstCommentTime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">评论内容：</td>
                    <td colspan="3">
                        <asp:Label runat="server" ID="labFirstCommentContent"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">回复内容：</td>
                    <td colspan="3">
                        <asp:Label runat="server" ID="labContent"></asp:Label>
                        <asp:TextBox runat="server" ID="txtContent" Width="500px" Height="80px" TextMode="MultiLine"  CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入回复内容"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 150px;" class="tr">回复时间：</td>
                    <td colspan="3">
                        <asp:Label ID="labContentTime" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
          <%--  <div style="font-size: 13px; font-weight: bold; padding: 5px 20px">该商品的追加评论信息：</div>
            <table class="infotable" cellpadding="0px" cellspacing="1px">
                <tr>
                    <td style="width: 150px;" class="tr">评论时间：</td>
                    <td colspan="3">
                        <asp:Label ID="labAppendCommentTime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">评论内容：</td>
                    <td colspan="3">
                        <asp:Label runat="server" ID="labAppendCommentContent"></asp:Label>
                    </td>
                </tr>
            </table>--%>
            <div class="action">
                <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn" OnClientClick="return $('#form1').form('validate')"  OnClick="BtnSubmit_Click" />
                <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
            </div>
        </div>
    </form>
</body>
</html>
