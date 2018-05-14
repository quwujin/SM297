<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminMessageConfig.aspx.cs" Inherits="MyAdmin_AdminMessageConfig" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<%@ Register src="../inc/Top.ascx" tagname="Top" tagprefix="uc1" %>
<%@ Register src="../inc/Foot.ascx" tagname="Foot" tagprefix="uc2" %>
<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>短信配置管理</title>
   <uc1:common runat="server" ID="common" />
 
</head>
<body style="background:#fff;">
    <form id="form1" runat="server"  >
       
 
		 
           
    <asp:HiddenField ID="hidid" Value="0" runat="server" />
             
    <div class="table-header">
            短信配置
        </div>
    <table width="100%" class="table table-bordered" >
        <tr>
            <td class="col-md-2"> 
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="输入标题" ></asp:TextBox> 
            </td>
            <td class="col-md-2">
                <asp:DropDownList ID="ddlSupplierId" runat="server">
                    <asp:ListItem Value="0">请选择供应商</asp:ListItem>
                    <asp:ListItem Value="1">助通</asp:ListItem>
                    <asp:ListItem Value="2">建周</asp:ListItem>
                    <asp:ListItem Value="3">创蓝253</asp:ListItem>
                    <asp:ListItem Value="4">科讯通-vip-dx</asp:ListItem>
                    <asp:ListItem Value="5">科讯通-esmart</asp:ListItem>
                    <asp:ListItem Value="6">科讯通-yuanhui</asp:ListItem>
                    <asp:ListItem Value="7">科讯通-shuomiao</asp:ListItem> 
                </asp:DropDownList>
            </td>
            <td class="col-md-2"> 
                <asp:DropDownList ID="ddlMsgType" runat="server">
                    <asp:ListItem Value="0">请选择发送类型</asp:ListItem>
                    <asp:ListItem Value="1">通知</asp:ListItem>
                    <asp:ListItem Value="2">验证码</asp:ListItem> 
                </asp:DropDownList>
            </td>
            <td> 
                <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" placeholder="输入短信内容" ></asp:TextBox> 
            </td>
            <td> 
                <asp:Button ID="Button2" runat="server" Text="添加短信+" CssClass="btn btn-sm btn-success" OnClick="Button2_Click" />
            </td>
        </tr>
        </table>
                
    <table id="sample-table-2" class="table table-striped table-bordered table-hover">
    <thead>
        <tr> 
             <th>编号</th>
             <th>标题</th>
             <th>供应商</th>
             <th>发送类型</th>
             <th>内容</th>
             <th>操作</th>
        </tr>
    </thead>

    <tbody id="body-colum">
        <asp:Repeater runat="server" ID="menuList" OnItemCommand="menuList_ItemCommand" OnItemDataBound="menuList_ItemDataBound">
            <ItemTemplate>
                <asp:Panel ID="plItem" runat="server">
                <tr >
                    <td class="col-md-1">
                        <span class="label label-sm label-warning"><%#Eval("Id") %></span>
                    </td>
                    <td class="col-md-1"><%#Eval("MsgTitle") %> </td>
                    <td class="col-md-1"><%#Enum.GetName(typeof(Common.MessageApi.Supplie),Common.TypeHelper.ObjectToInt(Eval("SupplierId"),0))  %></td> 
                    <td class="col-md-1"><%#Enum.GetName(typeof(Common.MessageApi.SendType),Common.TypeHelper.ObjectToInt(Eval("MsgType"),0)) %> </td>
                    <td class="col-md-6"><%#Eval("MsgTemp") %></td>  
                    <td >
                        <asp:LinkButton runat="server" ID="lbtEdit" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID")%>'
                         CommandName="Edit" Text="编辑" class="btn btn-success "></asp:LinkButton>&nbsp;&nbsp;
                    </td>
                    <td style="display:none;">
                        <div class="hidden-sm hidden-xs action-buttons">
                            <a class="green" href="Create_UserInfo.aspx?id=<%#Eval("Id")%>">
                                <i class="ace-icon fa fa-pencil bigger-130"></i>
                            </a>
                            <a class="red" onclick="return del();" href="Admin_UserInfo.aspx?id=<%#Eval("Id") %>&action=del">
                                <i class="ace-icon fa fa-trash-o bigger-130"></i>
                            </a>
                        </div>
                    </td>
                </tr> 
                </asp:Panel>
                <asp:Panel ID="plEdit" runat="server">
                 <tr>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "ID")%>
                    </td> 
                    <td>
                        <asp:TextBox ID="txtEditTitle"  class="form-control"  Text='<%# DataBinder.Eval(Container.DataItem,"MsgTitle") %>' runat="server"></asp:TextBox>
                    </td> 
                    <td>
                        <asp:DropDownList ID="txtSupplierId" runat="server" CssClass="form-control"   DataValueField='<%# DataBinder.Eval(Container.DataItem,"SupplierId").ToString() %>'>
                            <asp:ListItem Value="0">请选择供应商</asp:ListItem>
                            <asp:ListItem Value="1">助通</asp:ListItem>
                            <asp:ListItem Value="2">建周</asp:ListItem>
                            <asp:ListItem Value="3">创蓝253</asp:ListItem>
                            <asp:ListItem Value="4">科讯通-vip-dx</asp:ListItem>
                            <asp:ListItem Value="5">科讯通-esmart</asp:ListItem>
                            <asp:ListItem Value="6">科讯通-yuanhui</asp:ListItem>
                            <asp:ListItem Value="7">科讯通-shuomiao</asp:ListItem> 
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="txtSupplierId"  class="form-control"  Text='<%# DataBinder.Eval(Container.DataItem,"SupplierId") %>' runat="server"></asp:TextBox>--%>
                    </td>
                    <td>
                        <asp:DropDownList ID="txtMsgType" runat="server" CssClass="form-control"   DataValueField='<%# DataBinder.Eval(Container.DataItem,"MsgType").ToString() %>'>
                            <asp:ListItem Value="0">请选择发送类型</asp:ListItem> 
                            <asp:ListItem Value="1">通知</asp:ListItem>
                            <asp:ListItem Value="2">验证码</asp:ListItem> 
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="txtMsgType"  class="form-control"  Text='<%# DataBinder.Eval(Container.DataItem,"MsgType") %>' runat="server"></asp:TextBox>--%>
                    </td>
                    <td> 
                        <asp:TextBox ID="txtEditMessage"  class="form-control"  Text='<%# DataBinder.Eval(Container.DataItem,"MsgTemp") %>' runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:LinkButton runat="server" ID="lbtUpdate" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID")%>'
                        CommandName="Update" Text="更新" class="btn btn-success "></asp:LinkButton>&nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="lbtCancel" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID")%>'
                        CommandName="Cancel" class="btn btn-danger " Text="取消"></asp:LinkButton>
                    </td>
                </tr>

            </asp:Panel>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
                            
    </table> 
     <table width="100%" style="margin-top: 50px;">
        <tr>
            <td class="page">
                <webdiyer:AspNetPager ID="AspNetPager1" CssClass="page" runat="server"  OnPageChanged="AspNetPager1_PageChanged" 
                        CurrentPageButtonPosition="Center"  PageSize="30"  
                        ShowCustomInfoSection="Right" PageIndexBoxType="TextBox" 
                        ShowPageIndexBox="Never" 
                    CustomInfoHTML="<span>当前第 %CurrentPageIndex% 页, 共 %PageCount%页 共%RecordCount% 条记录</span>" FirstPageText="首页" 
                    LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页">
                </webdiyer:AspNetPager>
            </td>
        </tr>
    </table>
</form>
</body>
</html>
