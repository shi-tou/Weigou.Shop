//文件名：notify.js
//描述：公告管理
//时间：2015-05-27


$(function() {
    GetList();
});
var url = '/Ajax/Content.ashx';
//获取公告列表
function GetList() {
    var type = $('#ddlType').val();
    var queryParams = { 'action': 'GetNotifyList', 'Type': type, 'Title': $('#txtTitle').val(), 'Content': $('#txtContent').val() };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '公告消息列表',
        url: dealAjaxUrl(url),
        columns: [[
                        { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
                        { field: 'MemberName', title: '会员姓名', width: 100, align: 'center', hidden: (type == 1 ? false : true) },
                        { field: 'MerchantName', title: '商户名称', width: 150, align: 'center', hidden: (type == 1 ? true : false) },
                        { field: 'Title', title: '标题', width: 200, align: 'center' },
                        { field: 'Content', title: '内容', width: 100, align: 'center' },
                        { field: 'CreateUser', title: '操作人', width: 120, align: 'center' },
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
    OpenMaxWin('添加公告消息',  'NotifyAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        OpenMaxWin('修改公告消息', 'NotifyAdd.aspx?ID=' + rows[0].ID + '&Type=' + rows[0].Type);
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
                    data: 'action=DeleteNotify&ID=' + id,
                    dataType: 'json',
                    type: 'POST',
                    success: function (data) {
                        if (data.res == RT.SUCCESS) {
                            ShowMsg('删除成功！');
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
