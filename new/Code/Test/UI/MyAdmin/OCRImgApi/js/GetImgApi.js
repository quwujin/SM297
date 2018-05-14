var title_src = $("script").last().attr("src");

//GetImgApi.js?t=201801311459489561:3 js/GetImgApi.js?t=201801311459489561

var UrlFile = title_src.replace("GetImgApi.js", "GetApiResult.ashx").replace("js", "Controller");

var autoCssFile = title_src.replace("GetImgApi.js", "jquery-ui.min.css").replace("js", "css");
var viewerCssFile = title_src.replace("GetImgApi.js", "viewer.css").replace("js", "css");

var ocrCss = title_src.replace("GetImgApi.js", "ocr.css").replace("js", "css");
var globalCss = title_src.replace("GetImgApi.js", "global.css").replace("js", "css");

include_css(autoCssFile);//引入模糊搜索样式
include_css(viewerCssFile);

include_css(ocrCss);//引入 ocr.css
include_css(globalCss);

include_js(title_src.replace("GetImgApi.js", "jquery-ui.js"));
include_js(title_src.replace("GetImgApi.js", "viewer.js"));



var config = {
    opt:"",//图片Id
    imgsign: "",//图片唯一码
    imgurl: "",//图片地址
    returndata: 1, //0 返回匹配成功   1 返回所有匹配成功
    angle: 0, //旋转角度
    ordercode: "",//订单号
    hideduct: "",//隐藏产品序列 123456
    successid: "",//审核通过Id
    failid: "",//审核作废Id
    isShowId: "1",//是否显示审核按钮  0 不显示  1显示
    ordertime: "",//订单时间
    isCloseOcr: false,//是否关闭旋转后立即调用OCR
    cancelReason: [],//作废原因
    orderid: 0,//订单Id

};

function createModal() {
    var dom = `<div class ="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class ="modal-dialog" role="document">
              <div class ="modal-content">
                <div class ="modal-header">
                  <button type="button" class ="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                  <h4 class ="modal-title" id="myModalLabel">提示信息</h4>
                </div>
                <div class ="modal-body">
                  <div class ="form-group">
                    <div class ="modal-title text-center" id="txt_statu"></div>
                  </div>
                </div>
                <div class ="modal-footer">
                  <button type="button" class ="btn btn-default" data-dismiss="modal"><span class ="glyphicon glyphicon-remove" aria-hidden="true"></span>关闭</button>
                </div>
              </div>
            </div>
  </div>`
    $(dom).appendTo('#form1');
}


//初始化
var ImgApi = function (options) {
    if ($('#myModal').length == 0) {
        createModal();
    }
    $('.container').remove();
    
    options = options || {};
    if (options.hasOwnProperty("imgsign") && config.ordercode != options.ordercode) {
        config.imgsign = options.imgsign;
    }
    if (options.hasOwnProperty("imgurl") && config.ordercode != options.ordercode) {
        config.imgurl =options.imgurl;
    }
    if (options.hasOwnProperty("returndata")) {
        config.returndata = options.returndata;
    }

    if (options.hasOwnProperty("ordercode")) {
        config.ordercode = options.ordercode;
    }

    if (options.hasOwnProperty("opt")) {
        config.opt = options.opt;
    }

    if (options.hasOwnProperty("successid")) {
        config.successid = options.successid;
    }
     
    if (options.hasOwnProperty("failid")) {
        config.failid = options.failid;
    }

    if (options.hasOwnProperty("ordertime")) {
        config.ordertime = options.ordertime;
    }

    if (options.hasOwnProperty("cancelReason")) {
        config.cancelReason = options.cancelReason;
    }
    if (options.hasOwnProperty("orderid")) {
        config.orderid = options.orderid;
    }
         
    if (options.hasOwnProperty("angle") && options.angle != 0) {
        config.angle = options.angle;
        rotate();
    }
    else {
        GetApiResult();
    }

}

var Reg = new RegExp("^[-+]?[0-9]+(\.[0-9]+)?$");//验证金额
var RegNum = new RegExp("^[0-9]{1,4}$");//验证数量
var ductinfoid = "";//所有商品Id
var ductmoney = 0;//产品总金额 



