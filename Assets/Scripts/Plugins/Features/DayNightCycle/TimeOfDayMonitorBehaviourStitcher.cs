using System;
using Assets.Scripts.Plugins.Features.DayNightCycle.Api;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.DayNightCycle
{
    public sealed class TimeOfDayMonitorBehaviourStitcher : ITimeOfDayMonitorBehaviourStitcher
    {
        private readonly ITimeOfDayProvider _timeOfDayProvider;

        public TimeOfDayMonitorBehaviourStitcher(ITimeOfDayProvider timeOfDayProvider)
        {
            _timeOfDayProvider = timeOfDayProvider;
        }

        public IReadOnlyTimeOfDayMonitorBehaviour Attach(
            GameObject timeeOfDayMonitorObject,
            Light lightSource)
        {
            var timeOfDayMonitorBehaviour = timeeOfDayMonitorObject.AddComponent<TimeOfDayMonitorBehaviour>();
            timeOfDayMonitorBehaviour.TimeOfDayProvider = _timeOfDayProvider;
            
            timeOfDayMonitorBehaviour.MinRange = 100;
            timeOfDayMonitorBehaviour.MaxRange = 300;
            timeOfDayMonitorBehaviour.UpdateDelay = TimeSpan.FromSeconds(0.25);
            timeOfDayMonitorBehaviour.LightSource = lightSource;

            return timeOfDayMonitorBehaviour;
        }
    }
}