<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="MyAdmin_Default_Default" %>

<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>


<!DOCTYPE html>
<html lang="en">
	<head>
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
		<meta charset="utf-8" />
		<title><%=title %></title>

        <uc1:common runat="server" ID="common" />
        <link href="../css/StyleSheet.css" rel="stylesheet" />
        <link href="../StlyeSheet/css/font-awesome.min.css" rel="stylesheet" />
        <script src="../StlyeSheet/js/plugins.js"></script>

        <script>
            $(function () {
                $("#main").height($(window).height()-120); 
            }); 

        </script> 
	</head>

	<body style="overflow:hidden">

        <div id="top" class="clearfix">

            <!-- Start App Logo -->
            <div class="applogo">
              <span  class="logo"><%=title %>管理系统</span>
            </div> 
            <!-- End App Logo -->
              
            <!-- Start Top Right -->
            <ul class="top-right"> 

            <li class="dropdown link">
              <a href="#" data-toggle="dropdown" class="dropdown-toggle profilebox"><img src="../img/profileimg.png" /><b><%=userseesion.UserName %></b><span class="caret"></span></a>
                <ul class="dropdown-menu dropdown-menu-list dropdown-menu-right">
                <%--  <li role="presentation" class="dropdown-header">Profile</li>
                  <li><a href="#"><i class="fa falist fa-inbox"></i>Inbox<span class="badge label-danger">4</span></a></li>
                  <li><a href="#"><i class="fa falist fa-file-o"></i>Files</a></li>
                  <li><a href="#"><i class="fa falist fa-wrench"></i>Settings</a></li>--%>
                  <li class="divider"></li>
                  <li><a href="../user/updatePwd.aspx"><i class="fa falist fa-lock"></i> 修改密码</a></li>
                  <li><a href="../user/loginout.aspx"><i class="fa falist fa-power-off"></i> 退出登录</a></li>
                </ul>
            </li>

            </ul>
            <!-- End Top Right -->

          </div>

        <div class="sidebar clearfix">

            <ul class="sidebar-panel nav" id="menus" runat="server">
               
            </ul>

            <ul class="sidebar-panel nav">
              <%--<li class="sidetitle">MORE</li>
              <li><a href="grid.html"><span class="icon color15"><i class="fa fa-columns"></i></span>Grid System</a></li>
              <li><a href="maps.html"><span class="icon color7"><i class="fa fa-map-marker"></i></span>Maps</a></li>
              <li><a href="customizable.html"><span class="icon color10"><i class="fa fa-lightbulb-o"></i></span>Customizable</a></li>
              <li><a href="helper-classes.html"><span class="icon color8"><i class="fa fa-code"></i></span>Helper Classes</a></li>
              <li><a href="changelogs.html"><span class="icon color12"><i class="fa fa-file-text-o"></i></span>Changelogs</a></li>--%>
            </ul>

            <div class="sidebar-plan">
              <%--Pro Plan<a href="#" class="link">Upgrade</a>
              <div class="progress">
                  <div class="progress-bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100" style="width: 60%;">
                  </div>
                </div>
              <span class="space">42 GB / 100 GB</span>--%>
            </div>

         </div>

        <!-- 内容区 -->
					
        <div class="content" >
            <iframe frameborder="0" scrolling="yes" id="main" name="main" marginheight="0" marginwidth="0" src="main.aspx" width="100%" height="100%"  ></iframe>
        </div>
        <!-- /内容区-->

 
	</body>
</html>
