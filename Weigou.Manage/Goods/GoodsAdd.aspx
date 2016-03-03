<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsAdd.aspx.cs" Inherits="Weigou.Manage.Goods.GoodsAdd" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
    <title></title>
    <link href="/JScript/KindEditor/plugins/code/prettify.css" rel="stylesheet" type="text/css" />
    <script src="/JScript/KindEditor/kindeditor.js" type="text/javascript"></script>
    <script src="/JScript/KindEditor/lang/zh_CN.js" type="text/javascript"></script>
    <script src="/JScript/KindEditor/plugins/code/prettify.js" type="text/javascript"></script>
    <script src="js/Goods.js" type="text/javascript"></script>
     
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
            if ($('#cbGenerate').prop('checked') == true) {
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
        
        function SelectAttr() {
            $('#BtnSelect').click();
        }

        $(function(){
            $("#ddlType").change(function(){
                $('#BtnSelect').click();
                $('#createTable').html('');
            });             
            InitValidate_GoodsAdd();
        });

    </script>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="s1"></asp:ScriptManager>
        <div class="ibox">
            <div class="ibox-title">
                <h5>商品添加/编辑</h5>
            </div>
             <div class="ibox-content">
            <table class="form form-horizontal" >
                <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">商品编码：</td>
                    <td >
                        <asp:TextBox runat="server" ID="txtCode" CssClass="form-control inline" Width="120px"></asp:TextBox>
                        <asp:CheckBox runat="server" ID="cbGenerate" Text="系统自动生成" onclick="GenerateCode()" />
                    </td>
                    <td style="width: 150px;" class="tr">商品类别：</td>
                    <td > <%--OnSelectedIndexChanged="ddlType_SelectedIndexChanged" AutoPostBack="true"--%>
                          <asp:DropDownList runat="server" ID="ddlType" class="form-control" > 
                          </asp:DropDownList>     
                    </td>
                   
                </tr>
                <tr>
                    <td style="width: 150px;" class="tr">商品名称：</td>
                    <td style="width: 250px;">
                        <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox></td>
                    <td class="tr">上架状态：</td>
                    <td>
                        <asp:DropDownList ID="ddlShelvesStatus" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">下架</asp:ListItem>
                            <asp:ListItem Value="1">上架</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>

                    <td class="tr">销售价格：</td>
                    <td>
                        <asp:TextBox ID="txtSalePrice" runat="server" Text="0.00" CssClass="form-control"></asp:TextBox></td>
                    <td class="tr">市场价格：</td>
                    <td>
                        <asp:TextBox ID="txtMarketPrice" runat="server" Text="0.00" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr>

                    <td class="tr">商品库存：</td>
                    <td>
                        <asp:TextBox ID="txtStock" runat="server" Text="0" CssClass="form-control inline" Width="90px"></asp:TextBox><span style="color: red;">(0表示不限制)</span></td>
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
                        <asp:TextBox runat="server" ID="txtSimpleDesc" CssClass="form-control" Width="500px" Height="80px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tr">详细介绍：</td>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" CssClass="form-control" Width="700px" Height="200px"></asp:TextBox>
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
                        <div class="div_contentlist2" style="width:100%">
                            <div id="createTable"></div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        
                <asp:Button runat="server" ID="BtnSubmit" Text="保存信息" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                <a href="GoodsList.aspx" class="btn btn-white">返回列表</a>
                <asp:Button runat="server" ID="BtnSelect" Text="Show" Style="display:none;" OnClick="BtnSelect_Click" />

                   </td>
                </tr>
            </table>
            </div> 
        </div>
          
        <script type="text/javascript">
            var  myHideJson=<%=strJson%>;
        </script>
        <script src="js/SaleProp.js" type="text/javascript"></script>
    </form>
</body>
</html>
