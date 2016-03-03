$(function() {
  var dalert = $("#dialog-message").dialog({
    modal: true,
    buttons: {
      "确定": function() {
        $(this).dialog("close");
      }
    }
  }).dialog("close"),

  dconfirm = $("#dialog-confirm").dialog({
    resizable: false,
    height: 140,
    modal: true,
    buttons: {
      "确定": function() {
        $(this).dialog("close");
        dalert.dialog("open");
      },
      "取消": function() {
        $(this).dialog("close");
      }
    }
  }).dialog("close"),

  val = $f("fancyform", {
    rules: {
      date: { required: 1, date: 1 }
      , age: { range: [10, 120] }
      , loc: { required: 1, equal: 3 }
      , email: { required: 1, email: 1 }
      , autoc: { required: 1, maxlength: 12 }
      , sex: { required: 1 }
      , lov2: { required: 1, minlength: 2 }
      , desc: { required: 1, rangelength: [20, 50] }
    }
    , messages: {
      loc: { equal: "火星人才能注册" }
      , lov1: { minlength: "至少要打3次酱油" }
      , lov2: { minlength: "至少要打2次酱油" }
    }
    , submitHandler: function() {
      dconfirm.dialog("open");
    }
    , highlight: function(elem) {
      elem = $(elem);
      elem.css("display") != "none" && elem.effect("highlight").removeClass("ui-state-default").addClass("ui-state-active");
    }
    , unhighlight: function(elem) {
      elem = $(elem);
      elem.css("display") != "none" && elem.removeClass("ui-state-active").addClass("ui-state-default");
    }
    , appendLabel: $f.appendLast
  });

  /* textbox hover */
  $(":text").hover(function() {
    $(this).addClass("ui-state-hover");
  }, function() {
    $(this).removeClass("ui-state-hover");
  });

  /* datepicker */
  $(":text[name=date]").datepicker();
  /* combobox */
  $("select[name=loc]").combobox();

  /* email autocomplete */
  $(":text[name=email]").autocomplete({
    source: function(request, response) {
      var hosts = ["gmail.com", "live.com", "hotmail.com", "yahoo.com", "火星.com"],
        key = request.term,
        ix = key.indexOf("@");
      if (ix > -1) key = key.slice(0, ix);
      if (key) {
        response($(hosts).map(function() {
          return key + "@" + this;
        }));
      }
    }
  });

  /* autocomplete */
  var availableSoures = [];
  for (var i = 0;i < 15;i++) {
    availableSoures[i] = "打酱油" + i.toString();
  }
  $(":text[name=autoc]").autocomplete({
    source: availableSoures
  });

  /*jqueryui slider*/
  $("#slider").slider({
    range: "min",
    value: 15,
    min: 10,
    max: 120,
    step: 1,
    slide: function(event, ui) {
      $("#age").val(ui.value);
      $("#agespan").html(ui.value);
    }
  });
  $("#age").val($("#slider").slider("value"));
  $("#agespan").html($("#age").val());

  /*xheditor*/
  $('#desc').xheditor({ tools: 'mini' });

  /* checkboxes */
  $(".bset").buttonset();

  /* dialog submitHandler */
  $(".todialog").dialog({
    position: [0, 0],
    height: 600,
    width: 700,
    modal: true,
    /* hide close button */
    open: function(event, ui) { $(".ui-dialog-titlebar-close", $(this).parent()).hide(); },
    buttons: {
      "提交": function() {
        if (val.checkForm())
          dconfirm.dialog("open");
      }
    }
  });
});