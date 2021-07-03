using Assets.Scripts.Plugins.Features.Minimap.Noesis;
using Assets.Scripts.Plugins.Features.Minimap.Noesis.Resources;

using Autofac;

namespace Assets.Scripts.Plugins.Features.Minimap.Autofac
{
    public sealed class MinimapModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MinimapOverlayNoesisViewModel>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MinimapOverlayView>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
