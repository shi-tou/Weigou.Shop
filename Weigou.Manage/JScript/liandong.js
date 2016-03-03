/**
 * Created by Administrator on 15-06-17.
 * 模拟淘宝SKU添加组合
 * 页面注意事项：
 *      1、 .div_contentlist   这个类变化这里的js单击事件类名也要改
 *      2、 .Father_Title      这个类作用是取到所有标题的值，赋给表格，如有改变JS也应相应改动
 *      3、 .Father_Item       这个类作用是取类型组数，有多少类型就添加相应的类名：如: Father_Item1、Father_Item2、Father_Item3 ...
 */
var json = myHideJson; // 收到后端的json
$(function () {
    init();

    //SKU信息
    
    $('body').on('change', ".div_contentlist span", function () {
        step().Creat_Table();
        LoadHTML(json);
    });

    //文本框改变事件，修改json值
    if (json != "0") {
        $('body').on("change", ".l-text", function () {

            var data_id = $(this).attr("data_id");
            var a = eval(json);
            var strTxtName = $(this).attr("name");
            var strValue = $(this).val();
            for (var i = 0; i < a.GoodsSaleProp.length; i++) {
                if (a.GoodsSaleProp[i].ID == data_id) {
                    switch (strTxtName) {
                        case "Txt_SalePrice":
                            a.GoodsSaleProp[i].SalePrice = strValue;
                            break;;
                        case "Txt_MarketPrice":
                            a.GoodsSaleProp[i].MarketPrice = strValue;
                            break;;
                        case "Txt_Quantity":
                            a.GoodsSaleProp[i].Quantity = strValue;
                            break;;
                    }
                }
            }
        });
    }
});

// 页面有数据时，让属性勾选
function init() {
    if (json != "0") {
        var a = eval(json);
        for (var i = 0; i < a.GoodsSaleProp.length; i++) {
            var temp = a.GoodsSaleProp[i].SaleProp;
            var array = temp.split(':');
            for (var j = 0; j < array.length; j++) {
                $("#GoodsAttributeHTML :input[type='checkbox']").each(function () {
                    var strAlt = $(this).parent("span").attr("alt");
                    if (strAlt == array[j]) {
                        $(this).attr("Checked", true);
                    }
                });
            }
        }
        step().Creat_Table();
        LoadHTML(json);
    }
}


//加载列表HTML，赋值价格等
function LoadHTML(strJson) {
    if (strJson != "0") {
        var a = eval(strJson);
        for (var i = 0; i < a.GoodsSaleProp.length; i++) {
            //循环所有属性值的隐藏控件
            $("input[name='hideSaleProp']").each(function (index, elemen) {
                if ($(this).val() == a.GoodsSaleProp[i].SaleProp) {
                    $(this).prev().val(a.GoodsSaleProp[i].SalePrice);//销售价格
                    $(this).prev().attr("data_id", a.GoodsSaleProp[i].ID);

                    $(this).parent().next().find("input[name='Txt_MarketPrice']").val(a.GoodsSaleProp[i].MarketPrice);//市场价格
                    $(this).parent().next().find("input[name='Txt_MarketPrice']").attr("data_id", a.GoodsSaleProp[i].ID);

                    $(this).parent().append("<input name=\"hideGoodsSalePropID\" type=\"hidden\" value=\"" + index + "_" + a.GoodsSaleProp[i].ID + "\">");

                    $(this).parent().next().next().find("input[name='Txt_Quantity']").val(a.GoodsSaleProp[i].Quantity);//市场价格
                    $(this).parent().next().next().find("input[name='Txt_Quantity']").attr("data_id", a.GoodsSaleProp[i].ID);

                }
            });
        }
    }
}




