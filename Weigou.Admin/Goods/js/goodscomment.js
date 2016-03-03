//文件名：goodcomment.js
//描述：商品评论
//时间：2015-5-28
//创建人：zq

$(function () {
    GetList();
});
var url = '/Ajax/Goods.ashx';

//获取查询条件
function GetParams() {
    var param = { 'action': 'GetGoodsCommentList', 'GoodsName': $('#txtName').val(), 'GoodsType': $("#hfTypeID").val(), 'Reply': $('#ddlReply').val() };
    return param;
}

//获取商品列表
function GetList() {
    var queryParams = GetParams();
    var tab = $('#ListTable');
    tab.datagrid({
        title: '评论列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'GoodsName', title: '商品名称', width: 200, align: 'center' },
           // { field: 'MerchantName', title: '商户名称', width: 150, align: 'center' },
            { field: 'GoodsType', title: '商品类别', width: 100, align: 'center' },
            { field: 'Star', title: '商品评分', width: 80, align: 'center' },
            { field: 'Content', title: '首次评论内容', width: 450, align: 'center' },
            { field: 'MemberName', title: '评论人', width: 80, align: 'center' },
            { field: 'CreateTime', title: '首次评论时间', width: 160, align: 'center', formatter: FormatDateTime },
            { field: 'ReplyBys', title: '回复状态', width: 80, align: 'center', formatter: GetReplyStatus },
            { field: 'RowID', title: '操作', width: 60, align: 'center', formatter: FormatOperation }
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

////回复
//function Reply() {
//    var rows = GetSelectValue('ListTable');
//    if (rows.length == 1) {
//        id = rows[0].ID;
//        OpenMaxWin('查看评论及回复', 'GoodsCommentReply.aspx?ID=' + id);
//    }
//    else {
//        AlertInfo('请选择一条需要查看的记录！');
//    }
//}

//按钮格式化 
function FormatOperation(v, row) {
    var btn = '';
    if ($('#hfReply').val() == 1) {
        btn += '<a class="EditBtn"  data-value=' + row.ID + ' onclick="Reply(' + row.ID + ')" href="javascript:void(0)">回复</a>';
    }
    return btn;
}
//初始化编辑按钮
function loadsuccess(data) {
    $('.EditBtn').linkbutton({ text: '回复', plain: true, iconCls: 'icon-edit' });
}

function FormatName(v, row) {
    var alink = '<a class="Detail"  data-value=' + row.ID + ' onclick="View(' + row.ID + ')" href="javascript:void(0)">' + row.GoodsName + '</a>';
    return alink;
}

function GetReplyStatus(v) {
    if (v != '0') {
        return '<span style="color:green;">已回复</span>';
    }
    else {
        return '<span style="color:red;">未回复</span>';
    }
}

//回复
function Reply(id) {
    OpenMaxWin('回复评论', 'GoodsCommentReply.aspx?ID=' + id);
}
//查看
function View(id) {
    OpenMaxWin('评论详情', 'GoodsCommentReply.aspx?ID=' + id + "&IsEdit=0");
}