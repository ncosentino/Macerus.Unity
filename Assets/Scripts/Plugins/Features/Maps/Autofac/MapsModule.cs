using Autofac;

using ProjectXyz.Api.Framework;
using ProjectXyz.Game.Interface.Mapping;
using ProjectXyz.Shared.Framework.Collections;

namespace Assets.Scripts.Plugins.Features.Maps.Autofac
{
    public sealed class MapsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<MapFactory>()
                .AsImplementedInterfaces()
                .SingleInstance();
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
                .RegisterType<MapObjectStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder
                .Register(x => new Cache<IIdentifier, IMap>(5))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
