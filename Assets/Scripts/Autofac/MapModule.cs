using Assets.Scripts.Scenes.Explore.Maps;
using Assets.Scripts.Scenes.Explore.Maps.TiledNet;
using Autofac;

namespace Assets.Scripts.Autofac
{
    public sealed class MapModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<TileLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TilesetSpriteResourceResolver>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ExploreMapFormatter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<MapResourceIdConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TiledNetMapRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TiledNetToMapConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
