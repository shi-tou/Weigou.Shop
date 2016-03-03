<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogList.aspx.cs" Inherits="Weigou.Manage.Sys.LogList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>操作日志</title>
</head>
<body>
    <form id="form1" runat="server">

        <div class="ibox">
            <div class="ibox-title">
                <h5><i class="fa fa-list"></i>&nbsp;操作日志</h5>
                <div class="ibox-tools">
                    <a class="btn btn-success btn-sm collapse-link">
                        <i class="fa fa-chevron-up"></i>&nbsp;收/开
                    </a>
                </div>
            </div>
            <div class="ibox-content">
                <table class="form-group">
                    <tr>
                        <th>模块：</th>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlModule" class="form-control">
                            </asp:DropDownList>
                        </td>
                        <th>类型：</th>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlOperation" class="form-control">
                            </asp:DropDownList>
                        </td>
                        <th>内容：</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtContent" class="form-control"></asp:TextBox>
                        </td>
                        <th>时间：</th>
                        <td>
                            <asp:TextBox runat="server" ID="txtMinTime" class="form-control" Width="100px" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" ></asp:TextBox>
                        </td>
                        <td>~</td>
                        <td><asp:TextBox runat="server" ID="txtMaxTime" class="form-control" Width="100px" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox></td>
                        <td>
                            &nbsp;<asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="查 询" OnClick="btnSearch_Click" /></td>
                    </tr>
                </table>

            </div>
        </div>
        <table class="table table-hover table-bordered ">
            <thead>
                <tr>
                    <th style="width: 38px; text-align:center;">#</th>
                    <th class="hidden">ID</th>
                    <th>模块</th>
                    <th>操作</th>
                    <th>内容</th>
                    <th>IP</th>
                    <th>操作人</th>
                    <th>操作时间</th>
                    
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repLog">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center;"><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Weigou.Common.EnumHelper.GetModule(Eval("Module")) %></td>
                            <td><%#Weigou.Common.EnumHelper.GetOperation(Eval("Operation")) %></td>
                            <td><%#Eval("Content") %></td>
                            <td><%#Eval("IP") %></td>
                            <td><%#Eval("CreateUser") %></td>
                            <td><%#Eval("CreateTime") %></td>
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
