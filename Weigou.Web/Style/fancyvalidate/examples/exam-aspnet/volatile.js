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
        , lov3: { required: 1 }
        , lov4: { required: 1 }
        , lov5: { required: 1, minlength: 2 }
        , lov6: { required: 1, minlength: 2 }
        , desc: { required: 1, rangelength: [20, 50] }
      }
      , messages: {
        uname: { prefix: "用户名必须fancy开头" }
        , headpic: { suffix: "必须为jpg格式" }
        , loc: { equal: "火星人才能注册" }
        , lov1: { minlength: "至少要打3次酱油" }
        , lov5: { minlength: "至少要打2次酱油" }
      }
      , submitHandler: function(form) {
        var btn = $f.dom.get('Button1');
        btn.value = "正在提交...";
        btn.disabled = true;
        submitForm();
      }
      , getKey: $f.getKeyForAspnet
      , getList: $f.getListByKey
  });
});