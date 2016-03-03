//文件名：backup.js
//描述：备份管理
//时间：2015-11-9


$(function () {
    GetList();
});
var url = '/Ajax/Sys.ashx';
//获取用户列表
function GetList() {
    var queryParams = { 'action': 'GetBackupList' };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '备份列表',
        url: dealAjaxUrl(url),
        columns: [[
                        { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
                        { field: 'BackName', title: '备份名称', width: 200, align: 'center' },
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


function AddBackup() {
    $.messager.confirm('操作提示', '是否确认执行备份数据库操作？', function (r) {
        if (r) {
            $.ajax({
                url: dealAjaxUrl(url),
                data: 'action=BackUpData',
                dataType: 'json',
                type: 'POST',
                success: function (data) {
                    if (data.res == 0) {
                        AlertInfo('备份成功！');
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