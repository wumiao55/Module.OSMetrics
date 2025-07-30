using System.Runtime.InteropServices;

namespace Module.OSMetrics.Memory
{
    internal class WindowsMemoryUsage : IMemoryUsage
    {
        /// <summary>
        /// 获取当前内存信息
        /// </summary>
        /// <returns></returns>
        public async Task<MemoryUsageInfo> GetCurrentInfoAsync()
        {
            var usageInfo = new MemoryUsageInfo();
            var memInfo = new MEMORYSTATUSEX();
            memInfo.dwLength = (uint)Marshal.SizeOf(memInfo);
            if (GlobalMemoryStatusEx(ref memInfo))
            {
                usageInfo.Total = Utils.BytesToGiB((long)memInfo.ullTotalPhys);
                usageInfo.Used = Utils.BytesToGiB((long)(memInfo.ullTotalPhys - memInfo.ullAvailPhys));
                usageInfo.UsagePercent = (double)usageInfo.Used / usageInfo.Total * 100;
            }
            return await Task.FromResult(usageInfo);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);
    }
}

    
