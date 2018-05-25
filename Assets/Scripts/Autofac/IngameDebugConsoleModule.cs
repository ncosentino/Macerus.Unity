using Assets.Scripts.Unity.IngameDebugConsole;
using Autofac;

namespace Assets.Scripts.Autofac
{
    public sealed class IngameDebugConsoleModule : Module
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
