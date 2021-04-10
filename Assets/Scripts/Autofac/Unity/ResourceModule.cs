using Assets.Scripts.Unity.Resources;
using Assets.Scripts.Unity.Resources.Prefabs;
using Assets.Scripts.Unity.Resources.Sprites;

using Autofac;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Framework.Collections;

using UnityEngine.Tilemaps;

namespace Assets.Scripts.Autofac.Unity
{
    public sealed class ResourceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(x => new Cache<string, ISpriteSheet>(20))
                .AsImplementedInterfaces();
            //.SingleInstance();
            // FIXME: we want to cache by IIdentifier
            //builder
            //    .Register(x => new Cache<IIdentifier, ISpriteSheet>(20))
            //    .AsImplementedInterfaces();
            //.SingleInstance();
            builder
                .Register(x => new Cache<string, Tile>(100))
                .AsImplementedInterfaces();
            builder
                .RegisterType<SpriteLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ResourceLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<PrefabCreator>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
