<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="Weigou.Admin.main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=PageTitle %></title>
    <style>
        #nav {
            list-style: none;
            margin: 0px;
            padding: 0px;
        }
        #nav li {
            border-bottom: solid 1px #ccc;
            margin: 0px;
            padding: 0px;
        }
        #nav li a {
            display: block;
            font-family: "Hiragino Sans GB","WenQuanYi Micro Hei","微软雅黑",Arial;
            font-size: 14px;
            font-weight: bold;
            padding: 12px 0px 12px 30px;
            text-decoration: none;
            color: #333;
            cursor: pointer;
        }
        #nav ul li a.current
        {
            background: #71A416;
            color: #fff;
        }
        #nav li a:hover
        {
            color: #fff;
            background: #63BAEA;
        }
        .panel-title
        {
            padding-left: 20px;
        }
    </style>
    <link href="/JScript/KindEditor/plugins/code/prettify.css" rel="stylesheet" type="text/css" />
    <script src="/JScript/KindEditor/kindeditor.js" type="text/javascript"></script>
    <script src="/JScript/KindEditor/lang/zh_CN.js" type="text/javascript"></script>
    <script src="/JScript/KindEditor/plugins/code/prettify.js" type="text/javascript"></script>
    <script src="/JScript/menu.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            ShowTime("time");
            initTabEvent();
            InitMenuEven();
                 
            setInterval(SetTotalCount(), 5*1*1000);                      
        });
        
        function SetTotalCount()
        {
            var query = { 'action': 'GetMallRemindSendCount' };                           
            $.ajax({
                url: dealAjaxUrl('/Ajax/Order.ashx'),
                data: query,
                dataType: 'json',
                type: 'POST',
                success: function (d) {                       
                    $("#spTotalCount").text(d.res);                       
                }
            });  
        }

        function ResetPassword() {
            OpenWin('修改密码', 400, 220, '/ResetPassword.aspx');
        }
    </script>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
        <!--顶部-->
        <div data-options="region:'north',border:false,split:false" style="height: 70px; background: #4593b9; padding: 10px;">
            <div class="headL">
                <h1 style="float: left;">微车联盟平台数据管理系统</h1>
                <span style="float: right;"></span></div>
            <div class="headR"><a href="javascript:void(0)" onclick="add('/Order/MallRemindSendList.aspx',this)" style="margin-right:25px;">订单提醒发货列表【<span id="spTotalCount" style="margin-right:0px;">0</span>】</a> 当前时间：<span id="time"></span><a href="javascript:ResetPassword();"><span style="color: yellow">修改密码</span></a><a href="LoginOut.aspx"><span style="color: yellow" >退出登录</span></a></div>
        </div>
        <!--左侧菜单-->
        <div data-options="region:'west',split:true,title:'系统菜单',iconCls:'icon-menuflag'" style="width: 150px;">
            <div class="easyui-accordion" data-options="fit:true,border:false">
                <%=StrMenu%>
            </div>
        </div>
        <!--内容-->
        <div data-options="region:'center',title:'',iconCls:'icon-tip'">
            <div id="nav_tabs" class="easyui-tabs" data-options="fit:true,border:false">
                <div title="首页" style="padding: 10px">
                    <div class="easyui-panel p10"  data-options="title:'登录信息',iconCls:'icon-tip'" style="height:80px;">
                        欢迎您，<span class="blue"> <%=UserInfo!=null?UserInfo.Name:"" %></span>！您最后一次登录的时间：<%=UserInfo!=null?UserInfo.LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss"):"" %>
                    </div>
                </div>
            </div>
        </div>
        <!--底部-->
        <div data-options="region:'south',border:false" style="line-height: 22px; background: #f2f2f2; text-align: center;">
            Power by 【移商网】
        </div>
        <!--弹出窗-->
        <div id="win" class="easyui-window" data-options="iconCls:'icon-save',top:'250px',closed:true,minimizable:false,maximizable:false,collapsible:false">
            <iframe id="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
        </div>
        <!--菜单-->
        <div id="mm" class="easyui-menu" style="width: 150px;">
            <div id="tabupdate" onclick="closeTab('refresh')"> 刷新</div>
            <div class="menu-sep"></div>
            <div id="close"  onclick="closeTab('close')">关闭</div>
            <div id="closeall">全部关闭</div>
            <div id="closeother">除此之外全部关闭</div>
            <div class="menu-sep"></div>
            <div id="closeright">当前页右侧全部关闭</div>
            <div id="closeleft">当前页左侧全部关闭</div>
            <div class="menu-sep"> </div>
            <div id="exit">退出</div>
        </div>
    </form>
</body>
</html>
