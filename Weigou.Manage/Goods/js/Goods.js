/*=======================编辑========================= */
//初始化验证表单
function InitValidate_GoodsAdd() {
    $("#form1").validate({
        rules: {
            txtCode: {
                required: true,
                minlength: 10
            },
            ddlType: "required",
            txtName: "required",
            txtDescription: "required",
            txtSalePrice: {
                required: true,
                min:0.01
            },
            txtMarketPrice: {
                required: true,
                min:0.01
            },
            txtStock: {
                required: true,
                min: 0
            }
        },
        messages: {
            txtCode: "请输入商品编码！",
            ddlType: "请选择商品类别",
            txtName: "请输入商品名称",
            txtDescription: "请输入商品描述",
            txtSalePrice: "请输入销售价格,不能小于0.01",
            txtMarketPrice: "请输入市场价格，不能小于0.01",
            txtStock: "请输入库存量"
        }
    });
}
 