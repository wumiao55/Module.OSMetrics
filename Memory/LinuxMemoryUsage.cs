namespace Module.OSMetrics.Memory
{
    internal class LinuxMemoryUsage : IMemoryUsage
    {
        /// <summary>
        /// 获取当前内存信息
        /// </summary>
        /// <returns></returns>
        public async Task<MemoryUsageInfo> GetCurrentInfoAsync()
        {
            var (total, used, free, buffers, cached, usedPercent) = await GetMemoryInfo();
            return new MemoryUsageInfo()
            {
                Total = Utils.KBToGiB(total),
                Used = Utils.KBToGiB(used),
                UsagePercent = usedPercent
            };
        }

        private static async Task<(long Total, long Used, long Free, long Buffers, long Cached, double UsedPercent)> GetMemoryInfo()
        {
            var lines = await File.ReadAllLinesAsync("/proc/meminfo");
            long total = 0, free = 0, buffers = 0, cached = 0;

            foreach (var line in lines)
            {
                if (line.StartsWith("MemTotal:"))
                    total = long.Parse(line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                else if (line.StartsWith("MemFree:"))
                    free = long.Parse(line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                else if (line.StartsWith("Buffers:"))
                    buffers = long.Parse(line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                else if (line.StartsWith("Cached:"))
                    cached = long.Parse(line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);

                if (total > 0 && free > 0 && buffers > 0 && cached > 0)
                    break;
            }

            var used = total - (free + buffers + cached);
            var usedPercent = (double)used / total * 100;
            return (total, used, free, buffers, cached, usedPercent);
        }
    }
}
