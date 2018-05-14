<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadFile.aspx.cs" Inherits="UploadFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server"><meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=0" />
<meta name="apple-mobile-web-app-capable" content="yes"/>
<meta name="apple-mobile-web-app-status-bar-style" content="black"/>
<meta name="format-detection" content="telephone=no"/>
<meta name="aplus-terminal" content="1"/> 
    <%=WebFramework.GeneralMethodBase.LoadJs() %>
    <script src="../js/jQueryRotate.2.2.js"></script>
    <script src="../js/jquery.ui.draggable.js"></script>  
    <script src="../js/jquery.easing.min.js"></script>
    <script src="../js/jquery.ui.widget.js"></script>
    <script src="../js/jquery.fileupload.js"></script>
    <script>
        var IsFile = false;

        $(function () {
            

            //上传图片
            $("#hpfb").fileupload({
                autoUpload: true,//是否自动上传
                url: '<%=WebFramework.GeneralMethodBase.GetUploadImgURL%>',//上传地址 
                send: function (e, data) { showbox(2, '正在上传中...'); },
                progressall: function (e, data) {//设置上传进度事件的回调函数  
                    var progress = parseInt(data.loaded / data.total * 100, 10);
                    $("#lbErr").html("正在上传中..." + progress + '%');
                },
                done: function (e, result) { //设置文件上传完毕事件的回调函数 
                    console.log(result.result);
                    return;
                    if (result.result.IsSuccess && result.result.ResultData.list != null && result.result.ResultData.list.length > 0) {
                        $.post(
                            "Servicefile.ashx",
                            {
                                FileName: result.result.ResultData.list[0].ImgUrl,
                                FileHash: result.result.ResultData.list[0].ImgSign,
                                OriginFileName: result.result.ResultData.list[0].OriginImgUrl,
                            },
                            function (xml) {
                                if (xml == "0") {
                                    IsFile = true;

                                    $(".no").attr("src", result.result.ResultData.list[0].ImgUrl);
                                    showbox(1, "上传成功...");
                                } else {
                                    showbox(1, "上传失败，请重新上传...");
                                }
                            })
                    }else {
                        showbox(1, "上传失败，请重新上传...");
                    }
                }

            });
              
        }) 

        function btnTjClick() {

            var Mobile = $("#tbMob").val();
            var files = $("#Hffile").val();
             
            if (checkStr(Mobile, 0) == false) {
                showbox(1, '请填写正确手机号！');
                return false;
            }

            if (IsFile == false) {
                showbox(1, '请上传小票！');
                return false;
            }
             
            showbox(2, '正在提交中，请等待……');
            $("#bt_tj").attr("disabled", "true");

            $.post("../Controller/ApiController.ashx", {
                mob: Mobile,
                GetResult: "UploadFile"
            }, function (data, status) {
                $("#sbox").fadeOut(200, function () {
                    if (status == "success") {
                        var result = $.parseJSON(data);
                        if (result.Success == false) {
                            showbox(1, result.ErrMessage);
                            $("#bt_tj").removeAttr("disabled");
                        } else {
                            window.location.href = 'UploadAdds.aspx';
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

        <asp:TextBox ID="tbMob" runat="server" MaxLength="11" CssClass="block bnone" placeholder="输入手机号" ></asp:TextBox>
        <img src="images/no.png" class="no" />
        <input type="file" id="hpfb" name="hpfb"  accept="image/*" capture="camera" />
        <input type="button" onclick="btnTjClick();" value="" id="bt_tj"/> 


    </form>
</body>
</html>
