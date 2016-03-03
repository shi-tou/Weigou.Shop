//文件名：goodselect.js
//描述：商品管理
//时间：2015-5-25
//创建人：zq

$(function () {
    GetList();
});
var url = '/Ajax/Goods.ashx';

//获取查询条件
function GetParams() {
    var param = { 'action': 'GetCarefulGoodsList', 'GoodsName': $('#txtName').val(), 'GoodsType': $("#hfTypeID").val(), 'GoodsStatus': $("#hfStatus").val(), 'GoodsShelvesStatus': $("#hfShelvesStatus").val() };
    return param;
}

//获取商品列表
function GetList() {
    var queryParams = GetParams();
    var tab = $('#ListTable');
    tab.datagrid({
        title: '',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'Name', title: '商品名称', width: 100, align: 'center' },
            { field: 'TypeName', title: '商品类别', width: 100, align: 'center' },
            { field: 'SalePrice', title: '商品价格', width: 100, align: 'center' },
            { field: 'GoodsStar', title: '商品评分', width: 100, align: 'center' }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        queryParams: queryParams,
        pagination: true,
        rownumbers: true,
        singleSelect: false,
        pageSize: 20
    });
    //设置分页
    SetPager(tab);
}

//批量加入商品到严选
function BatchAdd() {
    var batchurl = '/Ajax/Activity.ashx';
    var id = '';
    var rows = GetSelectValue('ListTable');
    if (rows.length >= 1) {
        for (i = 0; i < rows.length; i++) {
            id += rows[i].ID + ",";
        }
        $.messager.confirm('操作提示', '是否确认批量加入商品到严选？', function (r) {
            if (r) {
                $.ajax({
                    url: dealAjaxUrl(batchurl),
                    data: 'action=BatchAdd&ID=' + id,
                    dataType: 'json',
                    type: 'POST',
                    success: function (data) {
                        if (data.res == RT.SUCCESS) {
                            CloseWin('', parent.GetCarefulSelectList);
                        }
                        else {
                            AlertInfo('加入失败，请与管理员联系！');
                        }
                    }
                });
            }
        });
    }
    else {
        AlertInfo('请选择一条或多条要加入到严选的商品');
    }
}
