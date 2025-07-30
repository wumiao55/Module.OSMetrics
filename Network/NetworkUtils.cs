namespace Module.OSMetrics.Network
{
    public static class NetworkUtils
    {
        /// <summary>
        /// 将字节/秒转换为千字节/秒 (KBps)
        /// </summary>
        /// <param name="bytesPerSecond"></param>
        /// <returns></returns>
        public static double ToKBps(long bytesPerSecond)
        {
            return bytesPerSecond / 1000.0;
        }

        /// <summary>
        /// 将字节/秒转换为兆字节/秒 (MB/s)
        /// </summary>
        /// <param name="bytesPerSecond"></param>
        /// <returns></returns>
        public static double ToMBps(long bytesPerSecond)
        {
            return bytesPerSecond / 1_000_000.0;
        }

        /// <summary>
        /// 自动选择合适的单位并格式化输出 (KB/s, MB/s 等)
        /// </summary>
        /// <param name="bytesPerSecond"></param>
        /// <param name="decimalPlaces"></param>
        /// <returns></returns>
        public static string FormatSpeed(long bytesPerSecond, int decimalPlaces = 2)
        {
            if (bytesPerSecond <= 0)
            {
                return "0 B/s";
            }

            var KBps = ToKBps(bytesPerSecond);
            if (KBps < 1000)
            {
                return $"{Math.Round(KBps, decimalPlaces)} KB/s";
            }

            var MBps = ToMBps(bytesPerSecond);
            return $"{Math.Round(MBps, decimalPlaces)} MB/s";
        }
    }
}
