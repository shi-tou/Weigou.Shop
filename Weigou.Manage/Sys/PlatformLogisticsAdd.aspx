<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlatformLogisticsAdd.aspx.cs" Inherits="Weigou.Manage.Sys.PlatformLogisticsAdd" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ibox">
        <div class="ibox-title">
            <h5>运费模板添加/编辑</h5>
        </div>
        <div class="ibox-content">
            <!--form-horizontal:水平排列的表单-->
            <table class="form form-horizontal">
                <tr>
                    <td>运费模版名称:<asp:TextBox runat="server" ID="txtName" CssClass="form-control" title="请输入运费模块名称"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>快递公司名称:<asp:DropDownList ID="ddlLogistics" runat="server" CssClass="form-control"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        <h3>请选择并添加运费方式:</h3>
                        (提示：除指定地区外，其余地区的运费采用"默认运费")
                    </td>
                </tr>
                <tr>
                    <td>请设置默认运费：
                                                <asp:TextBox ID="txtDefaultPrice" runat="server" CssClass="form-control" data-options="required:true" min="0" precision="2" missingmessage="请输入默认运费"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        <h3>为指定地区设置物流运费:</h3>
                                        <asp:CheckBoxList ID="CheckBoxProvince" runat="server" RepeatColumns="5"></asp:CheckBoxList>
                                        指定地区的运费：<asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" min="0" precision="2" missingmessage="请输入指定地区运费"></asp:TextBox>
                                        <br />
                                        请添加运费说明：<br />
                                        <asp:TextBox ID="txtRemark" TextMode="MultiLine" Width="300" Height="75" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td> 
                        <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                        <a href="PlatformLogisticsList.aspx" class="btn btn-white">返回列表</a>
                    </td>
                </tr>
            </table> 
            </div>
        </div>
    </form>
</body>
</html>
