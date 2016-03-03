<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyComment.aspx.cs" Inherits="Weigou.Web.Member.MyComment" %>

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
                        <div class="panel-heading">我的评论</div>
                        <div class="panel-body">
                            <asp:UpdatePanel runat="server" ID="u1">
                                <ContentTemplate>
                                    <!--列表-->
                                    <table class="table table-striped">
                                        <asp:Repeater runat="server" ID="repComments">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th></th>
                                                    <th>商品名称</th>
                                                    <th>商家名称</th>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <div>评价：<%#Eval("Content") %>[<%#Eval("CreateTime") %>]</div>
                                                        <div style="color:#999;<%#Convert.ToString(Eval("AppendContent"))!=""?"display:block":"display:none" %>">
                                                            追加评价：<%#Eval("AppendContent") %>[<%#Eval("AppendTime") %>]
                                                        </div>
                                                        <div style="color:#b28500;<%#Convert.ToString(Eval("ReplyContent"))!=""?"display:block":"display:none" %>">
                                                            商家回复：<%#Eval("ReplyContent") %>[<%#Eval("ReplyTime") %>]
                                                        </div>
                                                    </td>
                                                    <td><%#Eval("GoodsName") %></td>
                                                    <td><%#Eval("MerchantName") %></td>

                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    <!--分页-->
                                    <div style="width: 500px;">
                                        <webdiyer:AspNetPager ID="AspNetPager" runat="server" PageSize="6" OnPageChanging="AspNetPager_PageChanging"
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
    </form>
</body>
</html>
