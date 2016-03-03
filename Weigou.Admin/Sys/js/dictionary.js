//文件名：diction.js
//描述：数据字典管理
//时间：2013-12-01


$(function() {
    GetList();
});
var url = '/Sys.ashx';
//获取资源列表
function GetList() {
    var queryParams = { 'action': 'GetDictionaryList' };
    var tab = $('#ListTable');
    tab.treegrid({
        title: '数据字典列表',
        url: dealAjaxUrl(url),
        idField: 'Code',
        treeField: 'Name',
        animate: true,
        fitColumns: false,
        showFooter: true,
        frozenColumns: [[
            { field: 'ID', width: 30,title:'编号', align: 'center'},
            { field: 'Name', title: '名称', width: 150, align: '' },
        ]],
        columns: [[
            { field: 'ParentName', title: '父级', width: 80, align: 'center' },
            { field: 'Value', title: '字典值', width: 150, align: 'center' },
            { field: 'Code', title: '编码', width: 150, align: 'center' },
            { field: 'Remark', title: '备注', width: 250, align: 'center' }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        queryParams: queryParams,
        pagination: false,
        rownumbers: true,
        singleSelect: true
    });
}
//添加
function Add() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        OpenWin('修改数据字典', 450, 330, 'DictionaryAdd.aspx?ParentID=' + rows[0].ID);
    }
    else
        OpenWin('添加数据字典', 450, 330, 'DictionaryAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        OpenWin('修改数据字典', 450, 300, 'DictionaryAdd.aspx?ID=' + rows[0].ID);
    }
    else {
        ShowMsg('请选择一条要修改的记录！');
    }
}
//删除
function Delete() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        $.messager.confirm('操作提示', '是否确认删除操作？', function(r) {
            if (r) {
                $.ajax({
                    url: dealAjaxUrl(url),
                    data: 'action=DeleteDictionary&ID=' + rows[0].ID,
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
        ShowMsg('请选择要删除的记录');
    }
}
