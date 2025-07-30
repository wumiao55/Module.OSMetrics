using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;

namespace Module.OSMetrics.OS
{
    public class OSUtils
    {
        /// <summary>
        /// 获取服务器基本信息
        /// </summary>
        /// <returns></returns>
        public static ServerBaseInfo GetServerBaseInfo()
        {
            var info = new ServerBaseInfo();
            var ips = new List<string>();
            try
            {
                ips = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(ni => ni.OperationalStatus == OperationalStatus.Up)
                    .SelectMany(ni => ni.GetIPProperties()?.UnicastAddresses)
                    .Where(ua => ua?.Address != null && ua.Address.AddressFamily == AddressFamily.InterNetwork)
                    .Select(ua => ua.Address.ToString()).ToList();
            }
            catch
            {
            }
            var t = TimeSpan.FromMilliseconds(Environment.TickCount64);
            info.SystemStartTime = DateTime.Now.Subtract(t);
            info.SystemUptime = Math.Round(t.TotalHours, 2);
            info.SystemUptimeDesc = $"{t.Days}天{t.Hours}小时{t.Minutes}分钟";
            info.Name = Dns.GetHostName();
            info.IPs = ips;
            info.SystemName = RuntimeInformation.OSDescription;
            info.SystemArchitecture = RuntimeInformation.OSArchitecture.ToString();

            return info;
        }
    }
}
