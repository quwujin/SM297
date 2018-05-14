<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Ued.ascx.cs" Inherits="CrmAdmin_inc_Ued" %>
  <script type="text/javascript" charset="utf-8">
      window.UEDITOR_HOME_URL = "/UI/ued/";
    </script>
<script type="text/javascript" charset="utf-8" src="/UI/ued/ueditor.all.js"></script>

<% 
    if (this.TypeId==1)
	{
		 
    %>
<script src="/UI/ued/ueditor.config.js" type="text/javascript"></script>
<%
     }
     else
     {
         %>
<script src="/UI/ued/ueditor.config_s.js" type="text/javascript"></script>
  <%  
     }
    %>