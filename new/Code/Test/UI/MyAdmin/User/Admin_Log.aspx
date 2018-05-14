<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin_Log.aspx.cs" Inherits="Admin_Log" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>
<!DOCTYPE html>
<html>
<head id="Head2" runat="server">
    <title>用户管理-ESmartWave</title> 
     <uc1:common runat="server" ID="common" />

    <script type="text/javascript">
         var $Id = function (id) { return document.getElementById(id); }
         $(function(){ 

         }); 
        
      
    </script>
    
    <link href="../js/icheck-1.x/skins/all.css" rel="stylesheet" />
    <script src="../js/icheck-1.x/icheck.js"></script> 
    <style>#calendarYear,#calendarMonth{width:50%;}</style>
</head>
<body style="background: #fff;">
    <form id="form2" runat="server"> 
        <asp:HiddenField ID="HiddenFieldNum" runat="server" Value=""/>
         

        <div class="main-content" style="width: 99%">
            <div class="row">
                <div class="col-xs-12">
                    <div class="table-header">
                        操作日志列表
                    </div> 
                    <div>
                          

                        <table id="checkSelect" width="100%" class="table table-bordered"   >
                              <tr> 
                                  <td>   
                                      搜索
                                    
                                       
                                  </td>

                                  <td>
                                       <asp:TextBox ID="txtUserName" runat="server" ></asp:TextBox>
                                  </td>
                                 
      
                                 <td style="text-align:right">
                                        日志时间：  </td> 
                                         <td style="width:150px;">
                                         <asp:TextBox ID="tbSt1" runat="server"   CssClass="form-control" style="width:150px;"  placeholder="开始时间" onclick="layui.laydate({elem: this, istime: false, format: 'YYYY-MM-DD'})"></asp:TextBox>
                                         </td>
                                       <td style="width:50px;">至 </td> 
                                      <td style="width:150px;"><asp:TextBox ID="tbSt2" CssClass="form-control"   placeholder="结束时间" style="width:150px;"  onclick="layui.laydate({elem: this, istime: false, format: 'YYYY-MM-DD'})"  runat="server" ></asp:TextBox>

                                    </td> 
                                       <td><asp:Button ID="Button3" runat="server" Text="搜 索" CssClass="btn btn-success"  OnClick="Button1_Click" />
                                   </td> 


                              
                               
                              </tr> 
                        </table>
                   
                         <div class="gallerys">
                        <table id="sample-table-2" class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr> 
                                    
                                    <th>ID</th>
                                      
                                    <th>登录时间</th>
                                     <th>登录IP</th>
                                     <th>说明</th>
                                     <th>登录人 </th>
                                </tr>
                            </thead>
                            <tbody id="body-colum">
                                <asp:Repeater runat="server" ID="menuList">
                                    <ItemTemplate>
                                        <tr >
                                            <td >
                                                <span class="label label-sm label-warning"><%#Eval("LogID") %></span>
                                            </td>
                                            <td > <%#Eval("LoginTime") %> </td>
                                            <td ><%#Eval("LoginIp") %></td>
                                          
                                            <td ><%#Eval("Notes") %></td>
                                            <td ><%#Eval("UserName") %></td>
                                         
                                       
                                         
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

 





 