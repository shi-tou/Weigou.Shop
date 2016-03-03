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
        width: 400,
        height: $(window).height() - 35,
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'Name', title: '类别名称', width: 100, align: 'center' },
            { field: 'Remark', title: '描述', width: 250, align: 'center' }
        ]],
        onBeforeLoad: function () {
            $(this).datagrid('rejectChanges');
        },
        onClickRow: function (index, data) {
            var row = $('#ListTable').datagrid('getSelected');
            if (row != null) {
                $("#hdCurrentIndex").val('1');
                var curParentId = row.ID;
                $("#hdCurrentID").val(curParentId);
                var index = $('#ListTable').datagrid('getRowIndex', row);
                $("#hdCurRowIndex_1").val(index);//当前选中行的索引
                $("#hdCurRowID_1").val(curParentId);//当前选中行的ID
                GetList1(curParentId);
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
        width: 400,
        height: $(window).height() - 35,
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'Name', title: '类别名称', width: 100, align: 'center' },
            { field: 'Remark', title: '描述', width: 250, align: 'center' }
        ]],
        onBeforeLoad: function () {
            $(this).datagrid('rejectChanges');
        },
        onClickRow: function (index, data) {
            var row = $('#ListTable1').datagrid('getSelected');
            if (row != null) {
                var curParentId = row.ID;
                $("#hdCurrentID").val(curParentId);
                $("#hdCurrentIndex").val('2');
                var index = $('#ListTable1').datagrid('getRowIndex', row);
                $("#hdCurRowIndex_2").val(index);//当前选中行的索引
                $("#hdCurRowID_2").val(curParentId);//当前选中行的ID
                //GetList2(curParentId);
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

//获取第三级商品类别列表
//function GetList2(curParentId) {
//    var queryParams = { 'action': 'GetGoodsType', 'ParentID': curParentId };
//    var tab = $('#ListTable2');
//    tab.datagrid({
//        title: '第三级商品分类列表',
//        width: 350,
//        height: $(window).height() - 35,
//        url: dealAjaxUrl(url),
//        columns: [[
//            { field: 'Name', title: '类别名称', width: 100, align: 'center' },
//            { field: 'Remark', title: '描述', width: 200, align: 'center' }
//        ]],
//        onBeforeLoad: function () {
//            $(this).datagrid('rejectChanges');
//        },
//        onClickRow: function (index, data) {
//            var row = $('#ListTable2').datagrid('getSelected');
//            if (row != null) {
//                var curParentId = row.ID;
//                $("#hdCurrentID").val(curParentId);
//                $("#hdCurrentIndex").val('3');
//                var index = $('#ListTable2').datagrid('getRowIndex', row);
//                $("#hdCurRowIndex_3").val(index);//当前选中行的索引
//                $("#hdCurRowID_3").val(curParentId);//当前选中行的ID
//            }
//        },
//        loadMsg: '正在加载数据，请稍候……',
//        queryParams: queryParams,
//        pagination: false,
//        rownumbers: true,
//        singleSelect: true,
//        pageSize: 20
//    });
//}




function FormatParentName(v) {
    if (v == '')
        return '无';
    return v;
}
//添加
function Add() {
    if ($("#hdCurrentIndex").val() == '3') {
        AlertInfo('最多只有三级类别，不能再继续添加子类');
    }
    else {
        $("#hdIsAdd").val('1');//新增
        OpenWin('添加商品类别', 450, 300, 'GoodsTypeAdd.aspx?ID=' + $("#hdCurrentID").val() + "&IsAdd=" + $("#hdIsAdd").val());
    }
}
//修改
function Edit() {
    $("#hdIsAdd").val('2');//修改
    if ($("#hdCurrentID").val() != '') {
        OpenWin('修改商品类别', 450, 300, 'GoodsTypeAdd.aspx?ID=' + $("#hdCurrentID").val() + "&IsAdd=" + $("#hdIsAdd").val());
    }
    else {
        AlertInfo('请选择一条要修改的记录！');
    }
}
//删除
function Delete() {
    var id = $("#hdCurrentID").val();
    if (id != '') {
        $.messager.confirm('操作提示', '是否确认删除操作？', function (r) {
            if (r) {
                $.ajax({
                    url: dealAjaxUrl(url),
                    data: 'action=DeleteGoodsType&ID=' + id,
                    dataType: 'json',
                    type: 'POST',
                    success: function (data) {
                        if (data.res == 0) {
                            AlertInfo('删除成功！');
                            ReLoad();
                        }
                        else {
                            AlertInfo('该类别下还有子类别数据，不能删除！如要删除请先将类别下的子类别全部删除。');
                        }
                    }
                });
            }
        });
    }
    else {
        AlertInfo('请选择要删除的分类');
    }
}

//设置商品分类品牌
function GoodsBrandSet() {
    if ($("#hdCurrentID").val() != '') {
        if ($("#hdCurrentIndex").val() == '2') {
            OpenMaxWin('设置商品分类品牌', 'GoodsBrandSet.aspx?ID=' + $("#hdCurrentID").val());
        }
        else {
            AlertInfo('请选择第二级分类下面的一条要设置的记录！');
        }
    }
    else {
        AlertInfo('请选择一条要设置的记录！');
    }

}

//设置商品分类属性
function GoodsAttributeSet() {
    if ($("#hdCurrentID").val() != '') {
        if ($("#hdCurrentIndex").val() == '2') {
            OpenMaxWin('设置商品分类属性', 'GoodsAttributeSet.aspx?ID=' + $("#hdCurrentID").val());
        }
        else {
            AlertInfo('请选择第二级分类下面的一条要设置的记录！');
        }
    }
    else {
        AlertInfo('请选择一条要设置的记录！');
    }

}

function ReLoad() {
    var Index_1 = $("#hdCurRowIndex_1").val();//第一级选中行的索引
    var Index_2 = $("#hdCurRowIndex_2").val();//第二级选中行的索引
    //var Index_3 = $("#hdCurRowIndex_3").val();//第三级选中行的索引
    var ID_1 = $("#hdCurRowID_1").val();//第一级选中行的ID
    var ID_2 = $("#hdCurRowID_2").val();//第二级选中行的ID

    //加载第一级
    GetList();
    $('#ListTable').datagrid({
        onLoadSuccess: function (data) {
            $('#ListTable').datagrid('selectRow', Index_1);
        }
    });
    //加载第二级
    if (ID_1 != "") {
        GetList1(ID_1);
        if (Index_2 != '') {
            $('#ListTable1').datagrid({
                onLoadSuccess: function (data) {
                    $('#ListTable1').datagrid('selectRow', Index_2);
                }
            });
        }
    }
    //加载第三级
    //if (ID_2 != "") {
    //    GetList2(ID_2);
    //    if (Index_3 != '') {
    //        $('#ListTable2').datagrid({
    //            onLoadSuccess: function (data) {
    //                $('#ListTable2').datagrid('selectRow', Index_3);
    //            }
    //        });
    //    }
    //}
}
