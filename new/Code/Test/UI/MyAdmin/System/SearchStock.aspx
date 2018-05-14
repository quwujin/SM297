<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchStock.aspx.cs" Inherits="MyAdmin_System_SearchStock" %>
<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>查询库存</title>
    <uc1:common runat="server" ID="common" />
</head>
<body style="background:#fff;">
    <form id="form1" runat="server">

     <div class="table-header"> 查询库存 </div> 
     <table  border="0" align="center"  class="table table-bordered"  >
         <tr>
            <td bgcolor="#FFFFFF"  height="50"> 
                <div align="right">
                    <strong>资源编号&nbsp; </strong>
                </div>
            </td>
            <td bgcolor="#FFFFFF"  height="50">
                <asp:TextBox ID="txtSid" runat="server" Text="" placeholder="输入资源ID" class="form-control" ></asp:TextBox>
            </td>
        </tr>
         <tr>
             <td>
                 <div align="right">
                    <strong>资源剩余量&nbsp; </strong>
                </div>
             </td>
             <td>
                 <asp:Label ID="LbSurplus" runat="server" Text=""></asp:Label>
             </td>
         </tr>

         <tr>
             <td></td>
             <td>
                 <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="查询" OnClick="Button1_Click" />
             </td>
         </tr>

     </table>

     <div class="table-header"> 项目配置-资源ID库存 </div> 
     <table  border="0" align="center"  class="table table-bordered"  >
        <tr>
            <td>资源Id</td>
            <td>资源名称</td>
            <td>资源剩余量</td>
        </tr>
        <%=GetStockList() %>  
     </table>

    </form>
</body>
</html>
