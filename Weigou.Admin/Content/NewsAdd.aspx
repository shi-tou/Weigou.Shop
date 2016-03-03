<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsAdd.aspx.cs" ValidateRequest="false" Inherits="Weigou.Admin.Content.NewsAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>角色添加-<%=PageTitle %></title>
    <link href="/JScript/KindEditor/plugins/code/prettify.css" rel="stylesheet" type="text/css" />
    <script src="/JScript/KindEditor/kindeditor.js" type="text/javascript"></script>
    <script src="/JScript/KindEditor/lang/zh_CN.js" type="text/javascript"></script>
    <script src="/JScript/KindEditor/plugins/code/prettify.js" type="text/javascript"></script>
    <script type="text/javascript">
        KindEditor.ready(function (K) {
            var pcontent1 = document.getElementById('txtContent');
            var editor1 = K.create(pcontent1, {
                cssPath: '/JScript/KindEditor/plugins/code/prettify.css',
                uploadJson: '/JScript/KindEditor/upload_json.ashx',
                fileManagerJson: '/JScript/KindEditor/file_manager_json.ashx',
                allowFileManager: true,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                }
            });
            prettyPrint();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="infotable">
                <tr><td colspan="2">&nbsp;</td></tr>
                <tr>
                    <td style="width: 100px;" class="tr">文章类别：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlType" CssClass="txt">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tr">文章标题：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTitle" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入标题" Width="500"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="tr">文章详情：</td>
                    <td>
                         <asp:TextBox runat="server" ID="txtContent" TextMode="MultiLine" Width="700px" Height="300px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <div class="action">
                <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn" OnClientClick="return $('#form1').form('validate')" OnClick="BtnSubmit_Click" />
                <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
            </div>
        </div>

    </form>
</body>
</html>
