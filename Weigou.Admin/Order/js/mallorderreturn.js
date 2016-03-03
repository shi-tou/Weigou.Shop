//文件名：mallorderreturn.js
//描述：退款管理
//时间：2015-08-11
//作者：zq
var tabID;
var listTable;
$(function () {
    GetWeixinRefundList();
    GetAlipayRefundList();
    GetUnionpayRefundList();
    $('#RefundTab').tabs({
        onSelect: function (title) {
            if (title == '微信退款管理') {
                tabID = 1;
                listTable = 'WeixinRefundTable';
            }
            else if (title == '支付宝退款管理') {
                tabID = 2;
                listTable = 'AlipayRefundTable';
            }
            else if (title == '银联退款管理') {
                tabID = 2;
                listTable = 'UnionpayRefundTable';
            }
            $('#hdTabID').val(tabID);
        }
    });
});
var url = '/Ajax/Order.ashx';
function GetWhere(obj) {
    var params = "";
    if (obj == 1) {
        params = {
            'action': 'GetMallOrderReturnList', 'OrderNo': $('#txtOrderNo_Wx').val(), 'MobileNo': $('#txtConsigneeMobileNo_Wx').val(), 'MinTime': $('#txtMinTime_Wx').val(), 'MaxTime': $('#txtMaxTime_Wx').val(), 'PayType': 1
        };
    }
    else if (obj == 2) {
        params = {
            'action': 'GetMallOrderReturnList', 'OrderNo': $('#txtOrderNo_AliPay').val(), 'MobileNo': $('#txtConsigneeMobileNo_AliPay').val(), 'MinTime': $('#txtMinTime_AliPay').val(), 'MaxTime': $('#txtMaxTime_AliPay').val(), 'PayType': 2
        };
    }
    else {
        params = {
            'action': 'GetMallOrderReturnList', 'OrderNo': $('#txtOrderNo_Unionpay').val(), 'MobileNo': $('#txtConsigneeMobileNo_Unionpay').val(), 'MinTime': $('#txtMinTime_Unionpay').val(), 'MaxTime': $('#txtMaxTime_Unionpay').val(), 'PayType': 3
        };
    }
    return params;
}
//微信获取退款列表
function GetWeixinRefundList() {    
    $("#WeixinRefundTable").height($(window).height() - 55);
    var queryParams = GetWhere(1);
    var tab = $('#WeixinRefundTable');
    tab.datagrid({
        title: '微信退款列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'OrderNo', title: '订单号', width: 150, align: 'center', formatter: FormatOrderNo },
            { field: 'NotifyTradeNo', title: '流水号', width: 220, align: 'center' },
            { field: 'MobileNo', title: '会员手机号', width: 100, align: 'center' },
            { field: 'MemberName', title: '会员姓名', width: 100, align: 'center' },
            { field: 'Status', title: '处理状态', width: 200, align: 'center', formatter: GetStatus },
            { field: 'ApplyTime', title: '申请时间', width: 150, align: 'center', formatter: FormatDateTime },
            { field: 'DealName', title: '处理人', width: 150, align: 'center' },
            { field: 'DealTime', title: '处理时间', width: 150, align: 'center', formatter: FormatDateTime },
            { field: 'RowID', title: '操作', width: 120, align: 'center', formatter: FormatWechatOperation }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        rownumbers: true, //显示记录数
        queryParams: queryParams, //查询参数
        pagination: true,
        singleSelect: false,
        pageSize: 20,
        nowrap: false,
        onLoadSuccess: loadsuccess
    });
    //设置分页
    SetPager(tab);
}
//获取支付宝退款列表
function GetAlipayRefundList() {
    $("#AlipayRefundTable").height($(window).height() - 55);
    var queryParams = GetWhere(2);
    var tab = $('#AlipayRefundTable');
    tab.datagrid({
        title: '支付宝退款列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'OrderNo', title: '订单号', width: 150, align: 'center', formatter: FormatOrderNo },
            { field: 'NotifyTradeNo', title: '流水号', width: 220, align: 'center' },
            { field: 'MobileNo', title: '会员手机号', width: 100, align: 'center' },
            { field: 'MemberName', title: '会员姓名', width: 100, align: 'center' },
            { field: 'Status', title: '处理状态', width: 100, align: 'center', formatter: GetStatus },
            { field: 'ApplyTime', title: '申请时间', width: 150, align: 'center', formatter: FormatDateTime },
            { field: 'DealName', title: '处理人', width: 150, align: 'center' },
            { field: 'DealTime', title: '处理时间', width: 150, align: 'center', formatter: FormatDateTime },
            { field: 'RowID', title: '操作', width: 120, align: 'center', formatter: FormatAlipayOperation }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        rownumbers: true, //显示记录数
        queryParams: queryParams, //查询参数
        pagination: true,
        singleSelect: false,
        pageSize: 20,
        nowrap: false,
        onLoadSuccess: loadsuccess
    });
    //设置分页
    SetPager(tab);
}

//获取银联退款列表
function GetUnionpayRefundList() {
    $("#UnionpayRefundTable").height($(window).height() - 55);
    var queryParams = GetWhere(3);
    var tab = $('#UnionpayRefundTable');
    tab.datagrid({
        title: '银联退款列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'OrderNo', title: '订单号', width: 150, align: 'center', formatter: FormatOrderNo },
            { field: 'NotifyTradeNo', title: '流水号', width: 220, align: 'center' },
            { field: 'MobileNo', title: '会员手机号', width: 100, align: 'center' },
            { field: 'MemberName', title: '会员姓名', width: 100, align: 'center' },
            { field: 'Status', title: '处理状态', width: 100, align: 'center', formatter: GetStatus },
            { field: 'ApplyTime', title: '申请时间', width: 150, align: 'center', formatter: FormatDateTime },
            { field: 'DealName', title: '处理人', width: 150, align: 'center' },
            { field: 'DealTime', title: '处理时间', width: 150, align: 'center', formatter: FormatDateTime },
            { field: 'RowID', title: '操作', width: 120, align: 'center', formatter: FormatUnionPayOperation }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        rownumbers: true, //显示记录数
        queryParams: queryParams, //查询参数
        pagination: true,
        singleSelect: false,
        pageSize: 20,
        nowrap: false,
        onLoadSuccess: loadsuccess
    });
    //设置分页
    SetPager(tab);
}

