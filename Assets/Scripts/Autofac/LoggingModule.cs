using Assets.Scripts.Logging;
using Autofac;

namespace Assets.Scripts.Autofac
{
    public sealed class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ConsoleLogger>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
