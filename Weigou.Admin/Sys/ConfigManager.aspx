<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigManager.aspx.cs" Inherits="Weigou.Admin.Sys.ConfigManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>配置管理-<%=PageTitle %></title>
    <script language="javascript" type="text/javascript">
        $(function () {
            var TabID = $("#hdTabID").val();
            $("#ConfigTab").tabs("select", parseInt(TabID));
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="s1"></asp:ScriptManager>
        <div id="ConfigTab" class="easyui-tabs">
            <div title="基本设置" data-options="iconCls:'icon-menu'" style="padding: 10px">
                <!--列表-->
                <table id="BaseTable" class="infotable">
                    <tr>
                        <td class="tr" style="width: 150px;">网站名称：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtWebName" CssClass="txt" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">标题：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtTitle" CssClass="txt" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">SEO关键词：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtKeywords" CssClass="txt" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">SEO描述：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDescription" CssClass="txt" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">LOGO：</td>
                        <td>
                            <asp:Image runat="server" ID="AvatarUrl" Width="200px" Height="200px" onerror="this.src='/Style/images/nopic.jpg'" />
                            <asp:HiddenField runat="server" ID="hfAvatar" />
                            <asp:FileUpload runat="server" ID="txtLogo" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">页面底部内容：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtFooter" CssClass="txt" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div class="action">
                    <asp:Button runat="server" ID="BtnSubmitBase" Text="保存" CssClass="btn" OnClick="BtnSubmitBase_Click" />
                    <asp:Button runat="server" ID="btnCancelBase" Text="返回" CssClass="btn" OnClientClick="javascript:history.go(-1);" />
                </div>
            </div>
            <div title="邮件设置" data-options="iconCls:'icon-menu'" style="padding: 10px;">
                <!--列表-->
                <table id="EmailTable" class="infotable">
                    <tr>
                        <td class="tr" style="width: 150px;">邮件服务器：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtSmtp" CssClass="txt" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">邮件服务器端口：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPort" CssClass="txt" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">用户名：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtUsername" CssClass="txt" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">邮件帐号：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtSysemail" CssClass="txt" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">邮件密码：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPassword" CssClass="txt" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div class="action">
                    <asp:Button runat="server" ID="BtnSubmitEmail" Text="保存" CssClass="btn" OnClick="BtnSubmitEmail_Click" />
                    <asp:Button runat="server" ID="BtnCancelEmail" Text="返回" CssClass="btn" OnClientClick="javascript:history.go(-1);" />
                </div>
            </div>
  
        </div>
        <asp:HiddenField ID="hdTabID" runat="server" />
    </form>
</body>
</html>
