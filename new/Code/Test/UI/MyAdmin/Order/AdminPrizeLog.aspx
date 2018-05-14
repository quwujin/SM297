<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminPrizeLog.aspx.cs" Inherits="MyAdmin_AdminPrizeLog" %>
 <%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>
<!DOCTYPE html>
<html>
<head id="Head2" runat="server">
    <title>用户管理-ESmartWave</title> 
    <uc1:common runat="server" ID="common" /> 
 

  
     <script type="text/javascript">
         var $j = jQuery.noConflict();
         var $Id = function (id) { return document.getElementById(id); }
         function send(id, ty) {
             var bool = window.confirm("是否确定提交修改？");
             if (bool == false) { return false; }
             $j.post("Admin_movie_orders.ashx", {
                 id: id,
                 ty: ty
             }, function (data) {
                 if (data.indexOf("修改成功") != -1) {
                     alert(data);
                     if(<%=AspNetPager1.CurrentPageIndex%>>1){
                        __doPostBack('AspNetPager1', '<%=AspNetPager1.Page%>');
                     }else{
                         window.location.href = window.location.href;
                     }
                 } else {
                     alert(data);
                 }
            });
         }
         
    </script>
    <script src="jquery-photo-gallery/jquery.js"></script>
    <script src="jquery-photo-gallery/jquery.photo.gallery.js"></script>
</head>
<body style="background: #fff;">
    <form id="form2" runat="server"> 
        <div class="main-content" style="width: 99%">
            <div class="row">
                <div class="col-xs-12">
                    <div class="table-header">
                        抽奖订单列表
                    </div> 
                    <div>
                        <table width="100%"  class="table table-bordered" >
                              <tr> 
                                  <td>手机号：<asp:TextBox ID="tbMob" runat="server" ></asp:TextBox></td> 
                                  <%--<td>搜索递归城市：<asp:TextBox ID="tbCity" runat="server" CssClass="input"></asp:TextBox></td>--%> 
                                  <td>
                                      状态：<asp:DropDownList ID="ddlState" runat="server">
                                           <asp:ListItem Value="不限">不限</asp:ListItem>
                                           <asp:ListItem Value="0">未审核</asp:ListItem>
                                           <asp:ListItem Value="1">已审核</asp:ListItem>
                                           <asp:ListItem Value="-1">已作废</asp:ListItem>
                                            </asp:DropDownList>
                                  </td>    
                              
                                      <td>
                           订单时间：</td>
                            <td><asp:TextBox ID="tbSt1" runat="server" CssClass="form-control"   placeholder="开始时间" onclick="layui.laydate({elem: this, istime: false, format: 'YYYY-MM-DD'})"></asp:TextBox>
                                </td>
                                <td>
                                <asp:TextBox   ID="tbSt2" runat="server" CssClass="form-control" placeholder="结束时间" onclick="layui.laydate({elem: this, istime: false, format: 'YYYY-MM-DD'})"></asp:TextBox>
                                </td>
                                   <td>
                                     <asp:Button ID="Button1" runat="server" Text="搜 索" CssClass="btn btn-success"  OnClick="Button1_Click" />
                                     <asp:Button ID="Button2" runat="server" Text="导 出 EXCEL" CssClass="btn btn-info" OnClick="Button2_Click" />
                                   </td> 
                              </tr>  
                        </table>
                         <div class="gallerys">
                        <table id="sample-table-2" class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr> 
                                    <th>编号</th>
                                    <th>订单号</th>
                                    <th>OpenId</th> 
                                    <th>手机号</th> 
                                    <th>奖项</th>
                                    <th>奖品</th>
                                    <th>Ip</th>
                                    <th>省</th>
                                    <th>市</th>
                                    <th>地址</th>
                                    <th>串码</th>
                                    <th>备注</th>  
                                    <th>订单时间</th>    
                                    <th>状态</th>
                                    <th>操作</th> 
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="menuList">
                                    <ItemTemplate>
                                        <tr>
                                            <td class="hidden-480">
                                                <span class="label label-sm label-warning"><%#Eval("ID") %></span>
                                            </td>
                                            <td> <%#Eval("OrderCode") %> </td>
                                            <td><%#Eval("OpenId") %></td>
                                            <td><%#Eval("Mob") %> </td>
                                            <td><%#Eval("Jx") %></td>
                                            <td><%#Eval("Jp") %></td>
                                            <td><%#Eval("Ip") %></td>
                                            <td><%#Eval("Pros") %></td>
                                            <td><%#Eval("City") %></td>
                                            <td><%#Eval("Adds") %></td> 
                                            <td><%#Eval("Code") %></td>
                                            <td><%#Eval("Note") %></td>
                                            <td><%#Eval("CTime") %></td>
                                            <td><%#Eval("States").ToString()=="0"?"未审核":Eval("States").ToString()=="1"?"已审核":"<span style='color:red'>作废</span>"  %></td>
                                            <td><%#getMenu(Eval("States").ToString(),Eval("ID").ToString()) %></td>
                                           
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
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                            
                        </table>
                       </div>
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
                    </div>
                </div>
            </div>
        </div>

          <script>
              layui.use('laydate', function () {
                  var laydate = layui.laydate;


              });
    </script>


    </form>
</body>
</html>

 





 