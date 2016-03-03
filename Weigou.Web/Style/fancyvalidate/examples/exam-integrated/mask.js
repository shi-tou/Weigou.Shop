$(function() {
  $f.addMethod("currency", function(value, elem, param) {
    var val = parseFloat(value.replace(",", ""));
    return this.optional(elem) || val >= param[0] && val <= param[1];
  }, "请输入在{0}-{1}之间的数值");

  $f("fancyform", {
    rules: {
      mdate: { required: 1, date: 1 }
      , mtime: { required: 1, time: 1 }
      , mip: { required: 1, ipv4: 1, maxlength: 15 }
      , mzip: { required: 1, zipcode: 1 }
      , mtel: { required: 1, phone: 1 }
      , mmobile: { required: 1, mobile: 1 }
      , mcurrency: { required: 1, currency: [100, 10000] }
    }
    , submitHandler: function() {
      alert("验证成功！");
    }
  }).placeHolder();
});