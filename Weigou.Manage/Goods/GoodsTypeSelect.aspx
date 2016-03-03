<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsTypeSelect.aspx.cs" Inherits="Weigou.Manage.Goods.GoodsTypeSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商品类别-<%=PageTitle %></title>
    <script src="js/goodstypeselect.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">

        <!--列表-->
        <table>
            <tr>
                <td style="vertical-align: top">
                    <table id="ListTable">
                    </table>
                </td>
                <td style="padding-left: 5px; vertical-align: top">
                    <table id="ListTable1">
                    </table>
                </td>
               <%-- <td style="padding-left: 5px; vertical-align: top">
                    <table id="ListTable2">
                    </table>
                </td>--%>
            </tr>
        </table>


        <!--弹出窗-->
        <div id="win" class="easyui-window" data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
            <iframe id="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
        </div>

        <input type="hidden" id="hdTypeID" />
        <input type="hidden" id="hdTypeName" />
        <asp:HiddenField ID="hfIDControl" runat="server" />
        <asp:HiddenField ID="hfNameControl" runat="server" />
    </form>
</body>
</html>
