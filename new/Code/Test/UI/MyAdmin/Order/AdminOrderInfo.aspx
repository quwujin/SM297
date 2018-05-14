<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminOrderInfo.aspx.cs" Inherits="MyAdmin_AdminOrderInfo" %>
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
               
             $(".notes").click(function(){

                 var t=$(this).val();
                 if (t=="隐藏") { 
                     $(this).parent().next(".txt_notes").fadeOut(100);
                     $(this).val("增加备注");
                 }
                 else {
                     $(this).parent().next(".txt_notes").fadeIn(100);
                     $(this).val("隐藏");
                 }
             });

             $("#Button1").click(function(){
                 if($("#DropDownListName").val()=="选择搜索项"&&$("#tbCheckName").val()!="")
                 {
                     $('#txt_statu').html("请选择搜索项");
                     $('#myModal').modal("show");
                     return false;
                 }
             })

             $(".gallery-pic").click(function(){
                 $.openPhotoGallery(this); 
             })

         });


         function send(id, ty,num,reason) {
             
             var bool = window.confirm("是否确定提交修改？");
             if (bool == false) { return false; }

             $('#txt_statu').html("提交中，请耐心等待。。。");
             $('#myModal').modal("show");

             $.post("UpdateOrder.aspx", {
                 id: id,
                 ty: ty,
             }, function (data) {
                 if (data.indexOf("修改成功") != -1) {
                     $('#txt_statu').html(data); 
                     setTimeout(function(){ 

                         $('#myModal').modal("hide");

                         if(<%=AspNetPager1.CurrentPageIndex%>>1){
                             __doPostBack('AspNetPager1', '<%=AspNetPager1.Page%>');
                         }else{
                             $("#Button1").click();
                         }

                     },1100);
                     
                 } else {
                     $('#txt_statu').html(data);
                 }
            });
         }
         
         function adds(id){ 
             var times=$("#times"+id).val();
             var price=$("#price"+id).val();
             var nums=$("#nums"+id).val();
             var store=$("#store"+id).val();
             $.post("UpdateOrder.aspx", {
                 ty: "note",
                 id: id,
                 nums:nums,
                 store:store,
                 price:price,
                 times:times
             }, function (data) {
                 if (data.indexOf("成功") != -1) {
                     alert(data);
                     $("#notes_box_"+id).hide();
                     $("#shownote" + id).val("增加备注+"); 
                 } else {
                     alert(data);
                 }
             });
         }

         function SelectAllCheckboxes(spanChk) {
             var elm = document.forms[0];
             for (i = 0; i <= elm.length - 1; i++) {
                 if (elm[i].type == "checkbox" && elm[i].id != spanChk.id) {
                     if (elm.elements[i].checked != spanChk.checked)
                         elm.elements[i].click();
                 }
             }
         } 
         function DeleteAll(ty) {
             var bool = window.confirm("是否确定提交批量修改？");
             if (bool == false) { return false; }

             $('#txt_statu').html("提交中，请耐心等待。。。");
             $('#myModal').modal("show");

             var elm = document.forms[0];
             var msgIdList="";
             for (i = 0; i <= elm.length - 1; i++) {
                 if (elm[i].type == "checkbox" && elm.elements[i].checked&&elm[i].id!="checkAll"&& elm[i].name=="checkbox") {
                     var msgId = elm.elements[i].value;
                     msgIdList = msgIdList + msgId;
                     msgIdList = msgIdList + ",";
                 } 
             }

             $.post("UpdateOrder.aspx", {
                 ty: ty,
                 Delid: msgIdList,
             }, function (data) {
                 if (data.indexOf("成功") != -1) {
                     $('#txt_statu').html(data); 
                     setTimeout(function(){ 
                          
                         $('#myModal').modal("hide");

                         if(<%=AspNetPager1.CurrentPageIndex%>>1){
                             __doPostBack('AspNetPager1', '<%=AspNetPager1.Page%>');
                         }else{
                             $("#Button1").click();
                         }

                     },1100);
                     
                 } else {
                     $('#txt_statu').html(data);
                 }
             });
         } 

        $(function(){
        
            var menu_div=$("td").find("div.AddMenu");

            for(var i=0;i<menu_div.length;i++){
                var menu=menu_div[i];
                
                var imgurl=$(menu).attr("imgurl");//;
                var imgsign=$(menu).attr("imgsign");//
                var ordercode=$(menu).attr("ordercode");
                var ordertime=$(menu).attr("ordertime");
                var id=$(menu).attr("id");

                var menu_html="";
                menu_html+="<input type=\"button\" value=\"旋转 90 °C\" class='btn btn-info ' onclick=\"ImgApi({ imgurl: '"+imgurl+"', imgsign: '"+imgsign+"', ordercode: '"+ordercode+"', angle: '90' ,opt:'rotImg"+id+"',successid:'successid"+id+"',failid:'fail"+id+"',ordertime:'"+ordertime+"',cancelReason:<%=cancelReason%>,orderid:"+id+"});\"/><br/>";
                menu_html+="<input type=\"button\" value=\"旋转 180 °C\" class='btn btn-info ' onclick=\"ImgApi({ imgurl: '"+imgurl+"', imgsign: '"+imgsign+"', ordercode: '"+ordercode+"', angle: '180' ,opt:'rotImg"+id+"',successid:'successid"+id+"',failid:'fail"+id+"',ordertime:'"+ordertime+"',cancelReason:<%=cancelReason%>,orderid:"+id+"});\"/><br/>";
                menu_html+="<input type=\"button\" value=\"旋转 270 °C\" class='btn btn-info ' onclick=\"ImgApi({ imgurl: '"+imgurl+"', imgsign: '"+imgsign+"', ordercode: '"+ordercode+"', angle: '270' ,opt:'rotImg"+id+"',successid:'successid"+id+"',failid:'fail"+id+"',ordertime:'"+ordertime+"',cancelReason:<%=cancelReason%>,orderid:"+id+"});\"/><br/>";
                menu_html+="<input type=\"button\" value=\"云处理\" class=\"btn btn-purple  ocr\" onclick=\"ImgApi({ imgurl: '"+imgurl+"', imgsign: '"+imgsign+"', ordercode: '"+ordercode+"' ,opt:'rotImg"+id+"',successid:'successid"+id+"',failid:'fail"+id+"',ordertime:'"+ordertime+"',cancelReason:<%=cancelReason%>,orderid:"+id+"});\"/>";

                $(menu).html(menu_html);
            }
            $(".AddMenu input,img").css({width:"100%",marginTop:"5px"});
        })

        $(function(){
             
            $("#body-colum").find("tr").each(function(index,element){  
                $(element).addClass("colum-"+index);

                $(".colum-"+index).find("td").each(function(index,element){  
                    $(element).addClass("colum-index-"+index);
                }); 
            });  

            var _Rows='<%=RowsName%>'.split(';');
            var _hideRows=$("#HiddenFieldNum").val().split(',');

            for(var i=0;i<_Rows.length;i++){
                if(_Rows[i].length>0&&_hideRows.indexOf(i.toString())<0){ 
                    $('#rowsBox'+i).iCheck('check');
                }else{
                    $(".rows-index-"+i+",.colum-index-"+i).hide();
                }
            }
              
            $('.rows-box').on('ifChanged', function(event){
               
                var num=$("#HiddenFieldNum").val().split(",");

                if(event.target.checked){
                    $(".rows-index-"+event.target.value+",.colum-index-"+event.target.value).show();
                    for(var i=0;i<num.length;i++){
                        if(num[i]==event.target.value||num[i]=="")
                            num.splice(i, 1);
                    }
                    $("#HiddenFieldNum").val(num.join(",")); 
                }
                else{

                    $(".rows-index-"+event.target.value+",.colum-index-"+event.target.value).hide();
                    num.push(event.target.value);
                    $("#HiddenFieldNum").val(num.join(","));
                }
            }).iCheck({
                checkboxClass: 'icheckbox_square-green',
                increaseArea: '0%' // optional
            });

            $("#select_btn").click(function(){

                if( $("#checkSelect").css("display")=="table"){
                    $("#checkSelect").css("display","none");
                }
                else{
                    $("#checkSelect").css("display","table"); 
                }
            })

            $("#checkbox_btn").click(function(){

                if($("#checkRows").css("display")=="table"){
                    $("#checkRows").css("display","none");
                }
                else{
                    $("#checkRows").css("display","table");
                }
            })

        })
    </script>
    
    <link href="../js/icheck-1.x/skins/all.css" rel="stylesheet" />
    <script src="../js/icheck-1.x/icheck.js"></script>
    <script src="jquery-photo-gallery/jquery.photo.gallery.js"></script>
    <script src="OCRImgApi/js/GetImgApi.js?a=<%=DateTime.Now.ToString("yyyyMMddhhmmssffff") %>"></script>

    <style>
        .txt_notes{display:none; border:1px solid #ccc;padding:5px;width:200px; background:#efefef; margin:5px;text-align:center;}
        #calendarYear,#calendarMonth{width:50%;}
    </style>
    
</head>
<body style="background: #fff;">
    <form id="form1" runat="server"> 
        <asp:HiddenField ID="HiddenFieldNum" runat="server" Value="2,3,4,5,12,14,17,18,19,20,21,22,23,24,27,28,29,30,31,32,33,34"/>

        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
              <div class="modal-content">
                <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                  <h4 class="modal-title" id="myModalLabel">提示信息</h4>
                </div>
                <div class="modal-body">
                  <div class="form-group">
                    <h4 class="modal-title text-center" id="txt_statu"></h4>
                  </div>
                </div>
                <div class="modal-footer">
                  <button type="button" class="btn btn-default" data-dismiss="modal"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span>关闭</button>
                </div>
              </div>
            </div>
       </div>

        <div class="main-content" style="width: 99%">
            <div class="row">
                <div class="col-xs-12">
                    <div class="table-header">
                         订单列表
                    </div> 
                    <div>
                    <!-------------------搜索--------------------->
                        
                    <table id="checkSelect" width="100%" class="table table-bordered" >
                        <tr class="info"> 
                                <td class="col-lg-3 col-md-4 col-sm-12 col-xs-5"> 搜索 ：
                                    <asp:TextBox ID="tbCheckName" runat="server" placeholder="请输入搜索内容"  style="width:180px;" ></asp:TextBox> 
                                    <asp:DropDownList ID="DropDownListName" runat="server" style="width:150px;" ></asp:DropDownList>
                                </td>
                                  
                                 
                                <% if (Request["status"]=="100")
	                            {
		                        %>
                                <td class="col-lg-2 col-md-2 col-sm-2 col-xs-2"> 状态： 
                                    <asp:DropDownList ID="ddlState" runat="server" style="width:100px;" >
                                        <asp:ListItem Value="不限">不限</asp:ListItem>
                                        <asp:ListItem Value="0">未审核</asp:ListItem>
                                        <asp:ListItem Value="1">已审核</asp:ListItem>
                                        <asp:ListItem Value="-1">已作废</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                                                                        
                                <%
	                            } %>           
                                                                                         
                                <td class="col-lg-2 col-md-2 col-sm-2 col-xs-2"> 奖项： 
                                    <asp:DropDownList ID="ddlJx" runat="server" style="width:100px;" >
                                        <asp:ListItem Value="不限">不限</asp:ListItem> 
                                        <asp:ListItem Value="一等奖">一等奖</asp:ListItem> 
                                        <asp:ListItem Value="二等奖">二等奖</asp:ListItem> 
                                        <asp:ListItem Value="参与奖">参与奖</asp:ListItem> 
                                    </asp:DropDownList>
                                </td>
                                <td class="col-lg-2 col-md-2 col-sm-2 col-xs-2"> 是否调用OCR： 
                                    <asp:DropDownList ID="ddlIsOCR" runat="server" style="width:100px;" >
                                            <asp:ListItem Value="不限">不限</asp:ListItem> 
                                            <asp:ListItem Value="是">是</asp:ListItem> 
                                            <asp:ListItem Value="否">否</asp:ListItem> 
                                        </asp:DropDownList>
                                </td>
                       
                                <td class="col-lg-3 col-md-4 col-sm-12 col-xs-2"> 订单时间： 
                                 <asp:TextBox ID="tbSt1" runat="server"  Style="width:150px;"    placeholder="开始时间" onclick="layui.laydate({elem: this, istime: false, format: 'YYYY-MM-DD'})"></asp:TextBox>
                                -
                                <asp:TextBox   ID="tbSt2" runat="server"  Style="width:150px;" placeholder="结束时间" onclick="layui.laydate({elem: this, istime: false, format: 'YYYY-MM-DD'})"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="搜 索" CssClass="btn btn-sm btn-primary"  OnClick="Button1_Click" />
                                    <asp:Button ID="Button2" runat="server" Text="导 出" CssClass="btn btn-sm btn-success" OnClick="Button2_Click" />
                                </td> 
                            </tr>
                    </table>

                    <div class="table-header" id="checkbox_btn">
                         点我显示勾选列表 <span class="glyphicon glyphicon-plus"></span>
                    </div>

                    <table width="100%" id="checkRows" class="table table-bordered" style="display:none;">
                        <%=GetCheckRows() %>
                    </table>
                    <!-------------------搜索--------------------->

                    <div class="tabbable">
						<ul class="nav nav-tabs" id="myTab">
							<li <% if (Request["status"] == "100")
                                { %> class="active"<%} %>>
								<a    href="Adminorderinfo.aspx?status=100">
													 
									全部订单
                                    <span class="badge badge-success"><%=orderQty %></span>
								</a>
							</li>

							<li <% if (Request["status"] == "0")
                                { %> class="active"<%} %>>
								<a   href="Adminorderinfo.aspx?status=0">
									待审核
									<span class="badge badge-success"><%=orderQty0 %></span>
								</a>
							</li>

							<li <% if (Request["status"] == "1")
                                { %> class="active"<%} %>>
								<a   href="Adminorderinfo.aspx?status=1">
									已完成
									<span class="badge badge-success"><%=orderQty1 %></span>
								</a>
							</li>

                            <li <% if (Request["status"] == "-1")
                                { %> class="active"<%} %>>
								<a   href="Adminorderinfo.aspx?status=-1">
									已作废
									<span class="badge badge-success"><%=orderQty2 %></span>
								</a>
							</li>
						</ul>

						<div class="tab-content">
							<div id="home" class="tab-pane fade active in">
                               <!-------------------------订单主体--------------------->
                                                                              
                               <div class="gallerys">
                                   <table width="100%"  class="table table-bordered" >
                                    <tr>
                                        <td>
                                            <input type="checkbox" id="checkAll" onclick="javascript: SelectAllCheckboxes(this);"/>全选 &nbsp;&nbsp;&nbsp;
                                            <div style="float:right;">
                                                <button class="btn btn-sm btn-danger "  onclick="DeleteAll('del')"   id="delbt" visible="false"  runat="server">
												        <i class="icon-fire bigger-110"></i>
												        <span class="bigger-110  no-text-shadow" >批量删除</span>
											        </button>

                                                <button class="btn btn-sm btn-warning "  onclick="DeleteAll('zflist')"  id="upbt"  runat="server">
												    <i class="icon-fire bigger-110"></i>
												    <span class="bigger-110 no-text-shadow" >批量作废</span>
											    </button>
                                            </div>
                                                <button type="button" class="btn btn-sm btn-danger "  onclick="CloseOcr()" >
												    <i class="icon-fire bigger-110"></i>
												    <span class="bigger-110 no-text-shadow" >X 关闭旋转自动识别</span>
											    </button>
                                                <button class="btn btn-sm btn-success "  onclick="DeleteAll('recorded')" >
												    <i class="icon-fire bigger-110"></i>
												    <span class="bigger-110 no-text-shadow" >批量预审勾选订单</span>
											    </button>
                                                <button class="btn btn-sm btn-warning "  onclick="DeleteAll('recordedAll')" >
												    <i class="icon-fire bigger-110"></i>
												    <span class="bigger-110 no-text-shadow" >批量预审未录入订单</span>
											    </button>
                                        </td>
                                    </tr>
                                   </table>

                                   <table id="sample-table-2" class="table table-striped table-bordered table-hover">
                                    <thead>
                                        <tr> 
                                            <%=GetTitle() %>
                                            <th style="width:200px;">备注</th>
                                            <th style="display:none;">操作</th>
                                        </tr>
                                    </thead>

                                    <tbody id="body-colum">
                                        <asp:Repeater runat="server" ID="menuList">
                                            <ItemTemplate>
                                                <tr >
                                                    <td >
						                                <input id="Checkbox1" name="checkbox" type="checkbox" value="<%#Eval("id") %>" />
                                                        <span class="label label-sm label-warning"><%#Eval("ID") %></span>
                                                    </td>
                                                    <td > <%#Eval("OrderCode") %> </td>
                                                    <td ><%#Eval("OpenId") %></td>
                                                    <td ><%#Eval("NickName") %></td>
                                                    <td ><%#Eval("HeadImgurl") %></td>
                                                    <td >
                                                        <div style="width:240px; height:170px;background:#ccc;">
                                                            <div style="width:120px; height:170px; float:left;border:1px solid #fff;padding:3px; ">
                                                                <img src="<%#string.IsNullOrEmpty(Eval("NewImgurl").ToString())==false ? Eval("NewImgurl").ToString():Eval("FileName").ToString() %>" id="rotImg<%#Eval("id")%>" width="100" height="100"  class="gallery-pic" />
                                                                (<%#Eval("HashCount") %>) <a href="<%#Eval("SaveName").ToString()%>" target="_blank">查看原图</a>
                                                            </div>
                                                            <div class="AddMenu" style="width:120px; height:170px;border:1px solid #fff;padding:3px;float:right;"
                                                            imgurl="<%#string.IsNullOrEmpty(Eval("NewImgurl").ToString())==false ? Eval("NewImgurl").ToString().ToLower():Eval("FileName").ToString().ToLower() %>"
                                                            imgsign="<%#string.IsNullOrEmpty(Eval("NewImgsign").ToString())==false ? Eval("NewImgsign").ToString():Eval("Hashdata").ToString() %>"
                                                            ordercode="<%#Eval("OrderCode").ToString() %> " id="<%#Eval("id") %>"
                                                            ordertime="<%#Eval("CreateTime").ToString() %>"
                                                            >
                                                            </div> 
                                                        </div>
                                                    </td> 
                                                    <td ><%#Eval("Jx") %></td>
                                                    <td ><%#Eval("Jp") %></td>
                                                    <td ><%#SafeVal(Eval("Name").ToString()) %> </td>
                                                    <td ><%#SafeVal(Eval("Mob").ToString())%> </td> 
                                                    <td ><%#Eval("CreateTime") %></td>
                                                    <td >
                                                        <span class="btn btn-xs  btn-white" style="width:100px;">
                                                            <%#Eval("States").ToString()=="0"?"未审核":Eval("States").ToString()=="1"?"<span style='color:green'>已审核</span>":Eval("States").ToString()=="5"?"无法审核":"<span style='color:red'>作废</span>"  %>
                                                        </span> 
                                                        <br /><%#Eval("Note").ToString()==""?"":"<span class='btn btn-xs btn-danger' style='width:100px;margin-top:10px;'>"+Eval("Note")+"</span>" %>
                                                    </td>
                                                    <td ><%#Eval("Number") %></td>
                                                    <td ><%#Eval("PrizeCode").ToString().Length<5 ?"":Eval("PrizeCode").ToString().Substring(0,5)+"*****"%></td>
                                                    <td ><%#Eval("IDCard") %></td>
                                                    <td ><%#SafeVal(Eval("Code").ToString())%></td>
                                                    <td ><%#Eval("Sources").ToString()%></td>
                                                    <td ><%#Eval("RedPackMoney").ToString()%></td>
                                                    <td ><%#Eval("HbOrderCode") %></td>
                                                    <td ><%#Eval("Ip") %></td>
                                                    <td ><%#Eval("IpAddress") %></td>
                                                    <td ><%#Eval("Types") %> </td>
                                                    <td ><%#SafeVal(Eval("Adds").ToString())%></td>
                                                    <td ><%#Eval("MobHome") %></td>
                                                    <td ><%#Eval("Province") %></td>
                                                    <td ><%#Eval("City") %></td>
                                                    <td ><%#Eval("Area").ToString()=="1"?"男":"女" %></td>
                                                    <td ><%#Eval("Account") %></td>
                                                    <td ><%#Eval("UpdateTime") %></td>
                                                    <td ><%#Eval("Note") %></td>
                                                    <td ><%#Eval("Title") %></td>
                                                    <td > <%#Eval("Texts") %></td>
                                                    <td > <%#Eval("Age") %></td>
                                                    <td > <%#Eval("Tdate") %></td> 
                                                    <td > <%#Eval("Hashdata") %></td> 
                                          
                                                    <td><div class='list_btn'> <input type="button" value="增加备注+" class="notes btn-minier btn btn-info" id="shownote<%# Eval("ID").ToString() %>" /></div>

                                                       <div id='notes_box_<%# Eval("ID").ToString() %>' class="txt_notes" style="background:#fff;">

                                                                <div class="input-group">
                                                                    <div class="input-group-addon">门店</div>
                                                                    <input type="text" id='store<%# Eval("ID").ToString() %>' class="form-control" value="<%#Eval("Title") %>"  />
                                      
                                                                </div>


                                                                <div class="input-group">
                                                                    <div class="input-group-addon">金额</div>
                                                                    <input type="text" id='price<%# Eval("ID").ToString() %>' class="form-control" value="<%#Eval("Age") %>"  />
                                      
                                                                </div>

                                                                <div class="input-group">
                                                                    <div class="input-group-addon">流水</div>
                                                                    <input type="text" id='nums<%# Eval("ID").ToString() %>' class="form-control" value="<%#Eval("Texts") %>"  />
                                      
                                                                </div>

                                                                <div class="input-group">
                                                                    <div class="input-group-addon">购物时间</div>
                                                                    <input type="text" id='times<%# Eval("ID").ToString() %>' class="form-control" value="<%#Eval("Tdate") %>"   onclick="layui.laydate({ elem: this, istime: false, format: 'YYYY-MM-DD' })"  class="form-control" value="<%#Eval("Tdate").ToString()==""?Common.Fun.ConvertTo<DateTime>(Eval("CreateTime").ToString(),DateTime.Now).ToShortDateString():Eval("Tdate") %>"   />
                                      
                                                                </div>
                                          
                                                               <input type="button" value="保存" onclick="adds('<%# Eval("ID").ToString() %>    ')" class="btn btn-sm btn-success" />
                                                        </div>
                                                    </td>

                                                    <td style="display:none;">
                                                        <%#getMenu(Eval("States").ToString(),Eval("ID").ToString(),Eval("Note").ToString()) %> 
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
                                                <webdiyer:AspNetPager ID="AspNetPager1" CssClass="page" runat="server" OnPageChanged="AspNetPager1_PageChanged"
                                                    CurrentPageButtonPosition="Center" PageSize="30"
                                                    ShowCustomInfoSection="Right" PageIndexBoxType="TextBox" PageIndexBoxStyle="width:50px;"
                                                     ShowPageIndexBox="Auto"  ShowBoxThreshold="10" 
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

 





 