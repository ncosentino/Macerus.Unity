using Autofac;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework.Collections;

using Tiled.Net.Maps;
using Tiled.Net.Tmx.Xml;

namespace Assets.Scripts.Plugins.Features.Maps.TiledNet.Autofac
{
    public sealed class TiledMapsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
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
                .RegisterType<TiledNetGameObjectRepository>()
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
                .RegisterType<XmlTmxMapParser>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .Register(x => new Cache<IIdentifier, ITiledMap>(5))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
