//文件名：master.js
//描述：用户管理
//时间：2013-09-29


$(function() {
    GetList();
});
var url = '/Ajax/Sys.ashx';
//获取用户列表
function GetList() {
    var queryParams = { 'action': 'GetLogList', 'Module': $('Module').val(), 'Operation': $('#ddlOperation').val(),
        'Content': $('#txtContent').val(), 'MinTime': $('#txtMinTime').val(), 'MaxTime': $('#txtMaxTime').val()
    };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '操作日志列表',
        url: dealAjaxUrl(url),
        columns: [[
                        { field: 'ID', title: '', width: 30, align: 'center',checkbox: true },
                        { field: 'Module', title: '模块', width: 100, align: 'center',formatter:GetModule },
                        { field: 'Operation', title: '操作', width: 100, align: 'center', formatter: GetOperation },
                        { field: 'Content', title: '内容', width: 200, align: 'center' },
                        { field: 'IP', title: 'IP', width: 100, align: 'center' },
                        { field: 'CreateUser', title: '操作人', width: 120, align: 'center' },
                        { field: 'CreateTime', title: '时间', width: 150, align: 'center', formatter: FormatDateTime }
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
//功能模块
function GetModule(m) {
    var res = "";
    switch (m) {
        case EnumModule.MerchantManage:
            res = "商户管理";
            break;
        case EnumModule.SystemManage:
            res = "系统管理";
            break;
        case EnumModule.MemberManage:
            res = "会员管理";
            break;
        case EnumModule.GoodsManage:
            res = "商品管理";
            break;
        case EnumModule.SmsManage:
            res = "短信管理";
            break;
        case EnumModule.ReportManage:
            res = "报表管理";
            break;
        case EnumModule.OrderManage:
            res = "订单管理";
            break;
        case EnumModule.Other:
            res = "其他";
            break;
    }
    return res;
}
//功能操作
function GetOperation(v) {
    var res = "";
    switch (v) {
        case EnumOperation.Login:
            res = "登录";
            break;
        case EnumOperation.Add:
            res = "添加";
            break;
        case EnumOperation.Edit:
            res = "编辑";
            break;
        case EnumOperation.Delete:
            res = "删除";
            break;
        case EnumOperation.Audit:
            res = "审核";
            break;
        case EnumOperation.Import:
            res = "导入";
            break;
        case EnumOperation.Export:
            res = "导出";
            break;
        case EnumOperation.LoginOut:
            res = "登出";
            break;
        case EnumOperation.Other:
            res = "其他";
            break;
    }
    return res;
}