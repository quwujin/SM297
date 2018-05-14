
            //全局变量，触摸开始位置
            var kg = 0;
            var startX = 0, startY = 0;
            var endX = 0, endY = 0;
            //touchstart事件
			var sc_sd=10;
			var fHeight=50;
			function show_Div(){}
			function hide_Div(){}
			function open_Page(){}
            function touchSatrtFunc(evt) {
                try {
				var target = evt.target;
				while (target.nodeType != 1) target = target.parentNode;
				if (!(target.tagName != 'SELECT' && target.tagName != 'INPUT' && target.tagName != 'A' && target.parentNode.tagName != 'A' && target.tagName != 'TEXTAREA' && target.className != 'success' && target.parentNode.className != 'ntus' && target.className != 'ntus'&& target.parentNode.className != 't_in' && target.className != 't_in' )) {
					return;
				}
					
                    evt.preventDefault(); //阻止触摸时浏览器的缩放、滚动条滚动等
                    kg = 1;
                    var touch = evt.touches[0]; //获取第一个触点
                    var x = Number(touch.pageX); //页面触点X坐标
                    var y = Number(touch.pageY); //页面触点Y坐标
                    //记录触点初始位置
                    startX = x;
                    startY = y;
                }
                catch (e) {
               //     alert('touchSatrtFunc：' + e.message);
                }
            }

            //touchmove事件，这个事件无法获取坐标
            function touchMoveFunc(evt) {
                try {
				var target = evt.target;
				while (target.nodeType != 1) target = target.parentNode;

				if (!(target.tagName != 'SELECT' && target.tagName != 'INPUT' && target.tagName != 'A' && target.parentNode.tagName != 'A' && target.tagName != 'TEXTAREA' && target.className != 'success' && target.parentNode.className != 'ntus' && target.className != 'ntus'&& target.parentNode.className != 't_in' && target.className != 't_in' )) {
				    return;
				}
                    evt.preventDefault(); //阻止触摸时浏览器的缩放、滚动条滚动等
                    var touch = evt.touches[0]; //获取第一个触点
                    var x = Number(touch.pageX); //页面触点X坐标
                    var y = Number(touch.pageY); //页面触点Y坐标
                    if(kg==1){
						var ntop=parseInt($(".scrollbar").css("margin-top").replace("px",""));
						if(startY>y){
						 	$(".scrollbar").css("margin-top",ntop-sc_sd+"px");
						}else{
						 	$(".scrollbar").css("margin-top",ntop+sc_sd+"px");
						}
						
						var sch=$(".scrollbar").height();
						var wch=$(window).height()-fHeight;
						var siww=0-(sch-wch);
						
						if(ntop<siww && sch-wch>0){
							show_Div();
						}
						
						if(sch-wch<0 && ntop<0){
							show_Div();
						}
						
						if(ntop>0){
							hide_Div();
						}
					}
                    startX = x;
                    startY = y;
					
			//	   $("#nyss").html($(".scrollbar").css("margin-top").replace("px","")+":"+sch+":"+wch);
					
                }
                catch (e) {
                //    alert('touchMoveFunc：' + e.message);
                }
            }

            //touchend事件
            function touchEndFunc(evt) {
                try {
				var target = evt.target;
				while (target.nodeType != 1) target = target.parentNode;
				if (!(target.tagName != 'SELECT' && target.tagName != 'INPUT' && target.tagName != 'A' && target.parentNode.tagName != 'A' && target.tagName != 'TEXTAREA' && target.className != 'success' && target.parentNode.className != 'ntus' && target.className != 'ntus'&& target.parentNode.className != 't_in' && target.className != 't_in' )) {
				    return;
				}
                    evt.preventDefault(); //阻止触摸时浏览器的缩放、滚动条滚动等
                 //   var touch = evt.touches[0]; //获取第一个触点
                 //   var x = Number(touch.pageX); //页面触点X坐标
                   // var y = Number(touch.pageY); //页面触点Y坐标
				   
					var sch=$(".scrollbar").height();
					var wch=$(window).height()-fHeight;
 					if(kg==1){
						var ntop=parseInt($(".scrollbar").css("margin-top").replace("px",""));
						if(ntop>0){
						 	$(".scrollbar").animate({ marginTop: 0 + 'px' }, { speed: "fast" });
							hide_Div();
						}
						var siww=0-(sch-wch);
						if(ntop<siww && sch-wch>0){
							$(".scrollbar").animate({ marginTop: siww + 'px' }, { speed: "fast" });
							open_Page();
						}
						
						if(sch-wch<0 && ntop<0){
						 	$(".scrollbar").animate({ marginTop: 0 + 'px' }, { speed: "fast" });
							open_Page();
						}
				   		//$(".fx_btn").val(sch+":"+wch+":"+$(".scrollbar").css("margin-top").replace("px",""));
					}
                    var text = 'TouchEnd事件触发';
                    kg = 0;
                   
                }
                catch (e) {
                   //alert('touchEndFunc：' + e.message);
                }
            }

            //绑定事件
            function bindEvent() {
                window.addEventListener('touchstart', touchSatrtFunc, false);
                window.addEventListener('touchmove', touchMoveFunc, false);
                window.addEventListener('touchend', touchEndFunc, false);
            }

            //判断是否支持触摸事件
            var width = 0;
            var height = 0;
            function isTouchDevice() {
                //document.getElementById("version").innerHTML = navigator.appVersion;
                width = $(window).width();
                height = $(window).height();

                try {
                    document.createEvent("TouchEvent");
                //    alert("支持TouchEvent事件！");
                    bindEvent(); //绑定事件
                }
                catch (e) {
                //    alert("不支持TouchEvent事件！" + e.message);
                }
            }
			
            window.onload = isTouchDevice;
			
function show_FxDiv(){
}