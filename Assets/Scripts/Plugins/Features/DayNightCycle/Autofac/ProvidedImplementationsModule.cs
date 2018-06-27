using Autofac;

namespace Assets.Scripts.Plugins.Features.DayNightCycle.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<TimeOfDayStartupInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
