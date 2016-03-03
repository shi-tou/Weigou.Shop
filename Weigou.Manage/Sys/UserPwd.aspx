<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPwd.aspx.cs" Inherits="Weigou.Manage.Sys.UserPwd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改密码-<%=PageTitle %></title>
    <script type="text/javascript" src="js/user.js"></script>
    <script type="text/javascript">
        $().ready(function () {
            InitValidate_UserPwd();
        });
    </script>
</head>
<body>
    <form id="form_UserPwd" runat="server">
        <div class="ibox">
            <div class="ibox-title">
                <h5>修改密码</h5>
            </div>
            <div class="ibox-content">
                <!--form-horizontal:水平排列的表单-->
                <table class="form form-horizontal">
                    <tr>
                        <td style="width: 150px;" class="tr">用户名：</td>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr >
                        <td class="tr">新密码：</td>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="tr">确认密码：</td>
                        <td>
                            <asp:TextBox ID="txtPassword0" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th></th>
                        <td>
                            <asp:Button runat="server" ID="BtnSubmit" Text="保存" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                            <a href="UserList.aspx" class="btn btn-white">返回列表</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
    
</body>
</html>
