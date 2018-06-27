using Autofac;

namespace Assets.Scripts.Plugins.Features.IngameDebugConsole.Autofac
{
    public sealed class ProividedImplementationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DebugConsoleManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
