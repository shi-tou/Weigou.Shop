//文件名：order.js
//描述：订单管理
//时间：2015-06-12
//作者：zq

$(function () {
    GetList();
});

//获取用户列表
function GetList() {
  
    var tab = $('#ListTable');
    tab.datagrid({
        title: '订单列表',
        data: 
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'OrderNo', title: '订单号', width: 120, align: 'center' },
            { field: 'MerchantName', title: '商户名', width: 180, align: 'center' },
            { field: 'MemberName', title: '会员名', width: 100, align: 'center' },
            { field: 'TotalCount', title: '商品总数', width: 80, align: 'center' },
            { field: 'TotaMoney', title: '订单总价', width: 80, align: 'center' },
            { field: 'Status', title: '订单状态', width: 80, align: 'center',formatter:GetOrderStatus },
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

