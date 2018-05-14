<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prizeconfig.aspx.cs" Inherits="MyAdmin_Config_PrizeConfig" %>

<%@ Register Src="~/MyAdmin/inc/common.ascx" TagPrefix="uc1" TagName="common" %>
<!DOCTYPE html>
<html>
<head id="Head2" runat="server">
    <title>中奖率管理-ESmartWave</title> 
   <uc1:common runat="server" ID="common" />
  
</head>
<body style="background:#fff;">
    <form id="form1" runat="server">
  <div class="table-header">
        <strong runat="server" id="jx_tle">大转盘中奖配置管理</strong> 
  </div>  
		 <br />
			
		 
                <table width="90%" border="0" align="center" cellpadding="5" cellspacing="0"  class="table table-bordered" >
               
              <tr class="info">
                <td  height="40"  >奖项</td>
                <td height="35">中奖率</td>
                <td>每天中奖数</td>
                <td>总中奖数</td>
                <td>统计</td>
              </tr>
              <tr>
                <td width="393"  height="40" class="auto-style1"><div align="center" id="one_div" runat="server">一等奖</div></td>
                <td width="615" height="35"> 
                      &nbsp;  <asp:TextBox ID="txtzjl1" runat="server"></asp:TextBox>
                      %
                        &nbsp;&nbsp;&nbsp;&nbsp;                </td>
                   <td width="625"><asp:TextBox ID="txtzjl7" runat="server"></asp:TextBox></td>
                   <td width="625"><asp:TextBox ID="txtzjl8" runat="server"></asp:TextBox></td>
                   <td width="625"  style="color:#0099CC;font-size:16px"><span id="tday1" runat="server"></span></td>
              </tr>

              <tr>
                <td height="40" class="auto-style1"><div align="center" id="two_div" runat="server">二等奖</div></td>
                <td width="615" height="35">&nbsp;
                    <asp:TextBox ID="txtzjl2" runat="server"></asp:TextBox>
                    %
                    &nbsp;&nbsp;&nbsp;&nbsp;                </td>
                  <td width="625"><asp:TextBox ID="txtzjl9" runat="server"></asp:TextBox></td>
                  <td width="625"><asp:TextBox ID="txtzjl10" runat="server"></asp:TextBox></td>
                  <td width="625"  style="color:#0099CC;font-size:16px"><span id="tday2" runat="server"></span></td>
              </tr>
            
             <tr id="three_tr" runat="server" visible="false">
                <td height="40" class="auto-style1"><div align="center" id="three_div" runat="server">三等奖</div></td>
                <td width="615" height="35">&nbsp;
                    <asp:TextBox ID="txtzjl3" runat="server"></asp:TextBox>%
                    &nbsp;&nbsp;&nbsp;&nbsp;</td>
                  <td width="625"><asp:TextBox ID="txtzjl11" runat="server"></asp:TextBox></td>
                  <td width="625"><asp:TextBox ID="txtzjl12" runat="server"></asp:TextBox></td>
                  <td width="625"  style="color:#0099CC;font-size:16px"><span id="tday3" runat="server"></span></td>
              </tr>

              <tr id="four_tr" runat="server" visible="false">
                <td height="40" class="auto-style1"><div align="center" id="four_div" runat="server">四等奖</div></td>
                <td width="615" height="35">&nbsp;
                    <asp:TextBox ID="txtzjl4" runat="server"></asp:TextBox>
                    %
                    &nbsp;&nbsp;&nbsp;&nbsp;</td>
                  <td width="625"><asp:TextBox ID="txtzjl13" runat="server"></asp:TextBox></td>
                  <td width="625"><asp:TextBox ID="txtzjl14" runat="server"></asp:TextBox></td>
                  <td width="625"  style="color:#0099CC;font-size:16px"><span id="tday4" runat="server"></span></td>
              </tr>

              <tr id="five_tr" runat="server" visible="false">
                <td height="40" class="auto-style1"><div align="center" id="five_div" runat="server">五等奖</div></td>
                <td width="615" height="35">&nbsp;
                    <asp:TextBox ID="txtzjl5" runat="server"></asp:TextBox>
                    %
                    &nbsp;&nbsp;&nbsp;&nbsp;</td>
                  <td width="625"><asp:TextBox ID="txtzjl15" runat="server"></asp:TextBox></td>
                  <td width="625"><asp:TextBox ID="txtzjl16" runat="server"></asp:TextBox></td>
                  <td width="625"  style="color:#0099CC;font-size:16px"><span id="tday5" runat="server"></span></td>
              </tr>

              <tr id="six_tr" runat="server" visible="false">
                <td height="40" class="auto-style1"><div align="center" id="six_div" runat="server">六等奖</div></td>
                <td width="615" height="35">&nbsp;
                    <asp:TextBox ID="txtzjl6" runat="server"></asp:TextBox>%
                    &nbsp;&nbsp;&nbsp;&nbsp;</td>
                  <td width="625"><asp:TextBox ID="txtzjl17" runat="server"></asp:TextBox></td>
                  <td width="625"><asp:TextBox ID="txtzjl18" runat="server"></asp:TextBox></td>
                  <td width="625"  style="color:#0099CC;font-size:16px"><span id="tday6" runat="server"></span></td>
              </tr>

               <tr>
                <td height="40" class="auto-style1"><div align="center" id="seven_div" runat="server">参与奖</div></td>
                <td width="615" height="35">&nbsp;
                    <asp:TextBox ID="txtzjl19" runat="server"></asp:TextBox>%  
                    <br />
                    （参与奖自动结算，不用设置）                 </td>
                   <td width="625">&nbsp;</td>
                   <td width="625">&nbsp;</td>
                   <td width="625"  style="color:#0099CC;font-size:16px"><span id="tday7" runat="server"></span></td>
              </tr>
            
              <tr>
                <td height="35" class="auto-style1">&nbsp;</td>
                <td height="35"><asp:Button ID="Button1" runat="server" CssClass="btn btn-success" onclick="Button1_Click" 
                        Text="保存" />            
                &nbsp; *注：中奖率相加为100%</td>
				 <td width="625"></td>
				 <td width="625"></td>
				 <td width="625"> </td>
              </tr>
            </table> 
		<br>
		 
</form>
</body>
</html>
