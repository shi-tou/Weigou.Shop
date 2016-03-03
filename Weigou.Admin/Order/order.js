//文件名：order.js
//描述：订单管理
//时间：2015-06-12
//作者：zq

$(function () {
    GetList();
});
var url = '/Ajax/Order.ashx';
function GetWhere() {
    var params = {
        'action': 'GetOrderList', 'OrderNo': $('#txtOrderNo').val(), 'MerchantName': $('#txtMerchantName').val(), 'MemberName': $('#txtMemberName').val(),
        'MobileNo': $('#txtMobileNo').val(), 'Status': $('#ddlStatus').val(), 'MinTime': $('#txtMinTime').val(), 'MaxTime': $('#txtMaxTime').val()
    };
    return params;
}
//获取用户列表
function GetList() {
    var queryParams = GetWhere();
    var tab = $('#ListTable');
    tab.datagrid({
        title: '订单列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'OrderNo', title: '订单号', width: 120, align: 'center' },
            { field: 'MemberName', title: '会员名', width: 100, align: 'center' },
            { field: 'MobileNo', title: '手机号', width: 100, align: 'center' },
            { field: 'TotalCount', title: '商品总数', width: 80, align: 'center' },
            { field: 'TotalMoney', title: '订单总价', width: 80, align: 'center' },
            { field: 'OrderTime', title: '订单时间', width: 150, align: 'center', formatter: FormatDateTime }
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

//修改
function View() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        var orderNo = rows[0].OrderNo;
        OpenMaxWin('订单详情', 'OrderDetail.aspx?OrderNo=' + orderNo);
    }
    else {
        AlertInfo('请选择一条要查看的订单！');
    }
}
