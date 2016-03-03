<%@ Page Language="C#" CodeFile="index.aspx.cs" Inherits="index" %>
<%@ Register Src="volatile.ascx" TagName="volatile" TagPrefix="uc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
  <title>Fancy Validate - asp.net autoid</title>
  <link href="../css/example.css" rel="stylesheet" />
  <link href="../../css/fancyValidate.css" rel="stylesheet" />
  <script src="../../fancyValidate.min.js"></script>
  <script src="../../fancyValidate.additional.min.js"></script>
  <script>
    function submitForm() {
      <%=ClientScript.GetPostBackEventReference(Button1, string.Empty) %>
    }
  </script>
  <script src="volatile.js"></script>
</head>
<body>
  <h1>兼容asp.net的AutoId</h1>
  <form id="fancyform" runat="server">
  <uc1:volatile ID="volatile1" runat="server" />
  <ul>
    <li>
      <label class="ttl"></label>
      <asp:Button runat="server" CssClass="btn" Text="提交" ID="Button1" OnClick="Button1_Click" />
    </li>
  </ul>
  <asp:Literal runat="server" ID="result" />
  </form>
</body>
</html>