<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagePic.aspx.cs" Inherits="Weigou.Admin.Product.ManagePic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../JScript/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../JScript/fancybox/jquery.fancybox.css" rel="stylesheet" />
    <style>
        #piclist {
            list-style: none;
            margin: 20px 10px;
            padding: 0px;
        }

        #piclist li {
            float: left;
            margin-right: 20px;
            text-align: center;
        }

        #piclist li img {
            border: solid 1px #aaa;
            width: 200px;
            height: 150px;
        }

        #piclist li span.b1, span.b2 {
            cursor: pointer;
            display: block;
            border: solid 1px #6CAEF5;
            padding: 3px 15px;
            background: #DAEEF5;
            margin-top: 10px;
        }

        #piclist li .b1 {
            background: #DAEEF5;
            float: left;
            margin-left: 10px;
        }

        #piclist li .b2 {
            background: #fc661c;
            float: right;
            border-color: #d30203;
            margin-right: 10px;
        }

        #piclist li font {
            color: Red;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript" src="../JScript/uploadify/jquery.uploadify.v2.1.4.min.js"></script>
    <script type="text/javascript" src="../JScript/uploadify/swfobject.js"></script>
    <script src="../JScript/fancybox/jquery.fancybox.pack.js"></script>
    <script type="text/javascript">
        var url = '/Ajax/Goods.ashx';
        var strPic = '';
        $(function () {
            var strPic = '';
            $('#file_upload').uploadify({
                'uploader': '../JScript/uploadify/uploadify.swf',
                'script': url,
                'auto': false,
                'multi': true,
                'buttonImage': '../JScript/uploadify/browse.png',
                'fileExt': '*.jpg;*.jpeg;*.gif;*.png',
                'sizeLimit': 1024 * 1024 * 2,
                'onComplete': function (event, queueID, file, response, data) {

                    strPic += response + ";";
                    if (data.fileCount == 0) {
                        SavePicture(strPic);
                    }
                },
                'onAllComplete': function (event, data) {
                    $('#uploadify').uploadifyClearQueue();
                    strPic = "";
                }
            });
            GetPictureList();
        });
        //上传（物理储存）
        function UploadPic() {
            $('#file_upload').uploadifySettings('scriptData', { 'action': 'UploadPicture', 'Type': $("#hfType").val() });
            $('#file_upload').uploadifyUpload()
            return false;
        }
        //获取图片
        function GetPictureList() {
            $.ajax({
                url: url,
                data: 'action=GetPictureList&TargetID=' + $('#hfTargetID').val() + "&Type=" + $('#hfType').val(),
                dataType: 'json',
                type: 'POST',
                success: function (data) {
                    var html = '';
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            html += '<li><a  rel="group" data-fancybox-group="gallery" href="' + data[i].LargePicture + '" >'
                            + '<img src="' + data[i].SmallPicture + '" /></a><br>'
                            + '<span class="b1" onclick="SetPictureTop(' + data[i].ID + ')">置顶</span>'
                            + '<font>' + (data[i].IsTop == 1 ? '顶' : '') + '</font>'
                            + '<span class="b2" onclick="DeletePicture(' + data[i].ID + ')">删除</span><li>';
                        }
                    }
                    $('#piclist').html(html);
                    $("a[rel=group]").fancybox();
                }
            });
        }
        //保存图片（数据存储）
        function SavePicture(strPic) {
            $.ajax({
                url: url,
                data: { 'action': 'SavePicture', 'TargetID': $('#hfTargetID').val(), 'StrPic': strPic, 'Type': $("#hfType").val() },
                dataType: 'json',
                type: 'POST',
                success: function (data) {
                    if (data.res == 0) {
                        AlertInfo('保存成功！');
                    }
                    else {
                        AlertInfo('保存失败！');
                    }
                    GetPictureList();
                }
            });
        }
        //删除图片
        function DeletePicture(id) {
            $.messager.confirm('操作提示', '是否确认删除操作？', function (r) {
                if (r) {
                    $.ajax({
                        url: url,
                        data: { 'action': 'DeletePicture', 'ID': id },
                        dataType: 'json',
                        type: 'POST',
                        success: function (data) {
                            if (data.res == 0) {
                                AlertInfo('删除成功！');
                                GetPictureList();
                            }
                            else {
                                AlertInfo('删除失败，请与管理员联系！');
                            }
                        }
                    });
                }
            });
        }
        //置顶图片
        function SetPictureTop(id) {
            $.ajax({
                url: url,
                data: { 'action': 'SetPictureTop', 'ID': id, 'TargetID': $('#hfTargetID').val(), 'Type': $("#hfType").val() },
                dataType: 'json',
                type: 'POST',
                success: function (data) {
                    if (data.res == 0) {
                        AlertInfo('置顶成功！');
                        GetPictureList();
                    }
                    else {
                        AlertInfo('操作失败');
                    }
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table class="infotable">
            <tr>
                <%if (hfType.Value == Convert.ToString((int)Weigou.Model.Enum.EnumPicture.Car))
                  {%>
                <td style="width: 70px;" class="tr">车辆名称：</td>
                <%} %>
                <%else
                  {%>
                <td style="width: 70px;" class="tr">商品名称：</td>
                <%} %>
                <td>
                    <asp:Label runat="server" Font-Bold="true" ForeColor="Red" ID="labCodeName"></asp:Label></td>
            </tr>
        </table>
        <div style="margin: 10px;">
            <input id="file_upload" name="file_upload" type="file" multiple="true" />
            <input onclick="javascript: UploadPic()" type="button" class="btn" value="上传" />
            <input onclick="javascript: CloseWin()" type="button" class="btn" value="取消" />
            <div id="queue"></div>
        </div>
        <div id="piclist">
        </div>
        <asp:HiddenField runat="server" ID="hfTargetID" />
        <asp:HiddenField runat="server" ID="hfType" />
    </form>
</body>
</html>
