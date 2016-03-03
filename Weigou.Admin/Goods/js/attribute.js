//文件名：good.js
//描述：扩展属性管理
//时间：2015-5-25
//创建人：zq

$(function () {
    GetList();
});
var url = '/Ajax/Goods.ashx';

//获取查询条件
function GetParams() {
    var param = { 'action': 'GetGoodsAttributeList', 'Name': $('#txtName').val(), 'Type': $("#ddlType").val() };
    return param;
}

//获取商品列表
function GetList() {
    var queryParams = GetParams();
    var tab = $('#ListTable');
    tab.datagrid({
        title: '商品属性列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'Name', title: '属性名称', width: 180, align: 'center' },
            { field: 'Alias', title: '属性别名', width: 180, align: 'center'},
            { field: 'Sort', title: '排序号', width: 80, align: 'center' },
            { field: 'CreateName', title: '创建人', width: 80, align: 'center' },
            { field: 'CreateTime', title: '创建时间', width: 160, align: 'center', formatter: FormatDateTime }
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

//类别
function GetType(s) {
    switch (parseInt(s)) {
        case 1:
            return '选项';
        case 2:
            return '文本';
        default: return '';
    }
}

//添加
function Add() {
    OpenWin('添加属性', 480, 320, 'GoodsAttributeAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenWin('修改属性',480,320, 'GoodsAttributeAdd.aspx?ID=' + id);
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
                    data: 'action=DeleteGoodsAttribute&ID=' + id,
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

function AttributeValueSet() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        var id = rows[0].ID;
        var Alias = rows[0].Alias;
        OpenMaxWin('商品属性值管理', 'AttributeValueSet.aspx?AttributeID=' + id + '&AttributeAlias=' + escape(Alias));
    }
    else {
        AlertInfo('请选择一条记录！');
    }
}