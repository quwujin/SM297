<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Top.aspx.cs" Inherits="inc_Top" %>

<!DOCTYPE html><html>
<head runat="server">
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../js/Fun.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 51%;
        }
    </style>
</head>

<body style="margin:0; background:#fff;">
    <form id="form1" runat="server">
        
    <script>

        $(function () {
            var list = $('.t');
            list.click(function () {
                var links = $(this).attr('href');
                list.removeClass('active');
                $(this).addClass('active');
               
                parent.window.frames['main'].location.href = links;
                return false;
            });
        });
    </script>
    <table width="100%" height="62" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td bgcolor="#014051"><div align="left">
      <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="5%"><div align="center"><img src="../img/user.png" width="50" height="50" /></div></td>
          <td style="font-size:14px; color:#fff;" class="auto-style1">
		 
		 </td>
          <td width="17%">
		  
		  <a href="../User/UpdatePwd.aspx" target="main"><img src="../img/key.png" border="0" style="border:0" /></a>
		  
		   <a href="../default/main.aspx" target="main"><img src="../img/desk.png" style="border:0" /></a>
		  <a href="../../login.aspx" target="_blank"> <img src="../img/home.png" style="border:0" /></a>
		  
		  <a onclick="return loginout();" href="../User/LoginOut.aspx"><img src="../img/exit.png" style="border:0" /></a></td>
        </tr>
      </table>
    </div>
    </td>
  </tr>
</table>
<table width="100%" height="49" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td height="49" bgcolor="#014051"><table width="100%" height="32" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="974" height="26" valign="top">
        
        <span class="menu" runat="server" id="menu">
		
		</span>        </td>
        </tr>
    </table></td>
  </tr>
</table>

    </form>
</body>
</html>
