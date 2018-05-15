<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

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
    <title>首页</title>
    <script type="text/javascript" src="js/rem.min.js"></script>
    <link rel="stylesheet" href="css/global.min.css">
    <link rel="stylesheet" href="css/index.min.css">
    <script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="js/index.min.js"></script>
</head>
<body class="indexactive">
<form runat="server">
    <div class="container indexcontainer">
        <div class="divlogotop"><img src="images/logotop.png" alt="" /></div>
        <div class="boxindexcotainer m0a ovh">
            <div class="boxindextitle">
                <div class="divindextitle"><img src="images/indextitle.png" alt="" class="imgindextitle m0a" style="width: 6.14rem;"></div>
                <div class="divindextitleinfo">
                    <p class="pindextitleinfo tac">在全国任意便利店购买<br>任意士力架或M&M'S产品，上传购物小票<br>即有机会赢取价值<span class="spanindextitleinfo">4999元</span>机票大奖<br>或限量定制足球</p>
                </div>
            </div>
            <div class="divinputbtn"><input type="button" value="" class="btn btntoupload " /></div>
        </div>
        <div class="divlogobot"><img src="images/logobot.png" alt="" /></div>
        <div class="divtoactinfo"><img src="images/toactinfo.png" alt="" class="imgtoactinfo" /></div>
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
                <div class="divinputbtn"><input type="button" value="" class="btnback btn" /></div>
            </div>
        </div>
    </div>
    <!--提示框-->
    <div id="sbox">
        <asp:Label ID="lbErr" runat="server"></asp:Label>
    </div>
</form>
</body>
<script>
 $(function(){
     //点击按钮跳转----抽奖赢大礼
     $("input.btntoupload").on("click",function() {
         window.location.href = "View_UploadFile/UploadFile.aspx";
     });

 })

</script>
</html>