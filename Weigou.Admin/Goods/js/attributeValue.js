//文件名：goodsvalue.js
//描述：属性值管理
//时间：2015-09-14
//作者:zq

$(function () {
    GetList();
});
var url = '/Ajax/Goods.ashx';

function GetList() {
    $("#ListTable").height($(window).height() - 55);
    var queryParams = { 'action': 'GetAttributeValueList', 'ID': $('#hfAttributeID').val() };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '属性值列表',
        url: dealAjaxUrl(url),
        columns: [[
                        { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
                        { field: 'Value', title: '属性值', width: 200, align: 'center' },
                        { field: 'Sort', title: '排序号', width: 100, align: 'center' },
                        { field: 'CreateName', title: '创建人', width: 100, align: 'center' },
                        { field: 'CreateTime', title: '创建时间', width: 160, align: 'center', formatter: FormatDateTime },
                        { field: 'AttributeID', title: '操作', width: 160, align: 'center', formatter: FormatOpt }
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

function FormatOpt(v, row) {
    var html = '<a href="javascript:void(0)" onclick="Edit(\'' + row.Value + '\',\'' + row.Sort + '\',\'' + row.ID + '\')">修改</a>&nbsp;&nbsp;';
    html += '<a href="javascript:void(0)" onclick="DeleteAttributeValue(\'' + row.ID + '\')">删除</a>';
    return html;
}
function Edit(v, s, id) {
    $("#txtValue").val(v);
    $("#txtSort").val(s);
    $("#hfValueID").val(id);
    $('#BtnSubmit').val('修改');
}

function DeleteAttributeValue(id) {
    $.messager.confirm('操作提示', '是否确认删除操作？', function (r) {
        if (r) {
            $.ajax({
                url: url,
                data: { 'action': 'DeleteAttributeValue', 'ID': id },
                dataType: 'json',
                type: 'POST',
                success: function (data) {
                    if (data.res == 0) {
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