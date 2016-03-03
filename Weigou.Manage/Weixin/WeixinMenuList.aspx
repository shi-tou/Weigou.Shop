<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeixinMenuList.aspx.cs" Inherits="Weigou.Manage.Weixin.WeixinMenuList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form_MenuList" runat="server">
        <div class="ibox">
            <div class="ibox-title">
                <h5>自定义菜单列表</h5>
                <div class="ibox-tools">
                    <a id="addUrl" runat="server" href="WeixinMenuAdd.aspx" class="btn btn-success btn-sm">
                        <i class="fa fa-plus"></i>&nbsp;添加
                    </a>
                    <a id="backUrl" runat="server" href="WeixinMenuList.aspx" class="btn btn-success btn-sm">
                        <i class="fa fa-reply"></i>&nbsp;返回上一级
                    </a>
                    <a class="btn btn-success btn-sm collapse-link">
                        <i class="fa fa-chevron-up"></i>&nbsp;收/开
                    </a>
                </div>
            </div>
            <div class="ibox-content">
                <ol style="color:#2f4050 ">
                    <li>菜单设置最多为两级，一级菜单和二级菜单</li>
                    <li>一级菜单可添加最多3个，每个一级菜单下可添加最多5个子菜单</li>
                    <li>添加子菜单后，一级菜单的内容将会失效</li>
                </ol>
                <asp:Button runat="server" ID="btnSetMenu" Text="同步至微信菜单" OnClick="btnSetMenu_Click" CssClass="btn btn-success"/>
            </div>
        </div>
        <table class="table table-hover table-bordered ">
            <thead>
                <tr>
                    <th style="width: 50px; text-align:center">#</th>
                    <th class="hidden">ID</th>
                    <th>菜单名称</th>
                    <th>上级</th>
                    <th>类型</th>
                    <th>关键字</th>
                    <th>连接</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repRole" >
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center"><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Eval("Name") %></td>
                            <td><%#Convert.ToString(Eval("ParentName"))==""?"无":Convert.ToString(Eval("ParentName")) %></td>
                            <td><%#Convert.ToString(Eval("Type"))=="click"?"事件":"链接" %></td>
                            <td><%#Eval("Key") %></td>
                            <td><%#Eval("Url") %></td>
                            <td>
                                <a href="WeixinMenuList.aspx?ParentID=<%#Eval("ID") %>" class="btn btn-success btn-sm" style="display:<%#Convert.ToInt32(Eval("ParentID"))==0?"":"none" %>" ><i class="fa fa-sitemap"></i>&nbsp;进入子类</a>
                                <a href="WeixinMenuAdd.aspx?ParentID=<%#Eval("ParentID") %>&ID=<%#Eval("ID") %>" class="btn btn-danger btn-sm"><i class="fa fa-edit"></i>&nbsp;编辑</a>
                                <asp:LinkButton runat="server" ID="lbDelete" CssClass="btn btn-danger btn-sm" CommandArgument='<%#Eval("ID") %>' CommandName="Delete" OnClientClick="return confirm('确认删除操作？')"><i class="fa fa-times"></i>&nbsp;删除</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </form>
</body>
</html>
