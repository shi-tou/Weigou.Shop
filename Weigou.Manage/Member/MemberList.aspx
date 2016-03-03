<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberList.aspx.cs" Inherits="Weigou.Manage.Member.MemberList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员列表-<%=PageTitle %></title>
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script> 
</head>
<body>
    <form id="form1" runat="server">
       <div class="ibox">
            <div class="ibox-title">
                <h5>会员列表</h5>
                <div class="ibox-tools"> 
                    <%if (CheckAuth("AddMember"))
                      { %> 
                    <a href="MemberAdd.aspx" class="btn btn-success btn-sm">
                        <i class="fa fa-plus"></i>&nbsp;添加
                    </a>
                    <%}%>
                    <a class="btn btn-success btn-sm collapse-link">
                        <i class="fa fa-chevron-up"></i>&nbsp;收/开
                    </a>
                </div>
            </div>  
  
         <div class="ibox-content">
                <table class="form-group">
                <tr>
                    <td>会员姓名：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtName" class="form-control" Width="100px"></asp:TextBox>
                    </td>
                    
                    <td> &nbsp;手机号码：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMobileNo" class="form-control" Width="130px"></asp:TextBox>
                    </td>
                    <td> &nbsp;会员级别：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlLevel" CssClass="form-control"> 
                        </asp:DropDownList>
                    </td>
                    <td> &nbsp;状态：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="0">冻结</asp:ListItem>
                            <asp:ListItem Value="1">启用</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td> &nbsp;会员性别：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlSex" CssClass="form-control">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="0">男</asp:ListItem>
                            <asp:ListItem Value="1">女</asp:ListItem>
                        </asp:DropDownList>
                    </td> 
                    <td> &nbsp;注册时间：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMinTime" class="form-control" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" Width="120px"></asp:TextBox>                        
                    </td>
                    <td>~</td>
                    <td><asp:TextBox runat="server" ID="txtMaxTime" class="form-control" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" Width="120px"></asp:TextBox></td>
                    <td >
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
                    <th>手机号</th>
                    <th>姓名</th>
                    <th>会员级别</th>
                    <th>性别</th>
                    <th>状态</th>
                    <th>个性签名</th>
                    <th>注册时间</th>
                    <th>创建人</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repData"  OnItemCommand="repData_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Eval("MobileNo") %></td>
                            <td><%#Eval("Name") %></td>
                            <td><%#Eval("LevelName") %></td>
                            <td><%#Eval("Sex").ToString()=="0"?"男":"女" %></td>
                            <td><%#Eval("Status").ToString()=="0"?"<span style='color:red'>冻结</span>":"<span style='color:green'>启用</span>" %></td>
                            <td><%#Eval("Signature") %></td>
                            <td><%#FormatDateTime(Eval("CreateTime")) %></td>  
                            <td><%#Eval("CreateName") %></td>
                            <td>
                                 <%if (CheckAuth("EditMember"))
                                   { %> 
                                <a href="MemberAdd.aspx?ID=<%#Eval("ID") %>" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>   
                                <%} if (CheckAuth("DeleteMember"))
                                  {  %>  
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
