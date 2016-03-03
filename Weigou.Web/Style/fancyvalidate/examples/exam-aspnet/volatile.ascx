<%@ Control Language="C#" %>
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
      <label class="ttl fl">爱好4：</label>
      <asp:CheckBoxList CssClass="fl" ID="lov5" runat="server">
        <asp:ListItem Value="1">打酱油</asp:ListItem>
        <asp:ListItem Value="2">打酱油</asp:ListItem>
        <asp:ListItem Value="3">打酱油</asp:ListItem>
      </asp:CheckBoxList>
      <div class="cl">
      </div>
    </li>
    <li>
      <label class="ttl">爱好5：</label>
      <asp:CheckBoxList ID="lov6" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
        <asp:ListItem Value="1">打酱油</asp:ListItem>
        <asp:ListItem Value="2">打酱油</asp:ListItem>
        <asp:ListItem Value="3">打酱油</asp:ListItem>
      </asp:CheckBoxList>
    </li>
    <li>
      <label class="ttl">自我描述：</label>
      <asp:TextBox runat="server" ID="desc" CssClass="tarea" TextMode="MultiLine" />
    </li>
  </ul>
</fieldset>