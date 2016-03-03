//文件名：sysversion.js
//描述：版本管理
//时间：2015-08-08
//创建人：zq

$(function () {
    GetList();
});
var url = '/Ajax/Sys.ashx';
//获取物流列表
function GetList() {
    var queryParams = { 'action': 'GetSysVersionList' };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '版本列表',
        url: dealAjaxUrl(url),
        columns: [[
                    { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
                    { field: 'Type', title: '设备类型', width: 100, align: 'center', formatter: GetType },
                    { field: 'VersionCode', title: '版本号', width: 100, align: 'center' },
                    { field: 'VersionName', title: '版本名称', width: 300, align: 'center' },
                    { field: 'ForceUpdate', title: '是否强制更新', width: 100, align: 'center', formatter: GetForceUpdate },
                    { field: 'AppUrl', title: '下载地址', width: 300, align: 'center' },
                    { field: 'CreateName', title: '创建人', width: 120, align: 'center' },
                    { field: 'CreateTime', title: '创建时间', width: 120, align: 'center', formatter: FormatDateTime }
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

//设备类型
function GetType(s) {
    switch (parseInt(s)) {
        case 1:
            return 'Android';
        case 2:
            return 'IOS';
        default: return '';
    }
}
//设备类型
function GetForceUpdate(s) {
    if(s)
    {
        return '强制';
    }
    else{
        return '不强制';     
    }        
}


//添加
function Add() {
    OpenWin('添加版本', 600, 480, 'SysVersionAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenWin('修改版本', 600, 480, 'SysVersionAdd.aspx?ID=' + id);
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
                    data: 'action=DeleteSysVersion&ID=' + id,
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
