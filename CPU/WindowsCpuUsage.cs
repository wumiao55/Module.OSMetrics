using System.Diagnostics;

namespace Module.OSMetrics.CPU
{
    internal class WindowsCpuUsage : ICpuUsage
    {
        /// <summary>
        /// 获取当前CPU信息
        /// </summary>
        /// <param name="sampleIntervalMs">采样间隔(毫秒)</param>
        /// <returns></returns>
        public async Task<CpuUsageInfo> GetCurrentInfoAsync(int sampleIntervalMs = 500)
        {
            return new CpuUsageInfo
            {
                CoreCount = Environment.ProcessorCount,
                UsagePercent = await GetCpuUsageAsync(sampleIntervalMs)
            };
        }

        private static async Task<double> GetCpuUsageAsync(int sampleIntervalMs = 500)
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _ = cpuCounter.NextValue();
            await Task.Delay(sampleIntervalMs);
            return cpuCounter.NextValue();
        }
    }
}
