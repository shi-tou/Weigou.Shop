<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Weigou.Weixin.Register" %>
<%@ Register src="uc/Footer.ascx" tagname="Footer" tagprefix="uc1" %>
<%@ Register src="uc/Header.ascx" tagname="Header" tagprefix="uc2" %>
<!doctype html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <script src="js/register.js" type="text/javascript"></script>
    <script src="js/layer/layer.min.js"></script>
</head>
<body>
    <!--header-->
    <uc2:Header ID="Header1" runat="server" HeaderTitle="微车联盟-会员注册" />
    <!--注册表单-->
    <form id="form1" runat="server" class="am-form">
        <div class="am-g">
            <div class="am-u-lg-6 am-u-md-8 am-u-sm-centered">
                <img src="Style/img/login_banner.png" style="width:100%;" />
                <br />
                <label for="txtMobileNo">手机号码:</label>
                <input type="text" id="txtMobileNo" />
                <label for="txtName">真实姓名:</label>
                <input type="text" id="txtName" />
                <label for="txtPassword">设置密码:</label>
                <input type="password" id="txtPassword" />
                <label for="txtPassword0">确认密码:</label>
                <input type="password" id="txtPassword0" />
                <label for="txtValidCode">验证码:</label>
                <table>
                    <tr>
                        <td><input type="text" id="txtValidCode" style=" width:100%" required /></td>
                        <td><input id="btnSms" type="button" value="获取验证码" class="am-btn am-btn-primary" onclick="AskCheckCodeForReg()" /></td>
                    </tr>
                </table>
                <br />
                <div class="am-cf">
                    <input type="button" value="注 册" class="am-btn am-btn-primary am-btn-block" onclick="Register()"/>
                    <%--<a href="Login.aspx?backurl=<%=BackUrl %>" class="am-btn am-btn-warning am-btn-block">已注册?去登录 ^_^?</a>--%>
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hfRecommendMemberID" Value="0" />
    </form>
    <uc1:Footer ID="Footer1" runat="server" />
</body>
</html>
