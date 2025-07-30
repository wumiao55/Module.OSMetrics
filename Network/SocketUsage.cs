using System.Runtime.InteropServices;

namespace Module.OSMetrics.Network
{
    public class SocketUsage : ISocketUsage
    {
        /// <summary>
        /// 获取当前Socket信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public async Task<SocketUsageInfo> GetCurrentInfoAsync()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return await new LinuxSocketUsage().GetCurrentInfoAsync();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return await new WindowsSocketUsage().GetCurrentInfoAsync();
            }
            else
            {
                throw new PlatformNotSupportedException("当前操作系统不支持");
            }
        }
    }
}
