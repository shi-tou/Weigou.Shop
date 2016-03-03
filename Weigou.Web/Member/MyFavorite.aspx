<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyFavorite.aspx.cs" Inherits="Weigou.Web.Member.MyFavorite" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="/UControl/MemHeader.ascx" TagName="MemHeader" TagPrefix="uc1" %>
<%@ Register Src="/UControl/MemMenu.ascx" TagName="MemMenu" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="s1"></asp:ScriptManager>
        <uc1:MemHeader ID="MemHeader1" runat="server" />
        <div class="container mem_container">
            <div class="row">
                <div class="col-md-2">
                    <uc2:MemMenu ID="MemMenu1" runat="server" />
                </div>
                <div class="col-md-10 mem_main">
                    <div class="panel panel-default">
                        <div class="panel-heading">我的收藏</div>
                        <div class="panel-body">
                            <div role="tabpanel">
                                <ul class="nav nav-tabs" role="tablist">
                                    <li role="presentation" class="active"><a href="#goods" aria-controls="goods" role="tab" data-toggle="tab">商品收藏</a></li>
                                    <li role="presentation"><a href="#shops" aria-controls="shops" role="tab" data-toggle="tab">店铺收藏</a></li>
                                </ul>
                                <div class="tab-content">
                                    <div role="tabpanel" class="tab-pane fade in active" id="goods">
                                        <asp:UpdatePanel runat="server" ID="u1">
                                            <ContentTemplate>
                                                <!--列表-->
                                                <table class="table table-striped">
                                                    <asp:Repeater runat="server" ID="repFavorite1" OnItemCommand="repFavorite_ItemCommand">
                                                        <HeaderTemplate>
                                                            <tr>
                                                                <th>图片</th>
                                                                <th>商品名称</th>
                                                                <th>收藏时间</th>
                                                                <th>操作</th>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <img src="<%#Eval("GoodsPic") %>" /></td>
                                                                <td><%#Eval("GoodsName") %></td>
                                                                <td><%#Eval("CreateTime") %></td>
                                                                <td>
                                                                    <asp:LinkButton runat="server" ID="lbDelete" CommandArgument='<%#Eval("ID") %>' CommandName="DeleteGoodsFav" Text="删除" OnClientClick="return confirm('确认删除操作？')"></asp:LinkButton></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                                <!--分页-->
                                                <div style="width: 500px;">
                                                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" PageSize="6" OnPageChanging="AspNetPager_PageChanging1"
                                                        ShowFirstLast="false" HorizontalAlign="Center" ShowPageIndexBox="Never" CssClass="pages" CurrentPageButtonClass="cpb" AlwaysShow="True">
                                                    </webdiyer:AspNetPager>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div role="tabpanel" class="tab-pane fade in" id="shops">
                                        <asp:UpdatePanel runat="server" ID="u2">
                                            <ContentTemplate>
                                                <!--列表-->
                                                <table class="table table-striped">
                                                    <asp:Repeater runat="server" ID="repFavorite2" OnItemCommand="repFavorite_ItemCommand">
                                                        <HeaderTemplate>
                                                            <tr>
                                                                <th>图片</th>
                                                                <th>商配额名称</th>
                                                                <th>收藏时间</th>
                                                                <th>操作</th>
                                                            </tr>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <img src="<%#Eval("MerchantPic") %>" /></td>
                                                                <td><%#Eval("MerchantName") %></td>
                                                                <td><%#Eval("CreateTime") %></td>
                                                                <td>
                                                                    <asp:LinkButton runat="server" ID="lbDelete" CommandArgument='<%#Eval("ID") %>' CommandName="DeleteMersFav" Text="删除" OnClientClick="return confirm('确认删除操作？')"></asp:LinkButton></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                                <!--分页-->
                                                <div style="width: 500px;">
                                                    <webdiyer:AspNetPager ID="AspNetPager2" runat="server" PageSize="6" OnPageChanging="AspNetPager_PageChanging2"
                                                        ShowFirstLast="false" HorizontalAlign="Center" ShowPageIndexBox="Never" CssClass="pages" CurrentPageButtonClass="cpb" AlwaysShow="True">
                                                    </webdiyer:AspNetPager>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
