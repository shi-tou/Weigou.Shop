<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaManage.aspx.cs" Inherits="Weigou.Manage.Sys.AreaManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>地区数据管理-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager runat="server" ID="s1"></asp:ScriptManager>
        <div class="ibox">
            <ul class="nav nav-tabs">
                <li class="<%=TabIndex==1?"active":"" %>"><a data-toggle="tab" href="AreaManage.aspx#tab-1" aria-expanded="<%=TabIndex==1?"true":"false" %>">省管理 </a>
                </li>
                <li class="<%=TabIndex==2?"active":"" %>"><a data-toggle="tab" href="AreaManage.aspx#tab-2" aria-expanded="<%=TabIndex==2?"true":"false" %>">市管理</a>
                </li>
                <li class="<%=TabIndex==3?"active":"" %>"><a data-toggle="tab" href="AreaManage.aspx#tab-3" aria-expanded="<%=TabIndex==3?"true":"false" %>">区管理</a>
                </li>
            </ul>
            <div class="tab-content">
                <div id="tab-1" class="tab-pane <%=TabIndex==1?"active":"" %>">
                    <div class="panel-body">
                        <div class="ibox">
                            <div class="ibox-title">
                                <h5>省份列表</h5>
                                <div class="ibox-tools">
                                    <%if (CheckAuth("AddProvice"))
                                      { %>
                                    <a href="AreaAdd.aspx?type=1" class="btn btn-success btn-sm">
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
                                    <th>省名称</th>
                                    <th>拼音</th>
                                    <th>首字母</th>
                                    <th>简称</th>
                                    <th style="width: 280px;">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="repOrder">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Container.ItemIndex+1 %></td>
                                            <td><%#Eval("ProvinceName") %></td>
                                            <td><%#Eval("Spell") %></td>
                                            <td><%#Eval("FirstLetter") %></td>
                                            <td><%#Eval("ShortName") %></td>
                                            <td>
                                                <%if (CheckAuth("EditProvice"))
                                                  { %>
                                                <a href="AreaAdd.aspx?ID=<%#Eval("ID") %>&Type=1" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>
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
                    </div>
                </div>
                <div id="tab-2" class="tab-pane <%=TabIndex==2?"active":"" %>">
                    <div class="panel-body">
                        <div class="ibox">
                            <div class="ibox-title">
                                <h5>城市列表</h5>
                                <div class="ibox-tools">
                                    <%if (CheckAuth("AddCity"))
                                      { %>
                                    <a href="AreaAdd.aspx?type=2" class="btn btn-success btn-sm">
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

                                        <th>省份：</th>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlProvice_City" class="form-control" Width="150"></asp:DropDownList>
                                        </td>
                                        <td>&nbsp;
                                    <asp:Button runat="server" ID="btnSearch_City" CssClass="btn btn-primary" Text="查 询" OnClick="btnSearch_City_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <table class="table table-hover table-bordered ">
                            <thead>
                                <tr>
                                    <th style="width: 50px">#</th>
                                    <th>市名称</th>
                                    <th>拼音</th>
                                    <th>首字母</th>
                                    <th>邮编</th>
                                    <th>所在省</th>
                                    <th style="width: 280px;">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="repOrderWx">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Container.ItemIndex+1 %></td>
                                            <td><%#Eval("CityName") %></td>
                                            <td><%#Eval("Spell") %></td>
                                            <td><%#Eval("FirstLetter") %></td>
                                            <td><%#Eval("ZipCode") %></td>
                                            <td><%#Eval("ProvinceName") %></td>
                                            <td>
                                                <%if (CheckAuth("EditCity"))
                                                  { %>
                                                <a href="AreaAdd.aspx?ID=<%#Eval("ID") %>&Type=2" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>
                                                <%} %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <webdiyer:AspNetPager ID="AspNetPagerCity" runat="server" PageSize="10" OnPageChanging="AspNetPagerCity_PageChanging" CssClass="pager" CurrentPageButtonClass="cpb"
                            PagingButtonSpacing="0" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
                <div id="tab-3" class="tab-pane <%=TabIndex==3?"active":"" %>">
                    <div class="panel-body">
                        <div class="ibox">
                            <div class="ibox-title">
                                <h5>区列表</h5>
                                <div class="ibox-tools">
                                    <%if (CheckAuth("AddDistrict") )
                                      { %>
                                    <a href="AreaAdd.aspx?type=3" class="btn btn-success btn-sm">
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
                                        <th>省份：<br />
                                            <br />
                                            <br />
                                            城市：</th>
                                        <td class="inline">
                                            <asp:UpdatePanel runat="server" ID="u1">
                                                <ContentTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlProvice_District" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" Width="150px">
                                                        <asp:ListItem Value="">-选择省份-</asp:ListItem>
                                                    </asp:DropDownList><br />
                                                    <asp:DropDownList runat="server" ID="ddlCity" CssClass="form-control" Width="150px">
                                                        <asp:ListItem Value="">-选择城市-</asp:ListItem>
                                                    </asp:DropDownList>

                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                        </td>

                                        <td>&nbsp;
                                    <asp:Button runat="server" ID="btnSearch_District" CssClass="btn btn-primary" Text="查 询" OnClick="btnSearch_District_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <table class="table table-hover table-bordered ">
                            <thead>
                                <tr>
                                    <th style="width: 50px">#</th>
                                    <th>区域名称</th>
                                    <th>所在市</th>
                                    <th>所在省</th>
                                    <th style="width: 280px;">操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="repDistrict">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Container.ItemIndex+1 %></td>
                                            <td><%#Eval("DistrictName") %></td>
                                            <td><%#Eval("CityName") %></td>
                                            <td><%#Eval("ProvinceName") %></td>
                                            <td>
                                                <%if (CheckAuth("EditArea"))
                                                  { %>
                                                <a href="AreaAdd.aspx?ID=<%#Eval("ID") %>&Type=3" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>
                                                <%} %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <webdiyer:AspNetPager ID="AspNetPagerDistrict" runat="server" PageSize="10" OnPageChanging="AspNetPagerDistrict_PageChanging" CssClass="pager" CurrentPageButtonClass="cpb"
                            PagingButtonSpacing="0" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页">
                        </webdiyer:AspNetPager>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
