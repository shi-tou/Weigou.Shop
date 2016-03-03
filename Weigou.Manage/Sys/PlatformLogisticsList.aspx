<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlatformLogisticsList.aspx.cs" Inherits="Weigou.Manage.Sys.PlatformLogisticsList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>平台物流管理-<%=PageTitle %></title>
    <script src="js/platformlogistics.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ibox">
            <div class="ibox-title">
                <h5>运费模板列表</h5>
                <div class="ibox-tools"> 
                    <%if (CheckAuth("AddPlatformLogistics"))
                      { %> 
                    <a href="PlatformLogisticsAdd.aspx" class="btn btn-success btn-sm">
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
                    <th>运费模版名称</th>
                    <th>快递公司</th>
                    <th>默认运费</th>
                    <th>特殊省份</th>
                    <th>指定地区运费</th>
                    <th>是否默认模板</th>
                    <th>备注</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repData"  OnItemCommand="repData_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Eval("Name") %></td>
                            <td><%#Eval("LogisticsName") %></td>
                            <td><%#Eval("DefaultPrice") %></td>
                            <td><%#Eval("ProvinceName") %></td>
                            <td><%#Eval("LogisticsPrice") %></td>
                            <td><%#Eval("IsDefault").ToString()=="False"?"非默认":"默认" %></td>
                            <td><%#Eval("Remark") %></td>
                            <td>
                                 <%if (CheckAuth("EditPlatformLogistics"))
                                   { %> 
                                <a href="PlatformLogisticsAdd.aspx?ID=<%#Eval("ID") %>" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>   
                                <%} if (CheckAuth("DeletePlatformLogistics"))
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
