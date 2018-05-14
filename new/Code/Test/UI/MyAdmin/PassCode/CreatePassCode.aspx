<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreatePassCode.aspx.cs" Inherits="MyAdmin_PassCode_CreatePassCode" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
 
<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>电子串码管理</title>
    <uc1:common runat="server" ID="common" />
  

</head>
<body style="background:#fff;">
    <form id="form1" runat="server"  >
  	<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

         <div class="table-header">
                        生成激活码
                    </div> 


                    <table  border="0" align="center"  class="table table-bordered"  >

                     <tr>
                        <td bgcolor="#FFFFFF"  height="50"> <div align="right">
                                 <strong>生成数量&nbsp; </strong>
                       </div></td>
                         <td bgcolor="#FFFFFF"  height="50">
                             <asp:TextBox ID="txtQty" runat="server" Text="1000" MaxLength="6"  class="form-control" ></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                        <td bgcolor="#FFFFFF"  height="50"> <div align="right">
                                 <strong>奖 项&nbsp; </strong>
                       </div></td>
                         <td bgcolor="#FFFFFF"  height="50">
                             <asp:DropDownList ID="ddlCity" runat="server" class="form-control" >
                                 <asp:ListItem value="">默认</asp:ListItem>
                                 <asp:ListItem>JN</asp:ListItem>
                                 <asp:ListItem>JW</asp:ListItem>
                                 <asp:ListItem>JS</asp:ListItem>
                                 <asp:ListItem>BJ</asp:ListItem>
                                 <asp:ListItem>GS</asp:ListItem>
                                 <asp:ListItem>HH</asp:ListItem>
                                 <asp:ListItem>LS</asp:ListItem>
                                 <asp:ListItem>HW</asp:ListItem>
                                 <asp:ListItem>ZH</asp:ListItem>
                             </asp:DropDownList>
                         </td>
                     </tr>
 
                     <tr>
                       <td bgcolor="#FFFFFF"  height="50"><div align="right">描述</div></td>
                       <td bgcolor="#FFFFFF"  height="50"><asp:TextBox ID="txtNotes" runat="server" class="form-control"  ></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNotes" runat="server" ErrorMessage="描述不能为空"></asp:RequiredFieldValidator>
                         </td>
                     </tr>

                           <tr>
                       <td bgcolor="#FFFFFF"  height="50"><div align="right">类型</div></td>
                       <td bgcolor="#FFFFFF"  height="50"> 
                           
                           <asp:DropDownList ID="DropDownList1" runat="server" class="form-control">
                               <asp:ListItem Value="19">19位随机数</asp:ListItem>
                               <asp:ListItem Value="16(4+4+4+4)">16位随机数(4+4+4+4)</asp:ListItem>
                               <asp:ListItem Value="16">4(字母）+6位数字随机</asp:ListItem>
                               <asp:ListItem Value="4+2">6(2字母4数字(位置随机)</asp:ListItem>
                               <asp:ListItem Value="8英文">8英文</asp:ListItem>
                               <asp:ListItem Value="9英文">9英文</asp:ListItem>
                               <asp:ListItem Value="8+4">8字母+4数字(SN)</asp:ListItem>
                           </asp:DropDownList>
                           
                         </td>



                     </tr>


                 
                     

                     
                     <tr>
                        <td bgcolor="#FFFFFF"  height="50">&nbsp;</td>
                         <td bgcolor="#FFFFFF"  height="50">
                       <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" onclick="Button1_Click" 
                                 Text="生成" />    </td>
                     </tr>
                </table>
                    
                          
            
  
    	</form>
</body>
</html>
