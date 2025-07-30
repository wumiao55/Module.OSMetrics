1. 获取系统数据(包含系统基础信息, CPU占用信息,内存占用信息, 磁盘信息,网络信息等)
2. 支持Windows和Linux系统

示例代码:
```c#
// 系统基础信息
var baseInfo = OSUtils.GetServerBaseInfo();
// CPU占用信息
var cpuUsage = await new CpuUsage().GetCurrentInfoAsync();
// 内存占用信息
var memoryUsage = await new MemoryUsage().GetCurrentInfoAsync();
// 磁盘信息
var diskUsages = await new DiskUsage().GetCurrentInfoAsync();
```