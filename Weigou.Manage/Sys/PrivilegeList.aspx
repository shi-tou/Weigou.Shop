<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrivilegeList.aspx.cs" Inherits="Weigou.Manage.Sys.PrivilegeList" %>

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
                <h5>权限列表</h5>
                <div class="ibox-tools">
                    <a id="addUrl" runat="server" href="PrivilegeAdd.aspx" class="btn btn-danger btn-sm">
                        <i class="fa fa-plus"></i>&nbsp;添加
                    </a>
                    <a id="backUrl" runat="server" href="PrivilegeList.aspx" class="btn btn-success btn-sm">
                        <i class="fa fa-reply"></i>&nbsp;返回上一级
                    </a>
                    <a class="btn btn-success btn-sm collapse-link">
                        <i class="fa fa-chevron-up"></i>&nbsp;收/开
                    </a>
                </div>
            </div>
            <div class="ibox-content">
                <table class="form-group">
                    <tr>
                        <th>编码：</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtCode" class="form-control"></asp:TextBox></td>
                        <th>名称：</th>
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
                    <th>名称</th>
                    <th>编码</th>
                    <th>上一级编码</th>
                    <th>类别</th>
                    <th>链接</th>
                    <th>事件</th>
                    <th>图标</th>
                    <th>排序号</th>
                    <th>状态</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repPrivilege" OnItemCommand="repUser_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Eval("Name") %></td>
                            <td><%#Eval("Code") %></td>
                            <td><%#Eval("ParentCode") %></td>
                            <td><%#GetType(Eval("PrivilegeType").ToString()) %></td>
                            <td><%#Eval("Url") %></td>
                            <td><%#Eval("Func") %></td>
                            <td><%#Eval("Icon") %></td>
                            <td><%#Eval("Sort") %></td>
                            <td><%# Weigou.Common.EnumHelper.GetStatus(Eval("Status")) %></td>
                            <td>
                                <a href="PrivilegeList.aspx?ParentCode=<%#Eval("Code") %>" class="btn btn-success btn-sm" ><i class="fa fa-sitemap"></i>&nbsp;进入子类</a>
                                <a href="PrivilegeAdd.aspx?ID=<%#Eval("ID") %>&ParentCode=<%#Eval("ParentCode") %>" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>
                                <asp:LinkButton runat="server" ID="lbDelete" CssClass="btn btn-danger btn-sm" CommandArgument='<%#Eval("ID") %>' CommandName="Delete" OnClientClick="return confirm('确认删除操作？')"><i class="fa fa-times"></i>&nbsp;删除</asp:LinkButton>
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
