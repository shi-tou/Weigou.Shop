<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BackupList.aspx.cs" Inherits="Weigou.Manage.Sys.BackupList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>备份管理-<%=PageTitle %></title>
    <script src="js/backup.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
      <div class="ibox">
            <div class="ibox-title">
                <h5>APP版本控制列表</h5>
                <div class="ibox-tools"> 
                    <a class="btn btn-success btn-sm collapse-link">
                        <i class="fa fa-chevron-up"></i>&nbsp;收/开
                    </a>
                </div>
            </div>
           <div class="ibox-content">
                <table class="form-group">
            <tr>
                <th>手动备份：</th>
                <td>&nbsp;<asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="开始备份" OnClick="btnSearch_Click" /></td>
            </tr>
        </table>
    </div> 
    </div>
         
     <table class="table table-hover table-bordered ">
            <thead>
                <tr>
                    <th style="width: 50px">#</th>
                    <th class="hidden">ID</th>
                    <th>备份名称</th>
                    <th>创建时间</th> 
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repData"  OnItemCommand="repData_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Eval("BackName") %></td>
                            <td><%#FormatDateTime(Eval("CreateTime")) %></td>                                                     
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <webdiyer:AspNetPager ID="AspNetPager" runat="server" PageSize="10" OnPageChanging="AspNetPager_PageChanging" CssClass="pager" CurrentPageButtonClass="cpb"
            PagingButtonSpacing="0" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页">
        </webdiyer:AspNetPager>
    </form>
</body>
</html>
