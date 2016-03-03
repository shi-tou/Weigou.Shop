<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsAdd.aspx.cs" ValidateRequest="false" Inherits="Weigou.Manage.Content.NewsAdd" %>

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
        $(function () {
            //初始化验证表单
            $("#form1").validate({
                rules: {
                    txtTitle: "required"
                },
                messages: {
                    txtTitle: "请输入文章标题"
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ibox">
            <div class="ibox-title">
                <h5>用户添加/编辑</h5>
            </div>
            <div class="ibox-content">
                <table class="form form-horizontal">
                    <tr>
                        <th style="width:150px;">文章类别：</th>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlType" CssClass="form-control" Width="350px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th >文章标题：</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" title="请输入标题" Width="350px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>文章图片：</th>
                        <td>
                            <asp:FileUpload runat="server" ID="txtPicture" />
                            <asp:Image ImageAlign="AbsMiddle" runat="server" ID="PictureUrl" Width="200px" Height="125px" onerror="this.src='/Style/images/nopic.jpg'" />
                            <asp:HiddenField runat="server" ID="hfPicture" />
                        </td>
                    </tr>
                    <tr>
                        <th>微信关键词：</th>
                        <td>
                            <asp:CheckBoxList runat="server" ID="cblKeyword" RepeatColumns="6" RepeatDirection="Vertical"></asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <th >文章详情：</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtContent" TextMode="MultiLine" Width="800px" Height="350px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th></th>
                        <td>
                            <div class="action">
                                <asp:Button runat="server" ID="BtnSubmit" Text="保存信息" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                                <a href="NewsList.aspx" class="btn btn-white">返回列表</a>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
