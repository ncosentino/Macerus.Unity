using Autofac;

namespace Assets.Scripts.Plugins.Features.Hud.Autofac
{
    public sealed class HudModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DropItemHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
