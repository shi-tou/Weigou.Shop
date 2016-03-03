/*公共JS类*/

//显示当前时间
function ShowTime(obj) {
    var now_time = new Date();
    var years = now_time.getFullYear();
    var months = now_time.getMonth();
    var dates = now_time.getDate();
    var days = now_time.getDay();
    var today = years + "年" + (months + 1) + "月" + dates + "日";
    var weeks;
    if (days == 0)
        weeks = "星期日";
    if (days == 1)
        weeks = "星期一";
    if (days == 2)
        weeks = "星期二";
    if (days == 3)
        weeks = "星期三";
    if (days == 4)
        weeks = "星期四";
    if (days == 5)
        weeks = "星期五";
    if (days == 6)
        weeks = "星期六";
    var hours = now_time.getHours();
    var minutes = now_time.getMinutes();
    var seconds = now_time.getSeconds();
    var timer = hours;
    timer += ((minutes < 10) ? ":0" : ":") + minutes;
    timer += ((seconds < 10) ? ":0" : ":") + seconds;
    var doc = document.getElementById(obj);
    doc.innerHTML = today + " " + timer + " " + weeks;
    setTimeout("ShowTime('" + obj + "')", 1000);
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
var JsonToParam = function(json) {
    if (json != null)
        var param = [];
    for (var key in json) {
        param.push(key + '=' + json[key]);
    }

    return param.join('&');
}

//获取状态
function GetStatus(s) {
    switch (parseInt(s)) {
        case EnumStatus.Disabled:
            return '<span style="color:red;">停用</span>';
        case EnumStatus.Normal:
            return '<span style="color:green;">启用</span>';
        default: return '';
    }
}
//性别
function GetSex(s) {
    switch (parseInt(s)) {
        case EnumSex.M:
            return '男';
        case EnumSex.F:
            return '女';
        default: return '';
    }
}
function GetOrderStatus(s) {
    switch (parseInt(s)) {
        case 0:
            return '订单提交';
        case 1:
            return SetBlue('已付款');
        case 2:
            return SetRed('申请退款');
        case 3:
            return '<span style="color:#ddd">订单取消</span>';
        case 4:
            return SetGreen('订单完成');
        default: return '';
    }
}
//账户/积分操作类型
function FormatChangeType(v) {
    switch (parseInt(v)) {
        case EnumChangeType.RegistAdd:
            return '登记赠送';
        case EnumChangeType.ManualAdd:
            return '手动增加';
        case EnumChangeType.ManualMinus:
            return '手动扣减';
        case EnumChangeType.ConsumeAdd:
            return '消费赠送';
        case EnumChangeType.ConsumeMinus:
            return '消费扣减';
        case EnumChangeType.RechargeAdd:
            return '会员充值';
        default: return '';
    }
}
//布尔值
function GetBoolen(v) {
    if (v == true || v == 'True' || v == 'true') {
        return '<img src="/style/images/s1.png" />';
    }
    else
        return '<img src="/style/images/s0.png" />';
}
//设置样式->红
function SetRed(v) {
    return '<span style="color:red">'+ v +'</span>';
}
//设置样式->蓝
function SetBlue(v) {
    return '<span style="color:blue">' + v + '</span>';
}
//设置样式->绿
function SetGreen(v) {
    return '<span style="color:green">' + v + '</span>';
}
//格式化时间(yyyy-MM-dd HH:mm)
function FormatDateTime(date) {
    if (date == '' || date == null)
        return '';
    date = date.substr(0, 4) + "/" + date.substr(5, 2) + "/" + date.substr(8, 2) + " " + date.substr(11, 5);
    date = new Date(date);
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    var h = date.getHours() > 9 ? date.getHours().toString() : '0' + date.getHours();
    var mm = date.getMinutes() > 9 ? date.getMinutes().toString() : '0' + date.getMinutes();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d) + " " + h + ":" + mm;
}

