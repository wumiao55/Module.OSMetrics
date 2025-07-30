namespace Module.OSMetrics.OS
{
    public class ServerBaseInfo
    {
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 服务器IP集合
        /// </summary>
        public List<string> IPs { get; set; } = new List<string>();

        /// <summary>
        /// 操作系统
        /// </summary>
        public string SystemName { get; set; } = string.Empty;

        /// <summary>
        /// 系统架构
        /// </summary>
        public string SystemArchitecture { get; set; } = string.Empty;

        /// <summary>
        /// 系统启动时间
        /// </summary>
        public DateTime? SystemStartTime { get; set; } = null;

        /// <summary>
        /// 系统运行时间(单位:小时)
        /// </summary>
        public double? SystemUptime { get; set; } = null;

        /// <summary>
        /// 系统运行时间描述
        /// </summary>
        public string SystemUptimeDesc { get; set; } = string.Empty;
    }
}
