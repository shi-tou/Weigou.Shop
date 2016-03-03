<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAddress.aspx.cs" Inherits="Weigou.Web.Member.MyAddress" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="/UControl/MemHeader.ascx" TagName="MemHeader" TagPrefix="uc1" %>
<%@ Register Src="/UControl/MemMenu.ascx" TagName="MemMenu" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="s1"></asp:ScriptManager>
        <uc1:MemHeader ID="MemHeader1" runat="server" />
        <div class="container mem_container">
            <div class="row">
                <div class="col-md-2">
                    <uc2:MemMenu ID="MemMenu1" runat="server" />
                </div>
                <div class="col-md-10">
                    <div class="panel panel-default">
                        <div class="panel-heading">添加收货地址</div>
                        <div class="panel-body">
                           <!--表单-->
                            <div class="form-inline">
                                <label for="">所在地区</label>
                                <asp:UpdatePanel runat="server" ID="u1">
                                    <ContentTemplate>
                                        <asp:DropDownList runat="server" ID="ddlProvince" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack="true" class="form-control" Style="width: auto">
                                            <asp:ListItem Value="">-请选择-</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList runat="server" ID="ddlCity" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="true" class="form-control" Style="width: auto">
                                            <asp:ListItem Value="">-请选择-</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList runat="server" ID="ddlDistrict" class="form-control" Style="width: auto">
                                            <asp:ListItem Value="">-请选择-</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="form-group">
                                <label for="txtZipCode">邮编：</label>
                                <asp:TextBox runat="server" ID="txtZipCode" class="form-control" Width="400px"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtAddress">街道地址：</label>
                                <asp:TextBox runat="server" ID="txtAddress" class="form-control" Width="400px"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtConsigneeName">收货人：</label>
                                <asp:TextBox runat="server" ID="txtConsigneeName" class="form-control" Width="400px"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtConsigneeMobileNo">收货人手机：</label>
                                <asp:TextBox runat="server" ID="txtConsigneeMobileNo" class="form-control" Width="400px"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="cbIsDefault">设为默认：</label>
                                <asp:CheckBox runat="server" ID="cbIsDefault" Text="默认收货地址"/>
                            </div>
                            <asp:Button runat="server" ID="btnSave" Text="新增" OnClick="btnSave_Click" class="btn btn-primary"/>

                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">收货地址列表</div>
                        <div class="panel-body">
                            <!--列表-->
                            <table class="table table-striped">
                                <asp:Repeater runat="server" ID="repDeliver" OnItemCommand="repDeliver_ItemCommand">
                                    <HeaderTemplate>
                                        <tr>
                                            <th>所在地</th>
                                            <th>邮编</th>
                                            <th>详细地址</th>
                                            <th>收货人</th>
                                            <th>收货人手机</th>
                                            <th>默认地址</th>
                                            <th>操作</th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("ProvinceName") %>&nbsp;<%#Eval("CityName") %>&nbsp;<%#Eval("DistrictName") %></td>
                                            <td><%#Eval("ZipCode") %></td>
                                            <td><%#Eval("Address") %></td>
                                            <td><%#Eval("ConsigneeName") %></td>
                                            <td><%#Eval("ConsigneeMobileNo") %></td>
                                            <td><%# Convert.ToBoolean(Eval("IsDefault"))?"<span style='color:green'>是":"否" %></td>
                                            <td>
                                                <a href="MyAddress.aspx?id=<%#Eval("ID") %>">修改</a>
                                                <asp:LinkButton runat="server" ID="btnSetDefault" CommandArgument='<%#Eval("ID") %>' CommandName="SetDefault" OnClientClick="return confirm('确认设置默认地址?')" Text="设为默认"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                                    <!--分页-->
                            <div style="width: 500px;">
                                <webdiyer:AspNetPager ID="AspNetPager" runat="server" PageSize="6" OnPageChanging="AspNetPager_PageChanging"
                                    ShowFirstLast="false" HorizontalAlign="Center" ShowPageIndexBox="Never" CssClass="pages" UrlPaging="true"
                                    CurrentPageButtonClass="cpb" AlwaysShow="True" EnableUrlRewriting="true" UrlRewritePattern="/member/address.html">
                                </webdiyer:AspNetPager>
                            </div>
                        </div>
                    </div>
                    
                </div>
            </div>
        </div>
    </form>
</body>
</html>
