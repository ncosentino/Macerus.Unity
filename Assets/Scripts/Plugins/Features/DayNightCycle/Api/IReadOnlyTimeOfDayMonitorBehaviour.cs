using System;
using ProjectXyz.Plugins.Features.TimeOfDay;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.DayNightCycle.Api
{
    public interface IReadOnlyTimeOfDayMonitorBehaviour
    {
        IReadOnlyTimeOfDayManager TimeOfDayManager { get; }

        TimeSpan UpdateDelay { get; }

        Light LightSource { get; }

        double MinRange { get; }

        double MaxRange { get; }
    }
}