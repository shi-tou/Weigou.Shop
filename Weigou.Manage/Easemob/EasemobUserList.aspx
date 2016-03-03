<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EasemobUserList.aspx.cs" Inherits="Weigou.Manage.Easemob.EasemobUserList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>环信用户管理-<%=PageTitle %></title>
    <script src="sms.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div class="ibox">
            <div class="ibox-title">
                <h5>环信用户列表</h5>
                <div class="ibox-tools"> 
                    <a class="btn btn-success btn-sm collapse-link">
                        <i class="fa fa-chevron-up"></i>&nbsp;收/开
                    </a>
                </div>
            </div>
           <div class="ibox-content">
                <table class="form-group">
            <tr>
                <th>姓名：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtName" class="form-control"></asp:TextBox>
                </td>
                <th>手机号：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtMobileNo" class="form-control"></asp:TextBox>
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
                    <th>环信用户名</th>
                    <th>姓名</th>
                    <th>呢称</th>
                    <th>手机号</th>
                    <th>性别</th>
                    <th>注册时间</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repData">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td style="width:100px"><img src="<%#Eval("Photo") %>" width="80px" onerror="this.src='/Style/Images/Nopic.jpg'" /></td>
                            <td><%#Eval("UserName") %></td>
                            <td><%#Eval("Name") %></td>
                            <td><%#Eval("NickName") %></td>
                            <td><%#Eval("MobileNo") %></td>
                            <td><%#Eval("CreateTime") %></td>
                            <td>
                                 <%if (CheckAuth("EasemobGroupList"))
                                   { %> 
                                <a href="EasemobGroupList.aspx?UserName=<%#Eval("UserName") %>" class="btn btn-danger btn-sm"><i class="fa fa-search"></i>&nbsp;查看群</a>   
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
