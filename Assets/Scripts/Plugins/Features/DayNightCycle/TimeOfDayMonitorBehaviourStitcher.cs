using System;
using Assets.Scripts.Plugins.Features.DayNightCycle.Api;
using ProjectXyz.Plugins.Features.TimeOfDay;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.DayNightCycle
{
    public sealed class TimeOfDayMonitorBehaviourStitcher : ITimeOfDayMonitorBehaviourStitcher
    {
        private readonly IReadOnlyTimeOfDayManager _timeOfDayManager;

        public TimeOfDayMonitorBehaviourStitcher(IReadOnlyTimeOfDayManager timeOfDayManager)
        {
            _timeOfDayManager = timeOfDayManager;
        }

        public IReadOnlyTimeOfDayMonitorBehaviour Attach(
            GameObject timeeOfDayMonitorObject,
            Light lightSource)
        {
            var timeOfDayMonitorBehaviour = timeeOfDayMonitorObject.AddComponent<TimeOfDayMonitorBehaviour>();
            timeOfDayMonitorBehaviour.TimeOfDayManager = _timeOfDayManager;
            
            timeOfDayMonitorBehaviour.MinRange = 100;
            timeOfDayMonitorBehaviour.MaxRange = 300;
            timeOfDayMonitorBehaviour.UpdateDelay = TimeSpan.FromSeconds(0.25);
            timeOfDayMonitorBehaviour.LightSource = lightSource;

            return timeOfDayMonitorBehaviour;
        }
    }
}