<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberLevelAdd.aspx.cs" Inherits="Weigou.Admin.Member.MemberLevelAdd" %>

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
                    <td class="tr" style="width: 200px;">会员级别名称：</td>
                    <td style="width: 280px;">
                        <asp:TextBox runat="server" ID="txtName" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入会员级别名称"></asp:TextBox>
                    </td>
                    <td class="tr" style="width: 200px;">会员年限：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtYear" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入会员年限"></asp:TextBox>(年)
                    </td>
                </tr>
                <tr>
                    <td class="tr">会员金额：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMoney" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入会员金额"></asp:TextBox>(元)</td>
                    <td class="tr">积分兑换标准：</td>
                    <td >
                        <asp:TextBox runat="server" ID="txtPoint" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入积分兑换标准" ></asp:TextBox>(分)
                    </td>
                </tr>
                <tr>
                    <td class="tr" style="width: 120px;">租车价格：</td>
                    <td>
                         <asp:TextBox runat="server" ID="txtPrice" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入租车价格"></asp:TextBox>(元/小时) 
                    </td>
                    <td class="tr">租车折扣：</td>
                    <td>
                       <asp:TextBox runat="server" ID="txtRentDiscount" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入租车折扣"></asp:TextBox>(折)
                    </td>
                </tr>
                <tr>
                    <td class="tr">免费开始时间：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtStartTime" CssClass="easyui-validatebox txt"  onclick="WdatePicker({dateFmt:'HH:mm'})" data-options="required:true" missingmessage="请输入免费开始时间"></asp:TextBox>
                    </td>
                    <td class="tr">免费结束时间：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEndTime" CssClass="easyui-validatebox txt"  onclick="WdatePicker({dateFmt:'HH:mm'})" data-options="required:true" missingmessage="请输入免费结束时间"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tr">购物折扣：</td>
                    <td>
                       <asp:TextBox runat="server" ID="txtShoppingDiscount" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入购物折扣"></asp:TextBox>(折)
                    </td>
                    <td class="tr">购车折扣：</td>
                    <td >
                       <asp:TextBox runat="server" ID="txtBuyCarDiscount" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入购车折扣"></asp:TextBox>(折)                       
                    </td>
                </tr>
                <tr>
                    <td class="tr">每消费百元可兑积分：</td>
                    <td >
                       <asp:TextBox runat="server" ID="txtConsumerPoint" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入消费百元可兑积分"></asp:TextBox>(分)
                    </td>   
                    <td class="tr">每1000公里可兑积分：</td>
                    <td>
                       <asp:TextBox runat="server" ID="txtMileagePoint" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入1000公里可兑积分"></asp:TextBox>(分)
                    </td>
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
