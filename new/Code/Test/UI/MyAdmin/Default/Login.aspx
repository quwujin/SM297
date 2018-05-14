<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" EnableEventValidation="false" %>

<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>


<!DOCTYPE html><html>
<head runat="server">
  
    <title>业务管理系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
 
      <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
 
    <uc1:common runat="server" ID="common" />

<script language="javascript" >
	function login()
	{
		if($("#username").val()=="")
		{
			$("#err").html("***提示:账号不能为空");
			$("#username").focus();
			setTimeout('$("#err").html("")',1500);
			return false;
		}
		if($("#password").val()=="")
		{
			$("#err").html("***提示：密码不能为空");
			$("#password").focus();
			setTimeout('$("#err").html("")',1500);
			return false;
		}
		 
	}
</script>


</head>
<body >
    <form id="form1" runat="server">
 
 

        
<body style="background:#39a7d7;">
	<br /><br /><br /><br /><br />
	<div class="row">
		<div class="col-xs-10 col-xs-offset-1 col-sm-8 col-sm-offset-2 col-md-4 col-md-offset-4">
			<div class="login-panel panel panel-default">
				<div class="panel-heading"><h2 style="font-size:18px;"><%=title %>_管理系统</h2></div>
				<div class="panel-body">
					<form role="form">
						<fieldset>
							<div class="form-group">
                                  <asp:TextBox ID="txtUserName" runat="server"  class="form-control"  autofocus="" placeholder="账号" ></asp:TextBox>
                                  <asp:RequiredFieldValidator 
                  ControlToValidate="txtUserName" CssClass="titlebai" Display="Dynamic"
                  ErrorMessage="没有账号怎么登录呢!" ID="RequiredFieldValidator1" runat="server" style="color:#D72C10;   padding:2px 10px;  "></asp:RequiredFieldValidator>


							</div>

							<div class="form-group">
				
                                 <asp:TextBox ID="txtPassWord" TextMode="Password" runat="server" class="form-control" placeholder="密码" ></asp:TextBox>

                                  <asp:RequiredFieldValidator 
                  ControlToValidate="txtPassWord" CssClass="titlebai" Display="Dynamic"
                  ErrorMessage="密码也是要填的哦!" ID="RequiredFieldValidator2" runat="server" style="   color:#D72C10; padding:2px 10px; "></asp:RequiredFieldValidator>
							</div>
                        
                            <div class="form-group">
				
                                 <asp:TextBox ID="safecode" TextMode="Password" runat="server" class="form-control" placeholder="安全码" ></asp:TextBox>

                                  <asp:RequiredFieldValidator 
                  ControlToValidate="txtPassWord" CssClass="titlebai" Display="Dynamic"
                  ErrorMessage="安全码也是要填的哦!" ID="RequiredFieldValidator3" runat="server" style="   color:#D72C10; padding:2px 10px; "></asp:RequiredFieldValidator>
							</div>
                            <div id="loginerr" runat="server"></div>
						 <asp:Button ID="Button1" runat="server" Text="登录系统" class="btn btn-primary" OnClick="Button1_Click1" />
							 
						</fieldset>
                     
					</form>
				</div>
                  
			</div>
		</div><!-- /.col-->
	</div><!-- /.row -->	
	 <div style="color:#fff; text-align:center">
         内容系统，请勿外泄

	 </div>
		

	
	<script>
	    !function ($) {
	        $(document).on("click", "ul.nav li.parent > a > span.icon", function () {
	            $(this).find('em:first').toggleClass("glyphicon-minus");
	        });
	        $(".sidebar span.icon").find('em:first').addClass("glyphicon-plus");
	    }(window.jQuery);

	    $(window).on('resize', function () {
	        if ($(window).width() > 768) $('#sidebar-collapse').collapse('show')
	    })
	    $(window).on('resize', function () {
	        if ($(window).width() <= 767) $('#sidebar-collapse').collapse('hide')
	    })
	</script>	
 

    </form>
</body>
</html>
