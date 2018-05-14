<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportByPrize.aspx.cs" Inherits="MyAdmin_Report_ReportByPrize" %>

<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>
<!DOCTYPE html>
<html>
<head id="Head2" runat="server">
    <title>用户管理-ESmartWave</title> 
   <uc1:common runat="server" ID="common" />
    <style type="text/css">
        .auto-style1 {
            width: 68px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

    <div style="margin:50px auto;width:90%">
 
											<!-- /section:custom/widget-box.options -->
											<div class="widget-body">
												<div class="widget-main no-padding">
													  <div class="col-xs-12 widget-container-col ui-sortable">
										<div class="widget-box ui-sortable-handle">
											<!-- #section:custom/widget-box.header.options -->
											<div class="widget-header widget-header-large">
												<h4 class="widget-title">订单统计</h4>

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
												 
                                                    
                                                   
						                    <span class="btn btn-app btn-lager btn-pink no-hover"   > 
													<span class="line-height-1 bigger-170"><%=totalOrders %> </span>

													<br>
													<span class="line-height-1 smaller-60"> 总订单 </span>
												</span>		
            
                                             <span class="btn btn-app btn-lager btn-pink no-hover">
													<span class="line-height-1 bigger-170"> <%=preOrders %>  </span>

													<br>
													<span class="line-height-1 smaller-60"> 昨日订单量 </span>
												</span>		
            

                                                  <span class="btn btn-app btn-lager btn-pink no-hover">
													<span class="line-height-1 bigger-170"> <%=todayOrders %> </span>

													<br>
													<span class="line-height-1 smaller-60"> 今日新增 </span>
												</span>		
 
                                                     <span class="btn btn-app btn-lager btn-info no-hover"   > 
													<span class="line-height-1 bigger-170"><%=totalOrders %> </span>

													<br>
													<span class="line-height-1 smaller-60"> 待审核订单 </span>
												</span>		
            
                                             <span class="btn btn-app btn-lager btn-success no-hover">
													<span class="line-height-1 bigger-170"> <%=preOrders %>  </span>

													<br>
													<span class="line-height-1 smaller-60"> 已审核订单 </span>
												</span>		
            

                                                  <span class="btn btn-app btn-lager btn-danger no-hover">
													<span class="line-height-1 bigger-170"> <%=todayOrders %> </span>

													<br>
													<span class="line-height-1 smaller-60"> 已作废订单 </span>
												</span>		

                                             
												</div>
											</div>
										</div>
									</div>



									 
                             <div class="col-xs-12 widget-container-col ui-sortable">
										<div class="widget-box ui-sortable-handle">
											<!-- #section:custom/widget-box.header.options -->
											<div class="widget-header widget-header-large">
												<h4 class="widget-title">按奖项统计</h4>

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
												 
                                                    
                                                   
						         
                                                      <span class="btn btn-app btn-lager btn-yellow no-hover"   > 
													<span class="line-height-1 bigger-170"> <%=prize1 %> </span>

													<br>
													<span class="line-height-1 smaller-60"> 一等奖中奖 </span>
												</span>		
            
                                             <span class="btn btn-app btn-lager btn-yellow no-hover">
													<span class="line-height-1 bigger-170"> <%=prize2%> </span>

													<br>
													<span class="line-height-1 smaller-60"> 二等奖中奖 </span>
												</span>		
            

                                                  <span class="btn btn-app btn-lager btn-yellow no-hover">
													<span class="line-height-1 bigger-170"> <%=prize3 %> </span>

													<br>
													<span class="line-height-1 smaller-60"> 三等奖中奖 </span>
												</span>		

                                                    <span class="btn btn-app btn-lager btn-yellow no-hover">
													<span class="line-height-1 bigger-170"> <%=prize4 %> </span>

													<br>
													<span class="line-height-1 smaller-60"> 四等奖中奖 </span>
												</span>		

												</div>
											</div>
										</div>
									</div>
												</div>
											</div>
										</div>
	 
    </form>
</body>
</html>
