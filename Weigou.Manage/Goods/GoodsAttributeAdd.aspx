<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsAttributeAdd.aspx.cs" Inherits="Weigou.Manage.Goods.GoodsAttributeAdd" ValidateRequest ="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>商品类别-<%=PageTitle %></title>
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
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td style="width:100px;" class="tr">属性名称：</td>
                <td><asp:TextBox runat="server" ID="txtName"  CssClass="form-control required" title="请输入属性名称"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:100px;" class="tr">别名：</td>
                <td><asp:TextBox runat="server" ID="txtAlias"  CssClass="form-control" ></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:100px;" class="tr">排序号：</td>
                <td><asp:TextBox runat="server" ID="txtSort"  CssClass="form-control required"  title="请输入排序号"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td>
                <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                <a href="GoodsAttributeList.aspx" class="btn btn-white">返回列表</a>
                </td>
            </tr>             

        </table> 
    </div>
   </div>
    </form>
</body>
</html>
