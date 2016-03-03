<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrivilegeAdd.aspx.cs" Inherits="Weigou.Manage.Sys.PrivilegeAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资源添加-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ibox">
            <div class="ibox-title">
                <h5>资源添加/编辑</h5>
            </div>
            <div class="ibox-content">
                <!--form-horizontal:水平排列的表单-->
                <table class="form form-horizontal">
                    <tr>
                        <th style="width: 150px;">上一级：</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtParentCode" CssClass="form-control"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>资源编码：</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtCode" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>类型：</th>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlPrivilegeType" CssClass="form-control">
                                <asp:ListItem Value="1">模块</asp:ListItem>
                                <asp:ListItem Value="2">主窗体</asp:ListItem>
                                <asp:ListItem Value="3">工具栏</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>资源名称：</th>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <th>链接：</th>
                        <td>
                            <asp:TextBox ID="txtUrl" runat="server" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>事件：</th>
                        <td>
                            <asp:TextBox ID="txtFunc" runat="server" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>资源图标：</th>
                        <td>
                            <asp:TextBox ID="txtIcon" runat="server" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>排序：</th>
                        <td>
                            <asp:TextBox ID="txtSort" runat="server" CssClass="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>禁用：</th>
                        <td>
                            <asp:CheckBox ID="cbDisabled" runat="server" /></td>
                    </tr>
                    <tr>
                        <th></th>
                        <td>
                            <asp:Button runat="server" ID="BtnSubmit" Text="保存信息" CssClass="btn btn-success"  OnClick="BtnSubmit_Click" />
                            <a id="backUrl" runat="server" href="PrivilegeList.aspx" class="btn btn-white">返回列表</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
