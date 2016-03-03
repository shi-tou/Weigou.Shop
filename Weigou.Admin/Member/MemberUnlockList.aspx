<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberUnlockList.aspx.cs" Inherits="Weigou.Admin.Member.MemberUnlockList" %>
<%@ Register src="../UControl/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title> 会员解冻列表-<%=PageTitle %></title>
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="js/memberunlocklist.js" type="text/javascript"></script>
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
                 <th>会员姓名：</th>
                <td >
                    <asp:TextBox runat="server" ID="txtMemberName" class="txt"></asp:TextBox>
                </td>
                <th>手机号码：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtMobileNo" class="txt"></asp:TextBox>
                </td>
                <th>状态：</th>
                <td>
                    <asp:DropDownList runat="server" ID="ddlStatus">
                        <asp:ListItem Value="">-全部-</asp:ListItem>
                        <asp:ListItem Value="0">-待审核-</asp:ListItem>
                        <asp:ListItem Value="1">-审核通过-</asp:ListItem>
                        <asp:ListItem Value="2">-审核不通过-</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th>申请时间：</th>
                <td>
                   <asp:TextBox runat="server" ID="txtMinTime" class="txt" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                   ~<asp:TextBox runat="server" ID="txtMaxTime" class="txt" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                </td>
                <td><input type="button" id="btnSearch" onclick="GetList()" class="btn" value="查询" /></td>
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
