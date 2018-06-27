using Autofac;

namespace Assets.Scripts.Plugins.Features.AnimatedWeather.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<AnimatedWeatherStartupInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
