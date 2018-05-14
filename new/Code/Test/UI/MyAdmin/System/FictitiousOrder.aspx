<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FictitiousOrder.aspx.cs" Inherits="MyAdmin_System_FictitiousOrder" %>
<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>虚拟订单管理</title>
    <uc1:common runat="server" ID="common" />
</head>
<body style="background:#fff;">
    <form id="form1" runat="server">

    <table id="checkSelect" width="100%" class="table table-bordered" >
        <tr class="info"> 
                       
                <td class="col-lg-2 col-md-2 col-sm-2 col-xs-2"> 状态： 
                    <asp:DropDownList ID="ddlState" runat="server" style="width:100px;" >
                        <asp:ListItem Value="不限">不限</asp:ListItem>
                        <asp:ListItem Value="0">未审核</asp:ListItem>
                        <asp:ListItem Value="1">已审核</asp:ListItem>
                        <asp:ListItem Value="-1">已作废</asp:ListItem>
                    </asp:DropDownList>
                </td>
                                                                                             
                <td class="col-lg-2 col-md-2 col-sm-2 col-xs-2"> 奖项： 
                    <asp:DropDownList ID="ddlJx" runat="server" style="width:100px;" >
                        <asp:ListItem Value="不限">不限</asp:ListItem> 
                        <asp:ListItem Value="一等奖">一等奖</asp:ListItem> 
                        <asp:ListItem Value="二等奖">二等奖</asp:ListItem> 
                        <asp:ListItem Value="参与奖">参与奖</asp:ListItem> 
                    </asp:DropDownList>
                </td> 
                       
                <td class="col-lg-5 col-md-4 col-sm-12 col-xs-2"> 订单时间： 
                    <asp:TextBox ID="tbSt1" runat="server"  Style="width:150px;"    placeholder="开始时间" onclick="layui.laydate({elem: this, istime: false, format: 'YYYY-MM-DD'})"></asp:TextBox>
                -
                <asp:TextBox   ID="tbSt2" runat="server"  Style="width:150px;" placeholder="结束时间" onclick="layui.laydate({elem: this, istime: false, format: 'YYYY-MM-DD'})"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="搜 索" CssClass="btn btn-sm btn-primary"  OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" Text="导 出" CssClass="btn btn-sm btn-success" OnClick="Button2_Click" />
                </td> 
            </tr>
    </table>
         
     <div class="table-header"> 虚拟订单 </div> 
     <table  border="0" align="center"  class="table table-bordered"  >
        <tr>
            <td>订单号</td>
            <td>手机号</td>
            <td>奖项</td>
            <td>奖品</td>
            <td>时间</td>
        </tr>
         
        <asp:Repeater runat="server" ID="menuList">
            <ItemTemplate>
                <tr >
                    <td>
                        <%# Eval("OrderCode").ToString() %>
                    </td>
                    <td>
                        <%# Eval("Mob").ToString() %>
                    </td>
                    <td>
                        <%# Eval("Jx").ToString() %>
                    </td>
                    <td>
                        <%# Eval("Jp").ToString() %>
                    </td>
                    <td>
                        <%# Eval("CreateTime").ToString() %>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>

     </table>

    <table width="100%" style="margin-top: 50px;">
        <tr> 
                <td class="page"> 
                    <webdiyer:AspNetPager ID="AspNetPager1" CssClass="page" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                        CurrentPageButtonPosition="Center" PageSize="30"
                        ShowCustomInfoSection="Right" PageIndexBoxType="TextBox" PageIndexBoxStyle="width:50px;"
                            ShowPageIndexBox="Auto"  ShowBoxThreshold="10" 
                        CustomInfoHTML="<span>当前第 %CurrentPageIndex% 页, 共 %PageCount%页 共%RecordCount% 条记录</span>" FirstPageText="首页"
                        LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页">
                    </webdiyer:AspNetPager>
                    </td>
        </tr>
    </table>

    </form>
</body>
</html>
