<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BannerList.aspx.cs" Inherits="Weigou.Manage.Content.BannerList" %>
 <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Banner图管理-<%=PageTitle %></title> 
</head>
<body>
    <form id="form1" runat="server">
        <div class="ibox">
            <div class="ibox-title">
                <h5>广告Banner列表</h5>
                <div class="ibox-tools">
                    <%if (CheckAuth("AddBanner"))
                      { %>
                    <a href="BannerAdd.aspx" class="btn btn-success btn-sm">
                        <i class="fa fa-plus"></i>&nbsp;添加
                    </a>
                    <%} %>
                    <a class="btn btn-success btn-sm collapse-link">
                        <i class="fa fa-chevron-up"></i>&nbsp;收/开
                    </a>
                </div>
            </div>
            <div class="ibox-content">
                <table class="form-group">
                    <tr>
                        <th>广告Banner分类：</th>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" class="form-control"></asp:DropDownList>
                        </td>
                        <td>&nbsp;&nbsp;<asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="查 询" OnClick="btnSearch_Click" /></td>
                    </tr>
                </table>
            </div>
        </div>
        <!--列表-->
        <table class="table table-hover table-bordered ">
            <thead>
                <tr>
                    <th style="width: 50px">#</th>
                    <th class="hidden">ID</th>
                    <th>图片</th>
                    <th>分类</th>
                    <th>标题</th>
                    <th>简单描述</th>
                    <th>跳转链接</th>
                    <th>创建人</th>
                    <th>创建时间</th>
                    <th style="width: 280px;">操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repBanner" OnItemCommand="repBanner_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td>
                                <img style="width: 110px; height: 50px" src="<%#Eval("Picture") %>"></td>
                            <td><%#Eval("TypeName") %></td>
                            <td><%#Eval("Title") %></td>
                            <td><%#Eval("SimpleDesc") %></td>
                            <td><%#Eval("Url") %></td>
                            <td><%#Eval("CreateName") %></td>
                            <td><%#FormatDateTime(Eval("CreateTime")) %></td>
                            <td>
                                <%if (CheckAuth("EditBanner"))
                                  { %>
                                <a href="BannerAdd.aspx?ID=<%#Eval("ID") %>" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>
                                <%} if (CheckAuth("DeleteBanner"))
                                  { %>
                                <asp:LinkButton runat="server" ID="lbDelete" CssClass="btn btn-danger btn-sm" CommandArgument='<%#Eval("ID") %>' CommandName="Delete" OnClientClick="return confirm('确认删除操作？')"><i class="fa fa-times"></i>&nbsp;删除</asp:LinkButton>
                                <%} %>
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
