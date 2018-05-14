<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Create_Menu.aspx.cs" Inherits="User_Create_Menu" %>

<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>


<!DOCTYPE html><html>
<head runat="server">
    <title></title>
    <uc1:common runat="server" ID="common" />
    <style type="text/css">
 
.STYLE1 {font-weight: bold}
 
    </style>
</head>
<body>
     <div class="main-content" style="width:99%">  

    <form runat="server" id="ok">
<table width="450" border="0" cellpadding="0" cellspacing="1"   style="border:1px solid #016BB4" >
  <tr>
    <td height="32" bgcolor="#016BB4"  class="bai_title">&nbsp;菜单管理</td>
  </tr>
  <tr>
    <td height="224" valign="top" bgcolor="#FFFFFF" style="padding:5px;">
    
                <table width="450" border="0" cellpadding="5" cellspacing="4"    >
                  <tr>
                    <td height="45" bgcolor="#FFFFFF"><div align="right"><strong>菜单分类&nbsp; </strong></div></td>
                    <td height="45" bgcolor="#FFFFFF">
		
		            <select name="bid" style="padding:5px;">
                      <option value="0">一级分类</option>
                    </select>
                    </td>
                  </tr>
                  <tr>
                    <td width="66" height="45" bgcolor="#FFFFFF"><div align="right"><strong>菜单名称&nbsp; </strong></div></td>
                    <td width="352" height="45" bgcolor="#FFFFFF"><input name="menuName" type="text" class="inputs " /></td>
                  </tr>
                  <tr>
                    <td height="45" bgcolor="#FFFFFF"><div align="right"><strong>菜单地址&nbsp; </strong></div></td>
                    <td height="45" bgcolor="#FFFFFF"><input name="menuUrl" type="text" class="inputs " /></td>
                  </tr>
                  <tr>
                    <td height="45" bgcolor="#FFFFFF"><div align="right"><strong>菜单排序&nbsp; </strong></div></td>
                    <td height="45" bgcolor="#FFFFFF"><input name="orderid" type="text" class="inputs " id="orderid" style="width:40px;" value="100" size="10" maxlength="5" />
                    <input name="menuId" type="hidden" id="menuId" /></td>
                  </tr>
                  <tr>
                    <td height="45" bgcolor="#FFFFFF"><div align="right"><strong>显示状态&nbsp; </strong></div></td>
                    <td height="45" bgcolor="#FFFFFF"><label>
                      <input name="statusid" type="radio" value="1" checked="checked" />
                      显示
                      <input type="radio" name="statusid" value="0" />
                    隐藏</label></td>
                  </tr>
                  <tr>
                    <td height="45" bgcolor="#FFFFFF"><div align="right"></div></td>
                    <td height="45" bgcolor="#FFFFFF">
                        <input name="s" type="submit"  class="bt" value="保存" id="saves"   />
                    </td>
                  </tr>
                </table></td>
              </tr>
            </table>

    </form>

         </div>
</body>
</html>
