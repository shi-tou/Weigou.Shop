//文件名：area.js
//描述：地区管理
//时间：2014-01-13


var tabID ;
var listTable;
$(function() {
    GetProvinceList();
    GetCityList();
    GetDistrictList();
    $('#AreaTab').tabs({
        onSelect: function(title) {
            if (title == '省管理') {
                tabID = 1;
                listTable = 'ProvinceTable';
            }
            else if (title == '市管理') {
                tabID = 2;
                listTable = 'CityTable';
            }
            else {
                tabID = 3;
                listTable = 'DistrictTable';
            }
            $('#hdTabID').val(tabID);
        }
    });
});

var url = '/Ajax/Sys.ashx';
//获取省份列表
function GetProvinceList() {
    var queryParams = { 'action': 'GetProvinceList' };
    var tab = $('#ProvinceTable');
    tab.datagrid({
        title: '省份列表',
        url: dealAjaxUrl(url),
        columns: [[
                    { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
                    { field: 'ProvinceName', title: '省名称', width: 150, align: 'center' },
                    { field: 'Spell', title: '拼音', width: 150, align: 'center' },
                    { field: 'FirstLetter', title: '首字母', width: 60, align: 'center'},
                    { field: 'ShortName', title: '简称', width: 120, align: 'center'}
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
//获取城市列表
function GetCityList() {
    var queryParams = { 'action': 'GetCityList' };
    var tab = $('#CityTable');
    tab.datagrid({
        title: '城市列表',
        url: dealAjaxUrl(url),
        columns: [[
                    { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
                    { field: 'CityName', title: '省名称', width: 150, align: 'center' },
                    { field: 'Spell', title: '拼音', width: 150, align: 'center' },
                    { field: 'FirstLetter', title: '首字母', width: 60, align: 'center' },
                    { field: 'ZipCode', title: '邮编', width: 120, align: 'center' },
                    { field: 'ProvinceName', title: '所在省', width: 120, align: 'center' }
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
//获取区域列表
function GetDistrictList() {
    var queryParams = { 'action': 'GetDistrictList' };
    var tab = $('#DistrictTable');
    tab.datagrid({
        title: '区列表',
        url: dealAjaxUrl(url),
        columns: [[
                    { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
                    { field: 'DistrictName', title: '区域名称', width: 150, align: 'center' },
                    { field: 'CityName', title: '所在市', width: 120, align: 'center' },
                    { field: 'ProvinceName', title: '所在省', width: 120, align: 'center' }
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
    OpenWin('添加', 430, 330, 'AreaAdd.aspx?Type=' + tabID);
}
//修改
function Edit() {
    var rows = GetSelectValue(listTable);
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenWin('修改', 430, 330, 'AreaAdd.aspx?Type=' + tabID + '&ID=' + id);
    }
    else {
        AlertInfo('请选择一条要修改的记录！');
    }
}
//删除
function Delete() {
    var code = '';
    var rows = GetSelectValue(listTable);
    if (rows.length == 1) {
        id = rows[0].ID;
        $.messager.confirm('操作提示', '是否确认删除操作？', function(r) {
            if (r) {
                $.ajax({
                    url: dealAjaxUrl(url),
                    data: 'action=DeleteArea&Type=' + tabID + '&ID=' + id,
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