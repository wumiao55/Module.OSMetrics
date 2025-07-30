namespace Module.OSMetrics.Disk
{
    public interface IDiskUsage
    {
        /// <summary>
        /// 获取当前磁盘信息
        /// </summary>
        /// <returns></returns>
        Task<List<DiskUsageInfo>> GetCurrentInfoAsync();
    }
}
