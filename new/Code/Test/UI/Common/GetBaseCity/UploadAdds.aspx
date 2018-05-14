<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadAdds.aspx.cs" Inherits="UploadAdds" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
<meta name="apple-mobile-web-app-capable" content="yes"/>
<meta name="apple-mobile-web-app-status-bar-style" content="black"/>
<meta name="format-detection" content="telephone=no"/>
<meta name="aplus-terminal" content="1"/>
    <%=WebFramework.GeneralMethodBase.LoadJs() %> 
    <link href="../css/Select.css" rel="stylesheet" />
    <script src="../js/SelectForMoblie.js"></script>
    <script>
        $(function () {
            var w = $(window).width();
            var h = $(window).height();

             
            $('#tbAdds').bind('input propertychange', function () {
                var txt = $('#tbAdds').val();
                if (txt.length >= 150) { $(this).val(txt.substr(0, 150)); return; }
            });

            $('#tbName').bind('input propertychange', function () {
                var txt = $('#tbName').val();
                if (txt.length >= 12) { $(this).val(txt.substr(0, 12)); return; }
            });

            //if($("#tbCity").val().length>0){
                
            //    $.post("getBaseCity.ashx", {
            //        GetType: "Dist",
            //        CityName:$("#tbCity").val()
            //    },function(data){
            //        if(data!="-1"){
            //            $("#tbDist").dropListBind({ html: data, select: "tbDist", color: "blue" });
            //        }
            //    }) 
            //}

            $("#tbProv").dropListBind({ html:'<%=GetProv()%>', select: "tbProv", color: "blue" });
        });

        function btnTjClick() {

            var name = $("#tbName").val();
            var prov = $("#tbProv").val();
            var city = $("#tbCity").val();
            var dist = $("#tbDist").val();
            var adds = $("#tbAdds").val();
            var mob = $("#tbMob").val();

            if (checkStr(name, 5) == false) {
                showbox(1, '请填写正确姓名！');
                return false;
            }
            if (checkStr(prov, 11) == false) {
                showbox(1, '请选择省！');
                return false;
            }
            if (checkStr(city, 11) == false) {
                showbox(1, '请选择市！');
                return false;
            }
            if (checkStr(dist, 11) == false) {
                showbox(1, '请选择区！');
                return false;
            }
            if (checkStr(adds, 11) == false) {
                showbox(1, '请填写正确地址！');
                return false;
            }
            if (checkStr(mob, 0) == false) {
                showbox(1, '请填写正确手机号！');
                return false;
            }

            showbox(2, '正在提交中，请等待……');
            $("#bt_tj").attr("disabled", "true");

            $.post("../Controller/ApiController.ashx", {
                name: name,
                prov: prov,
                city: city,
                dist: dist,
                adds: adds,
                mob : mob,
                GetResult: "UploadAddress"
            }, function (data, status) {
                $("#sbox").fadeOut(200, function () {
                    if (status == "success") {
                        var result = $.parseJSON(data);
                        if (result.status == "false") { 
                            window.location.href = "ErrorCode.aspx?erro=" + result.msg;
                        } else {
                            $('.tj-ok').show();
                        }
                         
                    }
                });
            });

        }

         
    </script>
    
</head>
<body>
    <form id="form1" runat="server">

        <div id="sbox"><asp:Label ID="lbErr" runat="server" Text="" ></asp:Label></div> 

        <div class="container bg2  ">
            <img src="../images/logo.png" alt="" class="logo position fl"/>
            <img src="../images/prize-info.png" alt="" class="prize-info fr"/>
            <div class="address-box fl">
                <asp:TextBox ID="tbName" runat="server" class="add-name" MaxLength="12" placeholder="收件人姓名" ></asp:TextBox> 
                <div class="prov-box">
                    <asp:TextBox ID="tbProv" class="prov" ReadOnly="true" runat="server" placeholder="省" ></asp:TextBox> 
                    <asp:TextBox ID="tbCity" class="city" ReadOnly="true" runat="server" placeholder="市"></asp:TextBox> 
                    <asp:TextBox ID="tbDist" class="dist" ReadOnly="true" runat="server" placeholder="区" ></asp:TextBox> 
                </div>
                <asp:TextBox ID="tbAdds" runat="server" class="address" placeholder="收件人街道地址" ></asp:TextBox>
                <asp:TextBox ID="tbMob" runat="server" class="add-phone" placeholder="收件人手机号" maxlength="11"></asp:TextBox>
                <input type="button" onclick="btnTjClick();" value=""  class="btn-address" id="bt_tj"/> 
            </div> 
        </div>
         
    </form>
</body>
</html>
