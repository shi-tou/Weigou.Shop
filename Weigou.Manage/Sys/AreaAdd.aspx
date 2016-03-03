<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaAdd.aspx.cs" Inherits="Weigou.Manage.Sys.AreaAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title> 
</head>
<body>
    <form id="form1" runat="server">
    <!--省-->
    <asp:Panel runat="server" ID="panel_P" Visible="false">
       <div class="ibox-title">
            <h5>省份添加/编辑</h5>
        </div>
        <div class="ibox-content">
            <!--form-horizontal:水平排列的表单-->
            <table class="form form-horizontal">
            <tr>
                <td class="tr" style="width:100px;">省名称</td>
                <td>
                    <asp:TextBox runat="server" ID="txtProvinceName" CssClass="form-control" title="请输入省名称"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">全拼：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtSpell_P" CssClass="form-control" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">首字母：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtFirstLetter_P" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">简称：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtShortName" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
                <tr>
                    <td></td>
                    <td> 
                        <asp:Button runat="server" ID="BtnSubmitP" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                        <a href="AreaManage.aspx?Type=1" class="btn btn-white">返回列表</a>
                    </td>
                </tr>
        </table>
        </div>  
    </asp:Panel>
    <!--市-->
    <asp:Panel runat="server" ID="panel_C" Visible="false">
        <div class="ibox-title">
            <h5>城市添加/编辑</h5>
        </div>
        <div class="ibox-content">
            <!--form-horizontal:水平排列的表单-->
            <table class="form form-horizontal">
            <tr>
                <td class="tr" style="width:100px;">城市名称</td>
                <td>
                    <asp:TextBox runat="server" ID="txtCityName" CssClass="form-control" title="请输入省名称"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">全拼：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtSpell_C" CssClass="form-control" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">首字母：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtFirstLetter_C" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">所在省：</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProvince_C" CssClass="form-control"></asp:DropDownList>
                </td>
            </tr>
             <tr>
                    <td></td>
                    <td> <asp:Button runat="server" ID="BtnSubmitC" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                         <a href="AreaManage.aspx?Type=2" class="btn btn-white">返回列表</a>
                    </td>
              </tr>
        </table> 
        </div>
    </asp:Panel>
    <!--区-->
    <asp:Panel runat="server" ID="panel_D" Visible="false" >
         <div class="ibox-title">
            <h5>区添加/编辑</h5>
        </div>
        <div class="ibox-content">
            <!--form-horizontal:水平排列的表单-->
            <table class="form form-horizontal">
            <tr>
                <td class="tr" style="width:100px;">地区名称</td>
                <td>
                    <asp:TextBox runat="server" ID="txtDistrictName" CssClass="form-control" title="请输入省名称"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">所在省：</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlProvince_D" OnSelectedIndexChanged="ddlProvinceD_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tr">所在市：</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCity" CssClass="form-control"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button runat="server" ID="BtnSubmitD" Text="确认" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                    <a href="AreaManage.aspx?Type=3" class="btn btn-white">返回列表</a>

                </td>
            </tr>
        </table> 
        </div>
    </asp:Panel>
    </form>
</body>
</html>
