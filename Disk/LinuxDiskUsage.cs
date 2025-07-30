using System.Diagnostics;

namespace Module.OSMetrics.Disk
{
    internal class LinuxDiskUsage : IDiskUsage
    {
        /// <summary>
        /// 获取当前磁盘信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<DiskUsageInfo>> GetCurrentInfoAsync()
        {
            var diskInfos = new List<DiskUsageInfo>();

            var processStartInfo = new ProcessStartInfo
            {
                FileName = "/bin/df",
                Arguments = "--output=source,size,used,avail,target --block-size=1",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                EnvironmentVariables =
                {
                    // 强制使用英文输出
                    ["LC_ALL"] = "C"
                }
            };

            try
            {
                using (var process = Process.Start(processStartInfo))
                {
                    if (process == null)
                    {
                        throw new InvalidOperationException("无法启动 df 进程");
                    }

                    var output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit(1000);
                    if (process.ExitCode != 0)
                    {
                        throw new InvalidOperationException($"df 命令执行失败，退出码: {process.ExitCode}");
                    }

                    var lines = output.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    if (lines.Length <= 1)
                    {
                        return diskInfos;
                    }

                    // 跳过第一行（标题）
                    for (int i = 1; i < lines.Length; i++)
                    {
                        var parts = lines[i].Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 5)
                        {
                            continue;
                        }

                        try
                        {
                            diskInfos.Add(new DiskUsageInfo
                            {
                                FileSystem = parts[0],
                                Total = Utils.BytesToGiB(long.Parse(parts[1])),
                                Used = Utils.BytesToGiB(long.Parse(parts[2])),
                                Avail = Utils.BytesToGiB(long.Parse(parts[3])),
                                Drive = parts[4]
                            });
                        }
                        catch
                        {
                        }
                    }
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
