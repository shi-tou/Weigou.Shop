//文件名：memberlist.js
//描述：会员管理
//时间：2013-12-09


$(function () {
    GetList();
});
var url = '/Ajax/Member.ashx';
//获取资源列表
function GetList() {
    var queryParams = {
        'action': 'GetMemberList', 'Name': $('#txtName').val(), 'MobileNo': $('#txtMobileNo').val(),
        'Sex': $('#ddlSex').val(), 'Status': $('#ddlStatus').val(), 'MinTime': $('#txtMinTime').val(), 'MaxTime': $('#txtMaxTime').val()
    };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '会员列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'MobileNo', title: '手机号', width: 130, align: 'center' },
            { field: 'Name', title: '姓名', width: 100, align: 'center' },
            { field: 'Sex', title: '性别', width: 60, align: 'center', formatter: GetSex },
            { field: 'EducationName', title: '学历', width: 100, align: 'center' },
            { field: 'OccupationName', title: '职位', width: 100, align: 'center' },
            { field: 'Status', title: '状态', width: 60, align: 'center', formatter: GetStatus },
            { field: 'AuthStatus', title: '认证状态', width: 80, align: 'center', formatter: GetAuthStatus },
            { field: 'Remark', title: '备注', width: 150, align: 'center' },
            { field: 'CreateTime', title: '注册时间', width: 120, align: 'center', formatter: FormatDateTime },
            { field: 'CreateName', title: '创建人', width: 80, align: 'center' }
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
//启用状态
function GetStatus(s) {
    switch (parseInt(s)) {
        case 0:
            return '<span style="color:red;">冻结</span>';
        case 1:
            return '<span style="color:green;">启用</span>';
        default: return '';
    }
}
//认证状态
function GetAuthStatus(s) {
    switch (parseInt(s)) {
        case 0:
            return '<span style="color:red;">待审核</span>';
        case 1:
            return '<span style="color:green;">审核通过</span>';
        case 2:
            return '<span style="color:green;">审核不通过</span>';
        default: return '';
    }
}

//性别
function GetSex(s) {
    switch (parseInt(s)) {
        case 0:
            return '男';
        case 1:
            return '女';
        default: return '';
    }
}

//添加
function Add() {
    OpenMaxWin('添加会员', 'MemberAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenMaxWin('修改会员', 'MemberAdd.aspx?ID=' + id);
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
                    data: 'action=DeleteMember&ID=' + id,
                    dataType: 'json',
                    type: 'POST',
                    success: function (data) {
                        if (data.res == 0) {
                            AlertInfo('删除成功！');
                            GetList();
                        }
                        else {
                            AlertInfo('该数据已在系统中使用，不能删除！');
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
function SetLevel() {
    OpenMaxWin('等级设置', 'Levellist.aspx');
}