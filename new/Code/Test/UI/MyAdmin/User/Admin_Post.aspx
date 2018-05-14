<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin_Post.aspx.cs" Inherits="User_Admin_Post" %>

 
<%@ Register src="../inc/LeftMenu.ascx" tagname="LeftMenu" tagprefix="uc2" %>

<!DOCTYPE html><html>
<head runat="server">
    <title>部门管理</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <script src="../js/Fun.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
   
   
 
 
        <table width="100%" height="302" border="0" cellpadding="0" cellspacing="0">
          <tr>
				<td width="180" valign="top">

					<uc2:LeftMenu ID="LeftMenu1" runat="server" />
					
					</td>
            <td valign="top" class="box">
			            
                <table width="98%" border="0" cellspacing="0" cellpadding="0" style="margin:0px auto;">
                      <tr>
                        <td width="3%" ><div align="center"><img src="../img/dian.jpg" width="13" height="14" /></div></td>
                        <td width="22%" height="35" >&nbsp;<span class="leftTitle">部门管理</span></td>
                        <td width="10%" >
			            <div class="add">
				            <a href="admin_Post.aspx?action=add">添加部门</a>			            </div> 
                          <div class="add"></div>			            </td>
                        <td width="65%" ><span class="add"><a href="admin_Post.aspx?<%=menuUrl%>">刷新</a></span></td>
                      </tr>
                    </table>


        	<table width="98%" border="0" cellspacing="0" cellpadding="0" style="margin:0px auto">
          <tr>
            <td width="47%" height="301" valign="top">
            
            
            <table width="500" border="0" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF">
              <tr>
                <td width="54" background="../img/titlebg.png" bgcolor="#FFFFFF" class="bai14"><div align="center">排序</div></td>
                <td width="251" background="../img/titlebg.png" bgcolor="#FFFFFF" class="bai14"><div align="center">名称</div></td>
 
                <td width="87" height="32" background="../img/titlebg.png" bgcolor="#FFFFFF" class="bai14"><div align="center">操作</div></td>
              </tr>

          <asp:Repeater runat="server"  ID="menuList"  >
            <ItemTemplate>
              <tr>
                <td background="../img/titlebg.jpg" bgcolor="#efefef"><div align="center"> <%#Eval("OrderId") %></div></td>
                <td background="../img/titlebg.jpg" bgcolor="#efefef"><b> <a href="Admin_Post.aspx?action=update&id=<%#Eval("PostId") %>" ><%#Eval("PostName") %></a></b></td>
          
                <td height="30" background="../img/titlebg.jpg" bgcolor="#efefef" ><div align="center">
                
                <a href="Admin_Post.aspx?action=update&id=<%#Eval("PostId") %>" ><img src="../img/update.png" width="16" height="16" style="border:0px;" /></a>
        &nbsp;&nbsp;
                
                 <a onclick="return del();" href="Admin_Post.aspx?id=<%#Eval("PostId") %>&action=del"><img src="../img/del.png" width="16" height="16" style="border:0" /></a></div></td>
              </tr>
              </ItemTemplate>
           </asp:Repeater>


            </table></td>
            <td width="53%" valign="top">
			<% if (Request["action"] == "add" || Request["action"] == "update")
      { %>
			<table width="450" border="0" cellpadding="0" cellspacing="1"   
                    style="border:1px solid #016BB4; height: 214px;" >
			  <tr>
				<td height="32" background="../img/titlebg.png" bgcolor="#39A9CB"  class="bai_title">&nbsp;<span class="bai14">部门管理</span></td>
			  </tr>
			  <tr>
    <td height="124" valign="top" bgcolor="#FFFFFF" style="padding:5px;">
                <table width="450" border="0" cellpadding="3" cellspacing="3"    >
               
                     <tr>
                         <td bgcolor="#FFFFFF" class="hui14" height="35" width="64">
                             <div align="right">
                                 <strong>部门&nbsp; </strong>
                             </div>
                        </td>
                         <td bgcolor="#FFFFFF" class="hui14" height="35" width="457">
                             <asp:TextBox ID="txtgroupName" runat="server" CssClass="input"></asp:TextBox>
                       
                        <asp:RequiredFieldValidator ID="CompareValidator1" runat="server" ControlToValidate="txtgroupName"
                            ErrorMessage="也太懒了点吧" style="color:red"></asp:RequiredFieldValidator>
                            <asp:HiddenField runat="server" Value="0" ID="hidId" />
                        </td>
                     </tr>
                 
                  
                     <tr>
                        <td bgcolor="#FFFFFF" class="hui14" height="35"> <div align="right">
                                 <strong>排序&nbsp; </strong>
                             </div></td>
                         <td bgcolor="#FFFFFF" class="hui14" height="35">
                             <asp:TextBox ID="txtOrderId" runat="server" CssClass="input" style="width:50px;" Text="100"></asp:TextBox>
                       
                         </td>
                     </tr>
 
                     <tr>
                        <td bgcolor="#FFFFFF" class="hui14" height="35">&nbsp;</td>
                         <td bgcolor="#FFFFFF" class="hui14" height="35">
                             <asp:Button ID="Button1" runat="server" CssClass="bt" onclick="Button1_Click" 
                                 Text="保存" />
                             <input type="button" value="关闭" class="bt"  OnClick="window.location.href='admin_post.aspx?<%=menuUrl%>'"/>
                         </td>
                     </tr>
 
                </table>
				<% } %>
			
				</td>
              </tr>
            </table>
			</td>
          </tr>
        </table>

         
           

        </td>
      </tr>
    </table>
    <br />
    	</form>
</body>
</html>
