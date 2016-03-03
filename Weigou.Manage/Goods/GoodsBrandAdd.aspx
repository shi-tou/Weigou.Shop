<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsBrandAdd.aspx.cs" Inherits="Weigou.Manage.Goods.GoodsBrandAdd" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加品牌-<%=PageTitle %></title>
    <script>
        $(function () {
            $("#form1").validate(); 
        });        
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <div class="ibox">
            <div class="ibox-title">
                <h5>商品属性添加/编辑</h5>
            </div>
             <div class="ibox-content">
            <table class="form form-horizontal">
                <tr>
                    <td style="width: 150px;" class="tr">品牌名称：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtName" CssClass="form-control required" title="请输入品牌名称"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="tr">品牌LOGO：</td>
                    <td>
                        <asp:FileUpload runat="server" ID="txtLogo" /></td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Image ImageAlign="AbsMiddle" runat="server" ID="AvatarUrl" Width="200px" Height="125px" onerror="this.src='/Style/images/nopic.jpg'" />
                        <asp:HiddenField runat="server" ID="hfAvatar" />
                    </td>
                </tr>
            <tr>
                <td></td>
                <td>
                <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                <a href="GoodsBrandList.aspx" class="btn btn-white">返回列表</a>
                </td>
            </tr>  

            </table> 
           </div>
        </div>
    </form>
</body>
</html>
