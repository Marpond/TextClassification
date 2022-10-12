using System.Diagnostics;

namespace TextClassificationGUI;

public static class TimeUtils
{
    public static long GetNanoseconds()
    {
        double timestamp = Stopwatch.GetTimestamp();
        var nanoseconds = 1_000_000_000.0 * timestamp / Stopwatch.Frequency;

        return (long)nanoseconds;
    }
}