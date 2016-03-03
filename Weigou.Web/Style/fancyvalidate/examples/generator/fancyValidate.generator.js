jQuery(function($) {
  var rulesBool = {
    required: false,
    numeric: false,
    digit: false,
    /*additional*/
    email: false,
    url: false,
    ipv4: false,
    date: false,
    time: false,
    zipcode: false,
    compressed: false,
    picture: false,
    chinese: false,
    phone: false,
    mobile: false,
    idcard: false,
    date2: false,
    idcard2: false
  },

  rulesStr = {
    compareTo: "",
    pattern: "",
    fn: "",
    minlength: "",
    maxlength: "",
    rangelength: "",
    min: "",
    max: "",
    range: "",
    prefix: "",
    suffix: "",
    equal: "",
    /*additional*/
    ajax: ""
  },

  container = $("<div class='generator' />").html("<h1>FancyValidate 规则生成器</h1>"),

  rules = $("<fieldset><legend><strong class='red'>规则Key</strong>: <input id='rulename' type='text' /><span class='red'>*(规则名称必须填写)</span></legend></fieldset>"),

  submitter = $("<input type='button' value='生成结果' />"),

  addfld = $("<input type='button' value='添加字段' />"),

  result = $("<fieldset><legend><strong>结果</strong></legend><div id='result' class='blue'></div></fieldset>"),

  createRow = function(name, text, type, cls) {
    return "<div class='" + cls + "'><label for='" + name + "'>" + text +
    ":</label><input type='" + type + "' id='" + name + "' /></div>";
  };

  $.each(rulesBool, function(name, value) {
    rules.append(createRow(name, name, "checkbox", "rbool"));
    rules.append(createRow(name + "_m", "消息", "text", "rstr"));
  });
  rules.append("<hr class='rsplit' />");
  $.each(rulesStr, function(name, value) {
    rules.append(createRow(name, name, "text", "rstr"));
    rules.append(createRow(name + "_m", "消息", "text", "rstr"));
  });

  addfld.click(function() {
    $(this).parent().before(rules.clone());
  });

  function endwith(value, param) {
    return value.slice(-param.length) == param;
  }

  var newline = "<br/>\n\r",
    ntab = "&nbsp;&nbsp;";

  submitter.click(function() {
    var rulelist = [], msglist = [];
    container.find("fieldset:not(:last)").each(function() {
      var rule = [], msg = [], name = $('legend input', this).val();
      if (!name) return;
      $("div :checkbox:checked", this).each(function() {
        if (endwith(this.id, "_m"))
          msg.push(ntab + ntab + this.id.replace("_m", "") + ": \"" + this.value + "\"");
        else
          rule.push(ntab + ntab + this.id + ": 1");
      });
      $('div :text[value!=""]', this).each(function() {
        if (endwith(this.id, "_m"))
          msg.push(ntab + ntab + this.id.replace("_m", "") + ": \"" + this.value + "\"");
        else
          rule.push(ntab + ntab + this.id + ": " + this.value);
      });
      if (rule.length > 0)
        rulelist.push(ntab + name + ": {" + newline + rule.join("," + newline) + newline + ntab + "}");
      if (msg.length > 0)
        msglist.push(ntab + name + ": {" + newline + msg.join("," + newline) + newline + ntab + "}");
    });
    result.find("#result").html("rules: {" + newline + rulelist.join("," + newline) + newline + "}," + newline + "messages:{" + newline + msglist.join("," + newline) + newline + "}");
  });

  $("<style> \n\r" +
    ".red { color: red; } \n\r" +
    ".blue { color: blue; } \n\r" +
    ".rsplit { color: #333; clear:both; } \n\r" +
    ".generator h1 { color: #c17878; border-bottom: 1px solid #dddddd; padding-bottom: 0.4em; } \n\r" +
    ".generator fieldset { border: 1px solid #333; margin: 5px auto;} \n\r" +
    ".generator label { width: 90px; display: inline-block; text-align: right; font-weight: bold; } \n\r" +
    ".generator div { float: left; padding: 5px 0;} \n\r" +
    ".generator input { color: blue;} \n\r" +
    ".generator .rbool { width: 120px; } \n\r" +
    ".generator .rstr { width: 170px; } \n\r" +
    ".generator .rstr input { width: 70px; } \n\r" +
    "</style>").appendTo("head");

  container
  .append(rules.clone())
  .append($("<div/>").css({ width: '100%', 'text-align': 'center' }).append(submitter).append(addfld))
  .append(result)
  .appendTo(document.body);
});