using System.Runtime.InteropServices;

namespace Module.OSMetrics.Memory
{
    public class MemoryUsage : IMemoryUsage
    {
        /// <summary>
        /// 获取当前内存信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public async Task<MemoryUsageInfo> GetCurrentInfoAsync()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return await new LinuxMemoryUsage().GetCurrentInfoAsync();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return await new WindowsMemoryUsage().GetCurrentInfoAsync();
            }
            else
            {
                throw new PlatformNotSupportedException("当前操作系统不支持");
            }
        }
    }
}
