using Autofac;

using ProjectXyz.Framework.Autofac;

namespace Assets.Scripts.Plugins.Features.Hud.ResourceOrbs.Autofac
{
    public sealed class ResourceOrbModule : SingleRegistrationModule
    {
        protected override void SafeLoad(ContainerBuilder builder)
        {
            builder
                .RegisterType<ResourceOrbBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
