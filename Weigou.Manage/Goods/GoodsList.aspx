<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsList.aspx.cs" Inherits="Weigou.Manage.Goods.GoodsList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商品列表-<%=PageTitle %></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />  
</head>
<body>
    <form id="form1" runat="server">
        <!--工具栏-->
        <div class="ibox">
            <div class="ibox-title">
                <h5>商品列表</h5>
                <div class="ibox-tools">
                   <%if(CheckAuth("AddGoods")){ %> 
                    <a href="GoodsAdd.aspx" class="btn btn-success btn-sm">
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
                    <th>商品名称：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtName" class="form-control"></asp:TextBox>
                    </td>
                    <th>审核状态：</th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStatus" class="form-control">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="0">待审核</asp:ListItem>
                            <asp:ListItem Value="1">审核通过</asp:ListItem>
                            <asp:ListItem Value="2">审核未通过</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>上架状态：</th>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlShelvesStatus" class="form-control">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="0">下架</asp:ListItem>
                            <asp:ListItem Value="1">上架</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>商品类别：</th>
                    <td>
                         <asp:DropDownList runat="server" ID="ddlType1" class="form-control"> 
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;<asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="查 询" OnClick="btnSearch_Click" /></td>
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
                    <th>商品名</th>
                    <th>商品图片</th>
                    <th>商品类别</th>
                    <th>销售价格</th>
                    <th>市场价格</th>
                    <th>商品库存</th>
                    <th>审核状态</th>
                    <th>上架状态</th>
                    <th>创建人</th>
                    <th>创建时间</th>
                    <th>审核人</th>
                    <th>审核时间</th>
                    <th style="width:280px;">操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repGoods" OnItemCommand="repGoods_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Eval("Name") %></td>
                            <td><%#FormatPic(Eval("SmallPicture")) %></td>
                            <td><%#Eval("TypeName") %></td>
                            <td><%#SetColor(Eval("SalePrice"),"red") %></td>
                            <td><%#SetColor(Eval("MarketPrice"),"red") %></td>
                            <td><%#SetColor(Eval("Stock"),"blue") %></td>
                            <td><%#GetStatus(Eval("Status")) %></td>
                            <td><%#GetShelvesStatus(Eval("ShelvesStatus")) %></td>
                            <td><%#Eval("CreateName") %></td>
                            <td><%#FormatDateTime(Eval("CreateTime")) %></td>
                            <td><%#Eval("ApprovedName") %></td>
                            <td><%#FormatDateTime(Eval("ApprovedTime")) %></td> 
                            <td>
                                <%if(CheckAuth("EditGoods")){ %> 
                                <a href="GoodsAdd.aspx?ID=<%#Eval("ID") %>" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>   
                                <%} if (CheckAuth("AuditGoods"))
                                  {  %>                             
                                <a href="GoodsAudit.aspx?ID=<%#Eval("ID") %>&TypeName=<%#Eval("TypeName") %>" class="btn btn-danger btn-sm" ><i class="fa fa-edit"></i>&nbsp;审核</a>
                                  <%} if (CheckAuth("ManagePic"))
                                  {  %>  
                                <a href="ManagePic.aspx?TargetID=<%#Eval("ID") %>&Name=<%#Eval("Name") %>&Type=1" class="btn btn-danger btn-sm" ><i class="fa fa-edit"></i>&nbsp;图片</a>
                                  <%} if (CheckAuth("DeleteGoods"))
                                  {  %>  
                                <asp:LinkButton runat="server" ID="lbDelete" CssClass="btn btn-danger btn-sm" CommandArgument='<%#Eval("ID") %>' CommandName="Delete" OnClientClick="return confirm('确认删除操作？')"><i class="fa fa-times"></i>&nbsp;删除</asp:LinkButton>
                                <%} %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <webdiyer:AspNetPager ID="AspNetPager" runat="server" PageSize="8" OnPageChanging="AspNetPager_PageChanging" CssClass="pager" CurrentPageButtonClass="cpb"
            PagingButtonSpacing="0" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页">
        </webdiyer:AspNetPager>
    </form>
</body>
</html>