function loadsuccess(data) {
    $('.EditBtn').linkbutton({ text: '编辑', plain: true, iconCls: 'icon-edit' });
}
//微信按钮格式化 
function FormatWechatOperation(v, row) {
    var btn = '';
    if ($('#hfWechat').val() == 1) {
        btn += '<a class="EditBtn"  data-value=' + row.OrderNo + ' onclick="Edit(' + row.ID + ',\'' + row.OrderNo + '\')" href="javascript:void(0)">编辑</a>';
    }
    return btn;
}
//支付宝按钮格式化 
function FormatAlipayOperation(v, row) {
    var btn = '';
    if ($('#hfAlipay').val() == 1) {
        btn += '<a class="EditBtn"  data-value=' + row.OrderNo + ' onclick="Edit(' + row.ID + ',\'' + row.OrderNo + '\')" href="javascript:void(0)">编辑</a>';
    }
    return btn;
}
//银联按钮格式化 
function FormatUnionPayOperation(v, row) {
    var btn = '';
    if ($('#hfUnionPay').val() == 1) {
        btn += '<a class="EditBtn"  data-value=' + row.OrderNo + ' onclick="Edit(' + row.ID + ',\'' + row.OrderNo + '\')" href="javascript:void(0)">编辑</a>';
    }
    return btn;
}
//查看退款详情
function Edit(id, orderno) {
    OpenMaxWin('退款详情', 'MallOrderReturnDetail.aspx?ID=' + id + "&OrderNo=" + orderno);
}
//处理状态
function GetStatus(s) {
    switch (parseInt(s)) {
        case 0:
            return '买家申请退款';
        case 1:
            return '<span style="color:red;">卖家同意退款</span>';
        case 2:
            return '<span style="color:green;">订单已退款</span>';
        case 3:
            return '<span style="color:green;">卖家已发货</span>';
        case 4:
            return '<span style="color:red;">同意退款(卖家超24小时未处理)</span>';
        case 5:
            return '<span style="color:red;">申请退款(已付款超15天未发货)</span>';
        default: return '';
    }
}

//按钮格式化
function FormatOrderNo(v, row) {
    var btn = '<a  data-value=' + row.OrderNo + ' onclick="View(' + row.ID + ',\'' + row.OrderNo + '\')" href="javascript:void(0)">' + row.OrderNo + '</a>'
    return btn;
}
//查看退款详情
function View(id, orderno) {
    OpenMaxWin('退款详情', 'MallOrderReturnDetail.aspx?ID=' + id + "&OrderNo=" + orderno + '&IsEdit=0');
}



//微信退款
function RefundWeixin() {
    var orderno = '';
    var rows = GetSelectValue('WeixinRefundTable');
    if (rows.length >= 1) {
        for (i = 0; i < rows.length; i++) {
            if (orderno != '')
                orderno += ",";
            orderno += rows[i].OrderNo;
        }
        $.messager.confirm('操作提示', '是否确认退款操作？', function (r) {
            if (r) {
                window.open("WeixinRefund.aspx?OrderNo=" + orderno);
            }
        });
    }
    else {
        AlertInfo('请选择要退款的记录');
    }
}


function isRepeat(arr) {
    var hash = {};
    for (var i in arr) {
        if (hash[arr[i]])
            return true;
        hash[arr[i]] = true;
    }
    return false;
}

//支付宝退款
function RefundAliPay() {
    var orderno = '';
    var arr = new Array();
    var rows = GetSelectValue('AlipayRefundTable');
    if (rows.length >= 1) {
        for (i = 0; i < rows.length; i++) {
            if (orderno != '') {
                orderno += ",";
            }
            orderno += rows[i].OrderNo;
            //添加流水号到数组
            arr.push(rows[i].NotifyTradeNo);
            //判断数组中是否含有相同流水号
            if (isRepeat(arr)) {
                AlertInfo('批量退款的选项当中不能包含相同流水号的记录');
                return;
            }
        }
        $.messager.confirm('操作提示', '是否确认退款操作？', function (r) {
            if (r) {
                window.open("AliPayRefund.aspx?OrderNo=" + orderno);
            }
        });
    }
    else {
        AlertInfo('请选择要退款的记录');
    }
}

//银联退款
function RefundUnionpay() {
    var orderno = '';
    var arr = new Array();
    var rows = GetSelectValue('UnionpayRefundTable');
    if (rows.length >= 1) {
        for (i = 0; i < rows.length; i++) {
            if (orderno != '')
                orderno += ",";
            orderno += rows[i].OrderNo;
            //添加流水号到数组
            arr.push(rows[i].NotifyTradeNo);
            //判断数组中是否含有相同流水号
            if (isRepeat(arr)) {
                AlertInfo('批量退款的选项当中不能包含相同流水号的记录');
                return;
            }
        }
        $.messager.confirm('操作提示', '是否确认退款操作？', function (r) {
            if (r) {
                window.open("UnionpayRefund.aspx?OrderNo=" + orderno);
            }
        });
    }
    else {
        AlertInfo('请选择要退款的记录');
    }
}


