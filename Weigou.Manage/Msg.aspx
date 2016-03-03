<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Msg.aspx.cs" Inherits="Weigou.Manage.Msg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Hui_3.2/css/font-awesome.min.css" rel="stylesheet" />
    <link href="Hui_3.2/css/style.min.css" rel="stylesheet" />
    <script src="Hui_3.2/js/jquery-2.1.1.min.js"></script>
    <script>
        $(function () {
            GoUrl();
        });
        var t = 0;
        function GoUrl() {
            t = t + 1;
            if (t == 3) {
                window.location.href = $('#hfUrl').val();
            }
            else {
                $('#t').html(3-t);
                setTimeout("GoUrl()", 1000);
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ibox">
            <div class="ibox-title">
                <h5>温馨提醒</h5>
            </div>
            <div class="ibox-content" style="text-align:center; font-size:20px; ">
                <i class="fa fa-bullhorn" style="color:green; font-size:30px;"></i>&nbsp;&nbsp;
                <asp:Label runat="server" ID="labMsg"></asp:Label>&nbsp;【<span id="t" style="color:blue; font-weight:bold;"></span>】&nbsp;s后系统自动跳转......
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hfUrl" />
    </form>
</body>
</html>
