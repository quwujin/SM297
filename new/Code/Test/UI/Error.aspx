<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
<meta name="apple-mobile-web-app-capable" content="yes"/>
<meta name="apple-mobile-web-app-status-bar-style" content="black"/>
<meta name="format-detection" content="telephone=no"/>
<meta name="aplus-terminal" content="1"/>
    <%=WebFramework.GeneralMethodBase.LoadJs() %>
      <script>
          $(function () {
              var h = $(window).height();
              var w = $(window).width();
              $("body").css("height", h + "px");
              $("body").css("width", w + "px");
              $("div").css({ "left": w / (640 / 70) + "px","width":w / (640 / 460)+"px"});
               $("div").css("fontSize", w / (640 / 24) + "px");
          })
    </script>
    <style>
        div{ position:absolute;z-index:9999; display:block; height:25px;line-height:25px; margin: auto; color:white; margin-top:200px;background:rgba(0,0,0,0.6);font-size:15px; border: 1px solid #dedede;-moz-border-radius: 5px; /* Gecko browsers */-webkit-border-radius: 5px; /* Webkit browsers */border-radius: 5px; /* W3C syntax */width: 260px;text-align: center; padding: 10px;}
    </style>
</head>
<body  style="background:url(images/bg.jpg) no-repeat;background-size:100% 100%;margin:0px; padding:0px;overflow:hidden;">
    <form id="form1" runat="server"> 
            <div id="Err" runat="server" visible="false" >
                非常抱歉，网络出错了哦！稍后再来吧！<br />  
            </div>
            <div id="End" runat="server" visible="false">
                谢谢参与，活动已经结束了哦！<br />  
            </div>
        
    </form>
</body>
</html>
