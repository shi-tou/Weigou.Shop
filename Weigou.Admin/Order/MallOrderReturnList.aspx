<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MallOrderReturnList.aspx.cs" Inherits="Weigou.Admin.Order.MallOrderReturnList" %>

<%@ Register Src="../UControl/ToolBar.ascx" TagName="ToolBar" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>退款列表-<%=PageTitle %></title>
    <script src="js/mallorderreturn.js?v=123" type="text/javascript"></script>
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ResetSearchFormA() {
            $("#tb_a input[type='text']").each(function () {
                $(this).val('');
            });
            $("#tb_a select").each(function () {
                $(this).find("option:first").prop("selected", 'selected');
            });
        }
        function ResetSearchFormB() {
            $("#tb_b input[type='text']").each(function () {
                $(this).val('');
            });
            $("#tb_b select").each(function () {
                $(this).find("option:first").prop("selected", 'selected');
            });
        }
        function ResetSearchFormC() {
            $("#tb_c input[type='text']").each(function () {
                $(this).val('');
            });
            $("#tb_c select").each(function () {
                $(this).find("option:first").prop("selected", 'selected');
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"> 
           <div id="RefundTab" class="easyui-tabs">
            <div title="微信退款管理" data-options="iconCls:'icon-menu'" style="padding: 10px;">
                <!--列表-->
                <table id="WeixinRefundTable" data-options="toolbar:'#tb_a',iconCls:'icon-tip'">
                </table>
            </div>
            <div title="支付宝退款管理" data-options="iconCls:'icon-menu'" style="padding: 10px">
                <!--列表-->
                <table id="AlipayRefundTable" data-options="toolbar:'#tb_b',iconCls:'icon-tip'">
                </table>
            </div>
            <div title="银联退款管理" data-options="iconCls:'icon-menu'" style="padding: 10px">
                <!--列表-->
                <table id="UnionpayRefundTable" data-options="toolbar:'#tb_c',iconCls:'icon-tip'">
                </table>
            </div>
        </div>
        <!--工具栏-->
        <div id="tb_a">
            <div>
                <uc1:ToolBar ID="ToolBar1" runat="server" />
            </div>
            <table>
                <tr>
                    <th>订单号：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtOrderNo_Wx" class="txt" Width="130"></asp:TextBox>
                    </td>
                    <th>会员手机号：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtConsigneeMobileNo_Wx" class="txt" Width="130"></asp:TextBox>
                    </td>                  
                    <th>申请时间：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtMinTime_Wx" class="txt" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                        ~<asp:TextBox runat="server" ID="txtMaxTime_Wx" class="txt" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                    </td>
                    <td>
                        <input type="button" id="btnSearch_Wx" onclick="GetWeixinRefundList()" class="btn" value="查询" />
                        <input type="button" id="Button2" onclick="ResetSearchFormA();" class="btn" value="重置" />
                    </td>
                </tr>
            </table>
        </div>
       <div id="tb_b">
            <div>
                <uc1:ToolBar ID="ToolBar2" runat="server" />
            </div>
            <table>
                <tr>
                    <th>订单号：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtOrderNo_AliPay" class="txt" Width="130"></asp:TextBox>
                    </td>
                    <th>会员手机号：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtConsigneeMobileNo_AliPay" class="txt" Width="130"></asp:TextBox>
                    </td>
                    <th>申请时间：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtMinTime_AliPay" class="txt" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                        ~<asp:TextBox runat="server" ID="txtMaxTime_AliPay" class="txt" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                    </td>
                    <td>
                        <input type="button" id="btnSearch_AliPay" onclick="GetAlipayRefundList()" class="btn" value="查询" />
                        <input type="button" id="Button3" onclick="ResetSearchFormB();" class="btn" value="重置" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="tb_c">
            <div>
                <uc1:ToolBar ID="ToolBar3" runat="server" />
            </div>
            <table>
                <tr>
                    <th>订单号：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtOrderNo_Unionpay" class="txt" Width="130"></asp:TextBox>
                    </td>
                    <th>会员手机号：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtConsigneeMobileNo_Unionpay" class="txt" Width="130"></asp:TextBox>
                    </td>
                    <th>申请时间：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtMinTime_Unionpay" class="txt" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                        ~<asp:TextBox runat="server" ID="txtMaxTime_Unionpay" class="txt" Width="130" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                    </td>
                    <td>
                        <input type="button" id="Button1" onclick="GetUnionpayRefundList()" class="btn" value="查询" />
                        <input type="button" id="Button4" onclick="ResetSearchFormC();" class="btn" value="重置" />
                    </td>
                </tr>
            </table>
        </div>
        <input id="hdTabID" value="1" type="hidden" />
        <!--弹出窗-->
        <div id="win" class="easyui-window" style="" data-options="iconCls:'icon-save',top:'20px',closed:true,minimizable:false,maximizable:false,collapsible:false">
            <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
        </div>
        <asp:HiddenField runat="server" ID="hfWechat" Value="0" /> 
        <asp:HiddenField runat="server" ID="hfAlipay" Value="0" />
        <asp:HiddenField runat="server" ID="hfUnionPay" Value="0" />
    </form>

</body>
</html>
