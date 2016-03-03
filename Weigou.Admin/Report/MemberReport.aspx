<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberReport.aspx.cs" Inherits="Weigou.Admin.Report.MemberReport" %>

<%@ Register Src="../UControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="report.js" type="text/javascript"></script>
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            GetMemberReport();
        });
        function Export() {
            ExportMemberReport();
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
                    <td>会员姓名：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtName" class="txt"></asp:TextBox>
                    </td>
                    <td>邮箱：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEmail" class="txt"></asp:TextBox>
                    </td>
                    <td>手机号码：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMobileNo" class="txt"></asp:TextBox>
                    </td>
                    <td>状态：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="txt">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="0">冻结</asp:ListItem>
                            <asp:ListItem Value="1">启用</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>会员性别：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlSex" CssClass="txt">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="0">男</asp:ListItem>
                            <asp:ListItem Value="1">女</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>注册时间：</td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtMinTime" class="txt" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                        ~<asp:TextBox runat="server" ID="txtMaxTime" class="txt" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                    </td>
                    <td colspan="6">
                        <input type="button" id="btnSearch" onclick="GetMemberReport()" class="btn" value="查询" /></td>
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
