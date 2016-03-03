//文件名：logistics.js
//描述：物流管理
//时间：2015-05-28
//创建人：zq

$(function () {
    GetList();
});
var url = '/Ajax/Sys.ashx';
//获取物流列表
function GetList() {
    var queryParams = { 'action': 'GetLogisticsList' };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '物流列表',
        url: dealAjaxUrl(url),
        columns: [[
                    { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
                    { field: 'Name', title: '物流公司名称', width: 100, align: 'center' },
                    { field: 'Remark', title: '物流公司描述', width: 300, align: 'center' },
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
//添加
function Add() {
    OpenWin('添加物流', 500, 220, 'LogisticsAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenWin('修改物流', 500, 220, 'LogisticsAdd.aspx?ID=' + id);
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
                    data: 'action=DeleteLogistics&ID=' + id,
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