//查询小票数据
var GetApiResult = function () {

    $("body").css("overflow", "hidden");

    ductinfoid = "";
    ductmoney = 0;

    AjaxAsync({ 
            imgsign: config.imgsign,
            imgurl: config.imgurl,
            ordercode:config.ordercode,
            GetType:"RequestOCR"
        }).done(function(result)
    {
        if (result.errNum == 0) {
            showImg(result.ResposeData);
        } 
    }); 
   
}



//显示小票图片
var showImg = function (result) {
    var needlenght = result.needData.length;
    var needData = result.needData;
    var StatisticsStore=result.StatisticsStore
    var ProductList = result.ProductList;
    var sameList = result.sameList;
    var container = "<div  class=\"container\"><div class=\"row\" ></div></div>";
    
    var left = "<div class=\"left xp_box\"></div>";

    var right =`<div class="right">
                <div class="ocrheader">
                    <!--四个选项-->
                    <div class="header_option">
                        <div class="h_item active">
                            数据读取
                        </div>
                        <div class="h_item ">
                            产品信息
                        </div>
                        <div class="h_item">
                            源数据
                        </div>
                        <div class="h_item ">
                            异常小票
                        <span class="number">
                            `+sameList.length+`
                        </span>
                        </div>
                    </div>
                    <input type="button" class="btn_close" onclick="RemoveOCR();" />
                </div>


                <!--内容-->
                <div class="content_box">
                    <!--数据读取-->
                    <div class="c_item ">
                        <div class="data_read">
                            <div class="data_sum">
                                <div class="left">
                                    <div class="top">累计金额</div>
                                    <div class ="bottom"> `+ (StatisticsStore != null ? StatisticsStore.StoreMoney : "0") +` </div>
                                </div>
                                <div class="right">
                                    <div class="top">
                                        参与单数
                                    </div>
                                    <ul class="bottom">
                                        <li class ="num_info">昨日: `+(StatisticsStore != null ? StatisticsStore.YesterdayNum : "0")+` </li>
                                        <li class ="num_info">今日: `+(StatisticsStore != null ? StatisticsStore.TodayNum : "0")+` </li>
                                        <li class ="num_info">总数: `+(StatisticsStore != null ? StatisticsStore.TotalNum : "0")+` </li>
                                    </ul>
                                </div>
                            </div>

                            <div class="data_info">
                                <div class="form-control">
                                    <span class="left_text">客户：</span>
                                    <input type="text" class ="user_name" id="txtmark" onclick='showret(\"Customer\");'  value= "`+(needlenght > 0 ? needData[0].Customer :"")+`" />
                                    <input type="button" class=" btn_close fr" />
                                </div>
                                <div class="form-control">
                                    <span class="left_text">门店名：</span>
                                    <input type="text" class ="user_name " id="txtmarkname" onclick='showret(\"StoreName\");' value= "`+(needlenght > 0 ? needData[0].StoreName : "")+ `" />
                                    <input type="button" class=" btn_close fr" />
                                </div>
                                <div class="form-control">
                                    <span class="left_text">门店号：</span>
                                    <input type="text" class ="user_name " id="txtmarknum" onclick='showret(\"StoreNum\");'  value= "`+(needlenght > 0 ? needData[0].StoreNum : "") +`" />
                                    <input type="button" class =" btn_close fr" />
                                </div>
                                <div class="form-control">
                                    <span class="left_text">流水号：</span>
                                    <input type="text" class ="user_name " id="txtmarkcode"  onclick='showret(\"SerialNumber\");' onblur='checkSerialNumber(this,"` + (needlenght > 0 ? needData[0].ductId : 0) + `");' value= "`+ (needlenght > 0 ? needData[0].SerialNumber : 0)  +`" />
                                    <input type="button" class=" btn_close fr" />
                                </div>
                                <div class="form-control">
                                    <span class="left_text">购物时间：</span>
                                    <input type="text" class ="user_name " id="txtmarktime" onclick='showret(\"ShoppingTime\");' value= "`+((needlenght > 0 && needData[0].ShoppingTime.length > 0) ? needData[0].ShoppingTime : config.ordertime)  +`" />
                                    <input type="button" class=" btn_close fr " />
                                </div>
                                <div class="form-control">
                                    <span class="left_text">小票总金额：</span>
                                    <input type="text" class ="user_name " id="txtmarkmoney" onclick='showret(\"MaxMonery\");' value= "`+(needlenght > 0 ? needData[0].MaxMonery : "")+`" />
                                    <input type="button" class=" btn_close fr "/>
                                </div>

                            </div>
                            <div class="btn_box">
                                <input type="button" class ="btn_void fail-bt"  value="审核作废">
                                <input type="button" class ="btn_up" value="修改" onclick="updateProduct(`+(needlenght > 0 ? needData[0].ductId : -1)+`)" />
                                <input type="button" class ="btn_pass success-bt" value="审核通过">
                            </div>
                        </div>

                    </div>
                    <!--产品信息-->
                    <div class="c_item">
                        <div class="product_info">
                            <div class ="shopNameList">

                            </div>
                            <div class="sum_money">
                                产品总金额：0.00
                            </div>
                            <input type="button" class ="btn_add" onclick="productadd('`+ result.ordercode +`');" />
                        </div>

                    </div>
                    <!--源数据-->
                    <div class="c_item">
                        <div class="source_date">
                            <div class="order_key">
                                <div class ="top">
                                   订单号KEY：` +result.pk+`
                                 </div>
                                <div class="bottom">
                                    订单号：`+result.ordercode+`
                                </div>
                            </div>
                            <div class="btn_box">
                                <div class="btn_list clear">
                                    <input type="text" class ="shop_name" id="sel_search_storname" placeholder="请输入门店" />
                                    <input type="text" class ="shop_key" id="storkeyword" placeholder="请输入门店关键字" />
                                    <input type="button" class ="btn_sub" value="提交" onclick='setStorNameKey();'  />
                                </div>
                                <div class="btn_list clear">
                                    <input type="text" class ="shop_name" id="sel_search_product" placeholder="请输入产品" />
                                    <input type="text" class ="shop_key" id="productkeyword" placeholder="请输入产品关键字" />
                                    <input type="button" class ="btn_sub"  value="提交" onclick='setProductNameKey();' />
                                </div>
                            </div>
                            <input type="button" class="btn_x red_bg" value="清除数据" onclick='removeData();' />
                            <div class="shop_info">
                            
                            </div>

                        </div>
                    </div>
                    <!--异常小票-->
                    <div class="c_item">
                        <div class="unnormal_xp">
                            <div class="btn_header">
                               <input type="button" class ="form-control btn_unNormal " value="`+sameList.length+`"/>
                                <div class ="option_box">
                                 

                                </div>

                                <input type="button" class ="btn_normal active"  onclick= "updateSame(`+ needData[0].ductId +`)" value="正常图片" />
                            </div>
                            <div class ="img_list">
                                <img src="" alt="" class ="xp erro-same-img"/>
                           
                            </div>
                        </div>
                    </div>
                </div>
            </div>`;
   
    $(container).appendTo('body');
    
   
    $(left).appendTo('.container .row');
    $(right).appendTo('.container .row');

    if(sameList.length<=0){
      $('.btn_normal').hide();
    }

    $(".fail-bt").click(function () {
        $("#"+config.failid).click();
    })

    $(".success-bt").click(function () {
        $("#"+config.successid).click();
    })

    //默认显示 数据读取
    $('.content_box .c_item').hide().eq(0).show();
    // 四个切换
    $('.header_option .h_item').on('click', function () {
        $('.header_option .h_item').removeClass('active');
        $(this).addClass('active');
        $('.content_box .c_item').hide().eq($(this).index()).show();
    });

    $(".btn_close").click(function () {
        $(this).prev().val("");
    })

    //读取产品信息   商品信息

    if (ProductList.length > 0) {

        for (var i = 0; i < ProductList.length; i++) {
            var id = ProductList[i].Id;
            var num = ProductList[i].Num;
            var monery = ProductList[i].Monery;
            var total = ProductList[i].Total;

            if (Reg.test(num) == false && Reg.test(monery) && total.length > 0)
                num = parseInt(total / monery);
            if (Reg.test(num) == true && Reg.test(monery) == false && total.length > 0)
                monery = parseFloat(total / num).toFixed(2);
            if (Reg.test(num) == true && Reg.test(monery) == true && total.length <= 0)
                total = parseFloat(num * monery).toFixed(2)
            var name_list = `<div class="name_list clear ductinfo` + id + `" editid="` + id + `"  >
                                    <div class="shop_left">
                                        <input type="text" class ="shop_name addductname"  placeholder="请输入商品名称"  value="`+ProductList[i].Commodity +`"  />
                                        <input type="text" class ="shop_num addductnum"  placeholder="请输入数量"   value="` + num + `"  />
                                        <input type="text" class ="shop_price addductmonery"  placeholder="请输入单价"  value="` + monery + `"   />
                                        <input type="text" class ="shop_stock addducttotal"  placeholder="合计"  value="` + total + `"  />
                                    </div>
                                    <div class="shop_right">
                                        <input type="button" class ="btn_delete" value="删除"  onclick='delProductInfo(this,`+ id +`)' />
                                        <input type="button" class ="btn_update" onclick="updateProductInfoByAll()"    value="修改"  />
                                    </div>
                              </div>`;

            $('.shopNameList').append(name_list);


            //计算产品总金额 
            if (total.length != 0) {
                ductmoney = parseFloat(((Reg.test(total) ? parseFloat(total) : 0) + parseFloat(ductmoney))).toFixed(2);

                $('.sum_money').html("产品总金额：" + ductmoney);
            }
        }
        //添加商品信息样式框 
        //AddBorderStyle(ProductList, "ductInfo", multipleW, multipleH);
    }
    var showImg = "<div class=\"showImg_div\"><div class=\"showImg gallery-pic\"></div><a target='_blank' style='color:blue; position:absolute;right:15px;top:10px;' href='" + (config.imgurl.indexOf('_') > 0 ? config.imgurl.substring(0, config.imgurl.indexOf('_')) + config.imgurl.substring(config.imgurl.lastIndexOf("."), config.imgurl.length) : config.imgurl) + "'>查看原图</a></div>";

    $(showImg).appendTo('.xp_box');
     
    var showImgMaxW = $('.container .left').width();
    var image = new Image();
     
    //图片加载完成后执行
    image.onload = function () {

        var ImgW = image.width; //图片原始宽高
        var ImgH = image.height;

        var multipleW = 0; //宽度压缩比例
        var multipleH = 0; //高度压缩比例
         
        //等比例缩小
        if (ImgW > ImgH) {
            imageWidth = showImgMaxW;
            imageHeight = ImgH * (ImgH / ImgW);

            multipleW = imageWidth / ImgW;
            multipleH = imageHeight / ImgH;
        }
        else if (ImgW < ImgH) {
            imageHeight = ImgH;
            imageWidth = showImgMaxW * (ImgW / ImgH);

            multipleW = imageWidth / ImgW;
            multipleH = imageHeight / ImgH;
        }
        else {
            imageWidth = showImgMaxW;
            imageHeight = ImgH;
        }

        $(".showImg").css("width", imageWidth);
        $(".showImg").css("height", imageHeight);
         
   
        $(".showImg").css({"background-image":"url(" + image.src + ")",backgroundSize:"100% 100%"});//小票



        $(".container").show();

        $(".showImg").click(function () {
            $("#" + config.opt).click();
            //$("#J_pg").css("z-index", "999");
        })

        //result.percentage 成功率

        //var success=result.needData; 返回产品数据 result.ProductList;返回商品数据 result.sameList;返回异常图片 result.ProductStyleList;返回产品信息样式
        //productdata(result.needData || [], result.pk, result.ProductList || [], result.sameList || [], result.ProductStyleList || [], result.StatisticsStore, multipleW, multipleH);


        //var success=result.successData; 返回匹配成功数据
        //if (config.returndata == 0)
        //    successdata(result.successData, result.pk, multipleW, multipleH);

        //var success=result.retData; 返回全部数据 result.pk//订单key
        if (config.returndata == 1)
            retdata(result.retData, result.pk, multipleW, multipleH, result.ordercode, result.StorNameAllList || [], result.ProductAllList || []);
    }

    image.src = config.imgurl; 

    autocomplete("#txtmark", 1);//模糊搜索客户
    autocomplete("#txtmarkname", 2);//模糊搜索门店
    autocomplete(".shop_name,.addductname", 3);//模糊搜索商品
      
    //读取异常图片
  
    for (var i = 0; i < sameList.length; i++) {
 
        $('<input type="button" class ="btn_img" value="图片-' + (sameList[i].ImgUrl.substring(sameList[i].ImgUrl.lastIndexOf("/") + 1, sameList[i].ImgUrl.lastIndexOf("/") + 15)) + '" onclick=showSame("'+sameList[i].ImgUrl+'")   />').appendTo('.option_box');
    }
    //onclick = showSame(' + sameList[i].ImgUrl + ')

    $("#" + config.opt).viewer();
    $('.img_list img').viewer();

    $(document).ready(function () {
        /** Coding Here */
    }).keydown(function (e) {
        if (e.which === 27) {
            RemoveOCR();
        }
    });


}
 
