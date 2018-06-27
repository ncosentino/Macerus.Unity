using Assets.Scripts.Plugins.Features.AnimatedWeather;
using Autofac;

namespace Assets.Scripts.Plugins.Features.AnimatedRain.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<AnimatedRainFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
