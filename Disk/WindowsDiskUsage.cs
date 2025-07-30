namespace Module.OSMetrics.Disk
{
    internal class WindowsDiskUsage : IDiskUsage
    {
        /// <summary>
        /// 获取当前磁盘信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<DiskUsageInfo>> GetCurrentInfoAsync()
        {
            var diskInfos = new List<DiskUsageInfo>();
            try
            {
                foreach (var drive in DriveInfo.GetDrives())
                {
                    // 跳过无法访问的驱动器
                    if (!drive.IsReady)
                    {
                        continue;
                    }

                    // 可选：跳过 CD-ROM、网络驱动器等
                    if (drive.DriveType == DriveType.CDRom || drive.DriveType == DriveType.Network)
                    {
                        continue;
                    }

                    diskInfos.Add(new DiskUsageInfo
                    {
                        Total = Utils.BytesToGiB(drive.TotalSize),
                        Used = Utils.BytesToGiB(drive.TotalSize - drive.AvailableFreeSpace),
                        Avail = Utils.BytesToGiB(drive.AvailableFreeSpace),
                        Drive = drive.Name,
                    });
                }
            }
            catch
            {
                return await Task.FromResult(diskInfos);
            }

            return await Task.FromResult(diskInfos);
        }
    }
}
