//文件名：order.js
//描述：商城订单管理
//时间：2015-06-12
//作者：zq

$(function () {
    GetList();
});
var url = '/Ajax/Order.ashx';
function GetWhere() {
    var params = {
        'action': 'GetMallOrderList', 'OrderNo': $('#txtOrderNo').val(), 'MemberName': $('#txtMemberName').val(),
        'MobileNo': $('#txtMobileNo').val(), 'OrderStatus': $('#ddlOrderStatus').val(), 'MinTime': $('#txtMinTime').val(), 'MaxTime': $('#txtMaxTime').val()
    };
    return params;
}
//获取商城订单列表
function GetList() {
    var queryParams = GetWhere();
    var tab = $('#ListTable');
    tab.datagrid({
        title: '商城订单列表',
        url: dealAjaxUrl(url),
        columns: [[
           // { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'OrderNo', title: '订单号', width: 120, align: 'center' },
            //{ field: 'MemberName', title: '会员名', width: 100, align: 'center' },

            { field: 'DeliverAddress', title: '收货地址', width: 300, align: 'center' },
            { field: 'ConsigneeMobileNo', title: '收货手机号', width: 80, align: 'center' },
            { field: 'ConsigneeName', title: '收货人', width: 50, align: 'center' },
            { field: 'MobileNo', title: '会员手机号', width: 100, align: 'center' },

            { field: 'TotalCount', title: '商品总数', width: 80, align: 'center' },
            { field: 'TotalMoney', title: '订单总价', width: 80, align: 'center' },
            { field: 'OrderStatus', title: '订单状态', width: 80, align: 'center', formatter: FormatOrderStatus },
            { field: 'OrderTime', title: '下单时间', width: 120, align: 'center', formatter: FormatDateTime },
            { field: 'PayTime', title: '支付时间', width: 120, align: 'center', formatter: FormatDateTime },
            { field: 'OrderRemark', title: '订单备注', width: 200, align: 'center' },
            { field: 'RowID', title: '操作', width: 150, align: 'center', formatter: FormatOperation }
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
function FormatOrderStatus(v) {
    var res = '';
    switch (parseInt(v)) {
        case 9:
            res = '<span style="color:gray">已删除</span>';
            break;
        case 10:
            res = SetBlue('待付款');
            break;
        case 20:
            res = '<span style="color:#6aa84f">已付款</span>';
            break;
        case 30:
            res = '<span style="color:#674ea7">待收货</span>';
            break;
        case 40:
            res = '<span style="color:#20124d">已收货</span>';
            break;
        case 50:
            res = SetRed('申请退款');
            break;
        case 51:
            res = SetRed('同意退款');
            break;
        case 52:
            res = SetRed('已退款');
            break;
        case 60:
            res = '<span style="color:gray">交易取消</span>';
            break;
        case 70:
            res = SetGreen('交易完成');
            break;
    }
    return res;
}
//按钮格式化 
function FormatOperation(v, row) {
    var btn = '';
    //if ($('#hfEdit').val() == 1) {
        btn += '<a class="EditBtn"  data-value=' + row.OrderNo + ' onclick="Edit(' + row.OrderNo + ')" href="javascript:void(0)">编辑</a>';
    //}
    btn += '<a class="PreviewBtn"  data-value=' + row.OrderNo + ' onclick="Preview(' + row.OrderNo + ')" href="javascript:void(0)">预览订单</a>';
    return btn;
}
//初始化编辑按钮
function loadsuccess(data) {
    $('.EditBtn').linkbutton({ text: '编辑', plain: true, iconCls: 'icon-edit' });
    $('.PreviewBtn').linkbutton({ text: '预览', plain: true, iconCls: 'icon-tip' });

}
//修改
function Edit(orderNo) {
    OpenMaxWin('修改订单', 'MallOrderDetail.aspx?OrderNo=' + orderNo);
}
//预览
function Preview(orderNo) {
    OpenMaxWin('预览订单', 'PrintOrder.aspx?OrderNo=' + orderNo);
}
////详情
//function View(orderNo) {
//    OpenMaxWin('订单详情', 'OrderDetail.aspx?OrderNo=' + orderNo + "&IsEdit=0");
//}

////订单详情
//function View() {
//    var rows = GetSelectValue('ListTable');
//    if (rows.length == 1) {
//        var orderNo = rows[0].OrderNo;
//        OpenMaxWin('订单详情', 'MallOrderDetail.aspx?OrderNo=' + orderNo);
//    }
//    else {
//        AlertInfo('请选择一条要查看的订单！');
//    }
//}
