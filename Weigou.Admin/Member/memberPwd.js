//文件名：memberPwd.js
//描述：会员密码
//时间：2013-12-09

var url = '/Ajax/Member.ashx';
var memberID = 0;
$(function() {

    $('#btnSumbit').bind('click', ChangePwd);
    setRadioValue('rblType', '1');
    $('#rblType').bind('change', SelectType);
});
//查询会员成功回调
function SearchMemberSuccessed(data) {
    memberID = data.ID;
}
//选择类型
function SelectType() {
    if (getRadioValue('rblType') == '1') {
        $('#trOldPwd').show();
        $('#txtOldPassword').validatebox('reduce'); //恢复  
    }
    else {
        $('#txtOldPassword').validatebox('remove'); //删除  
        $('#trOldPwd').hide();
    }
}
//充值
function ChangePwd() {
    if ($('#form1').form('validate')) {
        if (memberID == 0) {
            AlertInfo('请先执行会员刷卡操作！');
            return;
        }
        var query = { 'action': 'ChangePassword', 'MemberID': memberID, 'Type': $('#rblType').val(),
            'OldPassword': $('#txtOldPassword').val(), 'NewPassword': $('#txtNewPassword').val()
        };
        DoAjax(url, query, Result);
    }
}
//操作结果
function Result(d) {
    if (d.res == RT.SUCCESS) {
        AlertInfo('操作成功！');
        //清空会员信息
        ClearMember();
        memberID = 0;
        $('#txtOldPassword').val('');
        $('#txtNewPassword').val('');
        $('#txtNewPassword0').val('');
    }
    else if (d.res == RT.RESULT_NOT_EXIST) {
        AlertInfo('会员不存在！');
    }
    else if (d.res == RT.RESULT_ERROR_PASSWORD) {
        AlertInfo('原密码不正确！');
    }
    else if (d.res == RT.FAILED) {
        AlertInfo('操作失败！');
    }
}