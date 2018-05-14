using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

 
    public class PageBase : System.Web.UI.Page
    {
        public string SessionName = "UserSysSession"; 

        public Model.UserInfoModel userseesion = null;

        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
            try
            {
                #region 检查登录状态
                userseesion = GetSession();

                if (userseesion == null)
                {
                    Response.Write("<script language='javascript'>self.parent.location.href='../default/login.aspx'</script>");
                    Response.End();
                    return;
                }
                #endregion

                #region 检查页面访问权限
                //var AbsolutePath = Request.Url.AbsolutePath;

                //if (new Db.MenuDal().CheckGroupIdByAbsolutePath(AbsolutePath, userseesion.GroupId) <= 0)
                //{
                //    //无该页面权限
                //    Response.Write("<script language='javascript'>self.parent.location.href='../default/login.aspx'</script>");
                //    Response.End();
                //    return;
                //}
                #endregion
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>self.parent.location.href='../default/login.aspx'</script>");
                Response.End();
                return;
                
            }

        }
         
          
        #region 存取Session
        public void SetSession(Model.UserInfoModel orderSession)
        {
            Model.UserInfoModel OrderUser = new Model.UserInfoModel();
            OrderUser.UserId = orderSession.UserId;
            OrderUser.UserName = orderSession.UserName;
            OrderUser.LevelId = orderSession.LevelId;
            OrderUser.GroupId = orderSession.GroupId;
            OrderUser.GroupName = orderSession.GroupName;
            OrderUser.RealName = Common.Des.Encode(System.Web.HttpUtility.HtmlEncode(OrderUser.UserId + OrderUser.UserName + OrderUser.LevelId + OrderUser.GroupId + OrderUser.GroupName));
            Session[SessionName] = OrderUser;
        } 

        public Model.UserInfoModel GetSession()
        {
            Model.UserInfoModel OrderUser = new Model.UserInfoModel();
            if (Session[SessionName] == null)
            {
                return null;
            }
            OrderUser = (Model.UserInfoModel)Session[SessionName];
            if (System.Web.HttpUtility.UrlDecode(Common.Des.Decode(OrderUser.RealName)).Length <= 0)
            {
                return null;
            } 
            return OrderUser;
        }
        public void ClearData()
        {
            //清除某个Session
            Session[SessionName] = null;
            Session.Remove(SessionName);
        }

        /// <summary>
        /// 清除浏览器缓存
        /// </summary>
        public void ClearClientPageCache()
        {
            //清除浏览器缓存
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.Cache.SetNoStore();
        }
        #endregion 

        public static string SafeVal(string val)
        {
            if (val == null || val == "")
            {
                return "";
            }
            if (val.Length == 2)
            {
                val = val.Substring(0, 1) + "*";
            }
            else if (val.Length == 1)
            {
                val = "*";
            }
            else if (val.Length == 3)
            {
                val = val.Substring(0, 1) + "**";
            }
            else if (val.Length == 4)
            {
                val = val.Substring(0, 1) + "**" + val.Substring(val.Length - 1, 1);
            }
            else if (val.Length == 5)
            {
                val = val.Substring(0, 2) + "**" + val.Substring(val.Length - 1, 1);
            }
            else if (val.Length == 6)
            {
                val = val.Substring(0, 2) + "**" + val.Substring(val.Length - 2, 2);
            }
            else if (val.Length == 7)
            {
                val = val.Substring(0, 2) + "***" + val.Substring(val.Length - 2, 2);
            }
            else if (val.Length == 8)
            {
                val = val.Substring(0, 3) + "****" + val.Substring(val.Length - 2, 2);
            }
            else if (val.Length == 9)
            {
                val = val.Substring(0, 4) + "****" + val.Substring(val.Length - 1, 1);
            }
            else if (val.Length == 10)
            {
                val = val.Substring(0, 3) + "****" + val.Substring(val.Length - 3, 3);
            }
            else if (val.Length == 11)
            {
                val = val.Substring(0, 3) + "****" + val.Substring(val.Length - 4, 4);
            }
            else if (val.Length > 11)
            {
                val = val.Substring(0, 4) + "****" + val.Substring(val.Length - 4, 4);
            }
            return val;
        }

    }
 