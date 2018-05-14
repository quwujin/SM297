<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrizeList.aspx.cs" Inherits="PrizeList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
<meta name="apple-mobile-web-app-capable" content="yes"/>
<meta name="apple-mobile-web-app-status-bar-style" content="black"/>
<meta name="format-detection" content="telephone=no"/>
<meta name="aplus-terminal" content="1"/>
    <%=WebFramework.GeneralMethodBase.LoadJs() %>
    <script src="js/msclass.js"></script> 
    <script type="text/javascript">
        $(function(){

            SetStyle(".list-bg", 508, 431, 79, 10);
            
            SetStyle(".search_div", 430, 93, 114, 20, 0);
            SetStyle("#se_btn", 320, 53, 10, 15, 30);
            SetStyle("#ser_btn", 49, 60, 5, 155, 30);

            SetStyle("#List_div", 377, 153, 72, 150, 30); 
            SetStyle("#List_bg", 377, 153, 0, 0, 30); 
            //SetStyle(".title_div", 490, 32, 68, 35, 30);

            SetStyle(".ser_div", 377, 153, 72, 150, 30); 
            SetStyle("#ser_bg", 377, 153, 0, 0, 30); 

            SetStyle(".bk-bt", 204, 70, 218, 40, 35);
             
            //data();
             

        })


        var pcount = <%=pcount%>;
        var page = 1;
        function data() {
            $.post("PrizeList.aspx", { GetResult: "PrizeList", page: page }, function (xml) {
                $("#List_bg").append(xml);
              
                    //Step:速度  需要数据加载完毕后再执行
                new Marquee({ MSClassID: "List_bg", Direction: "top", Step: 2, Width: $(window).width()/(640/377), Height: $(window).width()/(640/153), Timer: 50, DelayTime: 0, WaitTime: 0, ScrollStep: 52, AutoStart: 1 });
               
            });
        }

        function btnTjClick() { 
            
            var mob=$("#se_btn").val();
            if(mob.length!=11){
                return;
            } 
            //showbox(2, '正在搜索中，请等待……');

            //if (!CheckVal("#se_btn")) {
            //    showbox(1, '请填写手机号！');
            //    $("#ser_btn").removeAttr("disabled");
            //    return ;
            //}
            if (!checkMob(mob)) {
                showbox(1, '请填写正确手机号！');
                $("#ser_btn").removeAttr("disabled");
                return;
            }

            $.post("PrizeList.aspx", { GetResult: "MobPrizeList", mob: $("#se_btn").val() }, function (xml) {
                $("#sbox").css("display", "none");
                $("#lbErr").val("");
                if (xml == "has_no") { 
                    showbox(1, '该手机号未中奖！'); 
                }else if(xml == "mob_err"){
                    showbox(1, '请填写正确手机号！');  
                } 
                else { 
                    $("#ser_bg").html(xml);

                    $("#List_div").css("display", "none");
                    $(".ser_div").css("display", "block");
                }
            });  
        }
    </script>

    <style> 
        .list-bg{display:block;float:left;background:url(images/list-bg.png) no-repeat;background-size:100% 100%;}

        .search_div{display:block;float:left;background:url(images/md_bg.png) no-repeat;background-size:100% 100%;}
        .search_div #se_btn,#ser_btn{padding:0px; display:block;float:left;color:white; background:none;border:none;text-align:center;}

        #List_div{display:block;float:left;}
        #List_div #List_bg{display:block;float:left;list-style:none;overflow:hidden;}
        #List_div #List_bg li{width:100%;display:block;float:left;}
        #List_div #List_bg li b,p{margin:0px;padding:0px;width:49%; display:block;float:left;font-weight:bold;color:#054c99;text-align:center;overflow:hidden;}
         
        .ser_div{display:none;float:left;list-style:none;overflow:hidden;}
        .ser_div #ser_bg{display:block;float:left;list-style:none;overflow:hidden;}
        .ser_div #ser_bg li{width:100%;display:block;float:left;}
        .ser_div #ser_bg li b,p{margin:0px;padding:0px; width:49%; display:block;float:left;font-weight:bold;color:#054c99;text-align:center;overflow:hidden;}

        .title_div{display:block;float:left;text-align:center;}
        .title_div b{display:block;float:left;font-weight:bold;color:#ffec00;margin:0px;padding:0px; width:49%; display:block;float:left;}


        .bk-bt{display:block;float:left;background:url(images/back-bt.png) no-repeat;background-size:100% 100%;border:none;border-radius:0px;font-weight:bold;color:#0065ac;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div id="sbox"><asp:Label ID="lbErr" runat="server" Text="" ></asp:Label></div> 
        <audio src="images/music.mp3" autoplay="autoplay"></audio> 
     <img src="images/logo.png" />
      
     <div class="list-bg">
         <%--<div class="title_div"><b>奖项</b><b>中奖手机号</b></div>--%>  

         <div id="List_div">
		    <ul id="List_bg">
               <%--<li><b>18321933327</b><p>一等奖</p></li>--%> 
		    </ul>
	     </div> 

         <div class="ser_div">
           <ul id="ser_bg">
                <%--<li><b>一等奖</b><p>18321933327</p></li>--%> 
	       </ul>
         </div> 
     </div>

     <div class="search_div">
            <input type="text" placeholder="请输入参与手机号查询" value="" maxlength="11"  id="se_btn" onblur="btnTjClick();" onkeyup="btnTjClick();" onfocus="btnTjClick();" />
            <%--<input type="button"  id="ser_btn" onclick="btnTjClick();" />--%> 
     </div>
         
    <input type="button" value="" class="bk-bt" onclick="window.location.href='default.aspx'" />
    </form>
</body>
</html>
