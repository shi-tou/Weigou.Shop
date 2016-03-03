<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsTypeAdd.aspx.cs" Inherits="Weigou.Manage.Goods.GoodsTypeAdd" ValidateRequest ="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>商品类别-<%=PageTitle %></title>
    <script type="text/javascript">
        $(function(){
            $("#form1").validate();
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
     <div class="ibox">
            <div class="ibox-title">
                <h5>商品分类添加/编辑</h5>
            </div>
             <div class="ibox-content">
            <table class="form form-horizontal">
            <tr>
                <td style="width:150px;" class="tr">类别名称：</td>
                <td><asp:TextBox runat="server" ID="txtName"  CssClass="form-control required" title="请输入类别名称"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:150px;" class="tr">简拼：</td>
                <td><asp:TextBox runat="server" ID="txtSpell"  CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:150px;" class="tr">上级类别：</td>
                <td><asp:DropDownList runat="server" ID="ddlParentType" CssClass="form-control"></asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width:150px;" class="tr">描述：</td>
                <td><asp:TextBox runat="server" ID="txtRemark" CssClass="form-control required" TextMode="MultiLine" style="width:300px;height:50px; font-size:12px; line-height:25px;" title="请输入类别名称"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td>
                <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                <a href="GoodsType.aspx?ParentID=<%=ParentID %>" class="btn btn-white">返回列表</a>
                </td>
            </tr>
        </table>
    </div> 
    </div>
    </form>
</body>
</html>
