using Autofac;

namespace Assets.Scripts.Plugins.Features.Controls.Autofac
{
    public sealed class ControlsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<PlayerControlConfiguration>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
