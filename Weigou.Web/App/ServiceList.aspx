<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceList.aspx.cs" Inherits="Weigou.Web.App.ServiceList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .am-active{ border-color:#FF8247 !important;}
        .am-tabs-d2 .am-tabs-nav > .am-active::after {border-color:transparent transparent  #FF8247 ;}
         .am-tabs-nav > .am-active a{ text-decoration:none; color:#FF8247 !important;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="max-width: 640px; margin: 0px auto;">
        </div>
        <div data-am-widget="tabs" class="am-tabs am-tabs-d2">
            <ul class="am-tabs-nav am-cf">
                <li class="am-active"><a href="[tab1]">我是租客</a></li>
                <li class=""><a href="[data-tab-panel-1]">我是车东</a></li>
            </ul>
            <div class="am-tabs-bd">
                <!--租客-->
                <div id="tab1" class="am-tab-panel am-active">
                    <ul class="am-list am-list-static am-list-border">
                        <asp:Repeater runat="server" ID="repNewsType1">
                            <ItemTemplate>
                                <li>
                                    <a style="margin: 0px; padding: 0px; color: black;" href="ServiceDetail.aspx?type=<%#Eval("ID") %>">
                                        <i class="<%#Eval("Icon") %> am-icon-fw"></i>
                                        <%#Eval("Name") %>
                                        <span class="am-icon-angle-right am-fr"></span></a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
                <!--车东-->
                <div id="tab2" class="am-tab-panel ">
                    <ul class="am-list am-list-static am-list-border">
                        <asp:Repeater runat="server" ID="repNewsType2">
                            <ItemTemplate>
                                <li>
                                    <a style="margin: 0px; padding: 0px; color: black;" href="ServiceDetail.aspx?type=<%#Eval("ID") %>">
                                        <i class="<%#Eval("Icon") %> am-icon-fw"></i>
                                        <%#Eval("Name") %>
                                        <span class="am-icon-angle-right am-fr"></span></a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
