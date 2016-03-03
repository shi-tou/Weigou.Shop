<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeixinKeywordAdd.aspx.cs" Inherits="Weigou.Manage.Weixin.WeixinKeywordAdd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>关键词添加-<%=PageTitle %></title>
    <script type="text/javascript">
        $(function(){
            $("#form_KeywrodAdd").validate({
                rules: {
                    txtName: "required"
                },
                messages: {
                    txtName: {
                        required: "请输入关键词"
                    }
                }
            });
        });
    </script>
</head>
<body>
    <form id="form_KeywrodAdd" runat="server">
    <div class="ibox">
        <div class="ibox-title">
            <h5>关键词添加/编辑</h5>
        </div>
        <div class="ibox-content">
            <!--form-horizontal:水平排列的表单-->
            <table class="form form-horizontal">
                <tr>
                    <th style="width: 150px;">关键词：</th>
                    <td><asp:TextBox ID="txtName" runat="server" CssClass="form-control" title="请输入关键词"></asp:TextBox></td>
                </tr>
                <tr>
                    <th></th>
                    <td>
                        <asp:Button runat="server" ID="BtnSubmit" Text="保存信息" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                        <a href="WeixinKeywordList.aspx" class="btn btn-white">返回列表</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
