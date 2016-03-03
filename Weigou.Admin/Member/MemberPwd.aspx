<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberPwd.aspx.cs" Inherits="Weigou.Admin.Member.MemberPwd " %>
<%@ Register src="../UControl/SearchMember.ascx" tagname="SearchMember" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script src="js/memberPwd.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="">
    <uc1:SearchMember ID="SearchMember" runat="server" />
    <div class="easyui-tabs" >
        <div title="修改密码" style="padding: 10px">
            <table class="infotable">
                <tr>
                    <td class="tr" style="width:100px;"></td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rblType" RepeatLayout="Table" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">修改密码</asp:ListItem>
                            <asp:ListItem Value="2">重置密码</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="trOldPwd">
                    <td class="tr">原密码：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtOldPassword" TextMode="Password" class="easyui-validatebox txt" data-options="required:true" missingmessage="请输入原密码"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tr">新密码：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNewPassword" TextMode="Password" class="easyui-validatebox txt" data-options="required:true" missingmessage="请输入新密码" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tr">确认密码：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtNewPassword0" TextMode="Password" class="easyui-validatebox txt" data-options="required:true" missingmessage="请输入确认密码" validType="EqualTo['#txtNewPassword']"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td><input type="button" id="btnSumbit" class="btn" value="确定" /></td>
                </tr>
            </table>
        </div>
    </div>
     <!--弹出窗-->
    <div id="win" class="easyui-window" data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
        <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
    </div>
    </form>
</body>
</html>
