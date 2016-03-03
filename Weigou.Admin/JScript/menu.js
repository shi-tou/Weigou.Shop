//文件名：menu.js
//描述：用于点击菜单时，在主框架中显示对应内容（页面）
//时间：2013-02-25

var index = 0;
function add(url, obj) {
    setCurrent(obj);
    var title = $(obj).html();
    
    if (isTabExist(title))
        return;
    //调用EasyUItabs方法添加选项卡
    $('#nav_tabs').tabs('add', {
        title: title,
        closable: true,
        content: "<iframe scrolling=\"auto\" frameborder=\"0\" src=\"" + url + "\" style=\"width:100%; height:100%;\"></iframe>"
    });
    initTabEvent();
}
//选项卡是否存在，存在则选中并返回true,不存在则
function isTabExist(title) {
    var flag = $('#nav_tabs').tabs('exists', title);
    if (flag) {
        $('#nav_tabs').tabs('select', title);
        var currTab = $('#nav_tabs').tabs('getTab', title);
        var iframe = $(currTab.panel('options').content);
        var url = iframe.attr('src');
        if (url) {
            $('#nav_tabs').tabs('update', { tab: currTab, options: { content: "<iframe scrolling=\"auto\" frameborder=\"0\" src=\"" + url + "\" style=\"width:100%; height:100%;\"></iframe>" } });
        }
        return true;
    }
    else
        return false;
}

function setCurrent(obj) {
    $('#nav a').removeClass('current');
    $(obj).find('a').addClass('current');
}

function initTabEvent() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children(".tabs-closable").text();
        $('#nav_tabs').tabs('close', subtitle);
    })
    /*为选项卡绑定右键*/
    $(".tabs-inner").bind('contextmenu', function (e) {
        $('#mm').menu('show', {
            left: e.pageX,
            top: e.pageY
        });

        var subtitle = $(this).children(".tabs-closable").text();

        $('#mm').data("currtab", subtitle);
        $('#nav_tabs').tabs('select', subtitle);
        return false;
    });
}
//绑定右键菜单事件
function InitMenuEven() {
    $('#mm').menu({
        onClick: function (item) {
            closeTab(item.id);
        }
    });
    return false;
}
//菜单相关操作
function closeTab(action) {
    var alltabs = $('#nav_tabs').tabs('tabs');
    var currentTab = $('#nav_tabs').tabs('getSelected');
    var allTabtitle = [];
    $.each(alltabs, function (i, n) {
        allTabtitle.push($(n).panel('options').title);
    })
    switch (action) {
        case "refresh":
            var iframe = $(currentTab.panel('options').content);
            var src = iframe.attr('src');
            $('#nav_tabs').tabs('update', {
                tab: currentTab,
                options: {
                    content: createFrame(src)
                }
            })
            break;
        case "close":
            var currtab_title = currentTab.panel('options').title;
            $('#nav_tabs').tabs('close', currtab_title);
            break;
        case "closeall":
            $.each(allTabtitle, function (i, n) {

                if (n != '首页') {
                    $('#nav_tabs').tabs('close', n);
                }
            });
            break;
        case "closeother":
            var currtab_title = currentTab.panel('options').title;
            $.each(allTabtitle, function (i, n) {
                if (n != currtab_title && n != '首页') {
                    $('#nav_tabs').tabs('close', n);
                }
            });
            break;
        case "closeright":
            var tabIndex = $('#nav_tabs').tabs('getTabIndex', currentTab);

            if (tabIndex == alltabs.length - 1) {

                return false;
            }
            $.each(allTabtitle, function (i, n) {
                if (i > tabIndex) {
                    if (n != '首页') {
                        $('#nav_tabs').tabs('close', n);
                    }
                }
            });
            break;
        case "closeleft":
            var tabIndex = $('#nav_tabs').tabs('getTabIndex', currentTab);
            if (tabIndex == 1) {

                return false;
            }
            $.each(allTabtitle, function (i, n) {
                if (i < tabIndex) {
                    if (n != '首页') {
                        $('#nav_tabs').tabs('close', n);
                    }
                }
            });
            break;
        case "exit":
            $('#closeMenu').menu('hide');
            break;
    }
}