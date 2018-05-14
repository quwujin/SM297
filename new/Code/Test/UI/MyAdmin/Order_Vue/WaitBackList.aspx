<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WaitBackList.aspx.cs" Inherits="MyAdmin_Order_Vue_WaitBackList" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>

<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>  
<!DOCTYPE html>
<html>
<head id="Head2" runat="server">
    <title>待回库订单管理</title> 
    <uc1:common runat="server" ID="common" />   

</head>
<body>
    <form id="form1" runat="server">

        <div class="table-header"> 待回库订单管理 </div>

        <table width="100%" class="table table-bordered" >
            <tr>
                <td style="width:150px;">
                    <asp:TextBox ID="tbSt1" runat="server"   CssClass="form-control" style="width:150px;"  placeholder="开始时间" onclick="layui.laydate({elem: this, istime: false, format: 'YYYY-MM-DD'})"></asp:TextBox>
                </td>
                <td style="width:50px;">至 </td> 
                <td style="width:150px;">
                    <asp:TextBox ID="tbSt2" CssClass="form-control"   placeholder="结束时间" style="width:150px;"  onclick="layui.laydate({elem: this, istime: false, format: 'YYYY-MM-DD'})"  runat="server" ></asp:TextBox>
                </td> 
                <td><asp:Button ID="Button2" runat="server" Text="搜 索" CssClass="btn btn-success"  OnClick="Button2_Click" />
                <td>
                    <asp:Button ID="Button1" runat="server" Text="全部回库" onclick="Button1_Click"   CssClass="btn btn-primary"/>
                </td>
            </tr>
        </table>
        <table id="sample-table-2" class="table table-striped table-bordered table-hover">
             <thead>
                <tr>
                    <th>订单号</th>
                    <th>手机号</th>
                    <th>奖项</th>
                    <th>奖品</th>
                    <th>创建时间</th>
                    <th>状态</th>
                    <th>是否回库</th>
                </tr>
            </thead>
            <tbody >
                <asp:Repeater runat="server" ID="menuList">
                     <ItemTemplate>
                        <tr>
                            <td><%#Eval("OrderCode") %></td>
                            <td><%#SafeVal(Eval("Mob").ToString()) %></td>
                            <td><%#Eval("Jx") %></td>
                            <td><%#Eval("Jp") %></td>
                            <td><%#Eval("CreateTime") %></td>
                            <td><%#Eval("States").ToString()=="0"?"待审核":Eval("States").ToString()=="1"?"已审核":"已作废" %></td>
                            <td><%#Eval("StatusId").ToString()=="0"?"未回库":Eval("States").ToString()=="1"?"已回库":"状态异常" %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

            </tbody>
        </table>
        <table width="100%" style="margin-top: 50px;">
                <tr>
                    <td class="page">
                        <webdiyer:AspNetPager ID="AspNetPager1" CssClass="page" runat="server"  OnPageChanged="AspNetPager1_PageChanged" 
                                CurrentPageButtonPosition="Center"  PageSize="30"  
                                ShowCustomInfoSection="Right" PageIndexBoxType="TextBox" 
                                ShowPageIndexBox="Never" 
                            CustomInfoHTML="<span>当前第 %CurrentPageIndex% 页, 共 %PageCount%页 共%RecordCount% 条记录</span>" FirstPageText="首页" 
                            LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页">
                        </webdiyer:AspNetPager>
                    </td>
                </tr>
        </table>
        <script>
            layui.use('laydate', function () {
                var laydate = layui.laydate;
            });
        </script>

    </form>
</body>
</html>
