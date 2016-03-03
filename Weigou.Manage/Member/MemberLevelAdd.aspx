<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberLevelAdd.aspx.cs" Inherits="Weigou.Manage.Member.MemberLevelAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员添加-<%=PageTitle %></title>
    <script src="/JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#form1").validate();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="s1"></asp:ScriptManager>
        <div class="ibox">
            <div class="ibox-title">
                <h5>会员级别加/编辑</h5>
            </div>
            <div class="ibox-content">
                <!--form-horizontal:水平排列的表单-->
                <table class="form form-horizontal">
                    <tr>
                        <td class="tr" style="width: 200px;">级别名称：</td>
                        <td style="width: 220px;">
                            <asp:TextBox runat="server" ID="txtName" CssClass="form-control  required" title="请输入会员级别名称" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tr" style="width: 220px;">所需积分：</td>
                        <td style="width: 250px;">
                            <asp:TextBox runat="server" ID="txtScore" CssClass="form-control inline  required" title="请输入所需积分" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                            <a href="MemberLevelList.aspx" class="btn btn-white">返回列表</a>
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </form>
</body>
</html>
