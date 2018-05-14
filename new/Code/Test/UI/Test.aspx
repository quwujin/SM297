<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title> 
    <script src="js/jquery-1.9.1.js"></script>
    <script src="js/trackingSystemCountly/trackingsystem.js"></script>
    <script>

        Countly.openid = "ovznPs02JfdRZSmkrsgjTzpPf2hw";

        pushClevents("testClick", "click", "#testbtn", {
            inputVal:document.getElementById("testVal").value
        });
         
    </script>
</head>
<body>
    <form id="form1" runat="server">  
        <input type="text" id="testVal" />
        <input type="button" id="testbtn" value="点击测试" />
    </form>
</body>
</html>
