using Assets.Scripts.Scenes.Explore.Api;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    using ILogger = ProjectXyz.Api.Logging.ILogger;

    public sealed class AnimatedWeatherStartupInterceptor : IExploreSceneStartupInterceptor
    {
        private readonly IWeatherSystemGuiWelder _weatherSystemGuiWelder;
        private readonly ILogger _logger;

        public AnimatedWeatherStartupInterceptor(
            IWeatherSystemGuiWelder weatherSystemGuiWelder,
            ILogger logger)
        {
            _weatherSystemGuiWelder = weatherSystemGuiWelder;
            _logger = logger;
        }

        public void Intercept(GameObject explore)
        {
            _logger.Debug($"Creating weather system via scene startup interceptor...");
            _weatherSystemGuiWelder.Weld();
            _logger.Debug($"Created weather system via scene startup interceptor.");
        }
    }
}
