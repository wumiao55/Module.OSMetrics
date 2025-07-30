namespace Module.OSMetrics.Memory
{
    public interface IMemoryUsage
    {
        /// <summary>
        /// 获取当前内存信息
        /// </summary>
        /// <returns></returns>
        Task<MemoryUsageInfo> GetCurrentInfoAsync();
    }
}
