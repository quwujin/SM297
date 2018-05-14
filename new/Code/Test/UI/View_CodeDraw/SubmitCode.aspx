<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubmitCode.aspx.cs" Inherits="SubmitCode" %>

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
             
        })
         
        function btnTjClick() {

            var MobBile = $("#tbMob").val();
            var Code = $("#tbCode").val();
  
            if (checkStr(MobBile,0) == false) {
                showbox(1, '请填写正确手机号！');
                return false;
            } 
            if (checkStr(Code, 12) == false) {
                showbox(1, '请填写正确确认码！');
                return false;
            }

            showbox(2, '正在提交中，请等待……');
            $("#bt_tj").attr("disabled", "true");
           
            $.post("../Controller/ApiController.ashx", {
                mob: MobBile,
                code: Code,
                GetResult: "GetCode"
            }, function (data, status) { 
                $("#sbox").fadeOut(200, function () {
                    if (status == "success") { 
                        var result = $.parseJSON(data);
                        if (result.Success == false) {
                            showbox(1, result.ErrMessage);
                            $("#bt_tj").removeAttr("disabled");
                        } else {
                            
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
   
        <asp:TextBox ID="tbMob" runat="server" MaxLength="11" Text=""></asp:TextBox>
        <asp:TextBox ID="tbCode" runat="server" MaxLength="13" Text=""></asp:TextBox>
        <input type="button" onclick="btnTjClick();" id="bt_tj" class="btn" value="兑换"  /> 

    </form>
</body>
</html>
