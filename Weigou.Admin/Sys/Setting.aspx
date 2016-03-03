<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Setting.aspx.cs" Inherits="Weigou.Admin.Sys.Setting" %>
<%@ Register src="../UControl/ToolBar.ascx" tagname="ToolBar" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link href="../JScript/layer/skin/layer.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        img{ border:none; cursor:pointer;}
    </style>
    <script src="../JScript/layer/layer.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function() {
            $("#menudiv").html($("#tb"));
            $("#tb").show();
            $('img').bind('click', function() {
                layer.tips($(this).attr('alt'), this, {
                    style: ['background-color:#0FA6D8; color:#fff', '#0FA6D8']
                });
            })
        });
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="menudiv" style=" width:99%; height:auto; background-color:#FAFAFA; border:1px solid #F0F0F0;"></div>
    <!--工具栏-->
    <div id="tb">
        <div>            
            <uc1:ToolBar ID="ToolBar1" runat="server" />            
        </div>
    </div>
    <table class="infotable" >
        <tr>
            <td class="tr" style="width:100px;">网站名称：</td>
            <td style="width:250px;"><asp:TextBox runat="server" ID="txtSiteName" CssClass="easyui-validatebox txt"  data-options="required:true" missingmessage="请输入网站名称"></asp:TextBox></td>
            <td class="tr" style="width:100px;">网站地址：</td>
            <td><asp:TextBox runat="server" ID="txtSiteUrl" CssClass="easyui-validatebox txt"  data-options="required:true" missingmessage="请输入网站地址"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tr" style="width:100px;">公司名称：</td>
            <td><asp:TextBox runat="server" ID="txtCompanyName" CssClass="easyui-validatebox txt"  data-options="required:true" missingmessage="请输入公司名称"></asp:TextBox></td>
            <td class="tr" style="width:100px;">公司地址：</td>
            <td><asp:TextBox runat="server" ID="txtAddress" CssClass="easyui-validatebox txt"  data-options="required:true" missingmessage="请输入公司地址"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tr" style="width:100px;">联系人：</td>
            <td><asp:TextBox runat="server" ID="txtContactName" CssClass="txt"></asp:TextBox></td>
            <td class="tr" style="width:100px;">联系手机：</td>
            <td><asp:TextBox runat="server" ID="txtMobileNo" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tr" style="width:100px;">联系电话：</td>
            <td><asp:TextBox runat="server" ID="txtPhoneNo" CssClass="txt"></asp:TextBox></td>
            <td class="tr" style="width:100px;">电子邮件：</td>
            <td><asp:TextBox runat="server" ID="txtEmail" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tr" style="width:100px;">传真：</td>
            <td><asp:TextBox runat="server" ID="txtFax" CssClass="txt"></asp:TextBox></td>
            <td class="tr" style="width:100px;">联系QQ：</td>
            <td><asp:TextBox runat="server" ID="txtQQ" CssClass="txt"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="tr" style="width:100px;">页面标题：</td>
            <td colspan="3"><asp:TextBox runat="server" ID="txtPageTitle" CssClass="txt" TextMode="MultiLine" Width="550px" Height="50px" MaxLength="200" Font-Size="13px"></asp:TextBox><img src="../Style/images/help.png" alt="页面标题很重要的，它在搜索引擎结果页面中的暴露程度最大。设置标题文字时要简洁、醒目，并且为了实现最佳转化，页面标题还应该是精确的总结了页面的内容。" /></td>
        </tr>
        <tr>
            <td class="tr" style="width:100px;">页面关键字：</td>
            <td colspan="3"><asp:TextBox runat="server" ID="txtPageKeyword" CssClass="txt" TextMode="MultiLine" Width="550px" Height="50px" MaxLength="200" Font-Size="13px"></asp:TextBox><img src="../Style/images/help.png" alt="关键字就是用户在使用搜索引擎时输入的、能够最大程度概括用户所要查找的信息内容的字或者词，是信息的概括化和集中化.如“法律咨询”" /></td>
        </tr>
        <tr>
            <td class="tr" style="width:100px;">页面描述</td>
            <td colspan="3"><asp:TextBox runat="server" ID="txtPageDescription" CssClass="txt" TextMode="MultiLine" Width="550px" Height="50px" MaxLength="200" Font-Size="13px"></asp:TextBox><img src="../Style/images/help.png" alt="描述是对一个网页概况的介绍，这些信息可能会出现在搜索结果中，因此需要根据网页的实际情况来设计，尽量避免与网页内容不相关的“描述”" /></td>
        </tr>
    </table>
    <div class="action">
        <asp:Button runat="server" ID="BtnSubmitP" Text="确认" CssClass="btn" OnClientClick="return $('#form1').form('validate')" OnClick="BtnSubmit_Click" />
        <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
    </div>
    </form>
</body>
</html>
