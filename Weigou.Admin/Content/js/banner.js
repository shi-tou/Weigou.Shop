//文件名：banner.js
//描述：Banner图管理
//时间：2015-05-27

var url = '/Ajax/Content.ashx';
$(function () {
    GetList();
});
//获取公告列表
function GetList() {
    var queryParams = { 'action': 'GetBannerList', 'Type': $('#ddlType').val() };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '',
        url: dealAjaxUrl(url),
        columns: [[
                        { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
                        { field: 'Picture', title: '图片', width: 200, align: 'center', formatter: FormatPic },
                        { field: 'TypeName', title: '分类', width: 100, align: 'center' },
                        { field: 'Title', title: '标题', width: 150, align: 'center' },
                        { field: 'SimpleDesc', title: '简单描述', width: 150, align: 'center' },
                        { field: 'Url', title: '跳转链接', width: 150, align: 'center' },
                        { field: 'CreateName', title: '创建人', width: 120, align: 'center' },
                        { field: 'CreateTime', title: '创建时间', width: 130, align: 'center', formatter: FormatDateTime }
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

function FormatPic(v) {
    return '<img style="width:150px; height:80px" src="' + v + '">';
}

//添加
function Add() {
    OpenWin('修改Banner图信息', 550, 450, 'BannerAdd.aspx');
}
//修改
function Edit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        OpenWin('修改Banner图信息', 550, 450, 'BannerAdd.aspx?ID=' + rows[0].ID );
    }
    else {
        AlertInfo('请选择一条要修改的记录！');
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
                    data: 'action=DeleteBanner&ID=' + id,
                    dataType: 'json',
                    type: 'POST',
                    success: function (data) {
                        if (data.res == RT.SUCCESS) {
                            AlertInfo('删除成功！');
                            GetList();
                        }
                        else {
                            AlertInfo('该数据已在系统中使用，不能删除！');
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
