<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FriendLinkAdd.aspx.cs" Inherits="Weigou.Admin.Content.FriendLinkAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色添加-<%=PageTitle %></title>
    <script src="../JScript/zTree_v3/js/jquery.ztree.all-3.5.min.js" type="text/javascript"></script>
    <link href="../JScript/zTree_v3/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="infotable" >
            <tr>
                <td style="width:100px;" class="tr">类别：</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlType" CssClass="txt">
                        <asp:ListItem Value="1">文字</asp:ListItem>
                        <asp:ListItem Value="2">图片</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="tr1">
                <td class="tr">标题：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtTitle" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入标题"></asp:TextBox>
                </td>
            </tr>
            <tr id="tr2" style="display:none;">
                <td class="tr">图片：</td>
                <td>
                    <asp:Image runat="server" ID="imgPicture" Width="200px" Height="80px" onerror="this.src='/Style/images/nopic.jpg'" /><br />
                    <asp:HiddenField runat="server" ID="hfPicture" /><span class="red"">图片尺寸（200px*80px）</span>
                    <asp:FileUpload runat="server" ID="fuPicture" />
                </td>
            </tr>
            <tr id="tr3">
                <td class="tr">链接地址：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtUrl" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入链接地址"></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="action">
            <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn" OnClientClick="return $('#form1').form('validate')" OnClick="BtnSubmit_Click" />
            <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
        </div>
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
                $('#tr1').show();
                $('#tr2').hide();
                $('#txtTitle').validatebox('reduce');
            }
            else {
                $('#tr1').hide();
                $('#tr2').show();
                $('#txtTitle').validatebox('remove');
            }
        }
    </script>
    </form>
</body>
</html>