//产品信息添加一条 空数据 获取editid 
var productadd = function (OrderCode) {
    //产品信息添加一条

    AjaxAsync({ OrderCode:OrderCode, GetType: "AddCommodity" }).done(function(result){
        
       if (result.errNum == 0) {

            var id = result.DuctId;

            var editid = id;//后端返回
            $('<div class="name_list clear" editid=' + editid + '> ' +
                '<div class="shop_left"> ' +
                '<input type="text" class="shop_name addductname" placeholder="请输入商品名称" /> ' +
                '<input type="text" class="shop_num addductnum" placeholder="请输入数量"  /> ' +
                '<input type="text" class="shop_price addductmonery" placeholder="请输入单价"  /> ' +
                '<input type="text" class="shop_stock addducttotal" value="" placeholder="总价" /> </div>' +
                ' <div class="shop_right"> ' +
                '<input type="button" class="btn_delete" value="删除" onclick="delProductInfo(this,' + editid + ')"  /> ' +
                '<input type="button" class="btn_update" value="修改" onclick="updateProductInfoByAll()" /> </div> </div>').appendTo('.shopNameList');

           autocomplete(".shop_name,.addductname", 3);//模糊搜索商品
       }  

    }); 
             
}



//修改异常产品状态
var updateSame = function (ductid) {

    AjaxAsync({
                uid: ductid, 
                GetType: "EditState"
       }).done(function(result)
    {
        //if (result.errNum == 0) { 
        //    $(".samebg,.same-bt,.erro-same-bg,.same-ok-bt").hide(); 
        //} 
    }); 

      
}



