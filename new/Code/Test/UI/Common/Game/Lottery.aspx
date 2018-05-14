<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lottery.aspx.cs" Inherits="View_UploadFile_Lottery" %>

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
     <script src="../js/jQueryRotate.2.2.js"></script>
     <script src="../js/jquery.ui.draggable.js"></script>
     <script src="../js/weixin.js"></script> 
	 <link href="../css/css.css" rel="stylesheet" />
     <script src="../js/jquery.easing.min.js"></script> 

    <script>

        $(function () {
             
            var w = $(window).width();
            var h = $(window).height();

            SetStyle(".mob-bg", 471, 337, 83, 0, 30);

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
                                showTurntable(".Lottery-bg", "ShowPrize.aspx", angle);
                            }
                        }
                    });
                });
            })

        })

        function showTurntable(obj, url, angle) {

            $(obj).rotate({
                duration: 3000,//转动时间间隔（转动速度）
                angle: 0,  //开始角度 
                animateTo: 3600 + parseInt(angle), //转动角度，10圈+
                easing: $.easing.easeOutSine, //动画扩展 
                callback: function () { //回调函数
                    window.location.href = url;
                }
            });
        }

    </script>

    <style> 
         

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="sbox"><asp:Label ID="lbErr" runat="server" Text="" ></asp:Label></div> 

        <img src="images/logo.png"/>
       

    </form>
</body>
</html>
