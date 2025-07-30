using System.Diagnostics;

namespace Module.OSMetrics.Network
{
    /// <summary>
    /// 获取当前Socket信息
    /// </summary>
    public class WindowsSocketUsage : ISocketUsage
    {
        /// <summary>
        /// 获取当前Socket信息
        /// </summary>
        /// <returns></returns>
        public async Task<SocketUsageInfo> GetCurrentInfoAsync()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "netstat",
                    Arguments = "-ano",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = await process.StandardOutput.ReadToEndAsync();
            process.WaitForExit(5000);

            int tcpInUse = 0;
            int timeWait = 0;
            int udpInUse = 0;

            var lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (line.Contains("TCP"))
                {
                    tcpInUse++;
                    if (line.Contains("TIME_WAIT"))
                    {
                        timeWait++;
                    }
                }
                else if (line.Contains("UDP"))
                {
                    udpInUse++;
                }
            }

            return new SocketUsageInfo
            {
                TCPCountInUse = tcpInUse,
                TCPCountTimeWait = timeWait,
                UDPCountInUse = udpInUse,
            };
        }
    }
}