//修改产品
var updateProduct = function (ductid) {

    var txtmark = $("#txtmark").val();
    var txtmarkname = $("#txtmarkname").val();
    var txtmarknum = $("#txtmarknum").val();
    var txtmarkcode = $("#txtmarkcode").val();
    var txtmarktime = $("#txtmarktime").val();
    var txtmarkmoney = $("#txtmarkmoney").val();

     AjaxAsync({
                uid: ductid,
                mark: txtmark,
                markname: txtmarkname,
                marknum: txtmarknum,
                markcode: txtmarkcode,
                marktime: txtmarktime,
                markmoney: txtmarkmoney,
                imgsign: config.imgsign,
                ordercode: config.ordercode,
                GetType: "EditProduct"
      }).done(function(result)
    {
        if (result.errNum == 0&&result.RepeatCode!=null) {
            //重复单号
            console.log(result.RepeatCode);
            //取消弹框关闭
            isHide=false;
        } 
    }); 
     
}



//修改所有商品
var updateProductInfoByAll = function (that) {

    //产品信息所有数据；
    var nameListVal = [];
         
    if (that == null||"") {
        that = window;
    }

    var nameList = $('.name_list');
    var txtname =$(".addductname");
    var txtnum = $(".addductnum");
    var txtmonery = $(".addductmonery");
    var txttotal = $(".addducttotal");

    var TotalAll=0;
     
    nameList.each(function () {
        var nameJson = {};
        //console.log($(this));
        nameJson.id = $(this).attr('editid');
        nameJson.name = $(this).find('.addductname').val();
        nameJson.num = $(this).find('.addductnum').val();
        nameJson.monery = $(this).find('.addductmonery').val();
        nameJson.total = $(this).find('.addducttotal').val();

        if (Reg.test(nameJson.num)==false &&Reg.test(nameJson.monery) && Reg.test(nameJson.total)) {
            nameJson.num = parseInt(nameJson.total / nameJson.monery);
            $(this).find('.addductnum').val(nameJson.num);
        }
        if (Reg.test(nameJson.monery)==false&& Reg.test(nameJson.num) && Reg.test(nameJson.total)) {
            nameJson.monery = parseFloat(nameJson.total / nameJson.num).toFixed(2);
            $(this).find('.addductmonery').val(nameJson.monery);
        }
        if (Reg.test(nameJson.total)==false&& Reg.test(nameJson.num)&& Reg.test(nameJson.monery)) {
            nameJson.total = parseFloat(nameJson.num * nameJson.monery).toFixed(2)
            $(this).find('.addducttotal').val(nameJson.total);
        }

        if (nameJson.total.length != 0){
            TotalAll = parseFloat(((Reg.test(nameJson.total) ? parseFloat(nameJson.total) : 0) + parseFloat(TotalAll))).toFixed(2);
        }

        nameListVal.push(nameJson);
    })

    AjaxAsync({
            DuctInfo:JSON.stringify(nameListVal),
            GetType: "EditAllCommodity"
        }).done(function(result)
    {
        if (result.errNum == "0") {
            ductmoney = TotalAll;
            $('.sum_money').html("产品总金额：" + TotalAll);
                 
        }
    }); 

}
     
