//文件名：memberlist.js
//描述：会员管理
//时间：2013-12-09


$(function () {
    GetList();
});
var url = '/Ajax/Member.ashx';
//获取资源列表
function GetList() {
    var queryParams = {
        'action': 'GetMemberLevelList'
    };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '会员级别列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'Name', title: '会员级别名', width: 150, align: 'center' },
            { field: 'Money', title: '会员金额', width: 100, align: 'center', formatter: GetYuan },
            { field: 'Year', title: '会员年限', width: 60, align: 'center', formatter: GetYear },
            { field: 'Price', title: '租车价格', width: 100, align: 'center', formatter: GetYuanHour },
            { field: 'StartTime', title: '免费开始时间', width: 90, align: 'center'},
            { field: 'EndTime', title: '免费结束时间', width: 90, align: 'center'},
            { field: 'RentDiscount', title: '租车折扣', width: 80, align: 'center', formatter: GetDiscount },
            { field: 'ShoppingDiscount', title: '购物折扣', width: 150, align: 'center', formatter: GetDiscount },
            { field: 'BuyCarDiscount', title: '购车折扣', width: 80, align: 'center', formatter: GetDiscount },
            { field: 'ConsumerPoint', title: '消费100元可兑积分', width: 150, align: 'center', formatter: GetMinute },
            { field: 'MileagePoint', title: '1000公里可兑积分', width: 150, align: 'center', formatter: GetMinute },
            { field: 'Point', title: '积分兑换标准', width: 150, align: 'center', formatter: GetMinute },
            { field: 'CreateTime', title: '添加时间', width: 120, align: 'center', formatter: FormatDateTime },
            { field: 'CreateName', title: '创建人', width: 80, align: 'center' }
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
//年
function GetYear(s) {
   return s+'(年)'
}
function GetYuan(s) {
    return s + '(元)'
}
function GetYuanHour(s) {
    return s + '(元/时)'
}
function GetDiscount(s) {
    return s + '(折)'
}
function GetMinute(s) {
    return s + '(分)'
}
//添加
function Add() {
    OpenMaxWin('添加会员级别', 'MemberLevelAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenMaxWin('修改会员级别', 'MemberLevelAdd.aspx?ID=' + id);
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
                    data: 'action=DeleteMemberLevel&ID=' + id,
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
