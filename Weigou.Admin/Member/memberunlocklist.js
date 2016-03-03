//文件名：MemberUnlockLis.js
//描述：会员解冻管理
//时间：2015-05-28

$(function() {
    GetList();
});
var url = '/Ajax/Member.ashx';
//获取会员解冻列表
function GetList() {
    var queryParams = { 'action': 'GetMemberUnlockList' };
    var queryParams = {
        'action': 'GetMemberUnlockList', 'MemberName': $('#txtMemberName').val(), 'MobileNo': $('#txtMobileNo').val(),
        'Status': $('#ddlStatus').val(), 'MinTime': $('#txtMinTime').val(), 'MaxTime': $('#txtMaxTime').val()
    };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '会员解冻列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'MemberName', title: '姓名', width: 100, align: 'center' },
            { field: 'MemberStatus', title: '会员状态', width: 100, align: 'center', formatter: GetMemberStatus },
            { field: 'Remark', title: '申请备注', width: 150, align: 'center' },
            { field: 'CreateTime', title: '申请时间', width: 120, align: 'center', formatter: FormatDateTime },
            { field: 'Status', title: '申请状态', width: 90, align: 'center', formatter: GetStatus },
            { field: 'ApprovedName', title: '审核人', width: 100, align: 'center' },
            { field: 'ApprovedTime', title: '审核时间', width: 120, align: 'center', formatter: FormatDateTime },
            { field: 'ApprovedRemark', title: '审核备注', width: 120, align: 'center' }
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

function GetMemberStatus(v) {
    if (v == 1)
        return SetGreen('已激活');
    else if (v == 2)
        return SetRed('冻结');
}

function GetStatus(v) {
    if (v == 0)
        return SetBlue('待审核');
    else if (v == 1)
        return SetGreen('审核通过');
    else
        return SetRed('审核不通过');
}
//审核通过
function Audit() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenMaxWin('会员申请解冻审核', 'MemberUnlockAudit.aspx?ID=' + id + '&MemberID=' + rows[0].MemberID);
    }
    else {
        AlertInfo('请选择一条要审核的记录！');
    }
}

