var Countly = Countly || {};
Countly.q = Countly.q || [];
//每个活动对应一个app_key,每个app_key的值由后台配置得出，后台需要做校验
Countly.app_key = "niumeng888";

/*openid属于活动必须收集的内容,赋值方式由上海团队根据前端业务情况自行决定，可以是cookie或者localcache*/
//Countly.openid = "";

//后台收集前端信息的地址
Countly.url = "http://47.97.198.185"; //your server goes here
//是否调试，设置为true则，可以在浏览器看到各个事件的出发情况
Countly.debug = false;
//默认跟踪事件设置
//默认跟踪pageview
Countly.q.push(['add_event',{
	"key": "pageview",
	"segmentation": {
    "page_id": '1',
    "page_title": document.title,
    "page_url": window.location.href,
    "page_param1" : "page_param1",
    "page_param2" : "page_param2"
	}
}]);
Countly.q.push(['add_event',{
	"key": "event",
	"segmentation": {
    "page_id": '1',
    "page_title": document.title,
    "page_url": window.location.href,
    "category" : "red_packet",
    "action" : "get",
    "event_param1" : "event_param1",
    "event_param2" : "event_param2"
	}
}]);
Countly.q.push(['track_sessions']);

// Countly.clevents=[];
// function initCollectEvent(clevents){
//     if(!clevents) return;
//     for(var i=0;i<clevents.length;i++){
//         var event = clevents[i],triggerObj;
//         initevent(event);
//     }
// }
// function  initevent(event) {
//     var ename = event["eventname"];
//     var trigger = event["triggerAttribute"];
//     var segmentation=event["evnetvalue"];
//     var enamestr = ename.split('=');
//     var key2 =enamestr[1];
//     triggerObj=  document.querySelector(trigger);
//     if(triggerObj.length<=0){
//         Countly.log_error("this trigger:"+trigger +"is not validate");
//     }
//     triggerObj.addEventListener(enamestr[0],function(){
//         var segmentation1=segmentation();
//         var key1 = key2;
//         Countly.q.push(['add_event',{
//             key:key1,
//             "segmentation": segmentation1
//         }]);
//     });
// }
$(function () {
    var cly = document.createElement('script'); cly.type = 'text/javascript';
    cly.async = true;
    //配置countly数据收集基础库的地址，可以是本地也可以是远程cdn地址
    cly.src = 'js/trackingSystemCountly/countly.min.js';
    cly.onload = function() {
        Countly.init({ "device_id": "ovznPs02JfdRZSmkrsgjTzpPf2hw" });
      // initCollectEvent(Countly.clevents);
    };
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(cly, s);
});


//无入侵埋点
function pushClevents(eventname, eventtype, triggerAttribute, evnetvalue)
{
    //Countly.clevents.push({ 
    //    "eventname": eventtype + "=" + eventname,//事件名称：joinactive，事件类型：click 
    //    "triggerAttribute": triggerAttribute, //促发事件的属性：#testButtonop 
    //    "evnetvalue": function () { //定义获取事件出发后，事件需要收集的数据，比如openid这个参数是必须的，还有其它个性化数据 
    //        //var prop = {};
    //        //prop.openid = "93525";
    //        //prop.validatecode = document.getElementById("custom").value;
    //        //prop.telephone = document.getElementById("telephone").value;
    //        return evnetvalue;
    //    }
    //}); 
}

//入侵式埋点
function addEvent(eventname, evnetvalue) {
    Countly.add_event({ 
        key: eventname, //埋点事件名称 
        "segmentation": evnetvalue //事件收集的数据对象 
    });
}



