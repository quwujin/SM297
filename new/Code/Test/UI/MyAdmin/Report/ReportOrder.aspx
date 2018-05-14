<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportOrder.aspx.cs" Inherits="MyAdmin_Report_ReportOrder" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>
<!DOCTYPE html>
<html>
<head id="Head2" runat="server">
    <title>用户管理-ESmartWave</title> 
   <uc1:common runat="server" ID="common" />
  
    <script src="https://img.hcharts.cn/highcharts/highcharts.js"></script>
	<script src="https://img.hcharts.cn/highcharts/modules/exporting.js"></script>
	<script src="https://img.hcharts.cn/highcharts-plugins/highcharts-zh_CN.js"></script>
    
 
 
</head>
<body style="background: #fff;">
    <form id="form2" runat="server"> 
        <asp:HiddenField ID="HiddenFieldNum" runat="server" Value=""/>
         

        <div class="col-xs-12 widget-container-col ui-sortable">
										<div class="widget-box ui-sortable-handle">
											<!-- #section:custom/widget-box.header.options -->
											<div class="widget-header widget-header-large">
												<h4 class="widget-title">24小时订单热度统计</h4>

												<div class="widget-toolbar">
												 
													<a href="#" data-action="reload">
														<i class="ace-icon fa fa-refresh"></i>
													</a>

													<a href="#" data-action="collapse">
														<i class="ace-icon fa fa-chevron-up"></i>
													</a>

													<a href="#" data-action="close">
														<i class="ace-icon fa fa-times"></i>
													</a>
												</div>
											</div>

											<!-- /section:custom/widget-box.header.options -->
											<div class="widget-body">
												<div class="widget-main">
												 
                                                    
                                                  
                                                      <div id="container" style="width:100%;height:600px"></div>

	                                                                    <script>
                                                                            $(function () {
                                                                                $('#container').highcharts({
                                                                                    chart: {
                                                                                        type: 'column'
                                                                                    },
                                                                                    title: {
                                                                                        text: '24小时订单热度统计'
                                                                                    },
                                                                                    subtitle: {
                                                                                        text: '数据截止当前时间，实时统计'
                                                                                    },
                                                                                    xAxis: {
                                                                                        type: 'category',
                                                                                        labels: {
                                                                                            rotation: -45,
                                                                                            style: {
                                                                                                fontSize: '13px',
                                                                                                fontFamily: 'Verdana, sans-serif'
                                                                                            }
                                                                                        }
                                                                                    },
                                                                                    yAxis: {
                                                                                        min: 0,
                                                                                        title: {
                                                                                            text: '订单个数'
                                                                                        }
                                                                                    },
                                                                                    legend: {
                                                                                        enabled: false
                                                                                    },
                                                                                    tooltip: {
                                                                                        pointFormat: '当前总数: <b>{point.y:.1f} 个</b>'
                                                                                    },
                                                                                    series: [{
                                                                                        name: '总订单数',
                                                                                        data: [
                                                                                            <%=hots%>
                                                                                        ],
                                                                                        dataLabels: {
                                                                                            enabled: true,
                                                                                            rotation: -90,
                                                                                            color: '#FFFFFF',
                                                                                            align: 'right',
                                                                                            format: '{point.y:.1f}', // one decimal
                                                                                            y: 10, // 10 pixels down from the top
                                                                                            style: {
                                                                                                fontSize: '13px',
                                                                                                fontFamily: 'Verdana, sans-serif'
                                                                                            }
                                                                                        }
                                                                                    }]
                                                                                });
                                                                            });

                                                                    </script>


												</div>
											</div>
										</div>
									</div>

 
 


    </form>
</body>
</html>

