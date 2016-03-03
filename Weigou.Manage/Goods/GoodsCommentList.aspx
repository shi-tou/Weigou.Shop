<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsCommentList.aspx.cs" Inherits="Weigou.Manage.Goods.GoodsCommentList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>  

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商品评论列表-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ibox">
            <div class="ibox-title">
                <h5>商品评论列表</h5>
                <div class="ibox-tools"> 
                    <a class="btn btn-success btn-sm collapse-link">
                        <i class="fa fa-chevron-up"></i>&nbsp;收/开
                    </a>
                </div>
            </div>
           <div class="ibox-content">
            <table class="form-group">
                <tr>
                    <th>商品名称：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtName" class="form-control"></asp:TextBox>
                    </td>
                     <th>回复状态：</th>
                    <td>
                        <asp:DropDownList ID="ddlReply" Cssclass="form-control" runat="server">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="1">已回复</asp:ListItem>
                            <asp:ListItem Value="2">未回复</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>商品类别：</th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlType1" class="form-control"> 
                        </asp:DropDownList>
                        <asp:HiddenField runat="server" ID="hfTypeID" /> 
                    </td>
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
                    <th>商品名称</th>
                    <th>商品类别</th>
                    <th>商品评分</th>
                    <th>评论内容</th>
                    <th>评论人</th>                    
                    <th>评论时间</th> 
                    <th>回复状态</th>
                    <th style="width:280px;">操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repGoods" OnItemCommand="repGoods_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Eval("GoodsName") %></td>
                            <td><%#Eval("GoodsType") %></td>
                            <td><%#Eval("Star") %></td>
                            <td><%#Eval("Content") %></td>
                            <td><%#Eval("MemberName") %></td>
                            <td><%#FormatDateTime(Eval("CreateTime")) %></td>
                            <td><%#GetReplyStatus(Eval("ReplyBys").ToString()) %></td>
                            <td>   <%if (CheckAuth("ReplyGoodsComment"))
                               { %>                               
                                <a href="GoodsCommentReply.aspx?ID=<%#Eval("ID") %>" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;回复</a>   
                                <%} %>                             
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
        <asp:HiddenField runat="server" ID="hfReply" Value="0" />
    </form>
</body>
</html>
