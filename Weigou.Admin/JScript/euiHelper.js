/*
封装easyui的Messager消息窗口
==================================================
参数说明：
title--标题
msg--显示消息内容
showType--定义如何显示消息窗口，可选值: null,slide(滑动),fade(渐隐渐显),show(渐大渐小)，默认为slide
==================================================
*/

//顶部左侧
function showTopLeft(title, msg,showType) {
    $.messager.show({
        title: title,
        msg: msg,
        showType: showType,
        style: {
            right: '',
            left: 0,
            top: document.body.scrollTop + document.documentElement.scrollTop,
            bottom: ''
        }
    });
}
//顶部中间
function showTopCenter(title, msg,showType) {
    $.messager.show({
        title: title,
        msg: msg,
        showType: showType,
        style: {
            right: '',
            top: document.body.scrollTop + document.documentElement.scrollTop,
            bottom: ''
        }
    });
}
//顶部右侧
function showTopRight(title, msg, showType) {
    $.messager.show({
        title: title,
        msg: msg,
        showType: showType,
        style: {
            left: '',
            right: 0,
            top: document.body.scrollTop + document.documentElement.scrollTop,
            bottom: ''
        }
    });
}
//中部左侧
function showCenterLeft(title, msg, showType) {
    $.messager.show({
        title: title,
        msg: msg,
        showType: showType,
        style: {
            left: 0,
            right: '',
            bottom: ''
        }
    });
}
//居中
function showCenter(title, msg, showType) {
    $.messager.show({
        title: title,
        msg: msg,
        showType: showType,
        style: {
            right: '',
            bottom: ''
        }
    });
}
//中部右侧
function showCenterRight(title, msg, showType) {
    $.messager.show({
        title: title,
        msg: msg,
        showType: showType,
        style: {
            left: '',
            right: 0,
            bottom: ''
        }
    });
}
//底部左侧
function showBottomLeft(title, msg, showType) {
    $.messager.show({
        title: title,
        msg: msg,
        showType: showType,
        style: {
            left: 0,
            right: '',
            top: '',
            bottom: -document.body.scrollTop - document.documentElement.scrollTop
        }
    });
}
//底部中间
function showBottomCenter(title, msg, showType) {
    $.messager.show({
        title: title,
        msg: msg,
        showType: showType,
        style: {
            right: '',
            top: '',
            bottom: -document.body.scrollTop - document.documentElement.scrollTop
        }
    });
}
//底部右侧
function showBottomRight(title, msg, showType) {
    $.messager.show({
        title: title,
        msg: msg,
        showType: showType
    });
}
//确定对话框
function confirmBox(title, msg) {
    $.messager.confirm(title, msg, function (r) {
        if (r) {
            return r;
        }
    });
}
//输入框
function promptBox(title, msg) {
    $.messager.prompt(title, msg, function (r) {
        if (r) {
            return r;
        }
    });
}
//弹出消息框
function AlertMsg(msg) {
    $.messager.alert('提示',msg);
}
function AlertError(msg) {
    $.messager.alert('错误提示', '<div style="line-height:30px;">' + msg + '</div>', 'error');
}
function AlertInfo(msg) {
    $.messager.alert('操作提示', '<div style="line-height:30px;">' + msg + '</div>', 'info');
}
function AlertQuestion(msg) {
    $.messager.alert('询问提示', '<div style="line-height:30px;">' + msg + '</div>', 'question');
}
function AlertWaring(msg) {
    $.messager.alert('警告提示', '<div style="line-height:30px;">' + msg + '</div>', 'warning');
}
//提示信息(右下角)
function ShowMsg(msg) {
    $.messager.show({
        title: '<span style="color:red;">操作提示</span>',
        msg: msg,
        showType: 'slide'
    });
}

/*封装easyui的datagrid相关方法
==========================================*/
//easyui-datagrid设置分页控件
function SetPager(tab) {
    var p = tab.datagrid('getPager')
    $(p).pagination({
        pageSize: 20, //每页显示的记录条数，默认为10 
        pageList: [10, 20, 50], //可以设置每页记录条数的列表 
        beforePageText: '第', //页数文本框前显示的汉字 
        afterPageText: '页    共{pages}页',
        displayMsg: '<div style="padding-right:20px;">当前显示 <b>{from} - {to}</b> 条记录   共 <b>{total}</b> 条记录<div>'
    });
}
function SetPagerSimple(tab) {
    var p = tab.datagrid('getPager')
    $(p).pagination({
        pageSize: 10, //每页显示的记录条数，默认为10 
        pageList: [10], //可以设置每页记录条数的列表 
        beforePageText: '第', //页数文本框前显示的汉字 
        afterPageText: '页 共{pages}页',
        displayMsg: '<div style="padding-right:20px;"><div>'
    });
}
//获取datagrid选中行
function GetSelectValue(tab) {
    var rows = $('#' + tab).datagrid('getSelections');
    return rows;
}
//扩展
var myview = $.extend({}, $.fn.datagrid.defaults.view, {
    onAfterRender: function (target) {
        $.fn.datagrid.defaults.view.onAfterRender.call(this, target);
        var opts = $(target).datagrid('options');
        var vc = $(target).datagrid('getPanel').children('div.datagrid-view');
        vc.children('div.datagrid-empty').remove();
        if (!$(target).datagrid('getRows').length) {
            var d = $('<div class="datagrid-empty"></div>').html(opts.emptyMsg || 'no records').appendTo(vc);
            d.css({
                position: 'absolute',
                left: 0,
                top: 50,
                width: '100%',
                textAlign: 'center'
            });
        }
    }
});

/*封装easyui的window相关方法
==========================================*/
//打开弹出窗口
var winDiv = '<div id="win" class="easyui-window" style=" " data-options="iconCls:\'icon-save\',top:\'50px\',closed:true,minimizable:false,maximizable:false,collapsible:false">'
            + '      <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>'
            + '</div>';
function OpenWin(title, w, h, url) {
    //$(document.body).append(winDiv);
    document.getElementById("iframe").src = url;
    $('#win').window({
        title: title,
        width: w,
        height: h,
        modal: true,
        left: (document.body.clientWidth - w) * 0.5,
        closed: false
    });
}
//打开弹出窗口(最大化)
function OpenMaxWin(title, url) {
    document.getElementById("iframe").src = url;
    $('#win').window({
        title: title,
        modal: true,
        closed: false,
        top: 0,
        left: 0,
        fit: true
    });
    
}
//关闭弹出框
function CloseWin(msg, fun) {
    parent.$('#win').window
    ({
        closed: true
    });
    if (msg != undefined && msg != null && msg != '') {
        parent.AlertInfo(msg);
    }
    if (fun != undefined && fun != null && fun != '') {
        fun();
    }
}