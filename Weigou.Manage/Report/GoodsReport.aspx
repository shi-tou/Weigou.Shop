<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsReport.aspx.cs" Inherits="Weigou.Manage.Report.GoodsReport" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商品销售报表-<%=PageTitle %></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="ibox">
            <div class="ibox-title">
                <h5>商品销售报表</h5>
                <div class="ibox-tools">
                </div>
            </div>
            <div class="ibox-content">
                <table class="form-group">
                    <tr>
                        <th>商品名称：</th>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" class="txt"></asp:TextBox>
                        </td>
                        <th>商品类别：</th>
                    <td>
                         <asp:DropDownList runat="server" ID="ddlType" class="form-control"> 
                        </asp:DropDownList>
                    </td>
                        <td>
                            <input type="button" id="btnSearch" onclick="GetGoodsReport()" class="btn" value="查询" /></td>
                    </tr>
                </table>
            </div>
        </div>
        <table class="table table-hover table-bordered ">
            <thead>
                <tr>
                    <th style="width: 50px">#</th>
                    <th class="hidden">ID</th>
                    <th>商品名</th>
                    <th>商品类别</th>
                    <th>销售价格</th>
                    <th>市场价格</th>
                    <th>商品库存</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="repGoods" OnItemCommand="repData_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Container.ItemIndex+1 %></td>
                            <td class="hidden"><%#Eval("ID") %></td>
                            <td><%#Eval("Name") %></td>
                            <td><%#Eval("TypeName") %></td>
                            <td style="color:red;"><%#Eval("SalePrice") %></td>
                            <td style="color:red;"><%#Eval("MarketPrice") %></td>
                            <td style="color:red;"><%#Eval("Stock")%></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <webdiyer:aspnetpager id="AspNetPager" runat="server" pagesize="10" onpagechanging="AspNetPager_PageChanging" cssclass="pager" currentpagebuttonclass="cpb"
            pagingbuttonspacing="0" firstpagetext="首页" lastpagetext="尾页" nextpagetext="下一页" prevpagetext="上一页">
        </webdiyer:aspnetpager>
    </form>
</body>
</html>
