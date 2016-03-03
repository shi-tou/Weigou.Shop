<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="Weigou.Api.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        .txt
        {
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            color: #555;
            font-size: 14px;
            width:400px;
            height: 28px;
            line-height:28px;
            padding: 3px 8px;
            transition: border-color 0.15s ease-in-out 0s, box-shadow 0.15s ease-in-out 0s;
        }
    </style>
    <script src="http://cdn.bootcss.com/jquery/1.11.3/jquery.min.js"></script>
    <script>
        function TestApi() {
            var method = $('#ddlMethod').find("option:selected").text();
            var param = $("#txtParams").val();
          
            $.ajax({
                type: "POST",
                url: "test.aspx?act=test",
                data: "method=" + method + "&params=" + encodeURIComponent(param),
                dataType: "json",
                success: function (data) {
                    if (method == 'MemberApi.Login') {
                        if(data.result.status==0)
                            $("#txtToken").val(data.result.data.Token);
                    }
                    $("#txtUrl").val(JSON.stringify(data.url));
                    $("#txtResult").val(JSON.stringify(data.result));
                }
            });
        }
        function GetVerifyCode() {
            var m = $("#txtMobileNo").val();
            $.ajax({
                type: "POST",
                url: "test.aspx?act=GetVerifyCode",
                data: "MobileNo=" + m ,
                dataType: "json",
                success: function (data) {
                    $('#txtVerifyCode').val(data.code);
                }
            });
        }
        function changeMethod() {
            var method = $('#ddlMethod').val();
            var desc = method.split('@');
            if (desc.length > 0) {
                $('#txtParams').val(desc[1]);
                $('#labMethod').html($("#ddlMethod").find("option:selected").text());
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding:20px;">
        <table>
            <tr>
                <td>API模块：</td>
                <td><asp:DropDownList runat="server" ID="ddlClass" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true" CssClass="txt"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Method参数：</td>
                <td><asp:DropDownList runat="server" ID="ddlMethod" onchange="changeMethod()" CssClass="txt"></asp:DropDownList></td>
            </tr>
             <tr>
                <td></td>
                <td><asp:Label runat="server" ID="labMethod" ForeColor="Red" Font-Size="14px"></asp:Label></td>
            </tr>
            <tr>
                <td>Token参数：</td>
                <td><asp:TextBox runat="server" ID="txtToken" CssClass="txt"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Params参数：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtParams" TextMode="MultiLine"  CssClass="txt" style=" height:100px; line-height:20px; font-size:13px;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <input type="button" value="测试接口" onclick="TestApi()" />
                    <asp:Button runat="server" ID="btnTest" OnClick="btnTest_Click" Text="测试" Visible="false" /></td>
            </tr>
            <tr>
                <td>请求url：</td>
                <td><asp:TextBox runat="server" ID="txtUrl" TextMode="MultiLine"  CssClass="txt" style=" height:100px; line-height:20px; font-size:13px;"></asp:TextBox></td>
            </tr>
            <tr>
                <td>请求结果：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtResult" TextMode="MultiLine"  CssClass="txt" style="width:450px; height:150px; line-height:20px; font-size:13px;"></asp:TextBox>
                </td>
            </tr>
        </table>
        <br /><br />
        查看手机验证码：
        <hr />
        <table>
            <tr>
                <td>手机号：</td>
                <td><asp:TextBox runat="server" ID="txtMobileNo" CssClass="txt"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td><input type="button" value="查看验证码" onclick="GetVerifyCode()" /></td>
            </tr>
            <tr>
                <td>验证码：</td>
                <td><asp:TextBox runat="server" ID="txtVerifyCode" CssClass="txt"></asp:TextBox></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
