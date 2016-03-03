//文件名：report.js
//描述：报表管理
//时间：2013-12-09


var url = dealAjaxUrl('/Ajax/Report.ashx');
/*===============================会员统计报表====================================*/
function GetMemberReport() {
    var queryParams = GetMemberReportWhere('GetMemberReport');
    var tab = $('#ListTable');
    tab.datagrid({
        title: '会员统计列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'Name', title: '姓名', width: 70, align: 'center' },
            { field: 'Sex', title: '性别', width: 50, align: 'center', formatter: GetSex },
            { field: 'MobileNo', title: '手机号', width: 90, align: 'center' },
            { field: 'Email', title: '邮箱', width: 130, align: 'center' },
            { field: 'Birthday', title: '生日', width: 60, align: 'center', formatter: FormatDay },
            { field: 'MemberScore', title: '会员积分', width: 60, align: 'center' },
            //{ field: 'CompanyName', title: '公司名称', width: 150, align: 'center' },
            //{ field: 'Address', title: '住址', width: 150, align: 'center' },
            { field: 'ProvinceName', title: '省份', width: 50, align: 'center' },
            { field: 'CityName', title: '城市', width: 50, align: 'center' },
            { field: 'DistrictName', title: '地区', width: 50, align: 'center' },
            { field: 'Status', title: '状态', width: 60, align: 'center', formatter: GetStatus },
            { field: 'Remark', title: '备注', width: 150, align: 'center' },
            { field: 'CreateTime', title: '注册时间', width: 120, align: 'center', formatter: FormatDateTime },
            { field: 'CreateUser', title: '创建人', width: 80, align: 'center' }
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

//启用状态
function GetStatus(s) {
    switch (parseInt(s)) {
        case 0:
            return '<span style="color:red;">冻结</span>';
        case 1:
            return '<span style="color:green;">启用</span>';
        default: return '';
    }
}

//性别
function GetSex(s) {
    switch (parseInt(s)) {
        case 0:
            return '男';
        case 1:
            return '女';
        default: return '';
    }
}

//导出会员报表
function ExportMemberReport() {
    var queryParams = GetMemberReportWhere('ExportMemberReport');
    OpenWin('', 0, 0, url + '&' + JsonToParam(queryParams));
    $('#win').window({ closed: true });
    return false;
}
//会员报表查询参数
function GetMemberReportWhere(action) {
    var q = {
        'action': action, 'Name': $('#txtName').val(), 'Email': $('#txtEmail').val(), 'MobileNo': $('#txtMobileNo').val(),
        'Sex': $('#ddlSex').val(), 'Status': $('#ddlStatus').val(), 'MinTime': $('#txtMinTime').val(), 'MaxTime': $('#txtMaxTime').val()
    };
    return q;
}
/*===============================积分统计报表====================================*/
function GetScoreReport() {
    var queryParams = GetScoreReportWhere('GetScoreReport');
    var tab = $('#ListTable');
    tab.datagrid({
        title: '积分统计列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'MemberName', title: '会员帐户', width: 100, align: 'center' },            
            { field: 'Score', title: '变动积分', width: 110, align: 'center', formatter: FormatChangeAmount },
            { field: 'Balance', title: '剩余积分', width: 110, align: 'center' },
            { field: 'ScoreType', title: '积分来源', width: 110, align: 'center', formatter: GetScoreType },
            { field: 'OrderNo', title: '订单号', width: 130, align: 'center' },
            { field: 'Remark', title: '备注', width: 150, align: 'center' },
            { field: 'CreateTime', title: '操作时间', width: 135, align: 'center', formatter: FormatDateTime },
            { field: 'CreateName', title: '操作人', width: 100, align: 'center' }
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
//积分变动
function FormatChangeAmount(v) {
    if (v >= 0)
        return SetBlue(v);
    else
        return SetRed(v);
}
//积分来源
function GetScoreType(s) {
    switch (parseInt(s)) {
        case 1:
            return '商户广告积分';
        case 2:
            return '商户娱乐积分';
        case 3:
            return '会员金币积分';
        case 4:
            return '会员赠品积分';
        case 5:
            return '会员娱乐积分';
        default: return '';
    }
}
//导出积分报表
function ExportScoreReport() {
    var queryParams = GetScoreReportWhere('ExportScoreReport');
    OpenWin('', 0, 0, url + '&' + JsonToParam(queryParams));
    $('#win').window({ closed: true });
    return false;
}
//积分报表查询参数
function GetScoreReportWhere(action) {
    var q = {
        'action': action, 'MemberName': $('#txtMemberName').val(), 'MerchantName': $('#txtMerchantName').val(),'ScoreType': $('#ddlScoreType').val(), 'MinTime': $('#txtMinTime').val(), 'MaxTime': $('#txtMaxTime').val()
    };
    return q;
}

/*===============================商品统计报表====================================*/
function GetGoodsReport() {
    var queryParams = GetGoodsReportWhere('GetGoodsReport');
    var tab = $('#ListTable');
    tab.datagrid({
        title: '商品统计列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'Name', title: '商品名称', width: 400, align: 'center' },
            { field: 'TypeName', title: '商品类别', width: 100, align: 'center' },
            { field: 'Sales', title: '销售量', width: 100, align: 'center' },
            { field: 'GoodsStar', title: '评分', width: 100, align: 'center' },
            { field: 'SalePrice', title: '销售价', width: 50, align: 'center' },
            { field: 'MarketPrice', title: '市场价', width: 50, align: 'center' },
            { field: 'Stock', title: '商品库存', width: 100, align: 'center' },
            { field: 'Status', title: '审核状态', width: 80, align: 'center', formatter: GetStatu },
            { field: 'ShelvesStatus', title: '上架状态', width: 80, align: 'center', formatter: GetShelvesStatus },
            { field: 'CreateName', title: '创建人', width: 80, align: 'center' },
            { field: 'CreateTime', title: '创建时间', width: 160, align: 'center', formatter: FormatDateTime },
            { field: 'ApprovedName', title: '审核人', width: 100, align: 'center' },
            { field: 'ApprovedTime', title: '审核时间', width: 160, align: 'center', formatter: FormatDateTime }
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


function GetGoodsReportWhere(action) {
    var q = {
        'action': action, 'GoodsName': $('#txtName').val(), 'GoodsType': $("#hfTypeID").val(), 'GoodsStatus': $("#ddlStatus").val(), 'GoodsShelvesStatus': $("#ddlShelvesStatus").val()
    };
    return q;
}
//导出商品列表
function ExportGoodsReport() {
    var queryParams = GetGoodsReportWhere('ExportGoodsReport');
    OpenWin('', 0, 0, url + '&' + JsonToParam(queryParams));
    $('#win').window({ closed: true });
    return false;
}


function FormatPic(v) {
    if (v == '' || v == null) {
        return '暂无置顶图片';
    }
    else
        return '<img src="' + v + '" style="width:150px; height:50px;" />';
}

//审核状态
function GetStatu(s) {
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


//库存提醒设置
function FormatStock(v) {
    if (v > 10)
        return v;
    return SetRed(v);
}

/*===============================订单统计报表====================================*/
function GetOrderReport() {
    var queryParams = GetOrderReportWhere('GetOrderReport');
    var tab = $('#ListTable');
    tab.datagrid({
        title: '订单统计列表',
        url: dealAjaxUrl(url),
        columns: [[
            { field: 'ID', title: '', width: 30, align: 'center', checkbox: true },
            { field: 'OrderNo', title: '订单号', width: 140, align: 'center' },
            { field: 'PayMentType', title: '支付类型', width: 100, align: 'center', formatter: GetPatMentType },
            { field: 'TotalMoney', title: '订单总金额', width: 80, align: 'center' },
           // { field: 'LogisticsPrice', title: '物流费用', width: 80, align: 'center' },
            { field: 'DeliverAddress', title: '收货地址', width: 200, align: 'center' },
            { field: 'ConsigneeMobileNo', title: '收货手机号', width: 100, align: 'center' },
            { field: 'ConsigneeName', title: '收货人', width: 100, align: 'center' },
            { field: 'OrderStatus', title: '订单状态', width: 120, align: 'center', formatter: GetOrderStatus },
            { field: 'OrderTime', title: '下单时间', width: 150, align: 'center', formatter: FormatDateTime },
            { field: 'PayTime', title: '支付时间', width: 150, align: 'center', formatter: FormatDateTime }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        rownumbers: true, //显示记录数
        queryParams: queryParams, //查询参数
        pagination: true,
        singleSelect: false,
        pageSize: 20,
        nowrap: false
    });
    //设置分页
    SetPager(tab);
}

//支付类型
function GetPatMentType(s) {
    switch (parseInt(s)) {
        case 1:
            return '微信';
        case 2:
            return '支付宝';
        case 3:
            return '银联';
        default: return '';
    }
}

//导出订单报表
function ExportOrderReport() {
    var queryParams = GetOrderReportWhere('ExportOrderReport');
    OpenWin('', 0, 0, url + '&' + JsonToParam(queryParams));
    $('#win').window({ closed: true });
    return false;
}
//订单报表查询参数
function GetOrderReportWhere(action) {
    var q = {
        'action': action, 'OrderNo': $('#txtOrderNo').val(), 'MemberName': $('#txtMemberName').val(),
        'MemberMobileNo': $('#txtMemberMobileNo').val(), 'OrderStatus': $('#ddlOrderStatus').val(), 'MinTime': $('#txtMinTime').val(), 'MaxTime': $('#txtMaxTime').val()
    };
    return q;
}
//订单详细
function View() {
    var rows = GetSelectValue('ListTable');
    if (rows.length == 1) {
        var orderNo = rows[0].OrderNo;
        OpenMaxWin('订单详情', '../Order/OrderDetail.aspx?OrderNo=' + orderNo);
    }
    else {
        AlertInfo('请选择一条要查看的订单！');
    }
}