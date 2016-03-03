<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleAdd.aspx.cs" Inherits="Weigou.Admin.Sys.RoleAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色添加-<%=PageTitle %></title>
    <script src="../JScript/zTree_v3/js/jquery.ztree.all-3.5.min.js" type="text/javascript"></script>
    <link href="../JScript/zTree_v3/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="infotable" >
            <tr>
                <td style="width:100px;" class="tr">角色名称：</td>
                <td><asp:TextBox ID="txtRoleName" runat="server" CssClass="easyui-validatebox txt" Width="200px" data-options="required:true" missingmessage="请输入角色名称"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="tr">角色描述：</td>
                <td>
                    <asp:TextBox runat="server" ID="txtRoleInfo" CssClass="txt" Width="200px" Height="50px" Font-Size="13px"  TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tr">权限分配：</td>
                <td>
                    <div style="width:300px; height:200px; overflow:scroll;border:solid 1px #d4d4d4;">
                        <ul id="TreePrivilege" class="ztree" style="width:260px; overflow:auto;"></ul>
                    </div>
                </td>
            </tr>
        </table>
        <div class="action">
            <asp:Button runat="server" ID="BtnSubmit" Text="确认" CssClass="btn" OnClientClick="return $('#form1').form('validate')" OnClick="BtnSubmit_Click" />
            <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn" OnClientClick="CloseWin()" />
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
