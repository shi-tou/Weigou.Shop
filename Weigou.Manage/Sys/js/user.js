
/*=======================修改密码========================= */
//初始化验证表单
function InitValidate_UserPwd() {
    $("#form_UserPwd").validate({
        rules: {
            txtPassword: {
                required: true,
                minlength: 3
            },
            txtPassword0: {
                required: true,
                minlength: 3,
                equalTo: "#txtPassword"
            }
        },
        messages: {
            txtPassword: {
                required: "请输入新密码",
                minlength: $.validator.format("密码不能小于{0}个字 符")
            },
            txtPassword0: {
                required: "请输入确认密码",
                minlength: "确认密码不能小于3个字符",
                equalTo: "两次输入密码不一致不一致"
            }
        },
        submitHandler: function (form) {
            
        }
    });
}

/*=======================用户编辑========================= */
//初始化验证表单
function InitValidate_UserAdd() {
    $("#form_UserAdd").validate({
        rules: {
            txtUserName: "required",
            txtPassword: {
                required: true,
                minlength: 3
            },
            txtPassword0: {
                required: true,
                minlength: 3,
                equalTo: "#txtPassword"
            },
            txtName: {
                required: true
            }
        },
        messages: {
            txtUserName: "请输入用户名",
            txtPassword: {
                required: "请输入登录密码",
                minlength: $.validator.format("密码不能小于{0}个字 符")
            },
            txtPassword0: {
                required: "请输入确认密码",
                minlength: "确认密码不能小于3个字符",
                equalTo: "两次输入密码不一致不一致"
            },
            txtName: "请输入姓名"
        }
    });
}
/*=======================角色编辑========================= */
//初始化验证表单
function InitValidate_RoleAdd() {
    $("#form_RoleAdd").validate({
        rules: {
            txtRoleName: "required"
        },
        messages: {
            txtRoleName: "请输入角色名称",
        }
    });
}
