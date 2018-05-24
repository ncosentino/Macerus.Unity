using Assets.Scripts.Unity.Resources;
using Assets.Scripts.Unity.Resources.Sprites;
using Autofac;
using ProjectXyz.Shared.Framework.Collections;

namespace Assets.Scripts.Autofac.Unity
{
    public sealed class ResourceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(x => new Cache<string, ISpriteSheet>(10))
                .AsImplementedInterfaces();
                //.SingleInstance();
            builder
                .RegisterType<SpriteLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<ResourcePrefabLoader>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
