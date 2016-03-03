//文件名：sms.js
//描述：短信管理
//时间：2013-09-29


var url = '/Ajax/Sms.ashx';
/*===================模板列表===================*/
//获取模板列表
function GetSmsTemplateList() {
    var queryParams = { 'action': 'GetSmsTemplateList', 'Code': $('#txtCode').val(), 'Content': $('#txtContent').val() };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '短信模板列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center',checkbox: true },
            { field: 'Code', title: '模板编号', width: 150, align: 'center' },
            { field: 'Content', title: '模板内容', width: 500},
            { field: 'Description', title: '描述', width: 150, align: 'center' }
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
//添加模板
function AddSmsTemplate() {
    OpenWin('添加模板', 520, 380, 'SmsTemplateAdd.aspx');
}
//修改模板
function EditSmsTemplate() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        id = rows[0].ID;
        OpenWin('修改模板', 520, 380, 'SmsTemplateAdd.aspx?ID=' + id);
    }
    else {
        AlertInfo('请选择一条要修改的记录！');
    }
}
//删除模板
function DeleteSmsTemplate() {
    var code = '';
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        if (rows[0].IsSystem) {
            AlertInfo('系统模板不允许删除！');
            return;
        }
        id = rows[0].ID;
        $.messager.confirm('操作提示', '是否确认删除操作？', function(r) {
            if (r) {
                $.ajax({
                    url: dealAjaxUrl(url),
                    data: 'action=DeleteSmsTemplate&ID=' + id,
                    dataType: 'json',
                    type: 'POST',
                    success: function(data) {
                        if (data.res == RT.FAILED) {
                            AlertInfo('操作失败，请与管理员联系！');
                        }
                        if (data.res == RT.SUCCESS) {
                            AlertInfo('删除成功！');
                            GetSmsTemplateList();
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
/*=========================短信发送===========================*/
//重置短信内容
function clearInfo() {
    $('#txtContent').val('');
    $('#txtContent').removeAttr('readonly');
    $('#hfTempCode').val('');
}
//选择发送类型
function SelectType() {
    var v = 1;
    $("input[name=rblSendType]").each(function() {
        if ($(this).attr("checked")) {
            v = $(this).val();
        }
    });
    if (v == 1) {
        $('#trPresetTime').hide(); //即时
        $('#txtPresetTime').validatebox('remove');
    }
    else {
        $('#trPresetTime').show(); //定时
        $('#txtPresetTime').validatebox('reduce');
    }
}
//验证发送表单
function ValidSmsForm() {
    var objMobileNo = window.document.getElementById("lbMobileNo");
    if (objMobileNo.options.length == 0) {
        AlertInfo("请添加手机号码");
        return false;
    }
    if (!$('#form1').form('validate')) {
        return false;
    }
    
    if ($('#txtSMSContent').val() == '') {
        AlertInfo("请选择模板内容或手动填写内容");
        return false;
    }
    var v = 0;
    $("input[name=rblSendType]").each(function() {
        if ($(this).attr("checked")) {
            v = $(this).val();
        }
    });
    if (v == 2) {
        if ($('#txtPresetTime').val() == '') {
            AlertInfo('请选择定时时间');
            return false;
        }
    }
    $.messager.confirm('操作提示', '请确认发送操作？', function(r) {
        if (r) {
            $('#BtnSubmit').click();
        }
    });
}

/*============短信发送日志===================*/
function GetSmsLogList() {
    var queryParams = { 'action': 'GetSmsLogList', 'MobileNo': $('#txtCode').val(), 'Content': $('#txtContent').val(),
        'SendType': $('#ddlSendType').val(), 'Source': $('#ddlSource').val(), 'Status': $('#ddlStatus').val(), 'MinTime': $('#txtMinTime').val(), 'MaxTime': $('#txtMaxTime').val()
    };
    var tab = $('#ListTable');
    tab.datagrid({
        title: '短信模板列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'MobileNo', title: '手机号', width: 100, align: 'center' },
            { field: 'Content', title: '短信内容', width: 300 },
            { field: 'Count', title: '手机数', width: 60, align: 'center' },
            { field: 'SendType', title: '发送方式', width: 80, align: 'center', formatter: FormatSendType },
            { field: 'Source', title: '来源', width: 80, align: 'center', formatter: FormatSource },
            { field: 'SendTime', title: '(发送/定时)时间', width: 120, align: 'center', formatter: FormatDateTime },
            { field: 'Status', title: '状态', width: 70, align: 'center', formatter: FormatSmsStatus },
            { field: 'CreateUser', title: '发送人', width: 80, align: 'center' },
            { field: 'CreateTime', title: '操作时间', width: 120, align: 'center', formatter: FormatDateTime }
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
//短信发送状态
function FormatSmsStatus(v) {
    if (v == 1) {
        return SetGreen('已发送');
    }
    else {
        return SetRed('待发送');
    }
}
function FormatSendType(v) {
    if (v == 1)
        return '即时';
    else
        return '定时';
}
function FormatSource(v) {
    if (v == 1)
        return '系统';
    else
        return '人工';
}