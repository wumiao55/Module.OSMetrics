namespace Module.OSMetrics.Network
{
    public interface ISocketUsage
    {
        /// <summary>
        /// 获取当前Socket信息
        /// </summary>
        /// <returns></returns>
        Task<SocketUsageInfo> GetCurrentInfoAsync();
    }
}