//删除商品
var delProductInfo = function (obj,delductinfoid) {
  
   AjaxAsync({uid: delductinfoid,GetType: "DelCommodity"}).done(function(result){
        
       if (result.errNum == 0) {

        var removemoney = $(obj).parent().prev().find('.addducttotal').val();
             
        if (removemoney.length != 0)
            ductmoney = parseFloat((parseFloat(ductmoney) - (parseFloat(removemoney) - 0))).toFixed(2);
                 
        $('.sum_money').html("产品总金额：" + ductmoney);

        $(obj).parent().parent().remove();
                    
       } 

   });
    
        
}

var isHide=true;

//提交请求
var AjaxAsync=function(RequestJson){

    isHide=true;

    $('#txt_statu').html("提交中，请耐心等待。。。");

    if($('#myModal').css("display")=="none"&&RequestJson.GetType!="CheckIsRepeat"){
        $('#myModal').modal({ show:  true });
    } 

    var def = $.Deferred();
     
    $.ajax({
            url: UrlFile,
            dataType: "json",
            async: true,
            data: RequestJson,
            type: "POST",   //请求方式
            beforeSend: function () {
                //请求前的处理
            },
            success: function (result) {
             
                def.resolve(result);

                $('#txt_statu').html(result.errMsg); 
                     
                setTimeout(function () {
                    if(isHide){
                        $('#myModal').modal('hide');
                    }
                }, 1000);    
                    
                //
            },
            complete: function () {
                //请求完成的处理 
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //请求出错处理
                //def.reject(textStatus);
            }
      });

    return def.promise();
}

  

