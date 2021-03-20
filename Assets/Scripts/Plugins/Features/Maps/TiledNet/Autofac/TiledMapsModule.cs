using Autofac;

namespace Assets.Scripts.Plugins.Features.Maps.TiledNet.Autofac
{
    public sealed class TiledMapsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MappingAssetPaths>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapResourceLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
