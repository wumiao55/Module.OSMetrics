namespace Module.OSMetrics.Memory
{
    public class MemoryUsageInfo
    {
        /// <summary>
        /// 总内存(单位:GiB)
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// 已使用内存(单位:GiB)
        /// </summary>
        public double Used { get; set; }

        /// <summary>
        /// 内存使用率
        /// </summary>
        public double UsagePercent { get; set; }
    }
}
