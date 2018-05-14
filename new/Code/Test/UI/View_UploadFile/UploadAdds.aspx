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
    <script>
        $(function () {
            var w = $(window).width();
            var h = $(window).height();

            SetStyle(".mob-bg", 471, 337, 83, 0, 30);
            SetStyle("#tbMob", 356, 46, 83, 146, 30);
            SetStyle(".no", 120, 100, 50, 30, 30);
            SetStyle("#bt_tj", 204, 70, 215, 20, 30);


            $('#tbAdds').bind('input propertychange', function () {
                var txt = $('#tbAdds').val();
                if (txt.length >= 150) { $(this).val(txt.substr(0, 150)); return; }
            });

            $('#tbName').bind('input propertychange', function () {
                var txt = $('#tbName').val();
                if (txt.length >= 12) { $(this).val(txt.substr(0, 12)); return; }
            });

        });

        function btnTjClick() {

            var name = $("#tbName").val();
            var adds = $("#tbAdds").val();

            if (checkStr(name, 5) == false) {
                showbox(1, '请填写正确姓名！');
                return false;
            }
            if (checkStr(adds, 11) == false) {
                showbox(1, '请填写正确地址！');
                return false;
            }

            showbox(2, '正在提交中，请等待……');
            $("#bt_tj").attr("disabled", "true");

            $.post("../Controller/ApiController.ashx", {
                name: name,
                adds: adds,
                GetResult: "UploadAdds"
            }, function (data, status) {
                $("#sbox").fadeOut(200, function () {
                    if (status == "success") {
                        var result = $.parseJSON(data);
                        if (result.Success == false) {
                            showbox(1, result.ErrMessage);
                            $("#bt_tj").removeAttr("disabled");
                        } else {
                            $("ok-bg").show();
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
         
            <asp:TextBox ID="tbName" runat="server" MaxLength="12" placeholder="输入姓名" ></asp:TextBox>
            <asp:TextBox ID="tbAdds" runat="server" placeholder="输入地址" ></asp:TextBox>
            <input type="button" onclick="btnTjClick();" value="" id="bt_tj"/>  


    </form>
</body>
</html>
