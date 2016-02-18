namespace Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.Implementation.QuickPulse
{
    using System;

    internal class QuickPulseCollectionTimeSlotManager
    {
        public virtual DateTime GetNextCollectionTimeSlot(DateTime currentTime)
        {
            return currentTime.Millisecond < 500 ? currentTime.AddMilliseconds(500 - currentTime.Millisecond) : currentTime.AddSeconds(1).AddMilliseconds(500 - currentTime.Millisecond);
        }
    }
}