<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminPassCode.aspx.cs" Inherits="MyAdmin_PassCode_AdminPassCode" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>
<!DOCTYPE html>
<html>
<head id="Head2" runat="server">
    <title>用户管理-ESmartWave</title> 
   <uc1:common runat="server" ID="common" />
  
    <script type="text/javascript">
        var $Id = function (id) { return document.getElementById(id); }
        $(function () {

        });
        $(function () {

            $("#body-colum").find("tr").each(function (index, element) {
                $(element).addClass("colum-" + index);

                $(".colum-" + index).find("td").each(function (index, element) {
                    $(element).addClass("colum-index-" + index);
                });
            });

            var _Rows = '<%=RowsName%>'.split(';');
            var _hideRows = $("#HiddenFieldNum").val().split(',');

            for (var i = 0; i < _Rows.length; i++) {
                if (_Rows[i].length > 0 && _hideRows.indexOf(i.toString()) < 0) {
                    $('#rowsBox' + i).iCheck('check');
                } else {
                    $(".rows-index-" + i + ",.colum-index-" + i).hide();
                }
            }

            $('.rows-box').on('ifChanged', function (event) {

                var num = $("#HiddenFieldNum").val().split(",");

                if (event.target.checked) {
                    $(".rows-index-" + event.target.value + ",.colum-index-" + event.target.value).show();
                    for (var i = 0; i < num.length; i++) {
                        if (num[i] == event.target.value || num[i] == "")
                            num.splice(i, 1);
                    }
                    $("#HiddenFieldNum").val(num.join(","));
                }
                else {

                    $(".rows-index-" + event.target.value + ",.colum-index-" + event.target.value).hide();
                    num.push(event.target.value);
                    $("#HiddenFieldNum").val(num.join(","));
                }
            }).iCheck({
                checkboxClass: 'icheckbox_square-green',
                increaseArea: '0%' // optional
            });

            $("#select_btn").click(function () {

                if ($("#checkSelect").css("display") == "table") {
                    $("#checkSelect").css("display", "none");
                }
                else {
                    $("#checkSelect").css("display", "table");
                }
            })

            $("#checkbox_btn").click(function () {
                if ($("#checkRows").css("display") == "table") {
                    $("#checkRows").css("display", "none");
                }
                else {
                    $("#checkRows").css("display", "table");
                }
            })

        })
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
                        激活码管理
                    </div> 
                    <div>
                        
                        <table id="checkSelect" width="100%" class="table table-bordered"    >
                              <tr> 
                                  <td>   
                                      搜索 </td>
                                  
                                  <td><asp:TextBox ID="tbCheckName" runat="server"   CssClass="form-control" ></asp:TextBox> </td> 
                                   <td>   <asp:DropDownList ID="DropDownListName" runat="server"   CssClass="form-control"></asp:DropDownList>
                                       
                                  </td>
                                 
                             
                                 <td style="text-align:right">
                                        订单时间：  </td> 
                                         <td style="width:150px;">
                                         <asp:TextBox ID="tbSt1" runat="server"   CssClass="form-control" style="width:150px;"  placeholder="开始时间" onclick="layui.laydate({elem: this, istime: false, format: 'YYYY-MM-DD'})"></asp:TextBox>
                                         </td>
                                       <td style="width:50px;">至 </td> 
                                      <td style="width:150px;"><asp:TextBox  CssClass="form-control"   placeholder="结束时间" style="width:150px;"  onclick="layui.laydate({elem: this, istime: false, format: 'YYYY-MM-DD'})" ID="tbSt2" runat="server" ></asp:TextBox>

                                    </td> 
                                       <td><asp:Button ID="Button2" runat="server" Text="搜 索" CssClass="btn btn-success"  OnClick="Button1_Click" />
                                     <asp:Button ID="Button3" runat="server" Text="导 出" Visible="false"  CssClass="btn btn-success" OnClick="Button2_Click" />
                                   </td> 
                              </tr> 
                        </table>

                       
                         <div class="gallerys">
                        <table id="sample-table-2" class="table table-striped table-bordered table-hover">
                            <thead>
                                <tr> 
                                    <%=GetTitle() %> 
                                    <%--<th>操作</th>--%>
                                </tr>
                            </thead>
                            <tbody id="body-colum">
                                <asp:Repeater runat="server" ID="menuList">
                                    <ItemTemplate>
                                        <tr >
                                          <td >
                                             <span class="label label-sm label-warning"><%#Eval("ID") %></span>
                                          </td>
                                          <td><%#Eval("Codes").ToString().Length<5 ?"":Eval("Codes").ToString().Substring(0,Eval("Codes").ToString().Length-5)+"*****" %></td>
                                          <td><%#Eval("CodeIndex") %></td>
                                          <td><%#Eval("CodeName") %></td>
                                          <td><%#Eval("OpenId").ToString().Length<5 ?"":Eval("OpenId").ToString().Substring(0,Eval("OpenId").ToString().Length-5)+"*****" %></td>
                                          <td><%#Eval("StatusId") %></td>
                                          <td><%#Eval("CreateTime") %></td>
                                          <td><%#Eval("ActiveTime") %></td>
                                          <td><%#Eval("Mob").ToString().Length<5 ?"":Eval("Mob").ToString().Substring(0,Eval("Mob").ToString().Length-5)+"*****"%></td> 
                                          <td><%#Eval("ActiveIp") %></td>
                                          <td><%#Eval("EventId").ToString()%></td>
                                          <td><%#Eval("CustomerId").ToString()%></td>
                                          <td><%#Eval("Notes") %></div></td>
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

