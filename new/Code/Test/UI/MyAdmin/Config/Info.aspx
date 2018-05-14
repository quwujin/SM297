<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Info.aspx.cs" Inherits="MyAdmin_Zp_Info" %>
<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <uc1:common runat="server" ID="common" />
  
    <link href="../ued/themes/default/css/umeditor.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../ued/third-party/jquery.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../ued/umeditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="../ued/umeditor.min.js"></script>
    <script type="text/javascript" src="../ued/lang/zh-cn/zh-cn.js"></script>
    <style>
        .page-header input {
            display:block;
            margin: auto;
        }
    </style>
</head>
<body style="background:#fff;">
    <form id="form1" runat="server">


        <div class="main-content" style="width: 99%">
            <div class="row">
                <div class="col-xs-12">
                    <div class="table-header">
                        活动说明管理
                    </div> 
    

                    <div class="tabbable" style="margin-top:30px">
											<ul class="nav nav-tabs" id="myTab">
												 

                                                <asp:Repeater runat="server" ID="infoList">
                                                     <ItemTemplate>
                                                
												                <li  <%# Request["id"]==Eval("Id").ToString()?"class='active info'":"" %> >
													                <a  href="info.aspx?id=<%#Eval("id") %>">
														                <%#Eval("Title") %>
														                
													                </a>
												                </li>
                                                    </ItemTemplate>
                                              </asp:Repeater> 

											   <li  <%=Common.Fun.ConvertTo<int>(Request["id"], 0) == 0 ? "class='active info'" : "" %> >
													                <a  href="info.aspx?id=0">
														                增加+
													                </a>
												 </li>
											</ul>

								 
										    <br>
									    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="table table-bordered">
                                          <tr>
                                            <td width="6%" height="34"><asp:HiddenField ID="hidvid" runat="server" />标 题 ：</td>
                                            <td width="75%"><asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" MaxLength="30" placeholder="名称："></asp:TextBox></td>
                                            <td width="19%"><div align="center">预览</div></td>
                                          </tr>
                                          <tr>
                                            <td>标 题 ：</td>
                                            <td><span style="margin-top: 30px;">
                                              <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" MaxLength="30" TextMode="MultiLine" Style="width: 100%; height: 640px;"></asp:TextBox>
                                            </span></td>
                                            <td width="19%" rowspan="2"><div style="width:315px; height:635px; background:url('../img/iphone.png'); position:relative">
                                              <div style="background:#fff;width:280px; height:500px; position:absolute; top:70px; left:20px;">
                                                <iframe  src="infoshow.aspx?id=<%=Request["id"] %>" style="width:280px; height:500px;"></iframe>
                                              </div>
                                            </div></td>
                                          </tr>
                                          <tr>
                                            <td>&nbsp;</td>
                                            <td><span style="margin-top: 30px;">
                                              <asp:Button runat="server" ID="sub" Text="保存" CssClass="btn btn-success" OnClick="sub_Click" />                                              
                                            </span>
											
											 <script>  var um = UM.getEditor('txtNotes');
                                              </script></td>
                                          </tr>
                                        </table>	 
						 
                    


				  </div>



       
                                                                   


                    </div>
                </div>
            </div>
    </form>
</body>
</html>
