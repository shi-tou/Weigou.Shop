<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Weigou.Web.Member.Main" %>
<%@ Register src="/UControl/MemHeader.ascx" tagname="MemHeader" tagprefix="uc1" %>
<%@ Register src="/UControl/MemMenu.ascx" tagname="MemMenu" tagprefix="uc2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:MemHeader ID="MemHeader1" runat="server" />
        <div class="container mem_container">
            <div class="row">
                <div class="col-md-3">
                    <uc2:MemMenu ID="MemMenu1" runat="server" />
                </div>
                <div class="col-md-9"></div>
            </div>
        </div>
    </form>
</body>
</html>
