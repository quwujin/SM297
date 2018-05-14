<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Default_Main" %>

<%@ Register Src="../inc/common.ascx" TagName="common" TagPrefix="uc1" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title><%=TITLE %></title>
    <uc1:common ID="common1" runat="server" />
    <script src="https://img.hcharts.cn/highcharts/highcharts.js"></script>
	<script src="https://img.hcharts.cn/highcharts/modules/exporting.js"></script>
	<script src="https://img.hcharts.cn/highcharts-plugins/highcharts-zh_CN.js"></script>
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        body {
            font-size: 14px;
            font-family: 微软雅黑;
        }
        input{text-align:center;}
    </style>
     
</head>
<body style="background: #f5f5f5; margin: 00px;">
    <form id="form1" runat="server">

    <%-- http://demo.cssmoban.com/cssthemes4/cpts_696_pup/index.html  当前模板网站 --%>

     
    <div class="container-widget">

        <div class="col-md-11">
            <ul class="topstats clearfix" >
            <li class="arrow"></li>
            <li class="col-xs-6 col-lg-2">
                <span class="title"><i class="fa fa-dot-circle-o"></i> Today Total</span>
                <h3><%=todayOrders %></h3>
                <span class="diff"> <b class="color-down"><%--<i class="fa fa-caret-down"></i>--%></b>今日参与人数</span>
            </li>
            <li class="col-xs-6 col-lg-2">
                <span class="title"><i class="fa fa-calendar-o"></i> Yesterday Total</span>
                <h3><%=preOrders %></h3>
                <span class="diff"><%--<b class="color-up"><i class="fa fa-caret-up"></i> 26%</b>--%> 昨日参与人数</span>
            </li>
            <li class="col-xs-6 col-lg-2">
                <span class="title"><i class="fa fa-shopping-cart"></i>All Total</span>
                <h3 class="color-up"><%=totalOrders %></h3>
                <span class="diff"><%--<b class="color-up"><i class="fa fa-caret-up"></i> 26%</b>--%> 总计参与人数</span>
            </li>
            <li class="col-xs-6 col-lg-2">
                <span class="title"><i class="fa fa-users"></i> Wait For</span>
                <h3><%=status0 %></h3>
                <span class="diff"><%--<b class="color-down"><i class="fa fa-caret-down"></i> 26%</b>--%> 待审核订单</span>
            </li>
            <li class="col-xs-6 col-lg-2">
                <span class="title"><i class="fa fa-eye"></i> Success</span>
                <h3 class="color-up"><%=status1 %></h3>
                <span class="diff"><%--<b class="color-down"><i class="fa fa-caret-down"></i> 26%</b>--%> 已审核订单</span>
            </li>
            <li class="col-xs-6 col-lg-2">
                <span class="title"><i class="fa fa-clock-o"></i> Fail</span>
                <h3 class="color-down"><%=status2 %></h3>
                <span class="diff"><%--<b class="color-up"><i class="fa fa-caret-up"></i> 26%</b>--%> 已作废订单</span>
            </li>
            </ul> 
        </div>

        <div class="row">

            <div class="col-md-12 col-lg-2">

                <div class="panel panel-widget" style="height:456px;margin-top:20px;">
                <div class="panel-title">
                    系统操作 
                </div>
                <div class="panel-body">
                    <ul class="basic-list">
                    <li><a href="../../ClearCacheBase.aspx" class="btn btn-sm btn-danger">清除缓存</a></li>
                    <li><asp:Button ID="Button1" runat="server" Visible="false" class="btn btn-sm btn-success" Text="WebApi数据导入" onclick="Button1_Click" /></li>
                    <li><asp:Button ID="Button2" runat="server" class="btn btn-sm btn-danger" Text="清除数据" onclick="Button2_Click" /></li>
                    </ul>

                </div>
                </div>
            </div>

            <div class="col-md-12 col-lg-5">
                <div class="panel panel-widget" style="height:auto;margin-top:20px;" id="UserTable">
                    <div class="panel-title">
                        项目信息 <%--<span class="label label-danger">29</span>--%>
                        <ul class="panel-tools">
                        <%--<li><a class="icon"><i class="fa fa-refresh"></i></a></li>--%>
                        <%--<li><a class="icon closed-tool"><i class="fa fa-times"></i></a></li>--%>
                        </ul>
                    </div>
                    <div class="panel-body table-responsive">

                        <table class="table table-dic table-hover ">
                        <tbody>
                            <tr>
                            <td><i class="fa fa-folder-o"></i>页面标题</td>
                            <td> <asp:Label ID="txtTITLE" runat="server" style="color:#000000;font-size:16px;"></asp:Label></td>
                            <%--<td class="text-r">27/2/2015 12:34 AM</td>--%>
                            </tr>
                            <tr>
                            <td><i class="fa fa-file-archive-o"></i>是否测试模式</td>
                            <td><asp:Label ID="IsTest" runat="server" Text=""></asp:Label></td>
                            <%--<td class="text-r">27/2/2015 12:34 AM</td>--%>
                            </tr>
                            <tr>
                            <td><i class="fa fa-file-code-o"></i>上线时间</td>
                            <td><asp:Label ID="txtStart_Time"   style="width: 240px; color:#ff0000;font-size:16px;" runat="server" Text='<%=DateTime.Parse(Start_Time).ToString("yyyy年MM月dd日 HH时mm分ss秒")%>'></asp:Label></td>
                            <%--<td class="text-r">27/2/2015 12:34 AM</td>--%>
                            </tr>
                            <tr>
                            <td><i class="fa fa-file-pdf-o"></i>下线时间</td>
                            <td><asp:Label ID="txtEnd_Time"  runat="server" style="width: 240px;color:#ff0000;font-size:16px;"></asp:Label></td>
                            <%--<td class="text-r">27/2/2015 12:34 AM</td>--%>
                            </tr>
                            <tr>
                            <td><i class="fa fa-folder-o"></i>活动说明</td>
                            <td> 
                                <asp:Repeater runat="server" ID="infoList">
                                    <ItemTemplate>
                                        <a href="../Config/info.aspx?id=<%#Eval("id") %>" style="color:blue"><%#Eval("Title") %></a><br />
                                            <span class="line-height-1 smaller-90">字数:<%#Eval("Notes").ToString().Length%></span>
                                        <br /><br />

                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                            <%--<td class="text-r">27/2/2015 12:34 AM</td>--%>
                            </tr>
                            <tr>
                            <td><i class="fa fa-folder-o"></i>项目体检情况</td>
                            <td><asp:Label ID="PhysicalTxt" runat="server" Text=""></asp:Label></td>
                            <%--<td class="text-r">27/2/2015 12:34 AM</td>--%>
                            </tr> 
                            <tr v-if="BackDay>0">
                            <td><i class="fa fa-folder-o"></i>奖项待回库数量</td>
                            <td> 
                                <span v-if="BackCount>0" style="color:red;"> {{ BackCount }}
                                    <a href="/MyAdmin/Order_Vue/WaitBackList.aspx" style="color:blue;">查看待回库订单</a>
                                </span>
                                <span v-else >
                                    0
                                </span>
                            </td>
                            <%--<td class="text-r">27/2/2015 12:34 AM</td>--%>
                            </tr> 
                        </tbody>
                        </table>          

                    </div>
                    </div>
            </div>

        </div>

    </div>
         
             
    <script src="../Vue/vue.min.js"></script>
    <link href="../Order_Vue/Element-UI/index.css" rel="stylesheet" />
    <script src="../Order_Vue/Element-UI/index.js"></script>

    <script>
        var Physical = '<%=Physical%>'; 

        var vm = new Vue({
            el: "#UserTable",
            data: {
                BackDay: parseInt('<%=BackDay%>'),
                BackCount: parseInt('<%=BackCount%>'),
            }
        });

        if (Physical != "项目正常" && Physical.length > 0) {

            var h = vm.$createElement;

            vm.$msgbox(
                {
                    title: '系统异常',
                    message: h('p', null, [h('b', { style: 'color: red;font-size:large' }, Physical + ",请及时联系管理员处理！")]),
                    type: 'error'
                }
            );

        }

    </script>
 
    </form>


</body>
</html> 
     
