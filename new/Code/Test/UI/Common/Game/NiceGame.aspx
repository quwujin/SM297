<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NiceGame.aspx.cs" Inherits="View_UploadFile_NiceGame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
<meta name="apple-mobile-web-app-capable" content="yes"/>
<meta name="apple-mobile-web-app-status-bar-style" content="black"/>
<meta name="format-detection" content="telephone=no"/>
<meta name="aplus-terminal" content="1"/>
    <title></title>
     <script src="http://apps.bdimg.com/libs/jquery/1.9.1/jquery.min.js"></script> 
     <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js" type="text/javascript"></script>
     <script src="http://wxshareapi2.esmartwave.com/wxshare.aspx?<%=DateTime.Now.ToString("yyyyMMddHHmmssfff")  %>"></script>
     <script src="../js/weixin.js"></script> 
	 <link href="../css/css.css" rel="stylesheet" />

    <script>

        $(function () {
             
            var w = $(window).width();
            var h = $(window).height();

            $("#nice_bg,.info-bg").css("height", h + "px");
            SetStyle("#nice,#nice1", 960, 635, 0, 0, 0);
            SetStyle("#content", 630, 510, 175, 50, 0);
            SetStyle("#content li", 204, 164, 3, 2, 0);
            SetStyle("#cnt4,#cnt6", 204, 164, 3, 7, 0);
            SetStyle("#cnt7,#cnt8,#cnt9", 204, 164, 3, 7, 0);
            SetStyle("#content img", 170, 137, 0, 10, 0);

            SetStyle(".nice-bg", 960, 635, 0, 0, 0);

            $("#start_btn").click(function () {
                $("#start_btn").attr("disabled", "true");

                $.post("../Controller/ApiController.ashx", {
                    GetResult: "UpTypes"
                }, function (data, status) {
                    $("#sbox").fadeOut(200, function () {
                        if (status == "success") {
                            var result = $.parseJSON(data);
                            if (result.status == "false") {
                                showbox(1, result.msg);
                                $("#start_btn").removeAttr("disabled");
                            } else {
                                jx = result.note;
                            }
                        }
                    });
                });
            })

        })

        var jx = 0;

       (function ($) {
             var list = [$('content').children[0], $('content').children[1], $('content').children[2], $('content').children[5], $('content').children[8], $('content').children[7], $('content').children[6], $('content').children[3]];

             var len = list.length, begin = $('cnt5'), index = 0, interval = null;

             begin.onclick = function () {
                 begin.onclick = function () { }
                 if (running) return;
                 var running = true;
                 var remain = 2000 + Math.random() * 1000;
                 var j = 0;

                 if (jx == "1") {
                     j = 1;
                 } else if (jx == "2") {
                     j = 3;
                 } else if (jx == "3") {
                     j = 7;
                 } else if (jx == "4") {
                     j = 6;
                 } else if (jx == "5") {
                     j = 4;
                 }
                 else {
                     j = 0;
                 }
 
                 interval = setInterval(function () {
                     if (remain < 200 && (index) == j) {

                         running = false;
                         clearInterval(interval);

                         window.location.href = "SubmitAdds.aspx";

                     } else {
                         list[index].className = "";

                         list[(index + 1) % len].className = "current";

                         index = ++index % len;

                         remain -= 100;
                     }
                 }, 150);
             }
         })(function (id) { return document.getElementById(id) });

    </script>

    <style>
        
        #BoxGame{display:none;float:left;position:fixed;}
        #nice {width: 100%; text-align: center;margin: auto;height:auto;background:url(../images/n_bg.png) no-repeat;background-size:100% 100%;float:left;}
        #content{width: 80%; margin: auto;list-style:none;padding:0px;background:none; position:absolute;z-index:3; }
        #content li{background:none; -moz-border-radius: 10px; -webkit-border-radius: 10px; border-radius:10px;}
        #content li#cnt1,#content li#cnt3,#content li#cnt7,#content li#cnt9{ background:none;}
        #content li#cnt5{background:none;background-size:100% 100%;}
        #content li,#begin{ width: 90px;height: 90px; text-align: center;float:left;padding:0px;margin:0px; vertical-align:middle;text-align:center;}
        #content li.current{background: #ffe400 !important;}
        #content li input{width:62px;height:60px;}
        #content li img{vertical-align:middle;}
        .s{background:url(../images/s55.png) no-repeat;background-size:100% 100%;border:none;margin-top:22px;}
        #begin{left: 100px;top: 100px;} 

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="sbox"><asp:Label ID="lbErr" runat="server" Text="" ></asp:Label></div> 

        <div id="nice" > 
             <ul id="content">
		              <li id="cnt1"><img src="images/s6.png"/></li>
		              <li id="cnt2" ><img src="images/s1.png"/></li>
                      <li id="cnt3"><img src="images/s6.png"/></li>
                      <li id="cnt4"><img src="images/s3.png"/></li>
                      <li id="cnt5" class="ntus"></li>
                      <li id="cnt6"><img src="images/s2.png"/></li>
		              <li id="cnt7"><img src="images/s4.png"/></li>
                      <li id="cnt8"><img src="images/s6.png"/></li>
                      <li id="cnt9"><img src="images/s5.png"/></li>
	        </ul>
        </div>

               
    </form>
</body>
</html>
