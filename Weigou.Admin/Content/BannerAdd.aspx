<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BannerAdd.aspx.cs" Inherits="Weigou.Admin.Content.BannerAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <title>banner图编辑-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="infotable" >
             <tr>
               <td class="tr" >分类：</td>
               <td >
                   <asp:DropDownList runat="server" ID="ddlType"></asp:DropDownList>
               </td>
            </tr>
            <tr>
                <td class="tr" style="width:100px">标题：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtTitle" CssClass="easyui-validatebox txt" Width="250px" data-options="required:true" missingmessage="请输入标题" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">简单描述：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtSimpleDesc" CssClass="txt" TextMode="MultiLine" style="width:250px; height:80px; font-size:12px; line-height:22px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">图片地址：</td>
                <td>
                    <asp:Image runat="server" ID="imgPicture" Width="200px" Height="100px" onerror="this.src='/Style/images/nopic.jpg'" /><br />
                    <asp:FileUpload runat="server" ID="fuPicture" />
                    <asp:HiddenField runat="server" ID="hfPicture" />
                </td>
            </tr>
            <tr>
                <td class="tr">链接地址：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtUrl" CssClass="txt" Width="250px"  ></asp:TextBox>
                </td>
            </tr>
        </table>
        <div class="action" style="text-align:left; padding-left:200px;">
            <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn" OnClientClick="return $('#form1').form('validate')" OnClick="BtnSubmit_Click" />
            <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn" OnClientClick="CloseWin()"/>
        </div>
    </div>
    </form>
</body>
</html>
