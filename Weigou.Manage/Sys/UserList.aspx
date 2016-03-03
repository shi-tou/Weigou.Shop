﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Weigou.Manage.Sys.UserList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form_UserList" runat="server">
        <div class="ibox">
            <div class="ibox-title">
                <h5>用户列表</h5>
                <div class="ibox-tools">
                    <a href="UserAdd.aspx" class="btn btn-success btn-sm">
                        <i class="fa fa-plus"></i>&nbsp;添加
                    </a>
                    <a class="btn btn-success btn-sm collapse-link">
                        <i class="fa fa-chevron-up"></i>&nbsp;收/开
                    </a>
                </div>
            </div>
            <div class="ibox-content">
                <table class="form-group">
                    <tr>
                        <th>用户名：</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtUserName" class="form-control"></asp:TextBox></td>
                        <th>姓名：</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtName" class="form-control"></asp:TextBox></td>
                        <td>
                            &nbsp;<asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="查 询" OnClick="btnSearch_Click" /></td>
                    </tr>
                </table>
            </div>
        </div>
        <table class="table table-hover table-bordered ">
            <thead>
                <tr>
                    <th style="width: 50px">#</th>
                    <th class="hidden">ID</th>
                    <th>用户名</th>
                    <th>姓名</th>
                    <th>所属角色</th>
                    <th>状态</th>
                    <th>创建时间</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repUser" OnItemCommand="repUser_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Eval("UserName") %></td>
                            <td><%#Eval("Name") %></td>
                            <td><%#Eval("RoleName") %></td>
                            <td><i class="fa <%#Eval("Status").ToString()=="1"?"fa-check green":"fa-times red" %>"></i></td>
                            <td><%#Eval("CreateTime") %></td>
                            <td>
                                <a href="UserAdd.aspx?ID=<%#Eval("ID") %>" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>
                                <asp:LinkButton runat="server" ID="lbDelete" CssClass="btn btn-danger btn-sm" CommandArgument='<%#Eval("ID") %>' CommandName="Delete" OnClientClick="return confirm('确认删除操作？')"><i class="fa fa-times"></i>&nbsp;删除</asp:LinkButton>
                                <a href="UserPwd.aspx?ID=<%#Eval("ID") %>" class="btn btn-danger btn-sm" ><i class="fa fa-lock"></i>&nbsp;修改密码</a>
                                
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <webdiyer:AspNetPager ID="AspNetPager" runat="server" PageSize="10" OnPageChanging="AspNetPager_PageChanging" CssClass="pager" CurrentPageButtonClass="cpb"
            PagingButtonSpacing="0" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页">
        </webdiyer:AspNetPager>
    </form>
</body>
</html>
