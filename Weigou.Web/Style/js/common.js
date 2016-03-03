//layer弹出方法
var i;
function loadLayer(title, url, w, h) {
    i = layer.open({
        type: 2,
        title: title,
        shadeClose: true,
        shade: [0.2, '#333', true],
        area: [w, h],
        content: url //iframe的url
    });
}

//成功
function successLayer(msg, fun) {
    layer.close(i);
    if (msg != undefined && msg != null && msg != '') {
        parent.layer.msg(msg, { icon: 1 });
    }
    if (fun != undefined && fun != null && fun != '') {
        fun();
    }
}

//失败
function failureLayer(msg) {
    if (msg != undefined && msg != null && msg != '') {
        parent.layer.msg(msg, { icon: 2 });
    }
}

//验证表单错误提示
//derection 1:上方 2:右方 3:下方 4:左方
function errorLayertips(msg, id, derection) {
    layer.tips(msg, id, {
        tips: [derection, '#F08080'],
        tipsMore: true
    });
}

//对每一个请求的URL进行处理，确保每一次请求都是新的,
//否则Url相同视为相同请求，浏览器则不执行，而是返回上一次请求结果（避免缓存问题)
function dealAjaxUrl(url) {
    var guid = new Date().getTime().toString(36);
    var ajaxurl;
    if (url.lastIndexOf("?") > -1)//url带参数
        ajaxurl = url + "&ajaxGuid=" + guid;
    else //url不带参数
        ajaxurl = url + "?ajaxGuid=" + guid;
    // ajaxurl += "&url=" + location.href; //加上URL参数，用于出现错误时返回上一页
    return ajaxurl;
}
//将json参数转换为Url参数
var JsonToParam = function (json) {
    if (json != null)
        var param = [];
    for (var key in json) {
        param.push(key + '=' + json[key]);
    }

    return param.join('&');
}

//转换成人民币格式
function formatRMB(num) {
    num = num.toString().replace(/\$|\,/g, '');
    if (isNaN(num))
        num = "0";
    sign = (num == (num = Math.abs(num)));
    num = Math.floor(num * 100 + 0.50000000001);
    cents = num % 100;
    num = Math.floor(num / 100).toString();
    if (cents < 10)
        cents = "0" + cents;
    for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3) ; i++)
        num = num.substring(0, num.length - (4 * i + 3)) +
num.substring(num.length - (4 * i + 3));
    return (((sign) ? '' : '-') + num + '.' + cents);
}