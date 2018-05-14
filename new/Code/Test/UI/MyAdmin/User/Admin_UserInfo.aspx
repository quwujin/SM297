<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin_UserInfo.aspx.cs" Inherits="User_Admin_UserInfo" %>

<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>用户管理-ESmartWave</title> 
    <uc1:common runat="server" ID="common" /> 
</head>
<body style="background: #fff;">
    <form id="form1" runat="server">
        <div class="main-content" style="width: 99%">
            <div class="row">
                <div class="col-xs-12">
                    <div class="table-header">
                        用户列表
                    </div>

                    <!-- <div class="table-responsive"> -->

                    <!-- <div class="dataTables_borderWrap"> -->
                    <div>
                        <table id="sample-table-2" class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr>
                                    <th>编号</th>
                                    <th>登录名</th>
                                    <th>组名</th> 
                                    <th>状态</th> 
                                    <th>操作</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="menuList">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="hidden-480">
                                                <span class="label label-sm label-warning"><%#Eval("UserId") %></span>
                                            </td>
                                            <td>
                                                <a href="#"><%#Eval("UserName") %></a>
                                            </td>
                                            <td><%#Eval("GroupName") %></td>
                                            <td><%#Eval("StatusId").ToString()=="1"?"正常":"<span style='color:red'>锁定</span>" %></td>
                                            <td>
                                                <div class="hidden-sm hidden-xs action-buttons">
                                                    <a class="green" href="Create_UserInfo.aspx?id=<%#Eval("UserId")%>">
                                                        <i class="ace-icon fa fa-pencil bigger-130"></i>
                                                    </a>
                                                    <a class="red" onclick="return del();" href="Admin_UserInfo.aspx?id=<%#Eval("UserId") %>&action=del">
                                                        <i class="ace-icon fa fa-trash-o bigger-130"></i>
                                                    </a>
                                                </div>
                                            </td>
                                        </tr> 
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
