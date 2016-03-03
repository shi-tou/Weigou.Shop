<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsAttributeSet.aspx.cs" Inherits="Weigou.Manage.Goods.GoodsAttributeSet" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>设置商品类别属性-<%=PageTitle %></title>
    <script src="/JScript/highlight.js"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $('#txtKey').bind('keyup change', function (ev) {
                // pull in the new value
                var searchTerm = $(this).val();
                // remove any old highlighted terms
                $('.form-horizontal').removeHighlight();
                // disable highlighting if empty
                if (searchTerm) {
                    // highlight the new term
                    $('.form-horizontal').highlight(searchTerm);
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ibox">
            <div class="ibox-title">
                <h5>商品分类属性设置</h5>
            </div>
             <div class="ibox-content">
            <table class="form form-horizontal">
                <tr>
                    <td style="width: 150px;" class="tr">类别名称：</td>
                    <td>
                        <asp:Label ID="labName" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">输入属性名称查找：</td>
                    <td>
                        <input id="txtKey" type="text" class="form-control" />
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">选择属性名称：</td>
                    <td>
                        <asp:CheckBoxList ID="cheAttribute" runat="server" RepeatDirection="Horizontal" RepeatColumns="5"></asp:CheckBoxList></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                           <a href="<%=Request.UrlReferrer.ToString() %>" class="btn btn-white">返回列表</a> 
                           <input type="hidden" id="hidReferrer" name="hidReferrer" value="<%=Request.UrlReferrer.ToString() %>" />
                    </td>
                </tr>
            </table> 
           </div>
        </div>
    </form>
</body>
</html>
