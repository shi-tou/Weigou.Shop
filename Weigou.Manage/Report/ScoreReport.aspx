<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScoreReport.aspx.cs" Inherits="Weigou.Manage.Report.ScoreReport" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="report.js" type="text/javascript"></script>
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            GetScoreReport();
        });
        function Export() {
            ExportScoreReport();
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
            <div>
                <uc1:ToolBar ID="ToolBar1" runat="server" />
            </div>
            <table>
                <tr>
                    <td>会员账户：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMemberName" class="txt"></asp:TextBox>
                    </td>
                    <td>商户名称：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMerchantName" class="txt"></asp:TextBox>
                    </td>
                    <td></td>
                    <td>
                        
                    </td>
                    <td>积分来源：</td>
                    <td>
                        <asp:DropDownList ID="ddlScoreType" runat="server" CssClass="txt">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="3">会员金币积分</asp:ListItem>
                            <asp:ListItem Value="4">会员赠品积分</asp:ListItem>
                            <asp:ListItem Value="5">会员娱乐积分</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>变动时间：</td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtMinTime" class="txt" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                        ~<asp:TextBox runat="server" ID="txtMaxTime" class="txt" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                    </td>
                    <td colspan="6">
                        <input type="button" id="btnSearch" onclick="GetScoreReport()" class="btn" value="查询" /></td>
                </tr>
            </table>
        </div>
        <!--弹出窗-->
        <div id="win" class="easyui-window" data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
            <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
        </div>
    </form>
</body>
</html>
