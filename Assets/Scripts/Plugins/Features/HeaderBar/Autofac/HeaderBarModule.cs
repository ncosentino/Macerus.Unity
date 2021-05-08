using Assets.Scripts.Plugins.Features.HeaderBar.Noesis;
using Assets.Scripts.Plugins.Features.HeaderBar.Noesis.Resources;

using Autofac;

namespace Assets.Scripts.Plugins.Features.HeaderBar.Autofac
{
    public sealed class HeaderBarModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<HeaderBarView>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<HeaderBarNoesisViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
