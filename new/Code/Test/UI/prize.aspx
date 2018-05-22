<%@ Page Language="C#" AutoEventWireup="true" CodeFile="prize.aspx.cs" Inherits="prize" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0,minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="aplus-terminal" content="1" />
    <%=WebFramework.GeneralMethodBase.LoadJs() %>
    <script type="text/javascript" src="js/rem.min.js"></script>
    <link rel="stylesheet" href="css/global.min.css">
    <link rel="stylesheet" href="css/weui.min.css">
    <link rel="stylesheet" href="css/index.min.css">
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="js/index.min.js"></script>
    <script src="js/city.min.js"></script>
    <script src="js/weui.min.js"></script>
</head>
<body class="">
    <form runat="server">
        <!--提示框-->
        <div id="sbox">
            <asp:Label ID="lbErr" runat="server"></asp:Label>
        </div>
        <asp:HiddenField  ID="Hprize" runat="server"  />
        <div class="container ">
            <div class="divlogotop">
                <img src="images/logotop.png" alt="" /></div>
            <div class="boxcotainer m0a ovh">
                <div class="boxprize1 hide">
                    <div class="boxprize1get">
                        <div class="prizeinfocontent m0a">
                            <div class="divprize1gettop m0a">
                                <p class="pboxprize1gettitle tac">恭喜您<br>
                                    获得一等奖俄罗斯机票</p>
                                <p class="pboxprize1gettitletips tac">（价值4999元携程代金券）</p>
                                <div class="divprize1getshow m0a">
                                    <img src="images/prize1.png" alt="" class="imgprize1getshow"></div>
                            </div>
                            <div class="divprize1getbottom m0a">
                                <p class="pprize1getbottom tac m0a">
                                    我们将在三个工作日内审核小票<br>
                                    小票审核通过，将与您联系安排发放奖品<br>
                                    小票审核失败，则视中奖权益无效<br>
                                    如有任何疑问，请致电客服热线：400-822-7508<br>
                                    工作时间：10:00-18:00（法定节假日除外）
                                </p>
                            </div>
                        </div>
                        <div class="divinputbtn">
                            <input type="button" value="" class="btn btnshare " /></div>
                    </div>
                </div>
                <div class="boxprize2 hide">
                    <div class="boxprize2get">
                        <p class="pboxprize2gettitle tac">恭喜您<br>
                            获得二等奖限量定制足球</p>
                        <div class="divprize2getshow m0a ball">
                            <img src="images/prize2.png" alt="" class="imgprize2getshow"></div>
                        <p class="pboxprize2gettitletips tac">*图片仅供参考，奖品以实物为准</p>
                        <div class="divinputbtn">
                            <input type="button" value="" class="btn btngetprize " /></div>
                    </div>
                    <div class="boxprize2addinfo hide">
                        <div class="prizeinfocontent m0a">
                            <div class="divprize2gettop m0a">
                                <p class="pboxprize2gettitle tac">请填写邮寄地址</p>
                                <div class="divinputtext m0a tac">
                                    <input type="text" class="tac username" placeholder="姓名" /></div>
                                <div class="shengshiqu m0a tac">
                                    <div class="divsheng divinputtext tac">
                                        <input readonly="readonly" type="text" name="" id="" class="sheng tac province" placeholder="省" onfocus="this.blur()" style="text-overflow: ellipsis; width: 1.26rem"  /></div>
                                    <div class="divshi divinputtext tac ">
                                        <input readonly="readonly" type="text" name="" id="" class="shi tac city" placeholder="市" onfocus="this.blur()" style="text-overflow: ellipsis; width: 1.26rem"  /></div>
                                    <div class="divqu divinputtext tac ">
                                        <input readonly="readonly" type="text" name="" id="" class="qu tac place" placeholder="区" onfocus="this.blur()" style="text-overflow: ellipsis; width: 1.26rem"  /></div>
                                </div>
                                <div class="divinputtext m0a tac">
                                    <input type="text" class="tac saddress" placeholder="请输入详细地址" /></div>
                            </div>
                            <div class="divprize2getbottom m0a">
                                <p class="pprize2getbottom tac m0a">
                                    我们将在三个工作日内审核小票<br>
                                    小票审核通过，将与您联系安排发放奖品<br>
                                    小票审核失败，则视中奖权益无效<br>
                                    如有任何疑问，请致电客服热线：400-822-7508<br>
                                    工作时间：10:00-18:00（法定节假日除外）
                                </p>
                            </div>
                        </div>
                        <div class="divinputbtn">
                            <input type="button" value="" class="btn btnsure " /></div>
                    </div>
                </div>
                <div class="boxprize0 hide">
                    <div class="divprize0get m0a">
                        <div class="divprize0getshow m0a">
                            <img src="images/prize0.png" alt="" class="imgprize0getshow"></div>
                        <div class="divinputbtn">
                            <input type="button" value="" class="btn btnclose " /></div>
                    </div>
                </div>
            </div>
            <div class="divlogobot">
                <img src="images/logobot.png" alt="" /></div>
            <div class="divtoactinfo">
                <img src="images/toactinfo.png" alt="" class="imgtoactinfo" /></div>
            <div class="pop hide">
                <div class="boxpopthing activeinfo hide">
                    <div class="divpopthing">
                        <div class="divpoptitle m0a tac">
                            <p class="ppoptitle">活动说明</p>
                        </div>
                        <div class="divpopcontent m0a">
                            <div class="activeinfocon m0a"><%=mm.Notes %></div>
                        </div>
                    </div>
                    <div class="divinputbtn">
                        <input type="button" value="" class="btnback btn" /></div>
                </div>
                <div class="boxpopthing share hide">
                    <div class="boxshare">
                        <img src="images/share.png" alt="" class="imgshare"></div>
                </div>
                <div class="boxpopthing upsuccess hide">
                    <div class="divpopthing">
                        <div class="divupsuccess m0a">
                            <img src="images/upsuccess.png" alt="" class="imgupsuccess">
                        </div>
                    </div>
                    <div class="divinputbtn">
                        <input type="button" value="" class="btnshare btn" /></div>
                </div>
            </div>
        </div>
    </form>
