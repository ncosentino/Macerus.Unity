using Assets.Scripts.Scenes.Explore.GameObjects.TiledNet;
using Assets.Scripts.Scenes.Explore.Maps.TiledNet;
using Assets.Scripts.TiledNet;
using Autofac;
using ProjectXyz.Api.Framework;
using ProjectXyz.Game.Interface.Mapping;
using ProjectXyz.Shared.Framework.Collections;
using Tiled.Net.Maps;
using Tiled.Net.Tmx.Xml;

namespace Assets.Scripts.Autofac
{
    public sealed class MapModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MapResourceIdConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TiledNetMapRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TiledNetGameObjectRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<XmlTmxMapParser>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CachingTiledMapLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<CachingTiledMapLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<TiledNetToMapConverter>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .Register(x => new Cache<IIdentifier, ITiledMap>(5))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(x => new Cache<IIdentifier, IMap>(5))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
