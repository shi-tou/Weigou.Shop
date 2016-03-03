//文件名：mallordersale.js
//描述：售后管理
//时间：2015-08-11
//作者：zq

$(function () {
    GetList();
});
var url = '/Ajax/Order.ashx';
function GetWhere() {  // 'MerchantName': $('#txtMerchantName').val(),
    var params = {
        'action': 'GetMallOrderSaleList', 'OrderNo': $('#txtOrderNo').val(), 'ConsigneeMobileNo': $('#txtConsigneeMobileNo').val(),
        'MinTime': $('#txtMinTime').val(), 'MaxTime': $('#txtMaxTime').val(), 'OrderType': $('#ddlOrderType').val(), 'Status': $('#ddlStatus').val()
    };
    return params;
}
//获取售后列表
function GetList() {
    var queryParams = GetWhere();
    var tab = $('#ListTable');
    tab.datagrid({
        title: '售后列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'OrderNo', title: '订单号', width: 150, align: 'center', formatter: ViewOrderDetails },
            //{ field: 'MerchantName', title: '店铺名称', width: 150, align: 'center' },
            { field: 'Type', title: '售后类型', width: 100, align: 'center', formatter: GetType },
            { field: 'GoodsName', title: '商品名称', width: 300, align: 'center' },
            { field: 'ApplyNumber', title: '申请数量', width: 80, align: 'center' },
            { field: 'ConsigneeMobileNo', title: '收货人手机号', width: 100, align: 'center' },
            { field: 'Status', title: '处理状态', width: 100, align: 'center', formatter: GetStatus },
            { field: 'ApplyTime', title: '申请时间', width: 150, align: 'center', formatter: FormatDateTime },
            { field: 'DealName', title: '处理人', width: 100, align: 'center' },
            { field: 'DealTime', title: '处理时间', width: 120, align: 'center', formatter: FormatDateTime },
            { field: 'RowID', title: '操作', width: 120, align: 'center', formatter: FormatOperation }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        rownumbers: true, //显示记录数
        queryParams: queryParams, //查询参数
        pagination: true,
        singleSelect: true,
        pageSize: 20,
        nowrap: false,
        onLoadSuccess: loadsuccess
    });
    //设置分页
    SetPager(tab);
}


function ViewOrderDetails(v, row) {
    return "<a href='javascript:View(" + row.ID + ");'>" + v + "</a>";
}

//按钮格式化 
function FormatOperation(v, row) {
    var btn = '';
    if ($('#hfEdit').val() == 1) {
        btn += '<a class="EditBtn"  data-value=' + row.ID + ' onclick="Edit(' + row.ID + ')" href="javascript:void(0)">编辑</a>';
    }
    return btn;
}
//初始化编辑按钮
function loadsuccess(data) {
    $('.EditBtn').linkbutton({ text: '编辑', plain: true, iconCls: 'icon-edit' });
}
//售后类型
function GetType(s) {
    switch (parseInt(s)) {
        case 1:
            return '退货';
        case 2:
            return '换货';
        case 3:
            return '维修';
        default: return '';
    }
}

//处理状态
function GetStatus(s) {
    switch (parseInt(s)) {
        case 0:
            return '买家申请处理';
        case 1:
            return '<span style="color:green;">卖家同意处理</span>';
        case 2:
            return '<span style="color:green;">卖家拒绝处理</span>';
        default: return '';
    }
}

//修改
function View(id) {
    OpenMaxWin('售后详情', 'MallOrderSaleDetail.aspx?ID=' + id + '&IsEdit=0');
}
//修改
function Edit(id) {
    OpenMaxWin('售后详情', 'MallOrderSaleDetail.aspx?ID=' + id);
}