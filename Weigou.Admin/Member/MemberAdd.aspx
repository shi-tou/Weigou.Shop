<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberAdd.aspx.cs" Inherits="Weigou.Admin.Member.MemberAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员添加-<%=PageTitle %></title>
    <script src="/JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="s1"></asp:ScriptManager>
        <div class="easyui-panel" title="<%=Title %>">
            <table class="infotable">
                <tr>
                    <td colspan="5" style="font-weight:bold; font-size:15px;">基本信息</td>
                </tr>
                <tr>
                    <td class="tr" style="width: 120px;">会员姓名：</td>
                    <td style="width: 200px;">
                        <asp:TextBox runat="server" ID="txtName" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入姓名"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                    <td rowspan="10" style="vertical-align: top">
                        <asp:Image runat="server" ID="AvatarUrl" Width="200px" Height="200px" onerror="this.src='/Style/images/nopic.jpg'" />
                        <asp:HiddenField runat="server" ID="hfAvatar" />
                    </td>
                </tr>
                <tr>
                    <td class="tr">会员手机：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMobileNo" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入手机" validType="Mobile"></asp:TextBox></td>
                    <td class="tr">会员邮箱：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEmail" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入邮箱" validType="Email"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="tr" style="width: 120px;">会员性别：</td>
                    <td style="width: 200px;">
                        <asp:DropDownList runat="server" ID="ddlSex">
                            <asp:ListItem Value="0">男</asp:ListItem>
                            <asp:ListItem Value="1">女</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tr">会员头像：</td>
                    <td>
                        <asp:FileUpload runat="server" ID="fuAvatar" /></td>
                </tr>
                <tr runat="server" id="trPwd">
                    <td class="tr">登录密码：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入密码"></asp:TextBox>
                    </td>
                    <td class="tr">确认密码：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPassword0" TextMode="Password" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入确认密码" validType="EqualTo['#txtPassword']"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tr">会员生日：</td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtBirthday" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请选择生日"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="tr">所在地：</td>
                    <td colspan="3">
                        <asp:UpdatePanel runat="server" ID="u1">
                            <ContentTemplate>
                                <asp:DropDownList runat="server" ID="ddlProvince" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack="true" CssClass="txt" Style="width: auto">
                                    <asp:ListItem Value="">-请选择-</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlCity" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="true" CssClass="txt" Style="width: auto">
                                    <asp:ListItem Value="">-请选择-</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="txt" Style="width: auto">
                                    <asp:ListItem Value="">-请选择-</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="tr">会员状态：</td>
                    <td colspan="3">
                        <asp:DropDownList runat="server" ID="ddlStatus" CssClass="txt">
                            <asp:ListItem Value="1">启用</asp:ListItem>
                            <asp:ListItem Value="0">冻结</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>

                <tr>
                    <td class="tr">个性签名：</td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtSignature" CssClass="txt" TextMode="MultiLine" Style="width: 300px; height: 50px; font-size: 12px; line-height: 25px;"></asp:TextBox>
                    </td>
                </tr>
            </table>
            
            <table class="infotable">
                <tr>
                    <td colspan="4" style="font-weight:bold; font-size:15px;">认证信息</td>
                </tr>
                <tr>
                    <td class="tr" style="width: 120px;">身份证号</td>
                    <td style="width: 200px;">
                        <asp:TextBox runat="server" ID="txtIdCard" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入姓名"></asp:TextBox>
                    </td>
                    <td class="tr" style="width: 120px;">驾照初领日期</td>
                    <td><asp:TextBox runat="server" ID="txtIssueDate" onclick="WdatePicker({dateFmt:'MM-dd'})" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请选择驾照初领日期"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="tr">准驾车型</td>
                    <td style="width: 200px;">
                        <asp:DropDownList runat="server" ID="ddlCarClass" CssClass="txt"></asp:DropDownList>
                    </td>
                    <td class="tr">驾照档案编号</td>
                    <td><asp:TextBox runat="server" ID="txtLicenseNo"  CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请选择驾照初领日期"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="tr">认证状态：</td>
                    <td colspan="3"><asp:Label runat="server" ID="labAuthStatus"></asp:Label></td>
                </tr>
            </table>
        </div>
        <div class="action">
            <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn" OnClientClick="return $('#form1').form('validate')" OnClick="BtnSubmit_Click" />
            <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
        </div>
    </form>
</body>
</html>
