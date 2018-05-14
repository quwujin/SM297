$(function () {
    
    var w = $(window).width();
    var h = $(window).height();
    var drop_model = function (obj,options)
    {
        var config = {
            select:"",
            html: ' <li><input type="button" class="sel_btn" value="无数据" /></li>' 
        };
         
        config = $.extend({}, config, options); //重新组合数据，有相同的变量则后面的参数替换前面的值，没有相同变量则增加

        $("#" + config.select + "success").remove();

        var drp_html = '<div id="' + config.select + 'success" class="' + config.select + 'success" style="width: 100%; height: auto; background: rgba(0,0,0,0.8); position: absolute; display:none; z-index: 999; height: auto; ">';
        drp_html += '<div class="sel">';
        drp_html += '<div class="title-bg"><input type="button" class="title-bt" value="请选择" /><span class="dot-bottom"></span></div>';
        drp_html += '<ul class="sel_pros">';
        drp_html += config.html;
        
        drp_html += '</ul>';
        drp_html += '<ul class="sel_city">';
        drp_html += '</ul>';
        drp_html += '</div>';
        drp_html += '</div>';
        $("body").prepend(drp_html);
        $("#" + config.select + "success").css("height", h + "px");
        $("#" + config.select + "success").bind("click",function () {
            $(this).fadeOut(1000);
        });
        $(obj).bind("click",function () {
            $("#" + config.select + "success").css("display", "block");
            $(".sel_pros").css("display", "block");
            $(".sel").fadeIn(1000);
        });
        return obj;
    }
    //绑定li里的button点击事件
    var drop_itemBindClick = function (obj, options) {
        var _config = $.extend({}, { html: "",select:"" }, options);
        $("#" + _config.select + "success").find(".sel").find("input[type=button]").bind("click", function () {
            var childObj = this;
            $(".sel").fadeOut(500, function () {
                var drp_val = $(childObj).val();
                if (drp_val == "请选择")
                    drp_val = "";
                
                $(obj).val(drp_val);
                $(obj).text(drp_val);
                $("#hd" + _config.select).val(drp_val);
                $("#" + _config.select + "success").fadeOut(1000);


                if (_config.select == "tbProv") {
                   
                    $.post("getBaseCity.ashx", {
                        ProvId: $(childObj).attr("data"),
                        GetType: "Prov"
                    }, function (data) {
                        if (data != "-1") {
                            $("#tbCity,#tbDist").val("");
                            $("#tbCity").dropListBind({ html: data, select: "tbCity", color: "blue" });
                        }
                    })
                }
                if (_config.select == "tbCity") {
                    
                    $.post("getBaseCity.ashx", {
                        CityId: $(childObj).attr("data"),
                        GetType: "City"
                    }, function (data) {
                        if (data != "-1") {
                            $("#tbDist").val("");
                            $("#tbDist").dropListBind({ html: data, select: "tbDist", color: "blue" });
                        }
                    })
                }
            });
        });
    }
   
    
    $.fn.dropListBind = function (options) //添加新方法 $("#..").方法名即可调用
    {
        var obj = this;
        drop_model(obj, options);
        drop_itemBindClick(obj,options);
        return obj;
    }
});

var qiuduiHtml = '<li><input type="button" class="sel_btn" value="25岁以内" /></li>';
qiuduiHtml += '<li><input type="button" class="sel_btn" value="25-35岁" /></li>';
qiuduiHtml += '<li><input type="button" class="sel_btn" value="35-45岁" /></li>';
qiuduiHtml += '<li><input type="button" class="sel_btn" value="45岁以上" /></li>';
//qiuduiHtml += '<li><input type="button" class="sel_btn" value="火箭" /></li>';
//qiuduiHtml += '<li><input type="button" class="sel_btn" value="步行者" /></li>';
//qiuduiHtml += '<li><input type="button" class="sel_btn" value="湖人" /></li>';
//qiuduiHtml += '<li><input type="button" class="sel_btn" value="热火" /></li>';
//qiuduiHtml += '<li><input type="button" class="sel_btn" value="尼克斯" /></li>';
//qiuduiHtml += '<li><input type="button" class="sel_btn" value="雷霆" /></li>';
//qiuduiHtml += '<li><input type="button" class="sel_btn" value="76人" /></li>';
//qiuduiHtml += '<li><input type="button" class="sel_btn" value="国王" /></li>';
//qiuduiHtml += '<li><input type="button" class="sel_btn" value="马刺" /></li>';

var chimaHtml = '<li><input type="button" class="sel_btn" value="3个月内" /></li>';
chimaHtml += '<li><input type="button" class="sel_btn" value="3-6个月" /></li>';
chimaHtml += '<li><input type="button" class="sel_btn" value="半年以上" /></li>';
//chimaHtml += '<li><input type="button" class="sel_btn" value="XL" /></li>';
//chimaHtml += '<li><input type="button" class="sel_btn" value="XXL" /></li>';
