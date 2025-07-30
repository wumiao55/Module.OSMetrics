using System.Runtime.InteropServices;

namespace Module.OSMetrics.CPU
{
    public class CpuUsage : ICpuUsage
    {
        /// <summary>
        /// 获取当前CPU信息
        /// </summary>
        /// <param name="sampleIntervalMs">采样间隔(毫秒)</param>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException"></exception>
        public async Task<CpuUsageInfo> GetCurrentInfoAsync(int sampleIntervalMs = 500)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return await new LinuxCpuUsage().GetCurrentInfoAsync(sampleIntervalMs);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return await new WindowsCpuUsage().GetCurrentInfoAsync(sampleIntervalMs);
            }
            else
            {
                throw new PlatformNotSupportedException("当前操作系统不支持");
            }
        }
    }
}
