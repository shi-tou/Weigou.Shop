<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EasemobGroupAdd.aspx.cs" Inherits="Weigou.Manage.Easemob.EasemobGroupAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>群组添加-<%=PageTitle %></title>
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
            <h5>物流添加/编辑</h5>
        </div>
        <div class="ibox-content">
            <!--form-horizontal:水平排列的表单-->
            <table class="form form-horizontal">
                <tr>
                    <td>群组图片：</td>
                    <td>
                        <asp:Image runat="server" ID="ImgPhoto" Width="200px" Height="200px" CssClass="form-control" onerror="this.src='/Style/images/nopic.jpg'" />
                        <asp:HiddenField runat="server" ID="hfPhoto" />
                        <asp:FileUpload runat="server" ID="fuPhoto"  CssClass="form-control"/>
                    </td>
                </tr>
                <tr>
                    <td class="tr" style="width: 120px;">群组名称：</td>
                    <td style="width: 200px;">
                        <asp:TextBox runat="server" ID="txtGroupName" CssClass="form-control required" title="请输入群组名称"></asp:TextBox>
                    </td>
                     <td>群组描述：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtGroupDesc" TextMode="MultiLine" CssClass="form-control required" style="height:80px;" title="请输入群组描述"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tr">是否公开：</td>
                    <td>
                        <asp:CheckBox runat="server" ID="cbIsPublic" />
                    </td>
                    <td class="tr">加群是否验证：</td>
                    <td><asp:CheckBox runat="server" ID="cbApproval" /></td>
                </tr>
                <tr>
                    <td class="tr">最大成员数：</td>
                    <td><asp:TextBox runat="server" ID="txtMaxUsers" CssClass="form-control required"  title="请输入最大成员数"></asp:TextBox></td>
                    <td class="tr">群主：</td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtOwner" CssClass="form-control required" title="请选择群主" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                         <a href="MemberList.aspx" class="btn btn-white">返回列表</a>
                    </td>
                </tr>
            </table>                     
        </div>
       </div>  
        <asp:HiddenField runat="server" ID="hidlevelID" />
    </form>
</body>
</html>
