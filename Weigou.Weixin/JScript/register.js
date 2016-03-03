//文件名：register.js
//描述：注册
//时间：2015-03-10
//作者：杨良斌

var url = '/Ajax/mem.ashx?temp=' + (new Date().getTime().toString(36));

//注册验证手机号码
function VerifyMobileNo() {
    var mobileNo = $('#txtMobileNo').val();
    if (mobileNo == '') {
        layer.alert('请输入您的手机号！');
        return false;
    }
    else {
        if (ValidMobile(mobileNo) == false) {
            layer.alert('输入手机号格式不正确！');
            return false;
        }
    }
    return true;
}
//验证表单
function CheckForm() {
    if (!VerifyMobileNo()) {
        return false;
    }
    if ($('#txtName').val() == '') {
        layer.alert('请输入真实姓名！');
        return false;
    }
    if ($('#txtPassword').val() == '') {
        layer.alert('请输入设置密码！');
        return false;
    }
    if ($('#txtPassword0').val() == '') {
        layer.alert('请输入确认密码！');
        return false;
    }
    if ($('#txtPassword').val() != $('#txtPassword0').val()) {
        layer.alert('两输入密码不一致！');
        return false;
    }
    if ($('#txtValidCode').val() == '') {
        layer.alert('请输入验证码！');
        return false;
    }
    return true;
}
//注册获取验证
var c = 0;
function AskCheckCodeForReg() {
    if (!VerifyMobileNo()) {
        return;
    }
    //判断重复获取
    if (c != 0)
        return;
    $.ajax({
        url: url,
        data: { 'action': 'AskCheckCodeForReg', 'MobileNo': $('#txtMobileNo').val() },
        dataType: 'json',
        type: 'POST',
        success: function(data) {
            if (data.res == 0) {
                TimedCount();
            }
            if (data.res == 107) {
                layer.alert('您输入的手机号已注册！');
            }
            else if (data.res == -1) {
                layer.alert('获取失败,请重试！');
            }
        }
    });
}
function TimedCount() {
    $('#btnSms').val('已发送(' + (60 - c) + ')');
    c = c + 1;
    if (c == 60) {
        StopCount();
    }
    else {
        t = setTimeout("TimedCount()", 1000)
    }
}
function StopCount() {
    $('#btnSms').val('重新获取');
    c = 0;
    clearTimeout(t)
}
//注册
function Register() {
    if (!CheckForm()) {
        return;
    }
    $.ajax({
        url: url,
        data: {
            'action': 'Register', 'MobileNo': $('#txtMobileNo').val(), 'Password': $('#txtPassword').val(),
            'Name': $('#txtName').val(), 'ValidCode': $('#txtValidCode').val()
        },
        dataType: 'json',
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.res == 0) {
                layer.alert("恭喜您,注册成功！赶紧下载App体验吧！", 1, '提示', function () {
                    location.href = 'AppDownload.aspx';
                });
            }
            else if (data.res == 113) {
                layer.alert('您输入验证码不正确！');
            }
            else if (data.res == 107) {
                layer.alert('您输入的手机号已注册！');
            }
            else if (data.res == -1) {
                layer.alert('操作失败,请重试！');
            }
        }
    });
}