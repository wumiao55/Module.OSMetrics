namespace Module.OSMetrics
{
    internal class Utils
    {
        private const double BytesToGiBMult = 1024.0 * 1024.0 * 1024.0;
        private const double KBToGiBMult = 1024.0 * 1024.0;

        public static double BytesToGiB(long bytes)
        {
            return bytes / BytesToGiBMult;
        }

        public static double KBToGiB(long bytes)
        {
            return bytes / KBToGiBMult;
        }
    }
}
