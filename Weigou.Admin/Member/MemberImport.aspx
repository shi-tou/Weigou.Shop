<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberImport.aspx.cs" Inherits="Weigou.Admin.Member.MemberImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function CreateExcelTempForMem() {
            OpenWin('555', 300, 333, '/Ajax/Excel.ashx?action=CreateExcelTempForMem');
            $('#win').window({
                closed: true
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="easyui-panel" title="批量导入会员">
        <div class="tip1">
            <p><b>注意事项:</b></p>
            <p style=" text-indent:2em;">1、导入数据时请下载并使用系统提供的《会员导入样本.xls》文件， 并严格按照文件说明录入需要导入的数据。</p>
            <p style=" text-indent:2em;">2、填写的数据中不要出现回车，换行等字符，以免对以后的操作造成影响。</p>
            <p style=" text-indent:2em;">2、导入的数据量不要过大，如需要导入大数据建议分批次导入，以减少错误。</p>
            <p><b>导入步骤：</b></p>
            <p style=" text-indent:2em;">1、点击<input value="下载" type="button" onclick="CreateExcelTempForMem()" />《会员导入样本.xls》文件，在Excel表格中录入数据。</p>
            <p style=" text-indent:2em;">2、选择录入完毕的Excel文件，执行导入操作<asp:FileUpload runat="server" ID="fileUpload" />
                <asp:Button runat="server" ID="btnImport" Text="导入" onclick="btnImport_Click" /></p>
        </div>
        <!--弹出窗-->
        <div id="win" class="easyui-window" data-options="iconCls:'icon-save',top:'50px',closed:true,minimizable:false,maximizable:false,collapsible:false">
            <iframe id="iframe" name="iframe" frameborder="0" style="width: 100%; height: 100%;"></iframe>
        </div>
    </div>
    </form>
</body>
</html>
