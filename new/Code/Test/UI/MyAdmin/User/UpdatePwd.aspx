<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePwd.aspx.cs" Inherits="User_UpdatePwd" %>

<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>






<!DOCTYPE html>

<html>
<head runat="server">
    <title>修改密码-ESmartWave</title>
    <uc1:common runat="server" ID="common" />
</head>



<body style="background:#fff;">
    <form id="form1" runat="server">

        
 	<div class="breadcrumbs" id="breadcrumbs">
					<script type="text/javascript">
					    try { ace.settings.check('breadcrumbs', 'fixed') } catch (e) { }
					</script>

					<ul class="breadcrumb">
						<li>
							<i class="ace-icon fa fa-home home-icon"></i>
							<a href="../default/main.aspx">桌面</a>
						</li>

						<li>
							<a href="#">修改密码</a>
						</li>
						 

                       
					</ul><!-- /.breadcrumb -->

				 

					<!-- /section:basics/content.searchbox -->
	</div>
 

      <div class="page-content-area">
        


  
                    
                    
                    <table width="825" border="0" align="center" cellpadding="3" cellspacing="3"    class="table table-bordered" >
               
                     <tr>
                         <td bgcolor="#FFFFFF" class="hui14" height="50" width="133">
                             <div align="right">
                       <strong>账号&nbsp; </strong>                             </div>                        </td>
                         <td bgcolor="#FFFFFF" class="hui14" height="50" width="671">
                             <asp:TextBox ID="txtUserName" style="background:#ccc;" runat="server" CssClass="input" ReadOnly="true" TextMode="SingleLine"></asp:TextBox>
                       <asp:HiddenField runat="server" Value="0" ID="hidId" />                        </td>
                     </tr>
                 
                  
                     <tr>
                        <td bgcolor="#FFFFFF" class="hui14" height="50"> <div align="right">
                                 <strong>原始密码&nbsp; </strong>
                       </div></td>
                         <td bgcolor="#FFFFFF" class="hui14" height="50">
                             <asp:TextBox ID="txtOldPwd" runat="server" CssClass="input" TextMode="Password"  ></asp:TextBox>
                       <asp:RequiredFieldValidator ID="CompareValidator1" runat="server" ControlToValidate="txtOldPwd"
                            ErrorMessage="原始密码不能为空" style="color:red"></asp:RequiredFieldValidator></td>
                     </tr>
 
                     <tr>
                       <td bgcolor="#FFFFFF" class="hui14" height="50"><div align="right">新密码</div></td>
                       <td bgcolor="#FFFFFF" class="hui14" height="50"><asp:TextBox ID="txtNewPwd" runat="server" CssClass="input" TextMode="Password"  ></asp:TextBox>
                       <asp:RequiredFieldValidator ID="CompareValidator11" runat="server" ControlToValidate="txtNewPwd"
                            ErrorMessage="不能为空" style="color:red"></asp:RequiredFieldValidator></td>
                     </tr>
                     <tr>
                       <td bgcolor="#FFFFFF" class="hui14" height="50"><div align="right">确认新密码</div></td>
                       <td bgcolor="#FFFFFF" class="hui14" height="50"><asp:TextBox ID="txtNewPwd2" runat="server" CssClass="input" TextMode="Password"  ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CompareValidator12" runat="server" ControlToValidate="txtNewPwd2"
                            ErrorMessage="不能为空" style="color:red" Display="Dynamic"></asp:RequiredFieldValidator>
                           <asp:CompareValidator ID="CompareValidator13" runat="server" 
                               ErrorMessage="两次密码输入不一致" style="color:red"  ControlToValidate="txtNewPwd2" 
                               ControlToCompare="txtNewPwd" Display="Dynamic"></asp:CompareValidator>
                       </td>
                     </tr>
                     
                     <tr>
                        <td bgcolor="#FFFFFF" class="hui14" height="50">&nbsp;</td>
                         <td bgcolor="#FFFFFF" class="hui14" height="50">
                       <asp:Button ID="Button1" runat="server"  onclick="Button1_Click"  class="btn btn-sm btn-success"
                                 Text="保存" />                              </td>
                     </tr>
                </table></td>
              </tr>
              <tr>
                <td background="../img/t3.jpg">&nbsp;</td>
              </tr>
            </table> 

          </div>
        </div>
        
  
    	</form>
</body>
</html>
