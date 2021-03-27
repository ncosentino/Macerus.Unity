using Assets.Scripts.Plugins.Features.Animations.Interceptors;
using Assets.Scripts.Plugins.Features.GameObjects.Actors.UnityBehaviours;

using Macerus.Plugins.Features.Animations.Lpc;

using Autofac;

namespace Assets.Scripts.Plugins.Features.Animations.Autofac
{
    public sealed class AnimationsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c => new LpcAnimationDiscovererSettings(
                    @$"assets/resources",
                    @"graphics/actors/LpcUniversal/"))
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SpriteAnimationBehaviourStitcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder
                .RegisterType<SpriteAnimationBehaviorInterceptor>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .AsSelf()
                .AutoActivate();
        }
    }
}
