<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin_Group.aspx.cs" Inherits="User_Admin_Group" %>
<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>
<!DOCTYPE html><html>
<head runat="server">
    <title>权限组管理-ESmartWave</title>
 
    <uc1:common runat="server" ID="common" />
</head>
<body style="background:#fff;">
    <form id="form1" runat="server">
   
    <script>
        function SelectAll() {
            var checkboxs = document.getElementsByName("menu_select_id");
            for (var i = 0; i < checkboxs.length; i++) {
                var e = checkboxs[i];
                e.checked = !e.checked;
            }
        }
</script>
 
 
 	<div class="breadcrumbs" id="breadcrumbs">
					<script type="text/javascript">
						try{ace.settings.check('breadcrumbs' , 'fixed')}catch(e){}
					</script>

					<ul class="breadcrumb">
						<li>
							<i class="ace-icon fa fa-home home-icon"></i>
							<a href="../default/main.aspx">桌面</a>
						</li>

						<li>
							<a href="#">分组管理</a>
						</li>
						 

                       
					</ul><!-- /.breadcrumb -->

				 

					<!-- /section:basics/content.searchbox -->
	</div>
 

      <div class="page-content-area">
        
                

                         <h4 class="pink">
									<i class="ace-icon fa fa-hand-o-right green"></i>
									<a href="Admin_Group.aspx?action=add" role="button" class="btn btn-primary btn-sm" > 添加分组 </a>
								</h4>
										 

         
         
                  
                     
                    <br>
                    <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin:0px auto"  class="table  table-condensed">
                      <tr>
                        <td width="35%" height="301" align="left" valign="top">
                            
                            <table width="314" border="0" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF" class="table  table-bordered" >
                            <tr  class="info">
                              <td width="56"   bgcolor="#FFFFFF"  ><div align="center"> ID</div></td>
                              <td width="190" bgcolor="#FFFFFF"  ><div align="center">权限组名</div></td>
                              <td width="130" height="32" bgcolor="#FFFFFF"  ><div align="center">操作</div></td>
                            </tr>
                            <asp:Repeater runat="server"  ID="menuList"  >
                              <ItemTemplate>
                                <tr>
                                  <td ><div align="center"> <%#Eval("GroupId") %></div></td>
                                  <td ><b> <a href="Admin_Group.aspx?action=update&id=<%#Eval("GroupId") %>" ><%#Eval("GroupName") %></a></b></td>
                                  <td height="30"  ><div align="center">
                                 
                                          <a class="btn btn-warning btn-xs" href="Admin_Group.aspx?action=update&id=<%#Eval("GroupId") %>">
												<i class="ace-icon fa fa-wrench  bigger-110 icon-only"></i>
											</a>


                                      &nbsp;&nbsp;  
                                         <a class="btn btn-danger btn-xs"onClick="return del();" href="Admin_Group.aspx?action=del&id=<%#Eval("GroupId") %>">
												<i class="ace-icon fa fa-trash-o bigger-110"></i>
											</a> 
                                          </div></td>
                                </tr>
                              </ItemTemplate>
                            </asp:Repeater>
                        </table></td>
                        <td width="65%" valign="top"><% if (Request["action"] == "add" || Request["action"] == "update")
      { %>
                            <table width="519" border="0" cellpadding="0" cellspacing="1"   class="table  table-bordered  " >
                              <tr>
                                <td width="415" height="32" class="info" > 权限组管理  </td>
                              </tr>
                              <tr>
                                <td height="224" valign="top" bgcolor="#FFFFFF" style="padding:5px;">
                                    
                                    
                                    <table width="600" border="0" cellpadding="5" cellspacing="3"    >
                                    <tr>
                                      <td bgcolor="#FFFFFF" height="35" width="100"><div align="right">   <strong>组名&nbsp; </strong> </div></td>
                                      <td bgcolor="#FFFFFF" height="35" width="300"><asp:TextBox ID="txtgroupName" runat="server"  ></asp:TextBox>
                                          <br />
                                          <asp:RequiredFieldValidator ID="CompareValidator1" runat="server" ControlToValidate="txtgroupName"
                            ErrorMessage="也太懒了点吧，起个名吧" style="color:red"></asp:RequiredFieldValidator>
                                          <asp:HiddenField runat="server" Value="0" ID="hidId" />
                                      </td>
                                    </tr>
                                    <tr>
                                      <td bgcolor="#FFFFFF"   height="35" ><div align="right">
                                          <p align="center">权<br />
                                            限</p>
                                     
                                      </div></td>
                                      <td bgcolor="#ffffff" class="hui14" height="35" width="416"><div runat="server" id="qx"> </div></td>
                                    </tr>
                                    <tr>
                                      <td bgcolor="#FFFFFF" class="hui14" height="35">&nbsp;</td>
                                      <td bgcolor="#FFFFFF" class="hui14" height="35"><br /><div class="btn  btn-sm btn-info"><a href="#" onClick="SelectAll()" style="color:#fff;">全选/反选</a> </div>
                                          <br />
                                          <br />

                                      </td>
                                    </tr>
                                    <tr>
                                      <td bgcolor="#FFFFFF" class="hui14" height="35">&nbsp;</td>
                                      <td bgcolor="#FFFFFF" class="hui14" height="35"><asp:Button ID="Button1" runat="server" class="btn btn-sm btn-success"  onclick="Button1_Click" 
                                 Text="&nbsp;&nbsp;&nbsp;&nbsp;保&nbsp;&nbsp;存&nbsp;&nbsp;&nbsp;&nbsp;" />                
                                          <input name="button" type="button"  class="btn btn-sm btn-default"  OnClick="window.location.href='admin_group.aspx'" value="关闭"/>
                                      </td>
                                    </tr>
                                  </table>
                                   
                                </td>
                              </tr>
                          </table>
						   <% } %>
						  
					    </td>
                      </tr>
                    </table>
                <br>
        <br>
		</div>
 
 
		
    </form>
</body>
</html>
