/*!
 * Fancy Validate Additional v0.1.4 - JavaScript Form Validation
 * Copyright 2013 cormin.lu@gmail.com
 * MIT Licensed
 * Build 09/22/2013
 */
(function($f) {
  var core = $f.core,
    dom = $f.dom,
    event = $f.event,

  $patterns = {
    /*RFC 2822 */
    email: /^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$/i,

    url: /^(http|https|ftp)\:\/\/[a-z\d\-\.]+\.[a-z]{2,3}(:[a-z\d]*)?\/?([a-z\d\-\._\?\,\'\/\\\+&amp;%\$#\=~])*$/i,

    /*RFC 3896 */
    rfc3896url: new RegExp(
        "^" +
        "(?:" +
          "([^:/?#]+)" +         // scheme
        ":)?" +
        "(?://" +
          "(?:([^/?#]*)@)?" +    // credentials
          "([^/?#:@]*)" +        // domain
          "(?::([0-9]+))?" +     // port
        ")?" +
        "([^?#]+)?" +            // path
        "(?:\\?([^#]*))?" +      // query
        "(?:#(.*))?" +           // fragment
        "$"
        ),

    ipv4: /^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/,

    date: /^(19|20)\d\d([- /.])(0[1-9]|1[012])\2(0[1-9]|[12][0-9]|3[01])(?: ([01]\d|2[0-3])(:[0-5]\d){0,2})?$/,
    
    date2: /^(19|20)\d\d([- /.])(0?[1-9]|1[012])\2(0?[1-9]|[12][0-9]|3[01])(?: ([01]?\d|2[0-3])(:[0-5]?\d){0,2})?$/,

    time: /^([01]\d|2[0-3])(:[0-5]\d){0,2}$/,

    zipcode: /^\d{6}$/,

    compressed: /.+\.(7z|zip|rar|tar|taz|tgz|gz|gzip)$/i,

    picture: /.+\.(jpe?g|bmp|png|gif|ico|pcx|tif|raw|tga)$/i,

    /*Unicode+Ansi*/
    chinese: /^[\u0391-\uFFE5\u4e00-\u9fa5]+$/,

    phone: /^([(]?0\d{2,3}[)]?[-]?)?\s?\d{4}\-?\d{4}(-\d+)?$/,

    mobile: /^(1[3458])\d{9}$/
  },

  $messages = {
    email: "请输入一个有效的的电子邮件地址",
    url: "请输入一个有效的的URL地址",
    ipv4: "请输入有效的ip地址",
    date: "请输入一个有效的日期",
    date2: "请输入一个有效的日期",
    time: "请输入一个有效的时间",
    zipcode: "请输入一个有效的邮编",
    compressed: "请选择一个有效的压缩文件",
    picture: "请选择一个有效的图片文件",
    chinese: "请输入中文",
    phone: "请输入一个有效的电话号码",
    mobile: "请输入一个有效的手机号码"
  },

  aCity = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江", 31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏", 61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外" },

  isIDCard = function(sId) {
    var d, iSum = 0;

    if (!/^\d{17}(\d|x)$/i.test(sId))
      return "你输入的身份证长度或格式错误";

    sId = sId.replace(/x$/i, "a");

    if (!aCity[parseInt(sId.substr(0, 2))])
      return "你的身份证地区非法";

    sBirthday = sId.substr(6, 4) + "-" + Number(sId.substr(10, 2)) + "-" + Number(sId.substr(12, 2));
    d = new Date(sBirthday.replace(/-/g, "/"));

    if (sBirthday != (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate()))
      return "身份证上的出生日期非法";

    for (var i = 17;i >= 0;i--)
      iSum += (Math.pow(2, i) % 11) * parseInt(sId.charAt(17 - i), 11);

    if (iSum % 11 != 1)
      return "你输入的身份证号非法";

    return true; //aCity[parseInt(sId.substr(0,2))]+","+sBirthday+","+(sId.substr(16,1)%2?"男":"女") 
  };

  $f.addMethod("idcard", function(value, element) {
    return this.optional(element) || isIDCard(value) === true;
  }, "请输入一个有效的身份证号码");

  core.each($patterns, function(re, name) {
    $f.addPattern(name, re, $messages[name]);
  });

  var $ajax = core.ajax = function(url) {
    this.url = url;
    this.xhr = $ajax.create();
  };
  core.extend($ajax, {
    create: function() {
      if (window.XMLHttpRequest) {
        return new XMLHttpRequest();
      } else if (window.ActiveXObject) {
        return new ActiveXObject("Microsoft.XMLHTTP");
      }
      core.error("XMLHTTP object could be created.");
    },

    param: function(a) {
      var s = [];

      // If an array was passed in, assume that it is an array
      // of form elements
      if (core.isArray(a))
      // Serialize the form elements
        core.each(a, function() {
          s.push(encodeURIComponent(this.name) + "=" + encodeURIComponent(this.value));
        });
      // Otherwise, assume that it's an object of key/value pairs
      else
      // Serialize the key/values
        for (var j in a)
        // If the value is an array then the key names need to be repeated
          if (core.isArray(a[j]))
            core.each(a[j], function() {
              s.push(encodeURIComponent(j) + "=" + encodeURIComponent(this));
            });
          else
            s.push(encodeURIComponent(j) + "=" + encodeURIComponent(a[j]));

      // Return the resulting serialization
      return s.join("&").replace(/%20/g, "+");
    },

    prototype: {
      request: function(option) {
        var data, url = this.url,
          xhr = this.xhr,
          method = option.method || "POST";

        if (this.isrun) {
          this.isrun = false;
          xhr.abort();
        }

        this.isrun = true;
        xhr.onreadystatechange = function() {
          if (xhr.readyState == 4) {
            option.callback(xhr.responseText, xhr.status, xhr);
          }
        };

        try {
          data = $ajax.param(option.data);
          xhr.open(method, url, true);
          xhr.setRequestHeader("Content-Length", data.length);
          xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
          xhr.send(data);
        } catch (e) {
          if (option.callback)
            option.callback(xhr.responseText, xhr.status, xhr, e);
        } finally {
          this.isrun = false;
        }
      },

      abort: function() {
        this.xhr && this.xhr.abort();
      }
    }
  });

  var ajaxMessages = {
    ing: "正在请求...",
    failed: "验证失败",
    error: "请求失败"
  },

  ajaxCallback = function(text) {
    return text == 1;
  };

  $f.addMethod("ajax", function(value, element, param) {
    if (this.optional(element) && !value)
      return true;

    var me = this,
      attr = this.attr(element),
      key = this.getKey(element),
      messages = this.settings.messages[key],
      url = core.isString(param) ? param : param.url,
      callback = param.callback || ajaxCallback,
      data = {};

    if (!messages)
      messages = this.settings.messages[key] = {};

    if (!attr.ajax) {
      attr.ajax = new $ajax(url);
      attr.ajaxMessage = messages["ajax"];
    }

    messages["ajax"] = ajaxMessages.ing;
    data[!core.isString(param) && param.key || key] = value;

    attr.ajax.request({
      data: data,
      callback: function(text, status, xhr, error) {
        var result = status == 200 && callback(text),
          message = (messages["ajax"] = status == 200 ?
            attr.ajaxMessage :
            ajaxMessages.error) ||
            ajaxMessages.failed;

        me.addResult(result, element, message);
        me.hideErrors();
        me.showErrors();
      }
    });
    return this.optional(element) || false;
  }, ajaxMessages.ing);

  function watermark(el, text, useContainer) {
    if (!(this instanceof watermark))
      return new watermark(el, text);

    var place = document.createElement("span");

    if (useContainer) {
      var container = document.createElement("span");
      el.parentNode.insertBefore(container, el);
      container.appendChild(place);
      container.appendChild(el);
      dom.addClass(container, "w-wrapper");
    } else
      el.parentNode.insertBefore(place, el);

    dom.addClass(place, "w-label");
    place.innerHTML = text;
    place.style.height = place.style.lineHeight = el.offsetHeight + "px";

    function hideIfHasValue() {
      if (el.value)
        dom.addClass(place, "w-hide");
    }
    hideIfHasValue();
    event.add(el, "focusin", function() {
      hideIfHasValue()
      if (!el.value)
        dom.addClass(place, "w-active");
    });
    event.add(el, "focusout", function() {
      if (!el.value)
        dom.removeClass(place, "w-active w-hide");
    });
    event.add(el, "keyup", hideIfHasValue);
    event.add(place, "click", function() {
      hideIfHasValue();
      dom.focus(el);
    });
  }

  core.extend(true, $f, {
    watermark: watermark,

    getKeyForAspnet: function(el) {
      if (!el.name) return;
      if (core.inArray("__EVENTTARGET __EVENTARGUMENT __VIEWSTATE __EVENTVALIDATION".split(" "), el.name) > -1) return;
      var clientIds = el.name.split("$"),
        len = clientIds.length,
        last;
      if (len === 1)
        return clientIds[0];
      last = clientIds[len - 1];
      if ($f.core.isDigit(last))
        return clientIds[len - 2];
      return last;
    },

    placeHolder: function(elems) {
      var ph, support = "placeholder" in document.createElement("input");
      if (!support) {
        core.each(elems, function(el) {
          ph = dom.attr(el, "placeholder");
          if (ph) watermark(el, ph);
        });
      }
    },

    bubbleTip: function(label, element) {
      dom.addClass(label, "b-b e-bubble");
      $f.appendContainer.call(this, label, element);
      label.innerHTML = '<span class="b-cor b-cor10 e-b-bot"></span><span class="b-cor b-cor10 e-b-top"></span><p class="b-con"></p>';
      label.textNode = label.childNodes[2];
      label.vtarget = element;
    },

    arrowTip: function(label, element) {
      dom.addClass(label, "b-b e-arrow");
      $f.appendContainer.call(this, label, element);
      label.innerHTML = '<span class="b-cor b-cor15"></span><p class="b-con"></p>';
      label.textNode = label.childNodes[1];
      label.vtarget = element;
    },

    tipText: function(label, message) {
      label.textNode.innerHTML = message;
      $f.appendContainer.call(this, label, label.vtarget);
    },

    prototype: {
      placeHolder: function() {
        $f.placeHolder(this.elements());
      }
    }
  });
})(window.fancy);
