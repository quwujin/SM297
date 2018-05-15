<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lottery.aspx.cs" Inherits="lottery" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport"
        content="width=device-width, initial-scale=1.0,minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="aplus-terminal" content="1" />
    <title>首页</title>
    <%=WebFramework.GeneralMethodBase.LoadJs() %>
    <script type="text/javascript" src="js/rem.min.js"></script>
    <link rel="stylesheet" href="css/global.min.css">
    <link rel="stylesheet" href="css/index.min.css">
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="js/index.min.js"></script>
    <script src="js/city.min.js"></script>
    <script src="js/jQueryRotate.2.2.min.js"></script>
</head>
<body class="">
    <form runat="server">
        <!--提示框-->
        <div id="sbox">
            <asp:Label ID="lbErr" runat="server"></asp:Label>
        </div>
        <div class="container ">
            <div class="divlogotop">
                <img src="images/logotop.png" alt="" /></div>
            <div class="boxcotainer m0a ovh">
                <div class="boxlottery m0a">
                    <img src="images/lotteryitem.png" alt="" class="lotteryitem m0a" />
                    <img src="images/btn_start_east.png" alt="" class="btn_start_east" />
                </div>
            </div>
            <div class="divlogobot">
                <img src="images/logobot.png" alt="" /></div>
            <div class="divtoactinfo">
                <img src="images/toactinfo.png" alt="" class="imgtoactinfo" /></div>
            <%--活动说明--%>
            <div class="pop hide">
                <div class="boxpopthing activeinfo hide">
                    <div class="divpopthing">
                        <div class="divpoptitle m0a tac">
                            <p class="ppoptitle">活动说明</p>
                        </div>
                        <div class="divpopcontent m0a">
                            <div class="activeinfocon m0a">
                                <%=mm.Notes %>
                            </div>
                        </div>
                    </div>
                    <div class="divinputbtn">
                        <input type="button" value="" class="btnback btn" /></div>
                </div>
            </div>
        </div>
        <input type="file" name="" id="" class="hide fileup" style="display: none;" accept="image/*" />
    </form>
</body>
<script>
    $(function () {
        var st = parseInt(Math.random() * 2);
        console.log(st);
        var str = "";
        //抽奖按钮
        $('.btn_start_east').click(function () {
            //按钮锁定
            $('.btn_start_east').attr({ "disabled": "disabled" });
            $.post("./Controller/ApiController.ashx",
                        {
                            GetResult: "getjx"
                        },
                        function (data, status) {
                            var result = JSON.parse(data);
                            if (result.Success == false) {
                                showbox(1, result.ErrMessage);
                            } else {
                                console.log(result.Notes);
                                if (result.Notes.indexOf("一等奖") > -1) {
                                   str =  { prize: 1, angle: 100 + parseInt(40 * Math.random()) };
                                }
                                if (result.Notes.indexOf("二等奖") > -1) {
                                   str = { prize: 2, angle: 160 + parseInt(40 * Math.random()) };
                                }
                                if (result.Notes.indexOf("参与奖") > -1) {
                                    str = { prize: 0, angle: 40 + parseInt(40 * Math.random()) };
                                }
                               
                                $(".lotteryitem").rotate({
                                    duration: 3000, //转动时间间隔（转动速度）
                                    angle: 0, //开始角度
                                    animateTo: 3600 + parseInt(str.angle), //转动角度，10圈+
                                    easing: $.easing.easeOutSine, //动画扩展
                                    callback: function () {
                                        $(".btn_start_east").removeAttr("disabled");
                                        //console.log(index);
                                        window.location.href = "prize.aspx";
                                        //setTimeout(function () {
                                        //    $('.draw_box').hide(); //抽奖页面隐藏
                                        //    if (index === 2) {
                                        //        $('.prize_no').show(); //未中奖
                                        //        GameContent.friend = "看剧享果粒，让乐趣上演。可口可乐送电影票啦";
                                        //        initsharemassage(GameContent);
                                        //    }
                                        //    if (index === 1) {
                                        //        $('.prize_box').show(); //中奖页面显示
                                        //        $('.container').addClass('active');
                                        //        GameContent.friend = "看剧享果粒，让乐趣上演。我在可口可乐活动中获得电影情侣套票，快来参与";
                                        //        initsharemassage(GameContent);
                                        //    }
                                        //},
                                        //    100);
                                    }
                                });
                            }
                        });
        });
        //活动说明
        $("div.divtoactinfo").on("click", function () {
            popdivc("activeinfo");
        });

    })

</script>
</html>
