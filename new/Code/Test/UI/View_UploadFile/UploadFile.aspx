<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadFile.aspx.cs" Inherits="UploadFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <meta name="aplus-terminal" content="1" />
    <%=WebFramework.GeneralMethodBase.LoadJs() %>
    <script src="../js/jQueryRotate.2.2.js"></script>
    <script src="../js/jquery.ui.draggable.js"></script>
    <script src="../js/jquery.easing.min.js"></script>
    <script src="../js/jquery.ui.widget.js"></script>
    <script src="../js/jquery.fileupload.js"></script>

    <script type="text/javascript" src="../js/rem.min.js"></script>
    <link rel="stylesheet" href="../css/global.min.css">
    <link rel="stylesheet" href="../css/index.min.css">
    <%--<script type="text/javascript" src="../js/jquery-1.9.1.min.js"></script>--%>
    <script type="text/javascript" src="../js/index.min.js"></script>

    <script>
        var IsFile = false;
        $(function() {
            //上传图片
            $("input.fileup").fileupload({
                autoUpload: true, //是否自动上传
                url: '<%=WebFramework.GeneralMethodBase.GetUploadImgURL%>', //上传地址 
                send: function(e, data) { showbox(2, '正在上传中...'); },
                progressall: function(e, data) { //设置上传进度事件的回调函数  
                    var progress = parseInt(data.loaded / data.total * 100, 10);
                    $("#lbErr").html("正在上传中..." + progress + '%');
                },
                done: function (e, result) { //设置文件上传完毕事件的回调函数 
                    if (result.result.IsSuccess &&
                        result.result.ResultData.list != null &&
                        result.result.ResultData.list.length > 0) {
                        $.post(
                            "Servicefile.ashx",
                            {
                                FileName: result.result.ResultData.list[0].ImgUrl,
                                FileHash: result.result.ResultData.list[0].ImgSign,
                                OriginFileName: result.result.ResultData.list[0].OriginImgUrl,
                            },
                            function(xml) {
                                if (xml == "0") {
                                    IsFile = true;
                                    $("#Hffile").val(result.result.ResultData.list[0].ImgUrl);
                                    $(".imguploadright").attr("src", result.result.ResultData.list[0].ImgUrl);
                                    showbox(1, "上传成功...");
                                } else {
                                    showbox(1, "上传失败，请重新上传...");
                                }
                            });
                    } else {
                        showbox(1, "上传失败，请重新上传...");
                    }
                }
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <%--错误提示信息--%>
        <div id="sbox">
            <asp:Label ID="lbErr" runat="server" Text=""></asp:Label></div>
        <asp:HiddenField runat="server" ID="Hffile" />
        <div class="container ">
            <div class="divlogotop">
                <img src="../images/logotop.png" alt="" /></div>
            <div class="boxcotainer m0a ovh">
                <div class="boxuploadall m0a">
                    <div class="boxupload m0a">
                        <div class="divuploadpic m0a">
                            <div class="divuploadleft fl">
                                <img src="../images/uploadleft.png" alt="" class="imguploadleft" style="width: 2.2rem;"></div>
                            <div class="divuploadright fr">
                                <img src="../images/uploadright.png" alt="" class="imguploadright" style="width: 2.2rem;"></div>
                        </div>
                        <div class="divuploadaddmob divinputtext m0a tac">
                            <input type="tel" class="inputuploadaddmob tac" maxlength="11" placeholder="请输入您的手机号码"
                                pattern="[0-9]*">
                        </div>
                        <p class="puploadaddmobtips m0a tac">*请如实填写您的手机号,将以此作为中奖查询依据</p>
                    </div>
                    <div class="boxuploadtips m0a tac">
                        <div class="divuploadtipstitle m0a tac">
                            <p class="puploadtipstitle">温馨提示</p>
                        </div>
                        <div class="divuploadtipscontent m0a">
                            <ol>
                                <li>1. 小票需要清晰显示便利店名称、时间、流水号和<br>
                                    &nbsp;&nbsp;&nbsp;&nbsp;士力架或M&M'S产品清单；</li>
                                <li>2. 购物小票打印时间需在活动期间：2018年6月1日<br>
                                    &nbsp;&nbsp;&nbsp;&nbsp;—2018年7月15日；</li>
                                <li>3. 一张购物小票仅限参与一次，同一个手机号、同<br>
                                    &nbsp;&nbsp;&nbsp;&nbsp;一个微信ID可以参与5次。</li>
                            </ol>
                        </div>
                    </div>
                </div>
                <div class="divinputbtn">
                    <input type="button" value="" class="btn btntolottery" /></div>
            </div>
            <div class="divlogobot">
                <img src="../images/logobot.png" alt="" /></div>
            <div class="divtoactinfo">
                <img src="../images/toactinfo.png" alt="" class="imgtoactinfo" /></div>
            <div class="pop hide">
                <%--活动说明--%>
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
                <%--小票样式--%>
                <div class="boxpopthing picetc hide">
                    <div class="divpopthing">
                        <div class="divpicetc m0a">
                            <img src="../images/picetc.png" alt="" class="imgpicetc">
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
        //点击上传小票
        $("div.divuploadright").on("click", function () {
            $("input.fileup").click();
        });
        //点击查看活动说明
        $("div.divtoactinfo").on("click", function () {
            popdivc("activeinfo");
        });
        //查看小票样式
        $("div.divuploadleft").on("click", function () {
            popdivc("picetc");
        });
        //提交按钮
        $("input.btntolottery").on("click", function () {
            var mob = $('input.inputuploadaddmob').val();
            /*判断上传文件*/
            var file = $("#Hffile").val();
            if (file == "") {
                showbox(1, '请上传小票');
                return false;
            }
            // 检查电话号码
            if (mob == "") {
                showbox(1, '请输入手机号');
                return false;
            }
            if (checkStr(mob, 0) == false) {
                showbox(1, '请填写正确手机号！');
                return false;
            }
            else {
                showbox(2, '正在提交中，请等待……');
                $("input.btntolottery").attr("disabled", "true");
                $.post("../Controller/ApiController.ashx", {
                    mob: mob,
                    file:file,
                    GetResult: "getcode"
                }, function (data, status) {
                    $("#sbox").fadeOut(200, function () {
                        if (status == "success") {
                            var result = $.parseJSON(data);
                            if (result.Success == false) {
                                showbox(1, result.ErrMessage);
                                $("input.btntolottery").removeAttr("disabled");
                            } else {
                                window.location.href = '/lottery.aspx';
                            }
                        }
                    });
                });
            }
        });

    })

</script>
</html>
