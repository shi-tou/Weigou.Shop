//文件名：brand.js
//描述：品牌管理
//时间：2015-06-02
//创建人：zq

$(function () {
    GetList();
});
var url = '/Ajax/Goods.ashx';

//获取查询条件
function GetParams() {
    var param = { 'action': 'GetGoodsBrandList', 'BrandName': $('#txtName').val() };
    return param;
}

//获取商品列表
function GetList() {
    var queryParams = GetParams();
    var tab = $('#ListTable');
    tab.datagrid({
        title: '品牌列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'Name', title: '品牌名称', width: 200, align: 'center' },
            { field: 'Logo', title: '品牌图片', width: 160, align: 'center', formatter: FormatPic },
            { field: 'CreateName', title: '创建人', width: 80, align: 'center' },
            { field: 'CreateTime', title: '创建时间', width: 160, align: 'center', formatter: FormatDateTime },
        ]],
        loadMsg: '正在加载数据，请稍候……',
        queryParams: queryParams,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        pageSize: 20
    });
    //设置分页
    SetPager(tab);
}

function FormatPic(v) {
    if (v == '')
        return '';
    else
        return '<img src="' + v + '" style="width:150px; height:50px;" />';
}


//添加
function Add() {
    OpenWin('添加品牌', 500, 350, 'GoodsBrandAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenWin('修改品牌', 500, 350, 'GoodsBrandAdd.aspx?ID=' + id);
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
                    data: 'action=DeleteGoodsBrand&ID=' + id,
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
