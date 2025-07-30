using System.Net.NetworkInformation;

namespace Module.OSMetrics.Network
{
    /// <summary>
    /// 网络统计服务
    /// </summary>
    public class NetworkStatsService
    {
        private Timer? _timer;
        private readonly TimeSpan _interval;
        private long _previousSentBytes = 0;
        private long _previousReceivedBytes = 0;

        public event EventHandler<NetworkInfo>? OnTotalSpeedUpdated;

        public NetworkStatsService(TimeSpan interval)
        {
            if (interval.TotalSeconds < 1)
            {
                throw new ArgumentException("interval must be greater than or equal to 1 second.");
            }
            _interval = interval;
        }

        public void Start()
        {
            _timer = new Timer((state) => UpdateNetworkStats(), null, TimeSpan.Zero, _interval);
        }

        public void Stop()
        {
            _timer?.Dispose();
        }

        private void UpdateNetworkStats()
        {
            try
            {
                var activeNics = GetActiveNetworkInterfaces();

                long currentSentBytes = 0;
                long currentReceivedBytes = 0;
                foreach (var nic in activeNics)
                {
                    try
                    {
                        var ipv4Stats = nic.GetIPv4Statistics();
                        currentSentBytes += ipv4Stats.BytesSent;
                        currentReceivedBytes += ipv4Stats.BytesReceived;
                    }
                    catch (Exception ex)
                    {
                        // 忽略单个接口的异常
                        Console.WriteLine($"Error reading stats for {nic.Name}: {ex.Message}");
                    }
                }

                if (_previousSentBytes == 0 || _previousReceivedBytes == 0)
                {
                    _previousSentBytes = currentSentBytes;
                    _previousReceivedBytes = currentReceivedBytes;
                    return;
                }
                long uploadSpeed = (currentSentBytes - _previousSentBytes) / (int)_interval.TotalSeconds;
                long downloadSpeed = (currentReceivedBytes - _previousReceivedBytes) / (int)_interval.TotalSeconds;

                _previousSentBytes = currentSentBytes;
                _previousReceivedBytes = currentReceivedBytes;
                OnTotalSpeedUpdated?.Invoke(this, new NetworkInfo
                {
                    UploadBytesSpeed = uploadSpeed,
                    DownloadBytesSpeed = downloadSpeed
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in network stats update: {ex.Message}");
            }
        }

        private static List<NetworkInterface> GetActiveNetworkInterfaces()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && 
                              nic.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                              nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
                              nic.NetworkInterfaceType != NetworkInterfaceType.Ppp).ToList();
        }
    }
}
