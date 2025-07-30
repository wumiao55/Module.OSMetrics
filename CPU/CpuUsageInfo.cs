namespace Module.OSMetrics.CPU
{
    public class CpuUsageInfo
    {
        /// <summary>
        /// CPU核数
        /// </summary>
        public int CoreCount { get; set; }

        /// <summary>
        /// CPU使用率
        /// </summary>
        public double UsagePercent { get; set; }
    }
}
