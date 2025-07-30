namespace Module.OSMetrics.Network
{
    public class SocketUsageInfo
    {
        /// <summary>
        /// Socket使用数量
        /// </summary>
        public int? SocketCountInUse { get; set; }

        /// <summary>
        /// TCP使用数量
        /// </summary>
        public int TCPCountInUse { get; set; }

        /// <summary>
        /// TCP TIME_WAIT状态数量
        /// </summary>
        public int TCPCountTimeWait { get; set; }

        /// <summary>
        /// UDP使用数量
        /// </summary>
        public int UDPCountInUse { get; set; }
    }
}