// 整合sku插件入口
function step() {
    var step = {
        //SKU信息组合
        Creat_Table: function () {
            step.hebingFunction();
            var SKUObj = $(".Father_Title");
            //var skuCount = SKUObj.length;//
            var arrayTile = new Array(); //标题组数
            var arrayInfor = new Array(); //盛放每组选中的CheckBox值的对象
            var arrayColumn = new Array(); //指定列，用来合并哪些列
            var bCheck = true; //是否全选

            var arrayValue = new Array();//盛放每组选中的CheckBox值的Value值
            var columnIndex = 0;
            var tempTitle = "";
            $.each(SKUObj, function (i, item) {
                arrayColumn.push(columnIndex);
                columnIndex++;
                tempTitle = ($(this).html().replace("：", ""));
                var itemName = "cblAttr_" + $(this).attr("alt");
                //选中的CHeckBox取值
                var order = new Array();
                var orderValue = new Array();
                $("#" + itemName + " :input[type='checkbox']:checked").each(function () {

                    order.push($(this).parent("span").attr("altText"));
                    orderValue.push($(this).parent("span").attr("alt"));
                });
                if (order.length > 0) {
                    arrayInfor.push(order);
                    arrayTile.push(tempTitle);
                    arrayValue.push(orderValue);
                }
            });
            if (arrayTile.length == 0) {
                bCheck = false;
            }
            //开始创建Table表
            if (bCheck == true) {
                var RowsCount = 0;
                $("#createTable").html("");
                var table = $("<table id=\"process\" border=\"1\" cellpadding=\"1\" cellspacing=\"0\" style=\"width:100%;padding:5px;\"></table>");
                table.appendTo($("#createTable"));
                var thead = $("<thead></thead>");
                thead.appendTo(table);
                var trHead = $("<tr></tr>");
                trHead.appendTo(thead);
                //创建表头
                $.each(arrayTile, function (index, item) {
                    var td = $("<th>" + item + "</th>");
                    td.appendTo(trHead);
                });
                var itemColumHead = $("<th  style=\"width:70px;\">销售价格</th><th  style=\"width:70px;\">市场价格</th><th style=\"width:70px;\">库存</th> ");
                itemColumHead.appendTo(trHead);
                //var itemColumHead2 = $("<td >商家编码</td><td >商品条形码</td>");
                //itemColumHead2.appendTo(trHead);
                var tbody = $("<tbody></tbody>");
                tbody.appendTo(table);
                ////生成组合
                var zuheDate = step.doExchange(arrayInfor);
                var zuheDateValue = step.doExchange(arrayValue);
                if (zuheDate.length > 0) {
                    //创建行
                    $.each(zuheDate, function (index, item) {
                        
                        var td_array = item.split(",");
                        var td_arrayValue = zuheDateValue[index];

                        var skuKeyAttrs = td_arrayValue.split(","); //SKU信息key属性值数组
                        skuKeyAttrs.sort(function (value1, value2) {
                            return parseInt(value1) - parseInt(value2);
                        });
                        skuKeyAttrs = skuKeyAttrs.join(":");

                        var tr = $("<tr></tr>");
                        tr.appendTo(tbody);
                        $.each(td_array, function (i, values) {
                            var td = $("<td>" + values + "</td>");

                            td.appendTo(tr);
                        });
                        var td1 = $("<td ><input name=\"Txt_SalePrice\"  data-options=\"required:true\" min=\"0\" precision=\"2\" missingmessage=\"请输入销售价格\" class=\"easyui-numberbox  l-text txt\" type=\"text\" value=\"" + $("#txtSalePrice").val() + "\"><input name=\"hideSaleProp\" type=\"hidden\" value=\"" + skuKeyAttrs + "\"></td>");
                        td1.appendTo(tr);
                        var td2 = $("<td ><input name=\"Txt_MarketPrice\" data-options=\"required:true\" min=\"0\" precision=\"2\" missingmessage=\"请输入市场价格\" class=\"easyui-numberbox  l-text txt\" type=\"text\" value=\"" + $("#txtMarketPrice").val() + "\"></td>");
                        td2.appendTo(tr);
                        var td3 = $("<td ><input name=\"Txt_Quantity\" data-options=\"required:true\" min=\"0\"  missingmessage=\"请输入商品库存数量\" class=\"easyui-numberbox  l-text txt\" type=\"text\" value=\"0\"></td>");
                        td3.appendTo(tr);
                    });
                }
                //结束创建Table表
                arrayColumn.pop(); //删除数组中最后一项
                //合并单元格
                $(table).mergeCell({
                    // 目前只有cols这么一个配置项, 用数组表示列的索引,从0开始
                    cols: arrayColumn
                });
            } else {
                //未全选中,清除表格
                document.getElementById('createTable').innerHTML = "";
            }
        }, //合并行
        hebingFunction: function () {
            $.fn.mergeCell = function (options) {
                return this.each(function () {
                    var cols = options.cols;
                    for (var i = cols.length - 1; cols[i] != undefined; i--) {
                        // fixbug console调试
                        // console.debug(cols[i]);
                        mergeCell($(this), cols[i]);
                    }
                    dispose($(this));
                });
            };

            function mergeCell($table, colIndex) {
                $table.data('col-content', ''); // 存放单元格内容
                $table.data('col-rowspan', 1); // 存放计算的rowspan值 默认为1
                $table.data('col-td', $()); // 存放发现的第一个与前一行比较结果不同td(jQuery封装过的), 默认一个"空"的jquery对象
                $table.data('trNum', $('tbody tr', $table).length); // 要处理表格的总行数, 用于最后一行做特殊处理时进行判断之用
                // 进行"扫面"处理 关键是定位col-td, 和其对应的rowspan
                $('tbody tr', $table).each(function (index) {
                    // td:eq中的colIndex即列索引
                    var $td = $('td:eq(' + colIndex + ')', this);
                    // 取出单元格的当前内容
                    var currentContent = $td.html();
                    // 第一次时走此分支
                    if ($table.data('col-content') == '') {
                        $table.data('col-content', currentContent);
                        $table.data('col-td', $td);
                    } else {
                        // 上一行与当前行内容相同
                        if ($table.data('col-content') == currentContent && currentContent.indexOf("<input") == -1) {
                            // 上一行与当前行内容相同则col-rowspan累加, 保存新值
                            var rowspan = $table.data('col-rowspan') + 1;
                            $table.data('col-rowspan', rowspan);
                            // 值得注意的是 如果用了$td.remove()就会对其他列的处理造成影响
                            $td.hide();
                            // 最后一行的情况比较特殊一点
                            // 比如最后2行 td中的内容是一样的, 那么到最后一行就应该把此时的col-td里保存的td设置rowspan
                            if (++index == $table.data('trNum'))
                                $table.data('col-td').attr('rowspan', $table.data('col-rowspan'));
                        } else { // 上一行与当前行内容不同
                            // col-rowspan默认为1, 如果统计出的col-rowspan没有变化, 不处理
                            if ($table.data('col-rowspan') != 1) {
                                $table.data('col-td').attr('rowspan', $table.data('col-rowspan'));
                            }
                            // 保存第一次出现不同内容的td, 和其内容, 重置col-rowspan
                            $table.data('col-td', $td);
                            $table.data('col-content', $td.html());
                            $table.data('col-rowspan', 1);
                        }
                    }
                });
            }
            // 同样是个private函数 清理内存之用
            function dispose($table) {
                $table.removeData();
            }
        },
        //组合数组
        doExchange: function (doubleArrays) {
            var len = doubleArrays.length;
            if (len >= 2) {
                var arr1 = doubleArrays[0];
                var arr2 = doubleArrays[1];
                var len1 = doubleArrays[0].length;
                var len2 = doubleArrays[1].length;
                var newlen = len1 * len2;
                var temp = new Array(newlen);
                var index = 0;
                for (var i = 0; i < len1; i++) {
                    for (var j = 0; j < len2; j++) {
                        temp[index] = arr1[i] + "," + arr2[j];
                        index++;
                    }
                }
                var newArray = new Array(len - 1);
                newArray[0] = temp;
                if (len > 2) {
                    var _count = 1;
                    for (var i = 2; i < len; i++) {
                        newArray[_count] = doubleArrays[i];
                        _count++;
                    }
                }
                return step.doExchange(newArray);
            } else {
                return doubleArrays[0];
            }
        },
    }
    return step;
}