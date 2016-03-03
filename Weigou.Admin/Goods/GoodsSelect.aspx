<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsSelect.aspx.cs" Inherits="Weigou.Admin.Goods.GoodsSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商品列表-<%=PageTitle %></title>
    <script src="js/goodsSelect.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        //选择商品分类
        function SelectMer() {
            OpenWin('商品分类列表<span style="color:red">(双击选择商品分类)</span>', 500, 350, '/Goods/GoodsTypeSelect.aspx?IDControl=hfTypeID&NameControl=txtTypeName');
            return;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!--列表-->
        <table id="ListTable" data-options="toolbar:'#tb',iconCls:'icon-tip'">
        </table>
        <!--工具栏-->
        <div id="tb">
            <table>
                <tr>
                    <th>商品名称：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtName" class="txt"></asp:TextBox>
                    </td>
                    <th>商品类别：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtTypeName" data-options="required:true" missingmessage="请选择商品分类" CssClass="easyui-validatebox txt" Enabled="false"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hfTypeID" />
                        <a href="javascript:void(0);" onclick="SelectMer()">选择商品类别</a>
                    </td>
                    <td>
                        <input type="button" id="btnSearch" onclick="GetList()" class="btn" value="查询" />
                        <input type="button" id="btnBatchAdd" onclick="BatchAdd()" class="btn" value="批量加入" />
                    </td>
                </tr>
            </table>
        </div>
        <!--弹出窗-->
        <div id="win" class="easyui-window" data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
            <iframe id="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
        </div>
        <asp:HiddenField runat="server" ID="hfStatus" />
        <asp:HiddenField runat="server" ID="hfShelvesStatus" />
    </form>
</body>
</html>
