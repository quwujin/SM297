<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InfoShow.aspx.cs" Inherits="MyAdmin_Config_InfoShow" %>

<%@ Register src="../inc/common.ascx" tagname="common" tagprefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title><uc1:common ID="common1" runat="server" />
</head>
<body style="background:#fff;">
    <form id="form1" runat="server">

    <div style="background:#000; color:#fff; text-align:center; font-size:16px; height:30px; line-height:30px;">活动说明</div>
    <div style="text-align:center; line-height:35px; height:35px; border-bottom:1px solid #ccc">
        <asp:Label ID="txtTitle" runat="server" Text="标题" Style="font-size:16px;"></asp:Label>
    </div>
        
     <div style="font-size:14px; line-height:26px;width:90%;margin:0px auto">
          <asp:Label ID="txtNotes" runat="server" Text="内容"></asp:Label>
     </div>

        <div style="text-align:center; width:100%; margin:10px auto">
            <input type="button" value="返回" class="btn btn-sm btn-success" />

        </div>
    </form>
</body>
</html>
