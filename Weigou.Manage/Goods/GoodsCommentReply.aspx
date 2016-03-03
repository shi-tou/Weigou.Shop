<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsCommentReply.aspx.cs" Inherits="Weigou.Manage.Goods.GoodsCommentReply" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">  
            <div class="ibox">
            <div class="ibox-title">
                <h5>该商品的评论信息</h5>
            </div>
             <div class="ibox-content">
            <table class="form form-horizontal">
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
                        <asp:TextBox runat="server" ID="txtContent" Width="500px" Height="80px" TextMode="MultiLine"  CssClass="form-control" title="请输入回复内容"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 150px;" class="tr">回复时间：</td>
                    <td colspan="3">
                        <asp:Label ID="labContentTime" runat="server"></asp:Label>
                    </td>
                </tr>
            <tr>
                <td></td>
                <td>
                <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                <a href="GoodsCommentList.aspx" class="btn btn-white">返回列表</a>
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
        </div>
       </div>
    </form>
</body>
</html>
