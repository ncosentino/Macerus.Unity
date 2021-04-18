using Assets.Scripts.Plugins.Features.NewHud.Noesis;
using Assets.Scripts.Plugins.Features.NewHud.Noesis.Resources;
using Autofac;

namespace Assets.Scripts.Plugins.Features.NewHud.Autofac
{
    public sealed class HudModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<HudNoesisViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HudView>()
                //.AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