</body>
<script>
    $(function() {

        //var prizenum = GetRequest();
        //{分享}
        $("input.btnshare").click(function() {
            popdivc("share");
        });

        $("input.btnclose").on("click",
            function() {
                closeWindow();
            });
        //{确认提交}
        $("input.btnsure").on("click",
            function() {
                if ($("#Hprize").val() == 2) {
                    var addresss = new RegExp(/^(?=.*[\u4E00-\u9FA5])[\w\u4E00-\u9FA5!！，,。.；;-]{0,180}$/);
                    var regExp2 = new RegExp(/^[a-zA-Z\s\u4E00-\u9FA5]{2,10}$/);
                    if ($("input.username").val() == "") {
                        showbox(1, '请输入您的姓名');
                        return false;
                    }
                    if (regExp2.test($("input.username").val()) == false) {
                        showbox(1, "请正确填写姓名");
                    }
                    if ($("input.sheng").val() == "") {
                        showbox(1, '请选择省');
                        return false;
                    }
                    if ($("input.shi").val() == "") {
                        showbox(1, '请选择市');
                        return false;
                    }
                    if ($("input.saddress").val() == "") {
                        showbox(1, '请填写详细地址');
                        return false;
                    }
                    if (addresss.test($("input.saddress").val()) == false) {
                        showbox(1, '请正确填写详细地址');
                        return false;
                    }
                }
                $("input.btntolottery").attr("disabled", "true");
                $.post("../Controller/ApiController.ashx", {
                    name: $("input.username").val(),
                    province:$("input.sheng").val(),
                    city:$("input.shi").val(),
                    area:$("input.qu").val(),
                    detailAddress:$("input.saddress").val(),
                    GetResult: "updateaddress"
                }, function (data, status) {
                    $("#sbox").fadeOut(200, function () {
                        if (status == "success") {
                            var result = $.parseJSON(data);
                            if (result.Success == false) {
                                showbox(1, result.ErrMessage);
                                $("input.btntolottery").removeAttr("disabled");
                            } else {
                                //提交成功
                                popdivc("upsuccess");
                            }
                        }
                    });
                });
                
            });
        
            //{我要领奖}
        $("input.btngetprize").on("click",
            function() {
                sandh(".boxprize2 .boxprize2addinfo", ".boxprize2 .boxprize2get");
            });

        if ($("#Hprize").val() == 1) { //一等奖
            $("div.boxcotainer div.boxprize1").removeClass("hide");
            GameContent.friend = "我在士力架世界杯活动中抽中了俄罗斯机票一张，快来抽出属于你的惊喜好礼吧!";
            initsharemassage(GameContent);
        } else if ($("#Hprize").val() == 2) { //二等奖
            $("div.boxcotainer div.boxprize2").removeClass("hide");
            GameContent.friend = "我在士力架世界杯活动中抽中了定制足球一个，快来抽出属于你的惊喜好礼吧!";
            initsharemassage(GameContent);
            // 省市区函数
            shenShi();
        } else { //参与奖
            $("div.boxcotainer div.boxprize0").removeClass("hide");
        }

    });

    function shenShi() {
        var shen = [];
        var shi = [];
        var qu = [];
        for (var i = 0; i < rawCitiesData.length; i++) {
            shen.push(rawCitiesData[i].name);
        }

        function creat(arr) {
            var mewArr = [];
            for (var i = 0; i < arr.length; i++) {
                mewArr[i] = {};
                mewArr[i].label = arr[i];
                mewArr[i].value = i;
            }
            return mewArr;
        }

        $('.province').click(function() {
            weui.picker(creat(shen.unique3()),
                {
                    className: 'custom-classname',
                    defaultValue: [1], //默认显示
                    //                  取消
                    onChange: function(result) {},
                    //                  确定
                    onConfirm: function(result) {
                        $('.province').val(result[0].label);
                        $('.city').val("");
                        $('.place').val("");
                        shi = [];
                        qu = [];
                    },
                    id: 'singleLinePicker'
                });
        });
        $('.city').click(function() {
            var sheVal = $('.province').val();
            for (var i = 0; i < rawCitiesData.length; i++) {
                if (sheVal == rawCitiesData[i].name) {
                    var taiy = rawCitiesData[i].sub;
                    for (var j = 0; j < taiy.length; j++) {
                        shi.push(taiy[j].name);
                    }
                }
            }
            weui.picker(creat(shi.unique3()),
                {
                    className: 'custom-classname',
                    defaultValue: [1], //默认显示
                    //                  取消
                    onChange: function(result) {},
                    //                  确定
                    onConfirm: function(result) {
                        $('.city').val(result[0].label);
                        $('.place').val("");
                        qu = [];
                    },
                    id: 'singleLinePicker'
                });

        });
        $('.place').click(function() {
            var sheVal = $('.province').val();
            var shiVal = $('.city').val();
            for (var i = 0; i < rawCitiesData.length; i++) {
                if (sheVal == rawCitiesData[i].name) {
                    for (var j = 0; j < rawCitiesData[i].sub.length; j++) {
                        var shi = rawCitiesData[i].sub[j];
                        if (shi.name == shiVal) {
                            for (var k = 0; k < shi.sub.length; k++) {
                                qu.push(shi.sub[k].name);
                            }
                        }

                    }
                }
            }
            weui.picker(creat(qu.unique3()),
                {
                    className: 'custom-classname',
                    defaultValue: [1], //默认显示
                    //                  取消
                    onChange: function(result) {},
                    //                  确定
                    onConfirm: function(result) {
                        $('.place').val(result[0].label);
                    },
                    id: 'singleLinePicker'
                });

        });
    }
    Array.prototype.unique3 = function () {
        var res = [];
        var json = {};
        for (var i = 0; i < this.length; i++) {
            if (!json[this[i]]) {
                res.push(this[i]);
                json[this[i]] = 1;
            }
        }
        return res;
    }
</script>
</html>