//显示所有匹配成功数据
var retdata = function (data, pk, multipleW, multipleH, ordercode, StorNameAllList, ProductAllList) {

    //$(".retdata").append("<input type='text' value='订单Key：" + pk + "' class='notes btn btn-danger'/>"); 
    //$(".retdata").append("<input type='text' value='订单号：" + ordercode + "' class='notes btn btn-danger'/>");
         
    //var select = "";
    //select += "<div class='sel_storname' ><input type='text' class='form-control ' id='sel_search_storname' placeholder='请输入门店'>";
    //select += "<input type='text' class='form-control ' id='storkeyword' placeholder='请输入门店关键字'>";
    //select += "<input type='button' class='btn btn-success ' id='stor_submit_btn' value='提交' onclick='setStorNameKey();' ></div>";
    //select += "<div class='sel_product'><input type='text' class='form-control ' id='sel_search_product' placeholder='请输入产品'>";
    //select += "<input type='text' class='form-control' id='productkeyword' placeholder='请输入产品关键字'>";
    //select += "<input type='button' class='btn btn-success' id='product_submit_btn' value='提交' onclick='setProductNameKey();' ></div>";

    //$(".retdata").append(select);

    //setstyle(".sel_storname,.sel_product", 0, 43);
    ////setstyle("#storkeyword,#productkeyword", 180, 0, 10, 0, 0);
    //setstyle("#stor_submit_btn,#product_submit_btn", 100, 0, 10, 0, 0);

    autocomplete("#sel_search_storname", 2);//模糊搜索门店
    autocomplete("#sel_search_product", 3);//模糊搜索商品

    for (var i = 0; i < data.length; i++) {
         var o = "<input type='button' value='" + data[i].Word + "' onclick='showret(" + i + ");' class='btn_info'/>";
         $(".shop_info").append(o);
    }

    //添加源信息样式框 
    AddBorderStyle(data, "ret", multipleW, multipleH);
}



