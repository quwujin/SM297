<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin_Menu.aspx.cs" Inherits="User_Admin_Menu"      %>

<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>


<!DOCTYPE html><html>
<head runat="server">
    <title>菜单设置-ESmartWave</title>
  
    <uc1:common runat="server" ID="common" />
 
 
    <style type="text/css">
<!--
.STYLE1 {color: #efefef}
-->
    </style>
</head>
<body>
    <form id="form1" runat="server">

 
  

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
							<a href="#">菜单管理</a>
						</li>
						 

                       
					</ul>
	</div>
 
		 	 

     
      <div class="page-content-area" style="background:#fff;">
        
                
                    <br />
                         <h4 class="pink">
									<i class="ace-icon fa fa-hand-o-right green"></i>
									<a href="Admin_menu.aspx?action=add" role="button" class="btn btn-primary btn-sm" > 添加菜单 </a>
								</h4>
										 
          <br/>

            
            
                  <table   width="100%" border="0" align="center" cellpadding="0" cellspacing="0"  class="table table-bordered" >
                  <tr>
                    <td  height="301" valign="top">
                        
                        
                        <table width="402" border="0" cellpadding="0" cellspacing="1"  class="table table-bordered">
                        <tr>
                          <td width="47"   ><div align="center">排序</div></td>
                          <td width="167"  ><div align="center">菜单名</div></td>
                          <td width="89" ><div align="center">添加子菜单 </div></td>
                          <td width="94" ><div align="center">操作</div></td>
                        </tr>
                        <asp:Repeater runat="server" ID="menuList" onitemdatabound="replist_ItemDataBound">
                          <ItemTemplate>
                            <tr class="info" style="text-align:center">
                              <td  >
                                <asp:Label ID="bid" Text='<%#Eval("MenuId") %>' style="display:none" runat="server"></asp:Label>
                                <%#Eval("OrderId") %></div></td>
                              <td   style="text-align:left"> <%#Eval("menuName") %></b></span></td>
                              <td  ><a href="admin_menu.aspx?action=add&bid=<%#Eval("MenuId") %>"  class="btn btn-info btn-xs" > 增加子菜单+ </a> 

                              </td>
                              <td height="30"   >
                                  
                                 
                                      <a class="btn btn-warning btn-xs" href="Admin_Menu.aspx?action=update&id=<%#Eval("MenuId") %>">
												<i class="ace-icon fa fa-wrench  bigger-110 icon-only"></i>
											</a>


                               
                                         <a class="btn btn-danger btn-xs" onClick="return del();" href="admin_Menu.aspx?id=<%#Eval("MenuId") %>&action=del">
												<i class="ace-icon fa fa-trash-o bigger-110"></i>
											</a> 

                                            </div>

                              </td>
                            </tr>
                            <asp:Repeater runat="server"  ID="menuList_sid" >
                              <ItemTemplate>
                                <tr height="30">
                                  <td height="30"  ><div align="center"><%#Eval("OrderId") %></div></td>
                                  <td  >-----<%#Eval("menuName") %></td>
                                  <td  >&nbsp;
                                      </div></td>
                                  <td  ><div align="center">
                                          <a class="btn btn-warning btn-xs" href="Admin_Menu.aspx?action=update&id=<%#Eval("MenuId") %>">
												    <i class="ace-icon fa fa-wrench  bigger-110 icon-only"></i>
											    </a>

                                             <a class="btn btn-danger btn-xs" onClick="return del();" href="admin_Menu.aspx?id=<%#Eval("MenuId") %>&action=del">
												    <i class="ace-icon fa fa-trash-o bigger-110"></i>
											    </a>


                                          </td>
                                </tr>
                              </ItemTemplate>
                            </asp:Repeater>
                          </ItemTemplate>
                        </asp:Repeater>
                    </table></td>
                    <td width="39%" valign="top">
                        
                        
                        <% if (Request["action"] == "add" || Request["action"] == "update")
     		 { %>
                        <table width="437" border="0" cellpadding="0" cellspacing="1"   class="table table-bordered" >
                          <tr>
                            <td width="433" height="32"   bgcolor="#39A9CB"  >&nbsp;<span class="bai14" style="color:#fff;">菜单管理</span></td>
                          </tr>
                          <tr>
                            <td height="224" valign="top" bgcolor="#FFFFFF" style="padding:5px;">
                                
                                
                                <table width="422" border="0" cellpadding="5" cellspacing="4"   class="table table-bordered" >
                                <tr>
                                  <td bgcolor="#FFFFFF"   height="45"><div align="right"> <strong>菜单分类&nbsp; </strong> </div></td>
                                  <td bgcolor="#FFFFFF"   height="45"><asp:DropDownList ID="ddlBid" runat="server">
                                      <asp:ListItem Value="0">一级菜单</asp:ListItem>
                                    </asp:DropDownList>
                                  </td>
                                </tr>
                                <tr>
                                  <td bgcolor="#FFFFFF"  height="45" width="84"><div align="right"> <strong>菜单名称&nbsp; </strong> </div></td>
                                  <td bgcolor="#FFFFFF"  height="45" width="306"><asp:TextBox ID="txtMenuName" runat="server" class="input-sm form-control"></asp:TextBox>
                                      <br />
                                      <asp:RequiredFieldValidator ID="CompareValidator1" runat="server" ControlToValidate="txtMenuName"
                            ErrorMessage="也太懒了点吧，留个菜单名吧"></asp:RequiredFieldValidator>
                                  </td>
                                </tr>
                                <tr>
                                  <td bgcolor="#FFFFFF"   height="45"><div align="right"> <strong>菜单地址&nbsp;
                                            <asp:HiddenField runat="server" Value="0" ID="hidId" />
                                  </strong> </div></td>
                                  <td bgcolor="#FFFFFF"   height="45"><asp:TextBox ID="txtMenuUrl" runat="server" class="input-sm form-control"></asp:TextBox>
                                  </td>
                                </tr>
                                <tr>
                                  <td bgcolor="#FFFFFF"  height="45"><div align="right"> <strong>菜单排序&nbsp; </strong> </div></td>
                                  <td bgcolor="#FFFFFF"   height="45"><asp:TextBox ID="txtOrderId" runat="server" class="input-sm form-control" style="width:50px;" Text="100"></asp:TextBox>
                                    &nbsp;
                                    <input name="menuId" type="hidden" id="menuId" value="0" /></td>
                                </tr>
                                <tr>
                                  <td bgcolor="#FFFFFF" class="hui14" height="45"><div align="right"> <strong>显示状态&nbsp; </strong> </div></td>
                                  <td bgcolor="#FFFFFF" class="hui14" height="45"><asp:RadioButtonList ID="radStatusId" runat="server" 
                                 RepeatDirection="Horizontal">
                                      <asp:ListItem Selected="True" Value="1">显示</asp:ListItem>
                                      <asp:ListItem Value="0">隐藏</asp:ListItem>
                                    </asp:RadioButtonList>
                                  </td>
                                </tr>
                                <tr>
                                  <td bgcolor="#FFFFFF" height="45"><div align="right"> </div></td>
                                  <td bgcolor="#FFFFFF"   height="45"><asp:Button ID="Button1" runat="server"  class="btn btn-sm btn-success" onclick="Button1_Click" 
                                 Text="保存" />                  
                                      <input name="button" type="button"  OnClick="window.location.href='admin_menu.aspx?<%=menuUrl %>'" value="关闭"  class="btn btn-sm btn-default"/>
                                  </td>
                                </tr>
                            </table></td>
                          </tr>
                        </table>
                      <% } %>
                    </td>
                  </tr>
                </table> 


          </div>
 
    </form>
</body>
</html>
