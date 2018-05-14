<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NearStor.aspx.cs" Inherits="NearStor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
<meta name="apple-mobile-web-app-capable" content="yes"/>
<meta name="apple-mobile-web-app-status-bar-style" content="black"/>
<meta name="format-detection" content="telephone=no"/>
<meta name="aplus-terminal" content="1"/>
    <%=WebFramework.GeneralMethodBase.LoadJs() %>
    <script src="../js/SelectForMoblie.js"></script>
    <link href="../css/Select.css" rel="stylesheet" />
    <script src="http://3gimg.qq.com/lightmap/components/geolocation/geolocation.min.js"></script>
    <script src="http://api.map.baidu.com/api?v=2.0&ak=8VnmdAKULKBah1wQ32vewHNT"></script>
    <script> 

        GetLocal();

        function GetLocal() {
            var geolocation = new qq.maps.Geolocation("OB4BZ-D4W3U-B7VVO-4PJWW-6TKDJ-WPB77", "myapp");
            if (geolocation) {
                var options = { timeout: 8000 };
                geolocation.getLocation(handleSuccess, handleError, options);
            } else {
                alert("定位尚未加载");
            }
        } 

        function handleError(error) {
            console.log(error);
        }
         
        function handleSuccess(position) {

            if (position.province.length > 0 && position.city.length > 0) {

                $(".prov").val(position.province);
                $(".city").val(position.city);

                //转换为百度经纬度
                $.ajax({
                    url: "http://api.map.baidu.com/geoconv/v1/",
                    dataType: "jsonp",   //返回格式为json
                    async: true, //请求是否异步，默认为异步，这也是ajax重要特性
                    data: { "coords": position.lng + "," + position.lat, "from": "1", "to": "5", "ak": "537af81275e3b62d0e8b5a860b745c9d" },//参数值coords:纬度，精度
                    type: "GET",   //请求方式 
                    success: function (req) {
                        //请求成功时处理    
                        //alert(req.result[0].y + "," + req.result[0].x); //31.23859226444,121.38413130766

                        ShowGetcoodingImg(req.result[0].y,req.result[0].x);
                    },
                    complete: function () {
                        //请求完成的处理
                    },
                    error: function () {
                        //请求出错处理
                    }
                });
            }

        }

        function ShowGetcoodingImg(lat, lng) {

            //console.log(lat + "," + lng);

            //alert(lat + "," + lng); //31.238433057381,121.38435227702

            //根据经纬度显示地址静态图
            $.post("getCity.ashx", { lat: lat, lng: lng }, function (xml) {

                var result = $.parseJSON(xml);


                var listStr = "";

                if (result.status == 0 && result.msg.length > 0) {

                    var Baidu = "http://api.map.baidu.com/staticimage/v2?ak=537af81275e3b62d0e8b5a860b745c9d&width=500&height=500&zoom=16";

                    var markers = [];
                    for (var i = 0 ; i < result.msg.length; i++) {

                        var label = "&labels=" + result.msg[i].Lng + "," + result.msg[i].Lat;
                        var labelStyle = "&labelStyles=" + result.msg[i].Channel + ",1,14,0xffffff,0x000fff,1";
                        var center = "&center=" + result.msg[i].Lng + "," + result.msg[i].Lat;
                        listStr = listStr + "<li>" + result.msg[i].Address + "</li>";

                        $("#BaiduImg" + i).attr("Src", Baidu + center + label + labelStyle);

                        markers[i] = { content: result.msg[i].Address, title: result.msg[i].Channel, imageOffset: { width: -106, height: -18 }, position: { lat: result.msg[i].Lat, lng: result.msg[i].Lng } };
                    }

                    initMap(markers);

                    $(".stor-ul-bg").html(listStr);

                    $(".stor-ul-bg li").click(function () {

                        var addrs=$(this).html();
                        
                        for (var i = 0; i < markers.length; i++) {
                            if (markers[i].content == addrs) {
                                markersNum = i;
                                break;
                            }
                        }
                        setCenten(markersNum, markers);
                        $(".nera-map").hide();
                        $(".show-map").show();

                        //$(".stor-ul-bg").hide();
                    })
                     
                } else {
                    $(".stor-ul-bg").html("<li>该地区暂无门店</li>");
                }

            });
        }
       
        $(function () {

            //ShowGetcoodingImg(31.238433057381, 121.38435227702);
             
            var w = $(window).width();
            var h = $(window).height();
               
            $(".shop").click(function () {
                if ($(this).val() == "点我查看您附近的门店地址") {
                    $(this).val("点我隐藏地址");
                    $(".stor-ul-bg").show();
                } else {
                    $(this).val("点我查看您附近的门店地址");
                    $(".stor-ul-bg").hide();
                }
            })

            $(".btn-back").click(function () {
                $(".nera-map").show();
                $(".show-map").hide();
            })

            $(".map-bt").click(function () {
                $(".nera-map").hide();
                $(".show-map").show();
            })
             

            SetStyle(".mapbg", 551, 551, 54, 86);
            SetStyle(".dt", 530, 452, 10, 10);
            
        })

    </script>
    <style>  
         .Img-div{width:4.6rem;height:1.35rem;display:block;float:left;margin-left:1rem;margin-top:0.3rem;}
         .Img-div .imgs{width:1.35rem;height:1.35rem;display:block;float:left;margin-left:5px;}
         #large-img{position:absolute;z-index:10;width:500px;height:500px;top:0px;left:0px; display:none;}
         .btn-position{display:block;float:left;}

         .show-map{display:none;}
         .mapbg{background:#ffc7db;border-radius:10px; }
         .map-bt{width:2.57rem;height:0.8rem;display:block;margin-left:1.96rem;margin-top:0.34rem;background-image:url(images/btn-map.png);background-size:100% 100%;}
         .btn-back{width:2.57rem;height:0.8rem;display:block;margin-left:1.96rem;margin-top:0.34rem;background-image:url(images/btn-near-back.png);background-size:100% 100%;}

         .stor-ul-bg{position:fixed;z-index:10;display:none;}
         .stor-ul-bg li{width:3.35rem;height:0.59rem;line-height:20px;text-align:center;overflow:hidden;background:#fff;border-bottom:1px solid;border-radius:0px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
       
        <div id="sbox"><asp:Label ID="lbErr" runat="server" Text="" ></asp:Label></div> 

          <div class="container bg2">

            <img src="#"  id="large-img"/>

            <img src="images/logo.png" alt="" class="logo"/>

            <div class="nera-map" >
                <img src="images/near-shop-t.png" alt="" class="near-shop-t"/>
                <div class="position-shop">
                    <input type="button" class="prov" value="省" />
                    <input type="button" class="city" value="市"  />
                    <input type="button" class="shop" value="点我查看您附近的门店地址"  />
                    <ul class="stor-ul-bg "> 
                        <%--<li>该地区暂无门店</li>--%>
                    </ul>  
                </div>

                <input type="button" onclick="window.history.go(-1);" class="btn-position"/>
                <input type="button"  class="map-bt"/>
            </div>


            <div class="show-map" >
                <div class="mapbg">
                    <div class="dt" style="border:#ccc solid 1px;font-size:12px;float:left;" id="map"></div>
                    <span style="display:block;text-align:center;"> 以上是距离您最近的门店信息<br />欢迎前去购买</span>
                </div>
                <input type="button"  class="btn-back" />
            </div>
               

            <div class="info-box">
                <div class="dali">线下大礼享不停</div>
                <div class="info fr">活动细则</div>
            </div>
        </div>
          
    </form>
    <script type="text/javascript">
        var markersNum = 0;

        //创建和初始化地图函数：
        function initMap(markers) {
            createMap(markers);//创建地图
            setCenten(markersNum, markers);
            setMapEvent();//设置地图事件
            addMapControl();//向地图添加控件
            addMapOverlay(markers);//向地图添加覆盖物
        }
        function createMap(markers) {
            map = new BMap.Map("map");  
        }
        function setCenten(num, markers) {
            //console.log(markers[markersNum].position.lng);
            map.centerAndZoom(new BMap.Point(markers[markersNum].position.lng, markers[markersNum].position.lat), 15);
            map.panBy(205, 165);//居中
        }
        function setMapEvent() {
            map.enableScrollWheelZoom();
            map.enableKeyboard();
            map.enableDragging();
            map.enableDoubleClickZoom()
        }
        function addClickHandler(target, window) {
            target.addEventListener("click", function () {
                target.openInfoWindow(window);
            });
        }
        function addMapOverlay(markers) {
           
            for (var index = 0; index < markers.length; index++) {
                var point = new BMap.Point(markers[index].position.lng, markers[index].position.lat);
                var marker = new BMap.Marker(point, {
                    icon: new BMap.Icon("http://api.map.baidu.com/lbsapi/createmap/images/icon.png", new BMap.Size(130, 25), {
                        imageOffset: new BMap.Size(markers[index].imageOffset.width, markers[index].imageOffset.height)
                    })
                });
                var label = new BMap.Label(markers[index].title, { offset: new BMap.Size(30, 5) });
                var opts = {
                    width: 200,
                    title: markers[index].title,
                    enableMessage: false
                };
                var infoWindow = new BMap.InfoWindow(markers[index].content, opts);
                marker.setLabel(label);
                addClickHandler(marker, infoWindow);
                map.addOverlay(marker);
            };
        }
        //向地图添加控件
        function addMapControl() {
            var navControl = new BMap.NavigationControl({ anchor: BMAP_ANCHOR_BOTTOM_RIGHT, type: BMAP_NAVIGATION_CONTROL_LARGE });
            map.addControl(navControl);
        }
        var map;
        //initMap();
  </script>
</body>
</html>
