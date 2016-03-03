//文件名：newstype.js
//描述：文章分类管理

$(function() {
    GetList();
});
var url = '/Ajax/Content.ashx';
//获取文章分类列表
function GetList() {
    var queryParams = { 'action': 'GetNewsTypeList' };
    var tab = $('#ListTable');
    tab.treegrid({
        title: '文章分类列表',
        url: dealAjaxUrl(url),
        idField: 'ID',
        treeField: 'Name',
        animate: true,
        fitColumns: false,
        showFooter: true,
        initialState:"collapsed",
        frozenColumns:[[
            { field: 'ID', width: 30, align: '', checkbox: true },
            { field: 'Name', title: '分类名称', width: 150, align: '' },
        ]],
        columns: [[
            { field: 'ParentName', title: '上级分类', width: 80, align: 'center'}
        ]],
        loadMsg: '正在加载数据，请稍候……',
        queryParams: queryParams,
        pagination: false,
        rownumbers: true,
        singleSelect:true
    });
}

//添加
function Add() {
    OpenWin('添加文章分类', 500, 250, 'NewsTypeAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        var id = rows[0].ID;
        OpenWin('修改文章分类', 500, 250, 'NewsTypeAdd.aspx?ID=' + id);
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
