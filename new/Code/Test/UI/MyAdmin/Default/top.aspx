<%@ Page Language="C#" AutoEventWireup="true" CodeFile="top.aspx.cs" Inherits="MyAdmin_Default_top" %>

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
        
        }
		
		.top a{width:80px; height:25px; line-height:25px; color:#fff; background:#0099CC; display:block; float:left; text-align:center; text-decoration:none; margin-left:10px;}
				.top a:Hover{width:80px; height:25px; line-height:25px; color:#fff; background:#00CC66; display:block; float:left; text-align:center; text-decoration:none; margin-left:10px;}
    </style>
</head>
<body>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td height="65" background="../img/toptop.jpg"  ><table width="96%" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td width="65%" ><div align="left"><strong> </strong>  </div></td>
          
            <td width="35%" class="top" align="right"><a href="../User/UpdatePwd.aspx" target="right">修改密码</a>
			<a onClick="return loginout();" href="../User/LoginOut.aspx" style="margin-left:10px;">退出登录</a></td>
          </tr>
        </table></td>
      </tr>
    </table>
    <form id="form1" runat="server">
    
    </form>
</body>
</html>
