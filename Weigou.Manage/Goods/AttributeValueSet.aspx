<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttributeValueSet.aspx.cs" Inherits="Weigou.Manage.Goods.AttributeValueSet" %>
 <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %> 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title> 
      <script>
          $(function () {
              $("#form1").validate();
          });
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <div class="ibox">
            <div class="ibox-title">
                <h5>管理商品属性值</h5>
            </div>
             <div class="ibox-content">
            <table class="form form-horizontal">
            <tr>
                <th style="width: 150px;">属性名：</th>
                <td>
                    <asp:Label runat="server" Font-Bold="true" ForeColor="Red" ID="labAttribute"></asp:Label></td>
            </tr>
            <tr>
                <th>属性值：</th>
                <td>
                    <asp:TextBox ID="txtValue" runat="server" CssClass="form-control required" title="请输入属性值"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tr" style="width: 100px;">排序号：</td>
                <td colspan="3">
                    <asp:TextBox ID="txtSort" runat="server" Width="300" Text="0" CssClass="form-control required" title="请输入排序号"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button runat="server" ID="BtnSubmit" Text="添加" CssClass="btn btn-success" OnClick="BtnSubmit_Click" /> 
                    <a href="GoodsAttributeList.aspx" class="btn btn-white">返回属性列表</a>
                </td>
            </tr>
            <asp:HiddenField runat="server" ID="hfValueID" />
        </table>
      </div>
   </div>
         <!--列表-->
        <table class="table table-hover table-bordered ">
            <thead>
                <tr>
                    <th style="width: 50px">#</th>
                    <th class="hidden">ID</th>
                    <th>属性值</th>
                    <th>排序号</th>
                    <th>创建人</th>
                    <th>创建时间</th> 
                    <th style="width:280px;">操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repGoods" OnItemCommand="repGoods_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Eval("Value") %></td>
                            <td><%#Eval("Sort") %></td>
                            <td><%#Eval("CreateName") %></td>
                            <td><%#FormatDateTime(Eval("CreateTime")) %></td>
                            <td>                                 
                                <a href="AttributeValueSet.aspx?ID=<%#Eval("ID") %>" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>                            
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

        <asp:HiddenField ID="hfAttributeID" runat="server" />
    </form>
</body>
</html>
