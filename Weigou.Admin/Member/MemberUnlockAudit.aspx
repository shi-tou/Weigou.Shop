<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberUnlockAudit.aspx.cs" Inherits="Weigou.Admin.Member.MemberUnlockAudit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>会员添加-<%=PageTitle %></title>
    <script>
        $(function() {
            $('.img').bind('click', function() {
                layer.tips($(this).attr('alt'), this, {
                    style: ['background-color:#3baae3; color:#fff', '#3baae3'],
                    closeBtn: [0, true]
                });
            })
        });
        function audit(s) {
            $.messager.confirm('操作提示', '是否确认当前操作？', function (r) {
                if (r) {
                    if (s == 1) {
                        $('#btnAudit').click();
                    }
                    else {
                        $('#btnDisAudit').click();
                    }
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="infotable" >
            <tr>
                <td class="tr">会员姓名：</td>
                <td>
                    <asp:Literal runat="server" ID="litMemberName"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="tr">会员手机号：</td>
                <td>
                    <asp:Literal runat="server" ID="litMobileNo"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="tr">申请解冻备注：</td>
                <td >
                    <asp:Literal runat="server" ID="litRemark"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="tr">状态：</td>
                <td>
                    <asp:Label runat="server" ID="labSatus"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tr">审核备注：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtRemark" CssClass="txt" TextMode="MultiLine" style="width:300px;height:50px; font-size:12px; line-height:25px;"></asp:TextBox>
                </td>
            </tr>
        </table>
         <div class="action">
             <input type="button" value="审核通过" class="btn" onclick="audit(1)" />
             <input type="button" value="审核不通过" class="btn" onclick="audit(2)" />
             <input type="button" value="取消" class="btn" onclick="CloseWin()" />
             <div style="display:none;">
            <asp:Button runat="server" ID="btnAudit" Text="审核通过" CssClass="btn" OnClick="btnAudit_Click"  />
            <asp:Button runat="server" ID="btnDisAudit" Text="审核不通过" CssClass="btn" OnClick="btnDisAudit_Click"  /></div>
        </div>
    </div>
    </form>
</body>
</html>
