using Autofac;

namespace Assets.Scripts.Logging.Autofac
{
    public sealed class ProvidedImplementationsModule : Module
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
