<%@ Page Language="C#" AutoEventWireup="true" CodeFile="left.aspx.cs" Inherits="MyAdmin_Default_left" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
	    <style>
        body {
            margin: 0px;
            font-size: 12px;
			color:#fff;
			background:#03579F;
        
        }
		
		.big{ color:#fff; text-align:center;  margin-bottom:5px; margin:10px auto; }
		.big a{ color:#fff; text-decoration:none; text-align:center;  font-size:12px; font-weight:bold; margin:5px auto; background:url(../img/left1.png); display:block; width:170px; height:35px; line-height:35px; }
		.slist{width:170px;   margin:0px auto;}
		.slist a{display:block; width:120px; padding-left:50px;  height:25px; text-decoration:none; text-align:left; margin:0px auto;   color:#fff; line-height:25px;}
		.slist a:hover{ color:#FFFF00;}
        </style>
</head>
<body>
    <form id="form1" runat="server">
   <table width="230" border="0" cellspacing="0" cellpadding="0">
     <tr>
       <td>
		 <div style="border:2px solid #FFFFFF;width:170px; height:30px;margin:5px auto; background:#006699; padding:5px; border-radius:10px;"><strong>
          您好：<br>
            <%=userseesion.UserName %> ,欢迎登录！
	    </strong></div></td>
	 </tr>
      <tr>
        <td>
           <div id="menus" runat="server" style="overflow:hidden;overflow-y: scroll; height:800px; width:100%;"></div>
        </td>
      </tr>
    </table>
    </form>
    
</body>
</html>
