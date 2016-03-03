//文件名：goodstype.js
//描述：商品类别管理
//时间：2015-5-25
//创建人：zq

$(function () {
    GetList();
});
var url = '/Ajax/Goods.ashx';
//获取查询条件
function GetParams() {
    var param = { 'action': 'GetGoodsType' };
    return param;
}


//获取第一级商品类别列表
function GetList() {
    var queryParams = GetParams();
    var tab = $('#ListTable');
    tab.datagrid({
        title: '第一级商品分类列表',
        width: 230,
        height: $(window).height() - 35,
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'Name', title: '类别名称', width: 200, align: 'center' },
        ]],
        onBeforeLoad: function () {
            $(this).datagrid('rejectChanges');
        },
        onClickRow: function (index, data) {
            var row = $('#ListTable').datagrid('getSelected');
            if (row != null) {
                var curTypeId = row.ID;
                $("#hdTypeID").val(curTypeId);
                $("#hdTypeName").val(row.Name);
                GetList1(curTypeId);
            }
        },
        loadMsg: '正在加载数据，请稍候……',
        queryParams: queryParams,
        pagination: false,
        rownumbers: true,
        singleSelect: true,
        pageSize: 20
    });
}

//获取第二级商品类别列表
function GetList1(curParentId) {
    var queryParams = { 'action': 'GetGoodsType', 'ParentID': curParentId };
    var tab = $('#ListTable1');
    tab.datagrid({
        title: '第二级商品分类列表',
        width: 230,
        height: $(window).height() - 35,
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'Name', title: '类别名称', width: 200, align: 'center' },
        ]],
        onBeforeLoad: function () {
            $(this).datagrid('rejectChanges');
        },
        onClickRow: function (index, data) {
            var row = $('#ListTable1').datagrid('getSelected');
            if (row != null) {
                var curTypeId = row.ID;
                $("#hdTypeID").val(curTypeId);
                $("#hdTypeName").val(row.Name);
                //GetList2(curTypeId);
            }
        },
        onDblClickRow: SelectRow,
        loadMsg: '正在加载数据，请稍候……',
        queryParams: queryParams,
        pagination: false,
        rownumbers: true,
        singleSelect: true,
        pageSize: 20
    });
}

//获取第三级商品类别列表
//function GetList2(curParentId) {
//    var queryParams = { 'action': 'GetGoodsType', 'ParentID': curParentId };
//    var tab = $('#ListTable2');
//    tab.datagrid({
//        title: '第三级商品分类列表',
//        width: 150,
//        height: $(window).height() - 35,
//        url: dealAjaxUrl(url),
//        columns: [[
//            { field: 'Name', title: '类别名称', width: 100, align: 'center' },
//        ]],
//        onBeforeLoad: function () {
//            $(this).datagrid('rejectChanges');
//        },
//        onClickRow: function (index, data) {
//            var row = $('#ListTable2').datagrid('getSelected');
//            if (row != null) {
//                var curTypeId = row.ID;
//                $("#hdTypeID").val(curTypeId);
//                $("#hdTypeName").val(row.Name);

//            }
//        },
//        onDblClickRow: SelectRow,
//        loadMsg: '正在加载数据，请稍候……',
//        queryParams: queryParams,
//        pagination: false,
//        rownumbers: true,
//        singleSelect: true,
//        pageSize: 20
//    });
//}

//双击选择行
function SelectRow() {

    //为控件赋值
    var id = $("#hdTypeID").val();
    var name = $("#hdTypeName").val();

    var idControl = $('#hfIDControl').val();
    var nameControl = $('#hfNameControl').val();

    if (idControl != '')
        parent.$('#' + idControl).val(id);
    if (nameControl != '')
        parent.$('#' + nameControl).val(name);
    CloseWin('', null);
}

