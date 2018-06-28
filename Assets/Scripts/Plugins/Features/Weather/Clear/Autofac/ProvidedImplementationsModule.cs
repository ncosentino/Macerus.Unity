using Autofac;

namespace Assets.Scripts.Plugins.Features.Weather.Clear.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<ClearWeatherFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
