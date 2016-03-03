
var strID;
$(function () {
    strID = $("#hideID").val();
    GetList();

});
var url = '/Ajax/Sys.ashx';

//获取查询条件
function GetParams() {
    var param = { 'action': 'GetPlatformLogisticsList', 'ID': strID };
    return param;
}

//获取商品列表
function GetList() {
    var queryParams = GetParams();
    var tab = $('#ListTable');
    tab.datagrid({
        title: '平台物流模版',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'Name', title: '运费模版名称', width: 100, align: 'center' },
            { field: 'LogisticsName', title: '快递公司', width: 100, align: 'center' },
            { field: 'DefaultPrice', title: '默认运费', width: 100, align: 'center' },
            { field: 'ProvinceName', title: '特殊省份', width: 300, align: 'center' },
            { field: 'LogisticsPrice', title: '指定地区运费', width: 100, align: 'center' },
            { field: 'IsDefault', title: '是否默认模板', width: 100, align: 'center', formatter: GetStatus },
            { field: 'Remark', title: '备注', width: 100, align: 'center' }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        queryParams: queryParams,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        pageSize: 20,
        nowrap: false
    });
    //设置分页
    SetPager(tab);
}

//审核状态
function GetStatus(s) {
    switch (s) {
        case false:
            return '<span style="color:red;">否</span>';
        case true:
            return '<span style="color:green;">是</span>';
        default: return '';
    }
}

//添加
function Add() {
    OpenMaxWin('添加平台物流模版', 'PlatformLogisticsAdd.aspx');
}


//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenMaxWin('修改平台物流模版', 'PlatformLogisticsAdd.aspx?ID=' + id);
    }
    else {
        AlertInfo('请选择一条要修改的记录！');
    }
}

function Set() {
    var code = '';
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        $.messager.confirm('操作提示', '是否确认设置默认操作？', function (r) {
            if (r) {
                $.ajax({
                    url: dealAjaxUrl(url),
                    data: 'action=SetDefaultLogistics&ID=' + id,
                    dataType: 'json',
                    type: 'POST',
                    success: function (data) {
                        if (data.res == RT.SUCCESS) {
                            AlertInfo('设置成功！');
                            GetList();
                        }
                        else {
                            AlertInfo('设置失败，请与管理员联系！');
                        }
                    }
                });
            }
        });
    }
    else {
        AlertInfo('请选择要设置的记录');
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
                    data: 'action=DeletePlatformLogistics&ID=' + id,
                    dataType: 'json',
                    type: 'POST',
                    success: function (data) {
                        if (data.res == RT.SUCCESS) {
                            AlertInfo('删除成功！');
                            GetList();
                        }
                        else {
                            AlertInfo('删除失败，请与管理员联系！');
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