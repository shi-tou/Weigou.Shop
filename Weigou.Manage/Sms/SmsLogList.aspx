<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsLogList.aspx.cs" Inherits="Weigou.Manage.Sys.SmsLogList" %>
 
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>短信记录-<%=PageTitle %></title>
    <script src="../JScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
   
    <!--工具栏-->
    <div class="ibox">
            <div class="ibox-title">
                <h5>短信发送列表</h5>
                <div class="ibox-tools"> 
                    <a class="btn btn-success btn-sm collapse-link">
                        <i class="fa fa-chevron-up"></i>&nbsp;收/开
                    </a>
                </div>
            </div>
           <div class="ibox-content">
                <table class="form-group">
            <tr>
                <th>手机号：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtMobileNo" CssClass="form-control" ></asp:TextBox>
                </td>
                <th>内容：</th>
                <td>
                    <asp:TextBox runat="server" ID="txtContent" CssClass="form-control" ></asp:TextBox>
                </td>
                <th>发送时间：</th>
                <td>
                     <asp:TextBox runat="server" ID="txtMinTime" class="form-control" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                     
                </td>
                <td>~</td>
                <td><asp:TextBox runat="server" ID="txtMaxTime" class="form-control" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox></td>
                <td>&nbsp;&nbsp;<asp:Button runat="server" ID="btnSearch" CssClass="btn btn-primary" Text="查 询" OnClick="btnSearch_Click" /></td>
            </tr>
        </table>
    </div>
    </div>
    <!--列表-->
        <table class="table table-hover table-bordered ">
            <thead>
                <tr>
                    <th style="width: 50px">#</th>
                    <th class="hidden">ID</th>
                    <th>手机号</th>
                    <th>短信内容</th>
                    <th>发送状态</th>
                    <th>失败说明</th>
                    <th>发送时间</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repData"  OnItemCommand="repData_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Eval("MobileNo") %></td>
                            <td><%#Eval("Content") %></td>
                            <td><%#Eval("ErrMsg").ToString()==""?"发送成功":"发送失败" %></td>
                            <td><%#Eval("ErrMsg") %></td>
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
