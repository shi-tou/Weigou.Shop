<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsType.aspx.cs" Inherits="Weigou.Manage.Goods.GoodsType" %>

 
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商品类别-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
         <div class="ibox">
            <div class="ibox-title">
                <h5>商品类别列表（<asp:Label runat="server" ID="labNav" ForeColor="Blue"></asp:Label>）</h5>
                <div class="ibox-tools">
                     <%if (CheckAuth("AddGoodsType"))
                        { %> 
                    <a id="addUrl" runat="server" href="GoodsTypeAdd.aspx" class="btn btn-danger btn-sm">
                        <i class="fa fa-plus"></i>&nbsp;添加
                    </a>
                    <%} %>
                    <a id="backUrl" runat="server" href="GoodsType.aspx" class="btn btn-success btn-sm">
                        <i class="fa fa-reply"></i>&nbsp;返回上一级
                    </a>
                    <a class="btn btn-success btn-sm collapse-link">
                        <i class="fa fa-chevron-up"></i>&nbsp;收/开
                    </a>
                </div>
            </div>
          </div> 
          <table class="table table-hover table-bordered ">
            <thead>
                <tr>
                    <th style="width: 50px">#</th>
                    <th class="hidden">ID</th>
                    <th>类别名称</th>
                    <th>描述</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repGoodsType" OnItemCommand="repGoodsType_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Eval("Name") %></td>
                            <td><%#Eval("Remark") %></td> 
                            <td>
                                <a href="GoodsType.aspx?ParentID=<%#Eval("ID") %>" class="btn btn-success btn-sm"><i class="fa fa-sitemap"></i>&nbsp;进入子类</a>
                                <%if (CheckAuth("GoodsBrandSet"))
                                  { %>
                                <a style="display: <%#Eval("ParentID").ToString()=="0"?"none":"" %>" href="GoodsBrandSet.aspx?ID=<%#Eval("ID") %>" class="btn btn-success btn-sm"><i class="fa fa-sitemap"></i>&nbsp;设置品牌</a>
                                <%} if (CheckAuth("GoodsAttributeSet"))
                                  { %>
                                <a style="display: <%#Eval("ParentID").ToString()=="0"?"none":"" %>" href="GoodsAttributeSet.aspx?ID=<%#Eval("ID") %>" class="btn btn-success btn-sm"><i class="fa fa-sitemap"></i>&nbsp;设置属性</a>
                                <%} if (CheckAuth("EditGoodsType"))
                                  { %>
                                <a href="GoodsTypeAdd.aspx?ID=<%#Eval("ID") %>&IsAdd=2" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>
                                <%} if (CheckAuth("DeleteGoodsType"))
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
