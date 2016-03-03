<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAdd.aspx.cs" Inherits="Weigou.Manage.Sys.UserAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户添加-<%=PageTitle %></title>
    <script type="text/javascript" src="js/user.js"></script>
    <script type="text/javascript">
        $().ready(function () {
            InitValidate_UserAdd();
        });
    </script>
</head>
<body>
    <form id="form_UserAdd" runat="server">
        <div class="ibox">
            <div class="ibox-title">
                <h5>用户添加/编辑</h5>
            </div>
            <div class="ibox-content">
                <!--form-horizontal:水平排列的表单-->
                <table class="form form-horizontal">
                    <tr>
                        <th style="width: 150px;" class="tr">用户名：</th>
                        <td>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr runat="server" id="trPwd">
                        <th>登录密码：</th>
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>(为空则不修改密码)</td>
                    </tr>
                    <tr runat="server" id="trPwd0">
                        <th>确认密码：</th>
                        <td>
                            <asp:TextBox ID="txtPassword0" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>姓名：</th>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>停用：</th>
                        <td>
                            <asp:CheckBox ID="cbDisabled" runat="server" /></td>
                    </tr>
                    <tr>
                        <th>所属角色:</th>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlRole" CssClass="form-control"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <th></th>
                        <td>
                            <asp:Button runat="server" ID="BtnSubmit" Text="保存信息" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                            <a href="UserList.aspx" class="btn btn-white">返回列表</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
