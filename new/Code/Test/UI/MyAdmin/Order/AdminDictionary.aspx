<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminDictionary.aspx.cs" Inherits="MyAdminDictionary" %>
 <%@ Register src="../inc/Top.ascx" tagname="Top" tagprefix="uc1" %>
<%@ Register src="../inc/Foot.ascx" tagname="Foot" tagprefix="uc2" %>
<!DOCTYPE html><html>
<head id="Head1" runat="server">
    <title>字典设置-ESmartWave</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
   
     <script src="../js/jquery-1.9.1.js" type="text/javascript"></script>
      <link href="../css/facebox.css" rel="stylesheet" type="text/css" />
    <script src="../js/facebox.js" type="text/javascript"></script>
    <script src="../js/Fun.js" type="text/javascript"></script>


 
 
    <style type="text/css">
<!--
.STYLE1 {color: #efefef}
-->
    </style>
</head>
<body>
    <form id="form1" runat="server">

 
    
 
 	   &nbsp;
 <br>
 <br>
		<br>  
		 	 
            <table width="900" border="0" align="center" cellpadding="0" cellspacing="0">
              <tr>
                <td height="60" background="../img/t1.jpg"><table width="96%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                      <td height="39" style="border-bottom:3px solid #efefef;"><div align="center"><span class="hui14"> &nbsp;
                            <strong>字典管理</strong></span></div></td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td background="../img/t2.jpg"><table width="95%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#efefef" style="margin:0px auto;border:1px solid #ccc;">
                  <tr>
                    <td width="9%" height="35" ><div class="add"> <a href="Admin_movie_dconfig.aspx?action=add">添加字典</a> </div></td>
                  </tr>
                </table>
                  <br>
                  <table width="854" border="0" align="center" cellpadding="0" cellspacing="0" style="margin:0px auto">
                  <tr>
                    <td width="61%" height="301" valign="top"><table width="402" border="0" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF">
                        <tr>
                          <td width="47" background="../img/titlebg.png" bgcolor="#FFFFFF" class="bai14"><div align="center">排序</div></td>
                          <td width="167" background="../img/titlebg.png" bgcolor="#FFFFFF" class="bai14"><div align="center">标题</div></td>
                          <td width="167" background="../img/titlebg.png" bgcolor="#FFFFFF" class="bai14"><div align="center">内容</div></td>
                          <td width="89" background="../img/titlebg.png" bgcolor="#FFFFFF" class="bai14"><div align="center">添加子类 </div></td>
                          <td width="94" height="32" background="../img/titlebg.png" bgcolor="#FFFFFF" class="bai14"><div align="center">操作</div></td>
                        </tr>
                        <asp:Repeater runat="server" ID="menuList" onitemdatabound="replist_ItemDataBound">
                          <ItemTemplate>
                            <tr>
                              <td bgcolor="#999999"><div align="center" class="STYLE1">
                                <asp:Label ID="bid" Text='<%#Eval("Id") %>' style="display: none" runat="server"></asp:Label>
                                <%#Eval("OrderId") %>（<%#Eval("Id") %>）</div></td>
                              <td bgcolor="#999999"><span style="color: #efefef"><b style="font-size:14px;"><%#Eval("Title") %></b></span></td>
                              <td bgcolor="#999999"><span style="color: #efefef"><b style="font-size:14px;"><%#Eval("Val") %></b></span></td>
                              <td bgcolor="#999999"><div align="center" class="STYLE1"><a href="Admin_movie_dconfig.aspx?action=add&bid=<%#Eval("Id") %>" style="color:#fff;"> 增加 </a> </div></td>
                              <td height="30" bgcolor="#999999" ><div align="center" class="STYLE1"> <a href="Admin_movie_dconfig.aspx?action=update&id=<%#Eval("Id") %>" ><img src="../img/update.png" width="16" height="16" style="border:0px;" /></a> &nbsp;&nbsp; <a onClick="return del();" href="Admin_movie_dconfig.aspx?id=<%#Eval("Id") %>&action=del"><img src="../img/del.png" width="16" height="16" style="border:0" /></a></div></td>
                            </tr>
                            <asp:Repeater runat="server"  ID="menuList_sid" >
                              <ItemTemplate>
                                <tr height="30">
                                  <td height="30"  bgcolor="#f5f5f5"><div align="center"><%#Eval("OrderId") %>（<%#Eval("Id") %>）</div></td>
                                  <td  bgcolor="#f5f5f5">-----<%#Eval("Title") %></td>
                                  <td  bgcolor="#f5f5f5">-----<%#Eval("Val") %></td>
                                  <td  bgcolor="#f5f5f5">&nbsp;
                                      </div></td>
                                  <td  bgcolor="#f5f5f5" ><div align="center"> <a href="Admin_movie_dconfig.aspx?action=update&id=<%#Eval("Id") %>" ><img src="../img/update.png" width="16" height="16" style="border:0px;" /></a> &nbsp;&nbsp; <a onClick="return del();" href="Admin_movie_dconfig.aspx?id=<%#Eval("Id") %>&action=del"><img src="../img/del.png" width="16" height="16" style="border:0" /></a></div></td>
                                </tr>
                              </ItemTemplate>
                            </asp:Repeater>
                          </ItemTemplate>
                        </asp:Repeater>
                    </table></td>
                    <td width="39%" valign="top"><% if (Request["action"] == "add" || Request["action"] == "update")
     		 { %> <asp:HiddenField runat="server" Value="0" ID="hidid" />
                        <table width="437" border="0" cellpadding="0" cellspacing="1"   style="border:1px solid #ccc" >
                          <tr>
                            <td width="433" height="32" background="../img/titlebg.png" bgcolor="#39A9CB"  class="bai_title">&nbsp;<span class="bai14">字典字典</span></td>
                          </tr>
                          <tr>
                            <td height="224" valign="top" bgcolor="#FFFFFF" style="padding:5px;"><table width="422" border="0" cellpadding="5" cellspacing="4"    >
                                <tr>
                                  <td bgcolor="#FFFFFF" class="hui14" height="45"><div align="right"> <strong>字典字典&nbsp; </strong> </div></td>
                                  <td bgcolor="#FFFFFF" class="hui14" height="45"><asp:DropDownList ID="ddlBid" runat="server">
                                      <asp:ListItem Value="0">一级字典</asp:ListItem>
                                    </asp:DropDownList>
                                  </td>
                                </tr>
                                <tr>
                                  <td bgcolor="#FFFFFF" class="hui14" height="45" width="84"><div align="right"> <strong>标题&nbsp; </strong> </div></td>
                                  <td bgcolor="#FFFFFF" class="hui14" height="45" width="306"><asp:TextBox ID="txtTitle" runat="server" CssClass="input"></asp:TextBox>
                                      <br />
                                      <asp:RequiredFieldValidator ID="CompareValidator1" runat="server" ControlToValidate="txtTitle"
                            ErrorMessage="请输入标题"></asp:RequiredFieldValidator>
                                  </td>
                                </tr>
                                <tr>
                                  <td bgcolor="#FFFFFF" class="hui14" height="45"><div align="right"> <strong>内容&nbsp;
                                  </strong> </div></td>
                                  <td bgcolor="#FFFFFF" class="hui14" height="45"><asp:TextBox ID="txtVal" runat="server" CssClass="input"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td bgcolor="#FFFFFF" class="hui14" height="45"><div align="right"> <strong>排序&nbsp; </strong> </div></td>
                                  <td bgcolor="#FFFFFF" class="hui14" height="45"><asp:TextBox ID="txtOrderId" runat="server" CssClass="input" style="width:30px;" Text="100"></asp:TextBox>
                                    &nbsp;
                                    <input name="Id" type="hidden" id="Id" value="0" /></td>
                                </tr>
                                <tr>
                                  <td bgcolor="#FFFFFF" class="hui14" height="45"><div align="right"></div></td>
                                  <td bgcolor="#FFFFFF" class="hui14" height="45"><asp:Button ID="Button1" runat="server" CssClass="bt" onclick="Button1_Click" 
                                 Text="保存" />                  
                                      <input name="button" type="button" class="bt"  OnClick="window.location.href='Admin_movie_dconfig.aspx'" value="关闭"/>
                                  </td>
                                </tr>
                            </table></td>
                          </tr>
                        </table>
                      <% } %>
                    </td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td background="../img/t3.jpg">&nbsp;</td>
              </tr>
            </table>
    </form>
</body>
</html>
