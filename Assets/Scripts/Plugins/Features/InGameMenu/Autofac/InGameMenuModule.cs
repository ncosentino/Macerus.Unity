using Assets.Scripts.Plugins.Features.InGameMenu.Noesis;
using Assets.Scripts.Plugins.Features.InGameMenu.Noesis.Resources;

using Autofac;

namespace Assets.Scripts.Plugins.Features.InGameMenu.Autofac
{
    public sealed class InGameMenuModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<InGameMenuView>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<InGameMenuNoesisViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
