<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsType.aspx.cs" Inherits="Weigou.Admin.Goods.GoodsType" %>

<%@ Register Src="../UControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商品类别-<%=PageTitle %></title>
    <script src="js/goodstype.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="easyui-panel p10" data-options="title:'商品类别',iconCls:'icon-tip'">
            <uc1:ToolBar ID="ToolBar1" runat="server" />
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
                    <%--<td style="padding-left: 5px; vertical-align: top">
                        <table id="ListTable2">
                        </table>
                    </td>--%>
                </tr>
            </table>
        </div>

        <!--弹出窗-->
        <div id="win" class="easyui-window" data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
            <iframe id="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
        </div>

        <input type="hidden" id="hdCurrentID" />
        <input type="hidden" id="hdCurrentIndex" />
        <!--列表当前选中行的索引以及ID-->
        <input type="hidden" id="hdCurRowIndex_1" />
        <input type="hidden" id="hdCurRowID_1" />
        <input type="hidden" id="hdCurRowIndex_2" />
        <input type="hidden" id="hdCurRowID_2" />
        <%--<input type="hidden" id="hdCurRowIndex_3" />
        <input type="hidden" id="hdCurRowID_3" />--%>
        <input type="hidden" id="hdIsAdd" />
    </form>
</body>
</html>
