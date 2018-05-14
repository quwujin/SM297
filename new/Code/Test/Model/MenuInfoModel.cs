using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class MenuInfoModel
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 菜单名
        /// </summary>
        public string MenuName { get; set; }
        
        /// <summary>
        /// 菜单地址
        /// </summary>
        public string MenuUrl { get; set; }

        /// <summary>
        /// 菜单排序
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 菜单父级ID
        /// </summary>
        public int Bid { get; set; }


        /// <summary>
        /// 权限ID
        /// </summary>
        public int PerId { get; set; }

        /// <summary>
        /// 组ID
        /// </summary>
        public int GroupId { get; set; }


        /// <summary>
        /// 状态 1显示，0 隐藏
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// 组名
        /// </summary>
        public string GroupName { get; set; }

    }
}
