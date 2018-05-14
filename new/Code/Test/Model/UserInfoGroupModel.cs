using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public  class UserInfoGroupModel
    {
       public UserInfoGroupModel() {
           this.PerList = new List<PermissionModel>();
       }
       public int GroupId { get; set; }
       public string GroupName { get; set; }
       public List<PermissionModel> PerList { get; set; }
    }

   public class PermissionModel {
    
       public int PerId { get; set; }
       public int MenuId { get; set; }
       public int MenuBid { get; set; }
       public int GroupId { get; set; }
   }
}
