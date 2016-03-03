<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsSend.aspx.cs" Inherits="Weigou.Manage.Sms.SmsSend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>短信发送-<%=PageTitle %></title>
    <script src="sms.js" type="text/javascript"></script>
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            SelectType();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="s1"></asp:ScriptManager>
    <div class="easyui-panel" title="发送短信">
        <table class="infotable" >
            <tr>
                <td style="width:100px;" class="tr">手机号：</td>
                <td>
                     <asp:UpdatePanel runat="server" ID="u1">
                    <ContentTemplate>
                    <table>
                        <tr>
                            <td>
                                <asp:ListBox runat="server" ID="lbMobileNo" Width="290px" CssClass="form-control" Height="100px"></asp:ListBox><br />
                                <div>当前发送手机数：<asp:Label runat="server" ID="lblCount" Font-Bold="true" ForeColor="Red">0</asp:Label></div>
                            </td>
                            <td>
                                
                                <asp:Button runat="server" ID="btnAdd" Text="<< 添加" class="btn" style=" margin-top:5px;" OnClick="btnAdd_Click" /><br /><%--(在上方文本框填写手机号，执行添加到左边列表框)--%>
                                <asp:Button runat="server" ID="btnDel" Text=">> 删除" class="btn" style=" margin-top:5px;" OnClick="btnDel_Click" /><%--(在左边列表框选中手机号，执行删除)<br />--%>
                            </td>
                            <td >
                                <asp:TextBox runat="server" ID="txtMobile" class="form-control" TextMode="MultiLine" Width="270px" Height="100px"></asp:TextBox><br />
                                <div>在上方文本框填写手机号(多个手机号请换行)，执行添加到左边列表框</div>
                            </td>
                        </tr>
                    </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
             <tr>
                <td style="text-align:right;">发送方式：</td>
                <td>
                    <asp:RadioButtonList ID="rblSendType" runat="server" RepeatDirection="Horizontal" onclick="SelectType()" >
                        <asp:ListItem Value="1" Selected="True" >即时</asp:ListItem>
                        <asp:ListItem Value="2" >定时</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr id="trPresetTime">
                <td style="text-align:right;">定时时间：</td>
                <td><asp:TextBox ID="txtPresetTime" runat="server" class="easyui-validatebox txt"  title="请选择定时发送时间"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm',minDate:'%y-%M-%d %H:%m'})"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tr">模板内容：</td>
                <td>
                    <table>
                        <tr>
                            <td><asp:TextBox ID="txtContent" runat="server" CssClass="form-control" title="请输入内容或选择模板" TextMode="MultiLine" style="width:280px;height:100px; font-size:12px;" ></asp:TextBox></td>
                            <td>
                                <asp:HiddenField runat="server" ID="hfTempCode" />
                                <input id="show" type="button" value="选择模板" onclick="showWin()" class="btn" /><br />
                                <input type="button" id="btnReset" onclick="clearInfo()"  class="btn" value="重置内容" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="action">
            <input type="button" class="btn" value="发送短信" onclick="ValidSmsForm()" />
            <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn" OnClick="BtnSubmit_Click" style="display:none;"/>
        </div>
    </div>
    </form>
</body>
</html>