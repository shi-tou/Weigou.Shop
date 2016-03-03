<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsTypeList.aspx.cs" Inherits="Weigou.Manage.News.NewsTypeList" %>
 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文章分类管理-<%=PageTitle %></title>
</head>
<body>
   <form id="form_MenuList" runat="server">
        <div class="ibox">
            <div class="ibox-title">
                <h5>文章分类列表</h5>
                <div class="ibox-tools">
                    <a id="addUrl" runat="server" href="NewsTypeAdd.aspx" class="btn btn-success btn-sm">
                        <i class="fa fa-plus"></i>&nbsp;添加
                    </a>
                    <a id="backUrl" runat="server" href="NewsTypeList.aspx" class="btn btn-success btn-sm">
                        <i class="fa fa-reply"></i>&nbsp;返回上一级
                    </a>
                    <a class="btn btn-success btn-sm collapse-link">
                        <i class="fa fa-chevron-up"></i>&nbsp;收/开
                    </a>
                </div>
            </div>
            <div class="ibox-content">
            </div>
        </div>
        <table class="table table-hover table-bordered ">
            <thead>
                <tr>
                    <th style="width: 50px; text-align:center">#</th>
                    <th class="hidden">ID</th>
                    <th>分类名称</th>
                    <th>上级</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repRole" >
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center"><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Eval("Name") %></td>
                            <td><%#Convert.ToString(Eval("ParentName"))==""?"无":Convert.ToString(Eval("ParentName")) %></td>
                            <td>
                                <a href="NewsTypeList.aspx?ParentID=<%#Eval("ID") %>" class="btn btn-success btn-sm"><i class="fa fa-sitemap"></i>&nbsp;进入子类</a>
                                <a href="NewsTypeAdd.aspx?ParentID=<%#Eval("ParentID") %>&ID=<%#Eval("ID") %>" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>
                                <asp:LinkButton runat="server" ID="lbDelete" CssClass="btn btn-danger btn-sm" CommandArgument='<%#Eval("ID") %>' CommandName="Delete" OnClientClick="return confirm('确认删除操作？')"><i class="fa fa-times"></i>&nbsp;删除</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </form>
</body>
</html>
