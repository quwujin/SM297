<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="MyAdmin_Report_ReportOrder" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>
<!DOCTYPE html>
<html>
<head id="Head2" runat="server">
    <title>用户管理-ESmartWave</title> 
    <uc1:common runat="server" ID="common" />
   
    <script src="Echarts-4.0.2/echarts.js"></script>
    <script src="http://echarts.baidu.com/examples/vendors/echarts/map/js/china.js?_v_=1516940408524"></script>
    <script>
        function requestFullScreen(element) {
            // 判断各种浏览器，找到正确的方法
            var requestMethod = element.requestFullScreen || //W3C
            element.webkitRequestFullScreen || //Chrome等
            element.mozRequestFullScreen || //FireFox
            element.msRequestFullScreen; //IE11
            if (requestMethod) {
                requestMethod.call(element);
            }
            else if (typeof window.ActiveXObject !== "undefined") {//for Internet Explorer
                var wscript = new ActiveXObject("WScript.Shell");
                if (wscript !== null) {
                    wscript.SendKeys("{F11}");
                }
            }
        }


    </script>
    
    <style>
        /*图表*/
        .statistics { width: 1000px;height:556px;display:none;margin:auto;margin-top:20px;}
    </style>
    
    <style>
         /*卡片*/
          .time { font-size: 13px; color: #999; }
          .bottom { margin-top: 13px;line-height: 12px;}
          .button { padding: 0;float: right;}
          .image { width: 90%; display: block;margin:auto; }
          .clearfix:before,.clearfix:after { display: table; content: "";}
          .clearfix:after { clear: both }
     </style>

</head>

<body style="background: #fff;"> 

    <div id="Card" style="width:1132px;margin:auto;" >
        <el-row>
          <el-col :span="5" v-for="(item, index) in chartsImg" :key="item.method" :offset="1">
            <el-card :body-style="{ padding: '0px' }">
              <img v-bind:src="item.url" class="image">
              <div style="padding: 14px;">
                <span>{{item.name}}</span>
                <div class="bottom clearfix">
                  <time class="time">{{ item.desc }}</time>
                  <el-button type="text" class="button" @click="See(index)">查看</el-button>
                </div>
              </div>
            </el-card>
          </el-col>
        </el-row>
    </div>

    <%--参与统计--%>
    <div id="WheaterStatistics" class="statistics" ></div>
    <%--参与趋势与中奖趋势--%>
    <div id="PartakeTrend" class="statistics"></div>
    <%--全国参与分布图--%>
    <div id="WholeCountry" class="statistics"></div>
    <%--奖项统计--%>
    <div id="AwardStatistics" class="statistics"></div>

    <script src="theme/macarons.js"></script>
    <script src="theme/vintage.js"></script>
    <script src="Echarts-4.0.2/EchartsPotting.js?t=<%=DateTime.Now.ToString("yyyyMMddHHmmssfff") %>"></script>

    <script src="../Vue/vue.min.js"></script>
    <link href="../Order_Vue/Element-UI/index.css" rel="stylesheet" />
    <script src="../Order_Vue/Element-UI/index.js"></script>
    <script>
         
        var vue = new Vue({
            el: '#Card',
            data: {
                chartsImg: [
                    { name: "参与统计", method: "WheaterStatistics", desc: "参与人数-中奖人数-发奖人数", url: "Images/WheaterStatistics.png" },
                    { name: "参与与中奖趋势", method: "PartakeTrend", desc: "参与人数-中奖人数趋势图", url: "Images/PartakeTrend.png" },
                    { name: "全国参与分布图", method: "WholeCountry", desc: "全国参与分布趋势图", url: "Images/WholeCountry.png" },
                    { name: "奖项统计", method: "AwardStatistics", desc: "奖项中奖与发奖趋势图", url: "Images/AwardStatistics.png" }
                ],
                RequestArry:[0]
            },
            methods: {
                See: function (index) {
                    $(".statistics").css("display", "none");
                    $("#" + vue.chartsImg[index].method).css("display", "block");

                    if (vue.RequestArry.indexOf(index) == -1) {
                        vue.RequestArry.push(index);
                        RequestData(vue.chartsImg[index].method);
                    }

                    //全屏
                    requestFullScreen(document.documentElement);
                }
            }
        })

        $("#WheaterStatistics").css("display", "block");
        RequestData(vue.chartsImg[0].method);

    </script>
     

</body>
</html>

