//文件名：memberselect.js
//描述：会员选择
//时间：2013-12-09

$(function () {
    GetList();
});
var url = '/Ajax/Member.ashx';
//获取资源列表
function GetList() {
    var queryParams = { 'action': 'GetMemberList', 'Name': $('#txtName').val(), 'MobileNo': $('#txtMobileNo').val(), 'Sex': $('#ddlSex').val() };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'Name', title: '姓名', width: 130, align: 'center' },
            { field: 'Sex', title: '性别', width: 70, align: 'center', formatter: GetSex },
            { field: 'MobileNo', title: '手机号', width: 120, align: 'center' },
            { field: 'LevelName', title: '会员等级', width: 120, align: 'center' }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        rownumbers: true, //显示记录数
        queryParams: queryParams, //查询参数
        pagination: true,
        singleSelect: true,
        pageSize: 20,
        nowrap: false,
        onDblClickRow: SelectRow
    });
    //设置分页
    SetPager(tab);
}
//双击选择行
function SelectRow() {
    var rows = GetSelectValue('ListTable');
    //会员ID-卡号-姓名
    var id = rows[0].ID;
    var name = rows[0].Name;
    //控件名
    var idControl = $('#hfIDControl').val();
    var nameControl = $('#hfNameControl').val();
    //为控件赋值
    if (idControl != '')
        parent.$('#' + idControl).val(id);
    if (nameControl != '')
        parent.$('#' + nameControl).val(name);
    //关闭窗口
    CloseWin('', parent.SelectBack);
}