<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberAdd.aspx.cs" Inherits="Weigou.Manage.Member.MemberAdd" %>

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
            <h5>物流添加/编辑</h5>
        </div>
        <div class="ibox-content">
            <!--form-horizontal:水平排列的表单-->
            <table class="form form-horizontal">
                <tr>
                    <td colspan="5" style="font-weight:bold; font-size:15px;">基本信息</td>
                </tr>
                <tr>
                    <td class="tr" style="width: 120px;">会员姓名：</td>
                    <td style="width: 250px;">
                        <asp:TextBox runat="server" ID="txtName" CssClass="form-control required" title="请输入姓名"></asp:TextBox>
                    </td>
                     <td class="tr" style="width: 120px;"> 会员级别：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlLevel" CssClass="form-control"> 
                        </asp:DropDownList>
                    </td>
                    <td rowspan="10" style="vertical-align: top">
                        <asp:Image runat="server" ID="imgPhoto" Width="200px" Height="200px" CssClass="form-control" onerror="this.src='/Style/images/nopic.jpg'" />
                        <asp:HiddenField runat="server" ID="hfPhoto" />
                    </td>
                </tr>
                <tr>
                    <td class="tr">会员手机：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMobileNo" CssClass="form-control required" title="请输入手机" validType="Mobile"></asp:TextBox></td>
                    <td class="tr">会员邮箱：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" title="请输入邮箱" validType="Email"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="tr">会员性别：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlSex" CssClass="form-control">
                            <asp:ListItem Value="0">男</asp:ListItem>
                            <asp:ListItem Value="1">女</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tr">会员头像：</td>
                    <td>
                        <asp:FileUpload runat="server" ID="fuPhoto"  CssClass="form-control"/></td>
                </tr>
                <tr runat="server" id="trPwd">
                    <td class="tr">登录密码：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control" title="请输入密码"></asp:TextBox>
                    </td>
                    <td class="tr">确认密码：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPassword0" TextMode="Password" CssClass="form-control" title="请输入确认密码" validType="EqualTo['#txtPassword']"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tr">会员生日：</td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtBirthday" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" CssClass="form-control" title="请选择生日" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="tr">所在地：</td>
                    <td colspan="3">
                        <asp:UpdatePanel runat="server" ID="u1">
                            <ContentTemplate>
                                <asp:DropDownList runat="server" ID="ddlProvince" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" Style="width: auto; display:inline;">
                                    <asp:ListItem Value="">-选择省份-</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlCity" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" Style="width: auto; display:inline;">
                                    <asp:ListItem Value="">-选择城市-</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control" Style="width: auto; display:inline;">
                                    <asp:ListItem Value="">-选择区-</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="tr">会员状态：</td>
                    <td colspan="3">
                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control" Width="95px">
                            <asp:ListItem Value="1">启用</asp:ListItem>
                            <asp:ListItem Value="0">冻结</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>

                <tr>
                    <td class="tr">个性签名：</td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtSignature" CssClass="form-control" TextMode="MultiLine" Style="width: 300px; height: 50px; font-size: 12px; line-height: 25px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="BtnSubmit" Text="提交信息" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
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
