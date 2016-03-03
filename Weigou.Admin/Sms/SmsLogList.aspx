<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsLogList.aspx.cs" Inherits="Weigou.Admin.Sys.SmsLogList" %>

<%@ Register src="../UControl/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>短信记录-<%=PageTitle %></title>
    <script src="sms.js" type="text/javascript"></script>
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script>
        $(function() {
            GetSmsLogList();
        });
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
                <th>手机号：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtMobileNo" CssClass="txt" ></asp:TextBox>
                </td>
                <th>内容：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtContent" CssClass="txt" ></asp:TextBox>
                </td>
                <th>发送时间：</th>
                <td>
                     <asp:TextBox runat="server" ID="txtMinTime" class="txt" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                     ~<asp:TextBox runat="server" ID="txtMaxTime" class="txt" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                </td>
                <td><input type="button" id="btnSearch" onclick="GetSmsLogList()" class="btn" value="查询" /></td>
            </tr>
            <tr>
                <th>发送方式：</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlSendType" CssClass="txt" >
                        <asp:ListItem Value="">-全部-</asp:ListItem>
                        <asp:ListItem Value="1">即时</asp:ListItem>
                        <asp:ListItem Value="2">定时</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>来源：</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlSource" CssClass="txt" >
                        <asp:ListItem Value="">-全部-</asp:ListItem>
                        <asp:ListItem Value="1">系统</asp:ListItem>
                        <asp:ListItem Value="2">人工</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>状态：</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlStatus" CssClass="txt" >
                        <asp:ListItem Value="">-全部-</asp:ListItem>
                        <asp:ListItem Value="0">待发送</asp:ListItem>
                        <asp:ListItem Value="1">已发送</asp:ListItem>
                    </asp:DropDownList>
                </td>
                
            </tr>
        </table>
    </div>
    <!--弹出窗-->
    <div id="win" class="easyui-window" style=" " data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
        <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
    </div>
    </form>
</body>
</html>
