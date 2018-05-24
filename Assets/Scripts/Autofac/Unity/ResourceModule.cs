using Assets.Scripts.Unity.Resources;
using Assets.Scripts.Unity.Resources.Sprites;
using Autofac;

namespace Assets.Scripts.Autofac.Unity
{
    public sealed class ResourceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(x => new SpriteSheetCache(10))
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
