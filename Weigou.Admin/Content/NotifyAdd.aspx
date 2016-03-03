<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotifyAdd.aspx.cs" Inherits="Weigou.Admin.Content.NotifyAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公告添加-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="infotable" >
            <tr>
                <td style="width:100px;" class="tr">类别：</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlType" CssClass="txt">
                        <asp:ListItem Value="1">会员公告</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trMember">
                <td class="tr">会员：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtMemberName" CssClass="txt" ></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hfMemberID" />
                    <a href="javascript:void(0);" onclick="Select()">选择</a>（不选则默认所有会员）
                </td>
            </tr>
          
            <tr>
                <td class="tr">标题：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtTitle" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入标题" Width="500px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">内容：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtContent" TextMode="MultiLine" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入内容" Width="500px" Height="100px" Font-Size="12px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="action" style="text-align:left; padding-left:200px;">
            <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn" OnClientClick="return $('#form1').form('validate')" OnClick="BtnSubmit_Click" />
            <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
        </div>
    </div>
    <!--弹出窗-->
    <div id="win" class="easyui-window" style=" " data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
        <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
    </div>
    <asp:HiddenField runat="server" ID="hfPrivilege" />
    <script type="text/javascript">
        $(function () {
            changeType();
            $('#ddlType').bind('change', function () {
                changeType();
            });
        });
        
        function changeType() {
            var t = $('#ddlType').val();
            if (t == 1) {
                $('#trMember').show();
                $('#trMerchant').hide();
            }
            else {
                $('#trMember').hide();
                $('#trMerchant').show();
            }
        }
        //选择商户
        function Select() {
            var t = $('#ddlType').val();
            if (t == 1) {
                OpenWin('会员列表<span style="color:red">(双击选择)</span>', 530, 400, '/Member/MemberSelect.aspx?IDControl=hfMemberID&NameControl=txtMemberName');
            }
            else {
                OpenWin('商户列表<span style="color:red">(双击选择)</span>', 480, 400, '/Merchant/MerchantSelect.aspx?IDControl=hfMerchantID&NameControl=txtMerchantName');
            }
            return;
        }
        function SelectBack() {
            $('#txtMemberName').attr('readonly', 'readonly');
            $('#txtMerchantName').attr('readonly', 'readonly');
        }
    </script>
    </form>
</body>
</html>
