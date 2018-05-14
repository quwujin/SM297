<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Info.aspx.cs" Inherits="Info" %> 

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<meta name="apple-mobile-web-app-status-bar-style" content="black" />
<meta name="format-detection" content="telephone=no" />
<meta name="aplus-terminal" content="1" />
     <%=WebFramework.GeneralMethodBase.LoadJs() %>
    <script type="text/javascript"> 
        $(function () {
            var w = $(window).width();
            var h = $(window).height();
            $("body").css("minHeight", h + "px");
            $("body").css("width", w + "px");
            SetStyle(".info", 0, 603, 38, 30);
            SetStyle("h3", 200, 50, 228, 30);
            SetStyle(".back_btn", 140, 48, 261, 48, 23);
            $(".back_btn").click(function () {
                history.go(-1);
            });
        });
    </script>
<style type="text/css">
    body{background:url(images/bg.jpg) no-repeat;background-size:100% 100%; font-family:微软雅黑;}
    *{margin:0px; padding:0px;}
    img{width:100%;vertical-align:middle;display:block;float:left;margin-top:0;}
    .back_btn{padding:0px;font-weight:bold;background:none;display:block;border:1px solid black;float:left; border-radius:13px;}

    .info {margin:auto;margin-top:50px; width:90%;display:block;float:left; overflow:auto;overflow-x:hidden; }
    h3 {font-weight:bold; font-size:18px; text-align:center;padding-top:30px; height:50px; line-height:50px;
         margin:0;padding:0;display:block;width:100%;float:left;}

     .info p,.info span {
    color:!important;  font-size:14px; text-align:left; line-height:25px; white-space: pre-line;
    width:100%;
    }
     p,span{
         white-space: normal !important;
     }
      
 </style>
</head>
<body> 
     
    <h3>活动说明</h3>
     <div class="info">
        <p style="font-weight:normal;">
            <%=mm.Notes %>
        </p>
    </div>
   <input type="button" class="back_btn" value="返回" />
     
</body>
</html>