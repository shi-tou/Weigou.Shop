//文件名：news.js
//描述：资讯管理
//时间：2015-06-03
//作者：zq

$(function () {
    GetList();
});
var url = '/Ajax/Content.ashx';
//获取用户列表
function GetList() {
    var queryParams = { 'action': 'GetNewsList', 'Title': $('#txtTitle').val(),'Type':$("#ddlType").val() };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '资讯列表',
        url: dealAjaxUrl(url),
        columns: [[
                        { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
                        { field: 'TypeName', title: '类型', width: 100, align: 'center'},
                        { field: 'Title', title: '标题', width: 200, align: 'center' },
                        { field: 'CreateName', title: '操作人', width: 120, align: 'center' },
                        { field: 'CreateTime', title: '时间', width: 150, align: 'center', formatter: FormatDateTime }
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

//添加
function Add() {
    OpenMaxWin('添加资讯', 'NewsAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenMaxWin('修改资讯', 'NewsAdd.aspx?ID=' + id);
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
                    data: 'action=DeleteNews&ID=' + id,
                    dataType: 'json',
                    type: 'POST',
                    success: function (data) {
                        if (data.res == RT.SUCCESS) {
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