//格式化年月日(yyyy-MM-dd)
function FormatDate(date) {
    if (date == '' || date == null)
        return '';
    date = date.substr(0, 4) + "/" + date.substr(5, 2) + "/" + date.substr(8, 2) + " " + date.substr(11, 5);
    date = new Date(date);
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
}
//格式化月日(MM-dd)
function FormatDay(date) {
    if (date == '' || date == null)
        return '';
    date = date.substr(0, 4) + "/" + date.substr(5, 2) + "/" + date.substr(8, 2) + " " + date.substr(11, 5);
    date = new Date(date);
    var m = date.getMonth() + 1;
    var d = date.getDate();
    return (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
}
//执行ajax
function DoAjax(url, data, callback) {
    ShowLoading();
    $.ajax({
        url: dealAjaxUrl(url),
        data: data,
        dataType: 'json',
        type: 'POST',
        success: function (d) {
            if(callback!=null)
                callback(d);
            CloseLoading();
        }
    });
}
//是否存在指定函数 
function isExitsFunction(funcName) {
    try {
        if (typeof (eval(funcName)) == "function") {
            return true;
        }
    } catch (e) { }
    return false;
}
//获取单选框RadioButtonList的值
function getRadioValue(name) {
    var v = 0;
    $('input[name=' + name + ']').each(function() {
        if ($(this).attr("checked")) {
            v = $(this).val();
        }
    });
    return v;
}
function setRadioValue(name, v) {
    $('input[name=' + name + ']').each(function() {
        if (v == $(this).val()) {
            $(this).attr("checked", true);
        }
    });
}
/*layer加载层方法
=================================*/
var loadi;
//需关闭加载层时，执行layer.close(loadi)即可
function ShowLoading() {
    loadi = layer.load(0, { time: 15 * 1000 });//设定最长等待15秒 
}
//关闭加载层
function CloseLoading() {
    layer.close(loadi);
}

//table高度
$(function () {
    $("#ListTable").height($(window).height());
});
function PrintThisPage() {
    ///	<summary>
    ///	打印页JavaScript
    ///	</summary>
    var sOption = "toolbar=yes,location=no,directories=yes,menubar=yes,";
    sOption += "scrollbars=yes,width=1000,height=600,left=100,top=25";

    var sWinHTML = document.getElementById('contentstart').innerHTML;
    //    var formHTML = document.getElementById('form').innerHTML;

    //var skinPath = document.getElementById("hdSkinCssPath").value;
       
    var winprint = window.open("", "", sOption);
    winprint.document.open();
    winprint.document.write('<html>');
    winprint.document.write('<head><meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>');
    winprint.document.write('<link href="/Hui_3.2/css/bootstrap.min.css?v=3.4.0" rel="stylesheet" type="text/css"/>');
    winprint.document.write('<link href="/Hui_3.2/css/font-awesome.min.css?v=4.3.0" rel="stylesheet" type="text/css"/>');
    winprint.document.write('<link href="/Hui_3.2/css/style.min.css?v=3.2.0" rel="stylesheet" type="text/css"/>');
    winprint.document.write('<script src="/Hui_3.2/js/jquery-2.1.1.min.js" type="text/javascript"></script>');
    winprint.document.write('<script src="/Hui_3.2/js/bootstrap.min.js?v=3.4.0" type="text/javascript"></script>');
    winprint.document.write('<script src="/Hui_3.2/js/plugins/layer/layer.min.js" type="text/javascript"></script>');
    winprint.document.write('<script src="/JScript/common.js" type="text/javascript"></script>');
    //winprint.document.write('<link rel="stylesheet" type="text/css" href="' + skinPath + '">');
    winprint.document.write('</head>');
    winprint.document.write('<body onload="JavaScript:window.print();">');
    //    winprint.document.write('<table border="0" cellpadding="5" cellspacing="5" width="100%">');
    //    winprint.document.write('<tr><td>');
    //var logoHtml = "<img height='45' src='" + $("#hdimageurl").val() + "' alt=''>";
    winprint.document.write('<br/><div id="header" style="width:980px;">' + "<br/><br/>" + '</div>');
    //    winprint.document.write(headerHTML);
    //    winprint.document.write('</div>');
    //    winprint.document.write('</td></tr>');
    //    //    winprint.document.write('<tr><td>');
    //    //    winprint.document.write(formHTML);
    //    //    winprint.document.write('</td></tr>');
    //    winprint.document.write('<tr><td>');
    winprint.document.write('<br/><div style="width:980px; margin:0 auto;">' + sWinHTML + '</div><br/>');
    //    winprint.document.write('</td></tr>');
    //    winprint.document.write('</table>');

    winprint.document.write('</body></html>');
    winprint.document.close();
    winprint.focus();
}
