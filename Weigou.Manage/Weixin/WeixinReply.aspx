<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeixinReply.aspx.cs" Inherits="Weigou.Manage.Weixin.WeixinReply" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>微信自动回复</title>
    <script type="text/javascript">
        $(function () {
            //初始化验证表单
            $("#form1").validate({
                rules: {
                    txtSubscribeReply: "required",
                    txtDefaultReply: "required"
                },
                messages: {
                    txtSubscribeReply: "请输入关注回复内容",
                    txtDefaultReply: "请输入默认回复内容"
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server" class="form form-horizontal">
        <div class="ibox">
            <div class="ibox-title">
                <h5>微信自动回复设置</h5>
            </div>
            <div class="ibox-content" >
                <table class="form form-horizontal">
                    <tr>
                        <th style="width: 150px;">自定义关注回复内容:</th>
                        <td><asp:TextBox runat="server" ID="txtSubscribeReply" TextMode="MultiLine" class="form-control" style="width:350px; height:80px;"></asp:TextBox></td>
                    </tr>
                     <tr>
                        <th style="width: 150px;">自定义默认回复内容:</th>
                        <td><asp:TextBox runat="server" ID="txtDefaultReply" TextMode="MultiLine" class="form-control" style="width:350px; height:80px;"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th></th>
                        <td><asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-success" Text="保存设置" OnClick="btnSubmit_Click"  /></td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
