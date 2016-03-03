<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigManager.aspx.cs" Inherits="Weigou.Manage.Sys.ConfigManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
            <div class="tab-content">
                <ul class="nav nav-tabs">
                    <li class="<%=TabIndex==1?"active":"" %>"><a data-toggle="tab" href="AreaManage.aspx#tab-1" aria-expanded="<%=TabIndex==1?"true":"false" %>">基本设置 </a>
                    </li>
                    <li class="<%=TabIndex==2?"active":"" %>"><a data-toggle="tab" href="AreaManage.aspx#tab-2" aria-expanded="<%=TabIndex==2?"true":"false" %>">邮箱管理</a>
                    </li> 
                </ul>
                <div id="tab-1" class="tab-pane <%=TabIndex==1?"active":"" %>">
                    <div class="panel-body"> 
                     <table id="BaseTable" class="form form-horizontal">
                    <tr>
                        <td class="tr" style="width: 150px;">网站名称：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtWebName" CssClass="form-control" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">标题：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">SEO关键词：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtKeywords" CssClass="form-control" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">SEO描述：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" Width="300"></asp:TextBox>
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
                            <asp:TextBox runat="server" ID="txtFooter" CssClass="form-control" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                         <tr>
                             <td></td>
                             <td>
                                  <asp:Button runat="server" ID="BtnSubmitBase" Text="保存" CssClass="btn btn-success" OnClick="BtnSubmitBase_Click" />
                    <asp:Button runat="server" ID="btnCancelBase" Text="返回" CssClass="btn btn-white" OnClientClick="javascript:history.go(-1);" />
                             </td>
                         </tr>
                </table> 
                </div>
                </div>
                <div id="tab-2" class="tab-pane <%=TabIndex==2?"active":"" %>">
                    <div class="panel-body">
                        <table id="EmailTable" class="form form-horizontal">
                    <tr>
                        <td class="tr" style="width: 150px;">邮件服务器：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtSmtp" CssClass="form-control" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">邮件服务器端口：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPort" CssClass="form-control" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">用户名：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">邮件帐号：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtSysemail" CssClass="form-control" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr">邮件密码：</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" Width="300"></asp:TextBox>
                        </td>
                    </tr>
                            <tr>
                             <td></td>
                             <td> <asp:Button runat="server" ID="BtnSubmitEmail" Text="保存" CssClass="btn btn-success" OnClick="BtnSubmitEmail_Click" />
                    <asp:Button runat="server" ID="BtnCancelEmail" Text="返回" CssClass="btn btn-white" OnClientClick="javascript:history.go(-1);" />

                             </td>
                            </tr>
                </table> 
                    </div>
                </div>
        </div>
         
    </form>
</body>
</html>
