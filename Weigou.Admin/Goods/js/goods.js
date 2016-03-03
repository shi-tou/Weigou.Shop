//文件名：good.js
//描述：商品管理
//时间：2015-5-25
//创建人：zq

$(function () {
    GetList();
});
var url = '/Ajax/Goods.ashx';

//获取查询条件
function GetParams() {
    var param = { 'action': 'GetGoodsList', 'GoodsName': $('#txtName').val(), 'GoodsType': $("#hfTypeID").val(), 'GoodsStatus': $("#ddlStatus").val(), 'GoodsShelvesStatus': $("#ddlShelvesStatus").val() };
    return param;
}

//获取商品列表
function GetList() {
    var queryParams = GetParams();
    var tab = $('#ListTable');
    tab.datagrid({
        title: '商品列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'Name', title: '商品名称', width: 150, align: 'center' },
            { field: 'SmallPicture', title: '商品图片', width: 120, align: 'center', formatter: FormatPic },
            { field: 'TypeName', title: '类别', width: 100, align: 'center' },
            //{ field: 'BrandName', title: '品牌', width: 100, align: 'center' },
            { field: 'SalePrice', title: '销售价', width: 60, align: 'center', formatter: SetRed },
            { field: 'MarketPrice', title: '市场价', width: 60, align: 'center', formatter: SetRed },
            { field: 'Stock', title: '商品库存', width: 100, align: 'center', formatter: SetBlue },
            { field: 'Status', title: '审核状态', width: 80, align: 'center', formatter: GetStatus },
            { field: 'ShelvesStatus', title: '上架状态', width: 80, align: 'center', formatter: GetShelvesStatus },
            { field: 'CreateName', title: '创建人', width: 80, align: 'center' },
            { field: 'CreateTime', title: '创建时间', width: 160, align: 'center', formatter: FormatDateTime },
            { field: 'ApprovedName', title: '审核人', width: 100, align: 'center' },
            { field: 'ApprovedTime', title: '审核时间', width: 160, align: 'center', formatter: FormatDateTime }
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

//审核状态
function GetStatus(s) {
    switch (parseInt(s)) {
        case 0:
            return '待审核';
        case 1:
            return '<span style="color:green;">审核通过</span>';
        case 2:
            return '<span style="color:red;">审核未通过</span>';
        default: return '';
    }
}

//上架状态
function GetShelvesStatus(s) {
    switch (parseInt(s)) {
        case 0:
            return '下架';
        case 1:
            return '<span style="color:green;">上架</span>';
        default: return '';
    }
}



function FormatPic(v) {
    if (v == '' || v == null) {
        return '暂无置顶图片';
    }
    else
        return '<img src="' + v + '" style="width:150px; height:50px;" />';
}

//库存提醒设置
function FormatStock(v) {
    if (v > 10)
        return v;
    return SetRed(v);
}

//添加
function Add() {
    OpenMaxWin('添加商品', 'GoodsAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenMaxWin('修改商品', 'GoodsAdd.aspx?ID=' + id);
    }
    else {
        AlertInfo('请选择一条要修改的记录！');
    }
}
//审核
function Audit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        typename = rows[0].TypeName;
        OpenMaxWin('审核商品', 'GoodsAudit.aspx?ID=' + id + "&TypeName=" + typename);
    }
    else {
        AlertInfo('请选择一条要审核的记录！');
    }
}
//删除
function Delete() {
    var code = '';
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        $.messager.confirm('操作提示', '是否确认删除操作？', function (r) {
            if (r) {
                $.ajax({
                    url: dealAjaxUrl(url),
                    data: 'action=DeleteGoods&ID=' + id,
                    dataType: 'json',
                    type: 'POST',
                    success: function (data) {
                        if (data.res == RT.SUCCESS) {
                            AlertInfo('删除成功！');
                            GetList();
                        }
                        else {
                            AlertInfo('删除失败，请与管理员联系！');
                        }
                    }
                });
            }
        });
    }
    else {
        AlertInfo('请选择要删除的记录');
    }
}

//图片管理
function ManagePic() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        name = rows[0].Name;
        //type=2为商品图片
        OpenMaxWin('商品图片管理', 'ManagePic.aspx?TargetID=' + id + '&Name=' + escape(name) + "&Type=2");
    }
    else {
        AlertInfo('请选择一条记录！');
    }
}