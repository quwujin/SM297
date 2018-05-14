var fHeight = 50;
$(function () {
    var w = $(window).width();
    var h = $(window).height();
    
    $("#footer").css("width", w + "px");
   // $("#sbox").css({ "left": w / (640 / 70) + "px", "width": w / (640 / 460) + "px", "fontSize": w / (640 / 27)  + "px" });
    var bgh = w/(640/1008);
    if (h < bgh) {
        h = bgh;
    }

})
 


function showbox(ty, t) {
    $("#lbErr").html(t);
    if (ty == 1) {
        $("#sbox").fadeIn(1500, function () {
            $("#sbox").fadeOut(3000);
        });
    } else if (ty == 2) {
        $("#sbox").fadeIn(1500);

    } else if (ty == 3) {
        $("#sbox").fadeOut(1000);
    } else if (ty == 4) {
        $(function () {
            var w = $(window).width();
            var h = $(window).height();
            $("#sbox").css({ left: "0px", width: w + "px", height: h + "px", display: "block", top: "0px", margin: "0px", padding: "0px", border: "none", borderRadius: "0px" });
            $("#lbErr").css({ display: "block",margin:"auto", marginTop: "200px", border: " 1px solid #dedede", borderRadius: " 5px", width: w/(640/300) + "px" });
            $("#lbErr").html(t);
        })
    }

}

function SetStyle(obj, width, height, mleft, mtop, size,lineheight) {
    var w = $(window).width();
    if (width > 0) {
        $(obj).css("width",w / (640 / width) +"px");
    }
    if (height > 0) {
        $(obj).css("height", w / (640 / height) + "px");
    }
    if (mleft > 0) {
        $(obj).css("marginLeft", w / (640 / mleft) + "px");
    }
    if (mtop > 0) {
        $(obj).css("marginTop", w / (640 / mtop) + "px");
    }
    if (size > 0) {
        $(obj).css("fontSize", w / (640 / size) + "px");
    }
    if (lineheight > 0) {
        $(obj).css("lineHeight", w / (640 / lineheight) + "px");
    }
    return obj;
} 

wx.config({
    debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
    appId: js_appid, // 必填，公众号的唯一标识
    timestamp: timestamp, // 必填，生成签名的时间戳
    nonceStr: nonceStr, // 必填，生成签名的随机串
    signature: signature,// 必填，签名，见附录1
    jsApiList: ['onMenuShareTimeline', 'onMenuShareAppMessage', 'hideOptionMenu'] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
});

var GameContent = {
    link: "http://" + document.domain + "/default.aspx",
    image: "http://" + document.domain + "/images/001.jpg",
    title: '',
    friend: '',
    isshare:''
};

function initsharemassage(content) {
    wx.onMenuShareTimeline({
        title: content.title, // 分享标题
        link: content.link, // 分享链接
        imgUrl: content.image, // 分享图标
        success: function () {
            //share();
        },
        cancel: function () {
            // 用户取消分享后执行的回调函数
        }
    });
    wx.onMenuShareAppMessage({
        title: content.title, // 分享标题
        desc: content.friend, // 分享描述
        link: content.link, // 分享链接
        imgUrl: content.image, // 分享图标  
        success: function () {
            // 用户确认分享后执行的回调函数
            //share();
        },
        cancel: function () {
            // 用户取消分享后执行的回调函数
        }
    });

    if(content.isshare)
        wx.hideOptionMenu();//取消分享

}

wx.ready(function () {
    initsharemassage(GameContent);
});
wx.error(function (res) {
    // config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。
    //alert(res);
});

//--------------------------------公共函数
 
//获得http url参数
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return decodeURIComponent(r[2]); return null;
}//end func


//---------------------------------------正则
function checkStr(str, type) {
    type = type || 0;
    switch (type) {
        case 0:
            var reg = new RegExp(/^1[123458769][0-9]{9}$/);//手机号码验证
            break;
        case 1:
            var reg = new RegExp(/^\d+$/);//是否为0-9的数字
            break;
        case 2:
            var reg = new RegExp(/^[a-z]+$/);//是否为小写
            break;
        case 3:
            var reg = new RegExp(/^[A-Z]+$/);//是否为大写
            break;
        case 4:
            var reg = new RegExp(/^\w+$/);//匹配由数字、26个英文字母或者下划线组成的字符串
            break;
        case 5:
            var reg = new RegExp(/^[a-zA-Z\s\u0391-\uFFE5]+$/);//匹配中文
            break;
        case 6:
            var reg = new RegExp(/\w+@\w+/);//匹配EMAIL
            break;
        case 7:
            var reg = new RegExp(/^[a-zA-Z\u0391-\uFFE5]+$/);//不能包含数字和符号
            break;
        case 8:
            var reg = new RegExp(/^\d{9}$/);//，9位数字
            break;
        case 9:
            var reg = new RegExp(/^[a-zA-Z\u0391-\uFFE5]*[\w\u0391-\uFFE5]*$/);//不能以数字或符号开头
            break;
        case 10:
            var reg = new RegExp(/(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/);//是否为身份证号
            break;
        case 11:
            var reg = new RegExp(/^(?=.*[\u4E00-\u9FA5])[\w\u4E00-\u9FA5!！，,。.；;-]{0,180}$/);//是否为地址
            break; 
        case 12:
            var reg = new RegExp(/^[a-zA-Z0-9]+$/);//是否为激活码
            break;
        default:
            break;
    }//end switch
    if (reg.exec($.trim(str))) return true;
    else return false;
}//end func

//判断是否为空 
function isEmpty(str){ 
	if(str==null||typeof str=="undefined"||str.trim()==""){ 
		return true; 
	}else{ 
		return false; 
	} 
} 

//---------------------------------------数学函数
//end func 获得范围内随机整数
function randomRange(min, max) {
    var randomNumber;
    randomNumber = Math.floor(Math.random() * (max - min + 1)) + min;
    return randomNumber;
} 
function randomSort(ary) {
    var len = ary.length;
    var rnd = [];
    for (var i = 0; i <= len - 1; i++) {
        var ran = Math.floor(Math.random() * ary.length);//从数组shu中随机选一个元素（第k个）
        rnd.push(ary[ran]);//把数组shu中选出的元素的值赋给数组myArry第i个元素；
        ary.splice(ran, 1);//把数组shu中第k个元素删掉（保证下一次选的一定不会重复)
    }//end for
    for (i = 0; i <= len - 1; i++) ary[i] = rnd[i];
}//end func 随机排序一个数组

function getRound(num, n) {
    n = n || 2;
    var r = Math.pow(10, n);
    return Math.round(num * r) / r;
}//end func 获得几位小数点

function randomPlus() {
    return Math.random() < 0.5 ? -1 : 1;
}//end func 随机正负

function toRadian(degree) {
    return degree * Math.PI / 180;
}//end func 角度转弧度

function toDegree(radian) {
    return radian / Math.PI * 180;
}//end func 弧度转角度	


