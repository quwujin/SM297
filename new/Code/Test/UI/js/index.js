$(function() {

    // 点击活动说明
    // $("div.divtoactinfo").click(function() {
    //     popdivc("activeinfobox");
    // });

    // 点击返回
    // $("div.pop div.activeinfobox input.btnback,div.pop div.etcimgbox input.btnback").click(function() {
    //     popdivc();
    // });

    // $("div.divtoprizelist").click(function() {
    //     window.location.href="myrank.html";
    // });

    // 客服机器人
    // var custHref="https://esmart.udesk.cn/im_client/?web_plugin_id=47298";
    // $('<a href='+custHref+' class="Customer"  ><img  class="cus_img" src="images/man.png" alt=""><p class="cus_text">在线客服</p></a>').appendTo($('body'));
	//todo 上线前修改
    showFist('前端测试链接，中奖无效',2,'right');
    isios();
    wh('div.container div.boxcotainer');
    // 点击活动说明
    $("div.divtoactinfo").click(function() {
        popdivc("activeinfo");
    });
    $("input.btnback").on("click",function(){
        popdivc();
    });
});
// 参数:字符串或空;字符串为需要显示的div.pop下面的class名字,输入为空则隐藏div.pop及其所有子元素)
function popdivc(){
        var cc = arguments[0] ? arguments[0] : "";   
        $("div.pop").addClass('hide');
        $("div.pop").children().addClass('hide');
        if(""!=cc){$("div.pop").removeClass('hide');$(".pop ."+cc).removeClass('hide');}
}
// 　　微信关闭
function closeWindow(){
    WeixinJSBridge.call('closeWindow');
    window.close();
}
function sandh(c, cc) {
    if (c != undefined && cc != undefined) {
        if (c != "" && cc != "") {
            $(c).removeClass('hide');
            $(cc).addClass('hide');
        } else if (cc != "" && c == "") {
            $(cc).addClass('hide');
        } else if (cc == "" && c != "") {
            $(c).removeClass('hide');
        }
    }
}

function showbox(ty, t) {
    if($('#sbox').length<=0){
        $('<div id="sbox"><div class="sbox_inner"></div></div>').appendTo('body');
    }
    var w = $(window).width();
    var h = $(window).height();
    if (ty == 0) {
        $(".sbox_inner").html(t);
        $("#sbox").stop().fadeIn(1500);
    }else if (ty == 1) {
        $(".sbox_inner,#lbErr").html(t);
        $("#sbox").fadeIn(1500, function () {
            $("#sbox").fadeOut(1500);
        });
    } else if (ty == 2) {
        $(".sbox_inner,#lbErr").html(t);
        $("#sbox").stop().css({opacity:1}).fadeIn(1500);
    } else if (ty == 3) {
        $(".sbox_inner,#lbErr").html(t);
        $("#sbox").fadeOut(1000);
    } else if (ty == 4) {
        $("#sbox").stop().css({opacity:1, display:'block',backgroundColor:'rgba(0,0,0,0.8)'});
        $(".sbox_inner,#lbErr").html(t).show();
    }else if(ty==5){
        $("#sbox").show().css({backgroundColor:'rgba(0,0,0,0.8)'});
        $(".sbox_inner,#lbErr").hide();
        if($('#sbox').find('img').length==0){
            var newImg=new Image();
            newImg.src=t;
            $(newImg).appendTo('#sbox');
        }else{
            $('#sbox').find('img').attr('src',t);
        }
    }
}
// 获取url参数 支持中文
function GetRequest() {
    var url = location.search; //获取url中"?"符后的字串
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for(var i = 0; i < strs.length; i ++) {
            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
        }
    }
    return theRequest;
}

function isios(){
    var u = navigator.userAgent;
    var isAndroid = u.indexOf('Android') > -1 || u.indexOf('Adr') > -1; //android终端
    var isiOS = !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/); //ios终端
    if (isiOS) {
        $(".input_file").removeAttr("capture");
    }
}
function wh(dom){
    var w=$(window).width();
    var h=$(window).height();
    var container=$('.container').height();
    if(h>container){
        $('body').addClass('active').css({width:w,height:h}).css({'overflow':'hidden'});
        $(".container").css({'height':h});

        // $(dom).css({'height':h});
        // var newH=(h-container)/2;
        // $(dom).css('margin-top',newH);
    }
}
//头部显示文案
// text   文案
//  color  默认 红色 2   灰色 1，
//  left   默认 left   right
function showFist(text,color,left) {
    if(color==undefined||color==2){
        color='red';
    }

    if(color==1){
        color='gray';
    }
    if(left==undefined||left=='left'){
        left=' left'
    }
    if(left=="right"){
        left=' right';
    }
    $('<div class="first '+color+left+'" style="">'+text+' <div class="right_top"></div> <div class="right_bottom"></div></div>').appendTo('body');
}

