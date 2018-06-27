using Assets.Scripts.Plugins.Features.DayNightCycle.Api;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.DayNightCycle
{
    public interface ITimeOfDayMonitorBehaviourStitcher
    {
        IReadOnlyTimeOfDayMonitorBehaviour Attach(
            GameObject timeeOfDayMonitorObject,
            Light lightSource);
    }
}