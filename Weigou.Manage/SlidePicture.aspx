<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SlidePicture.aspx.cs" Inherits="Weigou.Manage.SlidePicture" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>ͼƬ�ֲ��鿴</title>
    <link rel="stylesheet" href="/Style/lrtk.css?v=67567567">
</head>
<body>
    <form id="form1" runat="server">
        <!-- ���� ��ʼ -->
        <div class="slide_container" style="<%=CssStyle%>">
            <ul class="rslides" id="slider">
                <asp:Repeater ID="repList" runat="server">
                    <ItemTemplate>
                        <li>
                            <img src="<%#Eval("LargePicture") %>" alt="">
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <!-- ���� ���� -->
        <script src="/JScript/Slide/responsiveslides.min.js" type="text/javascript"></script>
        <script src="/JScript/Slide/slide.js" type="text/javascript"></script>

    </form>
</body>
</html>
