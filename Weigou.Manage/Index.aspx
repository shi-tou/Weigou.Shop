<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Weigou.Manage.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>微购屋-运营管理平台</title>
    <!--[if lt IE 8]>
    <script>
        alert('H+已不支持IE6-8，请使用谷歌、火狐等浏览器\n或360、QQ等国产浏览器的极速模式浏览本页面！');
    </script>
    <![endif]-->
</head>
<body class="fixed-sidebar full-height-layout gray-bg">
    <form id="wrapper" runat="server">
        <!--左侧导航开始-->
        <nav class="navbar-default navbar-static-side" role="navigation">
            <div class="nav-close">
                <i class="fa fa-times-circle"></i>
            </div>
            <div class="sidebar-collapse">
                <ul class="nav" id="side-menu">
                    <li class="nav-header">
                        <div class="dropdown profile-element">
                            <span>
                                <img class="img-circle" src="Style/Images/admin.png" width="64" height="64" /></span>
                            <a data-toggle="dropdown" class="dropdown-toggle" href="index.html#">
                                <span class="clear">
                                    <span class="block m-t-xs"><strong class="font-bold">石头小神</strong></span>
                                    <span class="text-muted text-xs block">超级管理员<b class="caret"></b></span>
                                </span>
                            </a>
                            <ul class="dropdown-menu animated fadeInRight m-t-xs">
                                <li><a class="J_menuItem" href="form_avatar.html">修改头像</a>
                                </li>
                                <li><a class="J_menuItem" href="profile.html">个人资料</a>
                                </li>
                                <li><a class="J_menuItem" href="contacts.html">联系我们</a>
                                </li>
                                <li><a class="J_menuItem" href="mailbox.html">信箱</a>
                                </li>
                                <li class="divider"></li>
                                <li><a href="login.html">安全退出</a>
                                </li>
                            </ul>
                        </div>
                        <div class="logo-element">H+</div>
                    </li>
                    <!--菜单-->
                    <asp:Literal runat="server" ID="litMenu"></asp:Literal>
                </ul>
            </div>
        </nav>
        <!--左侧导航结束-->
        <!--右侧部分开始-->
        <div id="page-wrapper" class="gray-bg dashbard-1">
            <div class="row border-bottom">
                <div style="margin:20px;">
                    当前时间：<span id="time"></span>
                    <script>ShowTime('time');</script>
                </div>
            </div>
            <!--菜单tab-->
            <div class="row content-tabs">
                <button class="roll-nav roll-left J_tabLeft">
                    <i class="fa fa-backward"></i>
                </button>
                <nav class="page-tabs J_menuTabs">
                    <div class="page-tabs-content">
                        <a href="javascript:;" class="active J_menuTab" data-id="index_v1.html">首页</a>
                    </div>
                </nav>
                <button class="roll-nav roll-right J_tabRight">
                    <i class="fa fa-forward"></i>
                </button>
                <div class="btn-group roll-nav roll-right">
                    <button class="dropdown J_tabClose" data-toggle="dropdown">关闭操作<span class="caret"></span></button>
                    <ul role="menu" class="dropdown-menu dropdown-menu-right">
                        <li class="J_tabShowActive"><a>定位当前选项卡</a></li>
                        <li class="divider"></li>
                        <li class="J_tabCloseAll"><a>关闭全部选项卡</a></li>
                        <li class="J_tabCloseOther"><a>关闭其他选项卡</a></li>
                    </ul>
                </div>
                <a href="login.aspx" class="roll-nav roll-right J_tabExit" style="color: #0094ff;">[退出]</a>
            </div>
            <!--主内容-->
            <div class="row J_mainContent" id="content-main">
                <iframe class="J_iframe" name="iframe0" width="100%" height="100%" src="Main.aspx" frameborder="0" data-id="Main.aspx" seamless></iframe>
            </div>
            <div class="footer">
                <div class="pull-right">
                    &copy; 2015-2016 <a href="#" target="_blank">微购屋</a>
                </div>
            </div>
        </div>
        <!--右侧部分结束-->
        
    </form>
    <!-- 全局js -->
    <script type="text/javascript" src="/Hui_3.2/js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script type="text/javascript" src="/Hui_3.2/js/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <!-- 自定义js -->
    <script type="text/javascript" src="/Hui_3.2/js/hplus.min.js?v=3.2.0"></script>
    <script type="text/javascript" src="/Hui_3.2/js/contabs.min.js"></script>
</body>
</html>
