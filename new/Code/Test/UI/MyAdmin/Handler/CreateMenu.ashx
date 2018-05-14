<%@ WebHandler Language="C#" Class="CreateMenu" %>

using System;
using System.Web;

public class CreateMenu : IHttpHandler {
    public Db.WxMenuDal bll = new Db.WxMenuDal();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        int bid = Common.TypeHelper.ObjectToInt(context.Request["_bid"], 0);
        int id = Common.TypeHelper.ObjectToInt(context.Request["_id"], 0);
        int orderid = Common.TypeHelper.ObjectToInt(context.Request["_orderid"], 0);
        string menuName = context.Request["_menuName"];
        string action = context.Request["action"];
        Model.WxMenuInfoModel m = new Model.WxMenuInfoModel();
        m.Bid = bid;
        m.Id = 0;
        m.Name = menuName;
        m.Keys = "";
        m.OrderId = orderid;
        m.Type = "";
        m.Url = "";
         switch(action)  
             { 
                case "add":
                   
                         
                        AddMenu(m);
                        break;
                case "update":
                        m.Id = id;
                        HttpContext.Current.Response.Write(bll.Updates(m)>0?"操作成功":"操作失败");
                break;

                case "list":
                        GetMenuList();
                break;
                 
                 case "count":
                GetCount(bid);
                break;
                 
             case  "gettxtpic":

                GetTxtPic();
                break;

             case "gettxtpics":

                GetTxtPics();
                break; 
                 
             case "del":
                Del(id);
                break;   
             }
    }

    /// <summary>
    /// 添加菜单
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public void AddMenu(Model.WxMenuInfoModel model) {
        if (bll.Add(model)>0)
        {
            HttpContext.Current.Response.Write("添加成功");
            
        }
        else
        {
            HttpContext.Current.Response.Write("操作失败");
        }
    }


    public void GetTxtPic()
    { 
          int vid = Common.TypeHelper.ObjectToInt(HttpContext.Current.Request["_id"], 0);
          Model.WxMessageModel mm = new Db.WxMessageDal().GetModel(vid);
          System.Text.StringBuilder str = new System.Text.StringBuilder();
          if (mm != null)
          {

              str.Append("  <div class=\"msglist\" style='margin:10px;'>");

              str.Append("   <table width=\"327\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"margin-top:20px;border:0\">");
              str.Append("       <tr>");
              str.Append("       <td height=\"168\" style='border:0' >");
              str.Append("          <table width=\"89%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"3\" style='margin:5px;'>");
              str.Append("         <tr>");
              str.Append("          <td height=\"32\"><div   style=\"word-wrap:break-word;width:290px\">"+mm.Title+"</div></td>");
              str.Append("        </tr>");
              str.Append("        <tr>");
              str.Append("         <td><div align=\"center\"><img src='../../"+mm.PicUrl+"'  width=\"290\" height=\"160\"></div></td>");
              str.Append("      </tr>");
              str.Append("      <tr>");
              str.Append("        <td height=\"68\" valign=\"top\"><div style=\"word-wrap:break-word;width:290px\">"+mm.Description+"</div></td>");
              str.Append("      </tr>");
              str.Append("    </table>");
              str.Append("  </td>");
              str.Append("  </tr>");
              str.Append("  </table>");
              str.Append("  </div><p style='clear:both' />");
             
          }
          HttpContext.Current.Response.Write(str);
    }


    public void GetTxtPics()
    {
        string vid = HttpContext.Current.Request["_id"];
        System.Data.DataTable dt = new Db.WxMessageDal().GetList("   and msgtype='多图文' and  groupcode='" + vid + "' and statusid=1 and orderid=0 ", "", "");
        System.Text.StringBuilder str = new System.Text.StringBuilder();
       
            str.Append("  <div class=\"msglist\" style='margin:10px;'>");

         if (dt.Rows.Count>0)
         {

            str.Append("   <table width=\"327\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"margin-top:20px;border:0\">");
            str.Append("       <tr>");
            str.Append("       <td height=\"168\" style='border:0' >");
            str.Append("          <table width=\"89%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"3\" style='margin:5px;'>");
            str.Append("         <tr>");
            str.Append("          <td height=\"32\"><div   style=\"word-wrap:break-word;width:290px\">" + dt.Rows[0]["title"] + "</div></td>");
            str.Append("        </tr>");
            str.Append("        <tr>");
            str.Append("         <td><div align=\"center\"><img src='../../" +dt.Rows[0]["picurl"]+ "'  width=\"290\" height=\"160\"></div></td>");
            str.Append("      </tr>");
            str.Append("    </table>");
            str.Append("  </td>");
            str.Append("  </tr>");
            str.Append("  </table>");
         }
        
             System.Data.DataTable dt2 =  new Db.WxMessageDal().GetList(" and groupcode='"+vid+"' and msgtype='多图文' and statusid=1 and orderid=100 ","","");
            for (int j = 0; j < dt2.Rows.Count; j++)
			{
			     str.AppendLine("<table width='89%' border='0' align='center' cellpadding='0' cellspacing='3' style='margin-top:15px; padding:5px; border:1px solid #ccc; '  > "); 
                  str.AppendLine("            <tr> "); 
                  str.AppendLine("              <td width='72%' align='left' style='padding:5px;'>  "+dt2.Rows[j]["Title"]+"  </td> "); 
                   str.AppendLine("               <td width='28%' style='padding:5px;'><img src='../../"+dt2.Rows[j]["picUrl"]+"'   width='60' height='60' ></td> "); 
                               
                              
                  str.AppendLine("      </table>   ");    
                              
			}
              
                              
            str.Append("  </div><p style='clear:both' />");

             HttpContext.Current.Response.Write(str);
    }
    
    

    public int Del() {
        int vid = Common.TypeHelper.ObjectToInt(HttpContext.Current.Request["id"], 0);
        if (vid>0)
        {
            bll.Del(vid, 0);
        }
        return 0;
    }

    public void GetCount(int bid)
    {
        int c = 0;
        string sql = " and  bid= " + bid;
        System.Data.DataTable dt = bll.GetList(sql);
        if (dt.Rows.Count>0)
        {
            c =  dt.Rows.Count ;
        }
        HttpContext.Current.Response.Write(c);
            
    }

    public void Del(int id)
    {
        if (bll.Del(id, 0)>0)
        {
            HttpContext.Current.Response.Write("操作成功");
        }
        else
        {
            HttpContext.Current.Response.Write("操作失败");
        }
    }
    
    public void GetMenuList() {

        System.Text.StringBuilder str = new System.Text.StringBuilder();
        System.Data.DataTable dt = bll.GetList(" and bid=0 ORDER BY ORDERID DESC ");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            str.AppendLine("<div class='menulist'>");
            str.AppendLine("<span class='titles'><img src='../img/jt.png'><strong><a href='Admin_Menu.aspx?id=" + dt.Rows[i]["Id"].ToString() + "'>" + dt.Rows[i]["Name"] + "</a></strong></span>");
                    str.AppendLine("<span class='creates'>");
                    str.AppendLine("<img src='../img/jia.png' class='addsub' bids='"+dt.Rows[i]["Id"]+"'>");
                    str.AppendLine("<img src='../img/xg.png' class='update' title='" + dt.Rows[i]["name"] + "' orderid='"+dt.Rows[i]["orderid"]+"' id='"+ dt.Rows[i]["Id"] + "'>");
                    str.AppendLine("<img src='../img/sc.png' class='del' id='"+dt.Rows[i]["Id"]+"'>");
                    str.AppendLine("</span>");
                str.AppendLine("</div>");

                System.Data.DataTable dt2 = bll.GetList(" and bid=" + dt.Rows[i]["id"] + " ORDER BY ORDERID DESC");
                for (int jj = 0; jj < dt2.Rows.Count; jj++)
                {
                    str.AppendLine("<div class='menulist2'>");
                    str.AppendLine("<span class='titles'><img src='../img/jt.png'><strong><a href='Admin_Menu.aspx?id="+dt2.Rows[jj]["Id"].ToString()+"'>" + dt2.Rows[jj]["Name"] + "</a></strong></span>");
                    str.AppendLine("<span class='creates'>");
                    str.AppendLine("<img src='../img/xg.png' class='update' title='" + dt2.Rows[jj]["name"] + "' orderid='" + dt2.Rows[jj]["orderid"] + "' id='" + dt2.Rows[jj]["Id"] + "'>");
                    str.AppendLine("<img src='../img/sc.png' class='del' id='" + dt2.Rows[jj]["Id"] + "'>");
                    str.AppendLine("</span>");
                    str.AppendLine("</div>");
                }
             
        }
        HttpContext.Current.Response.Write(str);
    }
        
    public bool IsReusable {
        get {
            return false;
        }
    }

}