<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleAdd.aspx.cs" Inherits="Weigou.Manage.Sys.RoleAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色添加-<%=PageTitle %></title>
    <script src="/JScript/zTree_v3/js/jquery.ztree.all-3.5.min.js" type="text/javascript"></script>
    <link href="/JScript/zTree_v3/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <script src="js/user.js"></script>
    <script type="text/javascript">
        $(function(){
            InitValidate_RoleAdd();
        });
    </script>
</head>
<body>
    <form id="form_RoleAdd" runat="server">
    <div class="ibox">
        <div class="ibox-title">
            <h5>角色添加/编辑</h5>
        </div>
        <div class="ibox-content">
            <!--form-horizontal:水平排列的表单-->
            <table class="form form-horizontal">
                <tr>
                    <th style="width: 150px;">角色名称：</th>
                    <td>
                        <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control" Width="300px"  title="请输入角色名称"></asp:TextBox></td>
                </tr>
                <tr>
                    <th>角色描述：</th>
                    <td>
                        <asp:TextBox runat="server" ID="txtRoleInfo" CssClass="form-control" Width="300px" Height="80px" Font-Size="13px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>权限分配：</th>
                    <td>
                        <div style="width: 300px; height: 300px; overflow: scroll; border: solid 1px #d4d4d4;">
                            <ul id="TreePrivilege" class="ztree" style="width: 280px; overflow: auto;"></ul>
                        </div>
                    </td>
                </tr>
                <tr>
                    <th></th>
                    <td>
                        <asp:Button runat="server" ID="BtnSubmit" Text="保存信息" CssClass="btn btn-success" OnClick="BtnSubmit_Click" />
                        <a href="RoleList.aspx" class="btn btn-white">返回列表</a>
                    </td>
                </tr>
            </table>
        </div>

    </div>
    <asp:HiddenField runat="server" ID="hfPrivilege" />
    <script>
        $(function () {
            var json = <%=PrivilegeJson %>;
            if(json=='')
                return;
            $.fn.zTree.init($("#TreePrivilege"), setting, json);
            var treeObj = $.fn.zTree.getZTreeObj("TreePrivilege");
            treeObj.expandAll(true);
        });
        var setting = {
            check: {
                enable: true
            },
            data: {
                simpleData: {
                    enable: true
                }
            },
            callback: {
                onCheck: getAllCheckNodes
            }
        };
        //获取所有选中的值
        function getAllCheckNodes() {
            var treeObj = $.fn.zTree.getZTreeObj('TreePrivilege'),
                    nodes = treeObj.getCheckedNodes(true),
                    v = '';
            for (var i = 0; i < nodes.length; i++) {
                if (v != '')
                    v += ',';
                v += nodes[i].value;
            }
            $('#hfPrivilege').val(v);
        }
       
    </script>
    </form>
</body>
</html>
