namespace Module.OSMetrics.Network
{
    public class LinuxSocketUsage : ISocketUsage
    {
        /// <summary>
        /// 获取当前Socket信息
        /// </summary>
        /// <returns></returns>
        public async Task<SocketUsageInfo> GetCurrentInfoAsync()
        {
            try
            {
                var sockstatPath = "/proc/net/sockstat";
                if (!File.Exists(sockstatPath))
                {
                    throw new IOException("Linux sockstat file not found.");
                }
                
                int socketInUse = 0;
                int tcpInUse = 0;
                int timeWait = 0;
                int udpInUse = 0;
                var lines = await File.ReadAllLinesAsync(sockstatPath);
                foreach (var line in lines)
                {
                    if (line.StartsWith("sockets:"))
                    {
                        var parts = line.Split(' ');
                        socketInUse += int.Parse(parts[2]); // used
                    }
                    else if (line.StartsWith("TCP:"))
                    {
                        var parts = line.Split(' ');
                        tcpInUse += int.Parse(parts[2]);    // inuse
                        timeWait += int.Parse(parts[6]);    // tw
                    }
                    else if (line.StartsWith("UDP:"))
                    {
                        var parts = line.Split(' ');
                        udpInUse += int.Parse(parts[2]);   // inuse
                    }
                }
                return new SocketUsageInfo
                {
                    SocketCountInUse = socketInUse,
                    TCPCountInUse = tcpInUse,
                    TCPCountTimeWait = timeWait,
                    UDPCountInUse = udpInUse
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine("get socket use info error:" + ex.Message);
                return new SocketUsageInfo();
            }
        }
    }
}
