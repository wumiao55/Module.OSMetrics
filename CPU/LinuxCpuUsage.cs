namespace Module.OSMetrics.CPU
{
    internal class LinuxCpuUsage : ICpuUsage
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

        /// <summary>
        /// 获取CPU使用率
        /// </summary>
        /// <param name="sampleIntervalMs">采样间隔(毫秒)</param>
        /// <returns>CPU使用率</returns>
        private static async Task<double> GetCpuUsageAsync(int sampleIntervalMs = 500)
        {
            var before = ReadCpuStats();
            await Task.Delay(sampleIntervalMs);
            var after = ReadCpuStats();

            var totalBefore = before.User + before.Nice + before.System + before.Idle + before.Iowait + before.Irq + before.Softirq;
            var totalAfter = after.User + after.Nice + after.System + after.Idle + after.Iowait + after.Irq + after.Softirq;

            var idleBefore = before.Idle + before.Iowait;
            var idleAfter = after.Idle + after.Iowait;

            var totalDelta = totalAfter - totalBefore;
            var idleDelta = idleAfter - idleBefore;

            return (double)(totalDelta - idleDelta) / totalDelta * 100;
        }

        private static (long User, long Nice, long System, long Idle, long Iowait, long Irq, long Softirq) ReadCpuStats()
        {
            var line = File.ReadAllText("/proc/stat").Split('\n').First(l => l.StartsWith("cpu "));
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return (
                User: long.Parse(parts[1]),
                Nice: long.Parse(parts[2]),
                System: long.Parse(parts[3]),
                Idle: long.Parse(parts[4]),
                Iowait: long.Parse(parts[5]),
                Irq: long.Parse(parts[6]),
                Softirq: long.Parse(parts[7])
            );
        }
    }
}
