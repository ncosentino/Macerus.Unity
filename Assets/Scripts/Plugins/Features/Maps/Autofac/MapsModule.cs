using Autofac;

namespace Assets.Scripts.Plugins.Features.Maps.Autofac
{
    public sealed class MapsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MapPrefabFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TileLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapFormatter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapObjectStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
