using Assets.Scripts.Gui.Unity.ViewWelding.Api;
using Assets.Scripts.Scenes.Explore.Gui.Api;

using ProjectXyz.Framework.ViewWelding.Api;

using UnityEngine;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather
{
    public sealed class WeatherSystemGuiWelder : IWeatherSystemGuiWelder
    {
        private readonly IGuiCanvasProvider _guiCanvasProvider;
        private readonly IWeatherSystemFactory _weatherSystemFactory;
        private readonly IViewWelderFactory _viewWelderFactory;

        public WeatherSystemGuiWelder(
            IGuiCanvasProvider guiCanvasProvider,
            IWeatherSystemFactory weatherSystemFactory,
            IViewWelderFactory viewWelderFactory)
        {
            _guiCanvasProvider = guiCanvasProvider;
            _weatherSystemFactory = weatherSystemFactory;
            _viewWelderFactory = viewWelderFactory;
        }

        public GameObject Weld()
        {
            var canvas = _guiCanvasProvider.GetCanvas();
            var weatherSystem = _weatherSystemFactory.Create();
            _viewWelderFactory
                .Create<IStackViewWelder>(
                    canvas,
                    weatherSystem)
                .Weld(new StackViewWeldingOptions()
                {
                    OrderFirst = true,
                });
            return weatherSystem;
        }
    }
}
