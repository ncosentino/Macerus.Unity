using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Plugins.Features.AnimatedWeather.Api;

using Autofac;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather.Autofac
{
    public sealed class InternalDependenciesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<WeatherMonitorBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherSystemGuiWelder>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherSystemFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<WeatherProvider>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<FadeInBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<FadeAndKillBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<Fader>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<AnimatedWeatherFactoryFacade>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .OnActivated(x =>
                {
                    x.Context
                     .Resolve<IEnumerable<IDiscoverableAnimatedWeatherPlugin>>()
                     .Foreach(d => x.Instance.Register(
                         d.WeatherResourceId,
                         d.Create));
                });
        }
    }
}