var showsuces = function (obj , id) {
    $(".suces,.sucesinfo").hide();
    $(obj).next().show();
    $("#suces" + id).show(); 
}



var showret = function (id) {
    $(".ret").css("display", "none");
    $("#ret" + id).css("display", "block");
    var marginTop = $("#ret" + id).css("marginTop");
    //console.log(marginTop);
    $(".showImg_div").animate({ scrollTop: marginTop }, 500);
}



//显示异常图片
var showSame = function (imgurl) {
    $(".erro-same-img").attr("Src", imgurl);
}



var removeImg = function () {
    $("body").css("overflow", "auto");
    $('.container').remove();
}


//旋转图片
var rotate = function () {

    AjaxAsync({
                imgsign: config.imgsign,
                imgurl: config.imgurl,
                angle: config.angle,
                ordercode: config.ordercode,
                GetType: "RoateImg"
         }).done(function(result)
     {
        
       if (result.errNum == 0) { 
            config.imgsign = result.NewImgsign;
            config.imgurl = result.NewImgurl;
                 
            $("#" + config.opt).attr("src", result.NewImgurl);
                  
            removeData(); 
                      
       } 
    });
     
}



//清除所有数据
var removeData = function () {

    AjaxAsync({
          ordercode: config.ordercode,
          GetType: "ClearAll"
      }).done(function(result)
   {
       if (result.errNum == 0) {
            
            removeImg();

            if (config.isCloseOcr) { return;}

            setTimeout(function () {
                GetApiResult();
            }, 2000);
       } 
   });   

}



//添加位置显示框
var AddBorderStyle = function (stylelist, type, multipleW, multipleH) {
     
    for (var Style = 0; Style < stylelist.length; Style++) {
         
        var height = stylelist[Style].height;
        var top = stylelist[Style].mtop;
        var left = stylelist[Style].mleft;
        var width = stylelist[Style].width;

        if (height != 0 || top != 0 || left != 0 || width != 0) {

            if (multipleH != 0) {
                height = height * multipleH;
                top = top * multipleH;
            }

            if (multipleW != 0) {
                width = width * multipleW;
                left = left * multipleW;
            }

            var border = "<div id='ret" + (type == "duct" ? stylelist[Style].StyleType : type == "ductInfo" ? type + Style : Style) + "' class='ret' style=\"";
            border += "width:" + width + "px;margin-top:" + top + "px;";
            border += "margin-left:" + left + "px;height:" + height + "px;\"></div>";

            $(".showImg").append(border);
        }
    }
}



//引入css文件
function include_css(path) {
    var fileref = document.createElement("link");
    fileref.rel = "stylesheet";
    fileref.type = "text/css";
    fileref.href = path;
    document.getElementsByTagName("HEAD")[0].appendChild(fileref);
}



//引入js文件
function include_js(path) {
    var filescript = document.createElement("script");
    filescript.src = path;
    document.getElementsByTagName("HEAD")[0].appendChild(filescript);
}



//autocomplete插件
var cache = {};

