/*!
 * Fancy Validate v0.1.9 - JavaScript Form Validation
 * Copyright 2013 cormin.lu@gmail.com
 * MIT Licensed
 * Build 09/22/2013
 */
(function(window, undefined) {
  var document = window.document,

  // Check if a string has a non-whitespace character in it
  rnotwhite = /\S/,

  // Used for trimming whitespace
  rtrimLeft = /^[\s\xA0]+/,
  rtrimRight = /[\s\xA0]+$/,

  // format
  rformat = /\{(\d+)\}/,
  rformats = {},

  // Save a reference to some core methods
  toString = Object.prototype.toString,
  hasOwn = Object.prototype.hasOwnProperty,
  slice = Array.prototype.slice,
  indexOf = Array.prototype.indexOf,
  trim = String.prototype.trim,

  // [[Class]] -> type pairs
  class2type = (function() {
    var c2t = {},
      ary = "Boolean Number String Function Array Date RegExp Object".split(" "),
      i = 0,
      l = ary.length;
    for (;i < l;) {
      c2t["[object " + ary[i] + "]"] = ary[i++].toLowerCase();
    }
    return c2t;
  })(),

  $core = {
    //Use native String.trim function wherever possible
    trim: trim ?
		  function(text) {
		    return text == null ? "" :
				  trim.call(text);
		  } :

    // Otherwise use our own trimming functionality
		  function(text) {
		    return text == null ? "" :
				  text.toString().replace(rtrimLeft, "").replace(rtrimRight, "");
		  },

    startsWith: function(a, b) {
      return a.slice(0, b.length) == b;
    },

    endsWith: function(a, b) {
      return a.slice(-b.length) == b;
    },

    makeArray: function(args) {
      return slice.call(args, 0);
    },

    inArray: indexOf ?
      function(array, element, i) {
        return indexOf.call(array, element, i);
      } :

      function(array, element, i) {
        var len = array.length;
        i = i ? i < 0 ? Math.max(0, len + i) : i : 0;
        for (;i < len;i++) {
          if (i in array && array[i] === element) {
            return i;
          }
        }
        return -1;
      },

    remove: function(array, i) {
      return i > -1 && array.splice(i, 1);
    },

    format: function(source, params) {
      if (arguments.length == 1)
        return function() {
          var args = $core.makeArray(arguments);
          args.unshift(source);
          return $core.format.apply(this, args);
        };
      if (arguments.length > 2 && !$core.isArray(params)) {
        params = $core.makeArray(arguments).slice(1);
      }
      if (!$core.isArray(params)) {
        params = [params];
      }
      $core.each(params, function(p, ix) {
        source = source.replace(
          rformats[ix] || (rformats[ix] = new RegExp("\\{" + ix + "\\}", "g")),
          p);
      });
      return source;
    },

    hasFormat: function(source) {
      return rformat.test(source);
    },

    parse: function(data) {
      if (!$core.isString(data) || $core.isEmpty(data)) {
        return null;
      }

      data = $core.trim(data);

      return (new Function("return " + data))();
    },

    each: function(obj, callback, context) {
      var name, i = 0,
        len = obj.length,
        isObj = len === undefined;

      if (isObj) {
        for (name in obj) {
          if (callback.call(context || obj[name], obj[name], name) === false) {
            break;
          }
        }
      } else {
        for (;i < len;) {
          if (callback.call(context || obj[i], obj[i], i++) === false)
            break;
        }
      }
      return obj;
    },

    getType: function(obj) {
      return obj == null ? String(obj) :
			  class2type[toString.call(obj)] || "object";
    },

    isType: function(obj, value) {
      return $core.getType(obj) === value;
    },

    isNumeric: function(obj) {
      return !isNaN(parseFloat(obj)) && isFinite(obj);
    },

    isDigit: function(obj) {
      return $core.isNumeric(obj) && obj % 1 == 0;
    },

    isDate: function(obj) {
      return $core.isType(obj, "date") && !isNaN(obj);
    },

    isEmpty: function(obj) {
      return !rnotwhite.test(obj);
    },

    isEmptyObject: function(obj) {
      for (var name in obj) {
        return false;
      }
      return true;
    },

    isPlainObject: function(obj) {
      // Must be an Object.
      // Because of IE, we also have to check the presence of the constructor property.
      // Make sure that DOM nodes and window objects don't pass through, as well
      if (!obj || !$core.isObject(obj) || obj.nodeType) {
        return false;
      }

      try {
        // Not own constructor property must be Object
        if (obj.constructor &&
				  !hasOwn.call(obj, "constructor") &&
				  !hasOwn.call(obj.constructor.prototype, "isPrototypeOf")) {
          return false;
        }
      } catch (e) {
        // IE8,9 Will throw exceptions on certain host objects #9897
        return false;
      }

      // Own properties are enumerated firstly, so to speed up,
      // if last one is own, then all properties are own.

      var key;
      for (key in obj) { }

      return key === undefined || hasOwn.call(obj, key);
    },

    extend: function() {
      var options, name, src, copy, copyIsArray, clone,
		    target = arguments[0] || {},
		    i = 1,
		    length = arguments.length,
		    deep = false;

      // Handle a deep copy situation
      if (typeof target === "boolean") {
        deep = target;
        target = arguments[1] || {};
        // skip the boolean and the target
        i = 2;
      }

      // Handle case when target is a string or something (possible in deep copy)
      if (typeof target !== "object" && !$core.isFunction(target)) {
        target = {};
      }

      // extend jQuery itself if only one argument is passed
      /*if (length === i) {
      target = this;
      --i;
      }*/

      for (;i < length;i++) {
        // Only deal with non-null/undefined values
        if ((options = arguments[i]) != null) {
          // Extend the base object
          for (name in options) {
            src = target[name];
            copy = options[name];

            // Prevent never-ending loop
            if (target === copy) {
              continue;
            }

            // Recurse if we're merging plain objects or arrays
            if (deep && copy && ($core.isPlainObject(copy) || (copyIsArray = $core.isArray(copy)))) {
              if (copyIsArray) {
                copyIsArray = false;
                clone = src && $core.isArray(src) ? src : [];

              } else {
                clone = src && $core.isPlainObject(src) ? src : {};
              }

              // Never move original objects, clone them
              target[name] = $core.extend(deep, clone, copy);

              // Don't bring in undefined values
            } else if (copy !== undefined) {
              target[name] = copy;
            }
          }
        }
      }

      // Return the modified object
      return target;
    },

    // throw error
    error: function(arg) {
      var e = $core.isObject(arg) ? arg : new Error(arg);
      throw e;
    }
  };

  $core.each("Boolean String Function Array RegExp Object".split(" "), function(name) {
    $core["is" + name] = function(obj) {
      return $core.isType(obj, name.toLowerCase());
    };
  });

  var $dom = {
    ready: function(fn) {
      // "uninitalized"、"loading"、"interactive"、"complete" 、"loaded"
      /in/.test(document.readyState) ? window.setTimeout(function() { $dom.ready(fn); }, 10) : fn();
    },

    get: function(obj) {
      return $dom.isElement(obj) ? obj :
        document.getElementById(obj);
    },

    isElement: function(element) {
      return element && element.nodeType === 1;
    },

    attr: function(element, name, val) {
      if ($dom.isElement(element)) {
        if (val === undefined) return element[name] || element.getAttribute(name);
        element.setAttribute(name, val);
      }
      return null;
    },

    addClass: function(element, cls) {
      if ($dom.isElement(element) && cls) {
        if (!element.className)
          element.className = cls;
        else {
          var cls2 = cls.split(" ");
          if (cls2.length > 1) {
            $core.each(cls2, function(cv) {
              $dom.addClass(element, cv);
            });
          } else if ($core.inArray(element.className.split(" "), cls) == -1)
            element.className += " " + cls;
        }
      }
    },

    removeClass: function(element, cls) {
      if ($dom.isElement(element) && element.className && cls) {
        var classes = element.className.split(" ");
        var cls2 = cls.split(" ");
        if (cls2.length > 1) {
          $core.each(cls2, function(cv) {
            $dom.removeClass(element, cv);
          });
        } else if ($core.remove(classes, $core.inArray(classes, cls)))
          element.className = classes.join(" ");
      }
    },

    toggle: function(element, val) {
      element.style.display = !val ? "none" : "";
    },
    
    remove: function(element) {
      element && element.parentNode && element.parentNode.removeChild(element);
    },

    offset: function(el) {
      /*if ("getBoundingClientRect" in document.documentElement) {
      try {
      return el.getBoundingClientRect();+scrollTop-clientTop
      } catch (e) { }
      }*/

      var t = 0, l = 0;
      do {
        t += el.offsetTop;
        l += el.offsetLeft;
      } while (el = el.offsetParent);
      return { top: t, left: l };
    },

    setRange: function(element, start, end) {
      if (element.setSelectionRange) {
        element.focus();
        element.setSelectionRange(start, end);
        //element.selectionStart = start;
        //element.selectionEnd = end;
      } else if (element.createTextRange) {
        var range = element.createTextRange();
        range.collapse(true);
        range.moveEnd("character", end);
        range.moveStart("character", start);
        range.select();
      }
      //element.focus();
    },

    getRange: function(element) {
      var s, e, range, duplicate, len,
        val = element.value;

      if (element.setSelectionRange) {
        s = element.selectionStart;
        e = element.selectionEnd;
      } else if (element.createTextRange) {
        range = document.selection.createRange();
        duplicate = range.duplicate();

        if (!$dom.istextarea(element)) {
          len = val.length;
          duplicate.moveEnd("character", len);
          s = (duplicate.text == "" ? len : val.lastIndexOf(duplicate.text));

          duplicate = range.duplicate();
          duplicate.moveStart("character", -len);
          e = duplicate.text.length;
        } else { /*for textarea*/
          duplicate.moveToElementText(element);
          duplicate.setEndPoint("EndToEnd", range);
          len = range.text.length;
          s = duplicate.text.length - len;
          e = s + len;
        }
      }

      return {
        start: s,
        end: e,
        text: val.substring(s, e),
        replace: function(st) {
          return element.value.substring(0, s) + st + element.value.substring(e, element.value.length);
        }
      };
    },

    getRange2: function(element) {
      var s, e, rng;
      if (element.setSelectionRange) {
        s = element.selectionStart;
        e = element.selectionEnd;
      } else { /*not work for ie6 textarea*/
        rng = document.selection.createRange();
        s = 0 - rng.duplicate().moveStart("character", -100000);
        e = s + rng.text.length;
      }
      return {
        start: s,
        end: e,
        text: element.value.substring(s, e),
        replace: function(st) {
          return element.value.substring(0, s) + st + element.value.substring(e, element.value.length);
        }
      };
    },

    focus: function(element) {
      if (element.istext === undefined)
        element.istext = $dom.istextarea(element) || $dom.isItext(element);
      try {
        if (element.istext && element.value)
          $dom.setRange(element, 0, element.value.length);
        else
          element.focus && element.focus();
      } catch (ex) { } // ignore IE throwing errors when focusing hidden elements
    },

    // log to console
    log: function() {
      window.console && console.log($core.makeArray(arguments).join(" "));
    }
  };

  $core.each("radio checkbox text".split(" "), function(name) {
    $dom["isI" + name] = function(element) {
      return $dom.isElement(element) && element.nodeName.toLowerCase() === "input" && element.type === name;
    }
  });
  $core.each("input textarea select option label".split(" "), function(name) {
    $dom["is" + name] = function(element) {
      return $dom.isElement(element) && element.nodeName.toLowerCase() === name;
    }
  });

  // fix nonW3C event
  function $event(e) {
    this.event = e;
    this.type = e.type;
    this.target = e.target || e.srcElement;
  }
  $core.extend($event, {
    specials: {
      focusin: "focus",
      focusout: "blur"
    },

    add: document.addEventListener ?
      function(element, type, fn) {
        var spec = $event.specials[type];
        element.addEventListener(spec || type, fn, !!spec);
      } :

      function(element, type, fn) {
        if (type == "input" && "onpropertychange" in element) {
          element.onpropertychange = function() {
            if (window.event.propertyName == "value") {
              fn.call(this, window.event);
            }
          };
        } else
          element.attachEvent("on" + type, fn);
      },

    remove: document.removeEventListener ?
      function(element, type, fn) {
        var spec = $event.specials[type];
        element.removeEventListener(spec || type, fn, !!spec);
      } :

      function(element, type, fn) {
        if (type == "input" && "onpropertychange" in element) {
          element.onpropertychange = null;
        } else
          element.detachEvent("on" + type, fn);
      },

    fix: function(e) {
      return new $event(e || window.event);
    },

    prototype: {
      stopPropagation: function() {
        var e = this.event;
        e.stopPropagation && e.stopPropagation();
        e.cancelBubble = true;
      },

      preventDefault: function() {
        var e = this.event;
        e.preventDefault && e.preventDefault();
        e.returnValue = false;
      },

      stop: function() {
        this.preventDefault();
        this.stopPropagation();
      }
    }
  });

  // for tips
  function $infoList() {
    this.list = [];
  }
  $core.extend($infoList.prototype, {
    first: function() {
      if (this.len()) {
        return this.list[0].key;
      }
    },

    len: function() {
      return this.list.length;
    },

    each: function(callback, context) {
      $core.each(this.list, callback, context);
    },

    find: function(key) {
      var i = -1;
      this.each(function(info, ix) {
        if (info.key == key) {
          i = ix;
          return false;
        }
      });
      return i;
    },

    findEntry: function(key) {
      var i = this.find(key);
      if (i > -1)
        return this.list[i];
    },

    remove: function(key) {
      $core.remove(this.list, this.find(key));
    },

    add: function(key, message) {
      this.remove(key);
      this.list.push({
        key: key,
        message: message || ""
      });
    },

    reset: function() {
      this.list.length = 0;
    }
  });

  function notImplemented(element) {
    $core.error("not implemented");
  }

  function $validator(options) {
    if (!(this instanceof $validator))
      return new $validator(options);
    this.init(options);
  }
  $core.extend($validator, {
    version: .1,
    build: new Date(1323273600000),
    core: $core,
    dom: $dom,
    event: $event,

    defaults: {
      rules: {},
      messages: {}
    },

    messages: {
      required: "此字段是必需的",
      compareTo: "请再次输入相同的值",
      pattern: "输入的格式不匹配",
      fn: "输入不匹配",
      minlength: $core.format("请输入一个长度最少是 {0} 的字符串"),
      maxlength: $core.format("请输入一个长度最多是 {0} 的字符串"),
      rangelength: $core.format("请输入一个长度介于 {0} 和 {1} 之间的字符串"),
      min: $core.format("请输入一个最小为 {0} 的值"),
      max: $core.format("请输入一个最大为 {0} 的值"),
      range: $core.format("请输入一个介于 {0} 和 {1} 之间的值"),
      digit: "请只输入整数",
      numeric: "请输入有效的数字",
      prefix: $core.format("请输入以 {0} 开头"),
      suffix: $core.format("请输入以 {0} 结尾"),
      equal: $core.format("请输入与 {0} 相等的值")
    },

    methods: {
      required: function(value, element) {
        return this.getLen(value, element) > 0;
      },

      compareTo: function(value, element, param) {
        var target = this.getElement(param);
        return target && value == this.getValue(target);
      },

      pattern: function(value, element, param) {
        var re = $core.isRegExp(param) ? param : new RegExp(param);
        return this.optional(element) || re.test(value);
      },

      fn: function(value, element, param) {
        var fn = $core.isFunction(param) ? param : $core.parse(param);
        return this.optional(element) || fn.call(this, value, element);
      },

      minlength: function(value, element, param) {
        return this.optional(element) || this.getLen(value, element) >= param;
      },

      maxlength: function(value, element, param) {
        return this.optional(element) || this.getLen(value, element) <= param;
      },

      rangelength: function(value, element, param) {
        var len = this.getLen(value, element);
        return this.optional(element) || len >= param[0] && len <= param[1];
      },

      min: function(value, element, param) {
        return this.optional(element) || value >= param;
      },

      max: function(value, element, param) {
        return this.optional(element) || value <= param;
      },

      range: function(value, element, param) {
        return this.optional(element) || value >= param[0] && value <= param[1];
      },

      numeric: function(value, element) {
        return this.optional(element) || $core.isNumeric(value);
      },

      digit: function(value, element) {
        return this.optional(element) || $core.isDigit(value);
      },

      prefix: function(value, element, param) {
        return this.optional(element) || $core.startsWith(value, param);
      },

      suffix: function(value, element, param) {
        return this.optional(element) || $core.endsWith(value, param);
      },

      equal: function(value, element, param) {
        return this.optional(element) || value == param;
      }
    },

    addMethod: function(name, fn, message) {
      if (!$core.isFunction(fn))
        $core.error("method is not a function " + fn);
      this.methods[name] = fn;
      if (message)
        this.messages[name] = message;
    },

    patterns: {},

    addPattern: function(name, re, message) {
      if (!$core.isRegExp(re))
        $core.error("argument is not a regular expression " + re);
      var me = this;
      me.patterns[name] = re;
      me.addMethod(name, function(value, element, param) {
        return this.optional(element) || me.patterns[name].test(value);
      }, message);
    },

    prototype: {
      init: function(options) {
        this.settings = $core.extend(true, {}, $validator.defaults, options);
        this.elementList = [];
        this.mismatch = "dependency-mismatch";
        this.valids = new $infoList();
        this.errors = new $infoList();
      },

      register: function() {
        var element = arguments[0],
          i = 1,
          l = arguments.length,
          key = this.getKey(element),
          rules = this.settings.rules,
          msgs = this.settings.messages,
          opt;

        if (!key || l < 2) return false;

        for (;i < l;) {
          if (opt = arguments[i++]) {
            rules[key] = $core.extend(rules[key], opt.rules);
            msgs[key] = $core.extend(msgs[key], rules[key].messages, opt.messages);
            delete rules[key].messages;
          }
        }
        if ($core.isEmptyObject(rules[key])) {
          delete rules[key];
          delete msgs[key];
          return false;
        }

        if ($core.inArray(this.elements(), element) == -1)
          this.elementList.push(element);
        return true;
      },

      optional: function(element) {
        return !this.constructor.methods.required.call(this,
          this.getValue(element), element) &&
            this.mismatch;
      },

      getKey: notImplemented,

      getValue: notImplemented,

      getLen: notImplemented,

      numOfErrors: function() {
        return this.errors.len();
      },

      valid: function() {
        return this.numOfErrors() == 0;
      },

      reset: function() {
        this.valids.reset();
        this.errors.reset();
      },

      elements: function() {
        return this.elementList;
      },

      checkAll: function() {
        var i = 0,
          elems = this.elements(),
          l = elems.length;

        this.reset();
        for (;i < l;) {
          this.check(elems[i++]);
        }
        return this.valid();
      },

      check: function(element) {
        var method, result, parameters, fn,
          key = this.getKey(element),
          rules = this.settings.rules[key];

        if (!key || element.disabled || $core.isEmptyObject(rules)) return;

        for (method in rules) {
          parameters = rules[method];
          fn = this.constructor.methods[method];
          if (!fn) $core.error("method " + method + " not found");

          try {
            result = fn.call(this, this.getValue(element), element, parameters);

            if (result == this.mismatch) continue;

            if (!result) {
              this.formatAndAdd(element, method, parameters);
              return false;
            }
          } catch (e) {
            $core.error(e);
          }
        }

        return this.addResult(result, element); //true
      },

      addResult: function(result, element, message) {
        if (result === false) {
          this.valids.remove(element);
          this.errors.add(element, message);
          return result;
        }
        this.errors.remove(element);
        if (result == this.mismatch) {
          this.valids.remove(element);
          return result;
        }
        this.valids.add(element);
        return true;
      },

      formatAndAdd: function(element, method, parameters) {
        var message = this.defaultMessage(element, method);
        if ($core.isFunction(message)) {
          message = message.call(this, parameters);
        } else if ($core.hasFormat(message)) {
          message = $core.format(message, parameters);
        }
        this.addResult(false, element, message);
      },

      defaultMessage: function(element, method) {
        var name = this.getKey(element);
        return this.customMessage(name, method) ||
          element.title ||
          this.constructor.messages[method] ||
          "<strong>Warning: No message defined for " + name + "</strong>";
      },

      customMessage: function(name, method) {
        var m = this.settings.messages[name];
        return m && ($core.isString(m) ? m : m[method]);
      }
    }
  });

  function $fancy(form, options) {
    if (!(this instanceof $fancy))
      return new $fancy(form, options);
    this.form = $dom.get(form);
    if (!this.form) $core.error("form not found");
    this.init(options);
    $core.extend(true, this.settings, $fancy.defaults, options);
    this.initForm();
  }
  $core.extend($fancy, {
    appendNext: function(label, element) {
      var checkable = this.attr(element).checkable,
        pn = element.parentNode;
      do {
        element = element.nextSibling;
      } while (element && checkable && $dom.islabel(element));
      pn.insertBefore(label, element);
    },

    findNext: function(element, forId) {
      do {
        element = element.nextSibling;
      } while (element && !($dom.isElement(element) && this.isErrLabel(element, forId)));
      return element;
    },

    appendContainer: function(label, el) {
      var ost, w, body = document.body;
      do {
        ost = $dom.offset(el);
        w = el.offsetWidth;
      } while (ost.top == 0 && (el = el.parentNode) && el !== body);
      if ($dom.islabel(el.nextSibling)) w += el.nextSibling.offsetWidth;
      label.style.top = ost.top + "px"
      label.style.left = ost.left + w + "px";
      this.settings.container.appendChild(label);
    },

    findContainer: function(element, forId) {
      var label;
      $core.each(this.settings.container.childNodes, function(el) {
        if (this.isErrLabel(el, forId)) {
          label = el;
          return false;
        }
      }, this);
      return label;
    },

    _parent: function(element) {
      var attr = this.attr(element);
      if (!attr.offsetParent) {
        var pl = this.settings.findDepth || 0,
              body = document.body;

        do {
          element = element.parentNode;
        } while (pl-- > 0 && element && element !== body);
        attr.offsetParent = element;
      }
      return attr.offsetParent;
    },

    findLast: function(element, forId) {
      var label = $fancy._parent.call(this, element).lastChild;
      if (!this.isErrLabel(label, forId)) label = null;
      return label;
    },

    appendLast: function(label, element) {
      $fancy._parent.call(this, element).appendChild(label);
    },

    getListByKey: function(obj) {
      var key = $core.isString(obj) ? obj : this.getKey(obj),
        ret = [];

      $core.each(this.form.elements, function(el) {
        if (this.isValElement(el) && this.getKey(el) === key)
          ret.push(el);
      }, this);
      return ret;
    },

    parseAttr: (function() {
      /* required,  required */
      var noStrict = /[a-z]\w+\s*,|[a-z]\w+\s*$/i,
      /* required */
        nameOnly = /^[^:,]+$/,
      /* split message and rule */
        splitMsg = /,\s*messages\s*:/i,
      /*,required*/
        noStrictLeft = /,\s*([a-z]\w+)/ig,
      /*required,*/
        noStrictRight = /([a-z]\w+)\s*,/ig,
      /*required:1:1 required:true:1*/
        surplus = /:(?:(?:\d|true):)*/g;

      return function(meta) {
        if (!$core.isEmpty(meta)) {
          var options;
          if (nameOnly.test(meta)) {
            meta = meta + ":1";
          } else if (noStrict.test(meta)) {
            meta = meta.split(splitMsg);
            meta[0] = meta[0].replace(noStrictLeft, ",$1:1")
            .replace(noStrictRight, "$1:1,")
            .replace(surplus, ":");
            meta = meta.join(",messages:");
          }

          if (meta.charAt(0) != "{")
            meta = "{" + meta + "}";
          options = $core.parse(meta);
          if (!options.rules)
            options = { rules: options };
          return options;
        }
      };
    })(),

    ruleToAttr: function(element, rules) {
      var len = rules["maxlength"] || rules["rangelength"];
      if (len && (element.maxLength == -1 || element.maxLength == 2147483647)) {
        if ($core.isArray(len)) len = len[1];
        element.maxLength = len;
      }
    }
  });
  $core.extend(true, $fancy, $validator, {
    defaults: {
      ruleKey: "name",
      ruleAttr: "data-val",
      validCls: "valid",
      errorCls: "error",
      activeElCls: "fld-active",
      errorElCls: "fld-error",
      validElCls: "fld-valid",
      errorElement: "label",
      interceptInput: true,
      interceptSubmit: true,
      focusInvalid: true,
      submitRandom: true,
      highlight: function(element) {
        this.elementCls(element, this.settings.errorElCls, this.settings.validElCls);
      },
      unhighlight: function(element) {
        this.elementCls(element, this.settings.validElCls, this.settings.errorElCls);
      },

      labelText: function(label, message) {
        label.innerHTML = message;
      },
      showErrors: function() {
        this.errors.each(this.showLabel, this);
        this.valids.each(this.showLabel, this);
      },
      findLabel: $fancy.findNext,
      appendLabel: $fancy.appendNext
    },

    setDefaults: function(settings) {
      $core.extend($fancy.defaults, settings);
    }
  });

  $core.extend(true, $fancy.prototype, $validator.prototype, {
    initForm: function() {
      this.time = +new Date();
      this.uuid = 0;
      this.uukey = "fancy_expando";
      this.attrMap = {};
      $core.each(this.form.elements, this.parseRule, this);

      if (this.elements().length) {
        this.hideErrors();
        this.reset();
        this.bindEvents();
      } else
        this.debug("no elements want to validate");
    },
    
    isValElement: function(element) {
      return $dom.isinput(element) || $dom.isselect(element) || $dom.istextarea(element);
    },

    getKey: function(element) {
      var sets = this.settings,
        ruleKeyAlter = sets.ruleKeyAlter,
        getKey = sets.getKey;
      return ruleKeyAlter && $dom.attr(element, ruleKeyAlter) || getKey && getKey.call(this, element) || $dom.attr(element, sets.ruleKey);
    },

    getValue: function(element) {
      return element.value;
    },

    getLen: function(value, element) {
      var len = 0, attr = this.attr(element);

      if (attr.checkable || attr.multisel) {
        $core.each(attr.chklist || element, function(el) {
          if (el.checked || el.selected) len++;
        });
        return len;
      }
      return $core.trim(value).length;
    },

    getElement: function(obj) {
      return this.form[obj] || $dom.get(obj) || $f.getListByKey.call(this, obj)[0];
    },

    debug: function() {
      this.settings.debug && $dom.log.apply(null, arguments);
    },

    checkForm: function() {
      var ret = this.checkAll();
      this.showErrors();
      return ret;
    },

    parseRule: function(element) {
      if (!this.isValElement(element)) return;
      var key = this.getKey(element),
        sets = this.settings,
        meta = $core.trim($dom.attr(element, sets.ruleAttr));

      if (key && this.register(element, $fancy.parseAttr(meta))) {
        $fancy.ruleToAttr(element, sets.rules[key]);
        var attr = this.attr(element);
        if (!attr) {
          var expando = element[this.uukey] = key + (this.uuid++);
          attr = this.attrMap[expando] = { expando: expando, key: key };
          this.defineAttr(element, attr);
        }
        if (attr.checkable && element != attr.lastchk) {
          var i = $core.inArray(this.elements(), element)
          if (i > -1)
            $core.remove(this.elements(), i);
        }
      }
    },

    defineAttr: function(element, attr) {
      attr.checkable = $dom.isIradio(element) || $dom.isIcheckbox(element);
      attr.multisel = $dom.isselect(element) && element.multiple === true;

      if (attr.checkable) {
        var list = [],
          sets = this.settings,
          uukey = this.uukey,
          list2 = sets.getList && sets.getList.call(this, element) || this.getElement(element.name);

        if (!list2.length) list2 = [list2];
        $core.each(list2, function(el) {
          if (!el[uukey] || el[uukey] == attr.expando) {
            list.push(el);
            if (!$dom.attr(el, sets.ruleKey)) $dom.attr(el, sets.ruleKey, attr.key);
            el[uukey] = attr.expando;
          }
        });
        attr.chklist = list;
        attr.lastchk = list[list.length - 1];
      }
    },

    attr: function(element) {
      return this.attrMap[element[this.uukey]];
    },

    bindEvents: function(unload) {
      var m = $event[unload ? "remove" : "add"];
      //click: radio/checkbox/select/option
      if (this.settings.interceptInput)
        $core.each("focusin focusout input keyup click".split(" "), function(evtName) {
          m(this.form, evtName, this.onInput());
        }, this);

      if (this.settings.interceptSubmit)
        m(this.form, "submit", this.onSubmit());
    },
    
    unload: function() {
      var me = this,
        uukey = this.uukey,
        cls = this.settings.validElCls + " " + this.settings.errorElCls;
      $core.each(this.elements(), function(el) {
        me.elementCls(el, "", cls);
        $dom.remove(me.errorsFor(el));
        delete el[uukey];
      });
      this.bindEvents(true);
      this.reset();
      this.attrMap = {};
      this.uuid = 0;
    },

    onInput: function() {
      var me = this,
        sets = this.settings;

      return function(e) {
        var value, len, attr, evt = $event.fix(e),
          target = evt.target;

        if ($dom.isoption(target))
          target = target.parentNode;
        if (($dom.isselect(target) || $dom.isIradio(target) || $dom.isIcheckbox(target)) && (evt.type != "click" && evt.type != "keyup"))
          return;

        attr = me.attr(target);
        if (attr && sets.rules[attr.key]) {
          if (evt.type == "focusin" || evt.type == "focus") {
            $dom.addClass(target, sets.activeElCls);
          } else if (evt.type == "focusout" || evt.type == "blur") {
            $dom.removeClass(target, sets.activeElCls);
          }

          value = me.getValue(target);
          len = me.getLen(value, target);
          if (attr.prevVal != value || attr.prevLen != len) {
            attr.prevVal = value;
            attr.prevLen = len;
            if (attr.checkable)
              target = attr.lastchk;
              
            var eel = me.errors.findEntry(target) || me.valids.findEntry(target);
            eel && me.hideLabel(eel);
            if (me.check(target) === true && sets.autoTab) {
              me.focusNext(target);
            }
            var eel2 = me.errors.findEntry(target) || me.valids.findEntry(target);
            eel2 && me.showLabel(eel2);
          }
        }
      };
    },

    onSubmit: function() {
      var me = this,
        sets = this.settings;

      return function(e) {
        var evt = $event.fix(e);
        if (!me.checkForm()) {
          evt.stop();
          if (sets.focusInvalid)
            $dom.focus(me.errors.first());
          return false;
        }

        sets.submitRandom && me.addRandom();
        sets.beforeSubmit && sets.beforeSubmit.call(me, me.form, evt);

        if (sets.submitHandler) {
          evt.stop();
          sets.submitHandler.call(me, me.form, evt);
          return false;
        }
      };
    },

    addRandom: function() {
      var me = this;
      $core.each({
        e: this.elements().length,
        d: +new Date(),
        t: this.time,
        r: Math.random()
      }, function(val, n) {
        var name = (me.getKey(this) || this.id) + "_" + n,
          el = me.getElement(name);
        if (!el) {
          el = document.createElement("input");
          el.type = "hidden";
          this.insertBefore(el, this.firstChild);
        }
        el.name = name;
        el.value = val;
      }, this.form);
    },

    focusNext: function(element) {
      var elems = this.elements(),
        len = elems.length,
        i = $core.inArray(elems, element) + 1;

      if (i > 0 && i < len) {
        $dom.focus(elems[i]);
      }
    },

    showErrors: function() {
      this.settings.showErrors.call(this);
    },

    showLabel: function(obj) {
      var label = this.errorsFor(obj.key),
        sets = this.settings;

      $dom.removeClass(label, sets.validCls);
      $dom.addClass(label, sets.errorCls);
      sets.labelText.call(this, label, obj.message);

      if (obj.message || sets.validCls)
        $dom.toggle(label, true);

      if (obj.message)
        sets.highlight.call(this, obj.key);
      else {
        sets.unhighlight.call(this, obj.key);
        $dom.addClass(label, sets.validCls);
      }
    },

    hideLabel: function(obj) {
      $dom.toggle(this.errorsFor(obj.key));
    },

    hideErrors: function() {
      //this.errors.each(this.hideLabel, this);
      //this.valids.each(this.hideLabel, this);
      var me = this;
      $core.each(this.elements(), function(el) {
        $dom.toggle(me.errorsFor(el), false);
      });
    },

    elementCls: function(element, clsa, clsb) {
      var attr = this.attr(element);
      if (attr.checkable)
        $core.each(attr.chklist, function(el) {
          $dom.removeClass(el, clsb);
          $dom.addClass(el, clsa);
        });
      else {
        $dom.removeClass(element, clsb);
        $dom.addClass(element, clsa);
      }
    },

    errorsFor: function(element) {
      var attr = this.attr(element),
        label = attr.errLabel;

      if (!label) {
        var sets = this.settings,
          forId = element.id || attr.key,
          labelExpando = this.uukey + "_for";

        label = sets.findLabel.call(this, element, forId);

        if (!label || (label[labelExpando] && label[labelExpando] != expando)) {
          label = document.createElement(sets.errorElement);
          $dom.attr(label, "for", forId);
          $dom.attr(label, "fancy", forId);
          sets.appendLabel.call(this, label, element);
          attr.errLabel = label;
          label[labelExpando] = attr.expando;
        }
      }
      return label;
    },

    isErrLabel: function(label, forId) {
      return $dom.attr(label, "for") == forId &&
        $dom.attr(label, "fancy") == forId;
    }
  });

  $fancy.base = $validator;
  window.fancy = window.$f = $fancy;
})(window);