using System.Runtime.InteropServices;

namespace Module.OSMetrics.Disk
{
    public class DiskUsage : IDiskUsage
    {
        /// <summary>
        /// 获取当前磁盘信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public async Task<List<DiskUsageInfo>> GetCurrentInfoAsync()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return await new LinuxDiskUsage().GetCurrentInfoAsync();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return await new WindowsDiskUsage().GetCurrentInfoAsync();
            }
            else
            {
                throw new PlatformNotSupportedException("当前操作系统不支持");
            }
        }
    }
}
