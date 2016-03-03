<%@ Page Language="C#" CodeFile="index.aspx.cs" Inherits="index" %>
<!DOCTYPE html>
<html>
<head runat="server">
  <title>Fancy Validate - asp.net checkbox</title>
  <link href="../css/example.css" rel="stylesheet" />
  <link href="../../css/fancyValidate.css" rel="stylesheet" />
  <script src="../../fancyValidate.min.js"></script>
  <script src="../../fancyValidate.additional.min.js"></script>
  <script>
  $f.dom.ready(function() {
    $f("fancyform", {
      rules: {
        lov2: { required: 1, minlength: 2 }
        , lov3: { required: 1 }
        , lov5: { required: 1, minlength: 2 }
        , lov6: { required: 1, minlength: 2 }
      }
      , messages: {
        lov2: { minlength: "至少要打2次酱油" }
        , lov5: { minlength: "至少要打2次酱油" }
        , lov6: { minlength: "至少要打2次酱油" }
      }
      , submitHandler: function(form) {
        var btn = document.getElementById('Button1');
        btn.value = "正在提交...";
        btn.disabled = true;
        <%=ClientScript.GetPostBackEventReference(Button1, string.Empty) %>
      }
      , getKey: $f.getKeyForAspnet
      , getList: $f.getListByKey
      , ruleKeyAlter: "rulekey2"
    });
  });
  </script>
</head>
<body>
  <h1>邪恶的checkbox</h1>
  <form id="fancyform" runat="server">
  <fieldset class="fld">
    <legend class="leg">其他信息</legend>
    <ul>
      <li>
        <label class="ttl">爱好1：</label>
        <input runat="server" type="checkbox" id="check1" rulekey2="lov2" /><label for="check1">打酱油</label>
        <input runat="server" type="checkbox" id="check2" rulekey2="lov2" /><label for="check2">打酱油</label>
        <input runat="server" type="checkbox" id="check3" rulekey2="lov2" /><label for="check3">打酱油</label>
      </li>
      <li>
        <label class="ttl">爱好2：</label>
        <asp:CheckBox runat="server" ID="lov3" Text="打酱油" />
      </li>
      <li>
        <label class="ttl fl">爱好3：</label>
        <asp:CheckBoxList CssClass="fl" ID="lov5" runat="server">
          <asp:ListItem Value="1">打酱油</asp:ListItem>
          <asp:ListItem Value="2">打酱油</asp:ListItem>
          <asp:ListItem Value="3">打酱油</asp:ListItem>
        </asp:CheckBoxList>
        <div class="cl">
        </div>
      </li>
      <li>
        <label class="ttl">爱好4：</label>
        <asp:CheckBoxList ID="lov6" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
          <asp:ListItem Value="1">打酱油</asp:ListItem>
          <asp:ListItem Value="2">打酱油</asp:ListItem>
          <asp:ListItem Value="3">打酱油</asp:ListItem>
        </asp:CheckBoxList>
      </li>
    </ul>
  </fieldset>
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