//文件名：common.js
//描述：公用js方法
//时间：2015-01-27
//作者：杨良斌

//获取url参数
function QueryString(key) {
    var value = "";
    var sURL = window.document.URL;
    if (sURL.indexOf("?") > 0) {
        var arrayParams = sURL.split("?");
        var arrayURLParams = arrayParams[1].split("&");
        for (var i = 0; i < arrayURLParams.length; i++) {
            var sParam = arrayURLParams[i].split("=");
            if ((sParam[0] == key) && (sParam[1] != "")) {
                value = sParam[1];
                break;
            }
        }
    }
    return trim(value);
}
//删除左右两端的空格
function trim(str) { 
    return str.replace(/(^\s*)|(\s*$)/g, "");
}
//url编码 对应HttpUtility.UrlEncode
function EncodeUrl(url) {
    return encodeURIComponent(url);
}
//url解码 对应HttpUtility.UrlDecode
function DecodeUrl(url) {
    return decodeURIComponent(url);
}
/*===============================验证方法==============================*/
//邮箱
function ValidEmail(v) {
    return /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/.test(v);
}
//电话号码
function ValidPhone(v) {
    return /^\d{3,4}-\d{7,8}(-\d{3,4})?$/.test(v);
}
//手机号
function ValidMobile(v) {
    return /^[1]\d{10}$/.test(v);
}
//非负浮点数
function ValidFloatPositive0(v) {
    return /^\d+(\.\d+)?$/.test(v);
}
//非负整数
function ValidIntegerPositive0(v) {
    return /^[0-9]+$/.test(v);
}
