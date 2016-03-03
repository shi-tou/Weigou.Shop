<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsAdd.aspx.cs" Inherits="Weigou.Admin.Goods.GoodsAdd" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/JScript/KindEditor/plugins/code/prettify.css" rel="stylesheet" type="text/css" />
    <script src="/JScript/KindEditor/kindeditor.js" type="text/javascript"></script>
    <script src="/JScript/KindEditor/lang/zh_CN.js" type="text/javascript"></script>
    <script src="/JScript/KindEditor/plugins/code/prettify.js" type="text/javascript"></script>
        <style>
        /*表格样式*/
        table#process {
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
            text-align: center;
        }

            table#process th {
                text-align: center;
                border-width: 1px;
                padding: 8px;
                border-style: solid;
                border-color: #666666;
                background-color: #dedede;
            }

            table#process td {
                text-align: center;
                border-width: 1px;
                padding: 8px;
                border-style: solid;
                border-color: #666666;
                background-color: #ffffff;
            }
    </style>
    <script type="text/javascript">
        var url = '/Ajax/Goods.ashx';
        KindEditor.ready(function (K) {
            var pcontent1 = document.getElementById('txtDescription');
            var editor1 = K.create(pcontent1, {
                cssPath: '/JScript/KindEditor/plugins/code/prettify.css',
                uploadJson: '/JScript/KindEditor/upload_json.ashx',
                fileManagerJson: '/JScript/KindEditor/file_manager_json.ashx',
                allowFileManager: true,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=example]')[0].submit();
                    });
                }
            });
            prettyPrint();
        });
        function GenerateCode() {
            if ($('#cbGenerate').attr("checked") == 'checked') {
                DoAjax(dealAjaxUrl('/Ajax/Sys.ashx'), { 'action': 'GenerateCode', 'type': '1' }, function (data) {
                    $('#txtCode').val(data.res);
                    $('#txtCode').attr('readonly', 'readonly');
                    $('#txtCode').css("background-color", "#eee");
                });
            }
            else {
                $('#txtCode').val('');
                $('#txtCode').removeAttr("readonly");
                $('#txtCode').css("background-color", "#fff");
            }
        }
        //选择商品分类
        function SelectGoodsType() {
            OpenWin('商品分类列表<span style="color:red">(双击选择商品分类)</span>', 500, 500, '/Goods/GoodsTypeSelect.aspx?IDControl=hfTypeID&NameControl=txtTypeName');
            return;
        }
        function SelectAttr() {
            $('#BtnSelect').click();
        }

        function CheckForm() {
            if ($('#form1').form('validate')) {
                if ($("#txtTypeName").val() != '') {
                    return true;
                }
                else {
                    AlertInfo('商品分类为空，请先设置分类！');
                }
            }
            return false;
        }

    </script>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="s1"></asp:ScriptManager>
        <div>
            <table class="infotable" cellpadding="0px" cellspacing="1px">
                <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">商品编码：</td>
                    <td >
                        <asp:TextBox runat="server" ID="txtCode" CssClass="easyui-validatebox txt" Width="100px" data-options="required:true" missingmessage="请输入商品编码"></asp:TextBox>
                        <asp:CheckBox runat="server" ID="cbGenerate" Text="系统自动生成" onclick="GenerateCode()" />
                    </td>
                    <td style="width: 150px;" class="tr">商品类别：</td>
                    <td >
                        <asp:TextBox runat="server" ID="txtTypeName" data-options="required:true" missingmessage="请选择商品分类" CssClass="easyui-validatebox txt" Enabled="false"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hfTypeID" />
                        <a href="javascript:void(0);" onclick="SelectGoodsType()">选择商品类别</a>
                    </td>
                   
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">商品名称：</td>
                    <td style="width: 200px;">
                        <asp:TextBox runat="server" ID="txtName" CssClass="easyui-validatebox txt" data-options="required:true" missingmessage="请输入商品名称"></asp:TextBox></td>
                    <td class="tr">上架状态：</td>
                    <td>
                        <asp:DropDownList ID="ddlShelvesStatus" runat="server" CssClass="txt">
                            <asp:ListItem Value="0">下架</asp:ListItem>
                            <asp:ListItem Value="1">上架</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>

                    <td class="tr">销售价格：</td>
                    <td>
                        <asp:TextBox ID="txtSalePrice" runat="server" Text="0.00" CssClass="easyui-numberbox txt" data-options="required:true" min="0" precision="2" missingmessage="请输入销售价格"></asp:TextBox></td>
                    <td class="tr">市场价格：</td>
                    <td>
                        <asp:TextBox ID="txtMarketPrice" runat="server" Text="0.00" CssClass="easyui-numberbox txt" data-options="required:true" min="0" precision="2" missingmessage="请输入市场价格"></asp:TextBox></td>
                </tr>
                <tr>

                    <td class="tr">商品库存：</td>
                    <td>
                        <asp:TextBox ID="txtStock" runat="server" Text="0" CssClass="easyui-numberbox txt" Width="50px" min="0" data-options="required:true" missingmessage="请输入商品库存数量"></asp:TextBox><span style="color: red;">(0表示不限制)</span></td>
                    <asp:Panel ID="IsViewStatus" runat="server" Visible="false">
                        <td class="tr">审核状态：</td>
                        <td colspan="3">
                            <asp:Label ID="txtStatus" runat="server"></asp:Label>
                        </td>
                    </asp:Panel>
                </tr>
                <tr>
                    <td class="tr">简单介绍：</td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtSimpleDesc" CssClass="txt" Width="500px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tr">详细介绍：</td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Width="700px" Height="200px"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td class="tr">商品品牌：</td>
                    <td colspan="3">
                        <asp:UpdatePanel runat="server" ID="u2">
                            <ContentTemplate>
                                <asp:Panel ID="pBrand" runat="server"></asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSelect" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td class="tr">扩展属性：</td>
                    <td colspan="3" id="GoodsAttributeHTML">
                        <asp:UpdatePanel runat="server" ID="u1">
                            <ContentTemplate>
                                <asp:Table ID="Table1" CssClass="div_contentlist" runat="server" CellPadding="0" CellSpacing="0">
                                </asp:Table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSelect" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                 <tr>
                    <td class="tr">销售属性：</td>
                    <td colspan="3">
                        <div class="div_contentlist2" style="width: 70%">
                            <div id="createTable"></div>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="action">
                <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn" OnClientClick="return CheckForm()" OnClick="BtnSubmit_Click" />
                <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
                <asp:Button runat="server" ID="BtnSelect" Text="" Style="display: none;" CssClass="btn" OnClick="BtnSelect_Click" />
            </div>
        </div>
        <!--弹出窗-->
        <div id="win" class="easyui-window" data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
            <iframe id="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
        </div>

        <script type="text/javascript">
            var  myHideJson=<%=strJson%>;
        </script>
        <script src="/JScript/SaleProp.js" type="text/javascript"></script>
    </form>
</body>
</html>
