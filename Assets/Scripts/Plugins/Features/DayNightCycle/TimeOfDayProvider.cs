using ProjectXyz.Plugins.Features.TimeOfDay.Api;

namespace Assets.Scripts.Plugins.Features.DayNightCycle
{
    public sealed class TimeOfDayProvider : ITimeOfDayProvider
    {
        private readonly IReadOnlyTimeOfDayManager _timeOfDayManager;

        public TimeOfDayProvider(IReadOnlyTimeOfDayManager timeOfDayManager)
        {
            _timeOfDayManager = timeOfDayManager;
        }

        public double CyclePercent => _timeOfDayManager.CyclePercent;
    }
}
