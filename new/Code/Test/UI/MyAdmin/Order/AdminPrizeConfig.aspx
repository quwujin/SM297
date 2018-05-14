<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminPrizeConfig.aspx.cs" Inherits="MyAdmin_AdminPrizeConfig" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<%@ Register src="../inc/Top.ascx" tagname="Top" tagprefix="uc1" %>
<%@ Register src="../inc/Foot.ascx" tagname="Foot" tagprefix="uc2" %>
<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>奖品配置管理</title>
   <uc1:common runat="server" ID="common" />
 
</head>
<body style="background:#fff;">
    <form id="form1" runat="server"  >
        
    <asp:HiddenField ID="hidid" Value="0" runat="server" />
             
    <div class="table-header">
            奖品配置
    </div> 
                
    <table id="sample-table-2" class="table table-striped table-bordered table-hover">
    <thead>
        <tr> 
             <th>编号</th>
          <%--   <th>奖项编号</th>--%>
             <th>奖项名称</th>
             <th>统计日期</th>
             <th>昨天数量</th>
             <th>当天数量</th>
             <th>总数量</th>
             <th>回库数量</th>
             <%--<th>奖项类型</th>--%>
             <th>奖品名称</th>
         <%--    <th>角度</th>
             <th>预设值-默认值中奖</th>
             <th>创建时间</th>
             <th>修改时间</th>
             <th>状态</th>--%>
             <th>备注</th>
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
               <%--     <td ><%#Eval("AwardsId") %> </td> --%>
                    <td ><%#Eval("AwardsName") %></td> 
                    <td ><%#Eval("DateStamp") %></td>  
                    <td ><%#Eval("YesterdayTotal") %></td>  
                    <td ><%#Eval("TodayTotal") %></td>  
                    <td ><%#Eval("AllTotal") %></td>  
                    <td ><%#Eval("BackTotal") %></td>  
                    <%--<td ><%#Eval("AwardsType") %></td>--%>  
                    <td ><%#Eval("PrizeName") %></td>  
                    <%--<td ><%#Eval("Angle") %></td>  
                    <td ><%#Eval("PresetValue") %></td>  
                    <td ><%#Eval("CreateTime") %></td>  
                    <td ><%#Eval("UpdateTime") %></td>  
                    <td ><%#Eval("StatusId") %></td>  --%>
                    <td ><%#Eval("Remark") %></td>  
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
                   <%-- <td>
                        <%# DataBinder.Eval(Container.DataItem, "AwardsId")%>
                    </td>--%>
                     <td>
                        <%# DataBinder.Eval(Container.DataItem, "AwardsName")%>
                    </td>
                     <td>
                        <%# DataBinder.Eval(Container.DataItem, "DateStamp")%>
                    </td>
                     <td >
                          <%# DataBinder.Eval(Container.DataItem, "YesterdayTotal")%>

                     </td>  
                     <td>
                        <%# DataBinder.Eval(Container.DataItem, "TodayTotal")%>
                    </td>
                     <td>
                        <%# DataBinder.Eval(Container.DataItem, "AllTotal")%>
                    </td>
                     <td>
                        <%# DataBinder.Eval(Container.DataItem, "BackTotal")%>
                    </td>
                    <%-- <td>
                        <%# DataBinder.Eval(Container.DataItem, "AwardsType")%>
                    </td>--%>
                     <td>
                          <asp:TextBox ID="txtEditPrizeName"  class="form-control"  Text='<%# DataBinder.Eval(Container.DataItem,"PrizeName") %>' runat="server"></asp:TextBox>
                    </td>
                    <%-- <td>
                        <%# DataBinder.Eval(Container.DataItem, "Angle")%>
                    </td>
                     <td>
                        <asp:TextBox ID="txtEditPresetValue"  class="form-control"  Text='<%# DataBinder.Eval(Container.DataItem,"PresetValue") %>' runat="server"></asp:TextBox>
                    </td>
                     <td>
                        <%# DataBinder.Eval(Container.DataItem, "CreateTime")%>
                    </td>
                     <td>
                        <%# DataBinder.Eval(Container.DataItem, "UpdateTime")%>
                    </td>
                     <td>
                        <%# DataBinder.Eval(Container.DataItem, "StatusId")%>
                    </td>--%>
                     <td>
                        <%# DataBinder.Eval(Container.DataItem, "Remark")%>
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
