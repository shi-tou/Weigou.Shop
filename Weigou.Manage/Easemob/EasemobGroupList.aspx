<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EasemobGroupList.aspx.cs" Inherits="Weigou.Manage.Easemob.EasemobGroupList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>环信群组管理-<%=PageTitle %></title>
    <script src="sms.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div class="ibox">
            <div class="ibox-title">
                <h5>环信群组列表</h5>
                <div class="ibox-tools">
                    <%if (CheckAuth("AddEasemobGroup"))
                      { %>
                    <a href="EasemobGroupAdd.aspx" class="btn btn-success btn-sm">
                        <i class="fa fa-plus"></i>&nbsp;添加群组
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
                <th>群名称：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtGroupName" class="form-control"></asp:TextBox>
                </td>
                <td>&nbsp;<asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="查 询" OnClick="btnSearch_Click" /></td>
            </tr>
        </table>
    </div>   
    </div>
         <table class="table table-hover table-bordered ">
            <thead>
                <tr>
                    <th style="width: 50px">#</th>
                    <th>头像</th>
                    <th>群名称</th>
                    <th>群简介</th>
                    <th>是否公开</th>
                    <th>最大人数</th>
                    <th>开启验证</th>
                    <th>群主</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repData">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td style="width:100px"><img src="<%#Eval("Photo") %>" width="80px" onerror="this.src='/Style/Images/Nopic.jpg'" /></td>
                            <td><%#Eval("GroupName") %></td>
                            <td><%#Eval("GroupDesc") %></td>
                            <td><%#Eval("IsPublic") %></td>
                            <td><%#Eval("MaxUsers") %></td>
                            <td><%#Eval("Approval") %></td>
                            <td><%#Eval("MemberName") %></td>
                            <td>
                                 <%if (CheckAuth("EasemobGroupUserList"))
                                   { %> 
                                <a href="EasemobGroupUserList.aspx?GroupID=<%#Eval("UserName") %>" class="btn btn-danger btn-sm"><i class="fa fa-search"></i>&nbsp;查看群成员</a>   
                                <%} if (CheckAuth("DeleteEasemobGroup"))
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
