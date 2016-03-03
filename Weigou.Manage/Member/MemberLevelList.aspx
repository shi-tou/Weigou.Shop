<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberLevelList.aspx.cs" Inherits="Weigou.Manage.Member.MemberLevelList" %>
  <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员级别列表-<%=PageTitle %></title>
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script> 
</head>
<body>
    <form id="form1" runat="server">
         <div class="ibox">
            <div class="ibox-title">
                <h5>用户级别列表</h5>
                <div class="ibox-tools">
                     <%if (CheckAuth("AddMemberLevel"))
                                   { %> 
                    <a href="MemberLevelAdd.aspx" class="btn btn-success btn-sm">
                        <i class="fa fa-plus"></i>&nbsp;添加
                    </a>
                    <%} %>
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
                    <th>会员级别名</th>
                    <th>所需积分</th>
                    <th>创建人</th> 
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
                            <td><%#Eval("Name") %></td>
                            <td><%#Eval("NeedScore")%></td>
                            <td><%#Eval("CreateName") %></td> 
                            <td><%#Eval("CreateTime") %></td>
                            <td>
                                
                                 <%if (CheckAuth("EditMemberLevel"))
                                   { %> 
                                <a href="MemberLevelAdd.aspx?ID=<%#Eval("ID") %>" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>
                               <%} if (CheckAuth("DeleteMemberLevel"))
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
