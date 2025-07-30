namespace Module.OSMetrics.CPU
{
    public interface ICpuUsage
    {
        /// <summary>
        /// 获取当前CPU信息
        /// </summary>
        /// <param name="sampleIntervalMs">采样间隔(毫秒)</param>
        /// <returns></returns>
        Task<CpuUsageInfo> GetCurrentInfoAsync(int sampleIntervalMs = 500);
    }
}
