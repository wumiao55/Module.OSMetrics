namespace Module.OSMetrics.Disk
{
    public class DiskUsageInfo
    {
        /// <summary>
        /// 文件系统
        /// </summary>
        public string FileSystem { get; set; } = string.Empty;

        /// <summary>
        /// 总大小(单位:GiB)
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// 已使用大小(单位:GiB)
        /// </summary>
        public double Used { get; set; }

        /// <summary>
        /// 可使用大小(单位:GiB)
        /// </summary>
        public double Avail { get; set; }

        /// <summary>
        /// 盘符类型
        /// </summary>
        public string Drive { get; set; } = string.Empty;
    }
}
