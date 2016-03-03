<%@ Page Language="C#" CodeFile="index.aspx.cs" Inherits="SigeShop.Web.Fancy.index" %>
<!DOCTYPE html>
<html>
<head runat="server">
  <title>Fancy Validate - asp.net</title>
  <link href="../css/example.css" rel="stylesheet" />
  <link href="../../css/fancyValidate.css" rel="stylesheet" />
  <script src="../../fancyValidate.min.js"></script>
  <script src="../../fancyValidate.additional.min.js"></script>
  <script>
  $f.dom.ready(function() {
    $f("fancyform", {
      rules: {
        uname: { required: 1, minlength: 7, prefix: "fancy" }
        , email: { required: 1, email: 1 }
        , pwd: { required: 1, rangelength: [6, 8] }
        , pwd2: { required: 1, compareTo: "pwd" }
        , headpic: { required: 1, suffix: ".jpg" }
        , rname: { required: 1, chinese: 1 }
        , sex: { required: 1 }
        , age: { range: [10, 120] }
        , loc: { required: 1, equal: 3 }
        , lov1: { required: 1, minlength: 3 }
        , lov2: { required: 1 }
        , lov3: { required: 1 }
        , lov4: { required: 1 }
        , desc: { required: 1, rangelength: [20, 50] }
      }
      , messages: {
        uname: { prefix: "用户名必须fancy开头" }
        , headpic: { suffix: "必须为jpg格式" }
        , loc: { equal: "火星人才能注册" }
        , lov1: { minlength: "至少要打3次酱油" }
      }
      , submitHandler: function(form) {
        var btn = document.getElementById('Button1');
        btn.value = "正在提交...";
        btn.disabled = true;
        <%=ClientScript.GetPostBackEventReference(Button1, string.Empty) %>
      }
    });
  });
  </script>
</head>
<body>
  <h1>asp.net集成</h1>
  <form id="fancyform" runat="server">
  <fieldset class="fld">
    <legend class="leg">用户信息</legend>
    <ul>
      <li>
        <label class="ttl">用户名：</label>
        <asp:TextBox runat="server" CssClass="fin h23" ID="uname" />
      </li>
      <li>
        <label class="ttl">Email：</label>
        <asp:TextBox runat="server" CssClass="fin h23" ID="email" />
      </li>
      <li>
        <label class="ttl">密码：</label>
        <asp:TextBox runat="server" TextMode="Password" CssClass="fin h23" ID="pwd" />
      </li>
      <li>
        <label class="ttl">确认密码：</label>
        <asp:TextBox runat="server" TextMode="Password" CssClass="fin h23" ID="pwd2" />
      </li>
    </ul>
  </fieldset>
  <fieldset class="fld">
    <legend class="leg">更多信息</legend>
    <ul>
      <li>
        <label class="ttl">头像：</label>
        <asp:FileUpload runat="server" CssClass="fin h23 file" ID="headpic" />
      </li>
      <li>
        <label class="ttl">姓名：</label>
        <asp:TextBox runat="server" CssClass="fin h23" ID="rname" />
      </li>
      <li>
        <label class="ttl">性别：</label>
        <asp:RadioButtonList ID="sex" name="sex" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
          <asp:ListItem Value="1">男</asp:ListItem>
          <asp:ListItem Value="2">女</asp:ListItem>
          <asp:ListItem Value="3">？？</asp:ListItem>
        </asp:RadioButtonList>
      </li>
      <li>
        <label class="ttl">年龄：</label>
        <asp:TextBox CssClass="fin h23" ID="age" runat="server" />
      </li>
      <li>
        <label class="ttl">位置：</label>
        <asp:DropDownList runat="server" CssClass="fin" ID="loc">
          <asp:ListItem Value="">请选择</asp:ListItem>
          <asp:ListItem Value="1">地球</asp:ListItem>
          <asp:ListItem Value="2">月球</asp:ListItem>
          <asp:ListItem Value="3">火星</asp:ListItem>
        </asp:DropDownList>
      </li>
    </ul>
  </fieldset>
  <fieldset class="fld">
    <legend class="leg">其他信息</legend>
    <ul>
      <li>
        <label class="ttl">爱好1：</label>
        <asp:ListBox runat="server" ID="lov1" CssClass="fin" Rows="5" SelectionMode="Multiple">
          <asp:ListItem Value="1">打酱油</asp:ListItem>
          <asp:ListItem Value="2">打酱油</asp:ListItem>
          <asp:ListItem Value="3">打酱油</asp:ListItem>
          <asp:ListItem Value="4">打酱油</asp:ListItem>
          <asp:ListItem Value="5">打酱油</asp:ListItem>
        </asp:ListBox>
      </li>
      <li>
        <label class="ttl">爱好2：</label>
        <asp:CheckBox runat="server" ID="lov3" Text="打酱油" />
      </li>
      <li>
        <label class="ttl">爱好3：</label>
        <asp:RadioButton runat="server" ID="lov4" Text="打酱油" />
      </li>
      <li>
        <label class="ttl">自我描述：</label>
        <asp:TextBox runat="server" ID="desc" CssClass="tarea" TextMode="MultiLine" />
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