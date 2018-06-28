using System;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.DayNightCycle.Api
{
    public interface IReadOnlyTimeOfDayMonitorBehaviour
    {
        ITimeOfDayProvider TimeOfDayProvider { get; }

        TimeSpan UpdateDelay { get; }

        Light LightSource { get; }

        double MinRange { get; }

        double MaxRange { get; }
    }
}