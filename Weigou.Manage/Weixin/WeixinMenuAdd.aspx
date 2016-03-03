<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeixinMenuAdd.aspx.cs" Inherits="Weigou.Manage.Weixin.WeixinMenuAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>自定义微信菜单-<%=PageTitle %></title>
    <script type="text/javascript">
        $(function () {
            selType();
            ValidForm();
        });
        function selType() {
            var t = $('#ddlType').val();
            $('#trKey').hide();
            $('#trUrl').hide();
            if (t == 'click') {
                $('#trKey').show();
            }
            else {
                $('#trUrl').show();
            }
        }

        function ValidForm() {
            //初始化验证表单
            $("#form1").validate({
                rules: {
                    txtName: "required"
                },
                messages: {
                    txtName: "请输入菜单名称"
                }
            });
        }
    </script>
</head>
<body>
    <form id="form_WeixinMenuAdd" runat="server">
    <div class="ibox">
        <div class="ibox-title">
            <h5>自定义微信菜单添加/编辑</h5>
        </div>
        <div class="ibox-content">
            <!--form-horizontal:水平排列的表单-->
            <table class="form form-horizontal">
                <tr>
                    <th style="width: 150px;">上一级：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtParentName" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>菜单名称：</th>
                    <td><asp:TextBox ID="txtName" runat="server" CssClass="form-control" title="请输入菜单名称"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>菜单类别</th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlType" onchange="selType()"  CssClass="form-control">
                            <asp:ListItem Value="click">事件</asp:ListItem>
                            <asp:ListItem Value="view">链接</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trKey">
                    <th>事件关键词：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtKey" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trUrl">
                    <th>链接地址：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtUrl" CssClass="form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th></th>
                    <td>
                        <asp:Button runat="server" ID="BtnSubmit" Text="保存信息" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                        <a href="WeixinMenuList.aspx" class="btn btn-white">返回列表</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
