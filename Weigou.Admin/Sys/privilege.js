//文件名：action.js
//描述：权限管理
//时间：2013-09-29


$(function() {
    GetList();
});
var url = '/Ajax/Sys.ashx';
//获取资源列表
function GetList() {
    var queryParams = { 'action': 'GetPrivilegeList' };
    var tab = $('#ListTable');
    tab.treegrid({
        title: '权限列表',
        url: dealAjaxUrl(url),
        idField: 'Code',
        treeField: 'Name',
        animate: true,
        fitColumns: false,
        showFooter: true,
        initialState:"collapsed",
        frozenColumns:[[
            { field: 'ID', width: 30, align: '', checkbox: true },
            { field: 'Name', title: '名称', width: 150, align: '' },
            { field: 'Code', title: '编码', width: 120, align: 'center' },
        ]],
        columns: [[
            { field: 'ParentCode', title: '上一级编码', width: 120, align: 'center' },
            { field: 'PrivilegeType', title: '类别', width: 80, align: 'center', formatter: GetType },
            { field: 'Url', title: '链接', width: 150, align: 'center' },
            { field: 'Func', title: '事件', width: 100, align: 'center' },
            { field: 'Icon', title: '图标', width: 100, align: 'center' },
            { field: 'Sort', title: '排序号', width: 80, align: 'center' },
            { field: 'Status', title: '状态', width: 80, align: 'center', formatter: GetStatus }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        queryParams: queryParams,
        pagination: false,
        rownumbers: true,
        singleSelect:true
    });
}
//资源类别
function GetType(v) {
    if (v == 1)
        return '模块';
    else if (v == 2)
        return '主窗体';
    else if (v == 3)
        return '工具栏';
}
//添加
function Add() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        Code = rows[0].Code;
        OpenWin('修改资源', 450, 430, 'PrivilegeAdd.aspx?ParentCode=' + Code);
    }
    else
        OpenWin('添加资源', 450, 430, 'PrivilegeAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenWin('修改资源',450, 430, 'PrivilegeAdd.aspx?ID=' + id);
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
        $.messager.confirm('操作提示', '是否确认删除操作？', function(r) {
            if (r) {
                $.ajax({
                    url: dealAjaxUrl(url),
                    data: 'action=DeletePrivilege&ID=' + id,
                    dataType: 'json',
                    type: 'POST',
                    success: function(data) {
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
