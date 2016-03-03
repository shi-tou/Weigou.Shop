//文件名：login.js
//描述：登录
//时间：2015-03-10
//作者：杨良斌

var url = 'Ajax.ashx?temp=' + (new Date().getTime().toString(36));
//注册验证手机号码
function VerifyMobileNo() {
    var f = true;
    var mobileNo = $('#txtMobileNo').val();
    if (mobileNo == '') {
        layer.alert('请输入您的手机号！');
        f = false;
    }
    else {
        if (!ValidMobile(mobileNo)) {
            layer.alert('输入手机号格式不正确！');
            f = false;
        }
    }
    return f;
}
//验证表单
function CheckForm() {
    if (!VerifyMobileNo()) {
        return false;
    }
    if ($('#txtPassword').val() == '') {
        layer.alert('请输入登录密码！');
        return false;
    }
    return true;
}

//登录
function Login() {
    if (!CheckForm()) {
        return;
    }
    $.ajax({
        url: url,
        data: { 'action': 'Login', 'MobileNo': $('#txtMobileNo').val(), 'Password': $('#txtPassword').val() },
        dataType: 'json',
        type: 'POST',
        async: false,
        success: function(data) {
            if (data.res == RT.SUCCESS) {
                var url = DecodeUrl(QueryString('backurl'));
                if (url == '') {
                    url = 'Member/MemberInfo.aspx';
                }
                location.href = url;
            }
            else if (data.res == RT.RES_NOTEXIST) {
                layer.alert('输入手机号还未注册！');
            }
            else if (data.res == RT.RES_ERROR_PASSWORD) {
                layer.alert('密码错误,请重新输入！');
            }
            else if (data.res == RT.OPERATION_FAILED) {
                layer.alert('登录失败,请重试！');
            }
        }
    });
}