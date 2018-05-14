<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Create_UserInfo.aspx.cs" Inherits="User_Create_UserInfo"      %>

<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>
<%@ Register Src="~/MyAdmin/inc/Location.ascx" TagPrefix="uc1" TagName="Location" %>




<!DOCTYPE html>
<html>
<head runat="server">
    <title>用户管理-ESmartWave</title>
    <uc1:common runat="server" ID="common" />
    <style type="text/css">
        .auto-style1 {
            height: 35px;
        }
    </style>
</head>
<body style="background:#fff;">

    <form id="form1" runat="server">
      <div class="main-content" style="width:99%">  
          <div class="row">
              <uc1:Location runat="server" ID="Location" Name="用户管理" />
 
                <table width="95%" border="0" align="center" class="table table-bordered">
       
              <tr>
                <td width="11%" height="35"><div align="center">姓名/账号<br />
                  (支持中文)</div></td>
                <td width="89%" height="35"><asp:TextBox ID="txtUserName" runat="server" CssClass="input"></asp:TextBox>
                  &nbsp;
                  <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="账号不能为空" style="color:red" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>
                </td>
              </tr>
              <tr>
                <td height="35"><div align="center">密码</div></td>
                <td height="35">
                    <asp:TextBox ID="txtPassWord" runat="server" Text="123456"   CssClass="input" TextMode="Password"></asp:TextBox>
                    <asp:TextBox ID="txtPassWord2" runat="server" Text=""   CssClass="input"  style="display:none"></asp:TextBox>
                  &nbsp;
                  <asp:RequiredFieldValidator  
                        ID="RequiredFieldValidator2" runat="server" 
                        ErrorMessage="密码不能为空" style="color:red" ControlToValidate="txtPassWord"></asp:RequiredFieldValidator>
                  <asp:Label Text="" runat="server" ID="txt"></asp:Label>
                </td>
              </tr>
              <tr>
                <td height="35"><div align="center">所属权限属</div></td>
                <td height="35"><asp:DropDownList ID="ddlGroupId" runat="server" CssClass="sel"> </asp:DropDownList>
                </td>
              </tr>
              <tr>
                <td class="auto-style1"><div align="center">状态</div></td>
                <td class="auto-style1"><asp:RadioButtonList ID="ddlStatusId" runat="server"  RepeatDirection="Horizontal" >
                    <asp:ListItem Selected="True" Value="1">正常</asp:ListItem>
                    <asp:ListItem Value="0">锁定</asp:ListItem>
                  </asp:RadioButtonList>
                    <asp:HiddenField ID="hidId" runat="server" Value="0" />
                </td>
              </tr>
              <tr>
                <td height="35">&nbsp;</td>
                <td height="35"><asp:Button ID="Button1" runat="server" CssClass="btn btn-success" onclick="Button1_Click" 
                        Text="保存" />            
                </td>
              </tr>
            </table> 
	 </div>

		</div> 
</form>
</body>
</html>