var autocomplete = function (id,type) {
    
    //缓存
    $(id).autocomplete({
        source: function (request, response) {
            var term = request.term; 
            if (term in cache) {
                response($.map(cache[term], function (item) {
                    return {
                        //dbId: item.dbid,
                        //jdbcUrl: item.jdbcUrl,
                        //ip: item.ip,
                        //port: item.port,
                        //sch: item.sch,
                        //username: item.username,
                        //password: item.password,
                        value: item.Name
                    }
                }));
                return;
            }
            $.ajax({
                url: UrlFile,
                dataType: "json",
                data: {
                    searchItem: request.term,//取到模糊搜索的词
                    searchType: type,//搜索类型
                    GetType: "SearchItemData"
                },
                success: function (data) {
                    var result=$.parseJSON(data.SearchData);
                    cache[term + type] = result;
                    response($.map(result, function (item) {
                        return {
                            //dbId: item.dbid,
                            //jdbcUrl: item.jdbcUrl,
                            //ip: item.ip,
                            //port: item.port,
                            //sch: item.sch,
                            //username: item.username,
                            //password: item.password,
                            value: item.Name
                        }
                    }));
                }
            });
        },
        minLength: 1,
        autoFocus: true,
        select: function (event, ui) {
            //$("#dbInforDdId").val(ui.item.dbId);
            //$("#dbInforDdjdbcUrl").val(ui.item.jdbcUrl);
            //$("#dbInforIp").val(ui.item.ip);
            //$("#dbInforPort").val(ui.item.port);
            //$("#dbInforSch").val(ui.item.sch);
            //$("#dbInforUserName").val(ui.item.username);
            //$("#dbInforPassword").val(ui.item.password);


        }
    });
}



//检查流水号是否重复
var checkSerialNumber = function (Own, ductid) {
    var SerialNumber = $(Own).val();

    if(SerialNumber.length>0){
        AjaxAsync({
                    ductid: ductid,
                    serialNumber: SerialNumber, 
                    GetType: "CheckIsRepeat"
                }).done(function(result)
            {
                if (result.errNum == 0) {
                    //重复单号
                    console.log(result.RepeatCode);

                    //取消弹框关闭
                    isHide=false;

                    $('#myModal').modal({ show:  true });
                } 
            });   
     }
}



//关闭旋转自动识别
var CloseOcr = function () 
{
    config.isCloseOcr = true;

    $('#txt_statu').html("关闭成功");
    $('#myModal').modal({ show: true });

    setTimeout(function () { 
        $('#myModal').modal('hide');
    }, 500);
}



//调用后台作废方法
var Reason = function (obj) 
{
    var index = obj.selectedIndex;

    if (index == 0) { return;}

    index = (index == 0 || index == 1) ? 0 : index - 1;

    send(config.orderid, 'zf', index, config.cancelReason[index]);
     
}



//设置门店关键字
var setStorNameKey = function () 
{ 
    //var storname = $('#sel_search_storname option:selected').val();

    var storname = $('#sel_search_storname').val();
    
    var keyword = $("#storkeyword").val();
     
    if (typeof (storname) == "undefined" || storname.length == 0||storname=="请选择门店") {
        $('#txt_statu').html("请选择门店");
        return;
    }
    if (typeof (keyword) == "undefined" || keyword.length == 0) {
        $('#txt_statu').html("请填写门店关键字");
        return;
    }
     
     AjaxAsync({
            storname: storname,
            keyword: keyword,
            GetType: "SetStoreKeyword"
     }).done(function(result)
    {
       if (result.errNum == 0) {
           
       } 
    });   
     
    
}



//设置产品关键字
var setProductNameKey = function () 
{

    //var productname = $('#sel_search_product option:selected').val();
    var productname = $('#sel_search_product').val();
     
    var keyword = $("#productkeyword").val();
         
    if (typeof (productname) == "undefined" || productname.length == 0||productname == "请选择产品") {
        $('#txt_statu').html("请选择产品");
        return;
    }
    if (typeof (keyword) == "undefined" || keyword.length == 0) {
        $('#txt_statu').html("请填写产品关键字");
        return;
    }

     AjaxAsync({
            productname: productname,
            keyword: keyword,
            GetType: "SetProductKeyword"
     }).done(function(result)
    {
       if (result.errNum == 0) {
           
       } 
    });   

}



//删除OCR
var RemoveOCR= function(){
    $("body").css("overflow", "auto");
    $('.container').remove();
}