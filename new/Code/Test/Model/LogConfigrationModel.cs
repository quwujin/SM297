
namespace Model
{
    public class LogConfigModel
    {
        /// <summary>
        /// 配置标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 配置内容 (更改的新值)
        /// </summary>
        public string Val { get; set; }

        /// <summary>
        /// 配置类型, 0:开关 1:内容 2:时间
        /// </summary>
        public int Types { get; set; }

        /// <summary>
        /// 每个tab的编号, 从左向右依次为1,2,3,4,5...
        /// </summary>
        public int TabId { get; set; }

        /// <summary>
        /// 当前节点的名称，eg  WarningLog，param
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 属性名称 eg:childDirectoryFormat, divideByType
        /// </summary>
        public string Attribute { get; set; }

        /// <summary>
        /// 从配置文件里面读出的值
        /// </summary>
        public string OldValue { get; set; }
    }
}
