using System.Linq;
using Assets.Scripts.Plugins.Features.AnimatedWeather;
using Assets.Scripts.Scenes.Explore.Api;
using Assets.Scripts.Unity.GameObjects;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.DayNightCycle
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class TimeOfDayStartupInterceptor : IExploreSceneStartupInterceptor
    {
        private readonly ITimeOfDayMonitorBehaviourStitcher _timeOfDayMonitorBehaviourStitcher;
        private readonly ILogger _logger;

        public TimeOfDayStartupInterceptor(
            ITimeOfDayMonitorBehaviourStitcher timeOfDayMonitorBehaviourStitcher,
            ILogger logger)
        {
            _timeOfDayMonitorBehaviourStitcher = timeOfDayMonitorBehaviourStitcher;
            _logger = logger;
        }

        public void Intercept(GameObject explore)
        {
            _logger.Debug($"Creating time of day system via scene startup interceptor...");

            // TODO: this is a really shitty thing to do... do it better
            var sunGameObject = explore
                .GetChildGameObjects()
                .First(x => x.name == "Sun");

            _timeOfDayMonitorBehaviourStitcher.Attach(
                explore,
                sunGameObject.GetRequiredComponent<Light>());

            _logger.Debug($"Created time of day system via scene startup interceptor.");
        }
    }
}
