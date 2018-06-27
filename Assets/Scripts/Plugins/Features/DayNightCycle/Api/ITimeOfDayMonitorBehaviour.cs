using System;
using ProjectXyz.Plugins.Features.TimeOfDay;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.DayNightCycle.Api
{
    public interface ITimeOfDayMonitorBehaviour : IReadOnlyTimeOfDayMonitorBehaviour
    {
        new IReadOnlyTimeOfDayManager TimeOfDayManager { get; set; }

        new TimeSpan UpdateDelay { get; set; }

        new Light LightSource { get; set; }

        new double MinRange { get; set; }

        new double MaxRange { get; set; }
    }
}