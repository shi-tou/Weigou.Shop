//文件名：admin.js
//描述：用户管理
//时间：2013-09-29


$(function () {
    GetList();
});
var url = '/Ajax/Sys.ashx';
//获取用户列表
function GetList() {
    var queryParams = { 'action': 'GetUserList', 'Type': 1 };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '用户列表',
        url: dealAjaxUrl(url),
        columns: [[
                        { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
                        { field: 'UserName', title: '用户名', width: 120, align: 'center' },
                        { field: 'Name', title: '姓名', width: 130, align: 'center' },
                        { field: 'RoleName', title: '所属角色', width: 150, align: 'center' },
                        { field: 'Status', title: '状态', width: 70, align: 'center', formatter: GetStatus },
                        { field: 'CreateTime', title: '创建时间', width: 130, align: 'center', formatter: FormatDateTime }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        rownumbers: true, //显示记录数
        queryParams: queryParams, //查询参数
        pagination: true,
        singleSelect: true,
        pageSize: 20,
        nowrap: false
    });
    //设置分页
    SetPager(tab);
}
function GetStatus(v) {
    if (v == 0)
        return SetRed('停用');
    else
        return SetGreen('启用');
}
//添加
function Add() {
    OpenWin('添加用户', 450, 300, 'UserAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenWin('编辑用户', 450, 300, 'UserAdd.aspx?ID=' + id);
    }
    else {
        AlertInfo('请选择一条要修改的记录！');
    }
}
//删除
function Delete() {
    var code = '';
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        $.messager.confirm('操作提示', '是否确认删除操作？', function (r) {
            if (r) {
                $.ajax({
                    url: dealAjaxUrl(url),
                    data: 'action=DeleteUser&ID=' + id,
                    dataType: 'json',
                    type: 'POST',
                    success: function (data) {
                        if (data.res == RT.FAILED) {
                            AlertInfo('操作失败，请与管理员联系！');
                        }
                        if (data.res == RT.SUCCESS) {
                            AlertInfo('删除成功！');
                            GetList();
                        }
                        else if (data.res == 2) {
                            AlertInfo('您正在登录此账户，不允许删除！');
                        }
                        else if (data.res == 3) {
                            AlertInfo('User用户不能删除！');
                        }
                    }
                });
            }
        });
    }
    else {
        AlertInfo('请选择要删除的记录');
    }
}