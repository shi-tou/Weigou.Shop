//文件名：class.js
//描述：栏目管理
//时间：2015-06-15


$(function () {
    GetList();
});
var url = '/Ajax/Sys.ashx';
//获取资源列表
function GetList() {
    var queryParams = { 'action': 'GetClassList' };
    var tab = $('#ListTable');
    tab.treegrid({
        title: '栏目列表',
        url: dealAjaxUrl(url),
        idField: 'ID',
        treeField: 'ClassName',
        animate: true,
        fitColumns: false,
        showFooter: true,
        initialState: "collapsed",
        frozenColumns: [[
            { field: 'ID', width: 30, align: '', checkbox: true },
            { field: 'ClassName', title: '栏目名称', width: 150, align: '' },
            { field: 'Code', title: '编码', width: 120, align: 'center' },
        ]],
        columns: [[
            { field: 'ParentCode', title: '上一级编码', width: 120, align: 'center' },
            { field: 'ClassPropertyID', title: '栏目属性', width: 80, align: 'center', formatter: GetType },
            { field: 'LinkUrl', title: '链接', width: 150, align: 'center' },
            { field: 'Sort', title: '排序号', width: 80, align: 'center' },
            { field: 'Status', title: '状态', width: 80, align: 'center', formatter: GetStatus }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        queryParams: queryParams,
        pagination: false,
        rownumbers: true,
        singleSelect: true
    });
}
//资源类别
function GetType(v) {
    if (v == 1)
        return '头部导航';
    else if (v == 2)
        return '底部导航';
}
//添加
function Add() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        Code = rows[0].Code;
        OpenWin('修改栏目', 450, 430, 'ClassAdd.aspx?ParentCode=' + Code);
    }
    else
        OpenWin('添加栏目', 450, 430, 'ClassAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenWin('修改栏目', 450, 430, 'ClassAdd.aspx?ID=' + id);
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
        var count = rows[0].children.length
        if (count > 0) {
            AlertInfo('该栏目下还有子栏目，不能删除！如要删除请先将该栏目下的子栏目全部删除。');
            return;
        }
        $.messager.confirm('操作提示', '是否确认删除操作？', function (r) {
            if (r) {
                $.ajax({
                    url: dealAjaxUrl(url),
                    data: 'action=DeleteClass&ID=' + id,
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
