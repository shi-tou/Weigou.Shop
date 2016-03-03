<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BannerAdd.aspx.cs" Inherits="Weigou.Manage.Content.BannerAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <title>banner图编辑-<%=PageTitle %></title>
    <script type="text/javascript">
        $(function () {
            $("#form1").validate();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="ibox">
            <div class="ibox-title">
                <h5>banner图添加/编辑</h5>
            </div>
            <div class="ibox-content">
                <!--form-horizontal:水平排列的表单-->
                <table class="form form-horizontal">
             <tr>
               <td class="tr" >分类：</td>
               <td >
                   <asp:DropDownList runat="server" ID="ddlType"></asp:DropDownList>
               </td>
            </tr>
            <tr>
                <td class="tr" style="width:100px">标题：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control  required" Width="250px" title="请输入标题" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">简单描述：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtSimpleDesc" Cssclass="form-control" TextMode="MultiLine" style="width:250px; height:80px; font-size:12px; line-height:22px;"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="txtUrl" Cssclass="form-control" Width="250px"  ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td> <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                     <a href="BannerList.aspx" class="btn btn-white">返回列表</a>

                </td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
