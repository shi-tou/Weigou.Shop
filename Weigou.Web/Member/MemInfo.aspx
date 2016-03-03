<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemInfo.aspx.cs" Inherits="Weigou.Web.Member.MemInfo" %>

<%@ Register Src="/UControl/MemHeader.ascx" TagName="MemHeader" TagPrefix="uc1" %>
<%@ Register Src="/UControl/MemMenu.ascx" TagName="MemMenu" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/Style/fancyvalidate/fancyValidate.min.js"></script>
    <script src="/Style/fancyvalidate/fancyValidate.additional.min.js"></script>
    <link href="/Style/fancyvalidate/css/fancyValidate.css" rel="stylesheet" />
</head>
<body>
    <form id="memform" runat="server">
        <asp:ScriptManager runat="server" ID="s1"></asp:ScriptManager>
        <uc1:MemHeader ID="MemHeader1" runat="server" />
        <div class="container mem_container">
            <div class="row">
                <div class="col-md-2">
                    <uc2:MemMenu ID="MemMenu1" runat="server" />
                </div>
                <div class="col-md-10 mem_main">
                    <div class="panel panel-default">
                        <div class="panel-heading">我的资料</div>
                        <div class="panel-body">
                            <table class="memform">
                                <tr>
                                    <th>会员姓名：</th>
                                    <td><asp:TextBox runat="server" ID="txtName" CssClass="txt"></asp:TextBox></td>
                                    <td rowspan="5">
                                        <asp:Image runat="server" ID="AvatarUrl" Width="200px" Height="200px" />
                                        <asp:HiddenField runat="server" ID="hfAvatar" />
                                        <asp:FileUpload runat="server" ID="fuAvatar" />
                                    </td>
                                </tr>
                                <tr>
                                    <th>会员性别：</th>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlSex" class="txt">
                                            <asp:ListItem Value="0">男</asp:ListItem>
                                            <asp:ListItem Value="1">女</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <th>会员生日：</th>
                                    <td><asp:TextBox runat="server" ID="txtBirthday" onclick="WdatePicker({dateFmt:'MM-dd'})" class="txt" ></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <th>所在地：</th>
                                    <td>
                                        <asp:UpdatePanel runat="server" ID="u1">
                                            <ContentTemplate>
                                                <asp:DropDownList runat="server" ID="ddlProvince" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged" AutoPostBack="true" class="txt" Style="width: auto">
                                                    <asp:ListItem Value="">-请选择-</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList runat="server" ID="ddlCity" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="true" class="txt" Style="width: auto">
                                                    <asp:ListItem Value="">-请选择-</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList runat="server" ID="ddlDistrict" class="txt" Style="width: auto">
                                                    <asp:ListItem Value="">-请选择-</asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <th>公司名称：</th>
                                    <td><asp:TextBox runat="server" ID="txtCompanyName" class="txt w400"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <th>联系地址：</th>
                                    <td><asp:TextBox runat="server" ID="txtAddress" class="txt w400"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <th>备注：</th>
                                    <td><asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" class="txt w400" Height="80px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <th></th>
                                    <td><asp:Button runat="server" ID="btnUpdateMem" Text="保存修改" OnClientClick="check()" OnClick="btnUpdateMem_Click" class="btn btn-primary" /></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script>
            $f.dom.ready(function () {
                $f("memform", {
                    rules: {
                        txtName: { required: 1, minlength: 2 }
                    }
              , messages: {
              }
              , submitHandler: function () {
                  var btn = document.getElementById('btnUpdateMem');
                  btn.value = "正在提交...";
                  btn.disabled = true;
              <%=ClientScript.GetPostBackEventReference(btnUpdateMem, string.Empty) %>
          }
            });
        });
    </script>
    </form>
</body>
</html>
