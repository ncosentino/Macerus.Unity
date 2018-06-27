using Autofac;

namespace Assets.Scripts.Plugins.Features.DayNightCycle.Autofac
{
    public sealed class InternalDependenciesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .RegisterType<TimeOfDayMonitorBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
