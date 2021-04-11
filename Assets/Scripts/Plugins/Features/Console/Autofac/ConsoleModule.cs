using Autofac;

namespace Assets.Scripts.Plugins.Features.Console.Autofac
{
    public sealed class ConsoleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ConsoleCommandRegistrar>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
