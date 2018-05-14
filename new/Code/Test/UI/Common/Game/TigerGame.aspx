<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TigerGame.aspx.cs" Inherits="View_UploadFile_TigerGame" %>

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

            SetStyle(".main_bg", 640, 658, 0, 0, 0);
            SetStyle(".main", 640, 658, 0, 0, 0);
            SetStyle(".num_box", 290, 99, 210, 350, 0);
            SetStyle(".btn", 193, 87, 221, 28, 0);
            SetStyle(".num", 91, 97, 0, 0, 0);
            //$(".num").css("marginLeft", w / (640 / 10) + "px");
            $(".num").css("marginRight", w / (640 / 45) + "px");
            //$(".num").css("backgroundPositionY", 0);  //1:0  2:182 3:354
            $("#num1").css("backgroundPositionY", 0);
            $("#num2").css("backgroundPositionY", (w / (640 / 97)) * 1);

            var isBegin = false;
            $(".btn").click(function () {
                if (isBegin) return false;
                isBegin = true;

                $.post("../Controller/ApiController.ashx", {
                    GetResult: "UpTypes"
                }, function (data, status) {
                    $("#sbox").fadeOut(200, function () {
                        if (status == "success") {
                            var result = $.parseJSON(data);
                            if (result.status == "false") {
                                showbox(1, result.msg);
                                isBegin = false;
                            } else {
                                showGame();
                            }
                        }
                    });
                });
            })



        })

        function showGame() {

            var u = w / (640 / 97); //每个表情的高

            var result = 333;//numRand();  //111 表情2 //222 表情3 //333 表情1 //345 表情1 2 3

            var num_arr = (result + '').split('');

            $(".num").each(function (index) {
                var _num = $(this);
                setTimeout(function () {
                    _num.animate({
                        backgroundPositionY: (u * 60) - (u * num_arr[index]) + h / (640 / 20)
                    }, {
                        duration: 2000 + index * 1000,//6000   3000
                        easing: "easeInOutCirc",
                        complete: function () {

                            $("#num1").css("backgroundPositionY", jx == 1 ? 0 : w / (640 / 97));
                            $("#num2").css("backgroundPositionY", jx == 1 ? 0 : w / (640 / 97));

                            if (index == 1) {
                                window.location.href = 'submitAdds.aspx';
                            }
                        }
                    });
                }, index * 300);
            });
        }

    </script>

    <style> 
        .main_bg{background:url(images/gamebg.jpg) top center no-repeat;background-size:100% 100%;}
        .main{position:relative;}
        .num_box{height:450px;width:750px;position:absolute;z-index:8;overflow:hidden;text-align:center;}
        .num{margin:0px;padding:0px; background:url(images/num.png) top center repeat-y;float:left;background-size:100% auto;}
        .btn{background:none;cursor:pointer;clear:both;background:url(images/star-bt.png) no-repeat;background-size:100% 100%;}

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="sbox"><asp:Label ID="lbErr" runat="server" Text="" ></asp:Label></div> 

        <div class="main_bg">
                <div class="main">
                    <div class="num_box">
                      <div class="num" id="num1"></div>
                      <div class="num" id="num2"></div>
                    </div>  
                </div>
         </div>
         <div class="btn"></div>
        
    </form>
</body>
</html>
